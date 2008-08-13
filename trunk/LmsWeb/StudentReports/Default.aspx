<%@ Page Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="Default.aspx.cs" 
    Inherits="StudentReports_Default" 
    Title="Щоденник слухача"
    EnableViewState="true" %>
<%@ Register Src="ReportTableControl.ascx" TagName="ReportTableControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="leftMenuPlaceHolder" Runat="Server">
Щоденник студента
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContentPlaceHolder" Runat="Server">
    <asp:Button ID="printButton" runat="server" Text="До друку..." OnClick="printButton_Click" />

    <uc1:ReportTableControl id="ReportTableControl1" runat="server">
    </uc1:ReportTableControl>
</asp:Content>

