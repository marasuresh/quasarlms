<?xml version="1.0"?> 
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
xmlns:msxml="urn:schemas-microsoft-com:xslt"
xmlns:script="myspace">
<xsl:output method="html" omit-xml-declaration="yes" indent="no"/>
	<!-- Input params: unseen@bk.ru -->
	<xsl:param name="Error" select="''"/>			
	<xsl:param name="recordId" select="''"/>			

<xsl:template match="/">

<xsl:if test="/xml/row/TaskTime">
 <div><xsl:value-of select="/xml/lng/TaskTopic"/>:
 <strong><xsl:value-of select="/xml/row/Message"/></strong>
 </div>
 <div><xsl:value-of select="/xml/lng/EditTaskTitle"/>:</div>
	<table cellspacing="0" cellpadding="0" border="0" width="100%" class="RegForm">
		<tr>
			<td><TEXTAREA name="EditTaskTxt" class="TopicTxt"><xsl:value-of select="/xml/row/Solution"/></TEXTAREA><br/></td>
		</tr>

		<xsl:if test="not(/xml/row/Complete) or number(/xml/row/Complete) &lt; 1">
		<tr>
			<td align="right" class="btnTopicPost"><INPUT name="btnEditTask" type="submit" value="{/xml/lng/EditTaskPost}"/></td>
		</tr>
		</xsl:if>
	</table>
</xsl:if>

<xsl:if test="count(/xml/Error)>0">
<table cellspacing="0" cellpadding="0" border="0" width="100%" class="RegForm">
	<tr><td><xsl:value-of select="/xml/lng/PostError"/></td></tr>
	<tr align="center"><td>
		<font color="#FF0000">
			&#160;<xsl:value-of select="/xml/Error"/>
		</font>
	</td></tr>
</table>
</xsl:if>
</xsl:template>
</xsl:stylesheet> 