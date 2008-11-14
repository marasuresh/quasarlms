<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Curriculum.ascx.cs"
	Inherits="N2.Calendar.Curriculum.UI.Views.Curriculum" %>
<n2:chromebox id="ChromeBox1" runat="server">
<table class="gridview" cellpadding="0" cellspacing="0">
	<tr class="header">
		<th>Курс</th>
		<th>&nbsp;&nbsp;- &nbsp;&nbsp; Обяз &nbsp; Сел</th></tr>
	<asp:Repeater runat="server" ID="rpt" OnItemCommand="rpt_ItemCommand">
		<ItemTemplate>
	<tr class='<%# Container.ItemIndex % 2 == 0 ? "row" : "altrow" %>'>
		<td><asp:Label
					runat="server"
					ID="l1"
					Text='<%# Eval("CourseName") %>' />
			<asp:HiddenField
					runat="server"
					ID="hf"
					Value='<%# Eval("CourseID") %>' /></td>
		<td><asp:RadioButtonList
					AutoPostBack="true"
					OnSelectedIndexChanged="rbl_SelectedIndexChanged"
					ID="rbl"
					RepeatLayout="Flow"
					runat="server"
					RepeatDirection="Horizontal"
					SelectedValue='<%#((CourseInfo)Container.DataItem).CourseExclude %>'>
				<asp:ListItem
						Value="0">&nbsp;.&nbsp;</asp:ListItem>
				<asp:ListItem Value="1">&nbsp;.&nbsp;</asp:ListItem>
				<asp:ListItem Value="2">&nbsp;.&nbsp;</asp:ListItem>
			</asp:RadioButtonList></td></tr>
		</ItemTemplate>
	</asp:Repeater>
</table>
</n2:chromebox>
