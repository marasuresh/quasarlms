<%@ Page
		Language="C#"
		MasterPageFile="~/Tools/Template.master"
		AutoEventWireup="true"
		CodeFile="Default.aspx.cs"
		Inherits="Orders_Default"
		Title="Untitled Page"
		Culture="auto"
		meta:resourcekey="PageResource1"
		UICulture="auto" %>

<%@ Register Src="OrderList.ascx" TagName="OrderList" TagPrefix="uc1" %>
<asp:Content
		ID="Content1"
		ContentPlaceHolderID="mainContentPlaceHolder"
		Runat="Server">
    <uc1:OrderList
			ID="OrderList1"
			runat="server" />
</asp:Content>

