<%@ Control
	Language="C#"
	AutoEventWireup="true"
	ClassName="CurriculumControl"
	CodeBehind="Curriculum.ascx.cs"
	Inherits="N2.Calendar.Curriculum.UI.Views.Curriculum" %>
<n2:chromebox runat="server">
<table class="gridview" cellpadding="0" cellspacing="0">
	<tr class="header">
		<th>Курс</th>
		<th>&nbsp;&nbsp;- &nbsp;&nbsp; Обяз &nbsp; Сел</th></tr>
	<asp:Repeater
		runat="server"
		ID="rpt"
		DataSourceID="ds">
		<ItemTemplate>
	<tr class='<%# Container.ItemIndex % 2 == 0 ? "row" : "altrow" %>'>
		<td><asp:Label
					runat="server"
					ID="l1"
					Text='<%# Eval("Title") %>' />
			<asp:HiddenField
					runat="server"
					ID="hf"
					Value='<%# Eval("Id") %>' /></td>
		<td><asp:RadioButtonList
					AutoPostBack="true"
					OnSelectedIndexChanged="rbl_SelectedIndexChanged"
					ID="rbl"
					RepeatLayout="Flow"
					runat="server"
					RepeatDirection="Horizontal"
					SelectedValue='<%# Eval("Status") %>'>
				<asp:ListItem
						Value="0">&nbsp;.&nbsp;</asp:ListItem>
				<asp:ListItem Value="1">&nbsp;.&nbsp;</asp:ListItem>
				<asp:ListItem Value="2">&nbsp;.&nbsp;</asp:ListItem>
			</asp:RadioButtonList></td></tr>
		</ItemTemplate>
	</asp:Repeater>
</table>
</n2:chromebox>
<asp:ObjectDataSource ID="ds" runat="server" 
	OldValuesParameterFormatString="original_{0}" onselecting="ds_Selecting" 
	SelectMethod="GetCurriculumCourseInfo" 
	TypeName="N2.Lms.Items.CourseContainer" onobjectcreating="ds_ObjectCreating">
	<SelectParameters>
		<asp:Parameter Name="curriculumName" Type="String" />
	</SelectParameters>
</asp:ObjectDataSource>

