<?xml version="1.0" encoding="windows-1251"?>
<xsl:stylesheet version="1.0" xmlns:xsl='http://www.w3.org/1999/XSL/Transform'
xmlns:msxml="urn:schemas-microsoft-com:xslt"
xmlns:script="myspace">
<xsl:output method="html"/> 
<msxml:script implements-prefix="script" language="jscript">
<![CDATA[		
 function dateFont(isFailure)
 {
 if (isFailure == "true")
 return "color: #CD2F62;";
 return "";
 }
 function dateAlt(isFailure, alt, failAlt)
 {
 return alt;
 if (isFailure == "true")
 return failAlt;
 return alt;
 }
	function formatDate(strDate)
	{	
		return strDate.substr(8, 2) + "." 
		+ strDate.substr(5, 2) + "." 
		+ strDate.substr(0, 4);
	}
]]>
</msxml:script>

<xsl:template match="/">
 <script language="javascript">
 function sendChecked()
 {
 document.courseCheck.submit();
 }
 function sendUnchecked()
 {
 document.courseUncheck.submit();
 }

function CouseRef(cid){
	if (cid != ""){
		AddParameter("cid", cid);
		AddParameter("cset", "CourseIntro");
		applyParameters();
	}
}
 </script>
	<p>
	<label for="searchKeyword"><xsl:value-of select="xml/EnterKeywords"/>:&#160;
	</label>
	<input	value="{xml/searchStr}"
			id="searchKeyword"
			name="keywords"
			type="text" />
	<input value="{xml/Search}" type="submit"/>
	</p>
	
	<xsl:if test="xml/NotFound">
		<h3 class="failure"><xsl:value-of select="xml/NotFoundA"/></h3>
	</xsl:if>
	
	<xsl:if test="0!=count(xml/tableExisdataTRequests/dataSet/Request)">
		<br/><h3 class="cap2"><xsl:value-of select="xml/tableExisdataTRequests/Caption"/></h3>
 <p class="help"><xsl:value-of select="xml/Help1"/><br/>
 <xsl:value-of select="xml/Help2"/></p>

 <input type="hidden" value="courseUncheck" name="formAction"/>
 <table cellspacing="0" cellpadding="3" border="1" width="100%" align="center" class="TableList">
 <xsl:for-each select="xml/tableExisdataTRequests/tableHeader/dataTD">
	 <th><xsl:value-of select="."/></th>
	 </xsl:for-each>
	 <xsl:for-each select="xml/tableExisdataTRequests/dataSet/Request">
 <tr>
 	 <xsl:if test="(position() div 2)=ceiling(position() div 2)"><xsl:attribute name="bgcolor">#F6F6F6</xsl:attribute></xsl:if>					
	 <xsl:if test="(position() div 2)!=ceiling(position() div 2)"><xsl:attribute name="bgcolor">#FFFFFF</xsl:attribute></xsl:if>
	 <td width="80" align="center" nowrap="true"><input type="checkbox" name="{id}"/></td>
	 <td width="100" align="center" nowrap="true">
	 <a><xsl:value-of select="script:formatDate(string(StartDate))"/></a>
	 </td>
		 <xsl:choose>
		 <xsl:when test="Code!=''">
	 <td width="40%" title="{description}"><a href="javascript:CouseRef('{cId}')"><xsl:value-of select="name"/></a></td>
		 </xsl:when>
		 <xsl:otherwise>
	 <td width="40%" title="{description}"><a><xsl:value-of select="name"/></a></td>
		 </xsl:otherwise>
		 </xsl:choose>
	 <td width="55%"><xsl:value-of select="Comments"/></td>
 </tr>
 </xsl:for-each>
 </table>
 <p align="right"><a href="javascript:sendUnchecked()"><xsl:value-of select="//deleteSenderCaption"/></a></p>

 
 <hr/>
 </xsl:if>
 
 
	<xsl:if test="count(xml/tableAccessibleCourses/dataSet/Courses) > 0">
		<h3 class="cap2"><xsl:value-of select="xml/tableAccessibleCourses/Caption"/>
			<xsl:if test="xml/Found">&#160;(<xsl:value-of select="xml/Founds"/>)</xsl:if></h3>
		
		<p class="help"><xsl:value-of select="xml/tableAccessibleCourses/Note"/></p>
		
		<input	type="hidden"
				value="courseCheck"
				name="formAction"/>
		
		<table	cellspacing="0"
				cellpadding="3"
				border="1"
				width="100%"
				align="center"
				class="TableList">
			<xsl:for-each select="xml/tableAccessibleCourses/tableHeader/dataTD">
				<th><xsl:value-of select="."/></th>
			</xsl:for-each>
			
			<xsl:for-each select="xml/tableAccessibleCourses/dataSet/Courses">
				<tr>
 					<xsl:if test="(position() div 2)=ceiling(position() div 2)"><xsl:attribute name="bgcolor">#F6F6F6</xsl:attribute></xsl:if>
					<xsl:if test="(position() div 2)!=ceiling(position() div 2)"><xsl:attribute name="bgcolor">#FFFFFF</xsl:attribute></xsl:if>
					<td width="80" align="center" nowrap="true">
						<input type="checkbox" name="{cId}"/></td>
					
					<td		width="100"
							align="center"
							nowrap="true"
							title="{script:dateAlt(string(failureDate), string(/xml/tableAccessibleCourses/DateAlt), string(/xml/tableAccessibleCourses/FailDateAlt))}">
						
						<input	type="text"
								name="date{cId}"
								class="clear" 
								style="{script:dateFont(string(failureDate))}" 
								value="{script:formatDate(string(currDate))}"/></td>
					
					<td		width="40%"
							title="{description}"><a href="javascript:CouseRef('{cId}')"><xsl:value-of select="name"/></a></td>
					
					<td		width="55%"
							title="{/xml/tableAccessibleCourses/CommentAlt}"><input type="text" name="comment{cId}" class="clear" maxlength="255"/></td>
				</tr>
			</xsl:for-each>
		</table>
		
		<p align="right">
			<a href="javascript:sendChecked()"><xsl:value-of select="//addSenderCaption"/></a></p>
	</xsl:if>

	<xsl:if test="count(xml/ds/Area)>0">
		<p class="help"><xsl:value-of select="xml/Help0"/></p>
		
		<ul>
			<xsl:for-each select="xml/ds/Area">
				<li><h3 class="cap3"><a href="javascript:menuClick('', 'TrainingRequest', '', '{id}', '', '')"><xsl:value-of select="Name"/></a>
					</h3>
				</li>
			</xsl:for-each>
		</ul>
	</xsl:if>

</xsl:template>
</xsl:stylesheet>