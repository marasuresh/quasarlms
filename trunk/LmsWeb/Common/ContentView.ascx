<%@ Control Language="c#" Inherits="DCE.Common.ContentView" CodeFile="ContentView.ascx.cs" %>
<%@ Import Namespace="System.Linq" %>
<script runat="server">
public string makeUrl(string croot, string folder, string path)
{
	string newPath = croot+folder+path;
	return newPath.Replace("\\", "/");
}
</script>

<script language="javascript">
var current=1;
var tcount=1;
function navigateClick(item, count){
	current=parseInt(item);
	tcount = parseInt(count);
	setButtons();
	for (var i=1; i <= tcount ; i++) {
		document.getElementById("contFrameId"+i).style.display= i == current ? "" : "none";
	}
	resizeFrame("contFrameId"+current);
}
function prev(count) {
	if (current > 1) {
		navigateClick(current-1, count);
	}
}
function next(count)
{
	if (current < count) {
		navigateClick(current+1, count);
	}
}
lastfr = "";
function resizeFrame(ctid)
{
	if (document.getElementById(ctid).style.display == "none")
		return;
	if (lastfr != ctid) {
		//setButtons();
		lastfr = ctid;
		try {
			theHeight = document.getElementById("TABLECV").scrollHeight;
			//cf.style.height = theHeight;
			//alert(ctid+" "+theHeight);
		} catch(e){}
		try {
			theHeight = document.getElementById(ctid).contentWindow.document.body.scrollHeight;
			if (theHeight != 0) {
				document.getElementById(ctid).style.height = theHeight+50;
			}
			//alert(ctid+" "+theHeight);
		} catch(e) {
			document.getElementById(ctid).scrolling="auto";
			//theHeight = document.getElementById("TABLECV").scrollHeight;
			document.getElementById(ctid).style.height = 2000;
		}
	}
}

			function setButtons()
			{
			for (var i=1; i <= tcount ; i++)
			{
			currentId = "navigateButton" + i;
			currentButton = document.getElementById(currentId);
			if (currentButton == null) return;
			if (i == current)
			{
			/* Изменение стиля кнопки */
			/* Вместо этого */
			currentButton.style.textDecoration="none";
			/* вставить это */
			currentButton.className="";
			currentButton.style.color = "gray";
			//currentButton.setAttribute("disabled", "true");
			currentButton.style.cursor="auto";
			}
			else
			{
			/* Изменение стиля кнопки */
			/* Вместо этого */
			currentButton.style.textDecoration="underline";
			/* вставить это */
			currentButton.className="";
			currentButton.style.color = "#0000cc";
			currentButton.removeAttribute("disabled");
			currentButton.style.cursor="hand";
			}
			}
			}
</script>

<table height="100%"
			   cellSpacing="0"
			   cellPadding="0"
			   width="100%"
			   align="center"
			   border="0">
			<% if(null != this.Training) { %>
				<tr><td><h3 class="cap4"><%= this.Training.Title %></h3></td></tr>
			<% } %>
			<% if (this.Themes.Any()) { %>
				<tr><td><h3 class="cap3"><%= this.Themes.First().Title %></h3></td></tr>
			<% } %>
			<% if (this.Themes.Count() > 1) { %>
				<tr><td><br/>
						<form id="prevId"
							  name="Navigate"
							  method="post"
							  action="">
							<input style="MARGIN-RIGHT: 3px; MARGIN-LEFT: 3px; BORDER: 0px; CURSOR: hand; COLOR: #0000cc; BACKGROUND-COLOR: transparent;"
								   type="button"
								   value="<<"
								   onclick="javascript:prev(<%= this.Themes.Count() %>)" />
							<% var j = 0; %>
							<% foreach (var _theme in this.Themes) { %>
								<input id="navigateButton<%= j %>"
									   type="button"
									   value="<%= j %>"
									   alt="{alt}"
									   title="{alt}"
									   onclick="javascript:navigateClick(<%= j %>, <%= this.Themes.Count() %>)"
									   style='MARGIN-RIGHT: 3px; MARGIN-LEFT: 3px; BORDER: 0px; CURSOR: auto; COLOR: #0000cc; BACKGROUND-COLOR: transparent; <%= j == 1
									? "COLOR: #808080; TEXT-DECORATION: none;"
									: "COLOR: #0000cc; TEXT-DECORATION: underline; CURSOR: hand;" %>'>
								</input>
							<% j++; %>
							<% } %>
							<input id="nextId"
								   style="MARGIN-RIGHT: 3px; MARGIN-LEFT: 3px; BORDER: 0px; CURSOR: hand; COLOR: #0000cc; BACKGROUND-COLOR: transparent;"
								   type="button"
								   value=">>"
								   onclick="javascript:next(<%= this.Themes.Count() %>)" />
						</form>
						<hr/>
					</td>
				</tr>
			<% } %>
			<tr>
				<td height="100%"
					width="100%"
					valign="top">
					<table height="100%"
						   cellSpacing="0"
						   cellPadding="0"
						   width="100%"
						   align="center"
						   border="0"
						   id="TABLECV">
						<tr valign="top">
							<td><% var i = 0; %>
								<% foreach(var _theme in this.Themes) { %>
									<iframe frameborder="no"
											name="contFrame<%= i %>"
											width="100%"
											height="100%"
											id="contFrameId<%= i %>"
											align="top"
											onload="javascript:resizeFrame('contFrameId<%= i %>');"
											onresize="javascript:resizeFrame('contFrameId<% =i %>');"
											src='<%= !string.IsNullOrEmpty(_theme.ContentUrl)
												? makeUrl(base.CoursesRootUrl, this.Training.Course.DiskFolder, _theme.ContentUrl)
												: this.ResolveUrl(@"~\" + DCE.Service.GetLanguagePath(this.Page)) + "NoContent.htm" %>'
											<% if(i > 1) { %>
											style="display:none"
											<% } %>>
									</iframe>
								<% i++; %>
								<% } %>
							</td>
						</tr>
					</table>
				</td>
			</tr>
		</table>
