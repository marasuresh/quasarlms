<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CreateNewsDetails.ascx.cs" Inherits="News_CreateNewsDetails" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../RegionEditControl.ascx" TagName="RegionEditControl" TagPrefix="uc1" %>
<table width=100%>
<tr><td valign=top>
            <asp:Literal runat=server ID=dateCaption meta:resourcekey="dateCaptionResource1" Text=" Date"></asp:Literal>
        </td>
        <td>
			<asp:TextBox
					runat="server"
					ID="tbDate"
					ReadOnly="true"
					Text='<%# DateTime.Today %>' />
			<cc1:CalendarExtender
					ID="CalendarExtender1"
					runat="server"
					TargetControlID="tbDate" />
        </td>
</tr>
<tr>
        <td valign=top meta:resourcekey="titleHeadResource">
            <asp:Literal runat=server ID=titleCaption meta:resourcekey="titleCaptionResource1" Text="&#13;&#10;            Title&#13;&#10;            "></asp:Literal>
        </td>
        <td>
            <asp:TextBox ID="titleTextBox" runat="server" Width="100%" meta:resourcekey="titleTextBoxResource1"></asp:TextBox></td>
</tr>
<tr>
        <td valign=top>
            <asp:Literal runat=server ID=contentCaption meta:resourcekey="contentCaptionResource1" Text="&#13;&#10;            Content&#13;&#10;            "></asp:Literal>
        </td>
        <td>
            <asp:TextBox ID="contentTextBox" runat="server" Height="10em" Width="100%" meta:resourcekey="contentTextBoxResource1"></asp:TextBox></td>
</tr>
<tr>
        <td valign=top>
            <asp:Literal runat=server ID=urlCaption meta:resourcekey="urlCaptionResource1" Text="&#13;&#10;            URL&#13;&#10;            "></asp:Literal>
        </td>
        <td>
            <asp:TextBox ID="urlTextBox" runat="server" Width="100%" meta:resourcekey="urlTextBoxResource1"></asp:TextBox></td>
</tr>
<tr>
        <td valign=top>
            <asp:Literal runat=server ID=regionCaption Text="Регіон"></asp:Literal>
        </td>
        <td>
            <uc1:RegionEditControl ID="RegionEditControl1" runat="server" />
        </td>
</tr>
</table>
<asp:Button ID="createButton" runat="server" OnClick="createButton_Click" Text="Create" meta:resourcekey="createButtonResource1" />&nbsp;<asp:Button
    ID="cancelButton" runat="server" Text="Cancel" OnClick="cancelButton_Click" meta:resourcekey="cancelButtonResource1" />