<?xml version="1.0" encoding="windows-1251"?> 
<xsl:stylesheet version="1.0"
 xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
 xmlns:msxml="urn:schemas-microsoft-com:xslt"
 xmlns:script="myspace"
	xmlns:rs="urn:schemas-microsoft-com:rowset" xmlns:z="#RowsetSchema">
<xsl:output method="html" omit-xml-declaration="yes" />
	<xsl:template name="FOR">
		<xsl:param name="i" select="1"/>
		<xsl:param name="N" select="1"/>	
		<!-- ************************************* -->
		<xsl:if test="$lastNumber>=$i">
		<td>
			<xsl:if test="$i!=$currentPage">			
				<a href="javascript:showPageI({$i},'{$Index}')"><xsl:value-of select="$i"/></a>
			</xsl:if>
			<xsl:if test="$i=$currentPage">
				<xsl:value-of select="$i"/>
			</xsl:if>
		</td>
		</xsl:if>	
		<!-- ************************************* -->
		<xsl:if test="$N>$i">
			<xsl:call-template name="FOR">
				<xsl:with-param name="i" select="$i+1"/>
				<xsl:with-param name="N" select="$N"/>
			</xsl:call-template>
		</xsl:if>
	</xsl:template>
	<xsl:param name="recordsCount">100</xsl:param>
	<xsl:param name="pageSize">3</xsl:param>
	<xsl:param name="currentPage">13</xsl:param>
		
	<xsl:variable name="linkCount" select="10"/>
	<xsl:variable name="lastNumber" select="ceiling($recordsCount div $pageSize)"/>		
	<xsl:variable name="portion" select="ceiling($currentPage div $linkCount)"/>
	<xsl:variable name="first" select="($portion+(-1))*$linkCount+1"/>
	<xsl:variable name="last" select="$portion*$linkCount"/>
		
	<xsl:template name="PageNavigator">
	<xsl:if test="$recordsCount>$pageSize">	
	<table align="right" cellspacing="4" cellpadding="0" border="0" class="PageNavigator"><tr>
	<!-- ************************************* -->		
	<td align="center"><xsl:value-of select="/xml/lng/NavigatorPages"/>:&#160;
	<xsl:if test="$portion*$linkCount+-$linkCount*2+1>0">
	<a href="javascript:showPageI(1,'{$Index}')">&lt;&lt;</a><xsl:text disable-output-escaping="yes">&amp;nbsp;&amp;nbsp;</xsl:text>
	<!--a href="javascript:showPageI({$portion*$linkCount+-$linkCount*1},'{$Index}')">&lt;</a-->
	<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
	</xsl:if>
	<!--xsl:if test="0>=$portion*$linkCount+-$linkCount*2+1">
	<xsl:text disable-output-escaping="yes">&lt;&lt;&amp;nbsp;&amp;nbsp;&lt;&amp;nbsp;</xsl:text>
	</xsl:if-->
	</td>
	<!-- ************************************* -->
	<xsl:call-template name="FOR">
		<xsl:with-param name="i" select="$first"/>
		<xsl:with-param name="N" select="$last"/>
	</xsl:call-template>
	<!-- ************************************* -->
	<td align="center">
	<xsl:if test="$lastNumber>=$portion*$linkCount+1">
		<!--xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text><a href="javascript:showPageI({$portion*$linkCount+1},'{$Index}')">&gt;</a-->
		<xsl:text disable-output-escaping="yes">&amp;nbsp;&amp;nbsp;</xsl:text><a href="javascript:showPageI({$lastNumber},'{$Index}')">&gt;&gt;</a>
	</xsl:if>
	<!--xsl:if test="$portion*$linkCount+1>$lastNumber">
		<xsl:text disable-output-escaping="yes">&amp;nbsp;&gt;&amp;nbsp;&amp;nbsp;&gt;&gt;</xsl:text>
	</xsl:if-->	
	</td>
	<td align="center">
	<xsl:text disable-output-escaping="yes">&amp;nbsp;&amp;nbsp;</xsl:text>
	<xsl:value-of select="/xml/lng/NavigatorTotal"/>:<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
	<xsl:value-of select="$lastNumber"/>&#160;<xsl:value-of select="/xml/lng/NavigatorPg"/></td>
	<!-- ************************************* -->
	</tr></table>
	</xsl:if>
	</xsl:template>
</xsl:stylesheet>
