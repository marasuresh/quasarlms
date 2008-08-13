<?xml version="1.0" encoding="windows-1251"?>
<xsl:stylesheet version="1.0" xmlns:xsl='http://www.w3.org/1999/XSL/Transform'
xmlns:msxml="urn:schemas-microsoft-com:xslt" xmlns:script="myspace">
<xsl:output method="html"/>

<xsl:template match="/xml">
<table cellpadding="0" cellspacing="0" width="100%" border="0" style="font-family: Arial, Tahoma, Verdana, Helvetica, Serif;color: #3367A3;font-size: 9pt;margin: 0,0,0,0;padding: 0,0,0,0;">
<tr><td>
<h2><a style="font-weight: bolder; color: #3367A3;" href="{href}"><xsl:value-of select="Kvazar"/></a></h2><hr/>
<xsl:value-of select="User"/>&#160;<xsl:value-of select="name"/><br/><br/>
<xsl:value-of select="Login"/>&#160;<xsl:value-of select="email"/><br/>
<xsl:value-of select="Password"/>&#160;<xsl:value-of select="passw"/><br/>
</td></tr>
</table>
</xsl:template>

</xsl:stylesheet>