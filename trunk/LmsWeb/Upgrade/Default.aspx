<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Upgrade_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
		<asp:GridView
			ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1"
			EmptyDataText="There are no data records to display.">
			<Columns>
				<asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" />
				<asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
				<asp:BoundField DataField="CodeName" HeaderText="CodeName" SortExpression="CodeName" />
				<asp:CheckBoxField DataField="IsGlobal" HeaderText="IsGlobal" SortExpression="IsGlobal" />
			</Columns>
		</asp:GridView>
		<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DceConnectionString %>"
			ProviderName="<%$ ConnectionStrings:DceConnectionString.ProviderName %>" SelectCommand="SELECT [ID], [Name], [CodeName], [IsGlobal] FROM [Roles]">
		</asp:SqlDataSource>
		<asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Populate ASP.Net Roles" />
		<br />
		<asp:Literal ID="Literal1" runat="server" Visible="False"></asp:Literal></div>
    </form>
</body>
</html>
