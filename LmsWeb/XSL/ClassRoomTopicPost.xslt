<?xml version="1.0" encoding="windows-1251"?> 
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
xmlns:msxml="urn:schemas-microsoft-com:xslt"
xmlns:script="myspace">
<xsl:output method="html" omit-xml-declaration="yes" indent="no"/>
	<!-- Input params: unseen@bk.ru -->
	<xsl:param name="Error" select="''"/>			
	<xsl:param name="recordId" select="''"/>			

<xsl:template match="/">
<br/>
<xsl:if test="count(/xml/btnT1)=0">
	<table cellspacing="0" cellpadding="0" border="0" width="100%" align="center" class="RegForm">
		<colgroup>
			<col width="80%"/>
			<col width="20%" align="right"/>
		</colgroup>
		<tr>
			<td>&#160;</td>
			<td class="btnTopicPost"><INPUT name="btnT1" id="btnT1" type="submit" value="{/xml/lng/PostTopicSubmit}"/></td>
		</tr>
	</table>
</xsl:if>
<xsl:if test="count(/xml/btnT1)>0">
	<xsl:value-of select="/xml/lng/PostTitle"/>
	<table cellspacing="0" cellpadding="0" border="0" width="100%" align="center" class="RegForm">
		<colgroup>
			<col width="20%"/>
			<col width="80%"/>
		</colgroup>
		<tr>
			<td><xsl:value-of select="/xml/lng/newTopicSubject"/></td>
			<td>
				<TEXTAREA name="newTopicSubject" id="newTopicSubject" class="TopicSubj"></TEXTAREA><br/>
			</td>
		</tr>
		<tr>
			<td><xsl:value-of select="/xml/lng/newTopicTxt"/>&#160;</td>
			<td>
				<TEXTAREA name="newTopicTxt" id="newTopicTxt" class="TopicTxt"></TEXTAREA><br/>
			</td>
		</tr>
		<tr>
			<td colspan="2" align="right" class="btnTopicPost">
				<INPUT name="btnTopicPost" id="btnTopicPost" type="submit" value="{/xml/lng/PostTopicPost}"/>
			</td>
		</tr>
	</table>
</xsl:if>

<xsl:if test="count(/xml/Error)>0">
<table cellspacing="0" cellpadding="0" border="0" width="100%" align="center" class="RegForm">
	<tr><td><xsl:value-of select="/xml/lng/PostError"/></td></tr>
	<tr align="center"><td><font color="#FF0000">&#160;<xsl:value-of select="/xml/Error"/></font>
	</td></tr>
</table>
</xsl:if>

</xsl:template>

</xsl:stylesheet>
 