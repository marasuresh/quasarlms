using System;
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
                MessageStore store = CurrentItem.MessageStore;

                string msgType = CurrentItem.Action;

                string user = Context.User.Identity.Name;
                
                switch (msgType)
                {
                    case "newLetter":
                        Letter letter = Engine.Definitions.CreateInstance<Letter>(store);
                        letter.Title = Server.HtmlEncode("letter");
                        letter.From = user;
                        letter.To = Server.HtmlEncode(txtTo.Text);
                        letter.Subject = Server.HtmlEncode(txtSubject.Text);
                        letter.Text = txtText.Text;
                        //Utility.Insert(letter, store, "Published DESC");
                        Engine.Persister.Save(letter);
                        break;
                    case "newTask":
                        Task task = Engine.Definitions.CreateInstance<Task>(store);
                        task.Title = Server.HtmlEncode("task");
                        task.From = user;
                        task.To = Server.HtmlEncode(txtTo.Text);
                        task.Subject = Server.HtmlEncode(txtSubject.Text);
                        task.Text = txtText.Text;
                        //Utility.Insert(task, store, "Published DESC");
                        Engine.Persister.Save(task);
                        break;
                    case "newAnnouncement":
                        Announcement announcement = Engine.Definitions.CreateInstance<Announcement>(store);
                        announcement.Title = Server.HtmlEncode("announcement");
                        announcement.From = user;
                        announcement.To = Server.HtmlEncode(txtTo.Text);
                        announcement.Subject = Server.HtmlEncode(txtSubject.Text);
                        announcement.Text = txtText.Text;
                        //Utility.Insert(announcement, store, "Published DESC");
                        Engine.Persister.Save(announcement);
                        break;
                }

                Response.Redirect(Url.Parse(CurrentPage.Url).AppendSegment("inbox").Path);
                    
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(CurrentPage.Url);
        }
    }
}