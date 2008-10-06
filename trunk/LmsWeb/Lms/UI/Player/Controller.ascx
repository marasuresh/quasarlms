<%@ Control
		Language="C#"
		ClassName="PlayerControl"
		Inherits="N2.Web.UI.ContentUserControl`1[[N2.Lms.Items.TrainingWorkflow.TrainingTicket, N2.Lms]], N2" %>
<%@ Import Namespace="System.Linq" %>

<script runat="server">
	protected void Navigator_Command(object sender, CommandEventArgs e)
	{
	}
</script>
<asp:Panel runat="server" GroupingText="Training Control">
<asp:ImageButton
		runat="server"
		ID="btnStop"
		ImageUrl="~/Lms/UI/Img/03/42.png"
		AlternateText="Stop"
		CommandName="Stop"
		OnCommand="Navigator_Command" />
<asp:ImageButton
		runat="server"
		ID="btnPause"
		ImageUrl="~/Lms/UI/Img/03/41.png"
		AlternateText="Pause" />
<asp:ImageButton ID="btnPlay"
		runat="server"
		ImageUrl="~/Lms/UI/Img/03/31.png"
		AlternateText="Play" />
</asp:Panel>