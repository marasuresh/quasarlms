<%@ Page
		language="c#"
		MasterPageFile="~/Templates/UI/Layouts/Top+SubMenu.Master"
		Inherits="DCE.MainPage"
		CodeFile="MainPage.aspx.cs"
		EnableEventValidation="true" %>
<asp:Content
		runat="server"
		ContentPlaceHolderID="ContentAndSidebar"
		ID="ctnCenter">
	vas ist dass
	<asp:PlaceHolder
			ID="PlaceHolder1"
			runat="server" />
</asp:Content>