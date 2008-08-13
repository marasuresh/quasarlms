using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Xml;
using System.Text;
using DCEAccessLib;

namespace DCECourseEditor
{
   /// <summary>
   /// Ответ на вопрос, с языковозависимым контентом и флагом правильности
   /// </summary>
   public class AnswerData 
   {
      public AnswerData() {}

      public AnswerData(bool right) 
      {
         Right = right;
      }

      public AnswerData(string text, string right, int lang) 
      {
         Right = (right == "true");
         this[lang] = text;
      }

      private bool isRight;
      
      internal static int CurrentLanguageId = 1;
      
      internal static AnswerData freshAnswer;
      
      internal static bool allowMultiple;

      internal static AnswerDataList datalist;

      public string Answer 
      {
         get { return this[CurrentLanguageId]; }
         set { this[CurrentLanguageId] = value; }
      }

      public bool Right 
      {
         set 
         { 
            isRight = value; 
            
            freshAnswer = this;

            if (!allowMultiple)
            {
               datalist.ValidateList(this, AnswerType.atSingle);
            }
         }
         get { return isRight; }
      }

      protected Hashtable xmlmap = new Hashtable();

      internal string this [int langid] 
      {
         get 
         { 
            if (!xmlmap.Contains(langid))
               return "";

            return (string)xmlmap[langid];
         }
         set 
         { 
            xmlmap[langid] = value; 
         }
      }
   }

   /// <summary>
   /// Ответ с флагом учитывания регистра
   /// </summary>
   public class AnswerTextboxData : AnswerData 
   {
      public AnswerTextboxData()
      : base (true)
      {
      }
      
      public void Clear()
      {
         this.xmlmap.Clear();
         casesensitive = false;
      }

      private bool casesensitive = false;
      public bool CaseSensitive
      {
         get { return casesensitive; }
         set { casesensitive = value; }
      }
      
      public new bool Right
      {
         get { return true; }
      }
   }

   /// <summary>
   /// Тип ответа : одиночный, множественный, поле ввода
   /// </summary>
   public enum AnswerType 
   { 
      atSingle, atMultiple, atTextbox 
   }

   /// <summary>
   /// Список ответов, с возможностью валидации
   /// </summary>
   public class AnswerDataList : ArrayList, IBindingList 
   {
      private ListChangedEventHandler onListChanged;
      
      public AnswerDataList() 
      {
         AnswerData.datalist = this;
      }

      public bool ValidateList(AnswerData current, AnswerType type)
      {
         if (type == AnswerType.atSingle)
         {
            bool hasfind = current == null ? false : current.Right;
            foreach (AnswerData d in this)
            {
               if (d.Right && hasfind && d != current)
                  d.Right = false;
               
               if (!hasfind && d.Right)
                  hasfind = true;
            }
            
            RefreshList();
         }

         return true;
      }
      
      public void AddAnswerData()
      {
         int index = Add( new AnswerData() );
         OnListChanged(new ListChangedEventArgs(ListChangedType.ItemAdded, index));
      }
      
      public void AddAnswerData(bool right)
      {
         int index = Add( new AnswerData(right) );
         OnListChanged(new ListChangedEventArgs(ListChangedType.ItemAdded, index));
      }
      
      public void AddAnswerData(string text, string right, int lang)
      {
         int index = Add( new AnswerData(text, right, lang) );
         OnListChanged(new ListChangedEventArgs(ListChangedType.ItemAdded, index));
      }

      public void RemoveAnswerData(int index)
      {
         this.RemoveAt(index);
         OnListChanged(new ListChangedEventArgs(ListChangedType.ItemDeleted, index));
      }
      
      public void ClearAnswerData()
      {
         for ( int i=this.Count-1; i>=0 ; i--)
            this.RemoveAnswerData(i);
      }
      
      public void CustomerChanged(AnswerData data) 
      {
         int index = base.IndexOf(data);
            
         OnListChanged(new ListChangedEventArgs(ListChangedType.ItemChanged, index));
      }

      public void RefreshList()
      {
         if (onListChanged != null) 
         {
            for (int index = 0; index < this.Count; index++)
            {
               onListChanged(this, new ListChangedEventArgs(ListChangedType.ItemChanged, index));
            }
         }
      }

      public AnswerData FreshAnswer
      {
         get { return AnswerData.freshAnswer; }
      }
      
      protected void OnListChanged(ListChangedEventArgs ev) 
      {
         if (onListChanged != null) 
         {
            onListChanged(this, ev);
         }
      }
      
      #region Implements IBindingList.
      bool IBindingList.AllowEdit 
      { 
         get { return true ; }
      }

      bool IBindingList.AllowNew 
      { 
         get { return false ; }
      }

      bool IBindingList.AllowRemove 
      { 
         get { return false ; }
      }

      bool IBindingList.SupportsChangeNotification 
      { 
         get { return true ; }
      }
        
      bool IBindingList.SupportsSearching 
      { 
         get { return false ; }
      }

      bool IBindingList.SupportsSorting 
      { 
         get { return false ; }
      }
      #endregion

      #region Events.
      public event ListChangedEventHandler ListChanged 
      {
         add 
         {
            onListChanged += value;
         }
         remove 
         {
            onListChanged -= value;
         }
      }
      #endregion

      #region Methods.
      object IBindingList.AddNew() 
      {
         AnswerData d = new AnswerData(false);
         base.Add(d);
         this.RefreshList();
         return d;
      }
      #endregion

      #region Unsupported properties.
      bool IBindingList.IsSorted 
      { 
         get { throw new NotSupportedException(); }
      }

      ListSortDirection IBindingList.SortDirection 
      { 
         get { throw new NotSupportedException(); }
      }


      PropertyDescriptor IBindingList.SortProperty 
      { 
         get { throw new NotSupportedException(); }
      }
      #endregion

      #region Unsupported Methods.
      void IBindingList.AddIndex(PropertyDescriptor property) 
      {
         throw new NotSupportedException(); 
      }

      void IBindingList.ApplySort(PropertyDescriptor property, ListSortDirection direction) 
      {
         throw new NotSupportedException(); 
      }

      int IBindingList.Find(PropertyDescriptor property, object key) 
      {
         throw new NotSupportedException(); 
      }

      void IBindingList.RemoveIndex(PropertyDescriptor property) 
      {
         throw new NotSupportedException(); 
      }

      void IBindingList.RemoveSort() 
      {
         throw new NotSupportedException(); 
      }
      #endregion
   }
   
   /// <summary>
	/// Контрол для отображения списка ответов на вопрос
	/// </summary>
	public class AnswerList : System.Windows.Forms.UserControl 
	{
      private QuestionListType answerListType;
      /// <summary>
      /// Тест/Практика/Анкета
      /// </summary>
      public QuestionListType AnswerListType
      {
         get 
         { 
            return answerListType; 
         }
         set
         {
            answerListType = value;
            UpdateAnswerListLayout();
         }

      }
      
      private bool restricted;
      private void UpdateAnswerListLayout()
      {
         switch (answerListType)
         {
            case QuestionListType.qltTest:
               restricted = false;
               CreateStyles(this.AnswersDataGrid);
               break;

            case QuestionListType.qltWork:
               restricted = false;
               CreateStyles(this.AnswersDataGrid);
               break;

            case QuestionListType.qltQuestionnaire:
               restricted = true;
               CreateStyles(this.AnswersDataGrid);
               break;
         }
      }

      public AnswerList() 
      {
         // This call is required by the Windows.Forms Form Designer.
         InitializeComponent();
         
         handler = new LangSwitcher.SwitchLangHandler(OnParentLanguageChange);
         update = new RecordEditNode.EnumDataSetsHandler(OnUpdateDataSet);
         reload = new RecordEditNode.ReloadContentHandler(UpdateContent);

         AnswersDataGrid.SetDataBinding(ListAnswers, null);
         CreateStyles(AnswersDataGrid);
         
         TextBox.DataBindings.Add("Text", textBoxAnswer, "Answer");
         CaseCheckBox.DataBindings.Add("Checked", textBoxAnswer, "Casesensitive");
      }
      
      #region Dispose code
      protected override void Dispose(bool disposing) 
      {
         if (disposing)
         {
            if(components != null)
            {
               components.Dispose();
            }

            if (this.dataView != null)
            {
               this.dataView.Dispose();
               this.dataView = null;
            }
            if (this.dataSet!=null)
            {
               this.dataSet.Dispose();
               this.dataSet = null;
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
         base.Dispose(disposing);
      }
      #endregion

      private void CreateStyles( DataGrid dg) 
      {
         ListAnswers.AddAnswerData();
         dg.TableStyles.Clear();

         DataGridTableStyle style = new DataGridTableStyle(
            (CurrencyManager)this.BindingContext[this.ListAnswers]);

         style.AlternatingBackColor = System.Drawing.Color.Bisque ;

         DataGridTextBoxColumn AnswerCol = new DataGridTextBoxColumn () ;

         AnswerCol.HeaderText = "Ответ" ;
         AnswerCol.MappingName = "Answer" ;
         AnswerCol.Width = 420 ;

         DataGridBoolColumn RightCol = new DataGridBoolColumn();
		
         RightCol.HeaderText = "Правильность" ;
         RightCol.MappingName = "Right" ;
         RightCol.AllowNull = false;
         RightCol.Width = 80;

         style.GridColumnStyles.Clear();
         
         if (!this.restricted)
         {
            style.GridColumnStyles.AddRange ( 
               new DataGridColumnStyle [] { AnswerCol, RightCol } 
               ) ;
         }
         else
         {
            style.GridColumnStyles.AddRange ( 
               new DataGridColumnStyle [] { AnswerCol } 
               ) ;
         }

         dg.TableStyles.Add ( style ) ;
         ListAnswers.ClearAnswerData();
      }
      
      protected RecordEditNode.EnumDataSetsHandler update;
      protected RecordEditNode.ReloadContentHandler reload;

      public void SetParentNode(RecordEditNode node) 
      {
         node.OnUpdateDataSet += update;
         node.OnReloadContent += reload;
      }
      
      public void ClearParentNode(RecordEditNode node) 
      {
         node.OnUpdateDataSet -= update;
         node.OnReloadContent -= reload;
      }

      #region Boring form variables definitions
      /// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
      
      private System.Windows.Forms.GroupBox QuestionTypeGroupBox;
      private System.Windows.Forms.RadioButton TextRadioButton;
      private System.Windows.Forms.RadioButton MultipleRadioButton;
      private System.Windows.Forms.RadioButton SingleRadioButton;
      private System.Windows.Forms.Panel RadioPanel;
      private System.Windows.Forms.ContextMenu AnswersContextMenu;
      private System.Windows.Forms.MenuItem CreateMenuItem;
      private System.Windows.Forms.MenuItem menuItem2;
      private System.Windows.Forms.MenuItem EditMenuItem;
      private System.Windows.Forms.MenuItem DeleteMenuItem;
      private System.Windows.Forms.Panel TextBoxPanel;
      private System.Windows.Forms.CheckBox CaseCheckBox;
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.TextBox TextBox;
      private System.Windows.Forms.DataGrid AnswersDataGrid;
      private System.Windows.Forms.Button CreateButton;
      private System.Windows.Forms.Button RemoveButton;
      private System.Windows.Forms.Panel AnswersPanel;
      private System.Windows.Forms.Panel DataGridPanel;
      private System.Windows.Forms.Panel ButtonsPanel;

      #endregion
      
      protected DataSet dataSet;
      protected DataView dataView;
      
      private XmlDocument doc = new XmlDocument();
      
      protected LangSwitcher switcher;
      protected LangSwitcher.SwitchLangHandler handler;
      
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
            if (this.switcher != null)
               this.switcher.LanguageChanged -= handler;
            
            this.switcher = value;
            
            if (this.switcher != null)
               this.switcher.LanguageChanged += handler;
         }
      }

      protected string m_eid;

      [Browsable(false)]
      public string eId 
      {
         get 
         {
			 return this.m_eid;
         }
         set 
         {
			 if(this.m_eid != value)
            {
				 this.m_eid = value;
            
               UpdateContent();
            }
         }
      }

      private AnswerType type;

      /// <summary>
      /// Получение строкового типа вопроса
      /// </summary>
      /// <param name="type"></param>
      /// <returns></returns>
      public static string GetTextAnswerType(AnswerType type) 
      {
         switch (type) 
         {
            case AnswerType.atSingle :
               return "single";
            case AnswerType.atMultiple :
               return "multiple";
            case AnswerType.atTextbox :
               return "textbox";
         }
         return "";
      }

      /// <summary>
      /// Тип вопроса
      /// </summary>
      public AnswerType Type 
      {
         get 
         { 
            return type; 
         }
         set 
         {
            type = value;
            
            ListAnswers.ValidateList(AnswerData.freshAnswer, type);

            switch (type)
            {
               case AnswerType.atSingle :
                  TextBoxPanel.Visible = false;
                  AnswersPanel.Visible = true;
                  AnswerData.allowMultiple = false;
                  
                  if (!SingleRadioButton.Checked)
                     SingleRadioButton.Checked = true;
                  
                  break;

               case AnswerType.atMultiple :
                  AnswersPanel.Visible = true;
                  TextBoxPanel.Visible = false;
                  AnswerData.allowMultiple = true;
                  
                  if (!MultipleRadioButton.Checked)
                     MultipleRadioButton.Checked = true;
                  
                  break;

               case AnswerType.atTextbox :
                  AnswersPanel.Visible = false;
                  TextBoxPanel.Visible = !this.restricted;
                  
                  if (!TextRadioButton.Checked)
                     TextRadioButton.Checked = true;

                  break;
            }
         }
      }
      
      private AnswerDataList ListAnswers = new AnswerDataList();
      private AnswerTextboxData textBoxAnswer = new AnswerTextboxData();
      
      /// <summary>
      /// Реакция на переключение языка
      /// </summary>
      /// <param name="LangId"></param>
      public void OnParentLanguageChange(int LangId) 
      {
         // записать данные для предыдущего языка
         ControlsEndEdit();

         AnswerData.CurrentLanguageId = LangId;

         // вычитать данные для выбранного языка
         ControlsRefreshData();
      }

      private void UpdateContent() 
      {
         if (this.eId == null || this.eId == "")
            return;

         dataSet = DCEAccessLib.DCEWebAccess.WebAccess.GetDataSet(
            
            "select * from dbo.Content" + 
            
            " where eid='" + this.m_eid + "'" + 
            
            " order by lang", 
            
            "dbo.Content");
         
         dataView.Table = dataSet.Tables["dbo.Content"];

         DataSet ds = DCEAccessLib.DCEWebAccess.WebAccess.GetDataSet(

            " select id from dbo.Languages order by id",

            "dbo.Languages");

         bool needinit = true;

         foreach(DataRow row in ds.Tables["dbo.Languages"].Rows)
         {
            int langid = (int)row["id"];
            
            bool neednew = true;
            foreach(DataRowView haverow in dataView)
            {
               int havelangid = (int)haverow["Lang"];

               if (langid == havelangid)
               {
                  if (SetLangXml((string)haverow["TData"], langid, needinit))
                  {
                     needinit = false;
                  }
                  neednew = false;
                  break;
               }
            }
            
            if (neednew)
            {
               DataRowView newrow = dataView.AddNew();
               newrow["id"] = System.Guid.NewGuid().ToString();
               newrow["eid"] = this.m_eid;
               newrow["Type"] = DCEAccessLib.ContentType._xml;
               newrow["Lang"] = langid;
               newrow.EndEdit();
            }
         }
         
         if (switcher != null)
            AnswerData.CurrentLanguageId = switcher.Language;
                  
         // обновить данные в контролах
         ControlsRefreshData();
      }
      
      bool SetLangXml(string xml, int langId, bool init) 
      {
         if (init)
         {
            ListAnswers.ClearAnswerData();
            textBoxAnswer.Clear();
         }
         try
         {
            doc.LoadXml(xml);
               
            XmlNode question = doc.SelectSingleNode("Question");
            if (question != null)
            {
               if (init)
               {
                  switch (question.Attributes["type"].Value)
                  {
                     case "single" :
                        Type = AnswerType.atSingle;
                        break;
                     case "multiple" :
                        Type = AnswerType.atMultiple;
                        break;
                     case "textbox" :
                        Type = AnswerType.atTextbox;
                        break;
                  }
               }

               XmlNodeList answers = question.SelectNodes("Answer");
               if (answers != null)
               {
                  if (Type == AnswerType.atTextbox)
                  {
                     XmlNode textboxanswer = answers[0];
                     if (textboxanswer != null)
                     {
                        textBoxAnswer[langId] = textboxanswer.InnerText;
                        textBoxAnswer.CaseSensitive = (textboxanswer.Attributes["casesensitive"].Value == "true");
                     }
                     else
                     {
                        return false;
                     }
                  }
                  else if (Type == AnswerType.atSingle || Type == AnswerType.atMultiple)
                  {
                     if (init) 
                     {
                        foreach(XmlNode answer in answers)
                        {  
                           ListAnswers.AddAnswerData(
                                 answer.InnerText, 
                                 answer.Attributes["right"].Value, 
                                 langId
                              );
                        }
                     }
                     else
                     {
                        int count = (ListAnswers.Count < answers.Count) ? 
                           ListAnswers.Count : answers.Count;

                        for (int i=0; i < count; i++)
                        {
                           AnswerData itemAnswer = ListAnswers[i] as AnswerData;
                           itemAnswer[langId] = answers[i].InnerText;
                        }
                     }
                  }
               }
            }
            else
            {
               return false;
            }

            return true;
         }
         catch
         {
            return false;
         }
      }
      
      string GetLangXml(int langId) 
      {
         StringBuilder xml = new StringBuilder();
         
         xml.Append( "<Question type = \"" + AnswerList.GetTextAnswerType(Type) + "\">" );

         if (Type == AnswerType.atTextbox)
         {
            xml.Append ( "<Answer right = \"" + (textBoxAnswer.Right ? "true" : "false") + "\" " );
            xml.Append ( "casesensitive = \"" + (textBoxAnswer.CaseSensitive ? "true" : "false") + "\">" );
            xml.Append ( textBoxAnswer[langId] );
            xml.Append ( "</Answer>" );
         }
         else
         {
            foreach(AnswerData data in ListAnswers)
            {
               xml.Append ( "<Answer right = \"" + (data.Right ? "true" : "false") + "\">" );
               xml.Append ( data[langId] );
               xml.Append ( "</Answer>" );
            }
         }
         
         xml.Append( "</Question>" );

         return xml.ToString();
      }

      void ControlsRefreshData() 
      {
         if (Type == AnswerType.atTextbox)
         {
            // обновить контролы (полностью вычитать данные)

            TextBox.Text = textBoxAnswer[AnswerData.CurrentLanguageId];
            CaseCheckBox.Checked = textBoxAnswer.CaseSensitive;
         }
         else
         {
            // обновить список ответов (полностью вычитать данные)
            ListAnswers.RefreshList();
         }
      }

      void ControlsEndEdit() 
      {
         if (Type == AnswerType.atTextbox)
         {
            textBoxAnswer[AnswerData.CurrentLanguageId] = TextBox.Text;
            textBoxAnswer.CaseSensitive = CaseCheckBox.Checked;
         }
         else
         {
            foreach(DataGridColumnStyle dgc in AnswersDataGrid.TableStyles[0].GridColumnStyles)
            {
               for(int i=0; i < this.ListAnswers.Count; i++)
                  AnswersDataGrid.EndEdit(dgc, i, false);
            }
         }
      }

      bool OnUpdateDataSet(string TransactionId, ref DCEAccessLib.DataSetBatchUpdate batchDataSet)
      {
         if (this.eId == null || this.eId == "") 
            return false;

         ControlsEndEdit();

         ListAnswers.ValidateList(null, type);

         foreach( DataRowView row in dataView)
         {
            row.BeginEdit();
            row["TData"] = GetLangXml((int)row["Lang"]);
            row.EndEdit();
         }
         
         batchDataSet.sql = "Select * from dbo.Content where eid = '" + this.m_eid + "'";
         batchDataSet.tableName = "dbo.Content";
         batchDataSet.dataSet = this.dataSet;
         
         return true;
      }

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
         this.QuestionTypeGroupBox = new System.Windows.Forms.GroupBox();
         this.TextRadioButton = new System.Windows.Forms.RadioButton();
         this.MultipleRadioButton = new System.Windows.Forms.RadioButton();
         this.SingleRadioButton = new System.Windows.Forms.RadioButton();
         this.RadioPanel = new System.Windows.Forms.Panel();
         this.AnswersContextMenu = new System.Windows.Forms.ContextMenu();
         this.CreateMenuItem = new System.Windows.Forms.MenuItem();
         this.menuItem2 = new System.Windows.Forms.MenuItem();
         this.EditMenuItem = new System.Windows.Forms.MenuItem();
         this.DeleteMenuItem = new System.Windows.Forms.MenuItem();
         this.dataSet = new System.Data.DataSet();
         this.dataView = new System.Data.DataView();
         this.TextBoxPanel = new System.Windows.Forms.Panel();
         this.CaseCheckBox = new System.Windows.Forms.CheckBox();
         this.label1 = new System.Windows.Forms.Label();
         this.TextBox = new System.Windows.Forms.TextBox();
         this.AnswersPanel = new System.Windows.Forms.Panel();
         this.DataGridPanel = new System.Windows.Forms.Panel();
         this.AnswersDataGrid = new System.Windows.Forms.DataGrid();
         this.ButtonsPanel = new System.Windows.Forms.Panel();
         this.CreateButton = new System.Windows.Forms.Button();
         this.RemoveButton = new System.Windows.Forms.Button();
         this.QuestionTypeGroupBox.SuspendLayout();
         this.RadioPanel.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.dataSet)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.dataView)).BeginInit();
         this.TextBoxPanel.SuspendLayout();
         this.AnswersPanel.SuspendLayout();
         this.DataGridPanel.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.AnswersDataGrid)).BeginInit();
         this.ButtonsPanel.SuspendLayout();
         this.SuspendLayout();
         // 
         // QuestionTypeGroupBox
         // 
         this.QuestionTypeGroupBox.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                           this.TextRadioButton,
                                                                                           this.MultipleRadioButton,
                                                                                           this.SingleRadioButton});
         this.QuestionTypeGroupBox.Location = new System.Drawing.Point(4, 4);
         this.QuestionTypeGroupBox.Name = "QuestionTypeGroupBox";
         this.QuestionTypeGroupBox.Size = new System.Drawing.Size(452, 44);
         this.QuestionTypeGroupBox.TabIndex = 204;
         this.QuestionTypeGroupBox.TabStop = false;
         this.QuestionTypeGroupBox.Text = "Тип ответа";
         // 
         // TextRadioButton
         // 
         this.TextRadioButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.TextRadioButton.Location = new System.Drawing.Point(328, 16);
         this.TextRadioButton.Name = "TextRadioButton";
         this.TextRadioButton.Size = new System.Drawing.Size(112, 20);
         this.TextRadioButton.TabIndex = 2;
         this.TextRadioButton.Text = "Поле ввода";
         this.TextRadioButton.CheckedChanged += new System.EventHandler(this.TextRadioButton_CheckedChanged);
         // 
         // MultipleRadioButton
         // 
         this.MultipleRadioButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.MultipleRadioButton.Location = new System.Drawing.Point(152, 16);
         this.MultipleRadioButton.Name = "MultipleRadioButton";
         this.MultipleRadioButton.Size = new System.Drawing.Size(176, 20);
         this.MultipleRadioButton.TabIndex = 1;
         this.MultipleRadioButton.Text = "Множественный выбор";
         this.MultipleRadioButton.CheckedChanged += new System.EventHandler(this.MultipleRadioButton_CheckedChanged);
         // 
         // SingleRadioButton
         // 
         this.SingleRadioButton.Checked = true;
         this.SingleRadioButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.SingleRadioButton.Location = new System.Drawing.Point(8, 16);
         this.SingleRadioButton.Name = "SingleRadioButton";
         this.SingleRadioButton.Size = new System.Drawing.Size(152, 20);
         this.SingleRadioButton.TabIndex = 0;
         this.SingleRadioButton.TabStop = true;
         this.SingleRadioButton.Text = "Одиночный выбор";
         this.SingleRadioButton.CheckedChanged += new System.EventHandler(this.SingleRadioButton_CheckedChanged);
         // 
         // RadioPanel
         // 
         this.RadioPanel.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                 this.QuestionTypeGroupBox});
         this.RadioPanel.Dock = System.Windows.Forms.DockStyle.Top;
         this.RadioPanel.Name = "RadioPanel";
         this.RadioPanel.Size = new System.Drawing.Size(660, 56);
         this.RadioPanel.TabIndex = 206;
         // 
         // AnswersContextMenu
         // 
         this.AnswersContextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                           this.CreateMenuItem,
                                                                                           this.menuItem2,
                                                                                           this.EditMenuItem,
                                                                                           this.DeleteMenuItem});
         // 
         // CreateMenuItem
         // 
         this.CreateMenuItem.Index = 0;
         this.CreateMenuItem.Text = "Создать";
         // 
         // menuItem2
         // 
         this.menuItem2.Index = 1;
         this.menuItem2.Text = "-";
         // 
         // EditMenuItem
         // 
         this.EditMenuItem.Index = 2;
         this.EditMenuItem.Text = "Редактировать";
         // 
         // DeleteMenuItem
         // 
         this.DeleteMenuItem.Index = 3;
         this.DeleteMenuItem.Text = "Удалить";
         // 
         // dataSet
         // 
         this.dataSet.DataSetName = "NewDataSet";
         this.dataSet.Locale = new System.Globalization.CultureInfo("uk-UA");
         // 
         // TextBoxPanel
         // 
         this.TextBoxPanel.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                   this.CaseCheckBox,
                                                                                   this.label1,
                                                                                   this.TextBox});
         this.TextBoxPanel.Dock = System.Windows.Forms.DockStyle.Top;
         this.TextBoxPanel.Location = new System.Drawing.Point(0, 56);
         this.TextBoxPanel.Name = "TextBoxPanel";
         this.TextBoxPanel.Size = new System.Drawing.Size(660, 64);
         this.TextBoxPanel.TabIndex = 210;
         this.TextBoxPanel.Visible = false;
         // 
         // CaseCheckBox
         // 
         this.CaseCheckBox.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
         this.CaseCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.CaseCheckBox.Location = new System.Drawing.Point(420, 8);
         this.CaseCheckBox.Name = "CaseCheckBox";
         this.CaseCheckBox.Size = new System.Drawing.Size(232, 24);
         this.CaseCheckBox.TabIndex = 213;
         this.CaseCheckBox.Text = "Учитывать регистр при проверке";
         // 
         // label1
         // 
         this.label1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
         this.label1.Location = new System.Drawing.Point(8, 21);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(144, 16);
         this.label1.TabIndex = 212;
         this.label1.Text = "Правильный ответ";
         // 
         // TextBox
         // 
         this.TextBox.Anchor = ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right);
         this.TextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.TextBox.Location = new System.Drawing.Point(8, 37);
         this.TextBox.Name = "TextBox";
         this.TextBox.Size = new System.Drawing.Size(644, 20);
         this.TextBox.TabIndex = 211;
         this.TextBox.Text = "";
         // 
         // AnswersPanel
         // 
         this.AnswersPanel.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                   this.DataGridPanel,
                                                                                   this.ButtonsPanel});
         this.AnswersPanel.Dock = System.Windows.Forms.DockStyle.Fill;
         this.AnswersPanel.Location = new System.Drawing.Point(0, 120);
         this.AnswersPanel.Name = "AnswersPanel";
         this.AnswersPanel.Size = new System.Drawing.Size(660, 320);
         this.AnswersPanel.TabIndex = 211;
         // 
         // DataGridPanel
         // 
         this.DataGridPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.DataGridPanel.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                    this.AnswersDataGrid});
         this.DataGridPanel.Dock = System.Windows.Forms.DockStyle.Fill;
         this.DataGridPanel.Name = "DataGridPanel";
         this.DataGridPanel.Size = new System.Drawing.Size(552, 320);
         this.DataGridPanel.TabIndex = 1;
         // 
         // AnswersDataGrid
         // 
         this.AnswersDataGrid.BackgroundColor = System.Drawing.SystemColors.Window;
         this.AnswersDataGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
         this.AnswersDataGrid.CaptionFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
         this.AnswersDataGrid.CaptionVisible = false;
         this.AnswersDataGrid.DataMember = "";
         this.AnswersDataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
         this.AnswersDataGrid.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
         this.AnswersDataGrid.HeaderForeColor = System.Drawing.SystemColors.ControlText;
         this.AnswersDataGrid.Name = "AnswersDataGrid";
         this.AnswersDataGrid.Size = new System.Drawing.Size(550, 318);
         this.AnswersDataGrid.TabIndex = 0;
         this.AnswersDataGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AnswersDataGrid_KeyDown);
         this.AnswersDataGrid.MouseDown += new System.Windows.Forms.MouseEventHandler(this.AnswersDataGrid_MouseAction);
         this.AnswersDataGrid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.AnswersDataGrid_KeyPress);
         this.AnswersDataGrid.MouseUp += new System.Windows.Forms.MouseEventHandler(this.AnswersDataGrid_MouseAction);
         this.AnswersDataGrid.Validating += new System.ComponentModel.CancelEventHandler(this.AnswersDataGrid_Validating);
         // 
         // ButtonsPanel
         // 
         this.ButtonsPanel.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                   this.CreateButton,
                                                                                   this.RemoveButton});
         this.ButtonsPanel.Dock = System.Windows.Forms.DockStyle.Right;
         this.ButtonsPanel.Location = new System.Drawing.Point(552, 0);
         this.ButtonsPanel.Name = "ButtonsPanel";
         this.ButtonsPanel.Size = new System.Drawing.Size(108, 320);
         this.ButtonsPanel.TabIndex = 2;
         // 
         // CreateButton
         // 
         this.CreateButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.CreateButton.Location = new System.Drawing.Point(12, 8);
         this.CreateButton.Name = "CreateButton";
         this.CreateButton.Size = new System.Drawing.Size(84, 24);
         this.CreateButton.TabIndex = 0;
         this.CreateButton.Text = "Создать";
         this.CreateButton.Click += new System.EventHandler(this.CreateButton_Click);
         // 
         // RemoveButton
         // 
         this.RemoveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.RemoveButton.Location = new System.Drawing.Point(12, 44);
         this.RemoveButton.Name = "RemoveButton";
         this.RemoveButton.Size = new System.Drawing.Size(84, 24);
         this.RemoveButton.TabIndex = 1;
         this.RemoveButton.Text = "Удалить";
         this.RemoveButton.Click += new System.EventHandler(this.RemoveButton_Click);
         // 
         // AnswerList
         // 
         this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                      this.AnswersPanel,
                                                                      this.TextBoxPanel,
                                                                      this.RadioPanel});
         this.Name = "AnswerList";
         this.Size = new System.Drawing.Size(660, 440);
         this.QuestionTypeGroupBox.ResumeLayout(false);
         this.RadioPanel.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.dataSet)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.dataView)).EndInit();
         this.TextBoxPanel.ResumeLayout(false);
         this.AnswersPanel.ResumeLayout(false);
         this.DataGridPanel.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.AnswersDataGrid)).EndInit();
         this.ButtonsPanel.ResumeLayout(false);
         this.ResumeLayout(false);

      }
		#endregion

      private void SingleRadioButton_CheckedChanged(object sender, System.EventArgs e) 
      {
         if (this.SingleRadioButton.Checked)
            Type = AnswerType.atSingle;
      }
      
      private void MultipleRadioButton_CheckedChanged(object sender, System.EventArgs e) 
      {
         if (this.MultipleRadioButton.Checked)
            Type = AnswerType.atMultiple;
      }
      
      private void TextRadioButton_CheckedChanged(object sender, System.EventArgs e) 
      {
         if (this.TextRadioButton.Checked)
            Type = AnswerType.atTextbox;
      }

      private void CreateButton_Click(object sender, System.EventArgs e) 
      {
         ListAnswers.AddAnswerData();
      }

      private void RemoveButton_Click(object sender, System.EventArgs e) 
      {
         if (ListAnswers.Count > 0)
            ListAnswers.RemoveAnswerData(AnswersDataGrid.CurrentCell.RowNumber);
      }

      private void AnswersDataGrid_MouseAction(object sender, System.Windows.Forms.MouseEventArgs e)
      {
         ControlsEndEdit();
         this.ListAnswers.ValidateList(ListAnswers.FreshAnswer, this.Type);      
      }

      private void AnswersDataGrid_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
      {
         // если выделен втрой столбец и нажат пробел то
         ControlsEndEdit();
         this.ListAnswers.ValidateList(ListAnswers.FreshAnswer, this.Type);      
      }

      private void AnswersDataGrid_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
      {
         // если выделен втрой столбец и нажат пробел то
         ControlsEndEdit();
         this.ListAnswers.ValidateList(ListAnswers.FreshAnswer, this.Type);      
      }

      private void AnswersDataGrid_Validating(object sender, System.ComponentModel.CancelEventArgs e)
      {
      
      }
	}
}
