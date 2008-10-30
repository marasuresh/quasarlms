<%@ Import Namespace="System.ComponentModel" %>
<%@ Control Language="C#" AutoEventWireup="true" Inherits="N2.Lms.Web.UI.MyAssignmentListControl`1[[N2.Lms.MyRequestsDAO, LmsWeb]], N2.Lms" %>
<%@ Import Namespace="System.Linq" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="N2.Lms.Items" %>
<%@ Import Namespace="System.Diagnostics" %>
<%@ Import Namespace="N2.Workflow" %>
<%@ Register Assembly="N2.Futures" Namespace="N2.Web.UI.WebControls" TagPrefix="n2" %>

<script runat="server">
    protected void lv_ItemUpdating(object sender, ListViewUpdateEventArgs e)
    {
        var _lv = sender as ListView;

        e.NewValues.Add("UserName", Profile.UserName);
    }
</script>

<asp:ObjectDataSource ID="dsRequests" runat="server" SelectMethod="FindAll" UpdateMethod="CancelRequest"
    TypeName="N2.Lms.MyRequestsDAO" OnObjectCreating="ds_ObjectCreating">
    <UpdateParameters>
        <asp:Parameter Name="UserName" Type="String" />
    </UpdateParameters>
</asp:ObjectDataSource>
<n2:ChromeBox ID="ChromeBox1" runat="Server">
    <asp:ListView ID="lv" DataKeyNames="ID" runat="server" DataSourceID="dsRequests"
        OnItemUpdating="lv_ItemUpdating">
        <LayoutTemplate>
            <table class="gridview" cellpadding="0" cellspacing="0">
                <tr class="header">
                    <th>
                    </th>
                    <th>
                        Студент
                    </th>
                    <th>
                        Наименование курса
                    </th>
                    <tr id="itemPlaceholder" runat="server" />
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr class='<%# Container.DataItemIndex % 2 == 0 ? "row" : "altrow" %>'>
                <td class="command">
                    <asp:LinkButton ID="btnEdit" runat="server" Text="детали..." CommandName="Edit" />
                </td>
                <td>
                    <%# ((Request)Container.DataItem).User %>
                </td>
                <td>
                    <%# N2.Web.Link.To(((Request)Container.DataItem).Course).ToString() %>
                </td>
            </tr>
        </ItemTemplate>
        <EditItemTemplate>
            <tr class='edit-info <%# Container.DataItemIndex == 0 ? "first" : string.Empty %>'>
                <td class="command">
                    <asp:LinkButton ID="btnEdit" runat="server" Text="Отмена" CommandName="Cancel" />
                </td>
                <td>
                    <%# ((Request)Container.DataItem).User %>
                </td>
                <td>
                    <%# (((Request)Container.DataItem).Course).Title %>
                </td>
            </tr>
            <tr>
                <td class="edit" colspan="3">
                    <div class="details">
                        <div class="header">
                            Edit details for '<%# Eval("Course.DescriptionUrl") %>'</div>
                        <table class="detailview" cellpadding="0" cellspacing="0">
                            <tr>
                                <th>
                                    Комментарий:
                                </th>
                                <td>
                                    <em>
                                        <%# Eval("Comments") %></em>
                                </td>
                            </tr>
                        </table>
                        <div class="footer command">
                            <asp:LinkButton ID="btnReject" runat="server" Text="Отклонить" CommandName="Update" CommandArgument="Reject" />
                            <asp:LinkButton ID="btnAccept" runat="server" Text="Принять" CommandName="Update" CommandArgument="Accept" />
                        </div>
                    </div>
                </td>
            </tr>
        </EditItemTemplate>
    </asp:ListView>
</n2:ChromeBox>
