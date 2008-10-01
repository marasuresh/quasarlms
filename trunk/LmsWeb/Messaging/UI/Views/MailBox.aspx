<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MailBox.aspx.cs" Inherits="Messaging_UI_MailBox" %>
<%@ Import Namespace="System.Linq" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TextContent" Runat="Server">
	<%--<br />
	<b>Сообщения</b>
	<% if (Messages != null && Messages.Any())
    { %>
    <table border="1">
        <tr><th>От кого</th><th>Кому</th><th>Тема</th><th>Создано</th></tr>
	<% foreach (var _msg in this.Messages)
    { %>
        <tr>
            <td><%= _msg.From %></td>
            <td><%= _msg.To %></td>
            <td><a href='<%= _msg.Url %>'><%= _msg.Subject %></a></td>
            <td><%= string.Format("{0}, {1}",_msg.Created.ToShortDateString(),_msg.Created.ToLongTimeString()) %></td>
        </tr>
	<% } %>
	</table>
	<% }
    else
    { %>
    <br />
    сообщений нет
	<% } %>
	<br />
	<br />
	<b>Письма</b>
	<% if (this.Letters.Any())
    { %>
    <table border="1">
        <tr><th>От кого</th><th>Кому</th><th>Тема</th><th>Создано</th></tr>
	<% foreach (var _msg in this.Letters)
    { %>
        <tr>
            <td><%= _msg.From %></td>
            <td><%= _msg.To %></td>
            <td><a href='<%= _msg.Url %>'><%= _msg.Subject %></a></td>
            <td><%= string.Format("{0}, {1}",_msg.Created.ToShortDateString(),_msg.Created.ToLongTimeString()) %></td>
        </tr>
	<% } %>
	</table>
	<% }
    else
    { %>
    <br />
    писем нет
	<% } %>
	<br />
	<b>Задания</b>
	<% if (this.Tasks.Any())
    { %>
    <table border="1">
        <tr><th>От кого</th><th>Кому</th><th>Тема</th><th>Создано</th></tr>
	<% foreach (var _msg in this.Tasks)
    { %>
        <tr>
            <td><%= _msg.From %></td>
            <td><%= _msg.To %></td>
            <td><a href='<%= _msg.Url %>'><%= _msg.Subject %></a></td>
            <td><%= string.Format("{0}, {1}",_msg.Created.ToShortDateString(),_msg.Created.ToLongTimeString()) %></td>
        </tr>
	<% } %>
	</table>
	<% }
    else
    { %>
    <br />
    заданий нет
	<% } %>
	<br />
	<b>Объявления</b>
	<% if (this.Announcement.Any())
    { %>
    <table border="1">
        <tr><th>От кого</th><th>Кому</th><th>Тема</th><th>Создано</th></tr>
	<% foreach (var _msg in this.Announcement)
    { %>
        <tr>
            <td><%= _msg.From %></td>
            <td><%= _msg.To %></td>
            <td><a href='<%= _msg.Url %>'><%= _msg.Subject %></a></td>
            <td><%= string.Format("{0}, {1}",_msg.Created.ToShortDateString(),_msg.Created.ToLongTimeString()) %></td>
        </tr>
	<% } %>
	</table>
	<% }
    else
    { %>
    <br />
    объявлений нет
	<% } %>
	<br />
	<br />
	<b>Корзина</b>
	<% if (this.inRecycleBin.Any())
    { %>
    <table border="1">
        <tr><th>От кого</th><th>Кому</th><th>Тема</th><th>Создано</th></tr>
	<% foreach (var _msg in this.inRecycleBin)
    { %>
        <tr>
            <td><%= _msg.From %></td>
            <td><%= _msg.To %></td>
            <td><a href='<%= _msg.Url %>'><%= _msg.Subject %></a></td>
            <td><%= string.Format("{0}, {1}",_msg.Created.ToShortDateString(),_msg.Created.ToLongTimeString()) %></td>
        </tr>
	<% } %>
	</table>
	<% }
    else
    { %>
    <br />
    Ваша корзина пуста
	<% } %>--%>
    <br />
    <div style="width:100%">
        <table>
            <tr>
                <td align="left">
                    <asp:Button ID="btnNewMsg" runat="server" Text="Новое" />
                    <asp:DropDownList ID="ddlNewMsgByType" runat="server">
                        <asp:ListItem value="letter" selected="True">
                            письмо
                        </asp:ListItem>
                        <asp:ListItem value="task">
                            задание
                        </asp:ListItem>
                        <asp:ListItem value="annoncement">
                            объявление
                        </asp:ListItem>
                     </asp:DropDownList>
                </td>
            </tr>
        </table>
        <br />
        <br />
        <table style="width:100%">
            <tr>
                <td align="right">
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
                    <asp:GridView ID="gvMailBox" runat="server" PageSize="15"  Width="100%"
			            AllowPaging="False" DataKeyNames="ID" AutoGenerateColumns="false">
			            <EmptyDataTemplate>
			                <div style="text-align:center">
			                    У вас нет сообщений.
			                </div>			                
			            </EmptyDataTemplate>
			            <Columns>
			                <asp:TemplateField HeaderText="Тип" ItemStyle-HorizontalAlign="Center">
				                <ItemTemplate>
				                    <asp:HyperLink ID="hlDeletedItem" runat="server" NavigateUrl='<%# Eval("RewrittenUrl") %>'>
						                <asp:Image ID="Image1" runat="server" ImageUrl='<%# Eval("IconUrl") %>' />
					                </asp:HyperLink>
				                </ItemTemplate>
			                </asp:TemplateField>
			                <asp:BoundField HeaderText="Отправитель" DataField="From" HeaderStyle-HorizontalAlign="Left"/>
			                <asp:BoundField HeaderText="Получатель" DataField="To" HeaderStyle-HorizontalAlign="Left"/>
			                <%--<asp:BoundField HeaderText="Тема" DataField="Subject" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="50%"/>--%>
			                <asp:TemplateField HeaderText="Тема" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="50%">
				                <ItemTemplate>
					                <asp:HyperLink ID="hlMsgLocation" runat="server" NavigateUrl='<%# Eval("RewrittenUrl") %>'>
						                <%# Eval("Subject")%>
					                </asp:HyperLink>
				                </ItemTemplate>
			                </asp:TemplateField>
			                <asp:BoundField HeaderText="Дата" DataField="Created" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Center"/>
			                <%--<asp:BoundField HeaderText="Прочитано" DataField="isRead" HeaderStyle-HorizontalAlign="Center"/>--%>
			            </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>