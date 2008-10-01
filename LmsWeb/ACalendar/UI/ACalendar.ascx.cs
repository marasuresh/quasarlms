using System;
using System.Collections.Generic;
using System.Linq;
using N2.Lms.Items;
using N2.Details;
using N2.Resources;
using N2.Web.UI;
using N2.ACalendar;
using items = N2.ACalendar;
	/// <summary>
	/// Отображение информации о календаре
	/// </summary>

public partial class ACalendar :  ContentUserControl<items.ACalendar>
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    ACalendar GetACalendar()
    {
        return this as ACalendar ;
    }
}

        //protected override void OnInit(EventArgs e)
        //{
        //    if (null != this.CurrentItem) {
        //        this.Session["courseName"] = this.CurrentItem.Title;

        //        this.CurrentItem["MetaKeywrods"] = this.CurrentItem.Keywords;
        //        this.CurrentItem["MetaDescription"] = this.CurrentItem.Text;
        //        /*
        //        var _metaApplier = new N2.Templates.SEO.TitleAndMetaTagApplyer(
        //            this.Page, this.CurrentItem);*/
        //    }

        //    base.OnInit(e);
        //}

