<?xml version="1.0" encoding="windows-1251"?>
<xsl:stylesheet version="1.0" xmlns:xsl='http://www.w3.org/1999/XSL/Transform'>
<xsl:output method="html" omit-xml-declaration="yes" indent="no"/>

<xsl:template name="formatDate">
	<xsl:param name="strDate"/>
	<!--2003-06-11T17:41:37.0630000+03:00
		 123456789012345678901234567890123
		 0 1 2 3-->
 <nobr>
	<xsl:value-of select="substring($strDate, 9, 2)"/>.
 <xsl:value-of select="substring($strDate, 6, 2)"/>.
	<xsl:value-of select="substring($strDate, 1, 4)"/></nobr>
</xsl:template>

<xsl:template name="MonthName">
	<xsl:param name="nMonth"/>
		<xsl:if test="$nMonth='12'">12</xsl:if>
		<xsl:if test="$nMonth='01'">01</xsl:if>
		<xsl:if test="$nMonth='02'">02</xsl:if>
		<xsl:if test="$nMonth='03'">03</xsl:if>
		<xsl:if test="$nMonth='04'">04</xsl:if>
		<xsl:if test="$nMonth='05'">05</xsl:if>
		<xsl:if test="$nMonth='06'">06</xsl:if>
		<xsl:if test="$nMonth='07'">07</xsl:if>
		<xsl:if test="$nMonth='08'">08</xsl:if>
		<xsl:if test="$nMonth='09'">09</xsl:if>
		<xsl:if test="$nMonth='10'">10</xsl:if>
		<xsl:if test="$nMonth='11'">11</xsl:if>
</xsl:template>

<xsl:template match="/">
 <script language="javascript">
 function CouseRef(cid)
 {
 if (cid != "")
 {
 AddParameter("cid", cid);
 AddParameter("cset", "CourseIntro");

 applyParameters();
 }
 }
 </script>
 
 <!--Треки курсов-->
 <!--xsl:if test="count(xml/CoursesList/DataSet/Courses)>0"-->
 <h3 class="cap4"><xsl:value-of select="xml/CTracks"/></h3>
 <xsl:variable name="head" select="xml/CoursesList/Header"/>
 <xsl:variable name="ds" select="xml/CoursesList/DataSet"/>

 <ol>
 <xsl:for-each select="xml/DataSet/Tracks">
 <xsl:variable name="id" select="TrackId"/>
 <h3>
	 <li>
		 <xsl:value-of select="Track"/>
		 &#160;<small>
			 <xsl:value-of select="TDescription"/>
		 </small>
	 </li>
 </h3>

 <table cellspacing="0" cellpadding="3" border="0" align="center" class="TableList" style="padding-right: 1pt;margin-right: 1pt;">
 <th nowrap="true" width="80%">
 <xsl:value-of select="$head/Name"/>
 </th>
 <th width="20%" nowrap="true"><xsl:value-of select="$head/Type"/></th>
 <th width="60pt" nowrap="true" style="text-transform: none;"><xsl:value-of select="$head/Cost1"/></th>
 <th width="60pt" nowrap="true" style="text-transform: none;"><xsl:value-of select="$head/Cost2"/></th>
 <th width="60pt" nowrap="true"><xsl:value-of select="$head/Duration"/></th>
 <colgroup>
	 <col/>
	 <col align="left"/>
	 <col nowrap="true" align="center"/>
	 <col nowrap="true" align="center"/>
	 <col nowrap="true" align="center"/>
 </colgroup>
 <xsl:for-each select="$ds/Courses[TrackId = $id]">
 <tr>
	 	 <xsl:if test="(position() div 2)=ceiling(position() div 2)"><xsl:attribute name="bgcolor">#F6F6F6</xsl:attribute></xsl:if>					
		 <xsl:if test="(position() div 2)!=ceiling(position() div 2)"><xsl:attribute name="bgcolor">#FFFFFF</xsl:attribute></xsl:if>
 <td>
		 <!--xsl:choose>
		 <xsl:when test="cid!=''"-->
	 <a href="javascript:CouseRef('{cid}')"><xsl:value-of select="Name"/>
 <xsl:if test="CPublic='true'">
	 &#160;(<xsl:value-of select="$head/Public"/>)
	 </xsl:if>
		 <br/><small><xsl:value-of select="Description"/></small>
	 </a>
		 <!--/xsl:when>
		 <xsl:otherwise>
	 <a ><xsl:value-of select="Name"/>
 <xsl:if test="CPublic='true'">
	 &#160;(<xsl:value-of select="$head/Public"/>)
	 </xsl:if>
		 <br/><small><xsl:value-of select="Description"/></small>
	 </a>
		 </xsl:otherwise>
		 </xsl:choose-->
		 </td>
	 <td>
		 <xsl:if test="Type">
		 <small><xsl:value-of select="Type"/></small>
		 </xsl:if>
	 </td>
	 <td>
 <xsl:if test="Cost1 and Cost1!=0">
	 <small><xsl:value-of select="Cost1"/></small>
	 </xsl:if>
	 </td>
	 <td>
 <xsl:if test="Cost2 and Cost2!=0">
	 <small><xsl:value-of select="Cost2"/></small>
	 </xsl:if>
	 </td>
	 <td>
 <xsl:if test="Duration!=''">
	 <small><xsl:value-of select="Duration"/></small>
	 </xsl:if>
	 </td>
 </tr>
 </xsl:for-each>
 </table>

 </xsl:for-each>
 </ol>
 
	<!--/xsl:if-->
 
</xsl:template>



</xsl:stylesheet>