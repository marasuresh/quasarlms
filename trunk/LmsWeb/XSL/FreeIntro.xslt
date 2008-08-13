<?xml version="1.0" encoding="windows-1251"?>
<xsl:stylesheet version="1.0" xmlns:xsl='http://www.w3.org/1999/XSL/Transform'
xmlns:msxml="urn:schemas-microsoft-com:xslt"
xmlns:script="myspace">
<xsl:output method="html"/> 
<msxml:script implements-prefix="script" language="jscript">
<![CDATA[		
	function formatDate(strDate)
	{	
		return strDate.substr(8, 2) + "." 
		+ strDate.substr(5, 2) + "." 
		+ strDate.substr(0, 4);
	}
	function replSlash(strPath)
	{	
	 var re;
 re = /(\\+)/g;
		return strPath.replace(re, "/");
	}
]]>
</msxml:script>

<xsl:param name="LangPath"/>
<xsl:template match="/">
 <script language="javascript">
 function resizeFrame(fid)
 {
 theHeight = document.getElementById(fid).contentWindow.document.body.scrollHeight;
 document.getElementById(fid).style.height = theHeight;
 }
 </script>

<xsl:choose>
	<xsl:when test="xml/id">
	
	<table cellspacing="0" cellpadding="0" width="100%" border="0" align="center" class="InnerTable">
	<tr valign="top">
		<td width="75%">
			<h3 class="cap3"><xsl:value-of select="xml/Caption"/></h3>
			<h3 class="cap4" title="{xml/Description}"><xsl:value-of select="xml/Name"/></h3>

 <iframe name="contFrame" width="100%" height="100%" id="contFrameId1" onLoad="resizeFrame('contFrameId1')" frameborder="no" align="top" scrolling="no">
		 <xsl:attribute name="src">
		 <xsl:choose>
		 <xsl:when test="xml/FullDescription">
		 <xsl:value-of select="xml/cRoot"/>
		 <xsl:value-of select="script:replSlash(string(xml/DiskFolder))"/>
		 <xsl:value-of select="script:replSlash(string(xml/FullDescription))"/>
		 </xsl:when>
		 <xsl:otherwise>
		 <!--<xsl:value-of select="$LangPath"/>NoContent.htm-->about:blank
		 </xsl:otherwise>
		 </xsl:choose>
		 </xsl:attribute>
 </iframe>
			
		</td>
		<td width="25%" align="center">
			<a href="{xml/Start/href}"><img src="images/course_img.jpg" width="70" height="70" border="0" alt="" align="center" style="MARGIN-BOTTOM: 10px; MARGIN-TOP: 10px;"/></a><br/>
			<small>
			 <a href="{xml/Start/href}"><xsl:value-of select="xml/Start/hrefLable"/></a><br/>
			</small>
		</td>
	</tr>
	</table>
 <xsl:if test="0!=count(xml/BulletinBoard/DataSet/Bulletins)">
	 <hr/>
	 <p class="CenterColumn"><img src="images/bullet.gif" width="14" height="14" border="0" alt=""/>&#160;&#160;&#160;<strong class="yellow"><xsl:value-of select="//BulletinBoard/Caption"/></strong></p>
	 <table cellspacing="0" cellpadding="3" width="100%" border="0" align="center" class="TableList">
		 <th class="Head"><xsl:value-of select="//BulletinBoard/Header/Date"/></th>
	 <th class="Head"><xsl:value-of select="//BulletinBoard/Header/Autor"/></th>
	 <th class="Head"><xsl:value-of select="//BulletinBoard/Header/Text"/></th>
		 <xsl:for-each select="xml/BulletinBoard/DataSet/Bulletins">
			 <tr>
			 <xsl:if test="(position() div 2)=ceiling(position() div 2)"><xsl:attribute name="bgcolor">#F6F6F6</xsl:attribute></xsl:if>					
			 <xsl:if test="(position() div 2)!=ceiling(position() div 2)"><xsl:attribute name="bgcolor">#FFFFFF</xsl:attribute></xsl:if>
				 <td align="center" nowrap="true"><xsl:value-of select="Date"/></td>
				 <td width="40%" nowrap="true"><xsl:value-of select="Autor"/></td>
				 <td width="55%"><xsl:value-of select="Text"/></td>
			 </tr>
		 </xsl:for-each>
	 </table>
	</xsl:if>
 <xsl:if test="0!=count(xml/BulletinBoard/DataSet/TestResults)">
	 <hr/>
	 <p class="CenterColumn"><img src="images/bullet.gif" width="14" height="14" border="0" alt=""/>&#160;&#160;&#160;<strong class="yellow"><xsl:value-of select="//Statistics/Caption"/></strong></p>
	 <table cellspacing="0" cellpadding="3" width="100%" border="0" align="center" class="TableList">
	 <th class="Head"><xsl:value-of select="//Statistics/Header/Theme"/></th>
	 <th class="Head"><xsl:value-of select="//Statistics/Header/Complete"/></th>
	 <th class="Head"><xsl:value-of select="//Statistics/Header/Tries"/></th>
	 <th class="Head"><xsl:value-of select="//Statistics/Header/Points"/></th>
	 <th class="Head"><xsl:value-of select="//Statistics/Header/CompletionDate"/></th>
	 <xsl:for-each select="xml/Statistics/DataSet/TestResults">
		 <tr>
		 <xsl:if test="(position() div 2)=ceiling(position() div 2)"><xsl:attribute name="bgcolor">#F6F6F6</xsl:attribute></xsl:if>					
		 <xsl:if test="(position() div 2)!=ceiling(position() div 2)"><xsl:attribute name="bgcolor">#FFFFFF</xsl:attribute></xsl:if>
			 <td width="50%"><xsl:value-of select="Theme"/></td>
			 <td width="20%" nowrap="true"><xsl:value-of select="Complete"/></td>
			 <td width="10%" align="center"><xsl:value-of select="Tries"/></td>
			 <td width="10%" align="center"><xsl:value-of select="Points"/></td>
			 <td width="10%" align="center"><xsl:value-of select="CompletionDate"/></td>
		 </tr>
	 </xsl:for-each>
	 </table>
	</xsl:if>
 </xsl:when>
	<xsl:otherwise>

 <table cellspacing="0" cellpadding="0" border="0" width="100%" align="center">
 <tr><td width="75%">
	 <h3 class="failure"><xsl:value-of select="//NoFree"/></h3><br/>
	</td></tr>
	</table>
	
	</xsl:otherwise>
</xsl:choose>

</xsl:template>
</xsl:stylesheet>