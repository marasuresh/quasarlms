<?xml version="1.0" encoding="windows-1251"?> 
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
xmlns:msxml="urn:schemas-microsoft-com:xslt"
xmlns:script="myspace">
<xsl:output method="html" omit-xml-declaration="yes" indent="no"/>
<!-- Input params: -->

<xsl:template match="/">
	<xsl:variable name="Chr" select="'&#160;'"/>
	<xsl:variable name="rowCount" select="count(/xml/row/*)"/>
	<div><strong><B><xsl:value-of select="/xml/lng/CurrentTopicBody"/>:</B></strong><BR/>
	<strong><xsl:value-of select="/xml/row/Topic"/></strong>
	</div>
	<div class="TopicBody">
		<xsl:if test="$rowCount>0">
			<xsl:value-of select="/xml/row/Message"/>
		</xsl:if>
		<xsl:if test="$rowCount=0">
			<xsl:value-of select="/xml/lng/NoTopicBody"/>
		</xsl:if>
	</div>
</xsl:template>
</xsl:stylesheet> 