<%@ Control
		Language="C#"
		ClassName="WorkflowAction"
		CodeFile="WorkflowAction.ascx.cs"
		Inherits="WorkflowAction" %>

<div id="wfAction"><div class="dragDiv">
<asp:DropDownList
		ID="ddlAction"
		runat="server"
		DataTextField="Text"
		DataValueField="Value"
		AppendDataBoundItems="true"
		width="150"
		OnPreRender="ddlAction_PreRender">
	<asp:ListItem Text="(select workflow action)" Value="" />
</asp:DropDownList>
<asp:TextBox runat="server" ID="tbComment" />
</div></div>