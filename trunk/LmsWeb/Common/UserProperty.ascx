<%@ Control Language="c#" Inherits="DCE.Common.UserProperty" CodeFile="UserProperty.ascx.cs" %>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<asp:ChangePassword ID="ChangePassword1" runat="server" BorderStyle="Dotted" BorderWidth="1px" ChangePasswordButtonText="<%$ Resources:Registration,ChangePassword %>" ChangePasswordTitleText="<%$ Resources:Registration, ChangePass %>" ConfirmNewPasswordLabelText="<%$ Resources:Registration,PasswordConfirm %>" CssClass="RegForm" NewPasswordLabelText="<%$ Resources:Registration,Password %>" PasswordLabelText="<%$ Resources:Registration,PasswordOld %>">
	<ChangePasswordButtonStyle CssClass="btn" />
	<TitleTextStyle Font-Bold="True" Font-Size="10pt" HorizontalAlign="Left" />
</asp:ChangePassword>
</ContentTemplate>
</asp:UpdatePanel>

<asp:FormView
		ID="fvUserProperties"
		runat="server"
		DataKeyNames="id"
		DataSourceID="dsUsers"
		DefaultMode="Edit"
		OnItemUpdating="fvUserProperties_ItemUpdating"
		CssClass="RegForm">
	<EditItemTemplate>
		<table>
			<tr><td><asp:Label
							runat="server"
							ID="lFirstName"
							AssociatedControlID="FirstNameTextBox"
							Text="<%$ Resources:Registration, FirstName %>" /></td>
				<td><asp:TextBox
							ID="FirstNameTextBox"
							runat="server"
							MaxLength="30"
							Text='<%# Bind("FirstName") %>' />
					<asp:RequiredFieldValidator
							runat="server"
							ID="rfvFirstName"
							Display="Dynamic"
							ControlToValidate="FirstNameTextBox" /></td></tr>
			<tr><td><asp:Label
							runat="server"
							ID="lPatronymic"
							AssociatedControlID="PatronymicTextBox"
							CssClass="LeftPad"
							Text="<%$ Resources:registration, MidName %>" /></td>
				<td><asp:TextBox
							ID="PatronymicTextBox"
							runat="server"
							MaxLength="50"
							Text='<%# Bind("Patronymic") %>' /></td></tr>
			<tr><td><asp:Label
							runat="server"
							ID="lLastName"
							AssociatedControlID="LastNameTextBox"
							Text="<%$ Resources:registration, LastName %>" /></td>
				<td><asp:TextBox
							ID="LastNameTextBox"
							runat="server"
							MaxLength="50"
							Text='<%# Bind("LastName") %>' />
					<asp:RequiredFieldValidator
							runat="server"
							ID="rfvLastName"
							Display="Dynamic"
							ControlToValidate="LastNameTextBox" /></td></tr>
			<tr><td><asp:Label
							runat="server"
							ID="lFirstNameEng"
							AssociatedControlID="FirstNameEngTextBox"
							Text="First name:" /></td>
				<td><asp:TextBox
							ID="FirstNameEngTextBox"
							runat="server"
							MaxLength="30"
							Text='<%# Bind("FirstNameEng") %>' /></td></tr>
			<tr><td><asp:Label
							runat="server"
							ID="lLastNameEng"
							AssociatedControlID="LastNameEngTextBox"
							Text="Last Name:" /></td>
				<td><asp:TextBox
							ID="LastNameEngTextBox"
							runat="server"
							MaxLength="50"
							Text='<%# Bind("LastNameEng") %>' /></td></tr>
			<tr><td><asp:Label
							runat="server"
							ID="lBirthday"
							AssociatedControlID="BirthdayTextBox"
							Text="<%$ Resources:registration, Birth %>" /></td>
				<td><asp:TextBox
							ID="BirthdayTextBox"
							runat="server"
							Text='<%# Bind("Birthday") %>' />
					<asp:CompareValidator
							runat="server"
							ID="cmpvalBirthday"
							ControlToValidate="BirthdayTextBox"
							Operator="DataTypeCheck"
							Type="Date"
							Display="Dynamic" />
					<asp:CalendarExtender
							ID="CalendarExtender1"
							runat="server"
							TargetControlID="BirthdayTextBox" /></td></tr>
			<tr><td><asp:Label
							runat="server"
							ID="lSex"
							AssociatedControlID="rblGender"
							Text="<%$ Resources:registration,Sex %>" /></td>
				<td><asp:RadioButtonList
							runat="server"
							ID="rblGender"
							SelectedValue='<%# Bind("Sex") %>'
							RepeatDirection="Horizontal"
							RepeatLayout="Flow">
						<asp:ListItem Text="<%$ Resources:registration,Female %>" Value="False" />
						<asp:ListItem Text="<%$ Resources:registration,Male %>" Value="True" />
						<asp:ListItem Text="n/a" Value="" />
					</asp:RadioButtonList></td></tr>
			<tr><td><asp:Label
							runat="server"
							ID="lOrganization"
							AssociatedControlID="OrganizationTextBox"
							Text="<%$ Resources:registration, CompanyName %>" /></td>
				<td><asp:TextBox
							ID="OrganizationTextBox"
							runat="server"
							Text='<%# Bind("Organization") %>' /></td></tr>
			<tr><td><asp:Label
							runat="server"
							ID="lChiefPhone"
							AssociatedControlID="ChiefPhoneTextBox"
							Text="<%$ Resources:registration, ManagerPhone %>" /></td>
				<td><asp:TextBox
							ID="ChiefPhoneTextBox"
							runat="server"
							Text='<%# Bind("ChiefPhone") %>' /></td></tr>
			<tr><td><asp:Label
							runat="server"
							ID="lChiefPosition"
							AssociatedControlID="ChiefPositionTextBox"
							Text="<%$ Resources:registration, ManagerOccup %>" /></td>
				<td><asp:TextBox
							ID="ChiefPositionTextBox"
							runat="server"
							Text='<%# Bind("ChiefPosition") %>' /></td></tr>
			<tr><td><asp:Label
							ID="lChief"
							runat="server"
							AssociatedControlID="ChiefTextBox"
							Text="<%$ Resources:registration, Manager %>" /></td>
				<td><asp:TextBox
							ID="ChiefTextBox"
							runat="server"
							Text='<%# Bind("Chief") %>' /></td></tr>
			<tr><td><asp:Label
							runat="server"
							ID="lJobPosition"
							AssociatedControlID="JobPositionTextBox"
							Text="<%$ Resources:registration, JobOccup %>" /></td>
				<td><asp:TextBox
							ID="JobPositionTextBox"
							runat="server"
							Text='<%# Bind("JobPosition") %>' /></td></tr>
			<tr><td><asp:Label
							runat="server"
							ID="lOrgType"
							AssociatedControlID="OrgTypeTextBox"
							Text="<%$ Resources:registration, CompanyType %>" /></td>
				<td><asp:TextBox
							ID="OrgTypeTextBox"
							runat="server"
							Text='<%# Bind("OrgType") %>' /></td></tr>
			<tr><td><asp:Label
							runat="server"
							ID="lCountry"
							AssociatedControlID="CountryDropDown"
							Text="<%$ Resources:registration, Country %>" /></td>
				<td><asp:DropDownList
							runat="server"
							ID="CountryDropDown"
							DataSourceID="xdsCountries"
							DataTextField="text"
							DataValueField="value"
							AppendDataBoundItems="true"
							Text='<%# Bind("Country") %>' >
						<asp:ListItem Value="" Text="...select the country..." />
					</asp:DropDownList></td></tr>
			<tr><td><asp:Label
							runat="server"
							ID="lCity"
							AssociatedControlID="CityTextBox"
							Text="<%$ Resources:registration, City %>" /></td>
				<td><asp:TextBox
							ID="CityTextBox"
							runat="server"
							Text='<%# Bind("City") %>' /></td></tr>
			<tr><td><asp:Label
							runat="server"
							ID="lZip"
							AssociatedControlID="ZIPTextBox"
							Text="<%$ Resources:registration, PostalCode %>" /></td>
				<td><asp:TextBox
							ID="ZIPTextBox"
							runat="server"
							Text='<%# Bind("ZIP") %>' /></td></tr>
			<tr><td><asp:Label
							runat="server"
							ID="lAddress"
							AssociatedControlID="AddressTextBox"
							Text="<%$ Resources:registration, Adress %>" /></td>
				<td><asp:TextBox
							ID="AddressTextBox"
							runat="server"
							Text='<%# Bind("Address") %>' /></td></tr>
			<tr><td><asp:Label
							runat="server"
							ID="lPhone"
							AssociatedControlID="PhoneTextBox"
							Text="<%$ Resources:registration,PhoneCityCode %>" /></td>
				<td><asp:TextBox
							ID="PhoneTextBox"
							runat="server"
							Text='<%# Bind("Phone") %>' /></td></tr>
			<tr><td><asp:Label
							runat="server"
							ID="lCertificates"
							AssociatedControlID="CertificatesTextBox"
							Text="<%$ Resources:registration,Cert %>" /></td>
				<td><asp:TextBox
							ID="CertificatesTextBox"
							runat="server"
							Text='<%# Bind("Certificates") %>' /></td></tr>
			<tr><td><asp:Label
							runat="server"
							ID="lCourses"
							AssociatedControlID="CoursesTextBox"
							Text="<%$ Resources:registration,Courses %>" /></td>
				<td><asp:TextBox
							ID="CoursesTextBox"
							runat="server"
							Text='<%# Bind("Courses") %>' /></td></tr>
			<tr><td><asp:Label
							runat="server"
							ID="lEducation"
							AssociatedControlID="EducationTextBox"
							Text="<%$ Resources:registration,CollegeProf %>" /></td>
				<td><asp:TextBox
							ID="EducationTextBox"
							runat="server"
							Text='<%# Bind("Education") %>' /></td></tr>
			<tr><td><asp:Label
							runat="server"
							ID="lFax"
							AssociatedControlID="FaxTextBox"
							Text="<%$ Resources:registration,FaxCityCode %>" /></td>
				<td><asp:TextBox
							ID="FaxTextBox"
							runat="server"
							Text='<%# Bind("Fax") %>' /></td></tr>
			<tr><td><asp:Label
							runat="server"
							ID="lComments"
							AssociatedControlID="CommentsTextBox"
							Text="<%$ Resources:registration,AddInfo %>" /></td>
				<td><asp:TextBox
							ID="CommentsTextBox"
							runat="server"
							TextMode="MultiLine"
							MaxLength="512"
							Rows="5"
							Columns="40"
							Text='<%# Bind("Comments") %>' /></td></tr>
			<tr><td><asp:Label
							runat="server"
							ID="lPhoto"
							AssociatedControlID="fuPhoto"
							Text="<%$ Resources:registration,Photo %>" /><br />
					<asp:Image
							runat="server"
							ID="imgPhoto"
							ImageUrl='<%# Eval("Photo", "~/UserPhoto.aspx?id={0}") %>'
							Height="120" /></td>
				<td><asp:FileUpload
							runat="server"
							ID="fuPhoto" /></td></tr>
			<tr><td colspan="2" class="btn" align="center">
					<asp:Button
							ID="UpdateButton"
							runat="server"
							CausesValidation="True"
							CommandName="Update"
							Text="<%$ Resources:registration,Submit %>" />
					<asp:Button
							ID="UpdateCancelButton"
							runat="server"
							CausesValidation="False"
							CommandName="Cancel"
							Text="<%$ Resources:registration,Reset %>" />
				</td></tr>
		</table>
		<asp:HiddenField
				id="hfldPhoto"
				runat="server"
				Value='<%# Bind("Photo") %>' />
	</EditItemTemplate>
	<InsertItemTemplate>
		FirstName:
		<asp:TextBox ID="FirstNameTextBox" runat="server" Text='<%# Bind("FirstName") %>'>
		</asp:TextBox><br />
		Patronymic:
		<asp:TextBox ID="PatronymicTextBox" runat="server" Text='<%# Bind("Patronymic") %>'>
		</asp:TextBox><br />
		LastName:
		<asp:TextBox ID="LastNameTextBox" runat="server" Text='<%# Bind("LastName") %>'>
		</asp:TextBox><br />
		FirstNameEng:
		<asp:TextBox ID="FirstNameEngTextBox" runat="server" Text='<%# Bind("FirstNameEng") %>'>
		</asp:TextBox><br />
		LastNameEng:
		<asp:TextBox ID="LastNameEngTextBox" runat="server" Text='<%# Bind("LastNameEng") %>'>
		</asp:TextBox><br />
		Birthday:
		<asp:TextBox ID="BirthdayTextBox" runat="server" Text='<%# Bind("Birthday") %>'>
		</asp:TextBox><br />
		<%--Sex:
		<asp:CheckBox ID="SexCheckBox" runat="server" Checked='<%# Bind("Sex") %>' /><br />
		--%>Organization:
		<asp:TextBox ID="OrganizationTextBox" runat="server" Text='<%# Bind("Organization") %>'>
		</asp:TextBox><br />
		ChiefPhone:
		<asp:TextBox ID="ChiefPhoneTextBox" runat="server" Text='<%# Bind("ChiefPhone") %>'>
		</asp:TextBox><br />
		ChiefPosition:
		<asp:TextBox ID="ChiefPositionTextBox" runat="server" Text='<%# Bind("ChiefPosition") %>'>
		</asp:TextBox><br />
		Chief:
		<asp:TextBox ID="ChiefTextBox" runat="server" Text='<%# Bind("Chief") %>'>
		</asp:TextBox><br />
		JobPosition:
		<asp:TextBox ID="JobPositionTextBox" runat="server" Text='<%# Bind("JobPosition") %>'>
		</asp:TextBox><br />
		OrgType:
		<asp:TextBox ID="OrgTypeTextBox" runat="server" Text='<%# Bind("OrgType") %>'>
		</asp:TextBox><br />
		Country:
		<asp:TextBox ID="CountryTextBox" runat="server" Text='<%# Bind("Country") %>'>
		</asp:TextBox><br />
		City:
		<asp:TextBox ID="CityTextBox" runat="server" Text='<%# Bind("City") %>'>
		</asp:TextBox><br />
		ZIP:
		<asp:TextBox ID="ZIPTextBox" runat="server" Text='<%# Bind("ZIP") %>'>
		</asp:TextBox><br />
		Address:
		<asp:TextBox ID="AddressTextBox" runat="server" Text='<%# Bind("Address") %>'>
		</asp:TextBox><br />
		Phone:
		<asp:TextBox ID="PhoneTextBox" runat="server" Text='<%# Bind("Phone") %>'>
		</asp:TextBox><br />
		Certificates:
		<asp:TextBox ID="CertificatesTextBox" runat="server" Text='<%# Bind("Certificates") %>'>
		</asp:TextBox><br />
		Courses:
		<asp:TextBox ID="CoursesTextBox" runat="server" Text='<%# Bind("Courses") %>'>
		</asp:TextBox><br />
		Education:
		<asp:TextBox ID="EducationTextBox" runat="server" Text='<%# Bind("Education") %>'>
		</asp:TextBox><br />
		Email:
		<asp:TextBox ID="EmailTextBox" runat="server" Text='<%# Bind("Email") %>'>
		</asp:TextBox><br />
		Fax:
		<asp:TextBox ID="FaxTextBox" runat="server" Text='<%# Bind("Fax") %>'>
		</asp:TextBox><br />
		Comments:
		<asp:TextBox ID="CommentsTextBox" runat="server" Text='<%# Bind("Comments") %>'>
		</asp:TextBox><br />
		<asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert"
			Text="Insert">
		</asp:LinkButton>
		<asp:LinkButton ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
			Text="Cancel">
		</asp:LinkButton>
	</InsertItemTemplate>
	<ItemTemplate>
		id:
		<asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>'></asp:Label><br />
		FirstName:
		<asp:Label ID="FirstNameLabel" runat="server" Text='<%# Bind("FirstName") %>'></asp:Label><br />
		Patronymic:
		<asp:Label ID="PatronymicLabel" runat="server" Text='<%# Bind("Patronymic") %>'>
		</asp:Label><br />
		LastName:
		<asp:Label ID="LastNameLabel" runat="server" Text='<%# Bind("LastName") %>'></asp:Label><br />
		FirstNameEng:
		<asp:Label ID="FirstNameEngLabel" runat="server" Text='<%# Bind("FirstNameEng") %>'>
		</asp:Label><br />
		LastNameEng:
		<asp:Label ID="LastNameEngLabel" runat="server" Text='<%# Bind("LastNameEng") %>'>
		</asp:Label><br />
		Birthday:
		<asp:Label ID="BirthdayLabel" runat="server" Text='<%# Bind("Birthday") %>'></asp:Label><br />
		Sex:
		<asp:CheckBox ID="SexCheckBox" runat="server" Checked='<%# Bind("Sex") %>' Enabled="false" /><br />
		Organization:
		<asp:Label ID="OrganizationLabel" runat="server" Text='<%# Bind("Organization") %>'>
		</asp:Label><br />
		ChiefPhone:
		<asp:Label ID="ChiefPhoneLabel" runat="server" Text='<%# Bind("ChiefPhone") %>'>
		</asp:Label><br />
		ChiefPosition:
		<asp:Label ID="ChiefPositionLabel" runat="server" Text='<%# Bind("ChiefPosition") %>'>
		</asp:Label><br />
		Chief:
		<asp:Label ID="ChiefLabel" runat="server" Text='<%# Bind("Chief") %>'></asp:Label><br />
		JobPosition:
		<asp:Label ID="JobPositionLabel" runat="server" Text='<%# Bind("JobPosition") %>'>
		</asp:Label><br />
		OrgType:
		<asp:Label ID="OrgTypeLabel" runat="server" Text='<%# Bind("OrgType") %>'></asp:Label><br />
		Country:
		<asp:Label ID="CountryLabel" runat="server" Text='<%# Bind("Country") %>'></asp:Label><br />
		City:
		<asp:Label ID="CityLabel" runat="server" Text='<%# Bind("City") %>'></asp:Label><br />
		ZIP:
		<asp:Label ID="ZIPLabel" runat="server" Text='<%# Bind("ZIP") %>'></asp:Label><br />
		Address:
		<asp:Label ID="AddressLabel" runat="server" Text='<%# Bind("Address") %>'></asp:Label><br />
		Phone:
		<asp:Label ID="PhoneLabel" runat="server" Text='<%# Bind("Phone") %>'></asp:Label><br />
		Certificates:
		<asp:Label ID="CertificatesLabel" runat="server" Text='<%# Bind("Certificates") %>'>
		</asp:Label><br />
		Courses:
		<asp:Label ID="CoursesLabel" runat="server" Text='<%# Bind("Courses") %>'></asp:Label><br />
		Education:
		<asp:Label ID="EducationLabel" runat="server" Text='<%# Bind("Education") %>'></asp:Label><br />
		Email:
		<asp:Label ID="EmailLabel" runat="server" Text='<%# Bind("Email") %>'></asp:Label><br />
		Fax:
		<asp:Label ID="FaxLabel" runat="server" Text='<%# Bind("Fax") %>'></asp:Label><br />
		Comments:
		<asp:Label ID="CommentsLabel" runat="server" Text='<%# Bind("Comments") %>'></asp:Label><br />
		<%--Photo:
		<asp:Label ID="PhotoLabel" runat="server" Text='<%# Bind("Photo") %>'></asp:Label><br />
		--%><asp:LinkButton ID="EditButton" runat="server" CausesValidation="False" CommandName="Edit"
			Text="Edit">
		</asp:LinkButton>
		<asp:LinkButton ID="DeleteButton" runat="server" CausesValidation="False" CommandName="Delete"
			Text="Delete">
		</asp:LinkButton>
		<asp:LinkButton ID="NewButton" runat="server" CausesValidation="False" CommandName="New"
			Text="New">
		</asp:LinkButton>
	</ItemTemplate>
</asp:FormView>
<asp:SqlDataSource ID="dsUsers" runat="server" ConflictDetection="CompareAllValues"
	ConnectionString="<%$ ConnectionStrings:DceConnectionString %>"
	DeleteCommand="DELETE FROM [Students] WHERE [id] = @original_id AND [FirstName] = @original_FirstName AND [Patronymic] = @original_Patronymic AND [LastName] = @original_LastName AND [FirstNameEng] = @original_FirstNameEng AND [LastNameEng] = @original_LastNameEng AND [Birthday] = @original_Birthday AND [Sex] = @original_Sex AND [Organization] = @original_Organization AND [ChiefPhone] = @original_ChiefPhone AND [ChiefPosition] = @original_ChiefPosition AND [Chief] = @original_Chief AND [JobPosition] = @original_JobPosition AND [OrgType] = @original_OrgType AND [Country] = @original_Country AND [City] = @original_City AND [ZIP] = @original_ZIP AND [Address] = @original_Address AND [Phone] = @original_Phone AND [Certificates] = @original_Certificates AND [Courses] = @original_Courses AND [Education] = @original_Education AND [Email] = @original_Email AND [Fax] = @original_Fax AND [Comments] = @original_Comments AND [Photo] = @original_Photo"
	InsertCommand="INSERT INTO [Students] ([id], [FirstName], [Patronymic], [LastName], [FirstNameEng], [LastNameEng], [Birthday], [Sex], [Organization], [ChiefPhone], [ChiefPosition], [Chief], [JobPosition], [OrgType], [Country], [City], [ZIP], [Address], [Phone], [Certificates], [Courses], [Education], [Email], [Fax], [Comments], [Photo]) VALUES (@id, @FirstName, @Patronymic, @LastName, @FirstNameEng, @LastNameEng, @Birthday, @Sex, @Organization, @ChiefPhone, @ChiefPosition, @Chief, @JobPosition, @OrgType, @Country, @City, @ZIP, @Address, @Phone, @Certificates, @Courses, @Education, @Email, @Fax, @Comments, @Photo)"
	OldValuesParameterFormatString="original_{0}" OnSelecting="dsUsers_Selecting"
	SelectCommand="SELECT id, FirstName, Patronymic, LastName, FirstNameEng, LastNameEng, Birthday, Sex, Organization, ChiefPhone, ChiefPosition, Chief, JobPosition, OrgType, Country, City, ZIP, Address, Phone, Certificates, Courses, Education, Email, Fax, Comments, Photo FROM Students WHERE (id = @id)"
	UpdateCommand="UPDATE Students SET FirstName = @FirstName, Patronymic = @Patronymic, LastName = @LastName, FirstNameEng = @FirstNameEng, LastNameEng = @LastNameEng, Birthday = @Birthday, Sex = @Sex, Organization = @Organization, ChiefPhone = @ChiefPhone, ChiefPosition = @ChiefPosition, Chief = @Chief, JobPosition = @JobPosition, OrgType = @OrgType, Country = @Country, City = @City, ZIP = @ZIP, Address = @Address, Phone = @Phone, Certificates = @Certificates, Courses = @Courses, Education = @Education, Email = @Email, Fax = @Fax, Comments = @Comments, Photo = CONVERT(uniqueidentifier, @Photo) WHERE (id = @original_id)">
	<DeleteParameters>
		<asp:Parameter Name="original_id" Type="Object" />
		<asp:Parameter Name="original_FirstName" Type="String" />
		<asp:Parameter Name="original_Patronymic" Type="String" />
		<asp:Parameter Name="original_LastName" Type="String" />
		<asp:Parameter Name="original_FirstNameEng" Type="String" />
		<asp:Parameter Name="original_LastNameEng" Type="String" />
		<asp:Parameter Name="original_Birthday" Type="DateTime" />
		<asp:Parameter Name="original_Sex" Type="Boolean" />
		<asp:Parameter Name="original_Organization" Type="String" />
		<asp:Parameter Name="original_ChiefPhone" Type="String" />
		<asp:Parameter Name="original_ChiefPosition" Type="String" />
		<asp:Parameter Name="original_Chief" Type="String" />
		<asp:Parameter Name="original_JobPosition" Type="String" />
		<asp:Parameter Name="original_OrgType" Type="String" />
		<asp:Parameter Name="original_Country" Type="String" />
		<asp:Parameter Name="original_City" Type="String" />
		<asp:Parameter Name="original_ZIP" Type="String" />
		<asp:Parameter Name="original_Address" Type="String" />
		<asp:Parameter Name="original_Phone" Type="String" />
		<asp:Parameter Name="original_Certificates" Type="String" />
		<asp:Parameter Name="original_Courses" Type="String" />
		<asp:Parameter Name="original_Education" Type="String" />
		<asp:Parameter Name="original_Email" Type="String" />
		<asp:Parameter Name="original_Fax" Type="String" />
		<asp:Parameter Name="original_Comments" Type="String" />
		<asp:Parameter Name="original_Photo" Type="Object" />
	</DeleteParameters>
	<UpdateParameters>
		<asp:Parameter Name="Patronymic" Type="String" />
		<asp:Parameter Name="LastName" Type="String" />
		<asp:Parameter Name="FirstNameEng" Type="String" />
		<asp:Parameter Name="LastNameEng" Type="String" />
		<asp:Parameter Name="Birthday" Type="DateTime" />
		<asp:Parameter Name="Sex" Type="Boolean" />
		<asp:Parameter Name="Organization" Type="String" />
		<asp:Parameter Name="ChiefPhone" Type="String" />
		<asp:Parameter Name="ChiefPosition" Type="String" />
		<asp:Parameter Name="Chief" Type="String" />
		<asp:Parameter Name="JobPosition" Type="String" />
		<asp:Parameter Name="OrgType" Type="String" />
		<asp:Parameter Name="Country" Type="String" />
		<asp:Parameter Name="City" Type="String" />
		<asp:Parameter Name="ZIP" Type="String" />
		<asp:Parameter Name="Address" Type="String" />
		<asp:Parameter Name="Phone" Type="String" />
		<asp:Parameter Name="Certificates" Type="String" />
		<asp:Parameter Name="Courses" Type="String" />
		<asp:Parameter Name="Education" Type="String" />
		<asp:Parameter Name="Email" Type="String" />
		<asp:Parameter Name="Fax" Type="String" />
		<asp:Parameter Name="Comments" Type="String" />
		<asp:Parameter Name="Photo" Type="Object" />
		<asp:Parameter Name="original_id" Type="Object" />
	</UpdateParameters>
	<SelectParameters>
		<asp:Parameter Name="id" />
	</SelectParameters>
	<InsertParameters>
		<asp:Parameter Name="id" Type="Object" />
		<asp:Parameter Name="FirstName" Type="String" />
		<asp:Parameter Name="Patronymic" Type="String" />
		<asp:Parameter Name="LastName" Type="String" />
		<asp:Parameter Name="FirstNameEng" Type="String" />
		<asp:Parameter Name="LastNameEng" Type="String" />
		<asp:Parameter Name="Birthday" Type="DateTime" />
		<asp:Parameter Name="Sex" Type="Boolean" />
		<asp:Parameter Name="Organization" Type="String" />
		<asp:Parameter Name="ChiefPhone" Type="String" />
		<asp:Parameter Name="ChiefPosition" Type="String" />
		<asp:Parameter Name="Chief" Type="String" />
		<asp:Parameter Name="JobPosition" Type="String" />
		<asp:Parameter Name="OrgType" Type="String" />
		<asp:Parameter Name="Country" Type="String" />
		<asp:Parameter Name="City" Type="String" />
		<asp:Parameter Name="ZIP" Type="String" />
		<asp:Parameter Name="Address" Type="String" />
		<asp:Parameter Name="Phone" Type="String" />
		<asp:Parameter Name="Certificates" Type="String" />
		<asp:Parameter Name="Courses" Type="String" />
		<asp:Parameter Name="Education" Type="String" />
		<asp:Parameter Name="Email" Type="String" />
		<asp:Parameter Name="Fax" Type="String" />
		<asp:Parameter Name="Comments" Type="String" />
		<asp:Parameter Name="Photo" Type="Object" />
	</InsertParameters>
</asp:SqlDataSource>

<asp:XmlDataSource
		ID="xdsCountries"
		runat="server"
		DataFile="~/Lang/EN/xml/Countries.xml">
	<Transform>
		<?xml version="1.0" encoding="utf-8"?>

		<xsl:stylesheet version="1.0"
			xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

		<xsl:template match="Countries">
			<Countries>
				<xsl:apply-templates />
			</Countries>
		</xsl:template>

			<xsl:template match="option">
				<option value="{@value}"
						text="{text()}" />
			</xsl:template>

		</xsl:stylesheet> 
	</Transform>
</asp:XmlDataSource>
