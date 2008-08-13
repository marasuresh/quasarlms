<%@ Page Language="C#" MasterPageFile="~/Tools/Template.master" AutoEventWireup="true" CodeFile="Details.aspx.cs" Inherits="Trainings_Details" Title="Untitled Page" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>
<%@ Register Src="TrainingDetails.ascx" TagName="TrainingDetails" TagPrefix="mi" %>
<%@ Register Src="TrainingSchedule.ascx" TagName="TrainingSchedule" TagPrefix="mi" %>
<asp:Content ID="mainContent" ContentPlaceHolderID="mainContentPlaceHolder" Runat="Server">
    <asp:Button ID="studentsButton" runat="server" OnClick="studentsButton_Click" Text="Students" meta:resourcekey="studentsButtonResource1" /><asp:Button
        ID="testsButton" runat="server" OnClick="testsButton_Click" Text="Tests" meta:resourcekey="testsButtonResource1" /><asp:Button
            ID="forumButton" runat="server" OnClick="forumButton_Click" Text="Forum" meta:resourcekey="forumButtonResource1" /><asp:Button
                ID="announcementsButton" runat="server" OnClick="announcementsButton_Click" Text="Announcements" meta:resourcekey="announcementsButtonResource1" /><asp:Button
                    ID="tasksButton" runat="server" OnClick="tasksButton_Click" Text="Tasks" meta:resourcekey="tasksButtonResource1" /><br />
    <br />
    <mi:TrainingDetails ID="TrainingDetails1" runat="server" />
    <br />
    <br />
    Розклад:<br />
    <mi:TrainingSchedule ID="TrainingSchedule1" runat="server" />
</asp:Content>

