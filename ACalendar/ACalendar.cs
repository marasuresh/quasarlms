using System.Collections.Generic;
using System.Linq;

using N2.Details;
using N2.Definitions;
using N2.Installation;
using N2.Integrity;
using N2.Edit.Trash;
using N2.Templates.Items;
using N2.Serialization;
using N2.Web.UI;
using System.IO;
using System.Runtime.Serialization;
using System.Xml.Serialization;

using System.Runtime.Serialization.Json;
using System.Text;

namespace N2.ACalendar
{
    [Definition ("ACalendar", "ACalendar", Installer = InstallerHint.NeverRootOrStartPage)]
    [RestrictParents(typeof(ACalendarContainer))]
    //[TabPanel("lms", "LMS", 200)]
    [AllowedChildren(typeof(AEvent))]
    public class ACalendar : AbstractContentPage
    {
        #region Properties

        public override string IconUrl { get { return "~/Templates/UI/Img/calendar.png"; } }
        public override string TemplateUrl { get { return "~/ACalendar/UI/ACalendar.aspx"; } }



        #endregion Properties

        //[EditableChildren("AEvent", "", "AEvent", 1)]
        //public string[] Weeks
        //{
        //    //get { return (string[])this.GetDetail("Weeks"); }
        //    get
        //    {
        //        return  new string[] { "учеба", "учеба", "учеба", "тесты", "учеба", "учеба", "учеба", "тесты", "экзамены", "каникулы" };
        //    }
            
        //    set { this.SetDetail<string[]>("Weeks", value); }
        //}

        public IEnumerable<AEvent> AEvents {
            get { return this.Children.OfType<AEvent>(); }
        }

    }
}



