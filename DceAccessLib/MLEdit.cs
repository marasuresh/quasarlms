using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace DCEAccessLib
{
   /// <summary>
   /// Контрол для редактирования многоязыкового контента
   /// </summary>
   public class MLEdit : System.Windows.Forms.TextBox
   {
      protected LangSwitcher switcher;
      protected LangSwitcher.SwitchLangHandler handler;
      protected dataFieldType dataType = dataFieldType.nvarchar255;
      
      /// <summary>
      /// Поле в таблице Content для храниния данных
      /// </summary>
      [BrowsableAttribute(false)]
      [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Visible)] 
      [DefaultValue(dataFieldType.nvarchar255)]
      public dataFieldType DataType
      {
         get
         {
            if (this.dataType == dataFieldType.nvarchar255 && this.MaxLength > 255)
               this.MaxLength = 255;
            
            return this.dataType;
         }
         set
         {
            this.dataType = value;
            
            if (this.dataType == dataFieldType.nvarchar255 && this.MaxLength > 255)
               this.MaxLength = 255;
         }
      }

      protected bool useCOrder =false;
      [BrowsableAttribute(true)]
      [DefaultValue(false)]
      public bool UseCOrder 
      {
         get 
         {
            return useCOrder;
         }
         set
         {
            useCOrder = value;
         }
      }
      protected int cOrder=0;
      [BrowsableAttribute(true)]
      [DefaultValue(0)]
      public int COrder
      {  
         get 
         {
            return cOrder;
         }
         set
         {
            cOrder = value;
         }

      }

      protected string dataField
      {
         get
         {
            switch (this.dataType)
            {
               case dataFieldType.ntext:
                  return "TData";
               case dataFieldType.nvarchar255:
               default:
                  return "DataStr";
            }
         }
      }

      /// <summary>
      /// Ссылка на переключатель языка формы
      /// </summary>
      public LangSwitcher LanguageSwitcher
      {
         get 
         {
            return switcher;
         }
         set
         {
            if (this.switcher!=null)
            {
               this.switcher.LanguageChanged -= handler;
            }
            this.switcher = value;
            if (this.switcher != null)
               this.switcher.LanguageChanged +=handler;

         }
      }

      IEditNode Node;
      public void SetParentNode(IEditNode node)
      {
         this.Node = node;
         node.OnUpdateDataSet += new RecordEditNode.EnumDataSetsHandler(this.OnUpdateDataSet);
         node.OnReloadContent += new RecordEditNode.ReloadContentHandler(this.UpdateContent);
      }

      private string qualif;
      public void SetParentNode(IEditNode node, string qualif)
      {
         this.qualif = qualif;
         node.OnUpdateDataSet += new RecordEditNode.EnumDataSetsHandler(this.OnUpdateDataSetQualif);
         node.OnReloadContent += new RecordEditNode.ReloadContentHandler(this.UpdateContent);
      }

      public void AcceptInput()
      {
         if (this.currentRow!=null)
         {
            if (this.currentRow.IsEdit)
            {
               
               if (this.Text.Length > 0 )
                  this.currentRow[dataField] = this.Text;
               else
                  this.currentRow[dataField] = System.DBNull.Value;
               this.currentRow["Type"] = (int)this.entType;
               this.currentRow.EndEdit();
            }
         }
      }
      public bool OnUpdateDataSet(string TransactionId, ref DCEAccessLib.DataSetBatchUpdate dataSet)
      {
         //this.validateText(true);
         AcceptInput();
         if (eid != null && eid !="")
         {
            if (this.useCOrder)
               dataSet.sql = "Select * from dbo.Content where eid = '" + this.eid + "' and COrder=" + this.cOrder.ToString();          
            else
               dataSet.sql = "Select * from dbo.Content where eid = '" + this.eid + "'";          
            dataSet.tableName = "dbo.Content";
            dataSet.dataSet = this.ds;
            return true;
         }
         return false;
      }

      public bool OnUpdateDataSetQualif(string TransactionId, ref DCEAccessLib.DataSetBatchUpdate dataSet)
      {
         if (qualif == TransactionId)
         {
            return OnUpdateDataSet(TransactionId, ref dataSet);
         }
         else
         {
            return false;
         }
      }

      /// <summary>
      /// Entity id field
      /// </summary>
      protected string eid;

      [Browsable(false)]
      public string eId
      {
         get
         {
            return eid;
         }
         set
         {
            if (this.eid != value)
            {
               this.eid = value;
               if (eid !="" && eid != null)
                  UpdateContent();
            }
         }
      }

      protected DataSet ds = null;
      protected DataView dv = null;

      public DataSet dataSet
      {
         get
         {
            return ds;
         }
      }
      protected DataRowView currentRow = null;

      /// <summary>
      /// replace current dataset with a prepared one
      /// </summary>
      /// <param name="newDataSet"></param>
      public void ReplaceData(DataSet newDataSet, string tablename)
      {
         if (this.currentRow !=null)
            this.currentRow.CancelEdit();
         // removing all old content
         this.ds.Tables["dbo.Content"].Rows.Clear();
         this.ds.AcceptChanges();
         dv = new DataView(ds.Tables["dbo.Content"]);

         foreach (DataRow row in newDataSet.Tables[tablename].Rows)
         {
            DataRowView newrow = this.dv.AddNew();
            newrow ["id"] = System.Guid.NewGuid().ToString();
            newrow ["eid"] = this.eid;
            newrow ["Type"] = entType;
            newrow ["Lang"] = row["Lang"];
            newrow [dataField] = row[dataField];
            newrow.EndEdit();
         }
         
         LoadText();
      }

      public bool GetLangContentString(int lang, out string ret)
      {
         AcceptInput();
         foreach(DataRowView row in dv)
         {
            if (row["Lang"].ToString() == lang.ToString())
            {
               ret = row["DataStr"].ToString();
               return true;
            }
         }
         ret = "";
         return false;
      }

      protected override void Dispose(bool dispose)
      {
         this.Node = null;
         if (dispose)
         {
            currentRow = null;
            if (dv != null)
            {
               dv.Dispose();
               dv = null;
            }
            if (ds!=null)
            {
               ds.Dispose();
               ds = null;
            }

            if (handler != null)
            {
               if (this.switcher != null)
               {
                  this.LanguageSwitcher = null;
               }
               handler = null;
            }
         }
         base.Dispose(dispose);
      }

      public void UpdateContent()
      {
         if(this.eid != null && this.eid != "")
         {
            string sql;
            if (this.useCOrder)
               sql = "select * from dbo.Content where eid='" 
                  + this.eid + "' and COrder="+this.COrder.ToString();
            else
               sql = "select * from dbo.Content where eid='" + this.eid + "'";

            ds = DCEAccessLib.DCEWebAccess.WebAccess.GetDataSet(sql, "dbo.Content");
            dv = new DataView(ds.Tables["dbo.Content"]);
            LoadText();
         }
      }
   
      private bool initEntType = false;
      private DCEAccessLib.ContentType entType = DCEAccessLib.ContentType._string;

      [BrowsableAttribute(false)]
      [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Visible)] 
      [DefaultValue(ContentType._string)]
      public DCEAccessLib.ContentType EntType
      {
         get
         {
            // NOTE : eId must be initialized before this line
            
            if (!initEntType)
               UpdateContent();

            return entType;
         }
         set
         {
            switch (value)
            {
               case ContentType._html:
               case ContentType._xml:
               case ContentType._longString:
                  this.DataType = dataFieldType.ntext;
                  break;

               case ContentType._string:
               case ContentType._url:
               case ContentType._object:
                  this.DataType = dataFieldType.nvarchar255;
                  break;
               default:
                  return;
            }

            entType = value;
            if (this.currentRow != null)
               this.currentRow["Type"] = (int)entType;
         }
      }
      
      /// <summary>
      /// Тип содержимого контрола
      /// </summary>
      [BrowsableAttribute(true)]
      [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Visible)] 
      [DefaultValue(ContentType._string)]
      public DCEAccessLib.ContentType ContentType
      {
         get
         {
            return this.EntType;
         }
         set
         {
            this.EntType = value;
         }
      }
      
      private EventHandler onEntTypeChanged;
      
      [BrowsableAttribute(true)]
      public event EventHandler EntTypeChanged
      {
         add 
         {
            onEntTypeChanged += value;
         }
         remove 
         {
            onEntTypeChanged -= value;
         }
      }
      
      protected System.Windows.Forms.Label captionLabel = null;
      [BrowsableAttribute(true)]
      [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Visible)] 
      public System.Windows.Forms.Label CaptionLabel
      {
         get
         {
            return this.captionLabel;
         }
         set
         {
            this.captionLabel = value;
         }
      }
      
      protected void LoadText()
      {
         if (dv != null && this.LanguageSwitcher != null)
         {
            bool newrow = true;
            foreach (DataRowView row in dv)
            {
               if ((int)row["Lang"] == this.LanguageSwitcher.Language)
               {
                  if (this.entType != (ContentType)(int)row["Type"])
                  {
                     this.EntType = (ContentType)(int)row["Type"];
                     initEntType = true;
                  
                     if (onEntTypeChanged != null)
                        onEntTypeChanged(this, new MLEditEntityEventArgs(entType));
                  }

                  this.silentChange = true;
                  this.Text = row[dataField].ToString();
                  this.silentChange = false;
                  
                  this.currentRow = row;
                  this.currentRow.BeginEdit();
                  newrow = false;
               }
            }
            if (newrow && this.LanguageSwitcher.Language>0)
            {
               this.currentRow = dv.AddNew();
               currentRow ["id"] = System.Guid.NewGuid().ToString();
               currentRow ["eid"] = this.eid;
               currentRow ["Type"] = entType;
               if (this.useCOrder)
               {
                  currentRow ["COrder"] = this.cOrder;
               }
               
               if (onEntTypeChanged != null)
                  onEntTypeChanged(this, new MLEditEntityEventArgs(entType));

               currentRow ["Lang"] = this.LanguageSwitcher.Language;
               currentRow [dataField] = "";
               this.silentChange = true;
               this.Text = "";
               this.silentChange = false;
            }
            if (this.dataType == dataFieldType.nvarchar255 && this.MaxLength > 255)
               this.MaxLength = 255;
         }
      }

      private void validateText(object sender, System.EventArgs e)
      {
         validateText(false);
      }
      private void validateText(bool doException)
      {
         string text = "";
         string ctext = this.captionLabel != null ? 
            this.captionLabel.Text : "";
         switch (this.entType)
         {
            case ContentType._xml:
               text = "В поле "+ctext+" введите правильный xml";
               break;
            case ContentType._html:
               text = "В поле "+ctext+" введите правильный HTML";
               break;
            default:
               return;
         }
         System.Xml.XmlDocument ndoc = new System.Xml.XmlDocument();
         try
         {
            string xml = "<xml>" + this.Text + "</xml>"; 
            ndoc.LoadXml(xml);
         }
         catch
         {
            if (doException)
            {
               throw new DCEException(text, DCEException.ExceptionLevel.InvalidAction);
            }
            MessageBox.Show(this, text, "Неверная операция");
            return;
         }
         return;
      }

      private bool silentChange = true;
      private void TextChanges(object sender, System.EventArgs e)
      {
         if (!silentChange)
         {
            if (this.Node !=null)
               Node.ChangeNotify();
         }
      }

      public MLEdit()
      {
         this.LostFocus += new System.EventHandler(validateText);
         this.TextChanged += new System.EventHandler(TextChanges);
         handler = new LangSwitcher.SwitchLangHandler(OnSwitchLang);
      }

      protected void OnSwitchLang(int langid)
      {
         AcceptInput();
         LoadText();
      }
   }

   public enum dataFieldType
   {
      nvarchar255 = 0,
      ntext = 1
   }
   
   public class MLEditEntityEventArgs : EventArgs
   {
      public MLEditEntityEventArgs(DCEAccessLib.ContentType type)
      {
         this.type = type;
      }
      private DCEAccessLib.ContentType type;

      public DCEAccessLib.ContentType GetEntType()
      {
         return type;
      }
   }
}
