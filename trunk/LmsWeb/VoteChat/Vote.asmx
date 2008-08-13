<%@ WebService Language="C#" Class="Vote" %>

using System;
using System.Collections;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Serialization;

[WebService(Namespace = "http://tempuri.org/")]
public class Vote  : System.Web.Services.WebService
{
    public static VoteQuestion LastQuestion;
    public static Hashtable VoteByUserName = new Hashtable();
     
    public struct VoteQuestion
    {
        [XmlAttribute] 
        public int Index; 
        
        public string Question;

        public VoteQuestion(int index, string question)
        {
            this.Index = index;
            this.Question = question;
        }
    }
    
    public struct PeerVoteResult
    {
        [XmlAttribute]  
        public string UserName;
        
        [XmlAttribute] 
        public bool IsAnswerYes;
        
        public PeerVoteResult(string userName, bool isAnswerYes)
        {
            this.UserName = userName;
            this.IsAnswerYes = isAnswerYes;
        } 
    }  

    [WebMethod] 
    public VoteQuestion GetQuestion()
    {
        return LastQuestion; 
    }  
     
    [WebMethod]
    public string[] ScriptGetQuestion()
    {
        VoteQuestion result = GetQuestion();
         
        return new string[]
            {
                result.Index.ToString(),
                result.Question
            };
    }  

    [WebMethod]
    public void SendVote(string userName, int index, bool isAnswerYes)
    {
        lock( VoteByUserName )
        {
            if( index != LastQuestion.Index )
                return;

            VoteByUserName[userName] = isAnswerYes;
        }
    }

    [WebMethod]
    public PeerVoteResult[] GetStatistics()
    {
        ArrayList resultList;
        ArrayList userNames;
        lock( VoteByUserName )
        {
            resultList = new ArrayList(VoteByUserName.Count * 2);
            userNames = new ArrayList(VoteByUserName.Keys);
        }        
        
        userNames.Sort();

        foreach( string user in userNames )
        {
            bool isAnswerTrue = (bool)VoteByUserName[user];
            resultList.Add(new PeerVoteResult(user,isAnswerTrue));
        }

        return (PeerVoteResult[])resultList.ToArray(typeof(PeerVoteResult));
    } 

    [WebMethod]
    public string[] ScriptGetStatistics()
    {
        try
        {
            PeerVoteResult[] result = GetStatistics();

            ArrayList resultList = new ArrayList(1 + result.Length * 2);
            resultList.Add(LastQuestion.Question);


            foreach( PeerVoteResult peerVote in result )
            {
                resultList.Add(peerVote.UserName);
                resultList.Add(peerVote.IsAnswerYes.ToString());
            }

            return (string[])resultList.ToArray(typeof(string));
        }
        catch( Exception error )
        {
            return new string[] { LastQuestion.Question, error.ToString() };
        }
    }

    [WebMethod]
    public void NewVote(string questionText)
    {
        LastQuestion = new VoteQuestion(
            LastQuestion.Index + 1,
            questionText);
        lock( VoteByUserName )
            VoteByUserName.Clear();
    }     
}