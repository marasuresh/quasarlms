<?xml version="1.0" encoding="windows-1251"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
xmlns:msxml="urn:schemas-microsoft-com:xslt"
xmlns:script="myspace"
xmlns:cs="urn:my-scripts">
<xsl:output method="html" indent="no"/> 
<msxml:script implements-prefix="cs" language="CSharp">
<![CDATA[		
   public string makeUrl(string croot, string folder, string path)
   {
      string newPath = croot+folder+path;
      return newPath.Replace("\\", "/");
   }
]]>
</msxml:script>
<msxml:script implements-prefix="script" language="jscript">
<![CDATA[	
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
   function showTime(stime)
   {
	   var iHours=Math.floor(stime/3600);
	   if (iHours < 0)
	      iHours=0;
	   var sHours = iHours + "";
	   if(sHours.length==1){
		   sHours="0" + sHours;
	   }
	   var iMinutes=Math.floor(stime%3600/60);
	   if (iMinutes < 0)
	      iMinutes=0;
	   var sMinutes = iMinutes + "";
	   if(sMinutes.length==1){
		   sMinutes="0" + sMinutes;
	   }
	   var iSeconds=stime%60;
	   if (iSeconds < 0)
	      iSeconds=0;
	   var sSeconds=iSeconds + "";
	   if(sSeconds.length==1){
		   sSeconds="0" + sSeconds;
	   }
   	return iHours + ":" + sMinutes + ":" + sSeconds;
   }
]]>
</msxml:script>

<xsl:param name="LangPath"/>
<xsl:template match="/xml">
<script src="xsl/TestWork.js" language="javascript"/>



<xsl:variable name="osrc">
	<xsl:value-of select="script:replSlash(string(//dsCommon/Common/cRoot))"/>
	<xsl:value-of select="script:replSlash(string(//dsCommon/Common/DiskFolder))"/>
	<xsl:value-of select="script:replSlash(string(dsQw/Qw/Content))"/>
</xsl:variable>
<table cellpadding="0" height="100%" cellspacing="0" width="100%" border="0">
<tr><td>
   <table cellpadding="0" cellspacing="0" width="100%" border="0" style="margin-top: 0px;" class="TestWindow">
   <tr><td valign="bottom">
		<xsl:choose>
		   <xsl:when test="dsTest/Tests/Type='2'">
	         <h3 class="cap4"><xsl:value-of select="dsCommon/Common/Course"/></h3>
	         <h3 class="cap3"><xsl:value-of select="practiceA"/>
	         <xsl:if test="dsCommon/Common/Theme">:&#160;"<xsl:value-of select="dsCommon/Common/Theme"/>"</xsl:if>
	         </h3>
		   </xsl:when>
		   <xsl:when test="dsCommon/Common/Theme">
	         <h3 class="cap4"><xsl:value-of select="dsCommon/Common/Course"/></h3>
	         <h3 class="cap3"><xsl:value-of select="dsCommon/Common/Theme"/></h3>
		   </xsl:when>
		   <xsl:when test="dsTest/Tests/Type='1'">
	         <h3 class="cap4"><xsl:value-of select="dsCommon/Common/Course"/></h3>
	         <h3 class="cap3"><xsl:value-of select="CourseTest"/></h3>
		   </xsl:when>
		   <xsl:otherwise>
		   </xsl:otherwise>
		</xsl:choose>
   </td>
	   <xsl:if test="dsTest/Tests/Type='1' and count(testEnd)=0">
	      <td valign="bottom" align="right"><small><xsl:value-of select="qRemain"/>&#160;</small><strong><xsl:value-of select="dsQw/Qw/Remain"/></strong></td>
	      <td valign="bottom" align="right"><small><xsl:value-of select="TimeLeft"/></small></td>
		   <td valign="bottom" align="right"><strong><a id="oTimer" timeLeft="{iTimeLeft}"></a></strong></td>
	   </xsl:if>
   </tr>
   </table>
   <hr/>
</td></tr><tr>
   <xsl:if test="'_url'=cType">
   <xsl:attribute name="height">100%</xsl:attribute>
   </xsl:if>
   <td>
   <xsl:if test="testEnd">
      <table cellpadding="0" cellspacing="0" border="0" width="100%" class="TestWindow">
         <tr>
		   <xsl:choose>
		      <xsl:when test="dsTest/Tests/Type='2'">
               <td>
   	            <h3 class="Success"><xsl:value-of select="practiceEnd"/></h3>
	            </td>
		      </xsl:when>
		      <xsl:otherwise>
               <td>
	                <xsl:if test="timeOut">
		               <h3 class="Failure"><xsl:value-of select="testTimeOut"/></h3>
                     <xsl:choose>
	                     <xsl:when test="dsCommon/Common/Theme">
	                        <p class="help"><xsl:value-of select="Help1"/></p>
	                     </xsl:when>
	                     <xsl:otherwise>
	                          <p class="help"><xsl:value-of select="Help3"/></p>
	                     </xsl:otherwise>
                     </xsl:choose>
	               </xsl:if>
	               <xsl:if test="testEnd[.='Complete']">
		               <h3 class="Success"><xsl:value-of select="testComplete"/></h3>
                     <xsl:choose>
	                     <xsl:when test="dsCommon/Common/Theme">
	                        <p class="help"><xsl:value-of select="Help2"/></p>
	                     </xsl:when>
	                     <xsl:otherwise>
	                          <p class="help"><xsl:value-of select="Help4"/></p>
	                     </xsl:otherwise>
                     </xsl:choose>
	               </xsl:if>
	               <xsl:if test="testEnd[.='Failure']">
		               <h3 class="Failure"><xsl:value-of select="testNotComplete"/></h3>
                     <xsl:choose>
	                     <xsl:when test="dsCommon/Common/Theme">
	                        <p class="help"><xsl:value-of select="Help1"/></p>
	                     </xsl:when>
	                     <xsl:otherwise>
	                          <p class="help"><xsl:value-of select="Help3"/></p>
	                     </xsl:otherwise>
                     </xsl:choose>
	               </xsl:if>
	            </td>
	            <td align="right">
	               <xsl:if test="iTimeElapse">
		               <small><xsl:value-of select="TimeComplete"/></small><strong><xsl:value-of select="script:showTime(string(dsRes/TestResult/TestDuration))"/></strong>
		            </xsl:if>
	            </td>
		      </xsl:otherwise>
		   </xsl:choose>
         </tr>
      </table>
   </xsl:if>

   <xsl:choose>
	   <xsl:when test="testEnd!=''">
         <table cellpadding="0" cellspacing="0" border="0" width="75%" class="TestWindow">
         <xsl:if test="dsTest/Tests/Type='1'">
            <xsl:if test="dsTest/Tests/Show='true'">
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
         </xsl:if>
         </table>
      <hr/>
	   </xsl:when>
	   <xsl:otherwise>
		   <xsl:choose>
		      <xsl:when test="'_string'=cType">
               <h3 class="cap3"><xsl:value-of select="dsQw/Qw/Content"/></h3>
		      </xsl:when>
		      <xsl:when test="'_html'=cType">
               <h3 class="cap3"><xsl:copy-of select="dsQw/Qw/Content"/></h3>
		      </xsl:when>
		      <xsl:when test="'_xml'=cType">
               <h3 class="cap3"><xsl:copy-of select="dsQw/Qw/Content"/></h3>
		      </xsl:when>
		      <xsl:when test="'_object'=cType">
               <OBJECT classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" width="100%" height="500px"
               codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,79,0"
               id="ObjectId" VIEWASTEXT="true">
               <PARAM NAME="movie" VALUE="{$osrc}"/>
               <PARAM NAME="quality" VALUE="high"/> <PARAM NAME="bgcolor" VALUE="#FFFFFF"/>
               <EMBED src="{$osrc}" height="500px" width="100%" quality="high" bgcolor="#FFFFFF" NAME="Object1"
               TYPE="application/x-shockwave-flash" PLUGINSPAGE="http://www.macromedia.com/go/getflashplayer"></EMBED>
               </OBJECT>
		      </xsl:when>
		      <xsl:when test="'_url'=cType">
               <iframe name="contFrame" width="100%" id="contFrameId" frameborder="no" align="top" onload="javascript:resizeFrame('contFrameId');" onresize="javascript:resizeFrame('contFrameId')">
		            <xsl:choose>
		               <xsl:when test="dsQw/Qw/Content">
		                  <xsl:attribute name="src">
		                     <!--contentpage.aspx?path=<xsl:value-of select="cs:makeUrl(string(//dsCommon/Common/PublicRoot),string(//dsCommon/Common/DiskFolder), string(dsQw/Qw/Content))"/>-->
		                     <xsl:value-of select="cs:makeUrl(string(//dsCommon/Common/cRoot),string(//dsCommon/Common/DiskFolder), string(dsQw/Qw/Content))"/>
		                  </xsl:attribute>
		               </xsl:when>
		               <xsl:otherwise>
		                  <xsl:attribute name="src">
		                     <xsl:value-of select="$LangPath"/>NoContent.htm
		                  </xsl:attribute>
		               </xsl:otherwise>
		            </xsl:choose>
               </iframe>
		      </xsl:when>
		      <xsl:otherwise>
               <h3 class="cap3"><xsl:value-of select="dsQw/Qw/Content"/></h3>
		      </xsl:otherwise>
		   </xsl:choose>
	   </xsl:otherwise>
   </xsl:choose>
</td></tr><tr height="100%" valign="top"><td>
   <table cellSpacing="15px" cellPadding="0" width="100%" align="center" border="0" class="RegForm">
      <xsl:if test="'none'!=hType">
         <tr valign="top">
            <td width="100%" valign="top" colspan="2">
            <table width="100%" border="0" class="Hints">
               <xsl:if test="dsQw/Qw/ShortHint">
                  <tr><td>
                     <a openState="false" id="sHintButton" href="javascript:sHint()">
                        <xsl:value-of select="sHintA"/>
                     </a>
                     <br/><a id="sHintArea" style="display:none;"><xsl:value-of select="dsQw/Qw/ShortHint"/></a>
                  </td></tr>
               </xsl:if>
               <xsl:if test="'both'=hType and dsQw/Qw/LongHint">
                  <tr><td>
                     <a openState="false" id="lHintButton" href="javascript:lHint()">
                        <xsl:value-of select="lHintA"/>
                     </a>
                     <br/><a id="lHintArea" style="display:none;"><xsl:copy-of select="dsQw/Qw/LongHint"/></a>
                  </td></tr>
               </xsl:if>
            </table>
            </td>
         </tr>
      </xsl:if>
      <form name="ContinueForm" value="Continue" method="post" action="">
      <input type="hidden" name="objRes"/>
      <tr valign="top">
         <td width="100%" valign="top" colspan="2" class="Answers" style="PADDING-TOP: 15px">
            <input id="qwidParam" name="qwId" type="hidden" value="{dsQw/Qw/id}"/>
	         <xsl:choose>
		         <xsl:when test="dsQw/Qw/Answer/Question/@type = 'textbox' or count(dsQw/Qw/Answer/Question/Answer[string(.)]) > 0">
 		            <xsl:call-template name="TestQw">
			            <xsl:with-param name="Qw" select="dsQw/Qw/Answer/Question"/>
		            </xsl:call-template>
		         </xsl:when>
		         <xsl:otherwise>
 		            <xsl:call-template name="TestQw">
			            <xsl:with-param name="Qw" select="dsQw/Qw/AnswerDef/Question"/>
		            </xsl:call-template>
		         </xsl:otherwise>
	         </xsl:choose>
         </td>
      </tr>
      <tr valign="top"><td class="btnTopicPost" align="left" valign="top">
		   <xsl:choose>
		      <xsl:when test="testEnd">
               <xsl:if test="testEnd='Failure'">
                  <input type="submit" name="RetryButton" value="{retry}"/>
               </xsl:if>
		      </xsl:when>
		      <xsl:otherwise>
               <input type="submit" name="ContinueButton" value="{Continue}"/>
		      </xsl:otherwise>
		   </xsl:choose>
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
      </td></tr>
	   </form>
   </table>
</td></tr>
</table>

</xsl:template>

<xsl:template name="TestQw">
	<xsl:param name="Qw"/>
   <xsl:if test="$Qw/@type='single'">
      <xsl:for-each select="$Qw/Answer[string(.)]">
   		<br/><input class="clear" type="radio" name="{$Qw/@type}" value="{string(position())}" />
   		&#160;<a><xsl:value-of select="."/></a>
      </xsl:for-each>
   </xsl:if>
   <xsl:if test="$Qw/@type='multiple'">
      <xsl:for-each select="$Qw/Answer[string(.)]">
   		<br/><input style="width:20px;background-color: #FFFFFF;border: 0px;" type="checkbox" name="{$Qw/@type}{string(position())}" value="check"/>
   		<!-- <br/><input class="bclear" type="checkbox" name="{$Qw/@type}{string(position())}" value="check"/> -->
   		&#160;<a><xsl:value-of select="."/></a>
      </xsl:for-each>
   </xsl:if>
   <xsl:if test="$Qw/@type='textbox'">
      <textarea rows="5" cols="40" name="{$Qw/@type}" maxlength="512" wrap="soft" style="overflow:hidden"/>
   </xsl:if>
</xsl:template>

</xsl:stylesheet>

  