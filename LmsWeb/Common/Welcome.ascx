<%@ Reference Page="~/Learn/Trainings.aspx" %>
<%@ Control Language="c#" Inherits="DCE.Common.Welcome" CodeFile="Welcome.ascx.cs" %>
<%@ Import Namespace="System.Xml" %>

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
function membersHref(){
	AddParameter("cset", "Members");
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
		<h3 class="failure">
		<asp:Literal
				runat="server"
				ID="ltrErrorMessage"
				Text='<%$ Resources:Welcome, NoTrainings %>' /></h3>
		<p class="blue">
			<asp:Hyperlink
					runat="server"
					ID="hlSubscribt"
					NavigateUrl="<%$ Resources: PageUrl, PAGE_SUBSCRIBE %>"
					Text='<%$ Resources:Welcome, GoToSubscr %>' />
			,&nbsp;
			<asp:Literal
					runat="server"
					ID="ltrSubscribeText"
					Text='<%$ Resources:Welcome, SubscrText %>' />
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
						<asp:Literal ID="Literal1"
								runat="server"
								Text='<%$ Resources:Welcome,Caption %>' /></h3>
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
					<asp:Literal ID="Literal2"
							runat="server"
							Text='<%$ Resources:Welcome,LastEntryTxt %>' />:
					&nbsp;<%# DceUserService.LastEntry %></p>
				
				<asp:PlaceHolder
						runat="server"
						ID="phBlocking"
						Visible='<%# this.Blocking %>'>
					<br/>
					<h3 class="Failure">
						<asp:Literal ID="Literal3"
								runat="server"
								Text='<%$ Resources:Welcome,BlockingText %>' /></h3>
				</asp:PlaceHolder>
			 
				 <!-- Приступить к обучению и Список участников -->
				<asp:HyperLink
						runat="server"
						ID="HyperLink1"
						NavigateUrl="javascript:membersHref()"
						Text='<%$ Resources:Welcome,Members_hrefLable %>'
						Visible='<%# (&#9;Convert.IsDBNull(Eval("qrId"))&#13;&#10;&#9;&#9;&#9;&#9;&#9;&#9;&#9;&#9;&#9;&#9;&& !Convert.IsDBNull(Eval("qId"))&#13;&#10;&#9;&#9;&#9;&#9;&#9;&#9;&#9;&#9;&#9;)&#13;&#10;&#9;&#9;&#9;&#9;&#9;&#9;&#9;&#9;&#9;|| this.Blocking %>' /><br/>
				<asp:PlaceHolder
						runat="server"
						ID="phLink"
						Visible='<%# !fvCourse.FindControl("HyperLink1").Visible %>'>
					<p><asp:HyperLink
								runat="server"
								ID="hlStart"
								NavigateUrl='<%# Resources.PageUrl.PAGE_TRAINING + "?trId=" + DCE.Service.TrainingID %>'
								Text='<%$ Resources:Welcome,Start_hrefLable %>' />&nbsp; &nbsp;
						<asp:HyperLink ID="HyperLink2"
								runat="server"
								align="right"
								NavigateUrl='javascript:membersHref()'
								Text='<%$ Resources:Welcome,Members_hrefLable %>' />
					</p>			
					<p class="help">
						<asp:Literal ID="Literal4"
								runat="server"
								Text='<%$ Resources:Welcome,Start_help %>' />
					</p>		 
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
	
	<hr/>
	<p		class="CenterColumn">
		<asp:Image
				runat="server"
				ID="imgBullet1"
				SkinID="Bullet1"
				ToolTip="" />&nbsp;&nbsp;&nbsp;
		<strong	class="yellow">
			<asp:Literal ID="Literal7"
					runat="server"
					Text='<%$ Resources:Welcome,BulletinBoard_Caption %>' /></strong>
	</p>
	
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
	
	<hr/>
	<p		class="CenterColumn">
		<asp:Image
				runat="server"
				ID="imgBullet"
				SkinID="Bullet1"
				ToolTip="" />&nbsp;&nbsp;&nbsp;
		<strong	class="yellow">
			<asp:Literal ID="Literal11"
					runat="server"
					Text='<%$ Resources:Welcome,Statistics_Caption %>' /></strong>
	</p>
	<asp:GridView
			runat="server"
			ID="gvTestResults"
			DataSourceID="odsTestResults"
			AutoGenerateColumns="false"
			CellPadding="3"
			CellSpacing="0"
			HorizontalAlign="Center"
			Width="100%"
			CssClass="TableList">
		<RowStyle BackColor="#f6f6f6" />
		<AlternatingRowStyle BackColor="#ffffff" />
		<Columns>
			<asp:TemplateField
					ItemStyle-Width="50%"
					HeaderText='<%$ Resources:Welcome,Statistics_Header_Theme %>'>
				<ItemTemplate>
					<asp:HyperLink
							runat="server"
							ID="hlTest"
							NavigateUrl='<%# Eval("testId", @"javascript:toTest(""{0}"")") %>'
							Text='<%# !Convert.IsDBNull(Eval("Theme"))
									? "Theme"
									: global::Resources.Welcome.Statistics_CourseTest %>' />
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField
					ItemStyle-Width="20%"
					ItemStyle-Wrap="false"
					HeaderText='<%$ Resources:Welcome,Statistics_Header_Complete %>'>
				<ItemTemplate>
					<%# (bool)Eval("Complete")
							? global::Resources.Welcome.Statistics_Complete
							: ((bool)Eval("Skipped")
								? global::Resources.Welcome.Statistics_Skipped
								: global::Resources.Welcome.Statistics_NotComplete)%>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:BoundField
					DataField="Tries"
					ItemStyle-Width="10%"
					ItemStyle-HorizontalAlign="Center"
					HeaderText='<%$ Resources:Welcome,Statistics_Header_Tries %>' />
			<asp:BoundField
					DataField="Points"
					ItemStyle-Width="10%"
					ItemStyle-HorizontalAlign="Center"
					HeaderText='<%$ Resources:Welcome,Statistics_Header_Points %>' />
			<asp:BoundField
					DataField="CompletionDate"
					ItemStyle-Width="10%"
					ItemStyle-HorizontalAlign="Center"
					HeaderText='<%$ Resources:Welcome,Statistics_Header_CompletionDate %>' />
		</Columns>
		<EmptyDataTemplate>
			No records
		</EmptyDataTemplate>
	</asp:GridView>
	
		<asp:ObjectDataSource
			runat="server"
			ID="odsTestResults"
			OldValuesParameterFormatString="original_{0}"
			OnSelecting="odsTestResults_Selecting"
			SelectMethod="GetStatistics"
			TypeName="DceAccessLib.DAL.TestController" >
		<SelectParameters>
			<asp:Parameter Name="courseId" Type="Object" />
			<asp:Parameter Name="studentId" Type="Object" />
		</SelectParameters>
	</asp:ObjectDataSource>
	
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