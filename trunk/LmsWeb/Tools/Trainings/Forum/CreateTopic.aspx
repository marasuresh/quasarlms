<%@ Page Language="C#" MasterPageFile="~/Tools/Template.master" AutoEventWireup="true" CodeFile="CreateTopic.aspx.cs" Inherits="Trainings_Forum_CreateTopic" Title="Untitled Page" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Src="CreateTopic.ascx" TagName="CreateTopic" TagPrefix="uc2" %>

<%@ Register Src="../TrainingLink.ascx" TagName="TrainingLink" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContentPlaceHolder" Runat="Server">
    <uc1:TrainingLink ID="TrainingLink1" runat="server" />
    <br />
    <br />
    <uc2:CreateTopic ID="CreateTopic1" runat="server" />
</asp:Content>

