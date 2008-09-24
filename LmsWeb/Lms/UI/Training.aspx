<%@ Page
	Language="C#"
	MasterPageFile="~/Templates/UI/Layouts/Top+SubMenu.Master"
	Inherits="N2.Templates.Web.UI.TemplatePage`1[[N2.Lms.Items.Training,N2.Lms]],N2.Templates" %>

<script runat="server">
	protected override void OnLoad(EventArgs e)
	{
		base.OnLoad(e);
		N2.Resources.Register.JQuery(this);
		N2.Resources.Register.JavaScript(this, @"
function resizeFrame(frame){
	var _frame = $(frame);
	try{{
		theHeight = frame.contentWindow.document.body.scrollHeight;
		if (theHeight != 0); {{
			document.getElementById(frame).style.height = theHeight+50;
		}}
	}}catch(e){{
		frame.scrolling='auto';
		frame.style.height = 500;
	}}
}}
",			N2.Resources.ScriptPosition.Header,
			N2.Resources.ScriptOptions.ScriptTags);
	}
	</script>

<asp:Content ID="Content1" ContentPlaceHolderID="TextContent" Runat="Server">
	<h3><%= this.CurrentItem.Title %></h3>
	<h4><%= this.CurrentItem.Course.Text %></h4>
	
	<small style="color:Navy">
	<%= this.CurrentItem.StartOn.ToLongDateString() %> / <%= this.CurrentItem.FinishOn.ToLongDateString() %>
	</small>
	

	<iframe	name="contFrame"
			width="100%"
			id="contFrameId"
			onresize="javascript:resizeFrame(this);"
			onload="javascript:resizeFrame(this);"
			frameborder="no"
			height="100%"
			align="top"
			scrolling="no"
			src='<%= string.IsNullOrEmpty(this.CurrentItem.Course.DescriptionUrl)
				? "about:blank"
				: this.ResolveClientUrl(this.CurrentItem.Course.DescriptionUrl) %>'>
		</iframe>
</asp:Content>
