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

			this.hlCancel.NavigateUrl = this.CurrentPage.Url;

			this.txtSubject.Text = this.CurrentItem.EditedItem.Subject;
			this.txtText.Text = this.CurrentItem.EditedItem.Text;
			this.selUser.SelectedUser = this.CurrentItem.EditedItem.To;
			
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

			this.CurrentPage.EditedItem.ID = 0;
			this.CurrentPage.EditedItem.Text = this.txtText.Text;
			this.CurrentPage.EditedItem.Subject = this.txtSubject.Text;
            this.CurrentPage.EditedItem.To = Regex.Replace(this.selUser.SelectedUser, ";", "; ");
			this.CurrentPage.EditedItem.From = this.Context.User.Identity.Name;
            this.CurrentPage.EditedItem.Owner = this.Context.User.Identity.Name;
            this.CurrentPage.EditedItem.IsRead = true;
			this.CurrentPage.EditedItem.Attachments = attacments;
			this.CurrentPage.EditedItem.Expires = DateTime.Now.AddDays(60);
			this.CurrentPage.EditedItem.MessageType = (MessageTypeEnum)Enum.Parse(typeof(MessageTypeEnum), this.rblMessageType.SelectedValue);

			this.CurrentPage.EditedItem.Save();
		}

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Page.Validate("CommentInput");
            if (Page.IsValid)
            {
				this.UpdateEditedMessage();

                //Создание копий получалелей.
                Array.ForEach(
                    new MailFactory().GetRecipients(this.CurrentPage.EditedItem.To),
                    recipient =>
                    {
                        var _copy = (N2.Messaging.Message)this.CurrentPage.EditedItem.Clone(false);
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
				N2.Context.Persister.Move(this.CurrentPage.EditedItem, this.CurrentPage.MessageStore.DraftsFolder);
                Response.Redirect(Url.Parse(CurrentPage.Url).AppendSegment("folder/" + MailBox.C.Folders.Inbox).Path);
            }
        }
    }
}