using System;
using System.IO;
using System.Web;

namespace N2.Messaging
{
    public sealed class MailFactory
    {
        public void CreateLetter(string from, string to, string owner, string subject, string text, string[] urlToFile, BaseStore store, MailBox CurItem)
        {
            Letter letter = Context.Definitions.CreateInstance<Letter>(store);
            letter.Title = "letter";
            letter.From = from;
            letter.To = to;
            letter.Owner = owner;
            letter.Subject = subject;
            letter.Text = text;
            letter.Attachments = urlToFile;
            letter.mailBox = CurItem;
            letter.Expires = DateTime.Now.AddDays(60);
            Context.Persister.Save(letter);
        }

        public void CreateTask(string from, string to, string owner, string subject, string text, string[] urlToFile, BaseStore store, MailBox CurItem)
        {
            Task task = Context.Definitions.CreateInstance<Task>(store);
            task.Title = "task";
            task.From = from;
            task.To = to;
            task.Owner = owner;
            task.Subject = subject;
            task.Text = text;
            task.Attachments = urlToFile;
            task.mailBox = CurItem;
            task.Expires = DateTime.Now.AddDays(60);
            Context.Persister.Save(task);
        }

        public void CreateAnnouncement(string from, string to, string owner, string subject, string text, string[] urlToFile, BaseStore store, MailBox CurItem)
        {
            Announcement announcement = Context.Definitions.CreateInstance<Announcement>(store);
            announcement.Title = "announcement";
            announcement.From = from;
            announcement.To = to;
            announcement.Owner = owner;
            announcement.Subject = subject;
            announcement.Text = text;
            announcement.Attachments = urlToFile;
            announcement.mailBox = CurItem;
            announcement.Expires = DateTime.Now.AddDays(60);
            Context.Persister.Save(announcement);
        }

        public string[] UploadFile(HttpPostedFile myFile, string UploadPath, string UploadVirtualPath)
        {
            string[] urlToFile = new string[2];

            //Short name of File.
            string strFileName = Path.GetFileName(myFile.FileName);
            string strUniqueName = Guid.NewGuid() + "$" + strFileName;

            string FilePath = UploadPath + strUniqueName;

            myFile.SaveAs(FilePath);

            urlToFile[0] = UploadVirtualPath + strUniqueName;

            return urlToFile;
        }

        public string[] GetRecipients(string userString)
        {
            //Example of userString = "student1; student2 student3,  student4: student5";
            char[] separators = new char[] { ',', ';', ':', ' ' };

            string[] users = userString.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            return users;
        }
    }
}
