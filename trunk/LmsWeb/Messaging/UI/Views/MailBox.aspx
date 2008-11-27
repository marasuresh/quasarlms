<%@ Import Namespace="System.ComponentModel" %>

<%@ Page Language="C#" MasterPageFile="~/Templates/UI/Layouts/Top+SubMenu.master"
    AutoEventWireup="true" CodeBehind="MailBox.aspx.cs" Inherits="Messaging_UI_MailBox" %>

<script runat="server">
    private static string GetAttachmentName(string attachFileUrl)
    {
        if (attachFileUrl != null)
        {
            int index = attachFileUrl.IndexOf("$") + 1;
            return attachFileUrl.Remove(0, index);
        }
        else
            return string.Empty;
    }

    string m_folder;
    protected string Folder
    {
        get
        {
            if (string.IsNullOrEmpty(this.m_folder))
            {
                this.m_folder = this.Engine.RequestContext.CurrentTemplate.Argument.Split('/')[0];
            }

            if (string.IsNullOrEmpty(this.m_folder))
            {
                this.m_folder = MailBox.C.Folders.Inbox;
            }

            return this.m_folder;
        }
    }
    
</script>

<%@ Import Namespace="System.Linq" %>
<%@ Import Namespace="N2.Web" %>
<%@ Import Namespace="N2.Messaging" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TextContent" runat="Server">
    <asp:MultiView ID="mvMailBox" runat="server" ActiveViewIndex="0">
        <asp:View ID="Tab1" runat="server">
            <div style="width: 100%;">
                <br />
                <%	var _url = Url.Parse(this.CurrentPage.Url);
                    var _filterLinks = new[] {
		new { Filter = MailBox.C.Filter.Letters, Image = "email_open", Title = "Письма" },
		new { Filter = MailBox.C.Filter.Announcements, Image = "bell", Title = "Объявления" },
		new { Filter = MailBox.C.Filter.Tasks, Image = "wrench", Title = "Задания" },
	};
                    foreach (var _link in _filterLinks)
                    { %>
                <a href='<%= _url.AppendSegment("folder")
					.AppendSegment(this.Folder)
					.AppendSegment("filter")
					.AppendSegment(_link.Filter) %>'>
                    <img src='<%= this.ResolveClientUrl("~/Messaging/UI/Images/" + _link.Image + ".png") %>' />
                    <%= _link.Title %>
                </a>
                <%
                    }
                %>
                <ul class="buttons">
                    <li><a href='<%= _url.AppendSegment(MailBox.ActionEnum.Create.ToString()) %>'>Новое
                        &hellip;</a></li>
                </ul>
                <br />
            </div>
        </asp:View>
        <asp:View ID="Tab2" runat="server">
            <br />
            <br />
        </asp:View>
        <asp:View ID="Tab3" runat="server">
            <br />
            <ul class="buttons">
                <li>
                    <asp:LinkButton ID="btnEmptyRecBin" runat="server" Text="Очистить" OnClick="btnEmptyRecBin_Click" /></li></div>
                <br />
        </asp:View>
    </asp:MultiView>
    <n2:ChromeBox runat="server">
        <asp:ObjectDataSource runat="server" ID="ds" OldValuesParameterFormatString="original_{0}"
            SelectMethod="GetFilteredFolderMessages" SelectCountMethod="TotalNumberOfMessage"
            TypeName="N2.Messaging.MailBox" OnObjectCreating="ds_ObjectCreating" EnablePaging="True">
        </asp:ObjectDataSource>
        <asp:ListView ID="lv" DataKeyNames="ID" runat="server" DataSourceID="ds">
            <LayoutTemplate>
                <table class="gridview" cellpadding="0" cellspacing="0">
                    <col align="center" />
                    <col align="left" />
                    <col align="left" />
                    <col align="left" width="50%" />
                    <col align="center" width="20%" />
                    <tr class="header">
                        <th>
                            Тип
                        </th>
                        <th>
                            Автор
                        </th>
                        <th>
                            Получатель
                        </th>
                        <th>
                            Тема
                        </th>
                        <th>
                            Дата
                        </th>
                    </tr>
                    <tr id="itemPlaceholder" runat="server" />
                </table>
            </LayoutTemplate>
            <EmptyDataTemplate>
                <tr>
                    <td colspan="5" align="center">
                        У вас нет сообщений.
                    </td>
                </tr>
            </EmptyDataTemplate>
            <ItemTemplate>
                <tr class='<%# Container.DataItemIndex % 2 == 0 ? "row" : "altrow" %>  <%# ((Message)Container.DataItem).IsRead ? "" : "NotReadMsg" %>'>
                    <td>
                        <asp:LinkButton runat="server" CommandName="Edit">
							<asp:Image runat="server" ImageUrl='<%# Eval("IconUrl") %>' />
                        </asp:LinkButton>
                    </td>
                    <td>
                        <%# Eval("From") %>
                    </td>
                    <td>
                        <%# Eval("To") %>
                    </td>
                    <td>
                        <asp:LinkButton runat="server" CommandName="Edit" Text='<%# Eval("Subject")%>' />
                    </td>
                    <td>
                        <small>
                            <%# Eval("Created") %></small>
                    </td>
                </tr>
            </ItemTemplate>
            <EditItemTemplate>
                <tr class='edit-info <%# Container.DataItemIndex == 0 ? "first" : string.Empty %>'>
                    <td>
                        <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Cancel">
                            <asp:Image ID="Image1" runat="server" ImageUrl='<%# Eval("IconUrl") %>' />
                        </asp:LinkButton>
                    </td>
                    <td>
                        <%# Eval("From") %>
                    </td>
                    <td>
                        <%# Eval("To") %>
                    </td>
                    <td>
                        <asp:LinkButton runat="server" CommandName="Cancel" Text='<%# Eval("Subject")%>' />
                    </td>
                    <td>
                        <small>
                            <%# Eval("Created") %></small>
                    </td>
                </tr>
                <tr>
                    <td class="edit" colspan="5">
                        <div class="details">
                            <div class="header">
                                Edit details for '<%# Eval("Title")%>'
                            </div>
                            <table class="detailview" cellpadding="0" cellspacing="0">
                                <tr>
                                    <th>
                                        Текст:
                                    </th>
                                    <td style="width: 85%">
                                        <div style="width: 100%; height: 100%; background-color: Gainsboro; border: solid 1px Silver">
                                            <%# Eval("Text") %>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Repeater ID="rAttach" runat="server" DataSource='<%# ((Message)Container.DataItem).Attachments %>'>
                                            <HeaderTemplate>
                                                <br />
                                                <table>
                                            </HeaderTemplate>
                                            <FooterTemplate>
                                                </table></FooterTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <img src='<%= this.ResolveClientUrl("~/Messaging/UI/Images/attach.png") %>' />
                                                        <asp:HyperLink ID="hlAttach" runat="server" NavigateUrl='<%# (string)Container.DataItem %>'><%# GetAttachmentName(((string)Container.DataItem)) %></asp:HyperLink>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </td>
                                </tr>
                            </table>
                            <ul class="buttons">
                                <% if (this.Folder == MailBox.C.Folders.Inbox ||
                                       this.Folder == MailBox.C.Folders.Outbox)
                                   { %>
                                <li><a href='<%# Url.Parse(this.CurrentPage.Url)
									.AppendSegment(MailBox.ActionEnum.Delete.ToString())
									.AppendSegment(Eval("ID").ToString()) %>'>В корзину</a></li>
                                <% } %>
                                <% if (this.Folder == MailBox.C.Folders.Drafts)
                                   { %>
                                <li><a href='<%# Url.Parse(this.CurrentPage.Url)
									.AppendSegment(MailBox.ActionEnum.Destroy.ToString())
									.AppendSegment(Eval("ID").ToString()) %>'>Удалить</a></li>
                                <% } %>
                                <% if (this.Folder == MailBox.C.Folders.RecyleBin)
                                   { %>
                                <li><a href='<%# Url.Parse(this.CurrentPage.Url)
									.AppendSegment(MailBox.ActionEnum.Restore.ToString())
									.AppendSegment(Eval("ID").ToString()) %>'>Восстановить</a></li>
                                <% } %>
                                <% if (this.Folder == MailBox.C.Folders.Inbox)
                                   { %>
                                <li><a href='<%# Url.Parse(this.CurrentPage.Url)
									.AppendSegment(MailBox.ActionEnum.Reply.ToString())
									.AppendSegment(Eval("ID").ToString()) %>'>Ответить</a></li>
                                <% } %>
                                <% if (this.Folder == MailBox.C.Folders.Outbox ||
                                       this.Folder == MailBox.C.Folders.Inbox ||
                                       this.Folder == MailBox.C.Folders.Drafts)
                                   { %>
                                <li><a href='<%# Url.Parse(this.CurrentPage.Url)
									.AppendSegment(MailBox.ActionEnum.Forward.ToString())
									.AppendSegment(Eval("ID").ToString()) %>'>Переслать</a></li>
                                <% } %>
                            </ul>
                        </div>
                    </td>
                </tr>
            </EditItemTemplate>
        </asp:ListView>
        <div style="text-align: center">
            <asp:DataPager ID="DataPager1" PagedControlID="lv" PageSize="15" runat="server">
                <Fields>
                    <asp:NumericPagerField />
                </Fields>
            </asp:DataPager>
        </div>
    </n2:ChromeBox>
</asp:Content>
