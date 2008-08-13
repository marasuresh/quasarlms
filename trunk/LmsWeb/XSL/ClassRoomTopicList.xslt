<?xml version="1.0" encoding="windows-1251"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
xmlns:msxml="urn:schemas-microsoft-com:xslt"
xmlns:script="myspace">
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
]]>
</msxml:script>
<xsl:output method="html" encoding="unicode"/> 
<!--xsl:output method="html" omit-xml-declaration="yes" indent="no"/-->
	<!-- Input params: -->
	<xsl:param name="ApplySorting" select="'true'"/>
	<xsl:param name="Index" select="''"/>
	<xsl:param name="order" select="'1'"/>
	<xsl:param name="field" select="''"/>			
	<xsl:param name="IE" select="0"/>
	<xsl:param name="Title" select="''"/>
	<xsl:param name="filterAction" select="''"/>
	<xsl:param name="isEmptyShow" select="'true'"/>
	<xsl:include href="_PageNavigatorI.xsl"/>
	<xsl:param name="recordId"/>

	<xsl:template match="/">
	<xsl:variable name="Chr" select="'&#160;'"/>
	<xsl:variable name="rowCount" select="count(/xml/row/*)"/>

<xsl:if test="script:iif($rowCount,$isEmptyShow)">

<script language="javascript">
function setTopic(topicID)
{
	AddParameter('recordId', topicID);			
	applyParameters();
}
</script>
<h3 class="cap4"><xsl:value-of select="/xml/lng/title"/></h3>
<p class="help"><xsl:value-of select="/xml/lng/Help1"/></p>
<input type="hidden" name="recordId" id="recordId" value=""/>
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
			 <xsl:with-param name="SortField" select="'sortingTopic'"/>
			 <xsl:with-param name="Label" select="/xml/lng/ReplyMsg"/>
			 <xsl:with-param name="tl" select="/xml/lng/ReplyMsgTip"/>
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
			<xsl:call-template name="AuthorColor"/>
		 <xsl:if test="$recordId=id">
			 <xsl:attribute name="bgcolor">#ff9999</xsl:attribute>	
		 </xsl:if>
			<xsl:attribute name="title"><xsl:value-of select="Message"/></xsl:attribute>
				<td><small class="PageNavigator">
				 <xsl:value-of select="script:formatDate(string(PostDate))"/>
				 </small>
				</td>
				<td><small class="PageNavigator">
					<xsl:if test="count(Email)>0">
					<a href="mailto:{Email}">
					<xsl:call-template name="Author"/>
					</a>
					</xsl:if>
					<xsl:if test="count(Email)=0">
					<xsl:call-template name="Author"/>
					</xsl:if>
					</small>
				</td>
				<td>
					<a>
					 <!--xsl:if test="Blocked='false'"-->
					 <xsl:attribute name="href">javascript:setTopic('<xsl:value-of select="id"/>')</xsl:attribute>
					 <!--/xsl:if-->
						<xsl:value-of select="Topic"/>
					 </a><br/>
						<small class="PageNavigator">
						<xsl:if test="concat(Student, '')=''">
					 &#160;(<xsl:value-of select="/xml/lng/Public"/>)
					 </xsl:if>
					 <xsl:if test="concat(Student, '')!=''">
					 &#160;(<xsl:value-of select="/xml/lng/Private"/>)
					 </xsl:if>
					 <xsl:if test="Blocked='true'">
					 &#160;(<xsl:value-of select="/xml/lng/Blocked"/>)
					 </xsl:if>
					 </small>
				</td>
		</tr>				
			</xsl:for-each>
		 </xsl:if>
		 <xsl:if test="$rowCount=0">
			 <tr align="center"><td colspan="3"><xsl:value-of select="xml/lng/TopicNosing"/></td></tr>
		 </xsl:if>
		</table>
		<xsl:call-template name="PageNavigator"/>
	</td></tr>
</table>
</xsl:if>
</xsl:template>

<xsl:template name="Author">
	<xsl:choose>
		<xsl:when test="usAuthor">
		 <xsl:value-of select="usAuthor"/>&#160;
		 (<xsl:value-of select="/xml/lng/Author"/>)
		</xsl:when>
		<xsl:otherwise>
		 <xsl:value-of select="stAuthor"/>&#160;
		 (<xsl:value-of select="/xml/lng/Student"/>)
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<xsl:template name="AuthorColor">
	<xsl:if test="usAuthor">
		<xsl:attribute name="bgcolor">#f3fff7</xsl:attribute>	
	</xsl:if>
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