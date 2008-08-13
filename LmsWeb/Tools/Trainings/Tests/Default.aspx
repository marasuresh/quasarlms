<%@ Page Language="C#" MasterPageFile="~/Tools/Template.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Trainings_Tests_Default" Title="Untitled Page" %>

<%@ Register Src="../TrainingLink.ascx" TagName="TrainingLink" TagPrefix="uc1" %>

<%@ Register Src="TrainingTestList.ascx" TagName="TrainingTestList" TagPrefix="mi" %>
<asp:Content ID="mainContent" ContentPlaceHolderID="mainContentPlaceHolder" Runat="Server">
    <uc1:TrainingLink ID="TrainingLink1" runat="server" />
    <br />
    <br />
    <mi:TrainingTestList ID="TrainingTestList1" runat="server" />
</asp:Content>

