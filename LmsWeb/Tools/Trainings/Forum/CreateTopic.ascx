<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CreateTopic.ascx.cs" Inherits="Trainings_Forum_CreateTopic" %>
<table width=100%><tr><td>
    <asp:TextBox ID="titleTextBox" runat="server" Width="100%" meta:resourcekey="titleTextBoxResource1"></asp:TextBox></td></tr><tr><td>
    <asp:Label ID="topicCaptionLabel" runat="server" Font-Italic="True" Font-Size="X-Small"
        Text="Topic Title" meta:resourcekey="topicCaptionLabelResource1"></asp:Label><br />
</td></tr><tr><td>
    <asp:TextBox ID="messateTextBox" runat="server" Height="20em" TextMode="MultiLine"
        Width="100%" meta:resourcekey="messateTextBoxResource1"></asp:TextBox></td></tr><tr><td style="height: 10px">
    <asp:Button ID="createTopicButton" runat="server" Text="Create Topic" meta:resourcekey="createTopicButtonResource1" OnClick="createTopicButton_Click" />&nbsp;<asp:Button
        ID="cancelButton" runat="server" OnClick="cancelButton_Click" Text="Cancel" meta:resourcekey="cancelButtonResource1" /></td></tr>
</table>