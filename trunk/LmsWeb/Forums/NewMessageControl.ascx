<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NewMessageControl.ascx.cs" Inherits="Forums_NewMessageControl" %>
<asp:TextBox ID="topicTitleTextBox" runat="server" Width="100%"></asp:TextBox><br />
<br />
<asp:Label ID="textCaptionLabel" runat="server" Text="<%$ Resources:classRoom, MessageText %>"></asp:Label><br />
<asp:TextBox ID="messageTextBox" runat="server" Height="20em" TextMode="MultiLine"
    Width="100%"></asp:TextBox>
<div align=right>
<asp:Button ID="sendButton" runat="server" Text="<%$ Resources:classRoom, PostTopicPost %>" OnClick="sendButton_Click" UseSubmitBehavior="False" />
</div>
