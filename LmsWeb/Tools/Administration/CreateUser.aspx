<%@ Page Language="C#" MasterPageFile="~/Tools/Template.master" AutoEventWireup="true" CodeFile="CreateUser.aspx.cs" Inherits="Tools_Administration_CreateUser" Title="Untitled Page" %>

<%@ Register Src="CreateUserEditor.ascx" TagName="CreateUserEditor" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContentPlaceHolder" Runat="Server">
    <uc1:CreateUserEditor id="CreateUserEditor1" runat="server">
    </uc1:CreateUserEditor>
</asp:Content>

