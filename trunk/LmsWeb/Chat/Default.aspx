<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register Src="UI/Parts/ChatGrand.ascx" TagName="ChatGrande" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Subgurim ASP.NET AJAX Chat</title>
    <style type="text/css">
        body
        {
            font-famoily: arial;
            margin: 5px;
        }
    
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Subgurim ASP.NET AJAX Chat</h1>
            For developers: <a href="http://www.codeplex.com/chat">Codeplex opensource project</a>
            <br />
            For users: <a href="http://chat.subgurim.net/">Official Website</a> 
            <br />
            <br />
            <uc1:ChatGrande ID="ChatGrande1" runat="server" />
            
        </div>
    </form>
</body>
</html>
