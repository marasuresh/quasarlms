<?xml version="1.0" encoding="windows-1251"?>
<xsl:stylesheet version="1.0" xmlns:xsl='http://www.w3.org/1999/XSL/Transform'
xmlns:msxml="urn:schemas-microsoft-com:xslt" xmlns:script="myspace">
<xsl:output method="html"/>

<xsl:template match="/xml">
	<script language="javascript">
	function goto(name)
	{
		document.location.hash = "#"+name;
	}
	</script>
	<table cellSpacing="0" cellPadding="0" width="100%" align="center" border="0" class="VocabularIndex">
	 <xsl:if test="0!=count(Course)">
		 <tr><td><h3 class="cap4"><xsl:value-of select="Vocabulary"/>:&#160;<xsl:value-of select="Course"/></h3></td></tr>
	 </xsl:if>
	 <tr><td>&#160;</td></tr>
	 <tr>
		<form>
		<td class="Anchors">
		<xsl:for-each select="Symbols/letter">
		<input type="button" value="{symbol}" onclick="goto('{id}');"/>
		</xsl:for-each>
		</td>
		</form>
	 </tr>
	</table>
	<table cellSpacing="0" cellPadding="0" width="100%" align="center" border="0" class="VocabularTable">
	<!--tr><td><h3 class="cap6"><xsl:value-of select="SymbolsA"/></h3></td></tr-->
	<xsl:for-each select="Symbols/letter">
		<tr><td>
		<xsl:if test="position()>1"><hr/></xsl:if>
		<a><xsl:attribute name="name"><xsl:value-of select="id"/></xsl:attribute></a><h3 class="cap7"><xsl:value-of select="symbol"/></h3>
		<blockquote>
		<xsl:for-each select="term">
		<p name="{id}{Abbr}"><strong>
 	<xsl:value-of select="name"/>
		</strong><xsl:text disable-output-escaping="yes">&amp;nbsp;&amp;#151;&amp;nbsp;</xsl:text>
		 <xsl:choose>
		 <xsl:when test="nameEN and Abbr!='EN '">
		 <a href="#{id}EN "><xsl:value-of select="nameEN"/></a>
		 </xsl:when>
		 <xsl:otherwise>
		 <xsl:value-of select="text"/>
		 </xsl:otherwise>
		 </xsl:choose>
		</p>
		</xsl:for-each>
		</blockquote>
		</td></tr>
	</xsl:for-each>
	</table>
</xsl:template>

</xsl:stylesheet>