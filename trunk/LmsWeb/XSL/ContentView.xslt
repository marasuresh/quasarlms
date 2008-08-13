<?xml version="1.0" encoding="windows-1251"?>
<xsl:stylesheet version="1.0"
				xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
				xmlns:msxml="urn:schemas-microsoft-com:xslt"
				xmlns:script="myspace"
				xmlns:cs="urn:my-scripts">
	<xsl:output method="html"/>
	<msxml:script implements-prefix="cs"
				  language="CSharp">
		<![CDATA[		
 public string makeUrl(string croot, string folder, string path)
 {
 string newPath = croot+folder+path;
 return newPath.Replace("\\", "/");
 }
]]>
	</msxml:script>

	<xsl:param name="LangPath"/>
	<xsl:template match="/xml">
		<script language="javascript">
			var current=1;
			var tcount=1;
			function navigateClick(item, count)
			{
			current=parseInt(item);
			tcount = parseInt(count);
			setButtons();
			for (var i=1; i &lt;= tcount ; i++)
			{
			if (i == current)
			{
			document.getElementById("contFrameId"+i).style.display="";
			}
			else
			{
			document.getElementById("contFrameId"+i).style.display="none";
			}
			}
			resizeFrame("contFrameId"+current);
			}
			function prev(count)
			{
			if (current &gt; 1)
			{
			navigateClick(current-1, count);
			}
			}
			function next(count)
			{
			if (current &lt; count)
			{
			navigateClick(current+1, count);
			}
			}
			lastfr = "";
			function resizeFrame(ctid)
			{
			if (document.getElementById(ctid).style.display == "none")
			return;
			if (lastfr != ctid)
			{
			//setButtons();
			lastfr = ctid;
			try
			{
			theHeight = document.getElementById("TABLECV").scrollHeight;
			//cf.style.height = theHeight;
			//alert(ctid+" "+theHeight);
			}
			catch(e){}
			try
			{
			theHeight = document.getElementById(ctid).contentWindow.document.body.scrollHeight;
			if (theHeight != 0)
			{
			document.getElementById(ctid).style.height = theHeight+50;
			}
			//alert(ctid+" "+theHeight);
			}
			catch(e)
			{
			document.getElementById(ctid).scrolling="auto";
			//theHeight = document.getElementById("TABLECV").scrollHeight;
			document.getElementById(ctid).style.height = 2000;
			}
			}
			}

			function setButtons()
			{
			for (var i=1; i &lt;= tcount ; i++)
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
			<xsl:if test="0!=count(trainings/training)">
				<tr>
					<td>
						<h3 class="cap4">
							<xsl:value-of select="trainings/training/tName"/>
						</h3>
					</td>
				</tr>
			</xsl:if>
			<xsl:if test="0!=count(dsContent/contentItem)">
				<tr>
					<td>
						<h3 class="cap3">
							<xsl:value-of select="dsContent/contentItem/Name"/>
						</h3>
					</td>
				</tr>
			</xsl:if>
			<xsl:if test="count(dsContent/contentItem) > 1">
				<tr>
					<td>
						<br/>
						<form id="prevId"
							  name="Navigate"
							  method="post"
							  action="">
							<input style="MARGIN-RIGHT: 3px; MARGIN-LEFT: 3px; BORDER: 0px; CURSOR: hand; COLOR: #0000cc; BACKGROUND-COLOR: transparent;"
								   type="button"
								   value="&lt;&lt;"
								   onclick="javascript:prev({string(count(/xml/dsContent/contentItem))})" />
							<xsl:for-each select="dsContent/contentItem">
								<input id="{concat('navigateButton', string(position()))}"
									   type="button"
									   value="{position()}"
									   alt="{alt}"
									   title="{alt}"
									   onclick="javascript:navigateClick({string(position())}, {string(count(/xml/dsContent/contentItem))})">
									<xsl:choose>
										<xsl:when test="position()=1">
											<xsl:attribute name="style">MARGIN-RIGHT: 3px; MARGIN-LEFT: 3px; BORDER: 0px; CURSOR: auto; COLOR: #0000cc; BACKGROUND-COLOR: transparent; TEXT-DECORATION: none; COLOR: #808080;</xsl:attribute>
										</xsl:when>
										<xsl:otherwise>
											<xsl:attribute name="style">MARGIN-RIGHT: 3px; MARGIN-LEFT: 3px; BORDER: 0px; CURSOR: auto; COLOR: #0000cc; BACKGROUND-COLOR: transparent; TEXT-DECORATION: underline; COLOR: #0000cc; CURSOR: hand;</xsl:attribute>
										</xsl:otherwise>
									</xsl:choose>
								</input>
							</xsl:for-each>
							<input id="nextId"
								   style="MARGIN-RIGHT: 3px; MARGIN-LEFT: 3px; BORDER: 0px; CURSOR: hand; COLOR: #0000cc; BACKGROUND-COLOR: transparent;"
								   type="button"
								   value="&gt;&gt;"
								   onclick="javascript:next({string(count(/xml/dsContent/contentItem))})" />
						</form>
						<hr/>
					</td>
				</tr>
			</xsl:if>
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
							<td>
								<xsl:for-each select="dsContent/contentItem">
									<iframe frameborder="no"
											name="contFrame{string(position())}"
											width="100%"
											height="100%"
											id="contFrameId{string(position())}"
											align="top"
											onload="javascript:resizeFrame('contFrameId{string(position())}');"
											onresize="javascript:resizeFrame('contFrameId{string(position())}');">
										<xsl:choose>
											<xsl:when test="url">
												<xsl:attribute name="src">
													<!--contentpage.aspx?path=<xsl:value-of select="cs:makeUrl(string(PublicRoot),string(DiskFolder), string(url))"/>-->
													<xsl:value-of select="cs:makeUrl(string(cRoot),string(DiskFolder), string(url))"/>
												</xsl:attribute>
											</xsl:when>
											<xsl:otherwise>
												<xsl:attribute name="src">
													<xsl:value-of select="$LangPath"/>NoContent.htm
												</xsl:attribute>
											</xsl:otherwise>
										</xsl:choose>
										<xsl:if test="position() > 1">
											<xsl:attribute name="style">display:none;</xsl:attribute>
										</xsl:if>
									</iframe>
								</xsl:for-each>
							</td>
						</tr>
					</table>
				</td>
			</tr>
		</table>
	</xsl:template>

</xsl:stylesheet>

