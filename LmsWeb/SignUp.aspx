<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SignUp.aspx.cs" Inherits="SignUp" Title="Untitled Page" %>

<%@ Register Src="Common/Registration.ascx" TagName="Registration" TagPrefix="uc1" %>
<asp:Content
		ID="ctnSignUp"
		ContentPlaceHolderID="cphCenterColumn"
		Runat="Server">
	<uc1:Registration
			ID="Registration1"
			runat="server" />
</asp:Content>

