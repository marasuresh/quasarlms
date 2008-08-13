<%@ Page Language="C#" MasterPageFile="~/Tools/Template.master" AutoEventWireup="true" CodeFile="Create.aspx.cs" Inherits="News_Create" Title="Untitled Page" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>
<%@ Register Src="CreateNewsDetails.ascx" TagName="CreateNewsDetails" TagPrefix="mi" %>
<asp:Content ID="mainContent" ContentPlaceHolderID="mainContentPlaceHolder" Runat="Server">
    <mi:CreateNewsDetails id="createNewsDetails" runat="server" NewsItemCreated="createNewsDetails_NewsItemCreated">
    </mi:CreateNewsDetails>
    
</asp:Content>

