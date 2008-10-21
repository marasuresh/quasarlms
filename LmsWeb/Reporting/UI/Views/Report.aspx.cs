using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
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
            if (!IsPostBack)
            {
                //foreach (string s in  Roles.GetAllRoles()) chblRoles.Items.Add(s);
                //this.chblRoles.DataBind();
            }
        }
        protected void btnGet_Click(object sender, EventArgs e)
        {
       
            //Фильтровка сообщений выводимых на экран.
            string strURL = "~/Reporting/ReportFiles/as.xls" ;

            switch (this.ddlReportType.SelectedValue)
            {
                case "erv"://Экзаменационно рейтинговые ведомости
                    strURL = "~/Reporting/ReportFiles/" + ExcelExport.ExportToFileZV(this.users);
                    this.hlnkReport.NavigateUrl = strURL;
                    this.hlnkReport.Text = "Экзаменационно рейтинговые ведомости";
                    break;
                case "svrs"://Сводные ведомости по результатам сессии
                    this.hlnkReport.NavigateUrl = strURL;
                    this.hlnkReport.Text = "не реализовано";
                    break;
                case "oaz"://Отчеты по академическим задолженностиям
                    this.hlnkReport.Text = "не реализовано";
                    break;
                case "pv"://Предварительные ведомости
                    this.hlnkReport.Text = "не реализовано";
                    break;
                case "oes"://Отчеты по экзаменационным сессиям
                    this.hlnkReport.Text = "не реализовано";
                    break;
                case "oz"://Отчет по заявкам
                    this.hlnkReport.Text = "не реализовано";
                    break;
                 case "irp"://Информация о ренабельности потоков
                    this.hlnkReport.Text = "не реализовано";
                    break;
           }
            
            
        }

        protected Request[] Requests
        {
            get
            {
                return CurrentItem.RequestContainer.Children.OfType<Request>().ToArray();  
                //return CurrentItem.RequestContainer.GetChildren(/*filtered by current user*/).OfType<Request>().ToArray();  
            }
        }

        protected Course[] Courses
        {
            get
            {
                return CurrentItem.CourseContainer.Children.OfType<Course>().ToArray();
                //return CurrentItem.RequestContainer.GetChildren(/*filtered by current user*/).OfType<Request>().ToArray();  
            }
        }

        protected MembershipUserCollection users
        {
            get
            {
                return Membership.GetAllUsers();//.Cast<MembershipUser>().Select(_user => _user.UserName);
                //MembershipUser u = new MembershipUser();
                //u.UserName
            }

        }

        protected void ddlReportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.Trace.Write("Lms", this.ddlReportType.SelectedValue);
            
            switch (this.ddlReportType.SelectedValue)
            {
                case "erv"://Экзаменационно рейтинговые ведомости
                    this.hlnkReport.Text = "Экзаменационно рейтинговые ведомости";
                    foreach (string s in Roles.GetAllRoles()) chblRoles.Items.Add(s);
                    this.chblRoles.DataBind();
                    break;
                case "svrs"://Сводные ведомости по результатам сессии
                    //this.hlnkReport.NavigateUrl = strURL;
                    this.hlnkReport.Text = "не реализовано";
                    break;
                case "oaz"://Отчеты по академическим задолженностиям
                    this.hlnkReport.Text = "не реализовано";
                    break;
                case "pv"://Предварительные ведомости
                    this.hlnkReport.Text = "не реализовано";
                    break;
                case "oes"://Отчеты по экзаменационным сессиям
                    this.hlnkReport.Text = "не реализовано";
                    break;
                case "oz"://Отчет по заявкам
                    this.hlnkReport.Text = "не реализовано";
                    break;
                case "irp"://Информация о ренабельности потоков
                    this.hlnkReport.Text = "не реализовано";
                    break;
            }


        }


    }

