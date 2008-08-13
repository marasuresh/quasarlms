<%@ Page Language="C#" MasterPageFile="~/Tools/Template.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Trainings_Forum_Default" Title="Untitled Page" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>
<%@ Register Src="../TrainingLink.ascx" TagName="TrainingLink" TagPrefix="mi" %>
<%@ Register Src="TopicList.ascx" TagName="TopicList" TagPrefix="mi" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContentPlaceHolder" Runat="Server">
    <mi:traininglink id="TrainingLink1" runat="server"></mi:traininglink>
    <br />
    <br />
    <asp:Button ID="createTopicButton" runat="server" Text="Create Topic..." meta:resourcekey="createTopicButtonResource1" OnClick="createTopicButton_Click" /><br />
    <br />
    <mi:TopicList ID="TopicList1" runat="server" PageSize=30 />
</asp:Content>

