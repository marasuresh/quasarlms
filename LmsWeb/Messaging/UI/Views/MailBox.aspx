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

    static string m_folder;
    protected string Folder
    {
        get
        {
            //if (string.IsNullOrEmpty(m_folder))
            //{
            //    m_folder = string.IsNullOrEmpty(Engine.RequestContext.CurrentTemplate.Argument) ? 
            //                    MailBox.C.Folders.Inbox : Engine.RequestContext.CurrentTemplate.Argument.Split('/')[0];
            //}
            string arg = Engine.RequestContext.CurrentPath.Argument;
            string arg_folder = "";
            
            if (string.IsNullOrEmpty(arg))
                m_folder = MailBox.C.Folders.Inbox;
            else
                arg_folder = arg.Split('/')[0];
                if (arg_folder.Equals(MailBox.C.Folders.Drafts, StringComparison.OrdinalIgnoreCase)||
                    arg_folder.Equals(MailBox.C.Folders.Inbox, StringComparison.OrdinalIgnoreCase)||
                    arg_folder.Equals(MailBox.C.Folders.Outbox, StringComparison.OrdinalIgnoreCase)||
                    arg_folder.Equals(MailBox.C.Folders.RecyleBin, StringComparison.OrdinalIgnoreCase))
                    m_folder = arg_folder;

            return m_folder;
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
		new { Filter = MailBox.C.Filter.Letters, Image = "email_open", Title = Resources.MailBox.LettersFilterTitle },
		new { Filter = MailBox.C.Filter.Announcements, Image = "bell", Title = Resources.MailBox.AnnouncementFilterTitle },
		new { Filter = MailBox.C.Filter.Tasks, Image = "wrench", Title = Resources.MailBox.TaskFilterTitle },
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
                    <li><a href='<%= _url.AppendSegment(MailBox.ActionEnum.Create.ToString()) %>'><asp:Literal ID="Literal1" runat="server" Text="New" meta:resourcekey="newMsgResource1"/>
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
                    <asp:LinkButton ID="btnEmptyRecBin" runat="server" Text="Empty" OnClick="btnEmptyRecBin_Click" meta:resourcekey="btnEmptyRecBinResource1"/>
                </li>
            </ul>
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
                            <asp:Literal ID="Literal2" runat="server" Text="Type" meta:resourcekey="headerTypeResource1"/>
                        </th>
                        <th>
                            <asp:Literal ID="Literal3" runat="server" Text="Author" meta:resourcekey="headerAuthorResource1"/>
                        </th>
                        <th>
                            <asp:Literal ID="Literal4" runat="server" Text="Recipient" meta:resourcekey="headerRecipientResource1"/>
                        </th>
                        <th>
                            <asp:Literal ID="Literal5" runat="server" Text="Subject" meta:resourcekey="headerSubjectResource1"/>
                        </th>
                        <th>
                            <asp:Literal ID="Literal6" runat="server" Text="Date" meta:resourcekey="headerDateResource1"/>
                        </th>
                    </tr>
                    <tr id="itemPlaceholder" runat="server" />
                </table>
            </LayoutTemplate>
            <EmptyDataTemplate>
                <tr>
                    <td colspan="5" align="center">
                        <asp:Literal ID="Literal6" runat="server" Text="No messages" meta:resourcekey="noMsgResource1"/>
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
                            <div style="padding: 4px 4px 4px 4px; border: 1px solid #A4D4F0">
                            <table class="detailview" cellpadding="0" cellspacing="0">
                                <tr>
                                    <th>
                                        <asp:Literal ID="Literal6" runat="server" Text="Text:" meta:resourcekey="textResource1"/>
                                    </th>
                                    <td style="width: 90%">
                                        <div style="width:auto; padding: 4px 4px 4px 4px; background-color: #F3F3F3; border: solid 1px Silver">
                                            <%# Eval("Text") %>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <th>
                                        <asp:Literal ID="Literal7" runat="server" Text="Files:" meta:resourcekey="filesResource1"/>
                                    </th>
                                    <td>
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
                            </div>
                            <ul class="buttons">
                                <%
                                    var editIndex  = lv.EditIndex;

                                    MessageTypeEnum msgType = MessageTypeEnum.Letter;
                                    
                                    if (lv.DataKeys[editIndex] != null)
                                    {
                                        var msgID = (int)lv.DataKeys[editIndex].Value;
                                        var curMsg = (Message)N2.Context.Persister.Get(msgID);
                                        msgType = curMsg.MessageType;
                                    }
                                    
                                    if (this.Folder == MailBox.C.Folders.Inbox && msgType == MessageTypeEnum.Letter ||
                                       this.Folder == MailBox.C.Folders.Outbox)
                                   { %>
                                <li><a href='<%# Url.Parse(this.CurrentPage.Url)
                                    .AppendSegment("folder/" + Folder)
									.AppendSegment(MailBox.ActionEnum.Delete.ToString())
									.AppendSegment(Eval("ID").ToString()) %>'><asp:Literal ID="Literal8" runat="server" Text="To RecycleBin" meta:resourcekey="mnuRecBinResource1"/></a></li>
                                <% } %>
                                <% if (this.Folder == MailBox.C.Folders.Drafts)
                                   { %>
                                <li><a href='<%# Url.Parse(this.CurrentPage.Url)
									.AppendSegment("folder/" + Folder)
									.AppendSegment(MailBox.ActionEnum.Destroy.ToString())
									.AppendSegment(Eval("ID").ToString()) %>'><asp:Literal ID="Literal9" runat="server" Text="Delete" meta:resourcekey="mnuDeleteResource1"/></a></li>
							    
							    <li><a href='<%# Url.Parse(this.CurrentPage.Url)
									.AppendSegment(MailBox.ActionEnum.DrCreate.ToString())
									.AppendSegment(Eval("ID").ToString()) %>'><asp:Literal ID="Literal13" runat="server" Text="ReCreate" meta:resourcekey="mnuReCreateResource1"/></a></li>
                                <% } %>
                                <% if (this.Folder == MailBox.C.Folders.RecyleBin)
                                   { %>
                                <li><a href='<%# Url.Parse(this.CurrentPage.Url)
									.AppendSegment("folder/" + Folder)
									.AppendSegment(MailBox.ActionEnum.Restore.ToString())
									.AppendSegment(Eval("ID").ToString()) %>'><asp:Literal ID="Literal10" runat="server" Text="Restore" meta:resourcekey="mnuRestoreResource1"/></a></li>
                                <% } %>
                                <% if (this.Folder == MailBox.C.Folders.Inbox)
                                   { %>
                                <li><a href='<%# Url.Parse(this.CurrentPage.Url)
									.AppendSegment(MailBox.ActionEnum.Reply.ToString())
									.AppendSegment(Eval("ID").ToString()) %>'><asp:Literal ID="Literal11" runat="server" Text="Reply" meta:resourcekey="mnuReplyResource1"/></a></li>
                                <% } %>
                                <% if (this.Folder == MailBox.C.Folders.Outbox ||
                                       this.Folder == MailBox.C.Folders.Inbox)
                                   { %>
                                <li><a href='<%# Url.Parse(this.CurrentPage.Url)
									.AppendSegment(MailBox.ActionEnum.Forward.ToString())
									.AppendSegment(Eval("ID").ToString()) %>'><asp:Literal ID="Literal12" runat="server" Text="Forward" meta:resourcekey="mnuForwardResource1"/></a></li>
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
