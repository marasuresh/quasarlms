<%@ Page Language="C#"  MasterPageFile="~/Messaging/Top+SubMenu.master" AutoEventWireup="true" CodeBehind="MailBox.aspx.cs" Inherits="Messaging_UI_MailBox" %>
<%@ Import Namespace="System.Linq" %>
<%@ Import Namespace="N2.Web" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TextContent" Runat="Server">
            <br />
            <div style="width:100%">
                <table style="width:100%">
                    <tr>
                        <td align="left">
                            <a class='letter'  href='<%= Url.Parse(CurrentPage.Url).AppendSegment("newLetter")%>'>Новое письмо&hellip;</a>
                            <img src="/Lms/UI/Img/04/50.png"/>
                        </td>
                        <td align="left">
                            <a class='announcement' href='<%= Url.Parse(CurrentPage.Url).AppendSegment("newAnnouncement")%>'>Новое объявление&hellip;</a>
                            <img src="/Lms/UI/Img/04/19.png"/>
                        </td>
                        <td align="left">
                            <a class='task' href='<%= Url.Parse(CurrentPage.Url).AppendSegment("newTask")%>'>Новое Задание&hellip;</a>
                            <img src="/Lms/UI/Img/04/15.png"/>
                        </td>
                    </tr>
                </table>
                <br />
                <br />
                <table style="width:100%">
                    <tr>
                        <td align="center">
                            <asp:Button ID="btnShowMsg" runat="server" Text="Показать" />
                            <asp:DropDownList ID="ddlMsgType" runat="server">
                                <asp:ListItem value="all" selected="True">
                                    Все
                                </asp:ListItem>
                                <asp:ListItem value="letters">
                                    Письма
                                </asp:ListItem>
                                <asp:ListItem value="tasks">
                                    Задания
                                </asp:ListItem>
                                <asp:ListItem value="announcements">
                                    Объявления
                                </asp:ListItem>
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlDirection" runat="server">
                                <asp:ListItem value="all">
                                    Все
                                </asp:ListItem>
                                <asp:ListItem value="incoming" selected="True">
                                    Полученные
                                </asp:ListItem>
                                <asp:ListItem value="sent">
                                    Отправленные
                                </asp:ListItem>
                            </asp:DropDownList>
                            <asp:CheckBox ID="cboxOnlyNew" runat="server" Text=" непрочитанные"/>
                        </td>
                    </tr>
                </table>
                <br />
                <table width="100%">
                    <tr>
                        <td>
                            <asp:GridView ID="gvMailBox" runat="server" PageSize="15"  Width="100%" RowStyle-CssClass="part"
			                    AllowPaging="False" DataKeyNames="ID" AutoGenerateColumns="false" 
                                GridLines="Horizontal">
			                    <EmptyDataTemplate>
			                        <div style="text-align:center">
			                            У вас нет сообщений.
			                        </div>			                
			                    </EmptyDataTemplate>
<RowStyle CssClass="part"></RowStyle>
			                    <Columns>
			                        <asp:TemplateField HeaderText="Тип" ItemStyle-HorizontalAlign="Center">
				                        <ItemTemplate>
				                            <asp:HyperLink ID="hlDeletedItem" runat="server" NavigateUrl='<%#  Eval("RewrittenUrl") %>'>
						                        <asp:Image ID="Image1" runat="server" ImageUrl='<%# Eval("IconUrl") %>' />
					                        </asp:HyperLink>
				                        </ItemTemplate>

<ItemStyle HorizontalAlign="Center"></ItemStyle>
			                        </asp:TemplateField>
			                        <asp:BoundField HeaderText="Отправитель" DataField="From" 
                                        HeaderStyle-HorizontalAlign="Left">
<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    </asp:BoundField>
			                        <asp:BoundField HeaderText="Получатель" DataField="To" 
                                        HeaderStyle-HorizontalAlign="Left">
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
			                        <asp:BoundField HeaderText="Дата" DataField="Created" ItemStyle-Width="20%" 
                                        ItemStyle-HorizontalAlign="Center">
<ItemStyle HorizontalAlign="Center" Width="20%"></ItemStyle>
                                    </asp:BoundField>
			                        <%--<asp:BoundField HeaderText="Прочитано" DataField="isRead" HeaderStyle-HorizontalAlign="Center"/>--%>
			                    </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </div>  
</asp:Content>