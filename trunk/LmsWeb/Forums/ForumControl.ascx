<%@ Control
		Language="C#"
		AutoEventWireup="true"
		CodeFile="ForumControl.ascx.cs"
		Inherits="Forums_ForumControl" %>
<%@ Register Src="ForumTopicList.ascx" TagName="ForumTopicList" TagPrefix="uc1" %>
<%@ Register Src="CreateTopicControl.ascx" TagName="CreateTopicControl" TagPrefix="uc2" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
	<ContentTemplate>
		<uc1:ForumTopicList
				ID="ForumTopicList1"
				runat="server"
				TrainingId='<%# this.TrainingId %>' />
		<br />
		<br />
		<asp:Label
				ID="Label1"
				runat="server"
				Text="<%$ Resources:classRoom, PostTopicSubmit %>" />
		<br />
		<uc2:CreateTopicControl
				ID="CreateTopicControl1"
				runat="server"
				TrainingId='<%# this.TrainingId %>' />
	</ContentTemplate>
</asp:UpdatePanel>
