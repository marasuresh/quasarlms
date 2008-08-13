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
	 <xsl:value-of select="/xml/lng/PostTitle"/>
	<table cellspacing="0" cellpadding="0" border="0" width="100%" align="center" class="RegForm">
		<!--colgroup>
			<col width="80%"/>
			<col width="20%" align="right"/>
		</colgroup-->
			<tr>
				<td width="100%">
					<TEXTAREA name="newReplyTxt" id="newReplyTxt" class="TopicTxt"></TEXTAREA><br/>
				</td>
			</tr>
			<tr>
				<td width="100%" align="right" class="btnTopicPost">
		 <xsl:if test="$recordId!=''"><INPUT name="btn1" id="btn1" type="submit" value="{/xml/lng/PostReplySubmit}"/></xsl:if>
		 <xsl:if test="$recordId =''">&#160;</xsl:if>
				</td>
			</tr>
	</table>


<xsl:if test="/xml/Error!=''">
<table cellspacing="0" cellpadding="0" border="0" width="100%" align="center" class="RegForm">
	<tr><td><xsl:value-of select="/xml/lng/PostError"/></td></tr>
	<tr align="center"><td width="100%" align="center">
		<font color="#FF0000">
			&#160;<xsl:value-of select="/xml/Error"/>
		</font>
	</td></tr>
</table>
</xsl:if>

</xsl:template>

</xsl:stylesheet>
 