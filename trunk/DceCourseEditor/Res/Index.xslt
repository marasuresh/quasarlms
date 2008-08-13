<?xml version="1.0" encoding="windows-1251"?>
<xsl:stylesheet version="1.0" xmlns:xsl='http://www.w3.org/1999/XSL/Transform'>
<xsl:output method="html" omit-xml-declaration="yes" indent="no"/>

<xsl:template match="/xml">
<h2>Курс:&#160;<xsl:value-of select="DataSet/Course/Name"/></h2>
<h3>Темы:</h3>
   <ol type="I">
      <xsl:apply-templates select="DataSet/Themes"/>
   </ol>
</xsl:template>

<xsl:template match="Themes">
   <li>
   <h4><xsl:value-of select="Name"/></h4>
   <blockquote>
      <xsl:if test="count(contentItems) > 0">
         <h4>Материалы:</h4>
         <ol type="disc">
            <xsl:apply-templates select="contentItems"/>
         </ol>
      </xsl:if>
      <xsl:if test="count(Themes) > 0">
      <h4>Подтемы:</h4>
         <ol type="1">
            <xsl:apply-templates select="Themes"/>
         </ol>
      </xsl:if>
   </blockquote>
   </li>
</xsl:template>

<xsl:template match="contentItems">
   <li/><a href="{url}" target="_blank"><xsl:value-of select="url"/></a>
</xsl:template>

</xsl:stylesheet>

  