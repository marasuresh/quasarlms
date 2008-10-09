<%@ Page Language="C#"  
Inherits="N2.Templates.Web.UI.TemplatePage`1[[N2.Messaging.MailBox, N2.Messaging]], N2.Templates" %>
<%@ Import Namespace="System.Linq" %>
<%@ Import Namespace="N2.Web" %>
<%@ Register TagPrefix="uc" TagName="MessageInput" 
    Src="~\Messaging\UI\Parts\MessageInput.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TextContent" Runat="Server">
            <uc:MessageInput id="miNewMsg" runat="server"/>
</asp:Content>