<%@ Control
		Language="c#"
		Inherits="Topic"
		CodeFile="Topic.ascx.cs" %>
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

<% if(null != this.CurrentItem.Training) { %>
	<h3 class="cap4"><%= ((N2.ContentItem)this.CurrentItem.Training ?? this.CurrentItem.Course).Title %></h3>
<% } %>
<h3 class="cap3"><%= this.CurrentItem.Title %></h3>

<% var _content = this.CurrentItem.GetDetailCollection("Content", false); %>
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
					? this.ResolveUrl(this.CurrentItem.Course.DiskFolder + "/" + _url)
					: this.ResolveUrl(@"~\") + "NoContent.htm" %>'>
		</iframe>
	</div>
	<% } %>
<% if(_displayTabs) { %>
</div>
<% } %>