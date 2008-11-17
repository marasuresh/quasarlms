<%@ Control
	Language="C#"
	ClassName="CurriculumList"
	Inherits="System.Web.UI.UserControl" %>
<%@ Register
	Src="Curriculum.ascx"
	TagName="Curriculum"
	TagPrefix="lms" %>

<script runat="server">
	#region Container management

	public int? ParentItemId {
		get { return (int?)this.ViewState["ParentItemId"]; }
		protected set { this.ViewState["ParentItemId"] = value; }
	}

	CourseContainer m_container;  //локальная коллекция курсов
	public CourseContainer ParentItem {
		get {
			return m_container ?? (m_container = LoadParentItem());
		}
		set {
			this.m_container = value;

			if (this.m_container != null) {
				this.ParentItemId = this.m_container.ID;
			}
		}
	}

	CourseContainer LoadParentItem()
	{
		return
			this.ParentItemId.HasValue
				? N2.Context.Persister.Get<CourseContainer>(this.ParentItemId.Value)
				: null;
	}

	#endregion Container management

	protected void ds_ObjectCreating(object sender, ObjectDataSourceEventArgs e)
	{
		e.ObjectInstance = this.ParentItem;
	}

	protected void curriculum_Changed(object sender, EventArgs e)
	{
	}

	protected override void OnInit(EventArgs e)
	{
		N2.Resources.Register.StyleSheet(this.Page, "~/Lms/UI/Css/MyAssignmentList.css");
		base.OnInit(e);
	}

	protected void lv_Updating(object sender, ListViewUpdateEventArgs e)
	{
		var _lv = sender as ListView;
		var _curriculum = _lv.Items[e.ItemIndex].FindControl("curriculum") as CurriculumControl;
		
		if (_curriculum.Update()) {
			N2.Context.Persister.Save(this.ParentItem);
			_lv.EditIndex = -1;
		}

		e.Cancel = true;
	}

	protected void lv_Deleting(object sender, ListViewDeleteEventArgs e)
	{
		var _lv = sender as ListView;
		var _curriculum = _lv.Items[e.ItemIndex].FindControl("curriculum") as CurriculumControl;
		
		if (this.ParentItem.DetailCollections.Remove(_curriculum.CurrentCurriculumName)) {
			N2.Context.Persister.Save(this.ParentItem);
			_lv.EditIndex = -1;
		}

		e.Cancel = true;
	}
</script>

<asp:ObjectDataSource ID="ds" runat="server" 
	OldValuesParameterFormatString="original_{0}" 
	onobjectcreating="ds_ObjectCreating"
	SelectMethod="GetCurriculumNames" 
	TypeName="N2.Lms.Items.CourseContainer"></asp:ObjectDataSource>
<n2:ChromeBox runat="server">
<asp:ListView
		ID="lv"
		runat="server"
		DataSourceID="ds"
		OnItemUpdating="lv_Updating"
		OnItemDeleting="lv_Deleting">
	
	<LayoutTemplate>
		<table class="gridview" cellpadding="0" cellspacing="0">
			<tr class="header">
				<th></th>
				<th><asp:Literal
						runat="server"
						Text="Curriculum"
						meta:resourecekey="CurriculumColumn" /></th></tr>
			<tr><td colspan="2"><%# this.Profile.Curriculum %></td></tr>
			<tr id="itemPlaceholder" runat="server" />
		</table>
	</LayoutTemplate>
	
	<ItemTemplate>
		<tr class='<%# Container.DataItemIndex % 2 == 0 ? "row" : "altrow" %>'>
			<td class="command">
				<asp:ImageButton
						runat="server"
						ImageUrl="~/Lms/UI/Img/clear.gif"
						CssClass="LibC_c"
						CommandName="Edit" /></td>
			<td><%# Container.DataItem %></td>
		</tr>
	</ItemTemplate>
	
	<EditItemTemplate>
		<tr class='edit-info <%# Container.DataItemIndex == 0 ? "first" : string.Empty %>'>
			<td class="command">
				<asp:ImageButton
						runat="server"
						ImageUrl="~/Lms/UI/Img/clear.gif"
						CssClass="LibC_o"
						CommandName="Cancel" /></td>
			<td><%# Container.DataItem %></td>
		</tr>
		<tr><td class="edit" colspan="2">
				<div class="details">
					<div class="header">Edit details for '<%# Container.DataItem %>'</div>
					<lms:Curriculum
						ID="curriculum"
						runat="server"
						CourseContainer="<%# this.ParentItem %>"
						CurrentCurriculumName="<%# Container.DataItem %>"
						OnChanged="curriculum_Changed" />
					<div class="footer command">
						
						<asp:LinkButton
								ID="btnSave"
								runat="server"
								meta:resourcekey="btnSave"
								CommandName="Update" />
						
						<asp:LinkButton
								runat="server"
								meta:resourcekey="btnDelete"
								CommandName="Delete" />
					</div>
				</div>
			</td>
		</tr>
	</EditItemTemplate>
</asp:ListView>
</n2:ChromeBox>