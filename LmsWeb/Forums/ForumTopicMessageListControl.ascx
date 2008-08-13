<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ForumTopicMessageListControl.ascx.cs" Inherits="Forums_ForumTopicMessageListControl" %>
<asp:GridView
		ID="threadMessageListGridView"
		runat="server"
		AllowPaging="True"
		AutoGenerateColumns="False"
		DataKeyNames="ID"
		DataSourceID="MessageListDataSource"
		EmptyDataText="<%$ Resources:classRoom, ReplyNosing %>">
	<Columns>
		<asp:TemplateField
				HeaderText="<%$ Resources:classRoom, TopicAuthor %>"
				SortExpression="AuthorName">
			<ItemStyle VerticalAlign="Top" />
			<ItemTemplate>
				<nobr>
					<asp:Image
							ID="studentIconImage"
							runat="server"
							ImageUrl="~/Images/Student16-brown2.gif" />
					<asp:Label
							ID="authorLabel"
							runat="server"
							Text='<%# Eval("AuthorName") %>' />
				</nobr>
				<div style="font-size: 80%"><asp:Label ID="dateLabel" runat="server" Text='<%# Eval("PostDate", "{0:d} {0:t}") %>'></asp:Label></div>
			</ItemTemplate>
		</asp:TemplateField>
		<asp:BoundField
				DataField="Message"
				HeaderText="<%$ Resources:classRoom, MessageText %>"
				SortExpression="Message">
			<ItemStyle VerticalAlign="Top" Width="90%" />
		</asp:BoundField>
	</Columns>
</asp:GridView>
<asp:SqlDataSource
		ID="MessageListDataSource"
		runat="server"
		ConnectionString="<%$ ConnectionStrings:Dce2005ConnectionString %>"
		SelectCommand="dcetools_Trainings_Forum_GetTopicMessages"
		SelectCommandType="StoredProcedure"
		CancelSelectOnNullParameter="False"
		OnSelecting="MessageListDataSource_Selecting">
    <SelectParameters>
        <asp:SessionParameter Name="homeRegion" SessionField="homeRegion" />
		<asp:Parameter Name="trainingID" />
		<asp:Parameter Name="topicID" />
    </SelectParameters>
</asp:SqlDataSource>