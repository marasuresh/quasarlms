<?xml version="1.0" encoding="windows-1251"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
xmlns:msxml="urn:schemas-microsoft-com:xslt"
xmlns:script="myspace">
<xsl:output method="html" indent="no"/> 
<msxml:script implements-prefix="script" language="jscript">
<![CDATA[	
	function formatDate(strDate)
	{	
		return strDate.substr(8, 2) + "." 
		+ strDate.substr(5, 2) + "." 
		+ strDate.substr(0, 4);
	}
 function persent(value, maxValue)
 {
 if (maxValue == 0)
 return 0;
 return Math.floor((value / maxValue) * 100);
 }
	function replSlash(strPath)
	{	
	 var re;
 re = /(\\+)/g;
		return strPath.replace(re, "/");
	}
 function contentType(intType)
 {
 switch (intType)
 {
 case "1":
 case "8":
 return "_string";
 break;
 case "2":
 return "_xml";
 break;
 case "3":
 return "_object";
 break;
 case "5":
 return "_url";
 break;
 case "6":
 return "_html";
 break;
 }
 }
]]>
</msxml:script>

<xsl:param name="LangPath"/>
<xsl:template match="/xml">
<script language="javascript">
function resizeFrame()
{
 theHeight = document.getElementById("contFrameId").contentWindow.document.body.scrollHeight;
 document.getElementById("contFrameId").style.height = theHeight;
}
</script>
<table cellpadding="0" cellspacing="0" width="100%" border="0">
<tr><td>
 <table cellpadding="0" cellspacing="0" width="100%" border="0" style="margin-top: 10px;" class="TestWindow">
		<xsl:choose>
		 <xsl:when test="Session">
 <tr><td valign="bottom">
	 <h3 class="cap4"><xsl:value-of select="dsCommon/Common/Course"/></h3>
		 <xsl:choose>
		 <xsl:when test="dsCommon/Common/Theme">
	 <h3 class="cap3"><xsl:value-of select="dsCommon/Common/Theme"/></h3>
		 </xsl:when>
		 <xsl:otherwise>
 <xsl:if test="dsTest/Tests/Type=1">
	 <h3 class="cap3"><xsl:value-of select="CourseTest"/></h3>
		 </xsl:if>
		 </xsl:otherwise>
		 </xsl:choose>
 <xsl:if test="dsCommon/Common/id=dsCommon/Common/StartQuestionnaire">
	 <h3 class="cap3"><xsl:value-of select="//StartQuestionnaire"/></h3>
 </xsl:if>
 <xsl:if test="dsCommon/Common/id=dsCommon/Common/FinishQuestionnaire">
	 <h3 class="cap3"><xsl:value-of select="//FinishQuestionnaire"/></h3>
 </xsl:if>
 </td></tr>
		 </xsl:when>
		 <xsl:otherwise>
 <xsl:if test="dsCommon/Common/Student">
 <tr valign="bottom" style="padding-top: 5px;">
 <td><strong><xsl:value-of select="StudentA"/>:</strong></td><td><h3 class="cap3"><xsl:value-of select="dsCommon/Common/Student"/></h3></td>
 </tr>
 </xsl:if>
		 <xsl:choose>
		 <xsl:when test="dsCommon/Common/Theme">
		 <xsl:if test="dsCommon/Common/Course">
	 <tr valign="bottom">
	 <td><strong><xsl:value-of select="CourseA"/>:</strong></td><td><h3 class="cap4"><xsl:value-of select="dsCommon/Common/Course"/></h3></td>
 </tr><tr valign="bottom" style="padding-top: 5px;">
	 <td><strong><xsl:value-of select="ThemeA"/>:</strong></td><td><h3 class="cap3"><xsl:value-of select="dsCommon/Common/Theme"/></h3></td>
 </tr>
 </xsl:if>
		 </xsl:when>
		 <xsl:otherwise>
		 <xsl:if test="dsCommon/Common/Course">
	 <tr valign="bottom">
	 <td><strong><xsl:value-of select="CourseA"/>:</strong></td><td><h3 class="cap4"><xsl:value-of select="dsCommon/Common/Course"/></h3></td>
 </tr>
 <tr valign="bottom" style="padding-top: 5px;">
	 <td>
 <xsl:if test="dsTest/Tests/Type=1">
	 <h3 class="cap3"><xsl:value-of select="CourseTest"/></h3>
	 </xsl:if>
 <xsl:if test="dsCommon/Common/id=dsCommon/Common/StartQuestionnaire">
	 <h3 class="cap3"><xsl:value-of select="//StartQuestionnaire"/></h3>
 </xsl:if>
 <xsl:if test="dsCommon/Common/id=dsCommon/Common/FinishQuestionnaire">
	 <h3 class="cap3"><xsl:value-of select="//FinishQuestionnaire"/></h3>
 </xsl:if>
	 </td><td></td>
 </tr>
 </xsl:if>
		 </xsl:otherwise>
		 </xsl:choose>
		 </xsl:otherwise>
	 </xsl:choose>
 </table>
 <hr/>
</td></tr><tr><td>
 <xsl:if test="testEnd">
 <table cellpadding="0" cellspacing="0" border="0" width="100%" class="TestWindow">
 <tr>
 <xsl:if test="dsTest/Tests/Type=2">
 <td>
 	 <h3 class="cap3"><xsl:value-of select="dsCommon/Common/Practice"/></h3>
 	 <br/><h3 class="Success"><xsl:value-of select="practiceA"/>
		 &#160;<xsl:value-of select="PracticeComplete"/>&#160;<xsl:value-of select="script:formatDate(string(dsRes/TestResult/CompletionDate))"/></h3>
	 </td>
		 </xsl:if>
 <xsl:if test="dsTest/Tests/Type=1">
 <td>
	 <xsl:if test="timeOut">
		 <h3 class="Failure"><xsl:value-of select="testTimeOut"/></h3>
	 </xsl:if>
	 <xsl:if test="testEnd[.='Complete']">
		 <h3 class="Success"><xsl:value-of select="testComplete"/>
		 &#160;<xsl:value-of select="script:formatDate(string(dsRes/TestResult/CompletionDate))"/></h3>
	 </xsl:if>
	 <xsl:if test="testEnd[.='Failure']">
		 <h3 class="Failure"><xsl:value-of select="testNotComplete"/></h3>
	 </xsl:if>
	 </td>
	 <!--td align="right">
		 <small><xsl:value-of select="TimeComplete"/></small><strong><xsl:value-of select="iTimeElapse"/></strong>
	 </td-->
		 </xsl:if>
 <xsl:if test="dsTest/Tests/Type=3 or dsTest/Tests/Type=6">
		 <h3 class="Success"><xsl:value-of select="QuestionnaireComplete"/>
		 &#160;<xsl:value-of select="script:formatDate(string(dsRes/TestResult/CompletionDate))"/></h3>
 </xsl:if>
 </tr>
 </table>
 </xsl:if>

	<xsl:if test="testEnd and dsTest/Tests/Type='1'">
 <table cellpadding="0" cellspacing="0" border="0" width="75%" class="TestWindow">
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
 <tr>
	 <td nowrap="true"><strong><xsl:value-of select="TriesA"/></strong></td>
	 <td width="10%" align="center"><xsl:value-of select="dsRes/TestResult/Tries"/></td>
	 <td width="1200px" colspan="5">&#160;</td>
 </tr>
 <tr>
	 <td nowrap="true"><strong><xsl:value-of select="AllowTriesA"/></strong></td>
	 <td width="10%" align="center"><xsl:value-of select="dsRes/TestResult/AllowTries"/></td>
	 <td width="1200px" colspan="5">&#160;</td>
 </tr>
 </table>
 <br/>
	</xsl:if>
</td></tr><tr valign="top"><td>
 <table cellSpacing="0" cellPadding="0" width="100%" align="center" border="0" class="TableList" style="margin-top: 5px;">
	 <th class="HeadCenter"><xsl:value-of select="//QuestionA"/></th>
	 <xsl:if test="dsTest/Tests/Type='2'">
	 <!--th class="HeadCenter" width="30%"><xsl:value-of select="//AnswersA"/></th-->
	 </xsl:if>
		<xsl:if test="dsTest/Tests/Type='1'">
	 <th class="HeadCenter" width="80pt"><xsl:value-of select="//PointsAnsw"/></th>
	 </xsl:if>
 <xsl:for-each select="dsQw/Qw">
 <!--xsl:if test="count(./Answer/Question/Answer) != 0"-->
 <tr valign="top">
 <td valign="center" class="Answers">
 <xsl:if test="'_url'=script:contentType(string(cType))">
 <iframe name="contFrame" width="100%" id="contFrameId" onresize="resizeFrame()" onLoad="resizeFrame()" frameborder="no" height="100%" align="top" scrolling="no">
		 <xsl:choose>
		 <xsl:when test="Content">
		 <xsl:attribute name="src">
		 <xsl:value-of select="script:replSlash(string(//cRoot))"/>
		 <xsl:value-of select="script:replSlash(string(//dsCommon/Common/DiskFolder))"/>
		 <xsl:value-of select="script:replSlash(string(Content))"/>
		 </xsl:attribute>
		 </xsl:when>
		 <xsl:otherwise>
		 <xsl:attribute name="src">
		 <!--<xsl:value-of select="$LangPath"/>NoContent.htm-->about:blank
		 </xsl:attribute>
		 </xsl:otherwise>
		 </xsl:choose>
 </iframe>
 </xsl:if>
 <xsl:if test="'_string'=script:contentType(string(cType))">
 <a><xsl:value-of select="Content"/></a>
 </xsl:if>
 <xsl:if test="'_html'=script:contentType(string(cType))">
 <a><xsl:copy-of select="Content"/></a>
 </xsl:if>
 <xsl:if test="'_object'=script:contentType(string(cType))">
 <xsl:if test="Content">
 <OBJECT classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" width="100%" height="200px"
 codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,79,0"
 id="ObjectId" VIEWASTEXT="true">
 <PARAM NAME="movie" >
		 <xsl:attribute name="VALUE">
		 <xsl:value-of select="script:replSlash(string(//cRoot))"/>
		 <xsl:value-of select="script:replSlash(string(//dsCommon/Common/DiskFolder))"/>
		 <xsl:value-of select="script:replSlash(string(Content))"/>
		 </xsl:attribute>
 </PARAM>
 <PARAM NAME="quality" VALUE="high"/> <PARAM NAME="bgcolor" VALUE="#FFFFFF"/>
 <EMBED width="100%" height="200px" quality="high" bgcolor="#FFFFFF" NAME="Object1"
 TYPE="application/x-shockwave-flash" PLUGINSPAGE="http://www.macromedia.com/go/getflashplayer">
		 <xsl:attribute name="src">
		 <xsl:value-of select="script:replSlash(string(//cRoot))"/>
		 <xsl:value-of select="script:replSlash(string(//dsCommon/Common/DiskFolder))"/>
		 <xsl:value-of select="script:replSlash(string(Content))"/>
		 </xsl:attribute>
 </EMBED>
 </OBJECT>
 </xsl:if>
 </xsl:if>

 		 <xsl:call-template name="TestQw">
			 <xsl:with-param name="Qw" select="./Answer/Question"/>
			 <xsl:with-param name="QwBase" select="."/>
		 </xsl:call-template>
 </td>
		 <xsl:if test="//dsTest/Tests/Type='1'">
 <td align="center" valign="center" class="Answers">
 <p><xsl:value-of select="aPoints"/></p>
 </td>
 </xsl:if>
 </tr>
 <!--/xsl:if-->
 </xsl:for-each>
 </table>
</td></tr>
</table>
</xsl:template>

<xsl:template name="TestQw">
	<xsl:param name="Qw"/>
	<xsl:param name="QwBase"/>
 <xsl:if test="$Qw/@type='single'">
 <xsl:for-each select="$Qw/Answer">
 		<p><input disabled="true" type="radio" name="{$QwBase/id}">
 		<xsl:if test="@selected"><xsl:attribute name="checked"/></xsl:if>
 		</input>
 		&#160;<xsl:value-of select="."/></p>
 </xsl:for-each>
 </xsl:if>
 <xsl:if test="$Qw/@type='multiple'">
 <xsl:for-each select="$Qw/Answer">
 		<p><input disabled="true" type="checkbox" name="{$QwBase/id}">
 		<xsl:if test="@selected"><xsl:attribute name="checked"/></xsl:if>
 		</input>
 		&#160;<xsl:value-of select="."/></p>
 </xsl:for-each>
 </xsl:if>
 <p><xsl:if test="$Qw/@type='textbox'"><xsl:value-of select="$Qw/Answer/result"/>
 </xsl:if></p>
</xsl:template>

</xsl:stylesheet>

 