<%@ Page Language="C#" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>Vote and chat</title>
    <script language=JScript src=VoteWS.js></script>
    <script language=JScript src=Chat.js></script>
    <style type="text/css">
        body
        {
            background: #9CAEBD;
        }
    
        #chatContent
        {
            overflow: auto;            
            width: 100%;
            height: 120px;
            border: solid 1px black;
            background: white;
            
            font-size: 80%;
            
            padding: 2px;
        }
        
        #chatStatus
        {
            background: gray;
            color: silver;
            font-size: 50%;
        }        
        
        #chatText
        {
            width: 99%;
            height: 100%;
        }
        
        #voteAnswer
        {
            text-align: center;
        }
        
        #voteYes
        {
            background: transparent;
            border: none;
        }
        
        #voteNo
        {
            background: transparent;
            border: none;
        }

        #voteYes.selected
        {
            border: solid 4px white;
        } 
        
        #voteNo.selected
        {
            border: solid 4px white;
        } 
        
        #voteStatus
        {
            background: gray;
            color: silver;
            font-size: 50%;
        }        

        .chat-user
        {
            font-weight: bold;
            margin-right: 4px;
        }
        
    </style> 
</head>
<body>

<table width=100% height=100%>
<tr>
    <td>
            <table width=100% height=100%>
               <tr style="display:none"><td colspan=2>
                            <div id=chatStatus></div>  
                       </td>
               </tr>
                <tr><td colspan=2>
                            <div id=chatContent></div> 
                        </td>
                </tr>
               <tr bgcolor=#CED7DE><td width=99%>
                            <input type=text id="chatText" />
                       </td>
                       <td width=1>
                            <button id=chatSayBUTTON>>>></button>
                       </td>
               </tr> 
            </table>
    </td>
    <td width=200 valign=top>
        <div>
            <div id=voteStatus style="display: none"></div>  

            <span id=ws style="behavior:url(webservice.htc)"></span>
            <div id=voteQuestion>
                Loading vote question... 
            </div>
           
            <div id=voteAnswer>
                <button id=voteYes><img src=yes.gif /></button>
                <button id=voteNo><img src=no.gif /></button>
            </div>
        </div>
        
        <div id=userNameDIV style="display:none">
            <%=Session["UserName"]==null ? "anonymous" : Server.HtmlEncode(Session["UserName"]+"")%>
        </div>
    </td> 
</tr>
</table>

</body>
</html>
