<%@ Page
		Language="C#"
		MasterPageFile="~/Tools/Template.master"
		AutoEventWireup="true"
		CodeFile="Default.aspx.cs"
		Inherits="Trainings_Tasks_Default"
		Title="Untitled Page"
		Culture="auto"
		UICulture="auto" %>

<%@ Register Src="../TrainingLink.ascx" TagName="TrainingLink" TagPrefix="uc2" %>

<%@ Register Src="TaskListControl.ascx" TagName="TaskListControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContentPlaceHolder" Runat="Server">
    <uc2:TrainingLink ID="TrainingLink1" runat="server" />
    <br />
    <br />
    <asp:Button ID="createButton" runat="server" OnClick="createButton_Click" Text="Створити завдання..." /><br />
    <br />
    &nbsp;<uc1:TaskListControl id="TaskListControl1" runat="server">
    </uc1:TaskListControl>
</asp:Content>

