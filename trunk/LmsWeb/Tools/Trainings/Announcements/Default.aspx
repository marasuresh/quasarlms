<%@ Page Language="C#" MasterPageFile="~/Tools/Template.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Trainings_Announcements_Default" Title="Untitled Page" %>

<%@ Register Src="../TrainingLink.ascx" TagName="TrainingLink" TagPrefix="uc2" %>

<%@ Register Src="AnnouncementListControl.ascx" TagName="AnnouncementListControl"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContentPlaceHolder" Runat="Server">
    <uc2:TrainingLink ID="TrainingLink1" runat="server" />
    <br />
    <br />
    <uc1:AnnouncementListControl ID="AnnouncementListControl1" runat="server" />
    <br />
    <asp:Button ID="newAnnouncementButton" runat="server" Text="Нове оголошення" OnClick="newAnnouncementButton_Click" />
</asp:Content>

