<%@ Page Language="C#" MasterPageFile="~/Tools/Template.master" AutoEventWireup="true" CodeFile="TaskResults.aspx.cs" Inherits="Tools_Trainings_Tasks_TaskResults" Title="Untitled Page" %>

<%@ Register Src="../TrainingLink.ascx" TagName="TrainingLink" TagPrefix="uc2" %>

<%@ Register Src="TaskResultListControl.ascx" TagName="TaskResultListControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContentPlaceHolder" Runat="Server">
    <uc2:TrainingLink ID="TrainingLink1" runat="server" />
    <br />
    <br />
    <uc1:TaskResultListControl ID="TaskResultListControl1" runat="server" />
</asp:Content>

