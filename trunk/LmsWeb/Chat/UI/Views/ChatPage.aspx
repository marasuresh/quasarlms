<%@ Page Language="C#" 
    AutoEventWireup="true"
    MasterPageFile="~/Chat/UI/Top+SubMenu.Master" 
    CodeBehind="ChatPage.aspx.cs" 
    Inherits="N2.Templates.Chat.UI.Views.ChatPage" %>
<%@ Register Src="~/Chat/UI/Parts/ChatGrand.ascx" TagName="ChatGrande" TagPrefix="uc" %>

<asp:Content ID="defaultContent" ContentPlaceHolderID="TextContent" runat="server">
    <n2:ChromeBox ID="ChromeBox1" runat="server">
        <uc:ChatGrande ID="MyChatGrande1" runat="server" />
    </n2:ChromeBox>
</asp:Content>
