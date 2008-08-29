<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet
		version="1.0"
		xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
		xmlns:msxsl="urn:schemas-microsoft-com:xslt"
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
		<xsl:attribute name="typeName">N2.Lms.Items.Course, Convert</xsl:attribute>
		<xsl:attribute name="sortOrder">0</xsl:attribute>
		<xsl:attribute name="name">
			<xsl:value-of select="id"/></xsl:attribute>
		<xsl:attribute name="parent">15</xsl:attribute>
		<xsl:attribute name="visible">true</xsl:attribute>
	</xsl:attribute-set>

	<xsl:key name="Content" match="Content" use="eid"/>
	<xsl:key name="Topic" match="Themes" use="Parent"/>
	<xsl:key name="Test" match="Tests" use="id"/>
	<xsl:key name="TestsOf" match="Tests" use="Parent"/>
	<xsl:key name="QuestionsOf"
			 match="TestQuestions"
			 use="Test"/>
	<xsl:key name="Parent" match="Themes|Courses|Tests" use="id"/>
	
	<xsl:template match="/*">
		<n2 version="1.0.401.29980"
			exportVersion="2"
			exportDate="26.08.2008 17:12:35">
			<xsl:apply-templates select="//Courses"/>
		</n2>
	</xsl:template>

	<xsl:template match="Courses">
		<xsl:variable name="defLang"
					  select="CourseLanguage"/>
		<item	id="{$n2CourseId}"
				xsl:use-attribute-sets="Course"
				title="{normalize-space(key('Content',Name)[Lang = $defLang]/DataStr)}">
			<details>
				<detail name="Text"
						typeName="System.String, mscorlib">
					<xsl:value-of select="key('Content', DescriptionShort)[Lang = $defLang]/TData"/>
				</detail>
				<detail name="Version"
						typeName="System.Int32, mscorlib">
					<xsl:choose>
						<xsl:when test="number(Version) != NaN"><xsl:value-of select="Version"/></xsl:when>
						<xsl:otherwise>0</xsl:otherwise>
					</xsl:choose>
				</detail>
				<detail name="Keywords"
						typeName="System.String, mscorlib">
					<xsl:value-of select="key('Content', Keywords)[Lang = $defLang]/DataStr"/>
				</detail>
				<detail name="Type"
						typeName="System.String, mscorlib">Семинары</detail>
				<detail name="Duration"
						typeName="System.Int32, mscorlib">
					<xsl:value-of select="sum(key('Topic', id)/Duration)"/>
				</detail>
				<detail name="Cost1"
						typeName="System.Double, mscorlib">
					<xsl:value-of select="Cost1"/>
				</detail>
				<detail name="Cost2"
						typeName="System.Double, mscorlib">
					<xsl:value-of select="Cost2"/>
				</detail>
				<detail name="Author"
						typeName="System.String, mscorlib">
					<xsl:value-of select="key('Content', Author)[Lang = $defLang]/DataStr"/>
				</detail>
				<detail name="DescriptionUrl"
						typeName="System.String, mscorlib">
					<xsl:value-of select="key('Content', DescriptionLong)[Lang = $defLang]/TData"/>
				</detail>
				<detail name="RequirementsUrl"
						typeName="System.String, mscorlib">
					<xsl:value-of select="key('Content', Requirements)[Lang = $defLang]/TData"/>
				</detail>
				<detail name="Public"
						typeName="System.Boolean, mscorlib">
					<xsl:value-of select="CPublic"/>
				</detail>
			</details>
			<detailCollections>
				<collection name="Additions">
					<xsl:for-each select="key('Content', Additions)[Lang=$defLang]">
						<xsl:sort select="COrder"/>
						<detail name="{concat('Addition', COrder)}"
								typeName="System.String, mscorlib">
							<xsl:value-of select="DataStr"/>
						</detail>
					</xsl:for-each>
				</collection>
			</detailCollections>
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
			  typeName="N2.Lms.Items.TopicList, Convert"
			  discriminator="Topics"
			  xsl:use-attribute-sets="generic">
			<details />
			<detailCollections />
			<children>
				<xsl:apply-templates select="key('Topic', id)"
									 mode="Reference">
					<xsl:sort select="TOrder"/>
				</xsl:apply-templates>
				<xsl:apply-templates select="key('TestsOf', id)" mode="Reference" />
			</children>
			<authorizations />
		</item>
		
		<xsl:apply-templates select="key('Topic', id)"
							 mode="Content">
			<xsl:sort select="TOrder"/>
		</xsl:apply-templates>

		<xsl:apply-templates select="key('TestsOf', id)"
							 mode="Content"/>
	</xsl:template>

	<xsl:template match="Themes"
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
	
	<xsl:template match="Themes"
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
				name="{id}"
				sortOrder="{TOrder}"
				visible="True"
				typeName="N2.Lms.Items.Topic, Convert"
				discriminator="Topic"
				xsl:use-attribute-sets="generic"
				title="{key('Content', Name)[Lang = $defLang]/DataStr}">

			<xsl:attribute name="id">
				<xsl:call-template name="MakeIdOf">
					<xsl:with-param name="obj"
									select="."/>
				</xsl:call-template>
			</xsl:attribute>

			<details>
				<detail name="Duration"
						typeName="System.Int32, mscorlib">
					<xsl:value-of select="Duration"/>
				</detail>
				<detail name="Mandatory"
						typeName="System.Boolean, mscorlib">
					<xsl:value-of select="Mandatory"/>
				</detail>
			</details>
			<detailCollections>
				<collection name="Content">
					<xsl:for-each select="key('Content', Content)[Lang = $defLang]">
						<xsl:sort select="TOrder"/>
						<detail name="Content" typeName="System.String, mscorlib">
							<xsl:value-of select="DataStr"/>
						</detail>
					</xsl:for-each>
				</collection>
			</detailCollections>
			
			<xsl:element name="children">
				<xsl:apply-templates
					select="key('Topic', id)"
					mode="Reference" />
				<xsl:apply-templates
					select="key('Test', Practice)"
					mode="Reference"/>
			</xsl:element>
			
			<authorizations />
		</item>

		<xsl:apply-templates select="key('Topic', id)" mode="Content">
			<xsl:sort select="TOrder"/>
		</xsl:apply-templates>

		<xsl:apply-templates select="key('Test', Practice)"
							 mode="Content"/>
	</xsl:template>

	<xsl:template
		match="Tests"
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

	<xsl:template
		match="Tests"
		mode="Content">
		
		<xsl:variable name="parentId">
			<xsl:call-template name="MakeParentIdOf">
				<xsl:with-param name="obj"
								select="."/>
			</xsl:call-template>
		</xsl:variable>

		<xsl:variable name="TestType">
			<xsl:choose>
				<xsl:when test="Type = 1">Test</xsl:when>
				<xsl:when test="Type = 2">Practice</xsl:when>
				<xsl:when test="Type = 3">Questionaire</xsl:when>
				<xsl:when test="Type = 6">Global</xsl:when>
			</xsl:choose>
		</xsl:variable>
		
		<item
			name="{id}"
			parent="{$parentId}"
			title="{$TestType}"
			sortOrder="0"
			visible="{Show}"
			xsl:use-attribute-sets="generic"
			typeName="N2.Lms.Items.Test, Convert"
			discriminator="Test">
			<xsl:attribute name="id">
				<xsl:call-template name="MakeIdOf">
					<xsl:with-param name="obj"
									select="."/>
				</xsl:call-template>
			</xsl:attribute>
			<details>
				<detail name="Type"
						typeName="System.String, mscorlib">
					<xsl:value-of select="$TestType"/>
				</detail>
				<detail name="Duration"
						typeName="System.Int32, mscorlib">
					<xsl:value-of select="Duration"/>
				</detail>
				<detail name="Points"
						typeName="System.Int32, mscorlib">
					<xsl:value-of select="Points"/>
				</detail>
				<detail name="Split"
						typeName="System.Boolean, mscorlib">
					<xsl:value-of select="Split"/>
				</detail>
				<detail name="AutoFinish"
						typeName="System.Boolean, mscorlib">
					<xsl:value-of select="AutoFinish"/>
				</detail>
				<detail name="CanSwitchLang"
						typeName="System.Boolean, mscorlib">
					<xsl:value-of select="CanSwitchLang"/>
				</detail>
				<detail name="ShowThemes"
						typeName="System.Boolean, mscorlib">
					<xsl:value-of select="ShowThemes"/>
				</detail>
				<detail name="Hint"
						typeName="System.String, mscorlib">
					<xsl:choose>
						<xsl:when test="Hints = 1">None</xsl:when>
						<xsl:when test="Hints = 2">Single</xsl:when>
						<xsl:when test="Hints = 3">Both</xsl:when>
					</xsl:choose>
				</detail>
			</details>
			<children>
				<xsl:apply-templates select="key('QuestionsOf', id)"
									 mode="Reference">
					<xsl:sort select="QOrder"/>
				</xsl:apply-templates>
			</children>
		</item>

		<xsl:apply-templates select="key('QuestionsOf', id)"
							 mode="Content">
			<xsl:sort select="QOrder"/>
		</xsl:apply-templates>
	</xsl:template>

	<xsl:template match="TestQuestions"
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

	<xsl:template match="TestQuestions"
				  mode="Content">
		<xsl:variable name="defLang"
					  select="key('Parent', Test)/DefLanguage" />
		<item
			name="{id}"
			parent="-1"
			title="{key('Content', Content)[Lang = $defLang]/TData}"
			sortOrder="{QOrder}"
			visible="true"
			xsl:use-attribute-sets="generic"
			typeName="N2.Lms.Items.TestQuestion, Convert"
			discriminator="TestQuestion">
			<xsl:attribute name="parent">
				<xsl:call-template name="MakeParentIdOf">
					<xsl:with-param name="obj"
									select="."/>
				</xsl:call-template>
			</xsl:attribute>
			<xsl:attribute name="id">
				<xsl:call-template name="MakeIdOf">
					<xsl:with-param name="obj"
									select="."/>
				</xsl:call-template>
			</xsl:attribute>
			<details>
				<detail name="ShortHint"
						typeName="System.String, mscorlib">
					<xsl:value-of select="key('Content', ShortHint)[Lang = $defLang]/TData"/>
				</detail>
				<detail name="ShortHint"
						typeName="System.String, mscorlib">
					<xsl:value-of select="key('Content', LongHint)[Lang = $defLang]/DataStr"/>
				</detail>
				<detail name="Points"
						typeName="System.Int32, mscorlib">
					<xsl:value-of select="Points"/>
				</detail>
				<detail name="Type"
						typeName="System.Int32, mscorlib">
					<xsl:value-of select="Type"/>
				</detail>
				<detail name="Answers"
						typeName="System.String, mscorlib" />
			</details>
			<detailCollections>
			<fixAnswers>
				<xsl:value-of disable-output-escaping="yes" select="key('Content', Answer)[Lang = $defLang]/TData"/>
			</fixAnswers>
			</detailCollections>
		</item>
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
				   select="key('Parent', $ParentObjId)"/>
		
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
