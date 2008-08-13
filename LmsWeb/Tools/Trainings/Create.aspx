<%@ Page Language="C#" MasterPageFile="~/Tools/Template.master" AutoEventWireup="true" CodeFile="Create.aspx.cs" Inherits="Trainings_Create" Title="Untitled Page" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Src="Create.ascx" TagName="Create" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContentPlaceHolder" Runat="Server">
    <uc1:Create ID="Create1" runat="server"  OnTrainingCreated="Create1_TrainingCreated"/>
</asp:Content>

