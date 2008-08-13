<%@ Reference Page="~/Learn/Trainings.aspx" %>
<%@ Control Language="c#" Inherits="DCE.Common.Members" CodeFile="Members.ascx.cs" %>
<%@ Import Namespace="System.Xml" %>

<script language="javascript"><!--
 function resizeFrame()
 {
 theHeight = document.getElementById("contFrameId").contentWindow.document.body.scrollHeight;
 document.getElementById("contFrameId").style.height = theHeight;
 }
--></script>

<asp:FormView
		runat="server"
		ID="fvStudents"
		DataSourceID="odsCourse"
		OnDataBound="fvStudents_DataBound">
	<ItemTemplate>
<table	cellspacing="0"
		cellpadding="0"
		width="100%"
		border="0"
		align="center"
		class="InnerTable">
	<tr valign="top">
		<td width="75%" style="PADDING-TOP: 7px">
			<h3 class="cap3">
					<asp:Literal
							runat="server"
							ID="ltrCaption"
							Text='<%$ Resources:Members,Caption %>' /></h3>
			
			<h3 class="cap4" title='<%# Eval("Description") %>'>
				<%# Eval("Name")%>
			<br/><a><%# string.Format(@"{0:d}&nbsp;/&nbsp;{0:d}",
							Eval("StartDate"),
							Eval("EndDate")) %></a></h3>
		</td>
	</tr>
</table>

<hr/>
	<p		class="CenterColumn">
		<asp:Image ID="Image1"
				runat="server"
				ImageUrl="~/images/bullet.gif"
				Width="14"
				Height="14"
				border="0" />
		&nbsp;&nbsp;&nbsp;
		<strong	class="yellow">
			<asp:Literal
					runat="server"
					ID="ltrStudents"
					Text='<%$ Resources:Members,Students %>' /></strong></p>
<asp:GridView
		ID="GridView1"
		runat="server"
		AllowPaging="True"
		AllowSorting="True"
		DataSourceID="odsStudents"
		CssClass="TableList"
		CellPadding="3"
		CellSpacing="0"
		AutoGenerateColumns="false">
	<AlternatingRowStyle
			BackColor="#ffffff" />
	<RowStyle
			BackColor="#F6F6F6" />
	<Columns>
		<asp:ImageField
				DataImageUrlField="Photo"
				DataImageUrlFormatString="~/UserPhoto.aspx?id={0}"
				ControlStyle-Width="80"
				HeaderText='<%$ Resources:Members,Members_Header_Photo %>'
				HeaderStyle-CssClass="HeadCenter" />
		<asp:BoundField
				DataField="Name"
				HeaderStyle-CssClass="HeadCenter"
				HeaderText='<%$ Resources:Members,Members_Header_Name %>'
				ItemStyle-Width="40%"
				ItemStyle-Wrap="false" />
		<asp:HyperLinkField
				DataNavigateUrlFields="Email"
				DataNavigateUrlFormatString="mailto:{0}"
				DataTextField="Email"
				DataTextFormatString="<a href='mailto:{0:G}'>{0:G}</a>"
				ItemStyle-Width="55%"
				HeaderStyle-CssClass="HeadCenter"
				HeaderText='<%$ Resources:Members, Members_Header_email %>' />
	</Columns>
</asp:GridView>
<asp:ObjectDataSource
		ID="odsStudents"
		runat="server"
		OldValuesParameterFormatString="original_{0}"
		SelectMethod="SelectByTraining"
		TypeName="DceAccessLib.DAL.StudentController"
		OnSelecting="odsStudents_Selecting">
	<SelectParameters>
		<asp:Parameter Name="trainingId" Type="Object" />
	</SelectParameters>
</asp:ObjectDataSource>
	</ItemTemplate>
</asp:FormView>
<asp:ObjectDataSource ID="odsCourse" runat="server" OldValuesParameterFormatString="original_{0}"
	SelectMethod="SelectByTraining" TypeName="DceAccessLib.DAL.CourseController" OnSelecting="odsStudents_Selecting">
	<SelectParameters>
		<asp:Parameter Name="trainingId" Type="Object" />
	</SelectParameters>
</asp:ObjectDataSource>
