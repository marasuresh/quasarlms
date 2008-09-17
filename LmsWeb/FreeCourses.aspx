<%@ Page
		language="c#"
		Inherits="DCE.FreeCourses"
		CodeFile="FreeCourses.aspx.cs"
		MasterPageFile="~/MasterPage.master" %>
<%@ Register Src="~/Common/FreeIntro.ascx" TagName="FreeIntro" TagPrefix="lms" %>
<asp:Content
		runat="server"
		ID="ctnCenter"
		ContentPlaceHolderID="ContentAndSidebar">
	<lms:FreeIntro runat="server" />
</asp:Content>