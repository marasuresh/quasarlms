<%@ Page
		Title=""
		Language="C#"
		Inherits="N2.Templates.Web.UI.TemplatePage`1[[N2.Lms.Items.TrainingList, N2.Lms]], N2.Templates" %>
<%@ Import Namespace="System.Linq" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="N2.Lms.Items" %>
<%@ Import Namespace="System.Diagnostics" %>
<%@ Import Namespace="N2.Workflow" %>

<script runat="server">

	protected IEnumerable<Request> Requests
	{
		get
		{
			return
				this.CurrentItem
					.RequestContainer
					.Children
					.OfType<Request>()
					.Where(_r => _r.GetCurrentState().ToState.Name == "Active");
		}
	}
	
</script>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentAndSidebar" Runat="Server">
</asp:Content>

