<?xml version="1.0"?> 
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
xmlns:msxml="urn:schemas-microsoft-com:xslt"
xmlns:script="myspace">
<xsl:output method="html" omit-xml-declaration="yes" indent="no"/>
<msxml:script implements-prefix="script" language="jscript">
<![CDATA[		
	function iif(rowCount,isEmptyShow)
	{	
		if(	rowCount==0 && !isEmptyShow )	return false;
		return true;
	}
]]>
</msxml:script>
<xsl:output method="html" omit-xml-declaration="yes" indent="no"/>
	<!-- Input params: -->
	<xsl:param name="ApplySorting" select="'true'"/>
	<xsl:param name="Index" select="''"/><!--индекс на странице-->
	<xsl:param name="order" select="'1'"/>
	<xsl:param name="field" select="''"/>			
	<xsl:param name="IE" select="0"/>
	<xsl:param name="Title" select="''"/>
	<xsl:param name="filterAction" select="''"/>
	<xsl:param name="isEmptyShow" select="'true'"/>
	<xsl:param name="recordId"/>
	<xsl:include href="_PageNavigatorI.xsl"/>

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
		<h3 class="cap4"><xsl:value-of select="/xml/lng/TaskTitle"/></h3>
		<p class="help"><xsl:value-of select="/xml/lng/Help2"/></p>
		<input type="hidden" name="recordId" id="recordId" value=""/>
		<table border="0" cellspacing="0" cellpadding="0" width="100%" align="center" class="TableList">
		<!--tr-->
			<xsl:call-template name="ColSort">
				<xsl:with-param name="SortField" select="'aName'"/>
				<xsl:with-param name="Label" select="xml/lng/TaskAuthor"/>
				<xsl:with-param name="tl" select="xml/lng/TaskAuthorTip"/>
			</xsl:call-template>
			<xsl:call-template name="ColSort">
				<xsl:with-param name="SortField" select="''"/>
				<xsl:with-param name="Label" select="xml/lng/TaskTopic"/>
				<xsl:with-param name="tl" select="xml/lng/TaskTip"/>
			</xsl:call-template>
			<xsl:call-template name="ColSort">
				<xsl:with-param name="SortField" select="'TaskTime'"/>
				<xsl:with-param name="Label" select="xml/lng/TaskDate"/>
				<xsl:with-param name="tl" select="xml/lng/TaskDateTip"/>
			</xsl:call-template>
			<xsl:call-template name="ColSort">
				<xsl:with-param name="SortField" select="'Complete'"/>
				<xsl:with-param name="Label" select="xml/lng/TaskSolution"/>
				<xsl:with-param name="tl" select="xml/lng/TaskSolutionTip"/>
			</xsl:call-template>
		<!--/tr-->
			<colgroup>
				<col width="20%" nowrap="true"/>
				<col width="50%"/>
				<col width="40pt" align="center" nowrap="true"/>
				<col width="100pt"/>
			</colgroup>
			<xsl:if test="$rowCount=0">
			<tr>
				<td colspan="4" align="center"><xsl:value-of select="/xml/lng/TaskListisEmpty"/></td>
			</tr>
			</xsl:if>
			<xsl:if test="$rowCount>0">
				<xsl:for-each select="/xml/row">
				<tr>
				<!--xsl:call-template name="AuthorColor"/-->
				<xsl:if test="(position() div 2)=ceiling(position() div 2)"><xsl:attribute name="bgcolor">#F6F6F6</xsl:attribute></xsl:if>					
				<xsl:if test="(position() div 2)!=ceiling(position() div 2)"><xsl:attribute name="bgcolor">#FFFFFF</xsl:attribute></xsl:if>
		 <xsl:if test="$recordId=id">
			 <xsl:attribute name="bgcolor">#ff9999</xsl:attribute>	
		 </xsl:if>
				 <xsl:attribute name="title"><xsl:value-of select="Message"/></xsl:attribute>
					<td>
					 <xsl:if test="count(Email)>0">
						 <a href="mailto:{Email}">
							 <xsl:call-template name="Author"/>
						 </a>
						</xsl:if>
					 <xsl:if test="count(Email)=0">
							 <xsl:call-template name="Author"/>
						</xsl:if>
					</td>
					<td>
						<a href="javascript:setTopic('{id}')">
							<xsl:value-of select="Topic"/>
						</a>
					</td>
					<td>
						<a href="javascript:setTopic('{id}')">
						<xsl:call-template name="formatDate">
							<xsl:with-param name="dt" select="TaskTime"/>
						 </xsl:call-template>
						</a>
					</td>
					<td>
						<a href="javascript:setTopic('{id}')">
		 <xsl:choose>
		 <xsl:when test="count(Solution)=0">
						 <xsl:value-of select="/xml/lng/TaskNoSolution"/>
		 </xsl:when>
		 <xsl:when test="Complete=0">
		 <xsl:value-of select="/xml/lng/TaskUnchecked"/>
		 </xsl:when>
		 <xsl:when test="Complete=1">
		 <xsl:value-of select="/xml/lng/TaskIncomplete"/>
		 </xsl:when>
		 <xsl:when test="Complete=2">
		 <xsl:value-of select="/xml/lng/TaskIncorrect"/>
		 </xsl:when>
		 <xsl:when test="Complete=3">
		 <xsl:value-of select="/xml/lng/TaskPartially"/>
		 </xsl:when>
		 <xsl:when test="Complete=4">
		 <xsl:value-of select="/xml/lng/TaskAlmost"/>
		 </xsl:when>
		 <xsl:when test="Complete=5">
		 <xsl:value-of select="/xml/lng/TaskRight"/>
		 </xsl:when>
		 <xsl:otherwise>
						 <xsl:value-of select="/xml/lng/TaskNoSolution"/>
		 </xsl:otherwise>
		 </xsl:choose>
						</a>
					</td>
				</tr>				
				</xsl:for-each>
			</xsl:if>
			 <xsl:if test="$rowCount=0">
				 <tr><td colspan="3"><font color="#FF0000"><xsl:value-of select="xml/lng/TaskNosing"/></font></td></tr>
			 </xsl:if>
			</table>
			<xsl:call-template name="PageNavigator"/>
	</xsl:if>
</xsl:template>

<xsl:template name="Author">
			<xsl:value-of select="aName"/>&#160;
			(<xsl:value-of select="/xml/lng/TaskAuthor"/>)
</xsl:template>

<xsl:template name="AuthorColor">
		<xsl:if test="Author!=Student">
			<xsl:attribute name="bgcolor">#FFD6E2</xsl:attribute>	
		</xsl:if>
		<xsl:if test="count(Student)=0">
			<xsl:attribute name="bgcolor">#FFD6E2</xsl:attribute>	
		</xsl:if>
		<xsl:if test="Author=Student">
			<xsl:attribute name="bgcolor">#CDD8E4</xsl:attribute>	
		</xsl:if>
</xsl:template>

<xsl:template name="formatDate">
	<xsl:param name="dt"/>
	<xsl:value-of select="substring($dt,9,2)"/>.<xsl:value-of select="substring($dt,6,2)"/>.<xsl:value-of select="substring($dt,1,4)"/>
</xsl:template>

<xsl:template name="ColSort">
		
	<xsl:param name="SortField"/>
	<xsl:param name="Label"/>
	<xsl:param name="tl" select="''"/>

	<th class="HeadCenter" title="{$tl}">
		<xsl:if test="$ApplySorting='true' and $SortField!=''">
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
				<a href="javascript:sortI('{$SortField}',1,'{$Index}')"><xsl:value-of select="$Label"/></a>
			</xsl:if>	
		</xsl:if>	

		<xsl:if test="$ApplySorting!='true' or $SortField=''">
			<xsl:value-of select="$Label"/>
		</xsl:if>	
	</th>
</xsl:template>
</xsl:stylesheet> 