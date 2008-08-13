<?xml version="1.0" encoding="windows-1251"?> 
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
xmlns:msxml="urn:schemas-microsoft-com:xslt"
xmlns:script="myspace"
xmlns:cs="urn:my-scripts">
<xsl:output method="html" omit-xml-declaration="yes" indent="no"/>
<msxml:script implements-prefix="script" language="jscript">
<![CDATA[		
	function iif(rowCount,isEmptyShow)
	{	
		if(	rowCount==0 && !isEmptyShow )	return false;
		return true;
	}
	function formatDate(strDate)
	{	
		return strDate.substr(8, 2) + "." 
		+ strDate.substr(5, 2) + "." 
		+ strDate.substr(0, 4) + " "
		+ strDate.substr(11, 2) + ":"
		+ strDate.substr(14, 2) + ":"
		+ strDate.substr(17, 2);
	}
	function formatMess(strMess)
	{	
	 //var re;
 //re = /(\r\n+)/g;
		//return strMess.replace(re, "<br/>");
		return strMess.split("\r\n");
	}
]]>
</msxml:script>
<msxml:script implements-prefix="cs" language="CSharp">
<![CDATA[		
 public System.Xml.XmlNode formatMess(string strMess)
 {
 //string s = System.Web.HttpUtility.HtmlEncode(strMess);
 strMess = strMess.Replace("&", "&amp;");
 strMess = strMess.Replace("<", "&lt;");
 strMess = strMess.Replace(">", "&gt;");

 System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
 doc.LoadXml("<mess>"+strMess.Replace("\r\n", "<br/>")+"</mess>");

 return doc.DocumentElement;
 }
]]>
</msxml:script>
<xsl:output method="html" omit-xml-declaration="yes"/>
	<!-- Input params: -->
	<xsl:param name="ApplySorting" select="'true'"/>
	<xsl:param name="Index" select="''"/><!--индекс на странице-->
	<xsl:param name="order" select="'1'"/>
	<xsl:param name="field" select="''"/>			
	<xsl:param name="recordId" select="''"/>
	<xsl:include href="_PageNavigatorI.xsl"/>

	<xsl:template match="/">
	 <xsl:variable name="Chr" select="'&#160;'"/>
	 <xsl:variable name="rowCount" select="count(/xml/row/*)"/>
	
	 <!--xsl:if test="script:iif($rowCount,$isEmptyShow)"-->

		<table cellspacing="0" cellpadding="0" border="0" width="100%" align="center">
			<tr><td>
				<table border="0" cellspacing="0" cellpadding="3" width="100%" align="center" class="TableList">
					<xsl:call-template name="ColSort">
						<xsl:with-param name="SortField" select="'PostDate'"/>
						<xsl:with-param name="Label" select="/xml/lng/PostDate"/>
						<xsl:with-param name="tl" select="/xml/lng/PostDateTip"/>
					</xsl:call-template>
					<xsl:call-template name="ColSort">
						<xsl:with-param name="SortField" select="'sortingAuthor'"/>
						<xsl:with-param name="Label" select="/xml/lng/ReplyAuthor"/>
						<xsl:with-param name="tl" select="/xml/lng/ReplyAuthorTip"/>
					</xsl:call-template>
					<xsl:call-template name="ColSort">
						<xsl:with-param name="SortField" select="'sortingMessage'"/>
						<xsl:with-param name="Label" select="/xml/lng/MessageText"/>
						<xsl:with-param name="tl" select="/xml/lng/MessageTextTip"/>
					</xsl:call-template>
				<colgroup>
					<col width="5%" align="center"/>
					<col width="10%" align="center"/>
					<col width="85%" align="left"/>
				</colgroup>
				<xsl:if test="$rowCount>0">
					<xsl:for-each select="/xml/row">
						<tr>
				 <xsl:if test="(position() div 2)=ceiling(position() div 2)"><xsl:attribute name="bgcolor">#F6F6F6</xsl:attribute></xsl:if>					
				 <xsl:if test="(position() div 2)!=ceiling(position() div 2)"><xsl:attribute name="bgcolor">#FFFFFF</xsl:attribute></xsl:if>				
	 <xsl:if test="aName">
		 <xsl:attribute name="bgcolor">#f3fff7</xsl:attribute>	
	 </xsl:if>
							<td><small class="PageNavigator"><xsl:value-of select="script:formatDate(string(PostDate))"/></small></td>
							<td><small class="PageNavigator">
 <xsl:choose>
	 <xsl:when test="aName">
								 <xsl:value-of select="aName"/>
		 (<xsl:value-of select="/xml/lng/Author"/>)
	 </xsl:when>
	 <xsl:otherwise>
 								<xsl:value-of select="stName"/>
		 (<xsl:value-of select="/xml/lng/Student"/>)
	 </xsl:otherwise>
 </xsl:choose>
 </small>
							</td>
							<td>
 					 <xsl:copy-of select="cs:formatMess(string(Message))"/>
							</td>
						</tr>				
					</xsl:for-each>
				 </xsl:if>
				 <xsl:if test="$rowCount=0">
					 <tr><td colspan="3"><font color="#FF0000"><xsl:value-of select="/xml/lng/ReplyNosing"/></font></td></tr>
				 </xsl:if>
				</table>
				<xsl:call-template name="PageNavigator"/>
			</td></tr>
		</table>
	<!--/xsl:if-->
	<!--/xsl:if-->
	 
	</xsl:template>

	<xsl:template name="ColSort">
		
		<xsl:param name="SortField"/>
		<xsl:param name="Label"/>
		<xsl:param name="tl" select="''"/>
		<th class="HeadCenter" title="{$tl}">
			<xsl:if test="$ApplySorting='true'">
				<xsl:if test="$field=$SortField">
					<xsl:attribute name="bgcolor">#CDD8E4</xsl:attribute>	
					<xsl:if test="$order='1'">
						<a href="javascript:sortI('{$SortField}',0,'{$Index}')">
							<xsl:value-of select="$Label"/>&#160;<img src="images/asc.gif" border="0"/>
						</a>
					</xsl:if>
					<xsl:if test="$order='0'">
						<a href="javascript:sortI('{$SortField}',1,'{$Index}')">
							<xsl:value-of select="$Label"/>&#160;<img src="images/desc.gif" border="0"/>
						</a>
					</xsl:if>
				</xsl:if>
				<xsl:if test="$field!=$SortField">
					<a href="javascript:sortI('{$SortField}',0,'{$Index}')"><xsl:value-of select="$Label"/></a>
				</xsl:if>	
			</xsl:if>	

			<xsl:if test="$ApplySorting!='true'">
				<xsl:value-of select="$Label"/>
			</xsl:if>	
		</th>
	</xsl:template>
</xsl:stylesheet>