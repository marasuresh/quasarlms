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

    protected N2.Web.UI.WebControls.UserTree.DisplayModeEnum _displayMode = N2.Web.UI.WebControls.UserTree.DisplayModeEnum.Roles;
    protected N2.Web.UI.WebControls.UserTree.DisplayModeEnum _selectionMode = N2.Web.UI.WebControls.UserTree.DisplayModeEnum.Roles;



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

        protected override void OnPreRender(EventArgs e)
        {
            var ut = (this.SelectUser.FindControl("ut") as N2.Web.UI.WebControls.UserTree);
            if (ut != null)
            {
                ut.SelectionMode = _selectionMode;
                ut.DisplayMode = _displayMode;
            }
            
            base.OnPreRender(e);
        }

        protected void btnGet_Click(object sender, EventArgs e)
        {
       
            //Фильтровка сообщений выводимых на экран.
            string strURL = "~/Reporting/ReportFiles/" ;

            switch (this.ddlReportType.SelectedValue)
            {
                case "erv"://Экзаменационно рейтинговые ведомости
                    strURL += ExcelExport.ExportToFileZV(Roles.GetUsersInRole(this.SelectUser.SelectedUser));
                    this.hlnkReport.NavigateUrl = strURL;
                    this.hlnkReport.Text = "Экзаменационно рейтинговые ведомости группа " + this.SelectUser.SelectedUser;
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
                    var reqs = Requests;
                    strURL += ExcelExport.ExportToFileOZ(reqs, this.SelectUser.SelectedUser);
                    this.hlnkReport.NavigateUrl = strURL;
                    this.hlnkReport.Text = "Отчет по заявкам студента " + this.SelectUser.SelectedUser;
                    break;
                 case "irp"://Информация о ренабельности потоков
                    strURL += ExcelExport.ExportToFileIRP(Requests);
                    this.hlnkReport.NavigateUrl = strURL;
                     this.hlnkReport.Text = "Информация о ренабельности потоков";
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
            //string strURL = "~/Reporting/ReportFiles/";
 
            switch (this.ddlReportType.SelectedValue)
            {
                case "erv"://Экзаменационно рейтинговые ведомости
                    _selectionMode = N2.Web.UI.WebControls.UserTree.DisplayModeEnum.Roles;
                    _displayMode = N2.Web.UI.WebControls.UserTree.DisplayModeEnum.Roles;
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
                    //var reqs = from req in Requests where ( Roles.GetUsersInRole(this.SelectUser.SelectedUser).Contains(req.User)) select req;
                    _selectionMode = N2.Web.UI.WebControls.UserTree.DisplayModeEnum.Users;
                    _displayMode = N2.Web.UI.WebControls.UserTree.DisplayModeEnum.UsersAndRoles;
                    break;
                case "irp"://Информация о ренабельности потоков
                    this.SelectUser.Visible = false;
                    //this.hlnkReport.Text = "Информация о ренабельности потоков";
                    break;
            }


        }


    }

