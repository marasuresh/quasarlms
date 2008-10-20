using System;
using System.IO;
using System.Web;
using N2.Edit;
using N2.Templates;
using N2.Messaging;
using N2.Templates.Web.UI;
using N2.Web;

namespace N2.Messaging.Messaging.UI.Parts
{
    public partial class MessageInput : TemplateUserControl<N2.Messaging.MailBox, N2.Messaging.MailBox>
    {
        
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Page.Validate("CommentInput");
            if (Page.IsValid)
            {

                MailFactory mFactory = new MailFactory();

                BaseStore store = CurrentItem.MessageStore;

                string msgType = CurrentItem.Action;

                string curUser = Context.User.Identity.Name;
                
                string to = txtTo.Text;
               
                string subject = txtSubject.Text;
                string text = txtText.Text;

                string[] attacments = null;

                //Upload file.
                if (btnFileUpload.HasFile)  // File was sent
                {
                    // Get a reference to PostedFile object
                    HttpPostedFile myFile = btnFileUpload.PostedFile;

                    attacments = mFactory.UploadFile(myFile, Server.MapPath("./Upload/"), "~/Messaging/UI/Views/Upload/");
                }
                
                //Создание копии отправителя.
                switch (msgType)
                {
                    case "newLetter":
                        mFactory.CreateLetter(curUser, to, curUser, subject, text, attacments, store, CurrentItem);
                        break;
                    case "newTask":
                        mFactory.CreateTask(curUser, to, curUser, subject, text, attacments, store, CurrentItem);
                        break;
                    case "newAnnouncement":
                        mFactory.CreateAnnouncement(curUser, to, curUser, subject, text, attacments, store, CurrentItem);
                        break;
                }
                //Создание копий получалелей.
                string[] recipients = mFactory.GetRecipients(to);
                foreach (string recipient in recipients)
                {
                    switch (msgType)
                    {
                        case "newLetter":
                            mFactory.CreateLetter(curUser, recipient, recipient, subject, text, attacments, store, CurrentItem);
                            break;
                        case "newTask":
                            mFactory.CreateTask(curUser, recipient, recipient, subject, text, attacments, store, CurrentItem);
                            break;
                        case "newAnnouncement":
                            mFactory.CreateAnnouncement(curUser, recipient, recipient, subject, text, attacments, store, CurrentItem);
                            break;
                    }    
                }

                Response.Redirect(Url.Parse(CurrentPage.Url).AppendSegment("inbox").Path);
                    
            }
        }

        protected void btnToDr_Click(object sender, EventArgs e)
        {
            Page.Validate("CommentInput");
            if (Page.IsValid)
            {
                MailFactory mFactory = new MailFactory();

                BaseStore store = CurrentItem.DraughtStore;

                string msgType = CurrentItem.Action;

                string curUser = Context.User.Identity.Name;

                string to = txtTo.Text;

                string subject = txtSubject.Text;
                string text = txtText.Text;

                string[] attacments = null;

                //Upload file.
                if (btnFileUpload.HasFile)  // File was sent
                {
                    // Get a reference to PostedFile object
                    HttpPostedFile myFile = btnFileUpload.PostedFile;

                    attacments = mFactory.UploadFile(myFile, Server.MapPath("./Upload/"), "~/Messaging/UI/Views/Upload/");
                }

                switch (msgType)
                {
                    case "newLetter":
                        mFactory.CreateLetter(curUser, to, curUser, subject, text, attacments, store, CurrentItem);
                        break;
                    case "newTask":
                        mFactory.CreateTask(curUser, to, curUser, subject, text, attacments, store, CurrentItem);
                        break;
                    case "newAnnouncement":
                        mFactory.CreateAnnouncement(curUser, to, curUser, subject, text, attacments, store, CurrentItem);
                        break;
                }

                Response.Redirect(Url.Parse(CurrentPage.Url).AppendSegment("inbox").Path);

            }
        }
        
        protected void btnUsers_Click(object sender, EventArgs e)
        {

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(CurrentPage.Url);
        }
        
    }
}