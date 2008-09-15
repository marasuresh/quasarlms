<%@ Control Language="c#" Inherits="DCE.Common.Migrated_MainMenuControl" CodeFile="MainMenuControl.ascx.cs" %>

<script language="javascript" type="text/javascript">
function userProperty(){AddParameter("cset", "UserProperty");applyParameters();}
function forgotPassword(){AddParameter("cset", "ForgotPassw");applyParameters();}
</script>

	<table	cellpadding="1"
			cellspacing="0"
			border="0"
			width="100%"
			class="TopMenu"
			style="border-bottom:1px solid #4780C1;">
		<tr>
			<td style="padding-left:1em; padding-right:1em;">
				<asp:HyperLink
						runat="server"
						ID="hlHome"
						SkinID="HomeLink"
						NavigateUrl="<%$ Resources:PageUrl, PAGE_MAIN %>"
						ToolTip='<%$ Resources:HomeLeft,Home %>' /></td>
			<td><asp:Menu
						ID="mnMain"
						SkinID="MainMenu"
						runat="server"
						DataSourceID="SiteMapDataSource1"
						MaximumDynamicDisplayLevels="0" />
				<asp:SiteMapDataSource
						ID="SiteMapDataSource1"
						runat="server"
						ShowStartingNode="False" /></td>
			<td align="right">
				<asp:Menu
						ID="mnLang"
						SkinID="LanguageMenu"
						runat="server"
						StaticItemFormatString="<!---->"
						OnMenuItemClick="mnLang_MenuItemClick"
						OnPreRender="mnLang_PreRender">
	<Items>
		<asp:MenuItem
				ImageUrl="~/App_Themes/Default/images/lang_ukr.gif"
				ToolTip="Українська"
				Value="Ukrainian"
				Text="Ukrainian" />
		<asp:MenuItem
				ImageUrl="~/App_Themes/Default/images/lang_en.gif"
				ToolTip="English"
				Value="English"
				Text="English" />
		 <asp:MenuItem
				ImageUrl="~/App_Themes/Default/images/lang_ru.gif"
				ToolTip="Русский"
				Value="Russian"
				Text="Russian" /></Items>
	</asp:Menu>
			</td>
			<td style="padding-left:1em;padding-right:1em;" align="right"><asp:Hyperlink
						runat="server"
						ID="hlMail"
						SkinID="MailLink"
						NavigateUrl='mailto:admin'
						ToolTip='<%$ Resources:HomeLeft,Mail %>' /></td>
		</tr>
	</table>
<div id="dceLocalToolbar">
<table cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
	<tr vAlign="top">
		<td height="20">
			<asp:LoginView ID="LoginView1" runat="server">
				<AnonymousTemplate>
			<asp:Login
					ID="Login2"
					runat="server"
					OnLoggingIn="Login1_LoggingIn"
					OnLoggedIn="Login1_LoggedIn">
				<LayoutTemplate>
					<table	border="0"
							class="Login" width="100%">
						<tr height="18">
							<td><asp:Label
										ID="UserNameLabel"
										runat="server"
										AssociatedControlID="UserName"
										CssClass="Login_sign"
										Text="<%$ Resources: MainMenu, email %>" />
								<asp:TextBox
										ID="UserName"
										CssClass="field"
										runat="server" />
								<asp:RequiredFieldValidator
										ID="UserNameRequired"
										runat="server"
										ControlToValidate="UserName"
										Display="Dynamic"
										ErrorMessage="User Name is required."
										ToolTip="User Name is required."
										ValidationGroup="ctl00$Login1">*</asp:RequiredFieldValidator></td>
							<td style="width: 148px"><asp:Label
										ID="PasswordLabel"
										runat="server"
										AssociatedControlID="Password"
										CssClass="Login_sign"
										Text="<%$ Resources:MainMenu, password %>" />
								<asp:TextBox
										ID="Password"
										runat="server"
										CssClass="field"
										TextMode="Password" />
								<asp:RequiredFieldValidator
										ID="PasswordRequired"
										runat="server"
										ControlToValidate="Password"
										Display="Dynamic"
										ErrorMessage="Password is required."
										ToolTip="Password is required."
										ValidationGroup="ctl00$Login1">*</asp:RequiredFieldValidator></td>
							<td><asp:CheckBox
										ID="RememberMe"
										runat="server"
										CssClass="Login_sign"
										Text="Remember me next time." /></td>
							<td><asp:Button
										ID="LoginButton"
										runat="server"
										CommandName="Login"
										CssClass="submit_in"
										Text="LOGIN"
										ValidationGroup="ctl00$Login1" /></td>
							<td align="right">
								<asp:HyperLink
										runat="server"
										ID="hlForgotPassword"
										NavigateUrl="~/MainPage.aspx?cset=ForgotPassw"
										ToolTip="<%$ Resources:MainMenu, ForgotPassw_alt %>"
										Text="<%$ Resources:MainMenu, ForgotPassw_alt %>" />
								|
								<asp:HyperLink
										runat="server"
										ID="hlSignUp"
										NavigateUrl="~/SignUp.aspx"
										Text="Sign Up" /></td></tr>
					</table>
					<asp:Literal
							ID="FailureText"
							runat="server"
							EnableViewState="False" />
				</LayoutTemplate>
			</asp:Login>
				</AnonymousTemplate>
				<LoggedInTemplate>
					<div style="width:100%; text-align:right;">
						<asp:HyperLink
								runat="server"
								ID="hlProperties"
								ToolTip="<%$ Resources:MainMenu, UserProperty_alt %>"
								NavigateUrl="javascript:userProperty()">
							<asp:LoginName ID="LoginName1" runat="server" />
						</asp:HyperLink>
						<asp:LoginStatus
								ID="LoginStatus1"
								runat="server"
								CssClass="Login_sign" />
						&nbsp;&nbsp;&nbsp;
					</div>
				</LoggedInTemplate>
			</asp:LoginView>
		</td>
	</tr>
 </table>
</div>