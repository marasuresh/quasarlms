<?xml version="1.0" encoding="windows-1251"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
xmlns:msxml="urn:schemas-microsoft-com:xslt"
xmlns:script="myspace">
<xsl:output method="html" indent="no"/> 
<msxml:script implements-prefix="script" language="jscript">
<![CDATA[	
 function persent(value, maxValue)
 {
 if (maxValue == 0)
 return 0;
 return Math.floor((value / maxValue) * 100);
 }
]]>
</msxml:script>

<xsl:template match="/xml">
<xsl:choose>
	<xsl:when test="dsCommon/Common/Theme">
	 <h3 class="cap4"><xsl:value-of select="dsCommon/Common/Course"/></h3>
	 <h3 class="cap3"><xsl:value-of select="dsCommon/Common/Theme"/></h3>
	 <!--Если тест не сдан и нет больше попыток-->
 <xsl:if test="dsRes/TestResult/AllowTries=0">
 <p><xsl:value-of select="Help5"/></p>
 </xsl:if> 
	</xsl:when>
	<xsl:otherwise>
	 <h3 class="cap4"><xsl:value-of select="dsCommon/Common/Course"/></h3>
	 <h3 class="cap3"><xsl:value-of select="CourseTest"/></h3>
	 <!--Если тест не сдан и нет больше попыток-->
 <xsl:if test="dsRes/TestResult/AllowTries=0">
 <p><xsl:value-of select="Help5"/></p>
 </xsl:if> 
	</xsl:otherwise>
</xsl:choose>
<hr/>
<table cellpadding="0" cellspacing="0" border="0" width="100%">
	<xsl:if test="0!=count(Theme)">
	 <tr>
		<td><h3 class="cap3"><xsl:value-of select="ThemeA"/>:&#160;<xsl:value-of select="Theme"/></h3></td>
		<td class="TestWindow" align="right"><small><xsl:value-of select="TestDurationA"/>&#160;<strong><xsl:value-of select="dsTest/Tests/Duration"/>&#160;<xsl:value-of select="min"/></strong></small></td>
	 </tr>
	</xsl:if>
</table> 

<table cellpadding="0" cellspacing="0" border="0" width="75%" class="TestWindow" >
<xsl:if test="dsTest/Tests/Show='true' and dsRes/TestResult/Points!=''">
 <tr>
	 <td nowrap="true"><strong><xsl:value-of select="//PointsNeed"/>:</strong></td>
	 <td nowrap="true" width="10%" align="center"><strong><xsl:value-of select="dsTest/Tests/Points"/></strong></td>
	 <td width="3" rowspan="3"><img src="images/empty.gif" width="3" height="1"/></td>
	 <td bgcolor="#E4E4E4" rowspan="3"><img src="images/empty.gif" width="1" height="1"/></td>
	 <td width="1200px">
		 <table cellpadding="0" cellspacing="0" border="0" width="{script:persent(string(dsTest/Tests/Points), string(dsTest/Tests/MaxPoints))}%">
		 <tr><td bgcolor="#FFCC01"><img src="images/empty.gif" width="1" height="7"/></td></tr></table>
	 </td>
	 <td bgcolor="#E4E4E4" rowspan="3"><img src="images/empty.gif" width="1" height="1"/></td>
	 <td width="20" rowspan="3"><img src="images/empty.gif" width="20" height="1"/></td>
 </tr>
 <tr>
	 <td nowrap="true"><strong><xsl:value-of select="//Points"/>:</strong></td>
	 <td width="10%" align="center"><strong><xsl:value-of select="dsRes/TestResult/Points"/></strong></td>
	 <td width="1200px">
		 <table cellpadding="0" cellspacing="0" border="0" width="{script:persent(string(dsRes/TestResult/Points), string(dsTest/Tests/MaxPoints))}%">
		 <tr><td bgcolor="#CD2F62"><img src="images/empty.gif" width="1" height="7"/></td></tr></table>
	 </td>
 </tr>
 <tr>
	 <td nowrap="true"><strong><xsl:value-of select="//PointsMax"/>:</strong></td>
	 <td width="10%" align="center"><strong><xsl:value-of select="dsTest/Tests/MaxPoints"/></strong></td>
	 <td width="1200px">
		 <table cellpadding="0" cellspacing="0" border="0" width="100%"><tr><td bgcolor="#759CE2"><img src="images/empty.gif" width="1" height="7"/></td></tr></table>
	 </td>
 </tr>
 <tr>
	 <td>&#160;</td>
	 <td>&#160;</td>
	 <td colspan="5">
		 <table cellpadding="0" cellspacing="0" border="0" width="100%" align="center" class="TestWindow">
		 <tr>
			 <td class="Scala">0%</td><td width="100%">&#160;</td><td class="Scala">100%</td>
		 </tr>
		 </table>
	 </td>
 </tr>
 <xsl:if test="dsTest/Tests/ShowThemes='true'">
 <xsl:if test="count(dsTPoints/TPoints) > 0">
 <tr style="padding-top:15px;padding-left:25pt;">
 <td colspan="7"><h3 class="cap3"><xsl:value-of select="//Themes"/></h3></td>
 </tr>
 </xsl:if>
	 <xsl:for-each select="dsTPoints/TPoints">
	 <tr style="padding-top:15px;"><td colspan="7"><strong><xsl:value-of select="Theme"/></strong></td></tr>
 <tr>
	 <td nowrap="true"><strong><xsl:value-of select="//Points"/>:</strong></td>
	 <td width="10%" align="center"><strong><xsl:value-of select="ThemePoints"/></strong></td>
	 <td width="3" rowspan="2"><img src="images/empty.gif" width="3" height="1"/></td>
	 <td bgcolor="#E4E4E4" rowspan="2"><img src="images/empty.gif" width="1" height="1"/></td>
	 <td width="1200px">
		 <table cellpadding="0" cellspacing="0" border="0" width="{script:persent(string(ThemePoints), string(ThemeMaxPoints))}%">
		 <tr><td bgcolor="#CD2F62"><img src="images/empty.gif" width="1" height="7"/></td></tr></table>
	 </td>
	 <td bgcolor="#E4E4E4" rowspan="2"><img src="images/empty.gif" width="1" height="1"/></td>
	 <td width="20" rowspan="2"><img src="images/empty.gif" width="20" height="1"/></td>
 </tr>
 <tr>
	 <td nowrap="true"><strong><xsl:value-of select="//PointsMax"/>:</strong></td>
	 <td width="10%" align="center"><strong><xsl:value-of select="ThemeMaxPoints"/></strong></td>
	 <td width="1200px">
		 <table cellpadding="0" cellspacing="0" border="0" width="100%"><tr><td bgcolor="#759CE2"><img src="images/empty.gif" width="1" height="7"/></td></tr></table>
	 </td>
 </tr>
 <tr>
	 <td>&#160;</td>
	 <td>&#160;</td>
	 <td colspan="5">
		 <table cellpadding="0" cellspacing="0" border="0" width="100%" align="center" class="TestWindow">
		 <tr>
			 <td class="Scala">0%</td><td width="100%">&#160;</td><td class="Scala">100%</td>
		 </tr>
		 </table>
	 </td>
 </tr>
	 </xsl:for-each>
 </xsl:if>
</xsl:if>
<tr>
	<td nowrap="true"><strong><xsl:value-of select="TriesA"/></strong></td>
	<td width="10%" align="center"><xsl:value-of select="dsRes/TestResult/Tries"/></td>
	<td width="1200px" colspan="5">&#160;</td>
</tr>
<tr>
	<td nowrap="true"><strong><xsl:value-of select="AllowTriesA"/></strong></td>
	<td width="10%" align="center"><strong><xsl:value-of select="dsRes/TestResult/AllowTries"/></strong></td>
	<td width="1200px" colspan="5">&#160;</td>
</tr>
</table>

<table cellpadding="0" cellspacing="0" border="0" width="100%" class="TestWindow">
	<tr><td class="RegForm" align="left">
 <hr/>
 <form name="TestStartForm" value="Start" method="post" action="" class="btnTopicPost">
 <xsl:if test="dsRes/TestResult/Complete!='true'">
 <br/>
 <xsl:if test="not(disabled)">
 <input name="StartButton" type="submit" value="{Start}">
	 	 <xsl:if test="disabled"><xsl:attribute name="disabled">true</xsl:attribute></xsl:if>
		 </input>
		 </xsl:if>
	 <xsl:if test="skip">
 	 &#160;&#160;<input name="SkipButton" type="submit" value="{Skip}"/>
		 </xsl:if>
		</xsl:if>
 </form>
 </td></tr>
</table>

<!--Заключительная анкета-->
<xsl:if test="isCourseTest and testEnd='Complete' and dsCommon/Common/qId and not(dsCommon/Common/qRes)">
 <table cellpadding="0" cellspacing="0" width="100%" border="0" style="margin-top: 0px;" class="InnerTable">
 <tr><td>
 <p><xsl:value-of select="Questionnaire/Lable"/>&#160;&#160;&#160;
 <a href="javascript:qwHref('{string(dsCommon/Common/qId)}')">
 <xsl:value-of select="Questionnaire/hrefLable"/></a></p> 
 </td></tr>
 </table>
</xsl:if>

</xsl:template>
</xsl:stylesheet>