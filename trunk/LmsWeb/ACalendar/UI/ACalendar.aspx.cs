﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using N2.Web.UI.WebControls;
using System.Web.UI.WebControls;
using N2.ACalendar;
using N2.Resources;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Web.Security;
using N2.Collections;
using N2.Lms.Items;


public partial class ACalendar_UI_ACalendar :    N2.Templates.Web.UI.TemplatePage<N2.ACalendar.ACalendar>,  ICallbackEventHandler

{

    public string sCallBack = string.Empty;
    protected global::System.Web.UI.WebControls.HyperLink linkExcel; 

    protected bool IsEditable {
        get
        {
            return  Roles.IsUserInRole(this.User.Identity.Name, "Administrators");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        sCallBack = Page.ClientScript.GetCallbackEventReference(this,
            "message", "TikeInformation", "context");
        
        //this.eventProp.Visible = false;
        
        //string _current = "";
        //foreach (AEvent _e in AEvents)
        //{
        //    _current += "{\"act\": \"" + _e.Act + "\", \"dateStart\":\"" + _e.DateStart + "\",\"dateEnd\":\"" + _e.DateEnd + "\"},";// _e.Title.Remove(2);
        //}
        //if (_current.Length > 1) _current = _current.Remove(_current.Length - 1);
        //this.event_data_in.Text = _current;
    }

    //protected override void OnPreRender(EventArgs e)
    //{
    //    var ut = (this.SelectUser.FindControl("ut") as UserTree);
    //    if (ut != null)
    //    {
    //        ut.SelectionMode = UserTree.DisplayModeEnum.UsersAndRoles;

    //    }

    //    base.OnPreRender(e);
    //}



    // Обработчик события обратного вызова на серверной стороне
 void ICallbackEventHandler.RaiseCallbackEvent(string e)
 {
   var result  =  e ;
   
     DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(JsonAEvent));
   //string json = @"{""Name"" : ""My Product""}";       
     MemoryStream ms = new MemoryStream(System.Text.Encoding.Unicode.GetBytes(result));

   var _event = ser.ReadObject(ms) as JsonAEvent;

   if (_event.act == "у") del(_event);  
   else  save(_event);


 }

 //Выдача результата
 string ICallbackEventHandler.GetCallbackResult()
 {
     return "";
 }






    protected AEvent[] AEvents
    {
        get
        {
            return (from child in this.CurrentItem.AEvents select child).ToArray();
            //where string.Equals(child.To, Profile.UserName, StringComparison.OrdinalIgnoreCase)

        }
        set
        {
            //this.CurrentItem.Events = value;
        }
    }

    protected string data_in
    {
        get
        {
            string _current = "";
            foreach (AEvent _e in AEvents)
            {
                _current += "{\"act\": \"" + _e.Act + "\", \"dateStart\":\"" + _e.DateStart + "\",\"dateEnd\":\"" + _e.DateEnd + "\"},";// _e.Title.Remove(2);
            }
            if (_current.Length > 1) _current = _current.Remove(_current.Length - 1);
            return _current;
        }


    }

    protected override void OnLoad(EventArgs e)
    {
        Register.JQuery(this.Page);
        Register.StyleSheet(this.Page, "~/Lms/UI/Js/jCal.small.css");
        Register.JavaScript(this.Page, "~/Lms/UI/Js/jQuery.animate.clip.js");
        Register.JavaScript(this.Page, "~/Lms/UI/Js/jQuery.color.js");
        Register.JavaScript(this.Page, "~/Lms/UI/Js/jCal.js");
        Register.JavaScript(this.Page, "~/Lms/UI/Js/json2.js");
        //Register.JavaScript(this.Page, "~/Lms/UI/Js/jQuery.intellisense.js");

        base.OnLoad(e);
    }


 
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        var  cals =  (this.CurrentItem.Parent).Children.ToArray() ;
        string strURL = "~/Reporting/ReportFiles/"; 
         string path = Server.MapPath("../../Reporting/ReportFiles/");
       //strURL += ExcelExport.ExportToFile(cals, path);
        //Response.Redirect( ExcelExport.ExportToFile(cals, path))
        this.linkExcel.NavigateUrl = strURL;
        this.linkExcel.Text = "Академические календари";
    }



    protected void save(JsonAEvent _e)
    {
        //((N2.ACalendar.ACalendar)this.CurrentItem).Text = this.SelectUser.SelectedUser;
        switch (_e.act)
        {
            case "а":
                AEventAK ev = this.Engine.Definitions.CreateInstance<AEventAK>(this.CurrentItem);
                ev.Act = _e.act; 
                ev.DateStart = _e.dateStart;
                ev.DateEnd = _e.dateEnd;
                this.Engine.Persister.Save(ev);
                break;
            case "п":
                AEventPV pv = this.Engine.Definitions.CreateInstance<AEventPV>(this.CurrentItem);
                pv.Act = _e.act;
                pv.DateStart = _e.dateStart;
                pv.DateEnd = _e.dateEnd;
                this.Engine.Persister.Save(pv);
                break;
            case "э":
                AEventES es = this.Engine.Definitions.CreateInstance<AEventES>(this.CurrentItem);
                es.Act = _e.act;
                es.DateStart = _e.dateStart;
                es.DateEnd = _e.dateEnd;
                this.Engine.Persister.Save(es);
                break;
            case "к":
                AEventKO ko = this.Engine.Definitions.CreateInstance<AEventKO>(this.CurrentItem);
                ko.Act = _e.act;
                ko.DateStart = _e.dateStart;
                ko.DateEnd = _e.dateEnd;
                this.Engine.Persister.Save(ko);
                break;
            case "в":
                AEventVS vs = this.Engine.Definitions.CreateInstance<AEventVS>(this.CurrentItem);
                vs.Act = _e.act;
                vs.DateStart = _e.dateStart;
                vs.DateEnd = _e.dateEnd;
                this.Engine.Persister.Save(vs);
                break;
            default:
                Console.WriteLine("Учеба однако:(");
                break;
        }
    }
    protected void del(JsonAEvent _e)
    {
        foreach (AEvent _ce in this.CurrentItem.AEvents)
        {
            if (_ce.DateStart == _e.dateStart)
            {
                // можно написать проверку если конец не совпадает то обрезать только часть
            this.Engine.Persister.Delete(_ce);
            return;
            }

        }
     
    }


    //protected void btnSave_Click(object sender, EventArgs e)
    //{
    //    string _current = ""; 
    //        _current = "[" + _current + "]";
 
        

    //        int _c =  this.CurrentItem.AEvents.Count();
    //        while (this.CurrentItem.AEvents.Count() > 0)
    //        {
    //            AEvent _ce = this.CurrentItem.AEvents.First();
    //            this.Engine.Persister.Delete(_ce);
    //        }
    //    DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(JsonAEvent[]));
    //    MemoryStream ms = new MemoryStream(System.Text.Encoding.Unicode.GetBytes(_current));

    //    var events = ser.ReadObject(ms) as JsonAEvent[];
    //    foreach (JsonAEvent _e in events) if (_e.act != null) save(_e);

    //}
    public class JsonAEvent { public string act; public string dateStart; public string dateEnd;}


}