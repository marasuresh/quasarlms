<%@ Page Language="C#" MasterPageFile="~/Tools/Template.master" AutoEventWireup="true" CodeFile="DeleteMessage.aspx.cs" Inherits="Tools_Trainings_Announcements_DeleteMessage" Title="Untitled Page" %>

<%@ Register Src="MessageEditControl.ascx" TagName="MessageEditControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContentPlaceHolder" Runat="Server">
    <uc1:MessageEditControl ID="MessageEditControl1" runat="server" AllowEdit="false" />
    <br />
    <br />
    Видалити це оголошення?<br />
    <asp:Button ID="yesButton" runat="server" OnClick="yesButton_Click" Text="Так" Width="5em" />&nbsp;<asp:Button
        ID="noButton" runat="server" OnClick="noButton_Click" Text="Ні" Width="5em" />
</asp:Content>

