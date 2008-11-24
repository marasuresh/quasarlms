<%@ Import Namespace="N2.Lms.Items.Lms.RequestStates"%>
<%@ Import Namespace="System.ComponentModel" %>
<%@ Control Language="C#" Inherits="N2.Lms.Web.UI.MyAssignmentListControl" %>
<%@ Import Namespace="System.Linq" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="N2.Lms.Items" %>
<%@ Import Namespace="System.Diagnostics" %>
<%@ Import Namespace="N2.Workflow" %>

<script runat="server">

    private string _command;
    
    protected override void OnInit(EventArgs e)
    {
        this.lv.ItemCommand += (_o, _e) =>
        {
            if (string.Equals(_e.CommandName, "Accept", StringComparison.InvariantCultureIgnoreCase))
            {
                _command = "Accept";
                lv.UpdateItem(int.Parse((string)_e.CommandArgument), true);
            }
            else if (string.Equals(_e.CommandName, "Reject", StringComparison.InvariantCultureIgnoreCase))
            {
                _command = "Reject";
                lv.UpdateItem(int.Parse((string)_e.CommandArgument), true);
            }

            
        };
        
        base.OnInit(e);
    }
    
    protected void lv_ItemUpdating(object sender, ListViewUpdateEventArgs e)
    {
        var _lv = sender as ListView;

        e.NewValues.Add("command", _command);
        e.NewValues.Add("trainingID", ((DropDownList)_lv.EditItem.FindControl("ddlTrainings")).SelectedValue);
        e.NewValues.Add("comments", ((TextBox)_lv.EditItem.FindControl("tbComment")).Text);
    }
</script>

<asp:ObjectDataSource ID="dsRequests" runat="server" 
    SelectMethod="FindRequestsToApprove" 
    UpdateMethod="GoRequest"
    TypeName="N2.Lms.Items.MyAssignmentList" 
    OnObjectCreating="ds_ObjectCreating">
    <UpdateParameters>
        <asp:Parameter Name="command" Type="String" />
        <asp:Parameter Name="trainingID" Type="String" />
        <asp:Parameter Name="comments" Type="String" />
    </UpdateParameters>
</asp:ObjectDataSource>
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
					<asp:ImageButton
						ID="ImageButton1"
						runat="server"
						ImageUrl="~/Lms/UI/Img/clear.gif"
						CssClass="LibC_c"
						AlternateText="детали..."
						CommandName="Edit" /></td>
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
                    <asp:ImageButton
						ID="btnCancel"
						runat="server"
						ImageUrl="~/Lms/UI/Img/clear.gif"
						CssClass="LibC_o"
						CommandName="Cancel" />
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
                                <td>
                                    Комментарий студента:
                                </td>
                                <td>
                                    <em>
                                        <%# Eval("Comments") %></em>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Дата начала:
                                </td>
                                <td>
                                    <small>
                                        <%# ((Request)Container.DataItem).StartDate.ToShortDateString() %></small>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Дата окончания:
                                </td>
                                <td>
                                    <small>
                                        <%# ((Request)Container.DataItem).RequestDate.ToShortDateString() %></small>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <table class="detailview" cellpadding="0" cellspacing="0">
                            <tr>
                                <th>
                                    Тренинг:
                                </th>
                                <td>
                                    <asp:DropDownList ID="ddlTrainings" runat="server" DataSource='<%# ((Request)Container.DataItem).Course.Trainings %>'
                                        DataValueField="ID" DataTextField="Title">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    Комментарий:
                                </th>
                                <td>
                                    <asp:TextBox ID="tbComment" TextMode="MultiLine" runat="server" />
                                </td>
                            </tr>
                        </table>
                        <ul class="buttons">
                            <li><asp:LinkButton ID="btnReject" runat="server" Text="Отклонить" CommandName="Reject" CommandArgument='<%# Container.DataItemIndex %>' /></li>
                            <li><asp:LinkButton ID="btnAccept" runat="server" Text="Принять" CommandName="Accept" CommandArgument='<%# Container.DataItemIndex %>' /></li>
                        </ul>
                    </div>
                </td>
            </tr>
        </EditItemTemplate>
    </asp:ListView>
