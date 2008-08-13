using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using DCEAccessLib;
using System.Data;

namespace DCEInternalSystem
{
	/// <summary>
	/// Выбор пользователя
	/// </summary>
	public class UserSelect : System.Windows.Forms.Form
	{
      private System.Windows.Forms.Panel panel1;
      private System.Windows.Forms.Button CancelBtn;
      public DCEAccessLib.DataList dataList;
      private DCEAccessLib.DataColumnHeader dataColumnHeader2;
      private DCEAccessLib.DataColumnHeader dataColumnHeader1;
      private DCEAccessLib.DataColumnHeader dataColumnHeader3;
      private DCEAccessLib.DataColumnHeader dataColumnHeader5;
      private DCEAccessLib.DataColumnHeader dataColumnHeader4;
      private System.Windows.Forms.Button button1;
      private System.Data.DataSet dataSet;
      public System.Data.DataView dataView;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public UserSelect(string caption, bool user)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

         if (user)
         {
            dataSet = DCEAccessLib.DCEWebAccess.WebAccess.GetDataSet("select * from dbo.Users","Users");
            dataView.Table = dataSet.Tables["Users"];
         }
         else
         {
            // student
            this.dataList.Columns.RemoveAt(3); // должность
			dataSet = DCEAccessLib.DAL.Student.GetDataSet();
            dataView.Table = dataSet.Tables["Students"];
         }

         this.Text = caption;
		}

      // все студенты тренинга
      public UserSelect(string caption, string trainingId)
      {
         InitializeComponent();
         this.Text = caption;
         this.dataList.Columns.RemoveAt(3); // должность
         dataSet = DCEAccessLib.DAL.Training.GetStudents(new Guid(trainingId));
         dataView.Table = dataSet.Tables["Students"];
      }

      public static DataRowView SelectUser(DataView excludeList)
      {
         return SelectUser(excludeList,"id");
      }

      /// <summary>
      /// Выбор пользователя
      /// </summary>
      /// <param name="excludeList"></param>
      /// <param name="idField"></param>
      /// <returns></returns>
      public static DataRowView SelectUser(DataView excludeList, string idField)
      {
         UserSelect sel = new UserSelect("Добавить пользователя",true);

         if (excludeList !=null)
         {
            foreach (DataRowView row in excludeList)
            {
               for (int i=0; i<sel.dataView.Count; i++)
               {
                  if (row[idField].ToString() == sel.dataView[i]["id"].ToString())
                  {
                     sel.dataView.Delete(i);
                     break;
                  }
               }
            }
         }
         if (sel.ShowDialog() == DialogResult.OK)
         {
            if (sel.dataList.SelectedItems.Count>0)
            {
               return ((DataRowView)sel.dataList.SelectedItems[0].Tag);
            }
            return null;
         }
         else
            return null;
      }

      /// <summary>
      /// Выбор студента
      /// </summary>
      /// <param name="excludeList"></param>
      /// <param name="idField"></param>
      /// <returns></returns>
      public static DataRowView SelectStudent(DataView excludeList, string idField)
      {
         UserSelect sel = new UserSelect("Выберите студента",false);

         if (excludeList !=null)
         {
            foreach (DataRowView row in excludeList)
            {
               for (int i=0; i<sel.dataView.Count; i++)
               {
                  if (row["id"].ToString() == sel.dataView[i][idField].ToString())
                  {
                     sel.dataView.Delete(i);
                     break;
                  }
               }
            }
         }
         if (sel.ShowDialog() == DialogResult.OK)
         {
            if (sel.dataList.SelectedItems.Count>0)
            {
               return ((DataRowView)sel.dataList.SelectedItems[0].Tag);
            }
            return null;
         }
         else
            return null;
      }

      /// <summary>
      /// Выбор студента тренинга
      /// </summary>
      /// <param name="excludeList"></param>
      /// <param name="idField"></param>
      /// <param name="trainingId"></param>
      /// <returns></returns>
      public static DataRowView SelectTrainingStudent(DataView excludeList, string idField, string trainingId)
      {
         UserSelect sel = new UserSelect("Выберите студента",trainingId);

         if (excludeList !=null)
         {
            foreach (DataRowView row in excludeList)
            {
               for (int i=0; i<sel.dataView.Count; i++)
               {
                  if (row[idField].ToString() == sel.dataView[i]["id"].ToString())
                  {
                     sel.dataView.Delete(i);
                     
                  }
               }
            }
         }
         if (sel.ShowDialog() == DialogResult.OK)
         {
            if (sel.dataList.SelectedItems.Count>0)
            {
               return ((DataRowView)sel.dataList.SelectedItems[0].Tag);
            }
            return null;
         }
         else
            return null;
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
         this.panel1 = new System.Windows.Forms.Panel();
         this.button1 = new System.Windows.Forms.Button();
         this.CancelBtn = new System.Windows.Forms.Button();
         this.dataList = new DCEAccessLib.DataList();
         this.dataColumnHeader2 = new DCEAccessLib.DataColumnHeader();
         this.dataColumnHeader1 = new DCEAccessLib.DataColumnHeader();
         this.dataColumnHeader3 = new DCEAccessLib.DataColumnHeader();
         this.dataColumnHeader5 = new DCEAccessLib.DataColumnHeader();
         this.dataColumnHeader4 = new DCEAccessLib.DataColumnHeader();
         this.dataView = new System.Data.DataView();
         this.dataSet = new System.Data.DataSet();
         this.panel1.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.dataView)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.dataSet)).BeginInit();
         this.SuspendLayout();
         // 
         // panel1
         // 
         this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.panel1.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                             this.button1,
                                                                             this.CancelBtn});
         this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
         this.panel1.Location = new System.Drawing.Point(0, 317);
         this.panel1.Name = "panel1";
         this.panel1.Size = new System.Drawing.Size(596, 28);
         this.panel1.TabIndex = 0;
         // 
         // button1
         // 
         this.button1.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
         this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.button1.Location = new System.Drawing.Point(348, 4);
         this.button1.Name = "button1";
         this.button1.Size = new System.Drawing.Size(120, 20);
         this.button1.TabIndex = 2;
         this.button1.Text = "Выбрать";
         this.button1.Click += new System.EventHandler(this.button1_Click);
         // 
         // CancelBtn
         // 
         this.CancelBtn.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
         this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
         this.CancelBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.CancelBtn.Location = new System.Drawing.Point(472, 4);
         this.CancelBtn.Name = "CancelBtn";
         this.CancelBtn.Size = new System.Drawing.Size(120, 20);
         this.CancelBtn.TabIndex = 3;
         this.CancelBtn.Text = "Отменить";
         // 
         // dataList
         // 
         this.dataList.Alignment = System.Windows.Forms.ListViewAlignment.Default;
         this.dataList.AllowColumnReorder = true;
         this.dataList.AllowSorting = true;
         this.dataList.BorderStyle = System.Windows.Forms.BorderStyle.None;
         this.dataList.Columns.AddRange(new DCEAccessLib.DataColumnHeader[] {
                                                                               this.dataColumnHeader2,
                                                                               this.dataColumnHeader1,
                                                                               this.dataColumnHeader3,
                                                                               this.dataColumnHeader5,
                                                                               this.dataColumnHeader4});
         this.dataList.DataView = this.dataView;
         this.dataList.Dock = System.Windows.Forms.DockStyle.Fill;
         this.dataList.FullRowSelect = true;
         this.dataList.GridLines = true;
         this.dataList.HideSelection = false;
         this.dataList.MultiSelect = false;
         this.dataList.Name = "dataList";
         this.dataList.Size = new System.Drawing.Size(596, 317);
         this.dataList.Sorting = System.Windows.Forms.SortOrder.Ascending;
         this.dataList.TabIndex = 1;
         this.dataList.View = System.Windows.Forms.View.Details;
         // 
         // dataColumnHeader2
         // 
         this.dataColumnHeader2.FieldName = "LastName";
         this.dataColumnHeader2.Text = "Фамилия";
         this.dataColumnHeader2.Width = 100;
         // 
         // dataColumnHeader1
         // 
         this.dataColumnHeader1.FieldName = "FirstName";
         this.dataColumnHeader1.Text = "Имя";
         this.dataColumnHeader1.Width = 100;
         // 
         // dataColumnHeader3
         // 
         this.dataColumnHeader3.FieldName = "Patronymic";
         this.dataColumnHeader3.Text = "Отчество";
         this.dataColumnHeader3.Width = 100;
         // 
         // dataColumnHeader5
         // 
         this.dataColumnHeader5.FieldName = "JobPosition";
         this.dataColumnHeader5.Text = "Должность";
         this.dataColumnHeader5.Width = 100;
         // 
         // dataColumnHeader4
         // 
         this.dataColumnHeader4.FieldName = "Email";
         this.dataColumnHeader4.Text = "Email";
         this.dataColumnHeader4.Width = 100;
         // 
         // dataSet
         // 
         this.dataSet.DataSetName = "NewDataSet";
         this.dataSet.Locale = new System.Globalization.CultureInfo("ru-RU");
         // 
         // UserSelect
         // 
         this.AcceptButton = this.button1;
         this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
         this.CancelButton = this.CancelBtn;
         this.ClientSize = new System.Drawing.Size(596, 345);
         this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                      this.dataList,
                                                                      this.panel1});
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
         this.MaximizeBox = false;
         this.MinimizeBox = false;
         this.Name = "UserSelect";
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "UserSelect";
         this.panel1.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.dataView)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.dataSet)).EndInit();
         this.ResumeLayout(false);

      }
		#endregion

      private void button1_Click(object sender, System.EventArgs e)
      {
         if (dataList.SelectedItems.Count == 0)
         {
            MessageBox.Show("Никто не выбран","Ошибка");
         }
         else
         {
            this.DialogResult = DialogResult.OK;
            this.Close();
         }
      }
	}
}
