﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using N2.Lms.Items;
using N2.Details;



namespace N2.Calendar.Curriculum.UI.Views
{
    public partial class CurriculumList : N2.Templates.Web.UI.TemplatePage<N2.ACalendar.Curriculum>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //this ViewState["ParentItemId"] = ParentItem.ID;
            if (!this.IsPostBack)
            {
                this.CurrentCurriculum.CourseContainerId = this.CurrentItem.CourseContainer.ID;
                this.CurrentCurriculum.CurrentCurriculumName =
                    (this.ParentItem.DetailCollections.Select(_c => _c.Key)).First();

                var _dummy = this.CurrCurriculum;

               this.ddltup.DataSource = CollectionNames;
                this.ddltup.DataBind();

                this.rpt.DataSource = this.CurrCurriculum;
                this.rpt.DataBind();
            }
        }

        CourseContainer m_container;  //локальная коллекция курсов

        public int ParentItemId
        {
            get { return (int?)this.ViewState["ParentItemId"] ?? 0; }
            set { this.ViewState["ParentItemId"] = value; }
        }

        protected CourseContainer ParentItem
        {
            get
            {
                return m_container ?? (m_container =  LoadParentItem()) ;
            }
            set {
                this.m_container = value;
                
                if (this.m_container != null)
                {
                    this.ParentItemId = this.m_container.ID;
                }
                
            }
        }

        CourseContainer LoadParentItem()
        {
            var _container = N2.Context.Current.Persister.Get<CourseContainer>(this.ParentItemId)

                ?? new CourseContainer
            {
                Children = new List<N2.ContentItem>(new[] {
                            new Course { Name = "Course 1", Title = "Course 1", },
                            new Course { Name = "Course 2", Title = "Course 2", },
                             })
            };

            var _tup = _container.GetDetailCollection("", true);

            _tup.Clear();

            _tup.AddRange(
                from _item in this.rpt.Items.Cast<RepeaterItem>()
                select new N2.Details.IntegerDetail(
                    this.ParentItem,
                    ((HiddenField)_item.FindControl("hf")).Value,
                    int.Parse(((RadioButtonList)_item.FindControl("rbl")).SelectedValue)));

            return _container;
        }

        protected IEnumerable<string> CollectionNames //набор коллекций в текущих курсах
        {
            get { return this.CurrentItem.CourseContainer.DetailCollections.Select(_c => _c.Key); }
        }


        protected N2.Details.DetailCollection CurrCurriculum
        {
            get
            {
                return
                    this.ParentItem.GetDetailCollection(this.CurrentTup, true);
            }
        }


        string m_currentTup;
        protected string CurrentTup
        {  //   имя коллекции
            get
            {
                var _currentCollectionName = this.CollectionNames.First();
                // from dd read
                return
                    (this.m_currentTup = string.IsNullOrEmpty(this.m_currentTup) ?
                     "Default"
                    : _currentCollectionName);
            }
            set { this.m_currentTup = value; }
        }

        protected class CourseInfo : Course
        {
            public int Info { get; set; }
        }

        protected IEnumerable<CourseInfo> CourseInfos
        {
            get
            {
                return
                    from _course in ParentItem.Children.OfType<Course>()
                    join _tupInfo in this.CurrCurriculum.Details
                        on _course.Name equals _tupInfo.Name
                        into _tupInfos
                    from _ti in _tupInfos.DefaultIfEmpty()
                    select new CourseInfo
                    {
                        Name = _course.Name,
                        Title = _course.Title,
                        ID = _course.ID,
                        Info = _ti != null ? ((IntegerDetail)_ti).IntValue : 0,
                    };
                //return CurrentItem.RequestContainer.GetChildren(/*filtered by current user*/).OfType<Request>().ToArray();  
            }
        }

        protected void btnAddTUP_Click(object sender, EventArgs e)
        {

            var _container = this.CurrentItem.CourseContainer;
            string tupToBeAdded = this.txtAddTUP.Text;
            _container.GetDetailCollection(tupToBeAdded, true).AddRange(
                from _course in _container.GetChildren<Course>()
                select new StringDetail(_container, _course.ID.ToString(), "0" + _course.ID.ToString())
            );


            
            //List<StringDetail> a = new List<StringDetail>();
            //foreach (CourseInfo _ci in CourseInfos)
            //{
            //    var _detail = new N2.Details.IntegerDetail(ParentItem, _ci.Title, 1);
            //    a.Add( _detail );
            //}
 
            //foreach (Course _c in _container.GetChildren<Course>())
            //{
            //    var _detail = new N2.Details.StringDetail( _container, _c.ID.ToString(), "100");
            //    a.Add(_detail);
            //}


            //var _details = new N2.Details.DetailCollection(ParentItem, this.txtAddTUP.Text, a);
            //this.CurrentItem.CourseContainer.DetailCollections.Add(this.txtAddTUP.Text, _details);
            N2.Context.Current.Persister.Save(this.CurrentItem.CourseContainer);


            this.CurrentTup = tupToBeAdded;
            this.ddltup.DataSource = CollectionNames;
            this.ddltup.DataBind();
 
            foreach (ListItem li in this.ddltup.Items) 
            {
                if (li.Value == tupToBeAdded) li.Selected = true;
            }
            
        }

        protected void btnDelTUP_Click(object sender, EventArgs e)
        {
            string tupToBeDeleted = this.ddltup.SelectedValue;
            this.CurrentItem.CourseContainer.DetailCollections.Remove(tupToBeDeleted);
            N2.Context.Current.Persister.Save(this.CurrentItem.CourseContainer);
            this.ddltup.DataSource = CollectionNames;
            this.ddltup.DataBind();

        }

        protected void ddltup_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.debugl.Text = ((DropDownList)sender).SelectedValue;
            this.CurrentCurriculum.CourseContainerId = this.CurrentItem.CourseContainer.ID;
            DetailCollection dc=  
                this.CurrentItem.CourseContainer.GetDetailCollection(((DropDownList)sender).SelectedValue, false);
            this.CurrentCurriculum.DetailCollection = dc.Cast<String>(); 
            this.CurrentCurriculum.CurrentCurriculumName = ((DropDownList)sender).SelectedValue;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            DetailCollection dc=  
                this.CurrentItem.CourseContainer.GetDetailCollection(this.ddltup.SelectedValue, false);
            IEnumerable<string> dcToSave = this.CurrentCurriculum.DetailCollection;
            if (dcToSave == null) return;
            dc.Clear();
            dc.AddRange(
                from str in dcToSave 
                select new StringDetail(this.CurrentItem.CourseContainer, this.ddltup.SelectedValue, str));
            N2.Context.Current.Persister.Save(this.CurrentItem.CourseContainer);
        }

    }
}