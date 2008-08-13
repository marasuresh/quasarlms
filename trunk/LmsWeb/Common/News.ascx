<%@ Control Language="c#" Inherits="DCE.Common.News" CodeFile="News.ascx.cs" %>
<script language="javascript">
  function HidePanel(imdID,panelId)
	{
		var floatingPanel = window.document.getElementById(panelId);
		var fpPicture = window.document.getElementById(imdID.toString());
		var _expand = floatingPanel.style.display=='none';
		floatingPanel.style.display = _expand ? 'block' : 'none';
		fpPicture.src = window.document.getElementById(_expand ? "hiddenImgMinus" : "hiddenImgPlus").src;
	}
	
	function ShowHandCursor(pictureId)
	{
		window.document.getElementById(pictureId).style.cursor = 'hand';
	}
 function CouseRef(code)
	{
		
	if (code != "")
	AddParameter("code", code);
	AddParameter("cset", "CourseIntro");

	applyParameters();
	/*document.CourseDescr.action = __location;
	document.CourseDescr.submit();*/
	}
	function viewHref(href)
	{
	document.CourseDescr.newsUrl.value = href;
	AddParameter("cset", "PageView");
	applyParameters();
	}
</script>

	<img id="hiddenImgMinus" src="App_Themes/Default/images/minus.gif" style="display:none" />
	<img id="hiddenImgPlus" src="App_Themes/Default/images/plus.gif" style="display:none" />

<table cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr vAlign="top">
		<td		width="137"
				height="66"
				valign="middle"
				style="PADDING-LEFT: 5px;">
			<asp:Image
					runat="server"
					ID="imgTopMid"
					Height="56"
					AlternateText=""
					ImageUrl='<%$ Resources: MainMenu, Top_mid %>'
					Width="300"
					border="0" /></td>
		<td		align="right"
				width="100%"
				height="66">
			<asp:Image
					runat="server"
					ID="imgTopMid2"
					SkinID="TopMid2" /></td>
 <!-- / login --></tr>
 <tr>
 <td	width="100%"
		background="App_Themes/Default/images/bgr_line2.gif"
		colSpan="2"><IMG id="IMG1"
			runat="server"
			height="29"
			alt=""
			src="~/App_Themes/Default/images/empty.gif"
			width="1"
			border="0"/></td>
 </tr>
 </table>
 
<table border="0" cellpadding="2" width="100%" valign="top">
	<tr><td><img	src="App_Themes/Default/images/bullet.gif"
					width="14"
					height="14"
					border="0"
					alt="" />&nbsp;&nbsp;&nbsp;
			<strong><b><%# SelectScalar("//News") %></b></strong>	
		</td>
	</tr>
	<tr><td valign="top">
			<asp:Repeater
					runat="server"
					ID="rptNews"
					DataSource='<%# doc.SelectNodes("//Items/item") %>'>
				<ItemTemplate>
					<img	id='fpPicture<%# SelectScalar(Container.DataItem, "id") %>'
							src="App_Themes/Default/images/plus.gif"
							onclick='HidePanel(this.id,<%# @"""floatingPanel"+SelectScalar(Container.DataItem, "id")+@"""" %>)'
							onmouseover="ShowHandCursor(this.id)" />
					<asp:Placeholder
							runat="server"
							Visible='<%# !string.IsNullOrEmpty(SelectScalar(Container.DataItem, "short")) %>'>
						<span>
							<strong><%# FormatDateTime(SelectScalar(Container.DataItem, "date")) %></strong>&nbsp;&nbsp;
							<b><%# SelectScalar(Container.DataItem, "head")%></b>
						</span>
						<div	id='floatingPanel<%# SelectScalar(Container.DataItem, "id") %>'
								style="display:none">
							<span style="color:Gray">
								
								<img	runat="server"
										align="left"
										src='<%# "UserPhoto.aspx?id=" + SelectScalar(Container.DataItem, "Image") %>'
										border="0"
										visible='<%# !string.IsNullOrEmpty(SelectScalar(Container.DataItem, "Image")) %>' />
								
								<%# SelectScalar(Container.DataItem, "text") %>&nbsp;
								
								<asp:PlaceHolder
										ID="PlaceHolder1"
										runat="server"
										visible='<%# !string.IsNullOrEmpty(SelectScalar(Container.DataItem, "moreHref")) %>'>
									<a		id="A1"
											runat="server"
											target="_blank"
											href='<%# SelectScalar(Container.DataItem, "moreHref") %>'
											visible='<%# !string.IsNullOrEmpty(SelectScalar(Container.DataItem, "moreText")) %>'>
										<%# SelectScalar(Container.DataItem, "moreText") %></a>
									<a		id="A2"
											runat="server"
											target="_blank"
											href='<%# SelectScalar(Container.DataItem, "moreHref") %>'
											visible='<%# string.IsNullOrEmpty(SelectScalar(Container.DataItem, "moreText")) %>'>
										<%# SelectScalar(Container.DataItem, "/xml/more") %></a>
								</asp:PlaceHolder>
							</span>
						</div><br/>
					</asp:Placeholder>
				</ItemTemplate>
			</asp:Repeater>
		</td></tr>
</table>