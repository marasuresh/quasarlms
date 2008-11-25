﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MessageInput.ascx.cs"
    Inherits="N2.Messaging.Messaging.UI.Parts.MessageInput" %>
<%@ Import Namespace="N2.Web" %>
<%@ Import Namespace="N2.Messaging" %>
<%--<script type="text/javascript" src="../Js/jQuery.intellisense.js"></script>--%>
<n2:Box ID="commentInput" runat="server" CssClass="box" meta:resourcekey="BoxResource1">
	<table style="table-layout: fixed" cellspacing="0" cellpadding="0" width="100%">
		<tr><td width="20%">
				<asp:Label
						runat="server"
						AssociatedControlID="selUser"
						CssClass="label"
						meta:resourcekey="lblToResource1" /></td>
			<td>
			    <n2:SelectUser ID="selUser" runat="server" />
			    <asp:TextBox ID="windowOpen" runat="server"></asp:TextBox>
                <n2:PopUpWindow ID="PopUpWindow1" runat="server" Title="Выбор адресата..." AssociatedControlID="windowOpen" AllignTo="AssociatedControl">
                    <div>Inside a window</div>
                </n2:PopUpWindow>
			</td>
		</tr>
		<tr><td><asp:Label
						runat="server"
						AssociatedControlID="txtSubject"
						CssClass="label"
						meta:resourcekey="lblSubjectResource1" /></td>
			<td><asp:TextBox
						ID="txtSubject"
						runat="server"
						CssClass="tb"
						meta:resourcekey="txtSubjectResource1"
						Width="90%" />
				<asp:RequiredFieldValidator
						ID="RequiredFieldValidator1"
						runat="server"
						ValidationGroup="CommentInput"
						ControlToValidate="txtSubject"
						Text="*"
						Display="Dynamic"
						meta:resourcekey="rfvSubjectResource1" /></td></tr>
		<tr><td><asp:Label
						runat="server"
						AssociatedControlID="btnFileUpload"
						CssClass="label"
						meta:resourcekey="lblUploadResource1" /></td>
			<td><asp:FileUpload
						ID="btnFileUpload"
						runat="server"
						Width="90%" /></td></tr>
		<tr><td><asp:Label
						runat="server"
						AssociatedControlID="rblMessageType"
						CssClass="label"
						meta:resourcekey="rblMessageType"
						Text="Тип" /></td>
			<td><asp:RadioButtonList
						runat="server"
						ID="rblMessageType"
						RepeatDirection="Horizontal">
					<asp:ListItem Value='Letter' Selected="True" Text="">Письмо</asp:ListItem>
					<asp:ListItem Value='Announcement'>Объявление</asp:ListItem>
					<asp:ListItem Value='Task'>Задача</asp:ListItem>
				</asp:RadioButtonList></td></tr>
		<tr><td colspan="2"><br /></td></tr>
		<tr><td align="center" colspan="2">
				<n2:FreeTextArea
						ID="txtText"
						runat="server"
						TextMode="MultiLine"
						meta:resourcekey="txtTextResource1"
						CssClass="freeTextArea"
						EnableFreeTextArea="True"
						Width="100%" />
				<ul class="buttons">
					<li><asp:LinkButton
							ID="btnSubmit"
							runat="server"
							OnClick="btnSubmit_Click"
							Text="Submit"
							meta:resourcekey="btnSubmitResource1" /></li>
					<li><asp:LinkButton
							ID="btnToDr"
							runat="server"
							OnClick="btnToDr_Click"
							Text="ToDr"
							meta:resourcekey="btnToDrResource1" /></li>
					<li><asp:LinkButton
							ID="hlCancel"
							Text="Cancel"
							meta:resourcekey="btnCancelResource1"
							runat="server" /></li>
				</ul></td></tr>
	</table>
</n2:Box>
