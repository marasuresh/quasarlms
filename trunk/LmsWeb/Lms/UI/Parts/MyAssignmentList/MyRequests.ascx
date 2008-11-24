<%@ Import Namespace="System.ComponentModel" %>
<%@ Control Language="C#" Inherits="N2.Lms.Web.UI.MyAssignmentListControl" %>
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

<asp:ObjectDataSource
		ID="dsRequests"
		runat="server"
		SelectMethod="FindPendingRequests"
		UpdateMethod="CancelRequest"
		TypeName="N2.Lms.Items.MyAssignmentList"
		OnObjectCreating="ds_ObjectCreating">
    <UpdateParameters>
        <asp:Parameter Name="UserName" Type="String" />
    </UpdateParameters>
</asp:ObjectDataSource>
    <asp:ListView ID="lv" DataKeyNames="ID" runat="server" DataSourceID="dsRequests"
        OnItemUpdating="lv_ItemUpdating">
        <LayoutTemplate>
            <table class="gridview" cellpadding="0" cellspacing="0">
                <tr class="header">
                    <th></th><th>Курс</th><th>Дата</th></tr>
                    <tr id="itemPlaceholder" runat="server" />
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr class='<%# Container.DataItemIndex % 2 == 0 ? "row" : "altrow" %>'>
                <td class="command">
					<asp:ImageButton
						ID="btnEdit"
						runat="server"
						ImageUrl="~/Lms/UI/Img/clear.gif"
						CssClass="LibC_c"
						AlternateText="детали..."
						CommandName="Edit" />
                </td>
                <td>
                    <%# N2.Web.Link.To(((Request)Container.DataItem).Course).ToString() %>
                </td>
                <td>
                    <small>
                        <%# ((Request)Container.DataItem).Published.Value.ToShortDateString() %></small>
                </td>
            </tr>
        </ItemTemplate>
        <EditItemTemplate>
            <tr class='edit-info <%# Container.DataItemIndex == 0 ? "first" : string.Empty %>'>
                <td class="command">
					<asp:ImageButton
						ID="btnCancel"
						runat="server"
						ImageUrl="~/Lms/UI/Img/clear.gif"
						CssClass="LibC_o"
						CommandName="Cancel" />
                </td>
                <td>
                    <%# (((Request)Container.DataItem).Course).Title %>
                </td>
                <td>
                    <small>
                        <%# ((Request)Container.DataItem).Published.Value.ToShortDateString() %></small>
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
                        <ul class="buttons">
							<li><asp:LinkButton
									ID="btnDelete"
									runat="server"
									meta:resourcekey="btnDelete"
									Text="Убрать заявку"
									CommandName="Update" /></li>
                        </ul>
                    </div>
                </td>
            </tr>
        </EditItemTemplate>
    </asp:ListView>
