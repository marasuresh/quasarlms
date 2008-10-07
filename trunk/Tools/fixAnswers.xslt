<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet
		version="1.0"
		xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
		xmlns:msxsl="urn:schemas-microsoft-com:xslt"
		exclude-result-prefixes="msxsl">
	<xsl:output method="xml"
				indent="yes"/>
	<xsl:template match="fixAnswers">
		<collection name="Options">
			<xsl:for-each select="Question/Answer">
				<detail name="Option"
						typeName="System.String, mscorlib">
					<xsl:value-of select="."/>
				</detail>
			</xsl:for-each>
		</collection>
	</xsl:template>

	<xsl:template match="item[@discriminator='TestQuestion']/details/detail[@name='AnswerType']">
		<xsl:copy>
			<xsl:apply-templates select="@*|*|text()"/>
			<xsl:variable
					name="_answerTypeName"
					select="../../detailCollections/fixAnswers/Question/@type" />
			<!--DceCourseEditor\AnswerList.cs:712-->
			<xsl:choose>
				<xsl:when test="$_answerTypeName = 'textbox'">0</xsl:when>
				<xsl:when test="$_answerTypeName = 'multiple'">1</xsl:when>
				<xsl:when test="$_answerTypeName = 'single'">2</xsl:when>
			</xsl:choose>
		</xsl:copy>
	</xsl:template>

	<xsl:template match="item[@discriminator='TestQuestion']/details/detail[@name='Answers']">
		<xsl:copy>
			<xsl:apply-templates select="@*|*|text()"/>
			<xsl:for-each select="../../detailCollections/fixAnswers/Question/Answer">
				<xsl:choose>
					<xsl:when test="@right = 'true'">1</xsl:when>
					<xsl:when test="@right = 'false'">0</xsl:when>
					<xsl:otherwise>?</xsl:otherwise>
				</xsl:choose>
			</xsl:for-each>
		</xsl:copy>
	</xsl:template>
	
	<xsl:template match="*|@*|text()">
		<xsl:copy>
			<xsl:apply-templates select="@*|*|text()"/>
		</xsl:copy>
	</xsl:template>
</xsl:stylesheet>
