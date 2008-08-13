<%@ Page Language="C#" MasterPageFile="~/Tools/Template.master" AutoEventWireup="true" CodeFile="Topic.aspx.cs" Inherits="Trainings_Forum_Topic" Title="Untitled Page" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Src="TopicFirstMessage.ascx" TagName="TopicFirstMessage" TagPrefix="uc3" %>
<%@ Register Src="TopicPostMessageEditor.ascx" TagName="TopicPostMessageEditor" TagPrefix="uc4" %>

<%@ Register Src="TopicMessageList.ascx" TagName="TopicMessageList" TagPrefix="uc2" %>

<%@ Register Src="../TrainingLink.ascx" TagName="TrainingLink" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContentPlaceHolder" Runat="Server">
    <uc1:TrainingLink ID="TrainingLink1" runat="server" />
    &nbsp;(<asp:HyperLink ID="forumHyperLink" runat="server" meta:resourcekey="forumHyperLinkResource1">Forum</asp:HyperLink>)<br />
    <br />
    <uc3:TopicFirstMessage ID="TopicFirstMessage1" runat="server" />
    <br />
    <br />
    <uc2:TopicMessageList ID="TopicMessageList1" runat="server" />
    <br />
    <br />
    <uc4:TopicPostMessageEditor id="TopicPostMessageEditor1" runat="server">
    </uc4:TopicPostMessageEditor>
</asp:Content>

