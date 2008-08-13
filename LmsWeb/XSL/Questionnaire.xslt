<?xml version="1.0" encoding="windows-1251"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
xmlns:msxml="urn:schemas-microsoft-com:xslt"
xmlns:script="myspace">
<xsl:output method="html" indent="no"/> 
<msxml:script implements-prefix="script" language="jscript">
<![CDATA[	
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

<xsl:template match="/xml">

<h3 class="cap4"><xsl:value-of select="dsCommon/Common/Course"/></h3>
	
	<xsl:if test="dsQ/Tests/Type=6">
		<h3 class="cap3"><xsl:value-of select="QuestionnaireGlobal"/></h3>
		<p class="help"><xsl:value-of select="Help"/></p>
	</xsl:if>
	
	<xsl:if test="dsQ/Tests/Type=3">
		<h3 class="cap3"><xsl:value-of select="QuestionnaireCourse"/></h3>
	</xsl:if>
	
	<xsl:choose>
		<xsl:when test="thanks">
			<h3 class="failure"><xsl:value-of select="ThanksA"/></h3>
		</xsl:when>
		
		<xsl:otherwise>
			<input	id="qParam"
					name="qForm"
					type="hidden"
					value="q" />
			
			<ol>
				<xsl:for-each select="dsQw/Qw">
					<xsl:if test="'_string'=script:contentType(string(cType)) or '_html'=script:contentType(string(cType)) or '_xml'=script:contentType(string(cType))">
						<li>
							<h4>
								<label for="questionControl{id}">
									<xsl:value-of select="Content"/>
								</label>
							</h4>
						</li>
					</xsl:if>
								
					<xsl:call-template name="TestQw">
						<xsl:with-param name="Qw" select="."/>
					</xsl:call-template>
				</xsl:for-each>
			</ol>
				
			<input type="SUBMIT" value="{//qSend}" />
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<xsl:template name="TestQw">
	<xsl:param name="Qw"/>

	<xsl:if test="$Qw/Answer/Question/@type='single'">
		<p>
			<ol>
				<xsl:for-each select="$Qw/Answer/Question/Answer">
					<li>
						<input
							id="questionControl{$Qw/id}"
							name="{$Qw/id}"
							type="radio"
							value="{position()}"/>&#160;<xsl:value-of select="."/>
					</li>
				</xsl:for-each>
			</ol>
		</p>
		</xsl:if>
	
		<xsl:if test="$Qw/Answer/Question/@type='multiple'">
			<p><ol>
				<xsl:for-each select="$Qw/Answer/Question/Answer">
					<li>
						<input
							id="questionControl{$Qw/id}"
							type="checkbox"
							name="{$Qw/id}{position()}"
							value="1" />&#160;<xsl:value-of select="."/>
					</li>
				</xsl:for-each>
			</ol>
			</p>
		</xsl:if>
	
	<xsl:if test="$Qw/Answer/Question/@type='textbox'">
		<p><textarea
					id="questionControl{$Qw/id}"
					rows="3"
					cols="40"
					name="{$Qw/id}"
					maxlength="512"
					wrap="soft"
					style="overflow:hidden">
				<xsl:value-of select="result"/>
			</textarea></p>
	</xsl:if>
</xsl:template>

</xsl:stylesheet>