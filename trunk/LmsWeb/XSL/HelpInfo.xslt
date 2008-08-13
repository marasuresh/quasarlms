<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet version="1.0" xmlns:xsl='http://www.w3.org/1999/XSL/Transform'
xmlns:msxml="urn:schemas-microsoft-com:xslt"
xmlns:script="myspace">
<xsl:output method="html" indent="no"/>

<xsl:template match="/xml">
<table border="0" cellpadding="2" width="100%" valign="top" class="CenterColumn">
 <tr><td valign="top">
 <p class="help"> <xsl:value-of select="/xml/help1"/> <br/>
 
 <xsl:value-of select="/xml/help1_1"/> <br/>
 <xsl:value-of select="/xml/help1_2"/> </p>
 
 <p class="help"> <xsl:value-of select="/xml/help2"/> <br/>
 
 <xsl:value-of select="/xml/help2_1"/> <br/>
 <xsl:value-of select="/xml/help2_2"/> <br/>
 <xsl:value-of select="/xml/help2_3"/> <br/>
 <xsl:value-of select="/xml/help2_4"/> </p>
 
 <table border="0" cellpadding="2" width="100%" valign="top" class="CenterColumn">
 <tr>
 <td valign="top">
 <!--<img src="images/moc.jpg" width="119" height="43" border="0" hspace="5" wspace="5" alt="" align="left"/>-->
 </td>
 <td><p class="blue">
 <xsl:value-of select="/xml/help3_1"/><br/><br/>
 <xsl:value-of select="/xml/help3_2"/>
 </p>
 </td></tr>
 </table>
 
 </td></tr>
 </table>

</xsl:template>
</xsl:stylesheet>

  