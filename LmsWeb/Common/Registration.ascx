<%@ Control Language="c#" Inherits="DCE.Common.Registration" CodeFile="Registration.ascx.cs" %>
<asp:CreateUserWizard
		ID="createUserWizard"
		runat="server"
		OnCreatedUser="createUserWizard_CreatedUser"
		LoginCreatedUser="true"
		CssClass="RegForm"
		CreateUserButtonText="<%$ Resources:registration,Submit %>">
	<WizardSteps>
		<asp:CreateUserWizardStep
				ID="createUserWizardStep"
				runat="server">
			<ContentTemplate>
				<table	border="0"
						class="RegForm">
					<tr><td align="center" colspan="2">
							<asp:Literal
									runat="server"
									ID="ltrPageNote"
									Text="<%$ Resources:Registration, PageNote2 %>" /></td>
					</tr>
					<tr><td align="right">
							<asp:Label
									ID="UserNameLabel"
									runat="server"
									AssociatedControlID="UserName"
									Text="<%$ Resources:Registration, FirstName %>" /></td>
						<td><asp:TextBox
									ID="UserName"
									runat="server" />
							<asp:RequiredFieldValidator
									ID="UserNameRequired"
									runat="server"
									ControlToValidate="UserName"
									Display="Dynamic"
									ErrorMessage="User Name is required."
									ToolTip="User Name is required."
									ValidationGroup="CreateUserWizard1"
									Text="*" /></td></tr>
					<tr><td align="right">
							<asp:Label
									ID="PasswordLabel"
									runat="server"
									AssociatedControlID="Password"
									Text="<%$ Resources:Registration, Password %>" /></td>
						<td><asp:TextBox
									ID="Password"
									runat="server"
									TextMode="Password" />
							<asp:RequiredFieldValidator
									ID="PasswordRequired"
									runat="server"
									ControlToValidate="Password"
									ErrorMessage="Password is required."
									ToolTip="Password is required."
									ValidationGroup="CreateUserWizard1"
									Display="Dynamic"
									Text="*" /></td></tr>
					<tr><td align="right">
							<asp:Label
									ID="ConfirmPasswordLabel"
									runat="server"
									AssociatedControlID="ConfirmPassword"
									Text="<%$ Resources:Registration, PasswordConfirm %>" /></td>
						<td><asp:TextBox
									ID="ConfirmPassword"
										runat="server"
										TextMode="Password" />
								<asp:RequiredFieldValidator
										ID="ConfirmPasswordRequired"
										runat="server"
										ControlToValidate="ConfirmPassword"
										Display="Dynamic"
										ErrorMessage="Confirm Password is required."
										ToolTip="Confirm Password is required."
										ValidationGroup="CreateUserWizard1"
										Text="*" /></td></tr>
						<tr><td		align="right">
								<asp:Label
										ID="EmailLabel"
										runat="server"
										AssociatedControlID="Email"
										Text="<%$ Resources:Registration, Email %>" /></td>
							<td><asp:TextBox
										ID="Email"
										runat="server"
										AutoCompleteType="email" />
								<asp:RequiredFieldValidator
										ID="EmailRequired"
										runat="server"
										ControlToValidate="Email"
										Display="Dynamic"
										ErrorMessage="E-mail is required."
										ToolTip="E-mail is required."
										ValidationGroup="CreateUserWizard1"
										Text="*" /></td></tr>
						<tr><td align="right">
								<asp:Label
										ID="QuestionLabel"
										runat="server"
										AssociatedControlID="Question">Security Question:</asp:Label></td>
							<td>
								<asp:TextBox ID="Question" runat="server"></asp:TextBox>
								<asp:RequiredFieldValidator ID="QuestionRequired" runat="server" ControlToValidate="Question"
										Display="Dynamic"
									ErrorMessage="Security question is required." ToolTip="Security question is required."
									ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
							</td>
						</tr>
						<tr>
							<td align="right">
								<asp:Label ID="AnswerLabel" runat="server" AssociatedControlID="Answer">Security Answer:</asp:Label></td>
							<td>
								<asp:TextBox ID="Answer" runat="server"></asp:TextBox>
								<asp:RequiredFieldValidator ID="AnswerRequired" runat="server" ControlToValidate="Answer"
										Display="Dynamic"
									ErrorMessage="Security answer is required." ToolTip="Security answer is required."
									ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
							</td>
						</tr>
						<tr>
							<td align="center" colspan="2">
								<asp:CompareValidator ID="PasswordCompare" runat="server" ControlToCompare="Password"
									ControlToValidate="ConfirmPassword" Display="Dynamic" ErrorMessage="The Password and Confirmation Password must match."
									ValidationGroup="CreateUserWizard1"></asp:CompareValidator>
							</td>
						</tr>
						<tr>
							<td align="center" colspan="2" style="color: red">
								<asp:Literal ID="ErrorMessage" runat="server" EnableViewState="False"></asp:Literal>
							</td>
						</tr>
						<tr><td align="right">
								<asp:Label
										runat="server"
										ID="lLastName"
										AssociatedControlID="tbLastName"
										Text="<%$ Resources:Registration, LastName %>" />
								</td><td>
								<asp:TextBox
										runat="server"
										ID="tbLastName"
										AutoCompleteType="lastname"
										MaxLength="50" /></td></tr>
						<tr><td align="right">
								<asp:Label
										runat="server"
										ID="lFirstName"
										AssociatedControlID="tbFirstName"
										Text="<%$ Resources:Registration, FirstName %>" /></td>
							<td><asp:TextBox
										runat="server"
										ID="tbFirstName"
										AutoCompleteType="firstname"
										MaxLength="30" /></td></tr>
						<tr><td align="right" class="btn">
								<asp:Label
										runat="server"
										ID="lMidName"
										AssociatedControlID="tbMidName"
										Text="<%$ Resources:Registration, MidName %>" /></td>
							<td><asp:TextBox
										runat="server"
										ID="tbMidName"
										AutoCompleteType="middlename"
										MaxLength="50" /></td></tr>
					</table>
				</ContentTemplate>
			</asp:CreateUserWizardStep>
			<asp:CompleteWizardStep ID="CompleteWizardStep1" runat="server">
			</asp:CompleteWizardStep>
		</WizardSteps>
	</asp:CreateUserWizard>
