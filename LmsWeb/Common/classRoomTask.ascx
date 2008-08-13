<%@ Reference Control="~/common/dbtablecontrol.ascx" %>
<%@ Control Language="c#" Inherits="DCE.classRoomTask" CodeFile="classRoomTask.ascx.cs" %>
<%@ Register TagPrefix="uc1" TagName="dbTableControl" Src="dbTableControl.ascx" %>

<uc1:dbTableControl
		Index="1"
		InputVariables="trainingId;studentId;dceLanguage;dceDefLang;recordId"
		inputSQL="ClassRoomTaskList"
		inputXSL="ClassRoomTaskList.xslt"
		Paging="true"
		PNPageSize="5"
		ApplySorting="true"
		InputOrderBy="TaskTime "
		langXML="classRoom"
		ShowXml="false"
		id="DbTableControl1"
		runat="server" />
<br />
<asp:PlaceHolder id="PlaceHolder1" runat="server"></asp:PlaceHolder>
