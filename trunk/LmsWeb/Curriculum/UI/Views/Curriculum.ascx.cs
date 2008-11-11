using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using N2.Lms.Items;
using N2.Details;

namespace N2.Calendar.Curriculum.UI.Views
{
    public partial class Curriculum : System.Web.UI.UserControl
    {

        public int CourseContainerId { get; set; }

        string _currentCurriculumName = string.Empty;
        public string CurrentCurriculumName
        {
            get 
            {
                return _currentCurriculumName;
            }

            set
            {
                _currentCurriculumName = value;
                //if (this.CurrentCurriculum != null)
                //{
                //    this.rpt.DataSource = CourseInfos;
                //    this.rpt.DataBind();
                //}

            }
        }



        protected IEnumerable<CourseInfo> CourseInfos
        {
            get
            {
                
                    if (_detailCollection== null)  return null;
return
                    from _stringdetail in _detailCollection //  this.CurrentCurriculum.Details 
                    select new CourseInfo
                    {
                        CourseID = Convert.ToInt32( _stringdetail.ToString().Substring(1)),
                        CourseName = (N2.Context.Current.Persister.Get<Course>
                        (Convert.ToInt32(_stringdetail.ToString().Substring(1)))).Title,
                        CourseExclude = Convert.ToInt32(_stringdetail.ToString().Remove(1)),
                        
                        //_stringdetail.Value.ToString().Substring(0, 1)  ,                               //CourseExclude = Convert.ToBoolean(Convert.ToInt32(_stringdetail.Value.ToString().Substring(0, 1))),
                        //CourseObligatory = Convert.ToBoolean(_stringdetail.Value.ToString().Substring(1, 1)),
                        //CourseOptional = Convert.ToBoolean(_stringdetail.Value.ToString().Substring(2, 1)),
                    };
                //return CurrentItem.RequestContainer.GetChildren(/*filtered by current user*/).OfType<Request>().ToArray();  
            }
        }
        protected class CourseInfo 
        {
            public int CourseID { get; set; }
            public string CourseName { get; set; }
            public int CourseExclude { get; set; }
            public bool CourseObligatory { get; set; }
            public bool CourseOptional { get; set; }

        }


        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (this.CurrentCurriculum != null)
                {
                    this.rpt.DataSource = CourseInfos;
                    this.rpt.DataBind();
                }
            }

        }
        protected CourseContainer CourseContainer
        {
            get
            {
                return string.IsNullOrEmpty(this.CourseContainerId.ToString()) ?
                     null : N2.Context.Current.Persister.Get<CourseContainer>(this.CourseContainerId);
            }
        }
        public N2.Details.DetailCollection CurrentCurriculum
        {
            get
            {
                return string.IsNullOrEmpty(_currentCurriculumName) ?
                      null : this.CourseContainer.GetDetailCollection(_currentCurriculumName, false);
                //add 
            }
        }

        protected IEnumerable<String> _detailCollection;
        public IEnumerable<String> DetailCollection
            {
                get {
                    if (_detailCollection == null)
                    {
                        ArrayList _dc = new ArrayList();
                        foreach (RepeaterItem ri in this.rpt.Items)
                        {
                            _dc.Add(((RadioButtonList)ri.FindControl("rbl")).SelectedValue + ((HiddenField)ri.FindControl("hf")).Value);
                        }
                        _detailCollection = _dc.Cast<string>();
                    }
 
                    return _detailCollection; 
                
                
                }
                set { _detailCollection = value;
                if (_detailCollection.Any())
                {
                    this.rpt.DataSource = CourseInfos;
                    this.rpt.DataBind();
                }
                }
            }

        protected void rpt_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            ((Label)e.Item.FindControl("l10")).Text = ((HiddenField)e.Item.FindControl("hf")).Value;
        }

        public event EventHandler Changed;

        protected virtual void OnChanged(EventArgs e)
        {
            if (this.Changed != null) this.Changed(this, e);
        
        }

        protected void rbl_SelectedIndexChanged(object sender, EventArgs e)
        {
            //((Label)e.Item.FindControl("l10")).Text = ((HiddenField)e.Item.FindControl("hf")).Value;
            string currCurseID = ((HiddenField)((RadioButtonList)sender).NamingContainer.FindControl("hf")).Value;
            string currValue = ((RadioButtonList)sender).SelectedValue;
            //this.Label.Text = "courseID: " + currCurseID + "  value=" + ((RadioButtonList)sender).SelectedValue;
         
                string[] _dc = this.DetailCollection.ToArray();

            OnChanged(EventArgs.Empty);
           
            for (int i=0; i < _dc.Length; i++)
            {
                if (string.Equals(_dc[i].Substring(1), currCurseID))
                {
                    _dc[i] = currValue + currCurseID;
                    _detailCollection = (IEnumerable<String>)_dc;
                    return;

                }
            }



        }




    }
}