
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:dc="http://purl.org/dc/elements/1.1/" version="1.0">
        <xsl:output method="html"  />
    <xsl:template match="/">

<rule>
<root/>      
<HTML>       
<BODY bgcolor="white">
<center><hr width="70%"/><b> Экзаменационно - рейтинговые ведомости </b><hr width="70%"/><br/> 
<table width="90%" border="2">
    <xsl:apply-templates select="//ArrayOfString" />
<children/>  
</table></center>
</BODY>
</HTML>	
</rule>

    </xsl:template>

    <xsl:template match="ArrayOfString">
        <tr>
            <xsl:for-each select="string">
                <td>
                    <xsl:value-of select="." />
                </td>
            </xsl:for-each>
        </tr>
    </xsl:template>

    </xsl:stylesheet>
    
