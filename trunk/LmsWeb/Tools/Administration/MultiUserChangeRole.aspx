<%@ Page
    Language="C#"
    MasterPageFile="~/Tools/Template.master"
    AutoEventWireup="true"
    CodeFile="MultiUserChangeRole.aspx.cs"
    Inherits="Administration_MultiUserChangeRole"
    Title="Untitled Page"
    Transaction="Required" 
%>
<%@ Register Src="MultiUserChangeRole.ascx" TagName="MultiUserChangeRole" TagPrefix="uc1" %>
<%@ Register Src="MultiUserList.ascx" TagName="MultiUserList" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContentPlaceHolder" Runat="Server">
    <uc2:MultiUserList ID="MultiUserList1" runat="server" />
    <br />
    <asp:Label ID="setRoleLabel" runat="server" Text="Оберіть роль"></asp:Label><br />
    <uc1:MultiUserChangeRole id="MultiUserChangeRole1" runat="server">
    </uc1:MultiUserChangeRole>
</asp:Content>

