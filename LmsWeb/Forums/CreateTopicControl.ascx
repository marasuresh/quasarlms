<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CreateTopicControl.ascx.cs" Inherits="Forums_CreateTopicControl" %>
<%@ Register Src="NewMessageControl.ascx" TagName="NewMessageControl" TagPrefix="uc1" %>
<uc1:NewMessageControl ID="newMessageControl" runat="server" OnSendClick="newMessageControl_Click" />
