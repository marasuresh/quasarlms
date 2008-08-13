<%@ Page 
    Language="C#" 
    MasterPageFile="~/Tools/Template.master" 
    AutoEventWireup="true"
    CodeFile="MultiRemoveFromGroup.aspx.cs"
    Inherits="Administration_MultiRemoveFromGroup"
    Title="Untitled Page"
    Transaction="Required" 
%>
<%@ Register Src="MultiRemoveFromGroupControl.ascx" TagName="MultiRemoveFromGroupControl"
    TagPrefix="uc1" %>
<%@ Register Src="MultiUserList.ascx" TagName="MultiUserList" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContentPlaceHolder" Runat="Server">
    <uc2:MultiUserList ID="MultiUserList1" runat="server" />
    <br />
    <asp:Label ID="selectGroupToRemoveFromLabel" runat="server" Text="Оберіть групу для виключення:"></asp:Label><br />
    <uc1:MultiRemoveFromGroupControl ID="MultiRemoveFromGroupControl1" runat="server" />
</asp:Content>

