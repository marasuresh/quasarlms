namespace N2.ACalendar
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using N2.Details;
    using N2.Installation;
    using N2.Integrity;
    using N2.Edit.Trash;
    //using N2.Lms.Items;

    [Definition("AEvent", "AEvent", Installer = InstallerHint.NeverRootOrStartPage)]
    //[RestrictParents(typeof(ACalendar))]
    [NotThrowable]
    [WithEditableTitle("Title", 20)]
    //[WithEditableName("Name (Guid)", 10)]
    //[WithEditableDateRange("Период", 30, "DateStart", "DateEnd")]
    public class AEvent : ContentItem
    {
    //public enum weeks {  a=1, b=2, c=3, d=4, e=5, f=6, g=7 };

        
        #region Properties

        public override string IconUrl { get { return "~/Lms/UI/Img/04/19.png"; } }
        public override string TemplateUrl { get { return "~/Lms/UI/Topic.aspx"; } }
        public override bool IsPage { get { return false; } }


        #endregion Properties

        #region Lms Properties

        //public DateTime DateStart
        //{
        //    get { return (DateTime?)this.GetDetail("DateStart") ?? DateTime.Today; }
        //    set { this.SetDetail<DateTime>("DateStart", value); }
        //}

        // public DateTime DateEnd
        //{
        //    get { return (DateTime?)this.GetDetail("DateEnd") ?? DateTime.Today; }
        //    set { this.SetDetail<DateTime>("DateEnd", value); }
        //}

         //[DisplayableLiteral]
         //public virtual DateTime? Expires
         //{
         //    get { return expires; }
         //    set { expires = value != DateTime.MinValue ? value : null; }
         //}

         //[EditableEnum("week", 100, typeof(weeks))]
         //public weeks week
         //{
         //    get { return (weeks)this.GetDetail("week") ; }
         //    set { this.SetDetail<weeks>("week", value); }
         //}

        [EditableTextBox("WeekStart", 60)]
        public string WeekStart
        {
            get { return this.GetDetail("WeekStart") as string; }
            set { this.SetDetail<string>("WeekStart", value); }
        }
        [EditableTextBox("WeekEnd", 70)]
        public string WeekEnd
        {
            get { return this.GetDetail("WeekEnd") as string; }
            set { this.SetDetail<string>("WeekEnd", value); }
        }


        [EditableTextBox("Description", 110)]
        public string Description
        {
            get { return this.GetDetail("Description") as string; }
            set { this.SetDetail<string>("Description", value); }
        }

        //[EditableTextBox("Duration", 80)]
        //public int Duration
        //{
        //    get { return (int?)this.GetDetail("Duration") ?? 0; }
        //    set { this.SetDetail<int>("Duration", value); }
        //}


          #endregion Lms Properties
    }


    [Definition("полевой выход")]
    [RestrictParents(typeof(ACalendar))]
    public class AEventPV : AEvent
    {
        public override string IconUrl { get { return "~/Lms/UI/Img/04/19.png"; } }
        public override bool IsPage { get { return false; } }
        public override string Title { 
            get { return base.Title ?? "полевой выход"; }
            set { base.Title = value; }
        }


    }

    [Definition("экзаменационная сессия")]
    [RestrictParents(typeof(ACalendar))]

    public class AEventES : AEvent
    {
        public override string IconUrl { get { return "~/Lms/UI/Img/04/19.png"; } }
 
        public override string Title {
            get { return base.Title ?? "экзаменационная сессия"; }
            set { base.Title = value; }
        }
    }

    [Definition("каникулы, отпуск")]
    [RestrictParents(typeof(ACalendar))]

    public class AEventKO : AEvent
    {
        public override string IconUrl { get { return "~/Lms/UI/Img/04/19.png"; } }

        public override string Title
        {
            get { return base.Title ?? "каникулы, отпуск"; }
            set { base.Title = value; }
        }
    }

    [Definition("войсковая стажировка")]
    [RestrictParents(typeof(ACalendar))]

    public class AEventVS : AEvent
    {
        public override string IconUrl { get { return "~/Lms/UI/Img/04/19.png"; } }

        public override string Title
        {
            get { return base.Title ?? "войсковая стажировка"; }
            set { base.Title = value; }
        }
    }

    [Definition("аттестационная комиссия")]
    [RestrictParents(typeof(ACalendar))]

    public class AEventAK : AEvent
    {
        public override string IconUrl { get { return "~/Lms/UI/Img/04/19.png"; } }

        public override string Title
        {
            get { return base.Title ?? "аттестационная комиссия"; }
            set { base.Title = value; }
        }
    }


}
