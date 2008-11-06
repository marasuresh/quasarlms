<%@ Control Language="c#" Codebehind="cp_message.ascx.cs" AutoEventWireup="True"
    Inherits="yaf.pages.cp_message" %>
<%@ Register TagPrefix="yaf" Namespace="yaf.controls" Assembly="yaf" %>
<yaf:PageLinks runat="server" ID="PageLinks" />
<asp:Repeater ID="Inbox" runat="server">
    <HeaderTemplate>
        <table class="content" cellspacing="1" cellpadding="0" width="100%">
    </HeaderTemplate>
    <FooterTemplate>
        </table>
    </FooterTemplate>
    <SeparatorTemplate>
        <tr class="postsep">
            <td colspan="2" style="height: 7px">
            </td>
        </tr>
    </SeparatorTemplate>
    <ItemTemplate>
        <tr>
            <td class="header1" colspan="2">
                <%# HtmlEncode(DataBinder.Eval(Container.DataItem,"Subject")) %>
            </td>
        </tr>
        <tr>
            <td class="postheader">
                <%# HtmlEncode(DataBinder.Eval(Container.DataItem,"FromUser")) %>
            </td>
            <td class="postheader">
                <table cellspacing="0" cellpadding="0" width="100%">
                    <tr>
                        <td>
                            <b>
                                <%# GetText("posted") %>
                            </b>
                            <%# FormatDateTime((System.DateTime)((System.Data.DataRowView)Container.DataItem)["Created"]) %>
                        </td>
                        <td align="right">
                            <asp:LinkButton ID="DeleteMessage" OnLoad="DeleteMessage_Load" ToolTip="Delete this message" Visible='<%# !IsSentItem %>'
                                runat="server" CommandName="delete" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"UserPMessageID") %>'><%# GetThemeContents("BUTTONS","DELETEPOST") %></asp:LinkButton>
                            <asp:LinkButton ID="ReplyMessage" ToolTip="Reply to this message" runat="server"
                                CommandName="reply" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"UserPMessageID") %>'><%# GetThemeContents("BUTTONS","REPLYPM") %></asp:LinkButton>
                            <asp:LinkButton ID="QuoteMessage" ToolTip="Reply with quote" runat="server" CommandName="quote"
                                CommandArgument='<%# DataBinder.Eval(Container.DataItem,"UserPMessageID") %>'><%# GetThemeContents("BUTTONS","QUOTEPOST") %></asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="post">
                &nbsp;</td>
            <td class="post" valign="top">
                <%# FormatBody(Container.DataItem) %>
            </td>
        </tr>
    </ItemTemplate>
</asp:Repeater>
<yaf:SmartScroller ID="SmartScroller1" runat="server" />
