<%@ Control Language="C#" 
    AutoEventWireup="true" 
    CodeBehind="ChatBox.ascx.cs" 
    Inherits="N2.Templates.Chat.UI.Parts.ChatBox" %>
<%@ Register Src="~/Chat/UI/Parts/ChatGrand.ascx" TagName="ChatGrande" TagPrefix="uc" %>

<n2:ChromeBox runat="server">
    <div style="text-align:center; font-weight:bold">
        <asp:Label ID="Label1" runat="server" Text="Active Channel: " AssociatedControlID="lbDefChannel" meta:resourcekey="lbActiveChannelResource1"></asp:Label>
        <asp:DropDownList ID="lbDefChannel" runat="server" AutoPostBack="True"></asp:DropDownList>    
    </div>
	<uc:ChatGrande ID="MyChatGrande1" runat="server" />
</n2:ChromeBox>