<%@ WebService Language="C#" Class="Chat" %>

using System;
using System.Collections;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Serialization;

public class Chat  : System.Web.Services.WebService
{
    public struct Message
    {
        [XmlAttribute]
        public string UserName;
        
        public string Text;
        
        public Message(string username, string text)
        {
            this.UserName = username;
            this.Text = text;
        }
    }

    static ArrayList Messages = new ArrayList();
    
    [WebMethod]
    public Message[] GetMessagesFrom(int firstMessageIndex)
    {
        lock( Messages )
        {
            Message[] result = new Message[Messages.Count - firstMessageIndex];
            for( int i = firstMessageIndex; i < Messages.Count; i++ )
                result[i - firstMessageIndex] = (Message)Messages[i];
            return result;
        } 
    }

    [WebMethod]
    public string[] ScriptGetMessagesFrom(int firstMessageIndex)
    {
        Message[] result = GetMessagesFrom(firstMessageIndex);
        ArrayList resultList = new ArrayList(result.Length*2);
        foreach( Message msg in result )
        {
            resultList.Add(msg.UserName);
            resultList.Add(msg.Text);
        }
        return (string[])resultList.ToArray(typeof(string));
    }

    [WebMethod] 
    public void SendMessage(string userName, string text)
    {
        lock( Messages )
        {
            Messages.Add(new Message(userName, text));
        } 
    }
}