<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Users.aspx.cs" Inherits="Upgrade_Users" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
		<asp:Literal ID="Literal1" runat="server" Visible="False"></asp:Literal>
		<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="id"
			DataSourceID="SqlDataSource1" EmptyDataText="There are no data records to display."
			OnRowCommand="GridView1_RowCommand">
			<Columns>
				<asp:ButtonField ButtonType="Button" CommandName="Import" Text="Import" />
				<asp:BoundField DataField="FirstName" HeaderText="FirstName" SortExpression="FirstName" />
				<asp:BoundField DataField="Patronymic" HeaderText="Patronymic" SortExpression="Patronymic" />
				<asp:BoundField DataField="LastName" HeaderText="LastName" SortExpression="LastName" />
				<asp:BoundField DataField="FirstNameEng" HeaderText="FirstNameEng" SortExpression="FirstNameEng" />
				<asp:BoundField DataField="LastNameEng" HeaderText="LastNameEng" SortExpression="LastNameEng" />
				<asp:BoundField DataField="Birthday" HeaderText="Birthday" SortExpression="Birthday" />
				<asp:BoundField DataField="Country" HeaderText="Country" SortExpression="Country" />
				<asp:BoundField DataField="City" HeaderText="City" SortExpression="City" />
				<asp:BoundField DataField="ZIP" HeaderText="ZIP" SortExpression="ZIP" />
				<asp:BoundField DataField="Address" HeaderText="Address" SortExpression="Address" />
				<asp:BoundField DataField="Phone" HeaderText="Phone" SortExpression="Phone" />
				<asp:BoundField DataField="Fax" HeaderText="Fax" SortExpression="Fax" />
				<asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
				<asp:BoundField DataField="Comments" HeaderText="Comments" SortExpression="Comments" />
				<asp:BoundField DataField="Login" HeaderText="Login" SortExpression="Login" />
				<asp:BoundField DataField="Password" HeaderText="Password" SortExpression="Password" />
				<asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />
				<asp:BoundField DataField="MediaLibrary" HeaderText="MediaLibrary" SortExpression="MediaLibrary" />
				<asp:BoundField DataField="LastLogin" HeaderText="LastLogin" SortExpression="LastLogin" />
				<asp:BoundField DataField="Photo" HeaderText="Photo" SortExpression="Photo" />
				<asp:BoundField DataField="TotalLogins" HeaderText="TotalLogins" SortExpression="TotalLogins" />
				<asp:CheckBoxField DataField="useCDLib" HeaderText="useCDLib" SortExpression="useCDLib" />
				<asp:BoundField DataField="cdPath" HeaderText="cdPath" SortExpression="cdPath" />
				<asp:BoundField DataField="PasswordHash" HeaderText="PasswordHash" SortExpression="PasswordHash" />
				<asp:BoundField DataField="PasswordHashSalt" HeaderText="PasswordHashSalt" SortExpression="PasswordHashSalt" />
				<asp:BoundField DataField="CreateDate" HeaderText="CreateDate" SortExpression="CreateDate" />
				<asp:BoundField DataField="LastModifyDate" HeaderText="LastModifyDate" SortExpression="LastModifyDate" />
			</Columns>
		</asp:GridView>
		<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DceConnectionString %>"
			DeleteCommand="DELETE FROM [Students] WHERE [id] = @id" InsertCommand="INSERT INTO [Students] ([id], [FirstName], [Patronymic], [LastName], [FirstNameEng], [LastNameEng], [Birthday], [Sex], [Organization], [OrgType], [JobPosition], [Chief], [ChiefPosition], [ChiefPhone], [Country], [City], [ZIP], [Address], [Phone], [Fax], [Email], [Education], [Courses], [Certificates], [Comments], [Login], [Password], [Status], [MediaLibrary], [LastLogin], [Photo], [TotalLogins], [useCDLib], [cdPath], [PasswordHash], [PasswordHashSalt], [CreateDate], [LastModifyDate]) VALUES (@id, @FirstName, @Patronymic, @LastName, @FirstNameEng, @LastNameEng, @Birthday, @Sex, @Organization, @OrgType, @JobPosition, @Chief, @ChiefPosition, @ChiefPhone, @Country, @City, @ZIP, @Address, @Phone, @Fax, @Email, @Education, @Courses, @Certificates, @Comments, @Login, @Password, @Status, @MediaLibrary, @LastLogin, @Photo, @TotalLogins, @useCDLib, @cdPath, @PasswordHash, @PasswordHashSalt, @CreateDate, @LastModifyDate)"
			ProviderName="<%$ ConnectionStrings:DceConnectionString.ProviderName %>" SelectCommand="SELECT [id], [FirstName], [Patronymic], [LastName], [FirstNameEng], [LastNameEng], [Birthday], [Sex], [Organization], [OrgType], [JobPosition], [Chief], [ChiefPosition], [ChiefPhone], [Country], [City], [ZIP], [Address], [Phone], [Fax], [Email], [Education], [Courses], [Certificates], [Comments], [Login], [Password], [Status], [MediaLibrary], [LastLogin], [Photo], [TotalLogins], [useCDLib], [cdPath], [PasswordHash], [PasswordHashSalt], [CreateDate], [LastModifyDate] FROM [Students]"
			UpdateCommand="UPDATE [Students] SET [FirstName] = @FirstName, [Patronymic] = @Patronymic, [LastName] = @LastName, [FirstNameEng] = @FirstNameEng, [LastNameEng] = @LastNameEng, [Birthday] = @Birthday, [Sex] = @Sex, [Organization] = @Organization, [OrgType] = @OrgType, [JobPosition] = @JobPosition, [Chief] = @Chief, [ChiefPosition] = @ChiefPosition, [ChiefPhone] = @ChiefPhone, [Country] = @Country, [City] = @City, [ZIP] = @ZIP, [Address] = @Address, [Phone] = @Phone, [Fax] = @Fax, [Email] = @Email, [Education] = @Education, [Courses] = @Courses, [Certificates] = @Certificates, [Comments] = @Comments, [Login] = @Login, [Password] = @Password, [Status] = @Status, [MediaLibrary] = @MediaLibrary, [LastLogin] = @LastLogin, [Photo] = @Photo, [TotalLogins] = @TotalLogins, [useCDLib] = @useCDLib, [cdPath] = @cdPath, [PasswordHash] = @PasswordHash, [PasswordHashSalt] = @PasswordHashSalt, [CreateDate] = @CreateDate, [LastModifyDate] = @LastModifyDate WHERE [id] = @id">
			<DeleteParameters>
				<asp:Parameter Name="id" Type="Object" />
			</DeleteParameters>
			<UpdateParameters>
				<asp:Parameter Name="FirstName" Type="String" />
				<asp:Parameter Name="Patronymic" Type="String" />
				<asp:Parameter Name="LastName" Type="String" />
				<asp:Parameter Name="FirstNameEng" Type="String" />
				<asp:Parameter Name="LastNameEng" Type="String" />
				<asp:Parameter Name="Birthday" Type="DateTime" />
				<asp:Parameter Name="Sex" Type="Boolean" />
				<asp:Parameter Name="Organization" Type="String" />
				<asp:Parameter Name="OrgType" Type="String" />
				<asp:Parameter Name="JobPosition" Type="String" />
				<asp:Parameter Name="Chief" Type="String" />
				<asp:Parameter Name="ChiefPosition" Type="String" />
				<asp:Parameter Name="ChiefPhone" Type="String" />
				<asp:Parameter Name="Country" Type="String" />
				<asp:Parameter Name="City" Type="String" />
				<asp:Parameter Name="ZIP" Type="String" />
				<asp:Parameter Name="Address" Type="String" />
				<asp:Parameter Name="Phone" Type="String" />
				<asp:Parameter Name="Fax" Type="String" />
				<asp:Parameter Name="Email" Type="String" />
				<asp:Parameter Name="Education" Type="String" />
				<asp:Parameter Name="Courses" Type="String" />
				<asp:Parameter Name="Certificates" Type="String" />
				<asp:Parameter Name="Comments" Type="String" />
				<asp:Parameter Name="Login" Type="String" />
				<asp:Parameter Name="Password" Type="String" />
				<asp:Parameter Name="Status" Type="Int32" />
				<asp:Parameter Name="MediaLibrary" Type="String" />
				<asp:Parameter Name="LastLogin" Type="DateTime" />
				<asp:Parameter Name="Photo" Type="Object" />
				<asp:Parameter Name="TotalLogins" Type="Int32" />
				<asp:Parameter Name="useCDLib" Type="Boolean" />
				<asp:Parameter Name="cdPath" Type="String" />
				<asp:Parameter Name="PasswordHash" Type="Object" />
				<asp:Parameter Name="PasswordHashSalt" Type="Object" />
				<asp:Parameter Name="CreateDate" Type="DateTime" />
				<asp:Parameter Name="LastModifyDate" Type="DateTime" />
				<asp:Parameter Name="id" Type="Object" />
			</UpdateParameters>
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
				<asp:Parameter Name="OrgType" Type="String" />
				<asp:Parameter Name="JobPosition" Type="String" />
				<asp:Parameter Name="Chief" Type="String" />
				<asp:Parameter Name="ChiefPosition" Type="String" />
				<asp:Parameter Name="ChiefPhone" Type="String" />
				<asp:Parameter Name="Country" Type="String" />
				<asp:Parameter Name="City" Type="String" />
				<asp:Parameter Name="ZIP" Type="String" />
				<asp:Parameter Name="Address" Type="String" />
				<asp:Parameter Name="Phone" Type="String" />
				<asp:Parameter Name="Fax" Type="String" />
				<asp:Parameter Name="Email" Type="String" />
				<asp:Parameter Name="Education" Type="String" />
				<asp:Parameter Name="Courses" Type="String" />
				<asp:Parameter Name="Certificates" Type="String" />
				<asp:Parameter Name="Comments" Type="String" />
				<asp:Parameter Name="Login" Type="String" />
				<asp:Parameter Name="Password" Type="String" />
				<asp:Parameter Name="Status" Type="Int32" />
				<asp:Parameter Name="MediaLibrary" Type="String" />
				<asp:Parameter Name="LastLogin" Type="DateTime" />
				<asp:Parameter Name="Photo" Type="Object" />
				<asp:Parameter Name="TotalLogins" Type="Int32" />
				<asp:Parameter Name="useCDLib" Type="Boolean" />
				<asp:Parameter Name="cdPath" Type="String" />
				<asp:Parameter Name="PasswordHash" Type="Object" />
				<asp:Parameter Name="PasswordHashSalt" Type="Object" />
				<asp:Parameter Name="CreateDate" Type="DateTime" />
				<asp:Parameter Name="LastModifyDate" Type="DateTime" />
			</InsertParameters>
		</asp:SqlDataSource>
    
    </div>
    </form>
</body>
</html>
