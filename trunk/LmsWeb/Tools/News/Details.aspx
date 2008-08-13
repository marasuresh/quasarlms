<%@ Page Language="C#" MasterPageFile="~/Tools/Template.master" AutoEventWireup="true" CodeFile="Details.aspx.cs" Inherits="News_Details" Title="Untitled Page" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Src="NewsDetails.ascx" TagName="NewsDetails" TagPrefix="mi" %>
<asp:Content ID="mainContent" ContentPlaceHolderID="mainContentPlaceHolder" Runat="Server">
	<asp:UpdatePanel ID="UpdatePanel1" runat="server">
		<ContentTemplate>
			<mi:NewsDetails ID="NewsDetails1" runat="server" />
		</ContentTemplate>
	</asp:UpdatePanel>
    <br />
    <br />
    <asp:Button ID="removeButton" runat="server" OnClick="removeButton_Click" Text="Видалити" />
</asp:Content>