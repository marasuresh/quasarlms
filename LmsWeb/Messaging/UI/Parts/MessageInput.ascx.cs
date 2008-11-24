using System;
using System.Text.RegularExpressions;
using System.Web;
using N2.Templates.Web.UI;
using N2.Web;
using N2.Web.UI.WebControls;
using N2.Resources;

namespace N2.Messaging.Messaging.UI.Parts
{
    public partial class MessageInput : TemplateUserControl<MailBox, MailBox>
    {

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			this.hlCancel.PostBackUrl = this.CurrentPage.Url;
			Message _editedItem = this.CurrentItem.GetEditedItem();

			this.txtSubject.Text = _editedItem.Subject;
			this.txtText.Text = _editedItem.Text;
			this.selUser.SelectedUser = _editedItem.To;
			
		}
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            Register.JQuery(this.Page);
        }
        
        protected override void OnPreRender(EventArgs e)
        {
            var ut = (this.selUser.FindControl("ut") as N2.Web.UI.WebControls.UserTree);
            if (ut != null)
            {
                ut.AllowMultipleSelection = true;
                ut.SelectionMode = UserTree.DisplayModeEnum.Users;
                ut.DisplayMode = UserTree.DisplayModeEnum.Users;
            }

            base.OnPreRender(e);
        }

		void UpdateEditedMessage()
		{
			string[] attacments = new string[0];

			//Upload file.
			if (btnFileUpload.HasFile)  // File was sent
                {
				// Get a reference to PostedFile object
				HttpPostedFile myFile = btnFileUpload.PostedFile;

				attacments = new MailFactory().UploadFile(myFile, Server.MapPath("./Upload/"), "~/Messaging/UI/Views/Upload/");
			}

			Message _editedItem = this.CurrentPage.GetEditedItem();

			_editedItem.ID = 0;
			_editedItem.Text = this.txtText.Text;
			_editedItem.Subject = this.txtSubject.Text;
            _editedItem.To = Regex.Replace(this.selUser.SelectedUser, ";", "; ");
			_editedItem.From = this.Context.User.Identity.Name;
            _editedItem.Owner = this.Context.User.Identity.Name;
            _editedItem.IsRead = true;
			_editedItem.Attachments = attacments;
			_editedItem.Expires = DateTime.Now.AddDays(60);
			_editedItem.MessageType = (MessageTypeEnum)Enum.Parse(typeof(MessageTypeEnum), this.rblMessageType.SelectedValue);

			_editedItem.Save();
		}

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Page.Validate("CommentInput");
            if (Page.IsValid)
            {
				this.UpdateEditedMessage();

				Message _editedItem = this.CurrentItem.GetEditedItem();

                //Создание копий получалелей.
                Array.ForEach(
                    new MailFactory().GetRecipients(_editedItem.To),
                    recipient =>
                    {
                        var _copy = (N2.Messaging.Message)_editedItem.Clone(false);
                        _copy.To = recipient;
                        _copy.Owner = recipient;
                        _copy.IsRead = false;
                        _copy.Save();
                    });

                Response.Redirect(Url.Parse(CurrentPage.Url).AppendSegment("folder/" + MailBox.C.Folders.Inbox).Path);
                    
            }
        }

        protected void btnToDr_Click(object sender, EventArgs e)
        {
            Page.Validate("CommentInput");
            if (Page.IsValid)
            {
				this.UpdateEditedMessage();
				Message _editedItem = this.CurrentItem.GetEditedItem();
				N2.Context.Persister.Move(_editedItem, this.CurrentPage.MessageStore.DraftsFolder);
                Response.Redirect(Url.Parse(CurrentPage.Url).AppendSegment("folder/" + MailBox.C.Folders.Inbox).Path);
            }
        }
    }
}