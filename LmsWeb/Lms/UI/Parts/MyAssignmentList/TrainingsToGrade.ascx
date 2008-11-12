<%@ Import Namespace="System.ComponentModel" %>
<%@ Import Namespace="System.Linq" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="N2.Lms.Items" %>
<%@ Import Namespace="N2.Lms.Items.Lms.RequestStates" %>
<%@ Import Namespace="System.Diagnostics" %>
<%@ Import Namespace="N2.Workflow" %>
<%@ Import Namespace="N2.Lms.Items.TrainingWorkflow" %>
<%@ Control Language="C#" Inherits="N2.Lms.Web.UI.MyAssignmentListControl" %>
<%@ Register Assembly="N2.Futures" Namespace="N2.Web.UI.WebControls" TagPrefix="n2" %>

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
            else if (string.Equals(_e.CommandName, "Decline", StringComparison.InvariantCultureIgnoreCase))
            {
                _command = "Decline";
                lv.UpdateItem(int.Parse((string)_e.CommandArgument), true);
            }


        };

        base.OnInit(e);
    }
    
    protected void lv_ItemUpdating(object sender, ListViewUpdateEventArgs e)
    {
        var _lv = sender as ListView;

        e.NewValues.Add("command", _command);
        e.NewValues.Add("comments", ((TextBox)_lv.EditItem.FindControl("tbComment")).Text);
        e.NewValues.Add("grade", ((TextBox)_lv.EditItem.FindControl("tbGrade")).Text);
        e.NewValues.Add("trainingID", ((DropDownList)_lv.EditItem.FindControl("ddlTrainings")).SelectedValue);
    }
</script>

<asp:ObjectDataSource ID="dsRequests" runat="server"
	 SelectMethod="FindRequestsToGrade" UpdateMethod="GoRequest"
    TypeName="N2.Lms.Items.MyAssignmentList" OnObjectCreating="ds_ObjectCreating">
    <UpdateParameters>
        <asp:Parameter Name="command" Type="String" />
        <asp:Parameter Name="comments" Type="String" />
        <asp:Parameter Name="grade" Type="String" />
        <asp:Parameter Name="trainingID" Type="String" />
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
                        Тренинг
                    </th>
                    <th>
                        Студент
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
                    <%# ((Request)Container.DataItem).Course.Title %>
                </td>
                <td>
                    <%# ((Request)Container.DataItem).User %>
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
						CommandName="Cancel" /></td>
                <td>
                    <%# ((Request)Container.DataItem).Course.Title %>
                </td>
                <td>
                    <%# ((Request)Container.DataItem).User %>
                </td>
            </tr>
            <tr>
                <td class="edit" colspan="3">
                    <div class="details">
                        <div class="header">
                            Edit details for '<%# Eval("Title")%>'</div>
                        <table class="detailview" cellpadding="0" cellspacing="0">
                            <tr>
                                <th>
                                    Оценка:
                                </th>
                                <td>
                                    <asp:TextBox runat="server" ID="tbGrade" ValidationGroup='Accept' />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbGrade"
                                        ErrorMessage="*" ValidationGroup='Accept' Display="Dynamic" />
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="tbGrade"
                                        ErrorMessage="*" ValidationGroup='Accept' Display="Dynamic"
                                        Operator="DataTypeCheck" Type="Integer" />
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
                        <div class="footer command">
                            <asp:LinkButton ID="btnSave" runat="server" Text="Оценить" CommandName="Accept" CommandArgument='<%# Container.DataItemIndex %>' ValidationGroup='Accept' />
                        </div>
                        <br />
                        <table class="detailview" cellpadding="0" cellspacing="0">
                            <tr>
                                <th>
                                    Трениниг:
                                </th>
                                <td>
                                    <asp:DropDownList ID="ddlTrainings" runat="server" DataSource='<%# ((Request)Container.DataItem).Course.Trainings %>'
                                        DataValueField="ID" DataTextField="Title">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                        <div class="footer command">
                            <asp:LinkButton ID="LinkButton2" runat="server" Text="Вернуть" CommandName="Decline" CommandArgument='<%# Container.DataItemIndex %>' />
                        </div>
                    </div>
                </td>
            </tr>
        </EditItemTemplate>
    </asp:ListView>
</n2:ChromeBox>


<%--<script runat="server">
    
    protected override void OnInit(EventArgs e)
    {
        MyAssignmentList myAss = this.CurrentItem;

        this.rpt.ItemCommand += (_o, _e) =>
        {

            var _request = N2.Context.Persister.Get<Request>(int.Parse((string)_e.CommandArgument));

            switch (_e.CommandName.ToLower())
            {
                case "grade":
                    TextBox _tb = ((Control)_e.CommandSource).NamingContainer.FindControl("tbGrade") as TextBox;

                    int _grade = int.Parse(_tb.Text);

                    _request.PerformAction(
                        "Accept",
                        Profile.UserName,
                        string.Concat("Graded by ", Context.User.Identity.Name, " for ", _grade),
                        new Dictionary<string, object>{{
							"Grade", _grade
						}});
                    this.BindData(_request.Parent as RequestContainer);
                    break;
                case "replay":
                    _request.PerformAction(
                        "Decline",
                        Profile.UserName,
                        string.Concat("Declined by ", Context.User.Identity.Name),
                        null);
                    this.BindData(_request.Parent as RequestContainer);
                    break;
            }
        };

        base.OnInit(e);
    }

    protected override void OnLoad(EventArgs e)
    {

        base.OnLoad(e);
    }

    void BindData(RequestContainer rc)
    {
        this.rpt.DataSource = rc.MyFinishedAssignments;

        this.rpt.DataBind();

    }
</script>

<asp:Repeater runat="server" ID="rpt">
    <HeaderTemplate>
        <table>
    </HeaderTemplate>
    <FooterTemplate>
        </table></FooterTemplate>
    <ItemTemplate>
        <tr>
            <td>
                <%# Eval("Course.Title") %>
            </td>
            <td>
                <asp:TextBox runat="server" ID="tbGrade" ValidationGroup='<%# "vg" + Eval("ID") %>' />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbGrade"
                    ErrorMessage="*" ValidationGroup='<%# "vg" + Eval("ID") %>' Display="Dynamic" />
                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="tbGrade"
                    ErrorMessage="*" ValidationGroup='<%# "vg" + Eval("ID") %>' Display="Dynamic"
                    Operator="DataTypeCheck" Type="Integer" />
            </td>
            <td>
                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Lms/UI/Img/accept.png"
                    CommandName="Grade" ValidationGroup='<%# "vg" + Eval("ID") %>' CausesValidation="true"
                    CommandArgument='<%# Eval("ID") %>' />
                <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Lms/UI/Img/arrow_undo.png"
                    CommandName="Replay" CommandArgument='<%# Eval("ID") %>' />
            </td>
        </tr>
    </ItemTemplate>
</asp:Repeater>--%>
