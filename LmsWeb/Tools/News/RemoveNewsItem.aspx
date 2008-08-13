<%@ Page Language="C#" MasterPageFile="~/Tools/Template.master" AutoEventWireup="true" CodeFile="RemoveNewsItem.aspx.cs" Inherits="Tools_News_RemoveNewsItem" Title="Untitled Page" %>

<%@ Register Src="NewsDetails.ascx" TagName="NewsDetails" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContentPlaceHolder" Runat="Server">
    <uc1:NewsDetails ID="NewsDetails1" runat="server" AllowEdit="false" />
    <br />
    <br />
    <asp:Localize ID="Localize1" runat="server" Text="Чи справді ви бажаєте видалити цю новину?"></asp:Localize><br />
    <br />
    <asp:Button ID="yesButton" runat="server" OnClick="yesButton_Click" Text="Так" Width="6em" />
    <asp:Button ID="noButton" runat="server" OnClick="noButton_Click" Text="Ні" Width="6em" /><br />
</asp:Content>

