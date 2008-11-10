<%@ Control
		Language="c#"
		Inherits="N2.Templates.Web.UI.TemplateUserControl`2[[N2.Templates.Items.AbstractContentPage, N2.Templates], [N2.Lms.Items.Topic, N2.Lms]], N2.Templates" %>
<script runat="server">
	
	protected override void OnInit(EventArgs e)
	{
		base.OnInit(e);

		Register.JQuery(this.Page);
		Register.JavaScript(
			this.Page,
			"~/Lms/UI/Js/n2lms.js",
			N2.Resources.ScriptPosition.Header,
			N2.Resources.ScriptOptions.Include);

		if (this.CurrentItem.ContentLinks.Count() > 1) {
			Register.StyleSheet(this.Page, "~/Lms/UI/Js/jQuery.tabs.css");
			Register.JavaScript(this.Page, "~/Lms/UI/Js/jQuery.tabs.js");
			Register.JavaScript(this.Page, @"
$(function() {
$('div#tabs').tabs({ fxAutoHeight: true });
});
", ScriptOptions.DocumentReady);
		}

	}
</script>

<h3 class="cap3"><%= this.CurrentItem.Title %></h3>

<% var _content = this.CurrentItem.ContentLinks; %>
<% var _displayTabs = _content.Count() > 1; %>
<% var i = 0; %>
<% if (_displayTabs) { %>
<div id="tabs">
	<ul>
	<% foreach(string _url in _content) { %>
		<li><a href="#tab<%= _url.GetHashCode() %>">Page <%= ++i %></a></li>
	<% } %>
	</ul>
<% } %>
	<% foreach(string _url in _content) { %>
	<div id='tab<%= _url.GetHashCode() %>'>
		<iframe	frameborder="no"
				width="100%"
				height="100%"
				align="top"
				src='<%= !string.IsNullOrEmpty(_url)
					? this.ResolveUrl(this.CurrentItem.Course.DiskFolder + "/" + _url)
					: this.ResolveUrl(@"~\") + "NoContent.htm" %>'>
		</iframe>
	</div>
	<% } %>
<% if(_displayTabs) { %>
</div>
<% } %>