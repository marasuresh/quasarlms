<%@ Control Language="C#" AutoEventWireup="true" Inherits="N2.Templates.Web.UI.TemplateUserControl`2[[N2.Templates.Items.AbstractContentPage, N2.Templates], [N2.Lms.Items.MyAssignmentList, N2.Lms]], N2.Templates" %>
<%@ Register TagPrefix="lms" TagName="MyCourses" Src="~/Lms/UI/Parts/MyAssignmentList/MyCourses.ascx" %>
<%@ Register TagPrefix="lms" TagName="MyRequests" Src="~/Lms/UI/Parts/MyAssignmentList/MyRequests.ascx" %>
<%@ Register TagPrefix="lms" TagName="Requests" Src="~/Lms/UI/Parts/MyAssignmentList/Requests.ascx" %>
<%@ Register TagPrefix="lms" TagName="MyTrainings" Src="~/Lms/UI/Parts/MyAssignmentList/MyTrainings.ascx" %>
<%@ Register TagPrefix="lms" TagName="TrainingsToGrade" Src="~/Lms/UI/Parts/MyAssignmentList/TrainingsToGrade.ascx" %>
<%@ Register TagPrefix="lms" TagName="MyGradedTrainings" Src="~/Lms/UI/Parts/MyAssignmentList/MyGradedTrainings.ascx" %>

<script runat="server">
    
	protected override void CreateChildControls()
	{
		base.CreateChildControls();
		this.CreateControlHierarchy();
		this.ClearChildViewState();
	}
	//TODO move to common library
	static bool IsSelected(WizardStep step)
	{
		return step == step.Wizard.ActiveStep;
	}
	
	protected void CreateControlHierarchy()
	{
		var _adminQuerySet = new Dictionary<string, Type> {
			{ Resources.MyAssignmentList.MyRequestsTitle, typeof(ASP.lms_ui_parts_myassignmentlist_requests_ascx) },
			{ Resources.MyAssignmentList.MyGradesTitle, typeof(ASP.lms_ui_parts_myassignmentlist_trainingstograde_ascx) },
		};

		var _studentQuerySet = new Dictionary<string, Type> {
			{ "Предметы", typeof(ASP.MyCourses) },
			{ "Заявки", typeof(ASP.lms_ui_parts_myassignmentlist_myrequests_ascx) },
			{ "Тренинги", typeof(ASP.MyTrainings) },
			{ "Оценки", typeof(ASP.lms_ui_parts_myassignmentlist_mygradedtrainings_ascx) },
		};

		var _querySet =
			!this.Context.User.IsInRole("Administrators")
				? _studentQuerySet
				: _adminQuerySet;
		
		foreach (var _t in _querySet) {
			WizardStep _ws = new WizardStep {
					ID = _t.Value.AssemblyQualifiedName,
					StepType = WizardStepType.Auto,
					Title = _t.Key,
			};
			var _container = new N2.Web.UI.WebControls.ChromeBox();
			_container.Controls.Add(this.LoadControl(_t.Value, new object[0]));
			_ws.Controls.Add(_container);

			this.wz.WizardSteps.Add(_ws);
		}
	}
	
	protected override void OnInit(EventArgs e)
    {
		Register.StyleSheet(this.Page, "~/Lms/UI/Css/MyAssignmentList.css");
		
		base.OnInit(e);

		this.EnsureChildControls();
		this.wz.ActiveStepIndex = 0;
    }
</script>
<span class="assignment-list">
	<asp:Wizard
			runat="server"
			ID="wz"
			Font-Names="Verdana"
			Font-Size="0.8em"
			BackColor="#ffffff"
			Height="300"
			Width="100%">
        <NavigationStyle Height="0" CssClass="hidden" />
        <StepStyle Font-Size="0.8em" ForeColor="#333333" VerticalAlign="Top" />
        <StepStyle BackColor="White" CssClass="wzs" />
        <SideBarStyle VerticalAlign="Top" Width="100" />
        <SideBarTemplate>
			<asp:DataList
					runat="server"
					ID="SideBarList"
					Width="100%"
					AdapterEnabled="true"
					SelectedItemStyle-CssClass="selected"
					CssClass="sb">
                <ItemTemplate>
					<%# IsSelected((WizardStep)Container.DataItem) ? "<span class='selected'>" : string.Empty %>
                    <asp:LinkButton runat="server" ID="SideBarButton" CssClass='sba' />
                    <%# IsSelected((WizardStep)Container.DataItem) ? "</span>" : string.Empty %>
                </ItemTemplate>
            </asp:DataList>
        </SideBarTemplate>
    </asp:Wizard>
</span>