<%@ Page Language="C#" MasterPageFile="~/Tools/Template.master" AutoEventWireup="true" CodeFile="StudentResults.aspx.cs" Inherits="Trainings_Tests_StudentResults" Title="Untitled Page" %>

<%@ Register Src="../TrainingLink.ascx" TagName="TrainingLink" TagPrefix="uc2" %>

<%@ Register Src="StudentResultsControl.ascx" TagName="StudentResultsControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContentPlaceHolder" Runat="Server">
    <uc2:TrainingLink ID="TrainingLink1" runat="server" />
    <br />
    <br />
    <uc1:StudentResultsControl ID="StudentResultsControl1" runat="server" />
    <br />
    <br />
    <asp:Button ID="allowRetryButton" runat="server" OnClick="allowRetryButton_Click1"
        Text="Дозволити перездачу" />
</asp:Content>

