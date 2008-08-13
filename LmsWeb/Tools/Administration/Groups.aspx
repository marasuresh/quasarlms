<%@ Page 
    Language="C#" 
    MasterPageFile="~/Tools/Template.master" 
    AutoEventWireup="true" 
    CodeFile="Groups.aspx.cs" 
    Inherits="Administration_Groups" 
    Title="Untitled Page"
    Transaction="Required" 
%>
<%@ Register Src="GroupList.ascx" TagName="GroupList" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContentPlaceHolder" Runat="Server">
    <uc1:GroupList ID="GroupList1" runat="server" />
</asp:Content>

