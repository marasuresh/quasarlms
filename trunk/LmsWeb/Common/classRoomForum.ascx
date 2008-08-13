<%@ Control Language="c#" Inherits="DCE.classRoomForum" CodeFile="classRoomForum.ascx.cs" %>
<%@ Register TagPrefix="uc1" TagName="xmlControl" Src="xmlControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="dbTableControl" Src="dbTableControl.ascx" %>
	<uc1:dbTableControl
			InputVariables="trainingId;studentId"
			Index="1"
			inputSQL="ClassRoomTopicList"
			inputXSL="ClassRoomTopicList.xslt"
			Paging="true"
			PNPageSize="5"
			ApplySorting="true"
			InputOrderBy="PostDate "
			langXML="classRoom"
			ShowXml="false"
			id="DbTableControl1"
			runat="server" />
	<uc1:xmlControl
			id="Xmlcontrol2"
			langXML="classRoom"
			inputXSL="ClassRoomTopicPost.xslt"
			xmlVariables="Error;btnT1"
			runat="server" />
	<br>
	<uc1:dbTableControl
			inputSQL="ClassRoomTopicBody"
			inputXSL="ClassRoomTopicBody.xslt"
			Paging="false"
			langXML="classRoom"
			id="Dbtablecontrol2"
			runat="server" IsOne="true" />
	<br>
	<uc1:dbTableControl
			Index="2"
			inputSQL="ClassRoomReplyList"
			inputXSL="ClassRoomReplyList.xslt"
			Paging="true"
			PNPageSize="5"
			ApplySorting="true"
			InputOrderBy="PostDate "
			langXML="classRoom"
			ShowXml="false"
			id="Dbtablecontrol3"
			runat="server" IsOne="true" />
	<uc1:xmlControl
			id="XmlControl1"
			langXML="classRoom"
			inputXSL="ClassRoomPost.xslt"
			xmlVariables="xmlError"
			runat="server" />