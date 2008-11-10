<%@ Control Language="C#" 
    AutoEventWireup="true" 
    CodeBehind="ChatBox.ascx.cs" 
    Inherits="N2.Templates.Chat.UI.Parts.ChatBox" %>
<%@ Register Src="~/Chat/UI/Parts/ChatGrand.ascx" TagName="ChatGrande" TagPrefix="uc" %>

<n2:ChromeBox ID="ChromeBox1" runat="server">
        <uc:ChatGrande ID="MyChatGrande1" runat="server" />
</n2:ChromeBox>