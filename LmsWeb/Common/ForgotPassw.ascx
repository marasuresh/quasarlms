<%@ Control Language="c#" Inherits="DCE.Common.ForgotPassw" CodeFile="ForgotPassw.ascx.cs" %>
<asp:PasswordRecovery
		ID="PasswordRecovery1"
		runat="server">
	<UserNameTemplate>
		<h3 class="cap4">
			<asp:Literal
					runat="server"
					ID="ltrPageName"
					Text="<%$ Resources:PassWord, PageName %>" /></h3>
		<p class="blue">
			<asp:Literal
					runat="server"
					ID="ltrEnterEmail"
					Text="<%$ Resources:PassWord, EnterEmail %>" /></p>
		<table	align="left"
				border="0"
				cellpadding="0"
				cellspacing="0"
				width="100%"
				class="RegForm">
			<tr><colgroup>
					<col	style="padding-left: 0;"
							width="250px"
							nowrap="true" />
					<col	width="50%"/>
				</colgroup></tr>
			<tr><td align="right">
					<asp:TextBox
							ID="UserName"
							runat="server"
							MaxLength="80" />
					<asp:RequiredFieldValidator
							ID="UserNameRequired"
							runat="server"
							ControlToValidate="UserName"
							Display="Dynamic"
							ErrorMessage="User Name is required."
							ToolTip="User Name is required."
							ValidationGroup="ctl00$PasswordRecovery1">*</asp:RequiredFieldValidator>
				</td>
				<td class="btn">&nbsp;&nbsp;&nbsp;<asp:Button
							ID="SubmitButton"
							runat="server"
							CommandName="Submit"
							Text="<%$ Resources:PassWord, Submit %>"
							ValidationGroup="ctl00$PasswordRecovery1" /></td></tr>
			<tr><td colspan="2">
					<p class="gray">
						<asp:Label
								runat="server"
								ID="UserNameLabel"
								 AssociatedControlID="UserName"
								Text="<%$ Resources:PassWord, YouHave %>" /></p>
					<asp:Label
							ForeColor="red"
							ID="FailureText"
							runat="server"
							EnableViewState="False" />
				</td>
			</tr>
		</table>
	</UserNameTemplate>
</asp:PasswordRecovery>
