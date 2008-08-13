<?xml version="1.0" encoding="windows-1251"?>
<xsl:stylesheet version="1.0" xmlns:xsl='http://www.w3.org/1999/XSL/Transform'
xmlns:msxml="urn:schemas-microsoft-com:xslt"
xmlns:script="myspace">
<xsl:output method="html"/> 
<msxml:script implements-prefix="script" language="jscript">
<![CDATA[	
	function isAbsolutePath(url)
	{
	 return url.substr(0, 7) == "http://" || url.substr(0, 4) == "www.";
	}
	function prefix(url)
	{
	 if (url.substr(0, 4) == "www.")
	 return "http://";
	 return "";
	}
]]>
</msxml:script>

<xsl:template match="/xml">
 <script language="javascript">
 function resizeFrame()
 {
 theHeight = document.getElementById("contFrameId").contentWindow.document.body.scrollHeight;
 document.getElementById("contFrameId").style.height = theHeight;
 }
 </script>

 <table cellSpacing="0" cellPadding="0" width="100%" align="center" border="0" class="RegForm">
 <xsl:if test="0!=count(Course)">
 <tr>
 <td>
			 <h3 class="cap4"><xsl:value-of select="Course"/></h3>
			</td>
 </tr>
 </xsl:if>
 <xsl:if test="0!=count(ds/contentItem)">
 <tr>
 <td style="PADDING-TOP: 7px">
			 <h3 class="cap3"><xsl:value-of select="ds/contentItem/Name"/></h3>
 </td>
 </tr>
 </xsl:if>
 <tr><td><hr/></td></tr>
 <xsl:for-each select="ds/contentItem">
 <xsl:if test="url">
 <tr>
 <td>
 <a target="_blank" href="{script:prefix(string(url))}{url}"><xsl:value-of select="url"/></a>
 </td>
 </tr>
 </xsl:if>
 </xsl:for-each>
 </table>
</xsl:template>

</xsl:stylesheet>

 