using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace DCEAccessLib
{
	/// <summary>
	/// ListView, связанный с таблицей БД
	/// </summary>
	public class DataList : System.Windows.Forms.ListView
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		/// 

      #region DataColumnHeaderCollection class...

      /// <summary>
      /// The list box's items collection class
      /// </summary>
      public class DataColumnHeaderCollection : IList, ICollection, IEnumerable
      {
         DataList owner = null;

         public DataColumnHeaderCollection(DataList owner)
         {
            this.owner = owner;
         }

            #region ICollection implemented members...

         void ICollection.CopyTo(Array array, int index) 
         {
            for (IEnumerator e = this.GetEnumerator(); e.MoveNext();)
               array.SetValue(e.Current, index++);
         }

         bool ICollection.IsSynchronized 
         {
            get { return false; }
         }

         object ICollection.SyncRoot 
         {
            get { return this; }
         }

            #endregion

            #region IList implemented members...

         object IList.this[int index] 
         {
            get { return this[index]; }
            set {/* this[index] = (DataColumnHeader)value;*/ }
         }

         bool IList.Contains(object item)
         {
            throw new NotSupportedException();
         }

         int IList.Add(object item)
         {
            return this.Add((DataColumnHeader)item);
         }

         bool IList.IsFixedSize 
         {
            get { return false; }
         }

         int IList.IndexOf(object item)
         {
            throw new NotSupportedException();
         }

         void IList.Insert(int index, object item)
         {
            this.Insert(index, (DataColumnHeader)item);
         }

         void IList.Remove(object item)
         {
            throw new NotSupportedException();
         }

         void IList.RemoveAt(int index)
         {
            this.RemoveAt(index);
         }

            #endregion

         [Browsable(false)]
         public int Count 
         {
            get { return owner.DoGetItemCount(); }
         }

         public bool IsReadOnly 
         {
            get { return false; }
         }

         public DataColumnHeader this[int index]
         {
            get { return owner.DoGetElement(index); }
//            set { owner.DoSetElement(index, value); }
         }

         public IEnumerator GetEnumerator() 
         {
            return owner.DoGetEnumerator(); 
         }

         public bool Contains(object item)
         {
            throw new NotSupportedException();
         }

         public int IndexOf(object item)
         {
            throw new NotSupportedException();
         }

         public void Remove(DataColumnHeader item)
         {
            owner.DoRemoveItem(item); 
         }

         public void Insert(int index, DataColumnHeader item)
         {
            owner.DoInsertItem(index, item);
         }

         public int Add(DataColumnHeader item)
         {
            return owner.DoInsertItem(this.Count, item);
         }

         public void AddRange(DataColumnHeader[] items)
         {
            for(IEnumerator e = items.GetEnumerator(); e.MoveNext();)
               owner.DoInsertItem(this.Count, (DataColumnHeader)e.Current);
         }

         public void Clear()
         {
            owner.DoClear();
         }

         public void RemoveAt(int index)
         {
            owner.DoRemoveItem(index);
         }
      }

   #endregion

      #region Methods to access base class items...

//      private void DoSetElement(int index, DataColumnHeader value)
//      {
//         base.Columns[index] = value;
//      }

      private DataColumnHeader DoGetElement(int index)
      {
         return (DataColumnHeader)base.Columns[index];
      }

      private IEnumerator DoGetEnumerator()
      {
         return base.Columns.GetEnumerator();
      }

      private int DoGetItemCount()
      {
         return base.Columns.Count;
      }

      private int DoInsertItem(int index, DataColumnHeader item)
      {
//         item.imageList = this.imageList;
//         item.itemIndex = index;
         base.Columns.Insert(index, item);
         return index;
      }

      private void DoRemoveItem(int index)
      {
         base.Columns.RemoveAt(index);
      }

      private void DoRemoveItem(DataColumnHeader item)
      {
         base.Columns.Remove(item);
      }

      private void DoClear()
      {
         base.Columns.Clear();
      }

        #endregion

      private DataColumnHeaderCollection dataColumns;
      private System.Data.DataView dataView;
      private ListChangedEventHandler handler = null;
      private ListViewItemComparer comparer;
      private int HeaderHWnd=0;

      [Category("Behavior")]  
      [Localizable(true)]
      [MergableProperty(false)]
      [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
      new public DataColumnHeaderCollection Columns {
         get {
            return dataColumns;
         }
      }

      public delegate void RowParseHandler(DataRowView row, ListViewItem item);
      public event RowParseHandler RowParsed;

      bool allowsort = true;
      /// <summary>
      /// Включение сортировки по столбцам
      /// </summary>
      public bool AllowSorting
      {
         get
         {
            return allowsort;
         }
         set
         {
            allowsort = value;
            if (!value)
            {
               this.Sorting = SortOrder.None;
               this.ListViewItemSorter = null;
            }
            else
            {
               this.Sorting = this.comparer.Order;
               this.ListViewItemSorter = this.comparer;
            }
         }
      }
      public System.Data.DataView DataView {
         get {
            return dataView;
         }
         set {
            if ( dataView!=null)
            {
               dataView.ListChanged -= handler;
            }
            dataView = value;
            if ( dataView!=null)
               this.dataView.ListChanged += handler;
         }
      }

      public void UpdateList()
      {
         dataView_ListChanged(null,null);
      }
      public bool SuspendListChange = false;

      private void dataView_ListChanged(object sender, System.ComponentModel.ListChangedEventArgs e)
      {
         if (SuspendListChange)
            return;
         DataRowView selrow = null;
         ListViewItem selitm = null;
         if (this.SelectedItems.Count>0)
         {
            selrow = (DataRowView) this.SelectedItems[0].Tag;
         }
         this.BeginUpdate();
         this.SelectedItems.Clear();
         this.Items.Clear();
         if (this.Columns.Count>0)
         {
            foreach ( DataRowView row in dataView)
            {
				if(null == row.Row) {
					break;
				}
               string[] items = new string[this.Columns.Count];

               for (int i=0; i<items.Length; i++)
               {

                  items[i] =this.Columns[i].Parse(row);
//                  object val = row[this.Columns[i].FieldName];
//                  if (val != null )
//                  {
//                     Type type = val.GetType();
//                     if (type == typeof(System.DateTime))
//                        items[i] =((DateTime)val).ToString(DCEAccessLib.Settings.DateFormat);
//                     else
//                     if (type == typeof(System.Boolean))
//                     {
//                        if ((bool)val)
//                           items[i] = "Да";
//                        else
//                           items[i] = "Нет";
//                     }
//                     else
//                        items[i] = val.ToString();
//                  }
//                  else
//                  {
//                     items[i] = "";
//                  }
               }
               ListViewItem itm = new ListViewItem(items);
               itm.Tag = row;
               if (RowParsed !=null)
                  RowParsed(row,itm);
               this.Items.Add(itm);
               if (selrow!=null)
               if (row.Row == selrow.Row)
               {
                  selitm = itm;
               }
            }
         }
         if (this.AllowSorting)
            this.Sort();
         if (selitm != null)
         {
            selitm.Selected = true;
         }
         this.EndUpdate();
         this.EndUpdate();
         this.Invalidate();
      }

      public DataList()
      {
         this.HideSelection = false;
         dataColumns = new DataColumnHeaderCollection(this);
         handler = new System.ComponentModel.ListChangedEventHandler(dataView_ListChanged);
         comparer = new ListViewItemComparer ();
         this.ListViewItemSorter = comparer;
         this.ColumnClick += new ColumnClickEventHandler(OnColumnClick);
      }

      private void OnColumnClick(  object sender,  ColumnClickEventArgs e  )
      {
         if (!this.AllowSorting)
            return;

         if (e.Column == this.comparer.SortColumn)
         {
            if (this.comparer.Order == SortOrder.Ascending)
               this.comparer.Order = SortOrder.Descending;
            else
               this.comparer.Order = SortOrder.Ascending;
         }
         else
         {
            this.comparer.SortColumn = e.Column;
         }
         this.Sort();
         if (this.HeaderHWnd !=0)
            User32.InvalidateRect((IntPtr)this.HeaderHWnd,(IntPtr)0,0);
      }

      protected override void OnCreateControl()
      {
         base.OnCreateControl();
         RegistryKey key = 
            Registry.CurrentUser.CreateSubKey("Software\\DCE\\DataView");
         string cols="";
         string fields="";
         foreach (DataColumnHeader col in this.Columns)
         {
            cols += col.Text;
            fields += col.FieldName;
         }
         string widths = (string)key.GetValue(cols.GetHashCode().ToString()+"."+fields.GetHashCode().ToString(),null);
         if (widths != null && widths.Length != 0)
         {
            widths = widths.Substring(0,widths.Length-1); // removing last ,
            string[] ww = widths.Split(new char[]{','},this.Columns.Count);
            if( ww.Length == this.Columns.Count)
            {
               int i=0;
               foreach (DataColumnHeader col in this.Columns)
               {
                  col.Width =System.Convert.ToInt32(ww[i++]);
               }
            }
         }
      }

      protected override void Dispose( bool disposing )
      {
         if (disposing)
         {
            string cols="";
            string fields="";
            string widths="";
            foreach (DataColumnHeader col in this.Columns)
            {
               cols += col.Text;
               fields += col.FieldName;
               widths += col.Width.ToString() + ",";
            }
            RegistryKey key = 
               Registry.CurrentUser.CreateSubKey("Software\\DCE\\DataView");
            key.SetValue(cols.GetHashCode().ToString()+"."+fields.GetHashCode().ToString(), widths);            
         }

         if (this.dataView != null)
         {
            dataView.ListChanged -= handler;              
            dataView = null;
         }
      	base.Dispose( disposing );
      }

      protected unsafe override void WndProc(ref Message m)
      {

         base.WndProc(ref m);
         if (m.Msg == 0x4e /*WM_NOTIFY */)
         {
            User32.NMCUSTOMDRAW * hdr = (User32.NMCUSTOMDRAW *) m.LParam.ToPointer();
            if ( hdr->hdr.code == 0xfffffff4 /* NM_CUSTOMDRAW */ )
            {
               this.HeaderHWnd = hdr->hdr.hwndFrom;
               if (hdr->dwDrawStage == User32.CDDS_PREPAINT )
               {
                  m.Result = (IntPtr)User32.CDRF_NOTIFYITEMDRAW;
               }

               if (hdr->dwDrawStage == User32.CDDS_ITEMPOSTPAINT )
               {
                  System.Drawing.Graphics dc = System.Drawing.Graphics.FromHdc((IntPtr)hdr->hdc);
                  if (hdr->dwItemSpec == this.comparer.SortColumn && this.allowsort)
                  {
                     for (int i=0; i<4; i++)
                     {
                        if (this.comparer.Order == SortOrder.Ascending)
                        {
                           if (i==0)
                              dc.DrawLine(System.Drawing.Pens.Black,hdr->rc.right-10-i,hdr->rc.bottom-5-i,hdr->rc.right-10+i,hdr->rc.bottom-5-i-1);
                           else
                              dc.DrawLine(System.Drawing.Pens.Black,hdr->rc.right-10-i,hdr->rc.bottom-5-i,hdr->rc.right-10+i,hdr->rc.bottom-5-i);
                        }
                        else
                        {
                           if (i==0)
                              dc.DrawLine(System.Drawing.Pens.Black,hdr->rc.right-10-i,hdr->rc.bottom-8+i,hdr->rc.right-10+i,hdr->rc.bottom-8+i+1);
                           else
                              dc.DrawLine(System.Drawing.Pens.Black,hdr->rc.right-10-i,hdr->rc.bottom-8+i,hdr->rc.right-10+i,hdr->rc.bottom-8+i);
                        }
                     }
                  }
               }
   
               if (hdr->dwDrawStage == User32.CDDS_ITEMPREPAINT)
               {
                  if (this.comparer.SortColumn == hdr->dwItemSpec)
                     m.Result = (IntPtr)User32.CDRF_NOTIFYPOSTPAINT;   // redraw only item with arrow
               }
            }
         }
      }
   }
   public class User32
   {
      [DllImport("user32.dll", CharSet=CharSet.Auto)]
      public static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);

      [DllImport("user32.dll", CharSet=CharSet.Auto)]
      public static extern int InvalidateRect(
         IntPtr hWnd,           // handle to window
         IntPtr lpRect,  // rectangle coordinates
         int bErase          // erase state
         );
      
      public const  int CDRF_NOTIFYITEMDRAW = 0x20;
      public const  int CDRF_NOTIFYPOSTPAINT =  0x00000010;
      public const  int CDDS_ITEM =  0x00010000;
      public const  int CDDS_PREPAINT  = 0x00000001;
      public const  int CDDS_POSTPAINT  =  0x00000002;
      public const  int CDDS_ITEMPREPAINT   =    (CDDS_ITEM | CDDS_PREPAINT);
      public const  int CDDS_ITEMPOSTPAINT  =   (CDDS_ITEM | CDDS_POSTPAINT);

      public struct  NMHDR
      {
         public int hwndFrom; 
         public uint idFrom; 
         public uint code; 
      };

      public struct RECT
      {
         public int  left;
         public int  top;
         public int  right;
         public int  bottom;
      }

      public struct  NMCUSTOMDRAW
      {
         public NMHDR hdr;
         public uint   dwDrawStage;
         public int  hdc;
         public RECT rc;

         public uint   dwItemSpec;
         public uint   uItemState;
         public uint   lItemlParam;
      }
   }

   public class ListViewItemComparer : IComparer
   {
      // Specifies the column to be sorted
      private int ColumnToSort;

      // Specifies the order in which to sort (i.e. 'Ascending').
      private SortOrder order;

      // Case insensitive comparer object
      private CaseInsensitiveComparer ObjectCompare;

      // Class constructor, initializes various elements
      public ListViewItemComparer()
      {
         // Initialize the column to '0'
         ColumnToSort = 0;

         // Initialize the sort order to 'none'
         order = SortOrder.Ascending;

         // Initialize the CaseInsensitiveComparer object
         ObjectCompare = new CaseInsensitiveComparer();
      }

      // This method is inherited from the IComparer interface.
      // It compares the two objects passed using a case
      // insensitive comparison.
      //
      // x: First object to be compared
      // y: Second object to be compared
      //
      // The result of the comparison. "0" if equal,
      // negative if 'x' is less than 'y' and
      // positive if 'x' is greater than 'y'
      public int Compare(object x, object y)
      {
         int compareResult=0;

         // Cast the objects to be compared to ListViewItem objects
         DataRowView rowX = (DataRowView)(((ListViewItem)x).Tag);
         DataRowView rowY = (DataRowView)(((ListViewItem)y).Tag);
         if (rowX == null || rowY == null)
            compareResult = ObjectCompare.Compare(
               ((ListViewItem)x).Text, ((ListViewItem)y).Text );
         else
         {

            DataList list = (DataList)(((ListViewItem)x).ListView);

            // Case insensitive Compare

            object objx = rowX[list.Columns[ColumnToSort].FieldName];
            object objy = rowY[list.Columns[ColumnToSort].FieldName];

            if (objx.GetType() == typeof(System.DBNull))
            {
               if (objy.GetType() == typeof(System.DBNull))
                  compareResult = 0;
               else
                  compareResult = -1;
            }
            else
            {
               if (objy.GetType() == typeof(System.DBNull))
                  compareResult = 1;
               else
                  compareResult = ObjectCompare.Compare(
                     objx,
                     objy
                     );
            }
         }
         // Calculate correct return value based on object comparison
         if (order == SortOrder.Ascending)
         {
            // Ascending sort is selected, return normal result of compare operation
            return compareResult;
         }
         else if (order == SortOrder.Descending)
         {
            // Descending sort is selected, return negative result of compare operation
            return (-compareResult);
         }
         else
         {
            // Return '0' to indicate they are equal
            return 0;
         }
      }

      // Gets or sets the number of the column to which to
      // apply the sorting operation (Defaults to '0').
      public int SortColumn
      {
         set
         {
            ColumnToSort = value;
         }
         get
         {
            return ColumnToSort;
         }
      }

      // Gets or sets the order of sorting to apply
      // (for example, 'Ascending' or 'Descending').
      public SortOrder Order
      {
         set
         {
            order = value;
         }
         get
         {
            return order;
         }
      }
   }



   [Serializable()]
   [TypeConverter(typeof(DataColumnHeaderConverter))]
   [DefaultProperty("FieldName")]
   public class DataColumnHeader : System.Windows.Forms.ColumnHeader
   {
      private string fieldName;

      [Category("Data Source")]  
      public string FieldName { get { return fieldName; } set { fieldName = value; } }

      public delegate void FieldParseHandler(string FieldName, DataRowView row, ref string text);
      public event FieldParseHandler OnParse;

      public string Parse(DataRowView row)
      {
         if (!row.DataView.Table.Columns.Contains(this.FieldName))
            return "<unknown field name: "+this.FieldName+">";
         string text = "";
         object val = row[this.FieldName];
         if (val != null )
         {
            Type type = val.GetType();
            if (type == typeof(System.DateTime))
               text =((DateTime)val).ToString(DCEAccessLib.Settings.DateFormat);
            else
            if (type == typeof(System.Boolean))
            {
               if ((bool)val)
                  text = "Да";
               else
                  text = "Нет";
            }
            else
               text = val.ToString();
         }
         if (OnParse!=null)
         {
            OnParse(this.fieldName,row, ref text);
         }
         return text;
      }

      public DataColumnHeader()
      {
      }
      public DataColumnHeader(SerializationInfo info, StreamingContext context) 
      {
         this.fieldName = (string)info.GetValue("FieldName", typeof(string));
      }
      public void GetObjectData(SerializationInfo info, StreamingContext context)
      {
         info.AddValue("FieldName", this.fieldName);
      }

      new public object Clone() 
      {
         return new DataColumnHeader();
         //         return MemberwiseClone();
         //         return hdr;
      }

   }

   public class DataColumnHeaderConverter : ExpandableObjectConverter
   {
      public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
      {
         if (destinationType == typeof(System.ComponentModel.Design.Serialization.InstanceDescriptor))
         {
            return true;
         }
         // Always call the base to see if it can perform the conversion.
         return base.CanConvertTo(context, destinationType);
      }

      public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
      {
         if(destinationType == typeof(System.ComponentModel.Design.Serialization.InstanceDescriptor)) 
         {
            System.Reflection.ConstructorInfo ci = typeof(DataColumnHeader).GetConstructor(new Type[]{});
            DataColumnHeader item = (DataColumnHeader)value;
            return new System.ComponentModel.Design.Serialization.InstanceDescriptor(ci, new object[]{}, true);
         }
         return base.ConvertTo(context, culture, value, destinationType);
      }
   }

}
