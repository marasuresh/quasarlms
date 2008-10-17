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
                
                BaseStore store = CurrentItem.MessageStore;

                string msgType = CurrentItem.Action;

                string curUser = Context.User.Identity.Name;
                
                string to = Server.HtmlEncode(txtTo.Text);
                
                string subject = Server.HtmlEncode(txtSubject.Text);
                string text = txtText.Text;

                string[] urlToFile = null;

                //Upload file.
                if (btnFileUpload.HasFile)  // File was sent
                {
                    // Get a reference to PostedFile object
                    HttpPostedFile myFile = btnFileUpload.PostedFile;

                    urlToFile = UploadFile(myFile);
                }
                
                switch (msgType)
                {
                    case "newLetter":
                        CreateLetter(curUser, to, curUser, subject, text, urlToFile, store, CurrentItem);
                        CreateLetter(curUser, to, to, subject, text, urlToFile, store, CurrentItem);
                        break;
                    case "newTask":
                        CreateTask(curUser, to, curUser, subject, text, urlToFile, store, CurrentItem);
                        CreateTask(curUser, to, to, subject, text, urlToFile, store, CurrentItem);
                        break;
                    case "newAnnouncement":
                        CreateAnnouncement(curUser, to, curUser, subject, text, urlToFile, store, CurrentItem);
                        CreateAnnouncement(curUser, to, to, subject, text, urlToFile, store, CurrentItem);
                        break;
                }

                Response.Redirect(Url.Parse(CurrentPage.Url).AppendSegment("inbox").Path);
                    
            }
        }

        


        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(CurrentPage.Url);
        }

        protected void btnToDr_Click(object sender, EventArgs e)
        {
            Page.Validate("CommentInput");
            if (Page.IsValid)
            {

                BaseStore store = CurrentItem.DraughtStore;

                string msgType = CurrentItem.Action;

                string curUser = Context.User.Identity.Name;

                string to = Server.HtmlEncode(txtTo.Text);

                string subject = Server.HtmlEncode(txtSubject.Text);
                string text = txtText.Text;

                string[] urlToFile = null;

                //Upload file.
                if (btnFileUpload.HasFile)  // File was sent
                {
                    // Get a reference to PostedFile object
                    HttpPostedFile myFile = btnFileUpload.PostedFile;

                    urlToFile = UploadFile(myFile);
                }

                switch (msgType)
                {
                    case "newLetter":
                        CreateLetter(curUser, to, curUser, subject, text, urlToFile, store, CurrentItem);
                        break;
                    case "newTask":
                        CreateTask(curUser, to, curUser, subject, text, urlToFile, store, CurrentItem);
                        break;
                    case "newAnnouncement":
                        CreateAnnouncement(curUser, to, curUser, subject, text, urlToFile, store, CurrentItem);
                        break;
                }

                Response.Redirect(Url.Parse(CurrentPage.Url).AppendSegment("inbox").Path);

            }
        }
        
        protected void btnUsers_Click(object sender, EventArgs e)
        {

        }


        private void CreateLetter(string from, string to, string owner, string subject, string text, string[] urlToFile, BaseStore store, MailBox CurItem)
        {
            Letter letter = Engine.Definitions.CreateInstance<Letter>(store);
            letter.Title = "letter";
            letter.From = from;
            letter.To = to;
            letter.Owner = owner;
            letter.Subject = subject;
            letter.Text = text;
            letter.Attachments = urlToFile;
            letter.mailBox = CurItem;
            Engine.Persister.Save(letter);
        }

        private void CreateTask(string from, string to, string owner, string subject, string text, string[] urlToFile, BaseStore store, MailBox CurItem)
        {
            Task task = Engine.Definitions.CreateInstance<Task>(store);
            task.Title = "task";
            task.From = from;
            task.To = to;
            task.Owner = owner;
            task.Subject = subject;
            task.Text = text;
            task.Attachments = urlToFile;
            task.mailBox = CurItem;
            Engine.Persister.Save(task);
        }

        private void CreateAnnouncement(string from, string to, string owner, string subject, string text, string[] urlToFile, BaseStore store, MailBox CurItem)
        {
            Announcement announcement = Engine.Definitions.CreateInstance<Announcement>(store);
            announcement.Title = "announcement";
            announcement.From = from;
            announcement.To = to;
            announcement.Owner = owner;
            announcement.Subject = subject;
            announcement.Text = text;
            announcement.Attachments = urlToFile;
            announcement.mailBox = CurItem;
            Engine.Persister.Save(announcement);
        }

        private string[] UploadFile(HttpPostedFile myFile)
        {
            string[] urlToFile = new string[2];

            //Short name of File.
            string strFileName = Path.GetFileName(myFile.FileName);
            string strUniqueName = Guid.NewGuid() + "$" + strFileName;

            string strRootUpload = Server.MapPath("./Upload/");

            string FilePath = strRootUpload + strUniqueName;

            myFile.SaveAs(FilePath);

            urlToFile[0] = "~/Messaging/UI/Views/Upload/" + strUniqueName;

            return urlToFile;
        }

        
    }
}