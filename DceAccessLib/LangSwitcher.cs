using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace DCEAccessLib
{
   /// <summary>
   /// ѕереключение €зыка редактировани€ формы
   /// </summary>
   [Serializable()]
   public class LangSwitcher : System.Windows.Forms.UserControl
   {
      private System.Windows.Forms.ComboBox comboBox1;
      private int CurrLangId = -1;
      /// <summary> 
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.Container components = null;
      private System.Windows.Forms.Label textLabel;

      public class LangItem : System.Object
      {
         public string Name;
         public int id;
         public override string ToString()
         {
            return Name;
         }
      }
      
      public delegate void SwitchLangHandler(int langid);
      public event SwitchLangHandler LanguageChanged;

      private string text = "язык";
      
      [BrowsableAttribute(true)]
      [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Visible)] 
      public string TextLabel
      {
         get { return text; }
         
         set 
         {
            text = value;
            textLabel.Text = text;
         }
      }

      /// <summary>
      /// ѕолучение и установка выбранного €зыка
      /// Do not change attributes of this property ever!!!!
      /// </summary>
      [BrowsableAttribute(false)]
      [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)] 
      public int Language
      {
         get 
         {
            return CurrLangId;//((LangItem)this.comboBox1.Items[this.comboBox1.SelectedIndex]).id;
         }
         set 
         {
            if (value != this.CurrLangId)
            {
               bool found = false;
               foreach (LangItem item in this.comboBox1.Items)
               {
                  if (item.id == value)
                  {
                     found = true;
                     this.comboBox1.SelectedIndex = this.comboBox1.Items.IndexOf(item);
                  }
               }
               if (!found) 
               {
                  //throw new System.InvalidOperationException("Ќеизвестный id €зыка: " + value.ToString());
               }
               //               this.CurrLangId = value;
            }
         }
      }

      public LangSwitcher()
      {
         // This call is required by the Windows.Forms Form Designer.
         InitializeComponent();
      }

      protected override void InitLayout()
      {
         base.InitLayout();
         if (this.DesignMode)
         {
            LangItem item = new LangItem();
            item.Name = "<язык>";
            item.id = 1;
            this.comboBox1.Items.Add(item);
            this.comboBox1.SelectedIndex = 0;
         }
         else
         {
            DataSet ds = DCEAccessLib.DCEWebAccess.WebAccess.GetDataSet("Select * from dbo.Languages", "Languages");
            //"fileds : id, NameEng, NameNative";
            foreach (DataRow r in ds.Tables["Languages"].Rows)
            {
               LangItem item = new LangItem();
               item.id = (int)r["id"];
               item.Name = r["NameNative"].ToString();
               this.comboBox1.Items.Add(item);
            }
         
            if (this.comboBox1.Items.Count > 0)
            {
               this.comboBox1.SelectedIndex = 0;
            }
         }
      }


      /// <summary> 
      /// Clean up any resources being used.
      /// </summary>
      protected override void Dispose( bool disposing )
      {
         if( disposing )
         {
            if(components != null)
            {
               components.Dispose();
            }
         }
         base.Dispose( disposing );
      }

		#region Component Designer generated code
      /// <summary> 
      /// Required method for Designer support - do not modify 
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
		  this.textLabel = new System.Windows.Forms.Label();
		  this.comboBox1 = new System.Windows.Forms.ComboBox();
		  this.SuspendLayout();
		  // 
		  // textLabel
		  // 
		  this.textLabel.AutoSize = true;
		  this.textLabel.Location = new System.Drawing.Point(0, 4);
		  this.textLabel.Name = "textLabel";
		  this.textLabel.Size = new System.Drawing.Size(35, 13);
		  this.textLabel.TabIndex = 0;
		  this.textLabel.Text = "язык";
		  this.textLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		  // 
		  // comboBox1
		  // 
		  this.comboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
		  this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		  this.comboBox1.Enabled = global::DCEAccessLib.Properties.Settings.Default.LangSwitchEnabled;
		  this.comboBox1.Location = new System.Drawing.Point(60, 0);
		  this.comboBox1.Name = "comboBox1";
		  this.comboBox1.Size = new System.Drawing.Size(121, 21);
		  this.comboBox1.TabIndex = 1;
		  this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
		  // 
		  // LangSwitcher
		  // 
		  this.Controls.Add(this.comboBox1);
		  this.Controls.Add(this.textLabel);
		  this.Name = "LangSwitcher";
		  this.Size = new System.Drawing.Size(184, 24);
		  this.ResumeLayout(false);
		  this.PerformLayout();

      }
		#endregion


      private void comboBox1_SelectedIndexChanged(object sender, System.EventArgs e)
      {
         this.CurrLangId = ((LangItem)this.comboBox1.Items[this.comboBox1.SelectedIndex]).id;
         if (this.LanguageChanged != null)
         { 
            this.LanguageChanged(CurrLangId); 
         }
      }
   }


   public class LangMenuItem : MenuItem
   {
      public int LangId;
      public LangMenuItem (string Name , int langId , System.EventHandler h)
         : base(Name,h,Shortcut.None)
      {
         LangId = langId;
      }
   }

   public class LangToolBarButton : System.Windows.Forms.ToolBarButton 
   {
      private int CurrLangId = -1;
      private System.Collections.ArrayList EventTable = new System.Collections.ArrayList();

      public delegate bool BeforeLangChangeHandler(int oldlangid, int newlangid);
      public event BeforeLangChangeHandler OnBeforeLanguageChanged
      {
         add 
         {
            EventTable.Add( value ); 
         }
         remove
         {
            EventTable.Remove( value );
         }
      }
      public delegate void LangChangedHandler(int newlangid);
      public event LangChangedHandler OnLanguageChanged;

      /// <summary>
      /// Do not change attributes of this property ever!!!!
      /// </summary>
      [BrowsableAttribute(false)]
      [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)] 
      public int Language
      {
         get
         {
            return CurrLangId;
         }
         set
         {
            if (value != this.CurrLangId)
            {
               LangMenuItem found = null;
               foreach (LangMenuItem item in this.DropDownMenu.MenuItems)
               {
                  if (item.LangId == value)
                  {
                     found = item;
                     break;
                  }
               }
               if (found == null) 
               {
                  //throw new System.InvalidOperationException("Ќеизвестный id €зыка: " + value.ToString());
               }
               else
               {
                  foreach (BeforeLangChangeHandler handler in EventTable)
                  {
                     if (!handler(this.CurrLangId, value))
                        return;
                  }
                  this.Text = "язык: " + found.Text;
                  this.CurrLangId = value;
                  if (OnLanguageChanged !=null)
                     OnLanguageChanged(value);
               }
            }
         }
      }

      protected void onLangClick (object sender,
         EventArgs e)
      {
         this.Language = ((LangMenuItem) sender).LangId;
      }

      void onBtnClick(object sender, ToolBarButtonClickEventArgs arg)
      {
         if (arg.Button == this)
         {
            foreach (LangMenuItem item in this.DropDownMenu.MenuItems)
            {
               if (item.LangId == this.Language)
               {
                  int num = 1+this.DropDownMenu.MenuItems.IndexOf(item);
                  if (num>=this.DropDownMenu.MenuItems.Count)
                     num=0;
                  this.Language = ((LangMenuItem )this.DropDownMenu.MenuItems[num]).LangId;
                  break;
               }
            }
         }
      }

      public void Init()
      {
      //   this.Parent.ButtonClick += new ToolBarButtonClickEventHandler(onBtnClick);
         this.Style = ToolBarButtonStyle.DropDownButton;
         this.DropDownMenu = null;

         Menu m = new System.Windows.Forms.ContextMenu();
         
         DataSet ds = DCEAccessLib.DCEWebAccess.WebAccess.GetDataSet("Select * from dbo.Languages", "Languages");
         //"fileds : id, NameEng, NameNative";
         System.EventHandler h = new System.EventHandler(onLangClick);
         foreach (DataRow r in ds.Tables["Languages"].Rows)
         {
            LangMenuItem itm = new LangMenuItem(r["NameNative"].ToString(),
               (int)r["id"], h);

            m.MenuItems.Add(itm);
         }

         if (m.MenuItems.Count>0)
         {
            this.DropDownMenu = m;
            this.Language = ((LangMenuItem)m.MenuItems[0]).LangId;
         }
      }

      protected override void Dispose( bool disposing )
      {
         if( disposing )
         {
            if (this.EventTable !=null)
            {
               this.EventTable.Clear();
               this.EventTable = null;
            }
            this.OnLanguageChanged = null;
         }
         base.Dispose( disposing );
      }
   }
}
