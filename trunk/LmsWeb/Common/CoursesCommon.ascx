<%@ Control Language="c#" Inherits="DCE.Common.CoursesCommon" CodeFile="CoursesCommon.ascx.cs" %>
<%@ Import Namespace="System.Linq" %>
<%@ Import Namespace="System.Collections.Generic" %>
<asp:Xml id="Xml1" runat="server"></asp:Xml>
<% Action<IEnumerable<N2.Lms.Items.Course>, N2.Lms.Items.Area> CoursesTable =
	(courses, parentArea) => { %>
	<%-- Вывод таблицы курсов для области --%>
	<table cellspacing="0"
			   cellpadding="3"
			   border="0"
			   align="center"
			   class="TableList"
			   style="padding-right: 1pt;margin-right: 1pt;">
			<th nowrap="nowrap"><%= Resources.CourseCommon.CoursesList_Header_Name%></th>
			<th width="20%"
				nowrap="true"><%= Resources.CourseCommon.CoursesList_Header_Type%></th>
			<th width="60pt"
				nowrap="true"
				style="text-transform: none;"><%= Resources.CourseCommon.CoursesList_Header_Cost1%></th>
			<th width="60pt"
				nowrap="true"
				style="text-transform: none;"><%= Resources.CourseCommon.CoursesList_Header_Cost2%></th>
			<th width="60pt"
				nowrap="true"><%= Resources.CourseCommon.CoursesList_Header_Duration%></th>
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
			<%	int i = 0;
		foreach (var _course in courses.Where(_c => _c.IsReady)) { %>
				<%  i++; %>
				<tr bgcolor='<%= i % 2 == 0 ? "#F6F6F6" : "#FFFFFF" %>'>
					<td>
						<a href="Lms/UI/CourseInfo.aspx?cid=<%= _course.Name %>">
							<%= _course.Title%>
							<% if (_course.IsPublic) { %>
								&nbsp;(<%= Resources.CourseCommon.CoursesList_Header_Public%>)
							<% } %>
							<br/>
						</a>
						<small><%= _course.Description%></small>
					</td>
					<td><% if (!string.IsNullOrEmpty(_course.Type)) { %>
						<small><%= _course.Type%></small>
						<% } %></td>
					<td><% if (_course.Cost1 > 0) { %>
						<xsl:if test="Cost1 and Cost1!=0">
							<small><%= _course.Cost1 %></small>
						<% } %></td>
					<td><% if(_course.Cost2 > 0) { %>
						<small><%= _course.Cost2 %></small>
						<% } %></td>
					<td><small><%= _course.Duration %></small></td>
				</tr>
			<% } %>
		</table>
<% }; %>
<%--
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
--%>
		<!--Список тренингов-->
		<%--
		<xsl:if test="count(xml/TrainingsList/DataSet/Trainings)>0">
			<h3 class="cap4">
				<asp:Literal runat="server" Text="<%$ Resources: CourseCommon, TrainingsList_Caption %>" />
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
					<asp:Literal runat="server" Text="<%$ Resources: CourseCommon, TrainingsList_Header_Name %>" />
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
--%>
		<%--Список курсов--%>
		<% if(this.Courses.Any()) { %>
			<table>
				<tr><td nowrap="nowrap">
						<strong>
							<%= Resources.CourseCommon.EnterKeywords %>:&nbsp;
						</strong></td>
					<td width="300px">
						<asp:TextBox
								runat="server"
								ID="searchStr" /></td>
					<td class="btn">
						<asp:Button
								runat="server"
								Text="<%$ Resources: CourseCommon, Search %>" /></td>
				</tr>
			</table>

			<p class="help"><%= Resources.CourseCommon.CourseListHelp %></p>
			<% if(this.Courses.Any()) { %>
				<h3 class="cap4"><%= Resources.CourseCommon.CoursesList_Caption %></h3>
			<% }
				
				if (this.Courses.Any(_c => _c.IsReady)) {
					CoursesTable(this.Courses, null);
				}
			%>
		<% } %>
