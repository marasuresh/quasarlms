<%@ Page
		Language="C#"
		MasterPageFile="~/Tools/Template.master"
		AutoEventWireup="true"
		CodeFile="Create.aspx.cs"
		Inherits="Tools_Trainings_Tasks_Create"
		Culture="auto"
		UICulture="auto"
		meta:resourcekey="PageResource1" %>

<%@ Register
		Src="CreateTaskDetailsControl.ascx"
		TagName="CreateTaskDetailsControl"
		TagPrefix="uc1" %>
<%@ Register
		Src="../TrainingLink.ascx"
		TagName="TrainingLink"
		TagPrefix="uc2" %>
<asp:Content
		ID="Content1"
		ContentPlaceHolderID="mainContentPlaceHolder"
		Runat="Server">
    <uc2:TrainingLink ID="TrainingLink1" runat="server" />
    <br />
    <br />
    <uc1:CreateTaskDetailsControl ID="CreateTaskDetailsControl1" runat="server" />
</asp:Content>

