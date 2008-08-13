<%@ Page Language="C#" MasterPageFile="~/Tools/Template.master" AutoEventWireup="true" CodeFile="Subscribe.aspx.cs" Inherits="Orders_Subscribe" Title="Untitled Page" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Src="OrderDetailsHead.ascx" TagName="OrderDetailsHead" TagPrefix="uc2" %>

<%@ Register Src="SubscribeAvailableTrainingList.ascx" TagName="SubscribeAvailableTrainingList"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContentPlaceHolder" Runat="Server">
    <uc2:OrderDetailsHead ID="OrderDetailsHead1" runat="server" OnLoad="OrderDetailsHead1_Load" />
    <br />
    <br />
    <uc1:SubscribeAvailableTrainingList ID="SubscribeAvailableTrainingList1" runat="server" />
    <br />
    <asp:Button ID="cancelSubscribeButton" runat="server" OnClick="cancelSubscribeButton_Click"
        Text="Cancel" />
</asp:Content>

