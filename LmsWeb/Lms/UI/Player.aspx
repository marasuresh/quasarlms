<%@ Page
		Title=""
		Language="C#"
		EnableViewState="true"
		Inherits="N2.Templates.Web.UI.TemplatePage`1[[N2.Lms.Items.TrainingWorkflow.TrainingTicket, N2.Lms]], N2.Templates" %>
<%@ Register Src="~/Lms/UI/Player/Player.ascx" TagName="Content" TagPrefix="player" %>
<%@ Import Namespace="System.Web.UI.WebControls" %>
<%@ Import Namespace="N2.Lms.Items" %>
<%@ Import Namespace="N2.Lms.Items.TrainingWorkflow" %>
<script runat="server">
	protected override void OnInit(EventArgs e)
	{
		int _ticketId;
		TrainingTicket _ticket;
		
		string _attemptId = this.Request["id"];
		
		var _uriTemplateData = Engine.RequestContext.CurrentTemplate as UriTemplateData;
		
		if (string.IsNullOrEmpty(_attemptId)
				&& null != _uriTemplateData) {
			_attemptId = _uriTemplateData.Match.BoundVariables["attempt"];
		}
		
		if (int.TryParse(_attemptId, out _ticketId)
				&& null != (_ticket = N2.Context.Current.Persister.Get<TrainingTicket>(_ticketId))) {
			if (_ticket.Parent.Parent.IsAuthorized(this.Context.User)
					|| this.Engine.SecurityManager.IsAdmin(this.Context.User)
					|| this.Engine.SecurityManager.IsEditor(this.Context.User)) {
				this.InjectCurrentItem(_ticket);
				
				TrainingPlayer _player = (TrainingPlayer)this.LoadControl("~/Lms/UI/Player/Player.ascx");
				_player.CurrentItem = this.CurrentItem;
				this.phPlayer.Controls.Add(_player);
				
				base.OnInit(e);
			} else {
				Response.StatusCode = 403;
				Response.End();
			}
		} else {
			Response.StatusCode = 404;
			Response.End();
		}
	}

	void InjectCurrentItem(TrainingTicket ticket)
	{
		this.CurrentPage = ticket;
	}
</script>

<asp:Content runat="server" ContentPlaceHolderID="Top">
	<div id="Header">
	<%--<n2:EditableDisplay runat="server" PropertyName="Title" />--%>
	</div>
</asp:Content>

<asp:Content
		ID="Content2"
		ContentPlaceHolderID="PageWrapper"
		Runat="Server">
	<asp:PlaceHolder runat="Server" ID="phPlayer" />
</asp:Content>