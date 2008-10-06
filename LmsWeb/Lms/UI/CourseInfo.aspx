<%@ Page Title="" Language="C#"
Inherits="N2.Templates.Web.UI.TemplatePage`1[[N2.Lms.Items.Course, N2.Lms]], N2.Templates"
%>
<%@ Register src="CourseIntro.ascx" tagname="CourseIntro" tagprefix="uc1" %>

<asp:Content ContentPlaceHolderID="TextContent" Runat="Server">
	<uc1:CourseIntro ID="CourseIntro1" runat="server" />
	<n2:Zone runat="server" ZoneName="TopicContainer" />
</asp:Content>

