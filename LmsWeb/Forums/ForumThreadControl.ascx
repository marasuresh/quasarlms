<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ForumThreadControl.ascx.cs" Inherits="Forums_ForumThreadControl" %>
<%@ Register Src="CreateMessageControl.ascx" TagName="CreateMessageControl" TagPrefix="uc2" %>
<%@ Register Src="ForumTopicMessageListControl.ascx" TagName="ForumTopicMessageListControl"
    TagPrefix="uc1" %>
<uc1:ForumTopicMessageListControl
		ID="ForumTopicMessageListControl1"
		runat="server"
		PageSize="6"
		TopicId='<%# this.TopicId %>'
		TrainingId='<%# this.TrainingId %>' />
<br />
<br />
<uc2:CreateMessageControl
		id="CreateMessageControl1"
		runat="server"
		OnMessageCreated="CreateMessageControl1_MessageCreated"
		TopicId='<%# this.TopicId %>'
		TrainingId='<%# this.TrainingId %>' />
