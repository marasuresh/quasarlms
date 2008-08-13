<?xml version="1.0" encoding="windows-1251"?>
<xsl:stylesheet version="1.0"
				xmlns:xsl='http://www.w3.org/1999/XSL/Transform'>
	<xsl:output method="html"
				omit-xml-declaration="yes"
				indent="no"/>

	<xsl:template name="formatDate">
		<xsl:param name="strDate"/>
		<!--2003-06-11T17:41:37.0630000+03:00
		 123456789012345678901234567890123
		 0 1 2 3-->
		<nobr>
			<xsl:value-of select="substring($strDate, 9, 2)"/>.
			<xsl:value-of select="substring($strDate, 6, 2)"/>.
			<xsl:value-of select="substring($strDate, 1, 4)"/>
		</nobr>
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
		<!--Список тренингов-->
		<xsl:if test="count(xml/TrainingsList/DataSet/Trainings)>0">
			<h3 class="cap4">
				<xsl:value-of select="xml/TrainingsList/Caption"/>
			</h3>
			<table cellspacing="0"
				   cellpadding="3"
				   border="0"
				   width="100%"
				   align="center"
				   class="TableList">
				<tr>
				<th nowrap="nowrap">
					<xsl:value-of select="xml/TrainingsList/Header/Name"/>
				</th>
				<th nowrap="nowrap"
					width="100px">
					<xsl:value-of select="xml/TrainingsList/Header/StartDate"/>
				</th>
				<th nowrap="nowrap"
					width="100px">
					<xsl:value-of select="xml/TrainingsList/Header/EndDate"/>
				</th>
				</tr>
				<colgroup>
					<col/>
					<col nowrap="nowrap"
						 align="center"/>
					<col nowrap="nowrap"
						 align="center"/>
				</colgroup>
				<xsl:for-each select="xml/TrainingsList/DataSet/Trainings">
					<tr>
						<xsl:if test="(position() div 2)=ceiling(position() div 2)">
							<xsl:attribute name="bgcolor">#F6F6F6</xsl:attribute>
						</xsl:if>
						<xsl:if test="(position() div 2)!=ceiling(position() div 2)">
							<xsl:attribute name="bgcolor">#FFFFFF</xsl:attribute>
						</xsl:if>
						<td>
							<xsl:value-of select="Name"/>
						</td>
						<td>
							<xsl:if test="StartDate!=''">
								<xsl:call-template name="formatDate">
									<xsl:with-param name="strDate"
													select="StartDate"/>
								</xsl:call-template>
							</xsl:if>
						</td>
						<td>
							<xsl:if test="EndDate!=''">
								<xsl:call-template name="formatDate">
									<xsl:with-param name="strDate"
													select="EndDate"/>
								</xsl:call-template>
							</xsl:if>
						</td>
					</tr>
				</xsl:for-each>
			</table>
			<xsl:if test="count(xml/CoursesList/DataSet/Courses)>0">
				<hr/>
			</xsl:if>
		</xsl:if>


		<!--Список курсов-->
		<xsl:if test="count(xml/CoursesList/DataSet)>0 or count(xml/ds/Area)>0">

			<table>
				<tr>
					<td nowrap="nowrap">
						<strong>
							<xsl:value-of select="xml/EnterKeywords"/>:&#160;
						</strong>
					</td>
					<td width="300px">
						<input value="{xml/searchStr}"
							   name="keywords"
							   type="text"></input>
					</td>
					<td class="btn">
						<input value="{xml/Search}"
							   type="submit"/>
					</td>
				</tr>
			</table>

			<xsl:if test="xml/NotFound">
				<h3 class="failure">
					<xsl:value-of select="xml/NotFoundA"/>
				</h3>
			</xsl:if>
			<p class="help">
				<xsl:value-of select="xml/CourseListHelp"/>
			</p>
			<xsl:choose>
				<xsl:when test="count(xml/Area) != 0">
					<h3 class="cap4">
						<xsl:value-of select="xml/Area/Name"/>
					</h3>
				</xsl:when>
				<xsl:when test="count(xml/CoursesList/DataSet/Courses)>0">
					<h3 class="cap4">
						<xsl:value-of select="xml/CoursesList/Caption"/>
					</h3>
				</xsl:when>
			</xsl:choose>

			<xsl:if test="count(xml/CoursesList/DataSet/Courses[isReady='true'])>0">
				<xsl:call-template name="CoursesTable">
					<xsl:with-param name="ds"
									select="xml/CoursesList/DataSet"/>
					<xsl:with-param name="head"
									select="xml/CoursesList/Header"/>
				</xsl:call-template>
			</xsl:if>

			<xsl:if test="count(xml/ds/Area)>0">
				<br/>
				<table cellspacing="0"
					   cellpadding="0"
					   border="0"
					   width="100%">
					<tr>
						<td>
							<xsl:for-each select="xml/ds/Area">
								<h3 class="cap3">
									<a href="javascript:menuClick('', 'CoursesCommon', '', '{id}', '', '')">
										<xsl:value-of select="Name"/>
									</a>
								</h3>
							</xsl:for-each>
						</td>
					</tr>
				</table>
			</xsl:if>

			<xsl:call-template name="Areas">
				<xsl:with-param name="parent"
								select="xml/Area"/>
				<xsl:with-param name="head"
								select="xml/CoursesList/Header"/>
			</xsl:call-template>
		</xsl:if>

	</xsl:template>

	<xsl:template name="Areas">
		<xsl:param name="parent"/>
		<xsl:param name="head"/>
		<xsl:param name="level"
				   select="1"/>
		<xsl:param name="parentArea"/>

		<table cellspacing="0"
			   cellpadding="0"
			   border="0"
			   width="100%">
			<tr>
				<td>
					<xsl:for-each select="$parent/Area">
						<br/>
						<!--table style="border: 1px solid #808080;" cellspacing="0" cellpadding="0" border="0" width="100%" align="center">
 <th class="Head{$level}"><xsl:value-of select="$parentArea"/><xsl:value-of select="Name"/></th>
 </table-->
						<xsl:if test="count(DataSet/Courses[isReady='true'])>0">
							<h3>
								<li>
									<xsl:value-of select="$parentArea"/>
									<xsl:value-of select="Name"/>
								</li>
							</h3>

							<xsl:call-template name="CoursesTable">
								<xsl:with-param name="ds"
												select="DataSet"/>
								<xsl:with-param name="head"
												select="$head"/>
								<xsl:with-param name="level"
												select="$level"/>
								<xsl:with-param name="parentArea"
												select="$parentArea"/>
							</xsl:call-template>
						</xsl:if>


						<xsl:call-template name="Areas">
							<xsl:with-param name="parent"
											select="."/>
							<xsl:with-param name="head"
											select="$head"/>
							<xsl:with-param name="level"
											select="number($level)+1"/>
							<xsl:with-param name="parentArea"
											select="concat($parentArea,Name,' / ')"/>
						</xsl:call-template>
					</xsl:for-each>
				</td>
			</tr>
		</table>
	</xsl:template>

	<!-- Вывод таблицы курсов для области -->
	<xsl:template name="CoursesTable">
		<xsl:param name="ds"/>
		<xsl:param name="head"/>
		<xsl:param name="level"/>
		<xsl:param name="parentArea"/>

		<table cellspacing="0"
			   cellpadding="3"
			   border="0"
			   align="center"
			   class="TableList"
			   style="padding-right: 1pt;margin-right: 1pt;">
			<!--xsl:if test="not($ds/Courses/Area)"-->
			<th nowrap="nowrap">
				<xsl:choose>
					<xsl:when test="false">
						<xsl:value-of select="$parentArea"/>
						<xsl:value-of select="$ds/Courses/Area"/>
					</xsl:when>
					<xsl:otherwise>
						<xsl:value-of select="$head/Name"/>
					</xsl:otherwise>
				</xsl:choose>
			</th>
			<th width="20%"
				nowrap="true">
				<xsl:value-of select="$head/Type"/>
			</th>
			<th width="60pt"
				nowrap="true"
				style="text-transform: none;">
				<xsl:value-of select="$head/Cost1"/>
			</th>
			<th width="60pt"
				nowrap="true"
				style="text-transform: none;">
				<xsl:value-of select="$head/Cost2"/>
			</th>
			<th width="60pt"
				nowrap="true">
				<xsl:value-of select="$head/Duration"/>
			</th>
			<!--/xsl:if-->
			<colgroup>
				<col/>
				<col align="left"/>
				<col nowrap="true"
					 align="center"/>
				<col nowrap="true"
					 align="center"/>
				<col nowrap="true"
					 align="center"/>
			</colgroup>
			<xsl:for-each select="$ds/Courses[isReady='true']">
				<tr>
					<xsl:if test="(position() div 2)=ceiling(position() div 2)">
						<xsl:attribute name="bgcolor">#F6F6F6</xsl:attribute>
					</xsl:if>
					<xsl:if test="(position() div 2)!=ceiling(position() div 2)">
						<xsl:attribute name="bgcolor">#FFFFFF</xsl:attribute>
					</xsl:if>
					<td>
						<a href="javascript:CouseRef('{cId}')">
							<xsl:value-of select="Name"/>
							<xsl:if test="CPublic='true'">
								&#160;(<xsl:value-of select="$head/Public"/>)
							</xsl:if>
							<br/>
							<small>
								<xsl:value-of select="Description"/>
							</small>
						</a>
					</td>
					<td>
						<xsl:if test="Type">
							<small>
								<xsl:value-of select="Type"/>
							</small>
						</xsl:if>
					</td>
					<td>
						<xsl:if test="Cost1 and Cost1!=0">
							<small>
								<xsl:value-of select="Cost1"/>
							</small>
						</xsl:if>
					</td>
					<td>
						<xsl:if test="Cost2 and Cost2!=0">
							<small>
								<xsl:value-of select="Cost2"/>
							</small>
						</xsl:if>
					</td>
					<td>
						<xsl:if test="Duration!=''">
							<small>
								<xsl:value-of select="Duration"/>
							</small>
						</xsl:if>
					</td>
				</tr>
			</xsl:for-each>
		</table>
	</xsl:template>

</xsl:stylesheet>