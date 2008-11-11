using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using N2.Lms.Items.Lms.RequestStates;
using N2.Templates.Web.UI;
using N2.Workflow;
using N2.Lms.Items;
using Subgurim.Chat;
using Subgurim.Chat.Server;

namespace N2.Templates.Chat.UI.Parts
{
    public partial class ChatBox : TemplateUserControl<ContentItem, Items.ChatBox>
    {
       protected override void OnInit(EventArgs e)
        {
            var trChannels = new List<TrainingChannel>();
           
            //Публичный канал доступен для всех пользователей системы.
            var channelName = "PublicChannel";
            var result = ChatServer.channel_Add(channelName, "1");
            var trChannel = new TrainingChannel() {channelID = channelName, channelName = channelName};
            trChannels.Add(trChannel);

            //Создаем каналы согласно списка активных тренингов.
            foreach (N2.Lms.Items.Request req in CurrentItem.ApprovedRequests.ToList())
            {
                string newChannelID = ((ApprovedState)req.GetCurrentState()).Training.ID.ToString();
                string newChannelName = ((ApprovedState) req.GetCurrentState()).Training.Title;
                var errm = ChatServer.channel_Add(newChannelID, "1");
                
                var tc = new TrainingChannel() { channelID = newChannelID, channelName = newChannelName };

                if (!trChannels.Contains(tc, new ChannelComparer())) trChannels.Add(tc);
            }

            lbDefChannel.DataSource = trChannels;
            lbDefChannel.DataValueField = "channelID";
            lbDefChannel.DataTextField = "channelName";
            lbDefChannel.DataBind();
            lbDefChannel.TextChanged += new EventHandler(lbDefChannel_TextChanged);

            //Устанавливаем канал по умолчанию. 
            if (!IsPostBack)
            {
                var channel = ChatServer.GetChannelByName(channelName);
                if (channel != null)
                    MyChatGrande1.ActiveChannel = channel;
            }
            

            base.OnInit(e);
        }

        

       /// <summary>
       /// Переключение активного канала.
       /// </summary>
       void lbDefChannel_TextChanged(object sender, EventArgs e)
       {
           var channel = ChatServer.GetChannelByName(lbDefChannel.SelectedValue);
           if (channel != null)
               MyChatGrande1.ActiveChannel = channel;
       }

        private class ChannelComparer : IEqualityComparer<TrainingChannel>
        {
            public bool Equals(TrainingChannel x, TrainingChannel y)
            {
                return x.channelID == y.channelID && x.channelName == y.channelName ;
            }

            public int GetHashCode(TrainingChannel obj)
            {
                return obj.GetHashCode();
            }
        }

        private class TrainingChannel
        {
            public string channelID { set; get; }

            public string channelName { set; get; }
        }
    }
}