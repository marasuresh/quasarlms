<%@ Page Language="C#"  
Inherits="N2.Templates.Web.UI.TemplatePage`1[[N2.Messaging.Message, N2.Messaging]], N2.Templates" %>
<%@ Import Namespace="System.Linq" %>
<%@ Import Namespace="N2.Web" %>
<%@ Register TagPrefix="uc" TagName="Message" 
    Src="~\Messaging\UI\Parts\Message.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TextContent" Runat="Server">
            <uc:Message id="miViewMsg" runat="server"/>
</asp:Content>
