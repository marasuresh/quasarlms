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
	/// Расписание
	/// </summary>
	public class TrainingSchedule : System.Windows.Forms.UserControl
	{
      private System.Windows.Forms.Panel panel1;
      private System.Windows.Forms.Label label8;
      private System.Windows.Forms.Label label6;
      private System.Windows.Forms.Label TitleLabel;
      private System.Windows.Forms.Panel panel2;
      private System.Windows.Forms.Label label2;
      private SpinEdit Duration;
      private System.Windows.Forms.DateTimePicker EndDatePicker;
      private System.Windows.Forms.Label label4;
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.Label label3;
      private System.Windows.Forms.Label label9;
      private System.Windows.Forms.Label label10;
      private System.Windows.Forms.Label label11;
      private System.ComponentModel.IContainer components;

      private TrainingScheduleControl Node;
      private System.Data.DataSet dataSet;
      private System.Data.DataView dataView;
      private DCEAccessLib.DataList dataList;
      private DCEAccessLib.DataColumnHeader dataColumnHeader5;
      private DCEAccessLib.DataColumnHeader dataColumnHeader6;
      private DCEAccessLib.DataColumnHeader dataColumnHeader7;
      private DCEAccessLib.DataColumnHeader dataColumnHeader8;
      private DCEAccessLib.DataColumnHeader dataColumnHeader9;
      private DCEAccessLib.DataColumnHeader dataColumnHeader1;
      private DCEAccessLib.DataColumnHeader dataColumnHeader2;
      private System.Windows.Forms.CheckBox McheckBox;
      private System.Windows.Forms.Label FullWorkDays;
      private System.Windows.Forms.Label FullCalDays;
      private System.Windows.Forms.Label FullStartDate;
      private System.Windows.Forms.Label FullEndDate;
      private System.Windows.Forms.Panel panel3;
      private System.Windows.Forms.Button OkButton;
      private System.Windows.Forms.Button CancelButton;
      private System.Windows.Forms.ToolTip toolTip1;
      private System.Windows.Forms.CheckBox ThemeOpen;
      private System.Windows.Forms.DateTimePicker StartDatePicker;


      DataRowChangeEventHandler RowChangeHandler;
      protected void OnRowChanging(object obj, DataRowChangeEventArgs e)
      {
         this.Node.Changed = true;
      }

		public TrainingSchedule(TrainingScheduleControl node)
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

         this.Node = node;
         this.RowChangeHandler = new DataRowChangeEventHandler(OnRowChanging);
         RefreshData();

         if (this.Node.Readonly)
         {
            StartDatePicker.Enabled = false;
            EndDatePicker.Enabled = false;
            McheckBox.Enabled = false;
            Duration.Enabled = false;
            OkButton.Enabled = false;
            ThemeOpen.Enabled = false;
         }
		}

      public void RefreshData()
      {
         this.dataSet = DCEAccessLib.DCEWebAccess.WebAccess.GetDataSet(
            "select dbo.GetStrContentAlt(t.Name,'RU','EN') as TName, s.*,t.Duration from Schedule s, Themes t where s.Training = '"+Node.TrainingId 
            + "' and t.id = s.Theme order by s.StartDate","schedule");
         this.dataSet.Tables["schedule"].Columns.Add("WorkDays",typeof(int),"");
         CountWorkDays();
         this.dataView.Table = this.dataSet.Tables["schedule"];
         CalcTotals();
         this.dataSet.Tables["schedule"].RowChanging += this.RowChangeHandler;
         this.Node.Changed = false;
      }
		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
         currentRow = null;
         Node = null;
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
         this.components = new System.ComponentModel.Container();
         this.dataView = new System.Data.DataView();
         this.panel1 = new System.Windows.Forms.Panel();
         this.panel3 = new System.Windows.Forms.Panel();
         this.OkButton = new System.Windows.Forms.Button();
         this.CancelButton = new System.Windows.Forms.Button();
         this.FullEndDate = new System.Windows.Forms.Label();
         this.FullStartDate = new System.Windows.Forms.Label();
         this.label11 = new System.Windows.Forms.Label();
         this.label10 = new System.Windows.Forms.Label();
         this.label9 = new System.Windows.Forms.Label();
         this.panel2 = new System.Windows.Forms.Panel();
         this.ThemeOpen = new System.Windows.Forms.CheckBox();
         this.McheckBox = new System.Windows.Forms.CheckBox();
         this.label3 = new System.Windows.Forms.Label();
         this.label2 = new System.Windows.Forms.Label();
         this.Duration = new DCEAccessLib.SpinEdit();
         this.EndDatePicker = new System.Windows.Forms.DateTimePicker();
         this.StartDatePicker = new System.Windows.Forms.DateTimePicker();
         this.label4 = new System.Windows.Forms.Label();
         this.label1 = new System.Windows.Forms.Label();
         this.TitleLabel = new System.Windows.Forms.Label();
         this.FullWorkDays = new System.Windows.Forms.Label();
         this.label8 = new System.Windows.Forms.Label();
         this.FullCalDays = new System.Windows.Forms.Label();
         this.label6 = new System.Windows.Forms.Label();
         this.dataSet = new System.Data.DataSet();
         this.dataList = new DCEAccessLib.DataList();
         this.dataColumnHeader5 = new DCEAccessLib.DataColumnHeader();
         this.dataColumnHeader6 = new DCEAccessLib.DataColumnHeader();
         this.dataColumnHeader7 = new DCEAccessLib.DataColumnHeader();
         this.dataColumnHeader8 = new DCEAccessLib.DataColumnHeader();
         this.dataColumnHeader9 = new DCEAccessLib.DataColumnHeader();
         this.dataColumnHeader1 = new DCEAccessLib.DataColumnHeader();
         this.dataColumnHeader2 = new DCEAccessLib.DataColumnHeader();
         this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
         ((System.ComponentModel.ISupportInitialize)(this.dataView)).BeginInit();
         this.panel1.SuspendLayout();
         this.panel3.SuspendLayout();
         this.panel2.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.Duration)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.dataSet)).BeginInit();
         this.SuspendLayout();
         // 
         // panel1
         // 
         this.panel1.Controls.Add(this.panel3);
         this.panel1.Controls.Add(this.FullEndDate);
         this.panel1.Controls.Add(this.FullStartDate);
         this.panel1.Controls.Add(this.label11);
         this.panel1.Controls.Add(this.label10);
         this.panel1.Controls.Add(this.label9);
         this.panel1.Controls.Add(this.panel2);
         this.panel1.Controls.Add(this.TitleLabel);
         this.panel1.Controls.Add(this.FullWorkDays);
         this.panel1.Controls.Add(this.label8);
         this.panel1.Controls.Add(this.FullCalDays);
         this.panel1.Controls.Add(this.label6);
         this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
         this.panel1.Location = new System.Drawing.Point(0, 252);
         this.panel1.Name = "panel1";
         this.panel1.Size = new System.Drawing.Size(620, 172);
         this.panel1.TabIndex = 16;
         // 
         // panel3
         // 
         this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.panel3.Controls.Add(this.OkButton);
         this.panel3.Controls.Add(this.CancelButton);
         this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
         this.panel3.Location = new System.Drawing.Point(0, 140);
         this.panel3.Name = "panel3";
         this.panel3.Size = new System.Drawing.Size(620, 32);
         this.panel3.TabIndex = 206;
         // 
         // OkButton
         // 
         this.OkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
         this.OkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
         this.OkButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.OkButton.Location = new System.Drawing.Point(412, 2);
         this.OkButton.Name = "OkButton";
         this.OkButton.Size = new System.Drawing.Size(96, 24);
         this.OkButton.TabIndex = 7;
         this.OkButton.Text = "Сохранить";
         this.toolTip1.SetToolTip(this.OkButton, "Сохранить текущие изменения");
         this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
         // 
         // CancelButton
         // 
         this.CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
         this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
         this.CancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.CancelButton.Location = new System.Drawing.Point(516, 2);
         this.CancelButton.Name = "CancelButton";
         this.CancelButton.Size = new System.Drawing.Size(96, 24);
         this.CancelButton.TabIndex = 8;
         this.CancelButton.Text = "Отменить";
         this.toolTip1.SetToolTip(this.CancelButton, "Отменить текущие изменения");
         this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
         // 
         // FullEndDate
         // 
         this.FullEndDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
         this.FullEndDate.Location = new System.Drawing.Point(456, 100);
         this.FullEndDate.Name = "FullEndDate";
         this.FullEndDate.Size = new System.Drawing.Size(128, 12);
         this.FullEndDate.TabIndex = 205;
         this.FullEndDate.Text = "label13";
         this.FullEndDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // FullStartDate
         // 
         this.FullStartDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
         this.FullStartDate.Location = new System.Drawing.Point(184, 100);
         this.FullStartDate.Name = "FullStartDate";
         this.FullStartDate.Size = new System.Drawing.Size(120, 12);
         this.FullStartDate.TabIndex = 204;
         this.FullStartDate.Text = "label12";
         this.FullStartDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // label11
         // 
         this.label11.Location = new System.Drawing.Point(312, 100);
         this.label11.Name = "label11";
         this.label11.Size = new System.Drawing.Size(128, 12);
         this.label11.TabIndex = 16;
         this.label11.Text = "Дата окончания:";
         this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // label10
         // 
         this.label10.Location = new System.Drawing.Point(12, 100);
         this.label10.Name = "label10";
         this.label10.Size = new System.Drawing.Size(156, 12);
         this.label10.TabIndex = 15;
         this.label10.Text = "Дата начала:";
         this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // label9
         // 
         this.label9.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(122)), ((System.Byte)(170)), ((System.Byte)(190)));
         this.label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.label9.Dock = System.Windows.Forms.DockStyle.Top;
         this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
         this.label9.ForeColor = System.Drawing.SystemColors.Info;
         this.label9.Location = new System.Drawing.Point(0, 76);
         this.label9.Name = "label9";
         this.label9.Size = new System.Drawing.Size(620, 20);
         this.label9.TabIndex = 14;
         this.label9.Text = "Всего";
         this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // panel2
         // 
         this.panel2.Controls.Add(this.ThemeOpen);
         this.panel2.Controls.Add(this.McheckBox);
         this.panel2.Controls.Add(this.label3);
         this.panel2.Controls.Add(this.label2);
         this.panel2.Controls.Add(this.Duration);
         this.panel2.Controls.Add(this.EndDatePicker);
         this.panel2.Controls.Add(this.StartDatePicker);
         this.panel2.Controls.Add(this.label4);
         this.panel2.Controls.Add(this.label1);
         this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
         this.panel2.Location = new System.Drawing.Point(0, 20);
         this.panel2.Name = "panel2";
         this.panel2.Size = new System.Drawing.Size(620, 56);
         this.panel2.TabIndex = 13;
         // 
         // ThemeOpen
         // 
         this.ThemeOpen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.ThemeOpen.Location = new System.Drawing.Point(428, 8);
         this.ThemeOpen.Name = "ThemeOpen";
         this.ThemeOpen.Size = new System.Drawing.Size(116, 16);
         this.ThemeOpen.TabIndex = 4;
         this.ThemeOpen.Text = "Открыта";
         this.ThemeOpen.Click += new System.EventHandler(this.ThemeOpen_Click);
         // 
         // McheckBox
         // 
         this.McheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.McheckBox.Location = new System.Drawing.Point(312, 8);
         this.McheckBox.Name = "McheckBox";
         this.McheckBox.Size = new System.Drawing.Size(104, 16);
         this.McheckBox.TabIndex = 3;
         this.McheckBox.Text = "Обязательная";
         this.McheckBox.Click += new System.EventHandler(this.McheckBox_Click);
         // 
         // label3
         // 
         this.label3.Location = new System.Drawing.Point(480, 32);
         this.label3.Name = "label3";
         this.label3.Size = new System.Drawing.Size(108, 16);
         this.label3.TabIndex = 19;
         this.label3.Text = "рабочих дней";
         // 
         // label2
         // 
         this.label2.Location = new System.Drawing.Point(312, 32);
         this.label2.Name = "label2";
         this.label2.Size = new System.Drawing.Size(100, 16);
         this.label2.TabIndex = 18;
         this.label2.Text = "Длительность";
         // 
         // Duration
         // 
         this.Duration.Location = new System.Drawing.Point(416, 28);
         this.Duration.Maximum = new System.Decimal(new int[] {
                                                                 100000,
                                                                 0,
                                                                 0,
                                                                 0});
         this.Duration.Minimum = new System.Decimal(new int[] {
                                                                 1,
                                                                 0,
                                                                 0,
                                                                 0});
         this.Duration.Name = "Duration";
         this.Duration.Size = new System.Drawing.Size(56, 20);
         this.Duration.TabIndex = 6;
         this.Duration.Value = new System.Decimal(new int[] {
                                                               1,
                                                               0,
                                                               0,
                                                               0});
         this.Duration.ValueChanged += new System.EventHandler(this.Duration_ValueChanged);
         // 
         // EndDatePicker
         // 
         this.EndDatePicker.Location = new System.Drawing.Point(136, 28);
         this.EndDatePicker.Name = "EndDatePicker";
         this.EndDatePicker.Size = new System.Drawing.Size(160, 20);
         this.EndDatePicker.TabIndex = 5;
         this.EndDatePicker.ValueChanged += new System.EventHandler(this.EndDatePicker_ValueChanged);
         // 
         // StartDatePicker
         // 
         this.StartDatePicker.Location = new System.Drawing.Point(136, 4);
         this.StartDatePicker.Name = "StartDatePicker";
         this.StartDatePicker.Size = new System.Drawing.Size(160, 20);
         this.StartDatePicker.TabIndex = 2;
         this.StartDatePicker.ValueChanged += new System.EventHandler(this.StartDatePicker_ValueChanged);
         // 
         // label4
         // 
         this.label4.Location = new System.Drawing.Point(8, 32);
         this.label4.Name = "label4";
         this.label4.Size = new System.Drawing.Size(120, 16);
         this.label4.TabIndex = 14;
         this.label4.Text = "Дата окончания:";
         // 
         // label1
         // 
         this.label1.Location = new System.Drawing.Point(8, 8);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(120, 16);
         this.label1.TabIndex = 13;
         this.label1.Text = "Дата начала:";
         // 
         // TitleLabel
         // 
         this.TitleLabel.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(122)), ((System.Byte)(170)), ((System.Byte)(190)));
         this.TitleLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.TitleLabel.Dock = System.Windows.Forms.DockStyle.Top;
         this.TitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
         this.TitleLabel.ForeColor = System.Drawing.SystemColors.Info;
         this.TitleLabel.Location = new System.Drawing.Point(0, 0);
         this.TitleLabel.Name = "TitleLabel";
         this.TitleLabel.Size = new System.Drawing.Size(620, 20);
         this.TitleLabel.TabIndex = 11;
         this.TitleLabel.Text = "Тема:";
         this.TitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // FullWorkDays
         // 
         this.FullWorkDays.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
         this.FullWorkDays.Location = new System.Drawing.Point(456, 120);
         this.FullWorkDays.Name = "FullWorkDays";
         this.FullWorkDays.Size = new System.Drawing.Size(32, 16);
         this.FullWorkDays.TabIndex = 7;
         this.FullWorkDays.Text = "20";
         this.FullWorkDays.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         this.toolTip1.SetToolTip(this.FullWorkDays, "Суммарное количество рабочих дней тренинга");
         // 
         // label8
         // 
         this.label8.Location = new System.Drawing.Point(312, 120);
         this.label8.Name = "label8";
         this.label8.Size = new System.Drawing.Size(136, 16);
         this.label8.TabIndex = 6;
         this.label8.Text = "Всего рабочих дней:";
         this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         this.toolTip1.SetToolTip(this.label8, "Суммарное количество рабочих дней тренинга");
         // 
         // FullCalDays
         // 
         this.FullCalDays.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
         this.FullCalDays.Location = new System.Drawing.Point(184, 120);
         this.FullCalDays.Name = "FullCalDays";
         this.FullCalDays.Size = new System.Drawing.Size(24, 16);
         this.FullCalDays.TabIndex = 5;
         this.FullCalDays.Text = "30";
         this.FullCalDays.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         this.toolTip1.SetToolTip(this.FullCalDays, "Суммарное количество календарных дней тренинга (не включая промежутки между темам" +
            "и)");
         // 
         // label6
         // 
         this.label6.Location = new System.Drawing.Point(12, 120);
         this.label6.Name = "label6";
         this.label6.Size = new System.Drawing.Size(172, 16);
         this.label6.TabIndex = 4;
         this.label6.Text = "Всего календарных дней:";
         this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         this.toolTip1.SetToolTip(this.label6, "Суммарное количество календарных дней тренинга (не включая промежутки между темам" +
            "и)");
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
         this.dataList.AllowSorting = false;
         this.dataList.BorderStyle = System.Windows.Forms.BorderStyle.None;
         this.dataList.Columns.AddRange(new DCEAccessLib.DataColumnHeader[] {
                                                                               this.dataColumnHeader5,
                                                                               this.dataColumnHeader6,
                                                                               this.dataColumnHeader7,
                                                                               this.dataColumnHeader8,
                                                                               this.dataColumnHeader9,
                                                                               this.dataColumnHeader1,
                                                                               this.dataColumnHeader2});
         this.dataList.DataView = this.dataView;
         this.dataList.Dock = System.Windows.Forms.DockStyle.Fill;
         this.dataList.FullRowSelect = true;
         this.dataList.GridLines = true;
         this.dataList.HideSelection = false;
         this.dataList.Location = new System.Drawing.Point(0, 0);
         this.dataList.MultiSelect = false;
         this.dataList.Name = "dataList";
         this.dataList.Size = new System.Drawing.Size(620, 252);
         this.dataList.TabIndex = 1;
         this.dataList.View = System.Windows.Forms.View.Details;
         this.dataList.SelectedIndexChanged += new System.EventHandler(this.dataList_SelectedIndexChanged);
         // 
         // dataColumnHeader5
         // 
         this.dataColumnHeader5.FieldName = "TName";
         this.dataColumnHeader5.Text = "Тема";
         this.dataColumnHeader5.Width = 200;
         // 
         // dataColumnHeader6
         // 
         this.dataColumnHeader6.FieldName = "StartDate";
         this.dataColumnHeader6.Text = "Начало";
         this.dataColumnHeader6.Width = 70;
         // 
         // dataColumnHeader7
         // 
         this.dataColumnHeader7.FieldName = "EndDate";
         this.dataColumnHeader7.Text = "Окончание";
         this.dataColumnHeader7.Width = 70;
         // 
         // dataColumnHeader8
         // 
         this.dataColumnHeader8.FieldName = "isOpen";
         this.dataColumnHeader8.Text = "Открыта";
         // 
         // dataColumnHeader9
         // 
         this.dataColumnHeader9.FieldName = "Mandatory";
         this.dataColumnHeader9.Text = "Обязательна";
         this.dataColumnHeader9.Width = 80;
         // 
         // dataColumnHeader1
         // 
         this.dataColumnHeader1.FieldName = "WorkDays";
         this.dataColumnHeader1.Text = "Раб. дни";
         // 
         // dataColumnHeader2
         // 
         this.dataColumnHeader2.FieldName = "Duration";
         this.dataColumnHeader2.Text = "Дни (умолч.)";
         // 
         // toolTip1
         // 
         this.toolTip1.AutomaticDelay = 5000;
         this.toolTip1.AutoPopDelay = 60000;
         this.toolTip1.InitialDelay = 500;
         this.toolTip1.ReshowDelay = 100;
         this.toolTip1.ShowAlways = true;
         // 
         // TrainingSchedule
         // 
         this.Controls.Add(this.dataList);
         this.Controls.Add(this.panel1);
         this.Name = "TrainingSchedule";
         this.Size = new System.Drawing.Size(620, 424);
         ((System.ComponentModel.ISupportInitialize)(this.dataView)).EndInit();
         this.panel1.ResumeLayout(false);
         this.panel3.ResumeLayout(false);
         this.panel2.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.Duration)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.dataSet)).EndInit();
         this.ResumeLayout(false);

      }
		#endregion


      private int updating = 0;
      private DataRowView currentRow;
      private DataRowView CurrentRow
      {
         get
         {
            return currentRow;
         }
         set
         {
            updating++;
            try
            {
               currentRow = value;
               if (currentRow != null)
               {
                  this.StartDatePicker.Value = (DateTime)currentRow["StartDate"];
                  this.EndDatePicker.MinDate = (DateTime) this.currentRow["StartDate"];
                  Duration.Value = (int) this.currentRow["WorkDays"];            
                  this.EndDatePicker.Value = (DateTime)currentRow["EndDate"];
                  this.TitleLabel.Text = "Тема: "+ currentRow["TName"].ToString();
                  this.McheckBox.Checked = (bool)currentRow["Mandatory"];
                  ThemeOpen.Checked = (bool) currentRow["isOpen"];
               }
               else
               {
                  this.TitleLabel.Text = "Тема: <не выбрана>";
               }
            }
            finally
            {
               updating--;
            }
         }
      }
      private int CurrentIndex;

      private void dataList_SelectedIndexChanged(object sender, System.EventArgs e)
      {
         updating++;
         try
         {
            if (this.dataList.SelectedItems.Count>0 && 
               this.dataList.SelectedItems[0].Tag !=null)
            {
               this.CurrentIndex = this.dataList.SelectedItems[0].Index;
               if (CurrentIndex >0)
               {
                  DataRowView prev = (DataRowView)this.dataList.Items[this.CurrentIndex-1].Tag;
                  this.StartDatePicker.MinDate = ((DateTime)prev["EndDate"]).AddDays(1);
               }
               else
                  this.StartDatePicker.MinDate = new DateTime(1900,1,1);
               this.CurrentRow = (DataRowView)(this.dataList.SelectedItems[0].Tag);
            }
            else
            {
               this.CurrentRow = null;
               this.CurrentIndex=-1;
            }
         }
         finally
         {
            updating--;
         }
      }

      private void CountWorkDays()
      {
         foreach (DataRow row in this.dataSet.Tables["schedule"].Rows)
         {
            row["WorkDays"] = CountWorkDays((DateTime) row["StartDate"],(DateTime)row["EndDate"]);
         }
      }

      private int CountWorkDays(DateTime start, DateTime end)
      {
         int days = 0;
         while (start < end)
         {
            if (!(start.DayOfWeek == DayOfWeek.Saturday ||
                  start.DayOfWeek == DayOfWeek.Sunday) )
               days++;
            start = start.AddDays(1);
         }
         return days+1;
      }

      private void CalcTotals()
      {
         IEnumerator en = this.dataView.GetEnumerator();
         en.Reset();
         DateTime trStart = new DateTime(1900,1,1);
         DateTime trEnd = new DateTime(1900,1,1);
         int WorkDays = 0;
         int CalDays = 0;

         bool first = true;
         while (en.MoveNext())
         {
            DataRowView row = (DataRowView)en.Current;
            if (first)
            {
               trStart = (DateTime) row["StartDate"];
               first = false; 
            }
            trEnd = (DateTime) row["EndDate"];

            TimeSpan span = (DateTime)row["EndDate"] -  (DateTime)row["StartDate"];
            CalDays += 1+(int)span.Days;
            WorkDays += (int)row["WorkDays"];
         }
         if (!first)
         {
            this.FullCalDays.Text = CalDays.ToString();
            this.FullWorkDays.Text = WorkDays.ToString();
            this.FullStartDate.Text = trStart.ToString(DCEAccessLib.Settings.DateFormat);
            this.FullEndDate.Text = trEnd.ToString(DCEAccessLib.Settings.DateFormat);
         }
         else
         {
            this.FullCalDays.Text = "";
            this.FullWorkDays.Text= "";
            this.FullStartDate.Text ="";
            this.FullEndDate.Text ="";
         }
      }

      private void ShiftDates(DateTime startdate)
      {
         try
         {
            updating++;
            bool shiftdates = false;
            startdate = TrainingEditControl.SkipHolydays(startdate,1);
            IEnumerator en = this.dataView.GetEnumerator();
            en.Reset();

            while (en.MoveNext())
            {
               DataRowView row = (DataRowView)en.Current;

               if (shiftdates)
               {
                  row.BeginEdit();
                  int days = (int)row["WorkDays"];//CountWorkDays((DateTime)row["StartDate"],(DateTime)row["EndDate"]);
                  //row["WorkDays"] = days;
                  row["StartDate"] = startdate;
                  startdate = TrainingEditControl.SkipHolydays(startdate,days-1);
                  row["EndDate"] = startdate;
                  startdate = TrainingEditControl.SkipHolydays(startdate,1);
                  row.EndEdit();
               }

               if (row.Row == this.currentRow.Row)
               {
                  shiftdates = true;
               }
            }

         }
         finally
         {
            updating--;
         }
      }

      private void StartDatePicker_ValueChanged(object sender, System.EventArgs e)
      {
         // starting date changed
         if (this.CurrentRow != null && updating==0)
         {
            updating++;
            try
            {
               this.dataList.SuspendListChange = true;
               int days =(int) CurrentRow["WorkDays"];// CountWorkDays((DateTime)CurrentRow["StartDate"],(DateTime)CurrentRow["EndDate"]);
               DateTime startdate = TrainingEditControl.SkipHolydays(StartDatePicker.Value,0);
               if (this.CurrentIndex > 0)
               {
                  DataRowView prev = (DataRowView)this.dataList.Items[this.CurrentIndex-1].Tag;
                  if (startdate < (DateTime)prev["EndDate"])
                  {
                     throw new DCEException("Дата начала должна быть позднее даты окончания предыдущей темы.",DCEException.ExceptionLevel.InvalidAction);
                  }
               }
                  
               DateTime enddate = TrainingEditControl.SkipHolydays(startdate,days-1);

               this.currentRow.BeginEdit();
               this.currentRow["EndDate"] = enddate;
               this.EndDatePicker.MinDate = startdate;
               this.EndDatePicker.Value = enddate;
               this.currentRow["StartDate"] = startdate;
               this.currentRow.EndEdit();

               this.ShiftDates(enddate);
               CalcTotals();
            }
            finally
            {
               updating--;
               this.dataList.SuspendListChange = false;
               this.dataList.UpdateList();
            }
         }
      }

      private void EndDatePicker_ValueChanged(object sender, System.EventArgs e)
      {
         // end date changed
         if (this.CurrentRow != null && updating==0)
         {
            updating++;
            try
            {
               DateTime enddate = TrainingEditControl.SkipHolydays(this.EndDatePicker.Value,0);
               this.dataList.SuspendListChange = true;
               this.currentRow.BeginEdit();
               this.currentRow["WorkDays"] = CountWorkDays((DateTime)this.currentRow["StartDate"],enddate);
               this.currentRow["EndDate"] = enddate;
               this.currentRow.EndEdit();
               this.ShiftDates(enddate);
               CalcTotals();
            }
            finally
            {
               updating--;
               this.dataList.SuspendListChange = false;
               this.dataList.UpdateList();
            }
         }
      }

      private void Duration_ValueChanged(object sender, System.EventArgs e)
      {
         // duration changed
         if (this.CurrentRow != null && updating==0)
         {
            updating++;
            try
            {
               DateTime enddate = TrainingEditControl.SkipHolydays(this.StartDatePicker.Value,(int)Duration.Value-1);
               this.dataList.SuspendListChange = true;
               this.currentRow.BeginEdit();
               this.currentRow["WorkDays"] = CountWorkDays((DateTime)this.currentRow["StartDate"],enddate);
               this.currentRow["EndDate"] = enddate;
               this.currentRow.EndEdit();
               this.ShiftDates(enddate);
               CalcTotals();
            }
            finally
            {
               updating--;
               this.dataList.SuspendListChange = false;
               this.dataList.UpdateList();
            }
         }
      }

      private void OkButton_Click(object sender, System.EventArgs e)
      {
         this.dataList.SuspendListChange = true;
         try
         {
            updating++;
            this.dataSet.Tables["schedule"].Columns.Remove("WorkDays");
            this.dataSet.Tables["schedule"].Columns.Remove("TName");
            this.dataSet.Tables["schedule"].Columns.Remove("Duration");
//            DCEAccessLib.DCEWebAccess.WebAccess.UpdateDataSet("select * from Schedule where Training='"+Node.TrainingId +"'",
//               "schedule",ref   this.dataSet);
            DateTime start = (DateTime) this.dataSet.Tables["schedule"].Rows[0]["StartDate"];
            DateTime end = (DateTime) this.dataSet.Tables["schedule"].Rows[this.dataSet.Tables["schedule"].Rows.Count-1]["EndDate"];

            DCEAccessLib.DCEWebAccess.WebAccess.UpdateDataSets(
               new string[] {"select * from Schedule where Training='"+Node.TrainingId +"'" ,
            "update dbo.Trainings set StartDate = '"+DCEAccessLib.Settings.ToSQLDate(start)+"' , EndDate = '"
                               +DCEAccessLib.Settings.ToSQLDate(end)+"' where id='"+Node.TrainingId+"'"},
               new string[] {"schedule",""},
               new DataSet[] {this.dataSet,null}
            );
            this.RefreshData();
         }
         finally
         {
            updating--;
            this.dataList.SuspendListChange = false;
            this.dataList.UpdateList();
         }
      }

      private void CancelButton_Click(object sender, System.EventArgs e)
      {
         this.RefreshData();
      }

      private void McheckBox_Click(object sender, System.EventArgs e)
      {
         if (this.CurrentRow != null && updating==0)
         {
            updating++;
            try
            {
               this.currentRow.BeginEdit();
               this.currentRow["Mandatory"] = McheckBox.Checked;
               this.currentRow.EndEdit();

            }
            finally
            {
               updating--;
               this.dataList.SuspendListChange = false;
               this.dataList.UpdateList();
            }
         }      
      }

      private void ThemeOpen_Click(object sender, System.EventArgs e)
      {
         if (this.CurrentRow != null && updating==0)
         {
            updating++;
            try
            {
               this.currentRow.BeginEdit();
               this.currentRow["isOpen"] = ThemeOpen.Checked;
               this.currentRow.EndEdit();

            }
            finally
            {
               updating--;
               this.dataList.SuspendListChange = false;
               this.dataList.UpdateList();
            }
         }      
      }

   }

   public class TrainingScheduleControl : NodeControl
   {
      public override System.Windows.Forms.UserControl GetControl()
      {
         if (this.fControl == null)
         {
            this.fControl = new DCEInternalSystem.TrainingSchedule(this);
         }
         return this.fControl;
      }

      bool changed = false;
      public bool Changed
      {
         get 
         {
            return changed;
         }
         set
         {
            if (this.changed != value)
            {
               this.changed  = value;
               this.CaptionChanged();
            }
         }
      }

      public override void CaptionChanged()
      {
         string caption = this.GetCaption();
         if (this.changed)
            caption = "*" + caption;

         if (NodeControl.SelectedNode == this)
         {
            if (NodeControl.NodeLabel != null)
               NodeControl.NodeLabel.Text = caption;
         }
         if (this.treeNode != null)
         {
            this.treeNode.Text = caption;
         }
      }

      public override bool CanClose()
      {
         return false;
      }

      public string TrainingId;

      public TrainingScheduleControl(NodeControl parent, string trainingId)
         : base(parent)
      {
         this.rdonly = DCEUser.CurrentUser.Shedule != DCEUser.Access.Modify;
         this.TrainingId = trainingId;
      }

      public override String GetCaption()
      {
         return "Расписание";
      }

      public override bool HaveChildNodes()
      {
         return false;
      }
      public override void ReleaseControl()
      {
      // do nothing
      }

   }
}
