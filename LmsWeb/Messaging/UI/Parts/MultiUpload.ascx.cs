using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace N2.Messaging.Messaging.UI.Parts
{
    public partial class MultiUpload : System.Web.UI.UserControl
    {
        public enum typeOfAttachment
        {
            nowUpload,
            alreadyAttached
        }

        #region Properies

        static private readonly ArrayList _fuControls = new ArrayList();
        
        public List<HttpPostedFile> postedFiles 
        { 
            get 
            {
                var list = new List<HttpPostedFile>();
                foreach (FileUpload fu in _fuControls)
                {
                    list.Add(fu.PostedFile);
                }
                return list;
            }
        }

        static private ArrayList _attachedFiles = new ArrayList();
        public ArrayList attachedFiles
        {
            set
            {
                _attachedFiles = value;
            }
            get
            {
                return _attachedFiles;
            }
        }

        private List<linkToFile> attachmentDataSource
        {
            get
            {
                var ltf = new List<linkToFile>();
                
                for (int i = 0; i < attachedFiles.Count; i++)
                {
                    int index = ((string)attachedFiles[i]).IndexOf("$") + 1;
                    var fileName =  ((string)attachedFiles[i]).Remove(0, index);
                    
                    var fLink = new linkToFile {indexInParentList = i,
                                                fileName = fileName, 
                                                type = typeOfAttachment.alreadyAttached};
                    ltf.Add(fLink);
                }

                for (int i = 0; i < postedFiles.Count; i++ )
                {
                    var fLink = new linkToFile
                    {
                        indexInParentList = i,
                        fileName = postedFiles[i].FileName,
                        type = typeOfAttachment.nowUpload
                    };
                    ltf.Add(fLink);
                }

                return ltf;
            }
        }

        public bool HasFiles
        {
            get
            {
                return postedFiles.Count > 0;
            }
        }

        #endregion

        #region Methods

        protected override void OnInit(EventArgs e)
        {
            if (!IsPostBack)
            {
                ClearAttachments();
            }

            RepeaterDataBind();
            base.OnInit(e);
        }

        protected void btnAddFile_Click(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
            {
                if (FileUpload1.HasFile)
                {
                    _fuControls.Add(FileUpload1);
                    RepeaterDataBind();    
                }
            }
        }

        protected void btnDeleteFile_Click(object sender, EventArgs e)
        {
            int index = ((RepeaterItem) (((LinkButton) sender).Parent)).ItemIndex;
            linkToFile ltf = attachmentDataSource[index];

            switch (ltf.type)
            {
                case (typeOfAttachment.alreadyAttached):
                    _attachedFiles.RemoveAt(ltf.indexInParentList);
                    break;
                case (typeOfAttachment.nowUpload):
                    _fuControls.RemoveAt(ltf.indexInParentList);
                    break;
            }
            RepeaterDataBind();
        }

        private void RepeaterDataBind()
        {
            UploadedFiles.DataSource = attachmentDataSource;
            UploadedFiles.DataBind();
        }

        public override void DataBind()
        {
            RepeaterDataBind();
            base.DataBind();
        }

        public void ClearAttachments()
        {
            _fuControls.Clear();
            _attachedFiles.Clear();
        }

        #endregion

        public class linkToFile
        {
            public int indexInParentList { set; get; }
            
            public string fileName { set; get; }

            public typeOfAttachment type{ set; get;}

        }
    }
}