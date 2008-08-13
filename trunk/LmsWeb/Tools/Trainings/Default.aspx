<%@ Page Language="C#" MasterPageFile="~/Tools/Template.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Trainings_Default" Title="Untitled Page" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Src="TrainingList.ascx" TagName="TrainingList" TagPrefix="mi" %>
<asp:Content ID="mainContent" ContentPlaceHolderID="mainContentPlaceHolder" Runat="Server">
    <asp:Button ID="createTrainingButton" runat="server" Text="Create training..." meta:resourcekey="createTrainingButtonResource1" OnClick="createTrainingButton_Click" /><br />
    <br />
    <mi:TrainingList ID="largeTrainingList" runat="server" PageSize="30" OnLoad="largeTrainingList_Load" />
</asp:Content>

