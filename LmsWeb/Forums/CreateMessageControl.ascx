<%@ Control
		Language="C#"
		AutoEventWireup="true"
		CodeFile="CreateMessageControl.ascx.cs"
		Inherits="Forums_CreateMessageControl" %>
<asp:TextBox
		ID="messageTextBox"
		runat="server"
		Height="5em"
		TextMode="MultiLine"
		Width="100%" />
<div align=right>
	<asp:Button
			ID="sendButton"
			runat="server"
			Text="<%$ Resources:classRoom, PostReplySubmit %>"
			OnClick="sendButton_Click" />
</div>