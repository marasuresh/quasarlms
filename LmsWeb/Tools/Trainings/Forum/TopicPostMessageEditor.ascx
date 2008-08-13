<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TopicPostMessageEditor.ascx.cs" Inherits="Trainings_Forum_TopicPostMessageEditor" %>
<asp:TextBox ID="messageTextBox" runat="server" Height="20em" Width="100%" meta:resourcekey="messageTextBoxResource1"></asp:TextBox><br />
<br />
<asp:Button ID="postButton" runat="server" OnClick="postButton_Click" Text="Post" meta:resourcekey="postButtonResource1" />
<asp:Button ID="cancelButton" runat="server" Text="Cancel" meta:resourcekey="cancelButtonResource1" />
