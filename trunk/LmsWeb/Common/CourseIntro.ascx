<%@ Control Language="c#" Inherits="DCE.Common.CourseIntro" CodeFile="CourseIntro.ascx.cs" %>
<script language="javascript"><!--
function resizeFrm(ctid){
	try{
		theHeight = document.getElementById(ctid).contentWindow.document.body.scrollHeight;
		if(theHeight!=0);{
			document.getElementById(ctid).style.height = theHeight+50;
		}
	}catch(e){
		document.getElementById(ctid).scrolling="auto";
		document.getElementById(ctid).style.height = 500;
	}
}
function resizeFrame(){resizeFrm("contFrameId");}
function resizeFrame1(){resizeFrm("contFrameId1");}
//--></script>
<asp:FormView
		runat="server"
		ID="fvCourse" DataSourceID="odsCourse">
	<ItemTemplate>
<table	cellspacing="0"
		cellpadding="0"
		width="100%"
		border="0"
		align="center"
		class="InnerTable">
	<tr valign="top">
		<td width="75%">
			<h3 class="cap3">
				<asp:Literal
						runat="server"
						ID="ltrCaption"
						Text='<%$ Resources:CourseIntro,Caption %>' /></h3>
			<h3		class="cap4"
					title='<%# Eval("Description") %>'>
				<%# Eval("Name")%></h3>
			<p		runat="server"
					visible='<%# !Convert.IsDBNull(Eval("Author")) %>'>
				<asp:Literal
						runat="server"
						ID="ltrAuthor"
						Text='<%$ Resources:CourseIntro,AuthorA %>' />:&nbsp;<%# Eval("Author") %></p>
			
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
						? ((string)Eval("cRoot") + (string)Eval("DiskFolder") + (string)Eval("FullDescription")).Replace(@"\",@"/")
						: "about:blank" %>'>
			</iframe>
			
			<asp:PlaceHolder
					runat="server"
					ID="phRequirements"
					Visible='<%# !Convert.IsDBNull(Eval("Requirements")) %>'>
				<h3 class="cap3">
					<asp:Literal
							runat="server"
							ID="ltrRequirements"
							Text='<%$ Resources:CourseIntro,RequirementsA %>' />:</h3>
				<iframe	name="contFrame"
						width="100%"
						id="contFrameId1"
						onresize="resizeFrame1()"
						onLoad="resizeFrame1()"
						frameborder="no"
						height="100%"
						align="top"
						scrolling="no"
						src='<%# !Convert.IsDBNull(Eval("Requirements"))
								? (string)Eval("cRoot") + (string)Eval("DiskFolder") + (string)Eval("Requirements")
								: "about:blank" %>'>
				</iframe>
			</asp:PlaceHolder>
			
			<asp:PlaceHolder
					runat="server"
					ID="phAddition"
					Visible='<%# doc.SelectNodes("xml/dsAdd/Additions[Addition]").Count > 0 %>'>
				<h3 class="cap3">
					<asp:Literal
							runat="server"
							ID="ltrAdditions"
							Text='<%$ Resources:CourseIntro,Additions %>' />:</h3>
				<table>
					<asp:Repeater
							runat="server"
							ID="rptAdditions"
							DataSource='<%# doc.SelectNodes("xml/dsAdd/Additions") %>'>
						<ItemTemplate>
							<tr><td><small>
										<%# SelectScalar(Container.DataItem, "Addition") %></small></td></tr>
						</ItemTemplate>
					</asp:Repeater>
				</table>
			</asp:PlaceHolder>
		</td>
	</tr>
</table>
	</ItemTemplate>
</asp:FormView>
<asp:ObjectDataSource
		ID="odsCourse"
		runat="server"
		OldValuesParameterFormatString="original_{0}"
		SelectMethod="SelectByCodeOrId"
		TypeName="DceAccessLib.DAL.CourseController"
		OnSelecting="odsCourse_Selecting">
	<SelectParameters>
		<asp:Parameter Name="reqCourseCode" Type="String" />
		<asp:Parameter Name="reqCourseId" Type="Object" />
		<asp:Parameter Name="CoursesRoot" Type="String" />
	</SelectParameters>
</asp:ObjectDataSource>
