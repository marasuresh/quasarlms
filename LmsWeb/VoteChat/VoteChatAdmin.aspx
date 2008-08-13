<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>Vote & Chat: administrator version</title>
    <script language=JScript src=VoteAdmin.js></script>
    <script language=JScript src=Chat.js></script> 
    <style type="text/css">
        .chat-user
        {
            font-weight: bold;
            margin-right: 4px;
        }
        
        .voteTable
        {
            
        }
        
        .voteTable th
        {
            font-size: 90%;
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
        
        #voteQuestion
        {
            width: 100%;
            height: 5em;
        }
        
        
    </style> 
</head>
<body>

<table width=100%>
<tr><td width=60% valign=top>

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



</td><td valign=top>


<table bgcolor=silver cellspacing=1 cellpadding=5 class=voteTable>
<tr>
    <td colspan=2>
        <span id=ws style="behavior:url(webservice.htc)"></span> 
        <input type=text id="voteQuestion" style="width: 20em%; height: 2em;" />
        <button id=newVoteBUTTON style="display: none">New vote</button>
        <button id=rejectVoteBUTTON style="display: none">Revert text</button>
    </td>
</tr>
<tr>
    <th> Проголосовали &laquo;Да&raquo; </th>
    <th> Проголосовали &laquo;Нет&raquo; </th>
</tr>
<tr bgcolor=white>
    <td valign=top id=yesList>
    </td>
    <td valign=top id=noList>
    </td>
</tr>
<tr>
    <td id=yesTotal>
    </td>
    <td id=noTotal>
    </td> 
</tr>
</table>

</td>
</tr>
</table>


<div id=userNameDIV>
    <%=Session["UserName"]==null ? "Администратор" : Server.HtmlEncode(Session["UserName"]+"")%>
</div>


</body>
</html>
