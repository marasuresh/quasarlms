<%@ Page Language="C#" MasterPageFile="~/Tools/Template.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="News_Default" Title="Untitled Page" Culture="auto" meta:resourcekey="PageResource1" %>

<%@ Register Src="NewsList.ascx" TagName="NewsList" TagPrefix="mi" %>
<asp:Content ID="mainContent" ContentPlaceHolderID="mainContentPlaceHolder" Runat="Server">
    <asp:Button ID="createNewsButton" runat="server" PostBackUrl="~/Tools/News/Create.aspx"
        Text="Create News..." meta:resourcekey="createNewsButtonResource1" /><br />
    <br />
    <mi:NewsList ID="NewsList1" runat="server" PageSize="30" />
</asp:Content>

