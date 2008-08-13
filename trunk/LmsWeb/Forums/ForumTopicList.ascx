<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ForumTopicList.ascx.cs" Inherits="Forums_ForumTopicList" %>
<%@ Register Src="ForumThreadControl.ascx" TagName="ForumThreadControl" TagPrefix="uc1" %>
<asp:GridView
		ID="topicListGridView"
		runat="server"
		AllowPaging="True"
		AutoGenerateColumns="False"
		DataKeyNames="ID"
		DataSourceID="TopicsDataSource"
		OnRowCommand="topicListGridView_RowCommand"
		EmptyDataText='<%$ Resources:classRoom, TopicNosing %>'
		AllowSorting="True"
		PageSize="5"
		SkinID="ForumTopicList">
    <Columns>
        <asp:TemplateField meta:resourcekey="TemplateFieldResource1">
            <ItemTemplate>
                <asp:Image
						ID="topicIconImage"
						runat="server"
						ImageUrl="~/Images/card-people.gif" />
            </ItemTemplate>
            <ItemStyle Width="1px" VerticalAlign=Top />
        </asp:TemplateField>
		<asp:TemplateField
				HeaderText="<%$ Resources:classRoom, ReplyMsg %>"
				ShowHeader="False"
				SortExpression="Topic">
			<EditItemTemplate>
				<asp:Label
						ID="topicLabel"
						runat="server"
						Text='<%# Eval("Topic") %>' /><br />
				<div><asp:Label
							ID="messageLabel"
							runat="server"
							Text='<%# Eval("Message") %>' /></div>
				<uc1:ForumThreadControl
						ID="ForumThreadControl1"
						runat="server"
						TopicID='<%# Eval("ID") %>'
						TrainingId='<%# this.TrainingId %>'
						OnMessageCreated="ForumThreadControl1_MessageCreated" />
			</EditItemTemplate>
            <ItemStyle Width="60%" VerticalAlign="Top" />
            <ItemTemplate>
                <asp:LinkButton ID="topicLinkButton"
                    runat="server"
                    CausesValidation="False"
                    CommandName="Edit"
                    Text='<%# Eval("Topic") + " ("+Eval("MessageCount")+")" %>'
                    ToolTip='<%# Eval("Message") %>' />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="StudentFullName" HeaderText="<%$ Resources:classRoom, TopicAuthor %>" ReadOnly="True" SortExpression="StudentFullName" >
            <ItemStyle VerticalAlign="Top" />
        </asp:BoundField>
        <asp:BoundField
				DataField="LastPostDate"
				HeaderText="<%$ Resources:classRoom, PostDate %>"
				ReadOnly="True"
				SortExpression="LastPostDate"
				DataFormatString="{0:d} {0:t}"
				HtmlEncode="False" >
            <ItemStyle HorizontalAlign="Center" Width="5em" VerticalAlign="Top" />
        </asp:BoundField>
    </Columns>
</asp:GridView>
<asp:SqlDataSource
		ID="TopicsDataSource"
		runat="server"
		ConnectionString="<%$ ConnectionStrings:Dce2005ConnectionString %>"
		SelectCommand="dcetools_Trainings_Forum_GetTopicList"
		SelectCommandType="StoredProcedure"
		CancelSelectOnNullParameter="False"
		OnSelecting="TopicsDataSource_Selecting">
	<SelectParameters>
		<asp:SessionParameter
				Name="homeRegion"
				SessionField="homeRegion" />
		<asp:Parameter Name="trainingID" />
	</SelectParameters>
</asp:SqlDataSource>