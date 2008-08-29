<%@ Page
	Inherits="N2.Web.UI.ContentPage`1[[N2.Lms.Items.Topic, Convert]], N2"
	Language="C#" %>
<%@ Register src="Topic.ascx" tagname="Topic" tagprefix="uc2" %>
<asp:Content runat="server" ContentPlaceHolderID="TextContent">
	<uc2:Topic ID="Topic1" runat="server" />
</asp:Content>