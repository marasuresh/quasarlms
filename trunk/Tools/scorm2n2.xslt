<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet
		version="1.0"
		xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
	xmlns:msxsl="urn:schemas-microsoft-com:xslt"
	xmlns:lom="http://ltsc.ieee.org/xsd/LOM"
	exclude-result-prefixes="msxsl">
	<xsl:output method="xml"
				indent="yes"/>
	<xsl:variable name="defLang"
				  select="//Courses/CourseLanguage"/>

	<xsl:variable name="n2CourseId"
				  select="1"/>
	<xsl:variable name="n2TopicListId"
				  select="2"/>

	<xsl:attribute-set name="generic">
		<xsl:attribute name="created">21.08.2008 13:25:51</xsl:attribute>
		<xsl:attribute name="updated">21.08.2008 13:37:21</xsl:attribute>
		<xsl:attribute name="published">21.08.2008 13:25:51</xsl:attribute>
		<xsl:attribute name="expires" />
		<xsl:attribute name="zoneName" />
		<xsl:attribute name="savedBy">Export from DCE</xsl:attribute>
	</xsl:attribute-set>

	<xsl:attribute-set name="Course"
					   use-attribute-sets="generic">
		<xsl:attribute name="discriminator">Course</xsl:attribute>
		<xsl:attribute name="typeName">N2.Lms.Items.Course</xsl:attribute>
		<xsl:attribute name="sortOrder">0</xsl:attribute>
		<xsl:attribute name="name">
			<xsl:value-of select="id"/></xsl:attribute>
		<xsl:attribute name="parent">15</xsl:attribute>
		<xsl:attribute name="visible">true</xsl:attribute>
	</xsl:attribute-set>

	<xsl:key name="Resource" match="resource" use="identifier"/>
	<xsl:key name="Item" match="//item" use="identifier"/>
	<xsl:key name="Parent" match="//item" use="identifierref"/>
	
	<xsl:template match="*|/">
		<xsl:apply-templates />
	</xsl:template>

	<xsl:template match="manifest/organizations">
		<n2 version="1.0.401.29980"
			exportVersion="2"
			exportDate="17.11.2008 18:45:35">
			<xsl:apply-templates select="*"/>
		</n2>
	</xsl:template>

	<xsl:template match="organization">
		<xsl:variable name="defLang"
					  select="CourseLanguage"/>
		<item	id="{$n2CourseId}"
				xsl:use-attribute-sets="Course"
				name="{@identifier}"
				title="{title}">
			<details>
				<detail name="Text"
						typeName="System.String">
					<xsl:value-of select="/manifest/metadata/lom:lom/lom:description/lom:string"/>
				</detail>
				<detail name="Version"
						typeName="System.Int32">
					<xsl:choose>
						<xsl:when test="number(Version) != NaN"><xsl:value-of select="Version"/></xsl:when>
						<xsl:otherwise>0</xsl:otherwise>
					</xsl:choose>
				</detail>
				<detail name="Keywords"
						typeName="System.String">
					<xsl:value-of select="/manifest/metadata/lom:lom/lom:description/lom:keyword"/>
				</detail>
				<detail name="Type"
						typeName="System.String">Семинары</detail>
			</details>
			<detailsCollections />
			<children>
				<child id="{$n2TopicListId}" />
			</children>
		</item>
		
		<item id="{$n2TopicListId}"
			  name="{Code}"
			  parent="{$n2CourseId}"
			  title="Topics"
			  sortOrder="0"
			  visible="True"
			  typeName="N2.Lms.Items.TopicList"
			  discriminator="Topics"
			  xsl:use-attribute-sets="generic">
			<details />
			<detailCollections />
			<children>
				<xsl:apply-templates select="item"
									 mode="Reference" />
			</children>
			<authorizations />
		</item>
		
		<xsl:apply-templates select="item"
							 mode="Content" />

	</xsl:template>

	<xsl:template match="item"
				  mode="Reference">
		<child>
			<xsl:attribute name="id">
				<xsl:call-template name="MakeIdOf">
					<xsl:with-param name="obj"
									select="."/>
				</xsl:call-template>
			</xsl:attribute>
		</child>
	</xsl:template>
	
	<xsl:template match="item"
				  mode="Content">
		<xsl:param name="Counter"
				   select="100"/>
		
		<xsl:variable name="parentId">
			<xsl:call-template name="MakeParentIdOf">
				<xsl:with-param name="obj"
								select="."/>
			</xsl:call-template>
		</xsl:variable>
		
		<item	parent="{$parentId}"
				name="{@identifier}"
				visible="True"
				typeName="N2.Lms.Items.Topic"
				discriminator="Topic"
				xsl:use-attribute-sets="generic"
				title="{title}">

			<xsl:attribute name="id">
				<xsl:call-template name="MakeIdOf">
					<xsl:with-param name="obj"
									select="."/>
				</xsl:call-template>
			</xsl:attribute>

			<details />
			<detailCollections>
				<collection name="Content">
					<detail name="Content" typeName="System.String">
						<xsl:value-of select="key('Resource', @identifierref)/@href"/>
					</detail>
				</collection>
			</detailCollections>
			
			<xsl:element name="children">
				<xsl:apply-templates
					select="item"
					mode="Reference" />
			</xsl:element>
			
			<authorizations />
		</item>

		<xsl:apply-templates select="item" mode="Content" />

	</xsl:template>

	<xsl:template name="MakeParentIdOf">
		<xsl:param name="obj" />
		
		<xsl:variable name="ParentObjId">
			<xsl:choose>
				<xsl:when test="$obj/Parent">
					<xsl:value-of select="$obj/Parent"/>
				</xsl:when>
				<xsl:when test="$obj/Test">
					<xsl:value-of select="$obj/Test"/>
				</xsl:when>
			</xsl:choose>
		</xsl:variable>

		<xsl:variable name="ParentObj"
				   select="key('Item', $ParentObjId)"/>
		
		<xsl:variable
			name="ParentName"
			select="name($ParentObj)"/>
		
		<xsl:choose>
			<xsl:when test="$ParentName = 'Courses'">
				<xsl:value-of select="$n2TopicListId"/>
			</xsl:when>
			<xsl:when test="$ParentName = 'Themes'">
				<xsl:call-template name="_MakeIdOfTheme">
					<xsl:with-param name="obj"
									select="$ParentObj"/>
				</xsl:call-template>
			</xsl:when>
			<xsl:when test="$ParentName = 'Tests'">
				<xsl:call-template name="_MakeIdOfTest">
					<xsl:with-param name="obj"
									select="$ParentObj"/>
				</xsl:call-template>
			</xsl:when>
			<xsl:otherwise>
				<xsl:value-of select="$n2TopicListId"/>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>

	<xsl:template name="MakeIdOf">
		<xsl:param name="obj"/>

		<xsl:variable name="objName"
					  select="name()"/>
		
		<xsl:choose>
			<xsl:when test="$objName = 'Themes'">
				<xsl:call-template name="_MakeIdOfTheme">
					<xsl:with-param name="obj"
									select="$obj" />
				</xsl:call-template>
			</xsl:when>
			<xsl:when test="$objName = 'Tests'">
				<xsl:call-template name="_MakeIdOfTest">
					<xsl:with-param name="obj"
									select="$obj" />
				</xsl:call-template>
			</xsl:when>
			<xsl:when test="$objName = 'TestQuestions'">
				<xsl:call-template name="_MakeIdOfQuestion">
					<xsl:with-param name="obj"
									select="$obj" />
				</xsl:call-template>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	
	<xsl:template name="_MakeIdOfTheme">
		<xsl:param name="obj"/>
		<xsl:value-of select="$n2TopicListId + $obj/TOrder - //Themes[position()=1]/TOrder + 1"/>
	</xsl:template>
	
	<xsl:template name="_MakeIdOfTest">
		<xsl:param name="obj"/>
		
		<xsl:variable name="lastThemeId">
			<xsl:choose>
				<xsl:when test="count(//Themes) &gt; 0">
					<xsl:call-template name="_MakeIdOfTheme">
						<xsl:with-param name="obj"
										select="//Themes[position()=last()]"/>
					</xsl:call-template>
				</xsl:when>
				<xsl:otherwise>0</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		
		<xsl:variable name="parentId">
			<xsl:call-template name="MakeParentIdOf">
				<xsl:with-param name="obj"
								select="$obj"/>
			</xsl:call-template>
		</xsl:variable>

		<xsl:variable name="maxTestsPerParent"
					  select="4"/>
		
		<xsl:value-of select="$n2TopicListId + $lastThemeId + $maxTestsPerParent * $parentId + $obj/Type"/>
	</xsl:template>

	<xsl:template name="_MakeIdOfQuestion">
		<xsl:param name="obj"/>

		<xsl:variable name="lastTestId">
			<xsl:call-template name="_MakeIdOfTest">
				<xsl:with-param name="obj"
								select="//Tests[position()=last()]"/>
			</xsl:call-template>
		</xsl:variable>
		
		<xsl:value-of select="$lastTestId + $obj/QOrder"/>
	</xsl:template>
</xsl:stylesheet>
