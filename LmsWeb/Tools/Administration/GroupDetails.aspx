<%@ Page 
    Language="C#" 
    MasterPageFile="~/Tools/Template.master" 
    AutoEventWireup="true" 
    CodeFile="GroupDetails.aspx.cs" 
    Inherits="Tools_Administration_GroupDetails" 
    Title="Untitled Page"
    Transaction="Required" 
%>
<%@ Register Src="GroupUserListControl.ascx" TagName="GroupUserListControl" TagPrefix="uc1" %>
<%@ Register Src="GroupDetails.ascx" TagName="GroupDetails" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContentPlaceHolder" Runat="Server">
    <uc2:GroupDetails ID="GroupDetails1" runat="server" />
    <br />
    <br />
    <uc1:GroupUserListControl ID="GroupUserListControl1" runat="server" />
</asp:Content>

