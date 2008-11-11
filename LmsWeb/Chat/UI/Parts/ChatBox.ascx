<%@ Control Language="C#" 
    AutoEventWireup="true" 
    CodeBehind="ChatBox.ascx.cs" 
    Inherits="N2.Templates.Chat.UI.Parts.ChatBox" %>
<%@ Register Src="~/Chat/UI/Parts/ChatGrand.ascx" TagName="ChatGrande" TagPrefix="uc" %>

<n2:ChromeBox ID="ChromeBox2" runat="server">
    <div style="text-align:center; font-weight:bold">
        <asp:Label ID="Label1" runat="server" Text="Активный канал: " AssociatedControlID="lbDefChannel"></asp:Label>
        <asp:DropDownList ID="lbDefChannel" runat="server" AutoPostBack="True"></asp:DropDownList>    
    </div>
</n2:ChromeBox>
<n2:ChromeBox ID="ChromeBox1" runat="server">
        <uc:ChatGrande ID="MyChatGrande1" runat="server" />
</n2:ChromeBox>