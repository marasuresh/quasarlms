<%@ Reference Page="~/Learn/Trainings.aspx" %>
<%@ Control Language="c#" Inherits="DCE.Common.Welcome" CodeFile="Welcome.ascx.cs" %>
<%@ Import Namespace="System.Xml" %>
<%@ Import Namespace="System.Linq" %>
<%@ Register Src="~/Common/Members.ascx" TagName="Members" TagPrefix="lms" %>

<script language="javascript"><!--
function qwHref(qId){
	if (qId == "")
		return;
	QwSwitchForm.qwId.value = qId;
	QwSwitchForm.submit();
}
function toTest(id){
	AddParameter("cset", "TestWork");
	AddParameter("id", id);
	applyParameters();
}
function resizeFrame(){
	try{
		theHeight = document.getElementById("contFrameId").contentWindow.document.body.scrollHeight;
		if (theHeight != 0);
		{
			document.getElementById("contFrameId").style.height = theHeight+50;
		}
	}catch(e){
		document.getElementById("contFrameId").scrolling="auto";
		document.getElementById("contFrameId").style.height = 500;
	}
}
//--></script>
<asp:FormView
		runat="server"
		ID="fvCourse"
		DataMember="Courses"
		DefaultMode="ReadOnly">
	<EmptyDataTemplate>
		<h3 class="failure"><%= Resources.Welcome.NoTrainings %></h3>
		<p class="blue">
			<asp:Hyperlink
					runat="server"
					ID="hlSubscribt"
					NavigateUrl="<%= Resources.PageUrl.PAGE_SUBSCRIBE %>"
					Text='<%= Resources.Welcome.GoToSubscr %>' />
			,&nbsp;<%= Resources.Welcome.SubscrText %>
		</p>
	</EmptyDataTemplate>
	
	<ItemTemplate>
			<table	cellspacing="0"
			cellpadding="0"
			width="100%"
			border="0"
			align="center"
			class="InnerTable"
			id="TABLECV">
		<tr valign="top">
			<td		width="100%"
					style="PADDING-TOP: 7px">
				<h3		class="cap3">
					<%= Resources.Welcome.Caption %></h3>
				<h3		class="cap4"
						title='<%# Eval("Description") %>'>
					<%# Eval("Name") %></h3>
					
					<asp:HyperLink
							runat="server"
							ID="hlStartDate">
						<%# string.Format("{0:d}&nbsp;/&nbsp;{1:d}&nbsp;",
								Eval("StartDate"),
								Eval("EndDate"))%>
						</asp:HyperLink>
				<p	style="COLOR: darkgray">
					<%= Resources.Welcome.LastEntryTxt %>:&nbsp;<%# Profile.LastActivityDate %></p>
				<% if(this.IsBlocked) { %>
					<br/><h3 class="Failure"><%= Resources.Welcome.BlockingText %></h3>
				<% } %>
			 
				 <!-- Приступить к обучению и Список участников -->
				<asp:HyperLink
						runat="server"
						ID="HyperLink1"
						NavigateUrl="javascript:membersHref()"
						Text='<%$ Resources:Welcome,Members_hrefLable %>'
						Visible='<%# (Convert.IsDBNull(Eval("qrId"))&#13;&#10;&#9;&#9;&#9;&#9;&#9;&#9;&#9;&#9;&#9;&#9;&& !Convert.IsDBNull(Eval("qId"))&#13;&#10;&#9;&#9;&#9;&#9;&#9;&#9;&#9;&#9;&#9;)&#13;&#10;&#9;&#9;&#9;&#9;&#9;&#9;&#9;&#9;&#9;|| this.IsBlocked %>' /><br/>
				<asp:PlaceHolder
						runat="server"
						ID="phLink"
						Visible='<%# !fvCourse.FindControl("HyperLink1").Visible %>'>
					<p><asp:HyperLink
								runat="server"
								ID="hlStart"
								NavigateUrl='<%# Resources.PageUrl.PAGE_TRAINING + "?trId=" + DCE.Service.TrainingID %>'
								Text='<%$ Resources:Welcome,Start_hrefLable %>' />&nbsp; &nbsp;
					</p>			
					<p class="help"><%= Resources.Welcome.Start_help %></p>		 
				</asp:PlaceHolder>
 
				<!-- CDPath form -->
				<asp:PlaceHolder
						runat="server"
						ID="phCDPath"
						Visible='<%# GetBool(SelectScalar("xml/ie")) %>'>
<script runat="server">
protected string cdDefPathVar = string.Empty;
</script>
					<%# false ? (cdDefPathVar = "<" + Eval("cdDisk") + ">:\\") : string.Empty %>
					<form	action=""
							name="useCDForm"
							method="post">
						<input	type="hidden"
								name="cdAction"
								value="setCD" />
						<input	type="hidden"
								name="cdDefPath"
								value='<%# cdDefPathVar %>' />
						<table	class="RegForm"
								width="100%">
							<tr><td colspan="2">
									<p><small><a>
										<input	style="width:20px;background-color: #FFFFFF;border: 0px;"
												type="checkbox"
												name="checkUseCD"
												onclick="useCDForm.submit();"
												value="check"
												checked='<%# Eval("useCDLib") %>' />
										&nbsp;<asp:Literal ID="Literal5" runat="server" Text='<%$ Resources:Welcome,useCD %>' />
											</a></small>
									</p>
								</td>
							</tr>
							<tr id="Tr1"		runat="server"
									visible='<%# !Convert.IsDBNull(Eval("useCDLib")) && (bool)Eval("useCDLib") %>'>
								<td width="50%">
									<input	type="text"
											name="cdPath"
											maxlength="120"
											value='<%# !Convert.IsDBNull(Eval("cdPath"))
															? Eval("cdPath")
															: cdDefPathVar %>' /></td>
								<td		width="50%"
										class="btn">
									<input	id="Submit1"
											type="submit"
											value='<%$ Resources:Welcome,cdSubmit %>'
											name="dcePathBtn"
											runat="server"
											class="clear" />
								</td>
							</tr>
						</table>
					</form>
				</asp:PlaceHolder>
				
				<form	action=""
						name="QwSwitchForm"
						method="post">
					<input	type="hidden"
							name="qwId" />
					<input	type="hidden"
							name="qwAction"
							value="trainingStart" />
					<p id="P1"	runat="server"
							 visible='<%# Convert.IsDBNull(Eval("qrId"))&#13;&#10;&#9;&#9;&#9;&#9;&#9;&#9;&#9;&#9;&#9;&& !Convert.IsDBNull(Eval("qId")) %>'>
						<asp:Literal ID="Literal6"
								runat="server"
								Text='<%$ Resources:Welcome,Questionnaire_Lable %>' />&nbsp;&nbsp;&nbsp;
						<asp:HyperLink
								runat="server"
								ID="hlQuestionnaire"
								NavigateUrl='<%# string.Format(@"javascript:qwHref(""{0}"")", Eval("qId")) %>'
								Text='<%$ Resources:Welcome,Questionnaire_hrefLable %>' />
					</p>
				</form>
			</td>
		</tr>
	</table>
	
	<hr/><%--
	<p		class="CenterColumn">
		<asp:Image
				runat="server"
				ID="imgBullet1"
				SkinID="Bullet1"
				ToolTip="" />&nbsp;&nbsp;&nbsp;
		<strong	class="yellow"><%= Resources.Welcome.BulletinBoard_Caption %></strong>
	</p>--%>
	<%--
	<asp:GridView
			runat="server"
			ID="gvBulletins"
			CssClass="TableList"
			DataSourceID="odsBulletins"
			AutoGenerateColumns="false"
			CellPadding="3"
			Width="100%">
		<AlternatingRowStyle BackColor="#ffffff" />
		<RowStyle BackColor="#F6F6F6" />
		<Columns>
			<asp:TemplateField
					HeaderStyle-CssClass="HeadCenter"
					HeaderText='<%$ Resources:Welcome,BulletinBoard_Header_Date %>'
					ItemStyle-Width="100pt"
					ItemStyle-HorizontalAlign="Center"
					ItemStyle-Wrap="false">
				<ItemTemplate>
					<%# Eval("Date", "{0:d}")%>
				</ItemTemplate>
			</asp:TemplateField>
			
			<asp:TemplateField
					HeaderStyle-CssClass="HeadCenter"
					HeaderText='<%$ Resources:Welcome,BulletinBoard_Header_Author %>'
					ItemStyle-Width="40%"
					ItemStyle-Wrap="false">
				<ItemTemplate>
					<asp:HyperLink
							runat="server"
							ID="hlEmail"
							Text='<%# ("2" == Eval("UserRole"))
										? (global::Resources.Welcome.Curator+":&nbsp;")
										: ("2" == Eval("UserRole")
											? global::Resources.Welcome.Instructor+":&nbsp;"
											: string.Empty)
										+ Eval("Author") %>'
							NavigateUrl='<%# Eval("Email", "mailto:{0}") %>' />
				</ItemTemplate>
			</asp:TemplateField>
			
			<asp:BoundField
					HeaderStyle-CssClass="HeadCenter"
					HeaderText='<%$ Resources:Welcome,BulletinBoard_Header_Text %>'
					DataField="Text"
					ItemStyle-Width="55%" />
		</Columns>
		<EmptyDataTemplate>
			No records
		</EmptyDataTemplate>
	</asp:GridView>
		--%>
		<%--
		<asp:ObjectDataSource
				runat="server"
				ID="odsBulletins"
				OldValuesParameterFormatString="original_{0}"
				OnSelecting="odsBulletins_Selecting"
				SelectMethod="GetByTraining"
				TypeName="Bulletin">
			<SelectParameters>
				<asp:Parameter Name="trainingId" Type="Object" />
			</SelectParameters>
		</asp:ObjectDataSource>
	--%>
	<hr/>
	<p		class="CenterColumn">
		<asp:Image
				runat="server"
				ID="imgBullet"
				SkinID="Bullet1"
				ToolTip="" />&nbsp;&nbsp;&nbsp;
		<strong	class="yellow"><%= Resources.Welcome.Statistics_Caption %></strong>
	</p>
	<% if(this.Results.Any()) { %>
	<table cellpadding="3" cellspacing="0" width="100%" class="TableList">
		<tr><th><%= Resources.Welcome.Statistics_CourseTest %></th>
			<th><%= Resources.Welcome.Statistics_Header_Complete %></th>
			<th><%= Resources.Welcome.Statistics_Header_Tries %></th>
			<th><%= Resources.Welcome.Statistics_Header_Points %></th>
			<th><%= Resources.Welcome.Statistics_Header_CompletionDate %></th>
		</tr>
	<% var i = 0; %>
	<% foreach (var _result in this.Results) { %>
	<% i++; %>
		<tr bgcolor='<%= 0 == i % 2 ? "#ffffff" : "#f6f6f6" %>'>
			<td width="50%"><a href='javascript:toTest(<%= _result.Test.Name %>)'><%= string.IsNullOrEmpty(_result.Theme) ? Resources.Welcome.Statistics_CourseTest : _result.Theme %></a></td>
			<td width="20%" nowrap="nowrap"><%= _result.IsComplete ? Resources.Welcome.Statistics_Complete : (_result.IsSkipped ? Resources.Welcome.Statistics_Skipped : Resources.Welcome.Statistics_NotComplete) %></td>
			<td width="10%" align="center"><%= _result.AttemptsCount %></td>
			<td width="10%" align="center"><%= _result.Points %></td>
			<td width="10%" align="center"><%= _result.CompletedOn %></td>
		</tr>
	<% } %>
	</table>
	<% } else { %>
		No records
	<% } %>
		<iframe	name="contFrame"
			width="100%"
			id="contFrameId"
			onresize="javascript:resizeFrame();"
			onload="javascript:resizeFrame();"
			frameborder="no"
			height="100%"
			align="top"
			scrolling="no"
			src='<%# !Convert.IsDBNull(Eval("FullDescription"))
				? this.ResolveClientUrl(
					(string)Eval("cRoot")
					+ (string)Eval("DiskFolder")
					+ (string)Eval("FullDescription")).Replace(@"\", "/")
				: "about:blank" %>'>
		</iframe>
	</ItemTemplate>
</asp:FormView>

<lms:Members runat="server" />