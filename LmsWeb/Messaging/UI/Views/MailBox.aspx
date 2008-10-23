<%@ Page Language="C#" MasterPageFile="~/Messaging/Top+SubMenu.master" AutoEventWireup="true"
    CodeBehind="MailBox.aspx.cs" Inherits="Messaging_UI_MailBox" %>

<%@ Import Namespace="System.Linq" %>
<%@ Import Namespace="N2.Web" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TextContent" runat="Server">
    <asp:Menu ID="mnuMailBox" runat="server" Orientation="Horizontal" Width="100%" 
        OnMenuItemClick="mnuMailBox_MenuItemClick" BackColor="White">
        <Items>
            <asp:MenuItem Text="Все сообщения" Value="0" Selected="True"></asp:MenuItem>
            <asp:MenuItem Text="Черновики" Value="1"></asp:MenuItem>
            <asp:MenuItem Text="Корзина" Value="2"></asp:MenuItem>
        </Items>
    </asp:Menu>
    <asp:MultiView ID="mvMailBox" runat="server" ActiveViewIndex="0">
        <asp:View ID="Tab1" runat="server">
            <div style="width: 100%;">
                <br />
                <table style="width: 100%">
                    <tr>
                        <td align="left">
                            <a class='lettercss' href='<%= Url.Parse(CurrentPage.Url).AppendSegment("newLetter")%>'>
                                <img src='<%= this.ResolveClientUrl("~/Messaging/UI/Images/email_open.png") %>' />
                                Письмо&hellip;</a>
                            
                        </td>
                        <td align="left">
                            <a class='announcementcss' href='<%= Url.Parse(CurrentPage.Url).AppendSegment("newAnnouncement")%>'>
								<img src='<%= this.ResolveClientUrl("~/Messaging/UI/Images/bell.png") %>' />
                                Объявление&hellip;</a>
                            
                        </td>
                        <td align="left">
                            <a class='taskcss' href='<%= Url.Parse(CurrentPage.Url).AppendSegment("newTask")%>'>
                                <img src='<%= this.ResolveClientUrl("~/Messaging/UI/Images/wrench.png") %>' />
                                Задание&hellip;</a>
                            
                        </td>
                    </tr>
                </table>
                <br />
                <table style="width: 100%">
                    <tr>
                        <td align="center">
                            <asp:Button ID="btnShowMsg" runat="server" Text="Показать" />
                            <asp:DropDownList ID="ddlMsgType" runat="server">
                                <asp:ListItem Value="all" Selected="True">
                                    Все
                                </asp:ListItem>
                                <asp:ListItem Value="letters">
                                    Письма
                                </asp:ListItem>
                                <asp:ListItem Value="tasks">
                                    Задания
                                </asp:ListItem>
                                <asp:ListItem Value="announcements">
                                    Объявления
                                </asp:ListItem>
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlDirection" runat="server">
                                <asp:ListItem Value="all">
                                    Все
                                </asp:ListItem>
                                <asp:ListItem Value="incoming" Selected="True">
                                    Полученные
                                </asp:ListItem>
                                <asp:ListItem Value="sent">
                                    Отправленные
                                </asp:ListItem>
                            </asp:DropDownList>
                            <asp:CheckBox ID="cboxOnlyNew" runat="server" Text=" непрочитанные" />
                        </td>
                    </tr>
                </table>
                <br />
            </div>
        </asp:View>
        <asp:View ID="Tab2" runat="server">
            <br />
            <br />
        </asp:View>
        <asp:View ID="Tab3" runat="server">
            <br />
            <div>
                <asp:Button ID="btnEmptyRecBin" runat="server" Text="Очистить корзину" 
                    onclick="btnEmptyRecBin_Click" />        
            </div>
            <br />
        </asp:View>
    </asp:MultiView>
    <table width="100%">
        <tr>
            <td>
                <asp:GridView ID="gvMailBox" runat="server" PageSize="15" Width="100%" CssClass="Grid"
                    AllowPaging="False" DataKeyNames="ID" AutoGenerateColumns="false" GridLines="Horizontal">
                    <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                    <RowStyle CssClass="GridItem" />
                    <AlternatingRowStyle CssClass="GridAltItem" />
                    <EmptyDataTemplate>
                        <div style="text-align: center">
                            У вас нет сообщений.
                        </div>
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:TemplateField HeaderText="Тип" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:HyperLink ID="hlDeletedItem" runat="server" NavigateUrl='<%#  Eval("RewrittenUrl") %>'>
                                    <asp:Image ID="Image1" runat="server" ImageUrl='<%# Eval("IconUrl") %>' />
                                </asp:HyperLink>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Автор" DataField="From" HeaderStyle-HorizontalAlign="Left">
                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Получатель" DataField="To" HeaderStyle-HorizontalAlign="Left">
                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:BoundField>
                        <%--<asp:BoundField HeaderText="Тема" DataField="Subject" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="50%"/>--%>
                        <asp:TemplateField HeaderText="Тема" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="50%">
                            <ItemTemplate>
                                <asp:HyperLink ID="hlMsgLocation" runat="server" NavigateUrl='<%# Eval("RewrittenUrl") %>'>
						                        <%# Eval("Subject")%>
                                </asp:HyperLink>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                            <ItemStyle Width="50%"></ItemStyle>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Дата" DataField="Created" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Center">
                            <ItemStyle HorizontalAlign="Center" Width="20%"></ItemStyle>
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>
