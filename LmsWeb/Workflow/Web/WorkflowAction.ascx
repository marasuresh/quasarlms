<%@ Control
		Language="C#"
		ClassName="WorkflowAction"
		CodeFile="WorkflowAction.ascx.cs"
		Inherits="WorkflowAction" %>
<%@ Import Namespace="N2.Workflow.Items" %>
<asp:Panel runat="server" GroupingText="Workflow">
<asp:MultiView runat="server" ID="mv" ActiveViewIndex="0">
	<asp:View runat="server" ID="vActionList">
		<asp:Repeater
				runat="server"
				ID="rptActionList"
				OnItemCommand="Action_Command">
			<ItemTemplate>
				<asp:Image runat="server"
					ImageUrl='<%# ((ActionDefinition)Container.DataItem).LeadsTo.Icon %>' />
				<asp:LinkButton
					runat="server"
					CommandArgument='<%# Eval("Name") %>'
					Text='<%# Eval("Title") %>' />
			</ItemTemplate>
			<SeparatorTemplate><br /></SeparatorTemplate>
		</asp:Repeater>
	</asp:View>
	
	<asp:View runat="server" ID="vEditState">
		<n2:ItemEditor runat="server" ID="ie" />
		<asp:LinkButton
				runat="server"
				Text="&laquo; Cancel"
				OnClick="Cancel_Click" />
	</asp:View>
	
</asp:MultiView>
</asp:Panel>