<%@ Page
		language="c#"
		Inherits="DCE.CourseRequests"
		CodeFile="CourseRequests.aspx.cs"
		MasterPageFile="~/MasterPage.master" %>
<%@ Register Src="~/Common/TrainingRequest.ascx" TagName="TrainingRequest" TagPrefix="lms" %>
<asp:Content
		runat="server"
		ID="cntCenter"
		ContentPlaceHolderID="ContentAndSidebar">
	<lms:TrainingRequest runat="server" />
</asp:Content>