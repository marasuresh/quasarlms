<%@ Page Language="C#" MasterPageFile="~/Tools/Template.master" AutoEventWireup="true" CodeFile="Message.aspx.cs" Inherits="Trainings_Announcements_Message" Title="Untitled Page" %>

<%@ Register Src="../TrainingLink.ascx" TagName="TrainingLink" TagPrefix="uc2" %>

<%@ Register Src="MessageEditControl.ascx" TagName="MessageEditControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContentPlaceHolder" Runat="Server">
    <uc2:TrainingLink ID="TrainingLink1" runat="server" />
    <br />
    <br />
    <uc1:MessageEditControl ID="MessageEditControl1" runat="server" />
    <asp:Button ID="deleteButton" runat="server" Text="Видалити" OnClick="deleteButton_Click" />

</asp:Content>

