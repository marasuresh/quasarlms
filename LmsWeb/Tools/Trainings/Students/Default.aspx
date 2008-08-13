<%@ Page
		Language="C#"
		MasterPageFile="~/Tools/Template.master"
		AutoEventWireup="true"
		CodeFile="Default.aspx.cs"
		Inherits="Trainings_Students_Default"
		Title="Untitled Page"
		Culture="auto"
		meta:resourcekey="PageResource1"
		UICulture="auto" %>
<%@ Register Src="../TrainingLink.ascx" TagName="TrainingLink" TagPrefix="mi" %>
<%@ Register Src="TrainingStudentList.ascx" TagName="TrainingStudentList" TagPrefix="mi" %>
<asp:Content ID="mainContent" ContentPlaceHolderID="mainContentPlaceHolder" Runat="Server">
    <mi:TrainingLink ID="TrainingLink1" runat="server" />
    <br />
    <br />
    <asp:Button ID="subscribeButton" runat="server" OnClick="subscribeButton_Click" Text="Subscribe..." meta:resourcekey="subscribeButtonResource1" /><br />
    <br />
    <mi:TrainingStudentList ID="TrainingStudentList1" runat="server" OnLoad="TrainingStudentList1_Load" PageSize="30" />
</asp:Content>

