namespace N2.ACalendar
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using N2.Details;
    using N2.Installation;
    using N2.Integrity;
    using N2.Edit.Trash;
    using N2.Lms.Items;

    [Definition("AEvent", "AEvent", Installer = InstallerHint.NeverRootOrStartPage)]
    [RestrictParents(typeof(ACalendar))]
    [NotThrowable]
    [WithEditableName("Name (Guid)", 10)]
    [WithEditableTitle("Title", 20)]
    [WithEditableDateRange("Период", 30, "DateStart", "DateEnd", Required = true)]
    public class AEvent : ContentItem, IContinuous
    {
        #region Properties

        public override string IconUrl { get { return "~/Lms/UI/Img/04/19.png"; } }
        public override string TemplateUrl { get { return "~/Lms/UI/Topic.aspx"; } }
        public override bool IsPage { get { return false; } }


        #endregion Properties

        #region Lms Properties

        public DateTime DateStart
        {
            get { return (DateTime)this.GetDetail("DateStart"); }
            set { this.SetDetail<DateTime>("DateStart", value); }
        }

         public DateTime DateEnd
        {
            get { return this.GetDetail("DateEnd") as DateTime; }
            set { this.SetDetail<DateTime>("DateEnd", value); }
        }

        //[EditableTextBox("Type", 100)]
        //public string Type
        //{
        //    get { return this.GetDetail("Type") as string; }
        //    set { this.SetDetail<string>("Type", value); }
        //}

        [EditableTextBox("Description", 110)]
        public string Description
        {
            get { return this.GetDetail("Description") as string; }
            set { this.SetDetail<string>("Description", value); }
        }

        [EditableTextBox("Duration", 80)]
        public int Duration
        {
            get { return (int?)this.GetDetail("Duration") ?? 0; }
            set { this.SetDetail<int>("Duration", value); }
        }


          #endregion Lms Properties
    }
}
