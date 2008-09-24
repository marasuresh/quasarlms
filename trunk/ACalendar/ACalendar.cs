﻿using System.Collections.Generic;
using System.Linq;

using N2.Details;
using N2.Definitions;
using N2.Installation;
using N2.Integrity;
using N2.Edit.Trash;
using N2.Templates.Items;
using N2.Serialization;
using N2.Web.UI;
using N2.Lms.Items;

namespace N2.ACalendar
{
    [Definition ("ACalendar", "ACalendar", Installer = InstallerHint.NeverRootOrStartPage)]
    [RestrictParents(typeof(ACalendarContainer))]
    //[TabPanel("lms", "LMS", 200)]
    [AllowedChildren(typeof(AEvent))]

    public class ACalendar : AbstractContentPage
    {
        #region Properties

        public override string IconUrl { get { return "~/Template/UI/Img/calendar.png"; } }
        public override string TemplateUrl { get { return "~/ACalendar/UI/ACalendar.aspx"; } }

        public string[] weeks;


        #endregion Properties

        [EditableCheckBox("Is Public", 350)]
        public bool IsPublic
        {
            get { return (bool?)this.GetDetail("Public") ?? false; }
            set { this.SetDetail<bool>("Public", value); }
        }

        [EditableCheckBox("Is Ready", 355)]
        public bool IsReady
        {
            get { return (bool?)this.GetDetail("IsReady") ?? false; }
            set { this.SetDetail<bool>("IsReady", value); }
        }

        [EditableTextBox("Keywords", 360)]
        public string Keywords
        {
            get { return this.GetDetail("Keywords") as string; }
            set { this.SetDetail<string>("Keywords", value); }
        }

        [EditableTextBox("Type", 370)]
        public string Type
        {
            get { return this.GetDetail("Type") as string; }
            set { this.SetDetail<string>("Type", value); }
        }

        [EditableTextBox("Duration, <i>days</i>", 380)]
        public int Duration
        {
            get { return (int?)this.GetDetail("Duration") ?? 0; }
            set { this.SetDetail<int>("Duration", value); }
        }

        [EditableChildren("Weeks", "", "Weeks",1)]
        public string[] Weeks
        {
            //get { return (string[])this.GetDetail("Weeks"); }
            get
            {
                return  new string[] { "учеба", "учеба", "учеба", "тесты", "учеба", "учеба", "учеба", "тесты", "экзамены", "каникулы" };
            }
            
            set { this.SetDetail<string[]>("Weeks", value); }
        }

        public IEnumerable<AEvent> Events {
            get { return this.Children.OfType<AEvent>(); }
        }

    }
}