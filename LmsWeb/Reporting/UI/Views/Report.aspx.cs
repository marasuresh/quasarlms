using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using N2.Collections;
using N2.ACalendar;
using N2.Lms.Items;


using System.Data;



public partial class Reporting_UI_Report : N2.Templates.Web.UI.TemplatePage<N2.ACalendar.Reporting.Report>
    {

        protected string CurrentUserName
        {
            get { return this.Context.User.Identity.Name; }
        }

        //DropDownList ddlNewMsgByType;

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnGet_Click(object sender, EventArgs e)
        {
            string showingMsg = null;
         
            //Фильтровка сообщений выводимых на экран.
            switch (this.ddlReportType.SelectedValue)
            {
                case "erv"://Экзаменационно рейтинговые ведомости
                    showingMsg = this.ddlReportType.SelectedValue.ToString();
                    break;
                case "svrs"://Сводные ведомости по результатам сессии
                    showingMsg = this.ddlReportType.SelectedValue.ToString();
                    break;
                case "oaz"://Отчеты по академическим задолженностиям
                    showingMsg = this.ddlReportType.SelectedValue.ToString();
                    break;
                case "pv"://Предварительные ведомости
                    showingMsg = this.ddlReportType.SelectedValue.ToString();
                    break;
                case "oes"://Отчеты по экзаменационным сессиям
                    showingMsg = this.ddlReportType.SelectedValue.ToString();
                    break;
                case "oz"://Отчет по заявкам
                    showingMsg = this.ddlReportType.SelectedValue.ToString();
                    break;
                 case "irp"://Информация о ренабельности потоков
                    showingMsg = this.ddlReportType.SelectedValue.ToString();
                    break;
           }
            
            
            
            this.lbl.Text = showingMsg;
        }

        protected Request[] Requests
        {
            get
            {
                return CurrentItem.RequestContainer.Children.OfType<Request>().ToArray();  
                //return CurrentItem.RequestContainer.GetChildren(/*filtered by current user*/).OfType<Request>().ToArray();  
            }
        }


    }

