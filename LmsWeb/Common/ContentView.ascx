<%@ Control Language="c#" Inherits="DCE.Common.ContentView" CodeFile="ContentView.ascx.cs" %>
<%@ Import Namespace="System.Linq" %>
<script runat="server">
public string makeUrl(string croot, string folder, string path)
{
	string newPath = croot+folder+path;
	return newPath.Replace("\\", "/");
}
</script>
<script type="text/javascript" language="javascript">
$(function() {
$('div#tabs').tabs({ fxAutoHeight: true });
});
</script>

<% if(null != this.Training) { %>
	<h3 class="cap4"><%= this.Training.Title %></h3>
<% } %>
<h3 class="cap3"><%= this.Theme.Title %></h3>

<% var _content = this.Theme.GetDetailCollection("Content", false); %>
<% var _displayTabs = _content.Count > 1; %>
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
					? makeUrl("~/Upload/", this.Training.Course.DiskFolder, _url)
					: this.ResolveUrl(@"~\NoContent.htm") %>'>
		</iframe>
	</div>
	<% } %>
<% if(_displayTabs) { %>
</div>
<% } %>