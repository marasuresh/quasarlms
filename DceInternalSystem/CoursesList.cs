using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using DCEAccessLib;

namespace DCEInternalSystem
{
   /// <summary>
	/// Список курсов
	/// </summary>
   public class CoursesList : System.Windows.Forms.UserControl
	{
      public DataList dataList;
      private DataColumnHeader dataColumnHeader1;
      private System.Data.DataView dataView;
      private System.Data.DataSet dataSet;
      private DCEAccessLib.DataColumnHeader dataColumnHeader6;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public CoursesList()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

         dataSet = DCEAccessLib.DCEWebAccess.WebAccess.GetDataSet(
               @"select dbo.GetStrContentAlt(Name, 'RU', 'EN') as RName, 
                     dbo.GetStrContentAlt(Name, 'RU', 'EN') as CName, * 
                  from Courses where isReady=1 and CPublic=0", "Courses");
         dataView.Table = dataSet.Tables["Courses"];
      }
         
      /// <summary>
      /// получает список курсов за исключением тех, которые присутствуют в Excludes
      /// </summary>
      /// <param name="Excludes"></param>
      public void GenList (DataView Excludes)
      {
         this.dataList.SuspendListChange = true;
         try
         {
            dataSet = DCEAccessLib.DCEWebAccess.WebAccess.GetDataSet(
               @"select dbo.GetStrContentAlt(Name, 'RU', 'EN') as RName, 
                     dbo.GetStrContentAlt(Name, 'RU', 'EN') as CName, * 
                  from Courses where isReady=1 and CPublic=0","Courses");

            dataView.Table = dataSet.Tables["Courses"];
            if (Excludes !=null)
            {
               for (int i=0; i<Excludes.Count; i++)
               {
                  for(int j=0; j<dataView.Count;j++)
                  {
                     if (dataView[j]["id"].ToString()== Excludes[i]["id"].ToString())
                     {
                        dataView.Delete(j);
                        break;
                     }
                  }
               }
            }
         }
         finally
         {
            this.dataList.SuspendListChange = false;
            this.dataList.UpdateList();
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
      this.dataView = new System.Data.DataView();
      this.dataSet = new System.Data.DataSet();
      this.dataList = new DCEAccessLib.DataList();
      this.dataColumnHeader1 = new DCEAccessLib.DataColumnHeader();
      this.dataColumnHeader6 = new DCEAccessLib.DataColumnHeader();
      ((System.ComponentModel.ISupportInitialize)(this.dataView)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.dataSet)).BeginInit();
      this.SuspendLayout();
      // 
      // dataSet
      // 
      this.dataSet.DataSetName = "NewDataSet";
      this.dataSet.Locale = new System.Globalization.CultureInfo("ru-RU");
      // 
      // dataList
      // 
      this.dataList.Alignment = System.Windows.Forms.ListViewAlignment.Default;
      this.dataList.AllowColumnReorder = true;
      this.dataList.AllowSorting = true;
      this.dataList.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.dataList.Columns.AddRange(new DCEAccessLib.DataColumnHeader[] {
                                                                            this.dataColumnHeader1,
                                                                            this.dataColumnHeader6});
      this.dataList.DataView = this.dataView;
      this.dataList.Dock = System.Windows.Forms.DockStyle.Fill;
      this.dataList.FullRowSelect = true;
      this.dataList.GridLines = true;
      this.dataList.HideSelection = false;
      this.dataList.MultiSelect = false;
      this.dataList.Name = "dataList";
      this.dataList.Size = new System.Drawing.Size(392, 368);
      this.dataList.Sorting = System.Windows.Forms.SortOrder.Ascending;
      this.dataList.TabIndex = 9;
      this.dataList.View = System.Windows.Forms.View.Details;
      // 
      // dataColumnHeader1
      // 
      this.dataColumnHeader1.FieldName = "CName";
      this.dataColumnHeader1.Text = "Название";
      this.dataColumnHeader1.Width = 250;
      // 
      // dataColumnHeader6
      // 
      this.dataColumnHeader6.FieldName = "Version";
      this.dataColumnHeader6.Text = "Версия";
      this.dataColumnHeader6.Width = 100;
      // 
      // CoursesList
      // 
      this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                   this.dataList});
      this.Name = "CoursesList";
      this.Size = new System.Drawing.Size(392, 368);
      ((System.ComponentModel.ISupportInitialize)(this.dataView)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.dataSet)).EndInit();
      this.ResumeLayout(false);

   }
   #endregion
	}
   /// <summary>
   /// Класс описывающий ноду "Курсы"
   /// </summary>
   public class CoursesControl : NodeControl
   {
      /// <summary>
      /// наследовано от NodeControl
      /// </summary>
      /// <returns></returns>
      public override System.Windows.Forms.UserControl GetControl()
      {
         if (this.fControl == null)
         {
            this.fControl = new DCEInternalSystem.CoursesList();
         }
         return this.fControl;
      }

      /// <summary>
      /// конструктор
      /// </summary>
      /// <param name="parent"></param>
      public CoursesControl (NodeControl parent)
         : base(parent) 
      {}

      /// <summary>
      /// наследовано от NodeControl
      /// </summary>
      /// <returns></returns>
      public override String GetCaption()
      {
         return "Курсы";
      }
      
      /// <summary>
      /// наследовано от NodeControl
      /// </summary>
      /// <returns></returns>
      public override bool HaveChildNodes() { return true; }
   }
}
