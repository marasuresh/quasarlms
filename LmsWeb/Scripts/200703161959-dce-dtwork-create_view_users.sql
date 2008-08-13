/****** Object:  View [dbo].[Users]    Script Date: 03/15/2007 19:09:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[Users]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [dbo].[Users]
AS
SELECT     id, FirstName, Patronymic, LastName, FirstNameEng, LastNameEng, Birthday, Sex, Organization, OrgType, JobPosition, Chief, ChiefPosition, 
                      ChiefPhone, Country, City, ZIP, Address, Phone, Fax, Email, Education, Courses, Certificates, Comments, Login, Password, Status, MediaLibrary, 
                      LastLogin, Photo, TotalLogins, useCDLib, cdPath, PasswordHash, PasswordHashSalt, CreateDate, LastModifyDate, 
                      ''--------------------------------------------------------------------------------------------------------'' AS Rights, dbo.dcetools_Fn_Util_CombineFullName(FirstName, Patronymic, 
                      LastName) AS FullName
FROM         dbo.Students
'
GO