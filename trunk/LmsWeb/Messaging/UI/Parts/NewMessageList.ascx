<%@ Import Namespace="N2.Messaging"%>
<%@ Import Namespace="N2.Web"%>
<%@ Control Language="C#" 
    AutoEventWireup="true" 
    CodeBehind="NewMessageList.ascx.cs" 
    Inherits="N2.Messaging.Messaging.UI.Parts.NewMessageList" %>

<n2:EditableDisplay ID="EditableDisplay1" runat="server" PropertyName="Title" />
<n2:Box ID="Box1" runat="server">
    <div class="part">
    <n2:ItemDataSource id="idsNews" runat="server" />
    <n2:Repeater ID="Repeater1" runat="server" DataSourceID="idsNews">
        <HeaderTemplate><div class="sidelist"></HeaderTemplate>
        <ItemTemplate>
            <div>
                <table style="width:100%" >
                    <tr>
                        <td style="width:10%; vertical-align:middle" align="left">
                            <asp:Image ID="msgTypeImage" runat="server" ImageUrl='<%# Eval("IconUrl") %>' />
                        </td>
                        <td style="width:1%"></td>
                        <td style="vertical-align:middle; text-align:center; width:auto">
                            <a href='<%# Url.Parse(this.CurrentItem.MailBox.Url).AppendQuery("msg",Eval("ID").ToString()) %>' title='<%#Eval("From") + "   " + Eval("Published") %>'><%# Eval("Subject") %></a>
                        </td>
                    </tr>
                </table>
            </div>
        </ItemTemplate>
        <FooterTemplate></div></FooterTemplate>
        <EmptyTemplate>
            <div style="text-align:center"><asp:Label ID="lblNoMsg" runat="server" Text="No New Messages" meta:resourcekey="lblNoMsgResource1"></asp:Label></div>
        </EmptyTemplate>
    </n2:Repeater>
    <br />
    </div
    <div>
        <asp:HyperLink ID="hlMailBox" runat="server" Text="All messages" meta:resourcekey="hlMailBoxResource1"></asp:HyperLink>    
    </div>
</n2:Box>