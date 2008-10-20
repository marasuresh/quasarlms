using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using N2.Templates.Web.UI;
using N2.Web;

namespace N2.Messaging.Messaging.UI.Parts
{
    public partial class Message : TemplateUserControl<N2.Messaging.Message, N2.Messaging.Message>
    {
        private string msgContainer; 

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            #region Инициализация обработчиков

            btnToRecBin.Click += btnToRecBin_Click;
            btnRestore.Click += btnRestore_Click;

            btnUsers.Click += btnUsers_Click;
            
            btnExit.Click += btnExit_Click;
            btnExit2.Click += btnExit_Click;
            btnExit3.Click += btnExit_Click;

            #endregion 

            
            string curUser = Context.User.Identity.Name;

            //Проверяем в каком контейнере хранится письмо.
            if (CurrentItem.Parent is DraughtStore)
                msgContainer = "DraughtStore";
            else if (CurrentItem.Parent is RecycleBin)
                msgContainer = "RecycleBin";
            else
                msgContainer = "MessageStore";


            #region Отрисовка Контента письма. 

            if (msgContainer == "MessageStore" || msgContainer == "RecycleBin")
            {
                #region для обычного письма и письма из корзины

                mvMsgContent.ActiveViewIndex = 0;

                tbFrom.Text = CurrentItem.From;
                tbSubject.Text = CurrentItem.Subject;
                txtText.Text = CurrentItem.Text;
                tbDate.Text = CurrentItem.Created.ToString();

                //Если есть вложения отображаем их на экране.
                if (CurrentItem.Attachments != null)
                {
                    string attachFile = CurrentItem.Attachments[0];
                    int index = attachFile.IndexOf("$") + 1;
                    string FileName = attachFile.Remove(0, index);

                    hlAttach.NavigateUrl = attachFile;
                    hlAttach.Text = FileName;
                }
                else
                {
                    imgAttach.Visible = false;
                    hlAttach.Visible = false;
                }
                
                #endregion
            }
            else
            {
                #region для письма из черновиков

                mvMsgContent.ActiveViewIndex = 1;

                txtTo.Text = CurrentItem.To;
                txtSubject.Text = CurrentItem.Subject;
                ftaEdit.Text = CurrentItem.Text;

                //Если есть вложения отображаем их на экране.
                if (CurrentItem.Attachments != null)
                {
                    string attachFile = CurrentItem.Attachments[0];
                    int index = attachFile.IndexOf("$") + 1;
                    string FileName = attachFile.Remove(0, index);

                    hlAttachEdit.NavigateUrl = attachFile;
                    hlAttachEdit.Text = FileName;
                }
                else
                {
                    imgAttachEdit.Visible = false;
                    hlAttachEdit.Visible = false;
                }

                #endregion
            }
            

            #endregion


            #region Отрисовка Панели управления.
            
            switch (msgContainer)
            {
                case "MessageStore":
                    
                    #region для обычного письма

                    mvActionPanel.ActiveViewIndex = 0;

                    // Если сообщение просматривает пользователь, которому адресовано письмо... 
                    if (CurrentItem.To == curUser)
                    {
                        //Отмечаем сообщение как прочтенное.
                        CurrentItem.isRead = true;
                        CurrentItem.Save();
                    }

                    #endregion
                    
                    break;
                case "DraughtStore":
                    
                    #region для письма из черновиков

                    mvActionPanel.ActiveViewIndex = 1;

                    #endregion
                    
                    break;
                case "RecycleBin":

                    #region для письма из корзины

                    mvActionPanel.ActiveViewIndex = 2;

                    #endregion

                    break;

            }

            #endregion
        }

        
        
        void btnRestore_Click(object sender, EventArgs e)
        {
            Engine.Persister.Move(CurrentItem, CurrentItem.mailBox.MessageStore);
            Response.Redirect(Url.Parse(CurrentItem.mailBox.Url).AppendSegment("MessageStore").Path);
        }

        void btnUsers_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        void btnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect(Url.Parse(CurrentItem.mailBox.Url).AppendSegment(msgContainer).Path);
        }

        void btnToRecBin_Click(object sender, EventArgs e)
        {
            Engine.Persister.Move(CurrentItem, CurrentItem.mailBox.RecycleBin);
            Response.Redirect(Url.Parse(CurrentItem.mailBox.Url).AppendSegment("MessageStore").Path);
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            Page.Validate("CommentInput");
            if (Page.IsValid)
            {
                //Сохраняем изменения.
                CurrentItem.To = txtTo.Text;
                CurrentItem.Subject = txtSubject.Text;
                CurrentItem.Text = ftaEdit.Text;
                CurrentItem.Expires = DateTime.Now.AddDays(60);

                MailFactory mFactory = new MailFactory();
                
                //Upload file.
                string[] attacments = null;
                if (btnFileUpload.HasFile)  // File was sent
                {
                    // Get a reference to PostedFile object
                    HttpPostedFile myFile = btnFileUpload.PostedFile;

                    attacments = mFactory.UploadFile(myFile, Server.MapPath("./Upload/"), "~/Messaging/UI/Views/Upload/");
                }
                else
                    attacments = CurrentItem.Attachments;
                
                CurrentItem.Attachments = attacments;

                Engine.Persister.Save(CurrentItem);

                //Возвращаем письмо в отправленные.
                Engine.Persister.Move(CurrentItem, CurrentItem.mailBox.MessageStore);

                //Создаем дубликаты для получателей.
                BaseStore store = CurrentItem.mailBox.MessageStore;

                string msgType = CurrentItem.TypeOfMessage;

                string curUser = Context.User.Identity.Name;

                string to = txtTo.Text;
                string subject = txtSubject.Text;
                string text = ftaEdit.Text;


                //Создание копий получалелей.
                string[] recipients = mFactory.GetRecipients(to);
                foreach (string recipient in recipients)
                {
                    switch (msgType)
                    {
                        case "newLetter":
                            mFactory.CreateLetter(curUser, recipient, recipient, subject, text, attacments, store, CurrentItem.mailBox);
                            break;
                        case "newTask":
                            mFactory.CreateTask(curUser, recipient, recipient, subject, text, attacments, store, CurrentItem.mailBox);
                            break;
                        case "newAnnouncement":
                            mFactory.CreateAnnouncement(curUser, recipient, recipient, subject, text, attacments, store, CurrentItem.mailBox);
                            break;
                    }
                }

                Response.Redirect(Url.Parse(CurrentItem.mailBox.Url).AppendSegment("inbox").Path);
            }
        }

    }
}