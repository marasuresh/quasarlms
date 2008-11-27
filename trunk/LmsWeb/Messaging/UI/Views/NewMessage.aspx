<%@ Page Language="C#"  
Inherits="N2.Templates.Web.UI.TemplatePage`1[[N2.Messaging.MailBox, N2.Messaging]], N2.Templates" %>
<%@ Import Namespace="System.Linq" %>
<%@ Import Namespace="N2.Web" %>
<%@ Register TagPrefix="uc" TagName="MessageInput" 
    Src="~\Messaging\UI\Parts\MessageInput.ascx" %>

<script runat="server">
	protected override void OnInit(EventArgs e)
	{
		Register.StyleSheet(this.Page, "~/Lms/UI/Css/MyAssignmentList.css");
		base.OnInit(e);
	}
</script>

<asp:Content ID="Content1" ContentPlaceHolderID="TextContent" Runat="Server">
	<n2:ChromeBox ID="ChromeBox1" runat="server">
	    <uc:MessageInput id="miNewMsg" runat="server"/>
	</n2:ChromeBox>
</asp:Content>