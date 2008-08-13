<%@ Page
		language="c#"
		MasterPageFile="~/MasterPage.master"
		Inherits="DCE.MainPage"
		CodeFile="MainPage.aspx.cs"
		EnableEventValidation="true" %>
<asp:Content
		runat="server"
		ContentPlaceHolderID="cphCenterColumn"
		ID="ctnCenter">
	<asp:PlaceHolder
			ID="PlaceHolder1"
			runat="server" />
</asp:Content>