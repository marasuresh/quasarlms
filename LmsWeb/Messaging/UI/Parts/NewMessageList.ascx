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
                            <a href='<%# Eval("Url") %>' title='<%#Eval("From") + "   " + Eval("Published") %>'><label class="<%# Eval("TypeOfMessage") %>"><%# Eval("Subject") %></label></a>
                        </td>
                    </tr>
                </table>
            </div>
        </ItemTemplate>
        <FooterTemplate></div></FooterTemplate>
        <EmptyTemplate>
            <div style="text-align:center">Новых сообщений нет</div>
        </EmptyTemplate>
    </n2:Repeater>
    <br />
    <div style="text-align:center">
        <asp:HyperLink ID="hlMailBox" runat="server">Все сообщения</asp:HyperLink>    
    </div>
    </div>
</n2:Box>