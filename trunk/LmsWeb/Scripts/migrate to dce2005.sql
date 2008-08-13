/****** Object:  Role [dceUser]    Script Date: 03/16/2007 20:07:02 ******/
IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'dceUser')
BEGIN
IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'dceUser' AND type = 'R')
CREATE ROLE [dceUser]

END
GO
/****** Object:  Role [DevBusinessService]    Script Date: 03/16/2007 20:07:02 ******/
IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'DevBusinessService')
BEGIN
IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'DevBusinessService' AND type = 'R')
CREATE ROLE [DevBusinessService]

END
GO
/****** Object:  Schema [dceUser]    Script Date: 03/16/2007 20:07:03 ******/
IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = N'dceUser')
EXEC sys.sp_executesql N'CREATE SCHEMA [dceUser] AUTHORIZATION [dceUser]'
GO
/****** Object:  Table [dbo].[Tests]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tests]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Tests](
	[id] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[Type] [int] NOT NULL,
	[Parent] [uniqueidentifier] NULL,
	[Duration] [int] NOT NULL,
	[Points] [int] NOT NULL,
	[Show] [bit] NOT NULL,
	[Split] [bit] NOT NULL,
	[AutoFinish] [bit] NOT NULL,
	[DefLanguage] [int] NULL,
	[CanSwitchLang] [bit] NOT NULL,
	[Hints] [int] NOT NULL,
	[InternalName] [ntext] COLLATE Cyrillic_General_CI_AS NULL,
	[ShowThemes] [bit] NOT NULL,
 CONSTRAINT [PK_Tests] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (IGNORE_DUP_KEY = OFF)
)
END
GO
/****** Object:  Table [dbo].[Students]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (
	SELECT * FROM [sys].[columns]
	WHERE [object_id] = OBJECT_ID(N'[dbo].[Students]')
		AND [name] IN (N'PasswordHash', N'PasswordHashSalt', N'CreateDate', N'LastModifyDate'))
BEGIN
	ALTER TABLE [dbo].[Students] ADD
		[PasswordHash] [uniqueidentifier] NULL,
		[PasswordHashSalt] [uniqueidentifier] NULL,
		[CreateDate] [datetime] NOT NULL DEFAULT GETDATE(),
		[LastModifyDate] [datetime] NOT NULL DEFAULT GETDATE();
END
GO
/****** Object:  UserDefinedFunction [dbo].[dcetools_Fn_Util_CombineFullName]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Fn_Util_CombineFullName]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE FUNCTION [dbo].[dcetools_Fn_Util_CombineFullName]
(
    @firstName nvarchar(255),
    @patronymicName nvarchar(255), 
    @lastName nvarchar(255)
)
RETURNS nvarchar(255)
AS

BEGIN
	RETURN
	    CASE
	        WHEN
	                (@firstName IS NULL)
	            AND
	                (@patronymicName IS NULL)
	            AND
	                (@lastName IS NULL)
	        THEN
	            NULL
	           
	        ELSE 
                RTrim(
                    RTrim(
                        LTrim(RTrim(ISNULL(@firstName,''''))) + '' '' + LTrim(ISNULL(@patronymicName,''''))
                                ) + '' '' +LTrim(ISNULL(@lastName,'''')))
        END
END' 
END
GO
/****** Object:  View [dbo].[Users]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Users]') AND type in (N'U'))
BEGIN
	EXEC sp_rename @objname = '[dbo].[Users]', @newname = 'legacy_Users';
	
	INSERT INTO [dbo].[Students](
			id,
			FirstName,
			LastName,
			Patronymic,
			FirstNameEng,
			LastNameEng,
			Birthday,
			Sex,
			Organization,
			OrgType,
			JobPosition,
			Chief,
			ChiefPosition, 
			ChiefPhone,
			Country,
			City,
			ZIP,
			Address,
			Phone,
			Fax,
			Email,
			Education,
			Courses,
			Certificates,
			Comments,
			[Login],
			Password,
			Status,
			MediaLibrary, 
			LastLogin,
			Photo,
			TotalLogins,
			useCDLib,
			cdPath,
			PasswordHash,
			PasswordHashSalt,
			CreateDate,
			LastModifyDate)
	SELECT	id,
			FirstName,
			LastName,
			Patronymic,
			FirstNameEng,
			LastNameEng,
			Birthday,
			Sex,
			/*Organization*/NULL,
			/*OrgType*/NULL,
			JobPosition,
			/*Chief*/NULL,
			/*ChiefPosition*/NULL,
			/*ChiefPhone*/NULL,
			/*Country*/NULL,
			/*City*/NULL,
			/*ZIP*/NULL,
			Address,
			Phone,
			/*Fax*/NULL,
			Email,
			Education,
			Courses,
			Certificates,
			Comments + ' Rights: ' + Rights,
			[Login],
			Password,
			/*Status*/NULL,
			/*MediaLibrary*/NULL, 
			/*LastLogin*/NULL,
			Photo,
			/*TotalLogins*/0,
			/*useCDLib*/0,
			/*cdPath*/NULL,
			/*PasswordHash*/NULL,
			/*PasswordHashSalt*/NULL,
			/*CreateDate*/GETDATE(),
			/*LastModifyDate*/GETDATE()
	FROM         [dbo].[legacy_Users];
	
	DROP TABLE [dbo].[legacy_Users];
END
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[Users]'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE VIEW [dbo].[Users]
	AS
	SELECT     id, FirstName, Patronymic, LastName, FirstNameEng, LastNameEng, Birthday, Sex, Organization, OrgType, JobPosition, Chief, ChiefPosition, 
						  ChiefPhone, Country, City, ZIP, Address, Phone, Fax, Email, Education, Courses, Certificates, Comments, Login, Password, Status, MediaLibrary, 
						  LastLogin, Photo, TotalLogins, useCDLib, cdPath, PasswordHash, PasswordHashSalt, CreateDate, LastModifyDate, 
						  ''--------------------------------------------------------------------------------------------------------'' AS Rights, dbo.dcetools_Fn_Util_CombineFullName(FirstName, Patronymic, 
						  LastName) AS FullName
	FROM         dbo.Students
	'
END
GO
/****** Object:  UserDefinedFunction [dbo].[dcelegacy_Fn_Trainings_GetStudentCount]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcelegacy_Fn_Trainings_GetStudentCount]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE FUNCTION [dbo].[dcelegacy_Fn_Trainings_GetStudentCount]
(
	@trainingID uniqueidentifier
)
RETURNS int
AS

BEGIN
    
    RETURN
        (SELECT COUNT([ID])
        FROM dbo.dcetools_Fn_Trainings_Students_GetIDList(@trainingID))

END' 
END
GO
/****** Object:  Table [dbo].[Themes]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Themes]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Themes](
	[id] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[Type] [tinyint] NOT NULL,
	[Name] [uniqueidentifier] NOT NULL,
	[Parent] [uniqueidentifier] NOT NULL,
	[Duration] [int] NOT NULL,
	[Mandatory] [bit] NOT NULL,
	[Content] [uniqueidentifier] NOT NULL,
	[TOrder] [int] NOT NULL,
	[Practice] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Themes] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (IGNORE_DUP_KEY = OFF)
)
END
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Themes]') AND name = N'IX_Themes_Parent')
CREATE NONCLUSTERED INDEX [IX_Themes_Parent] ON [dbo].[Themes] 
(
	[Parent] ASC
)WITH (IGNORE_DUP_KEY = OFF)
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Themes]') AND name = N'IX_Themes_Practice')
CREATE NONCLUSTERED INDEX [IX_Themes_Practice] ON [dbo].[Themes] 
(
	[Practice] ASC
)WITH (IGNORE_DUP_KEY = OFF)
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Themes]') AND name = N'IX_Themes_TOrder')
CREATE NONCLUSTERED INDEX [IX_Themes_TOrder] ON [dbo].[Themes] 
(
	[TOrder] ASC
)WITH (IGNORE_DUP_KEY = OFF)
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Themes]') AND name = N'IX_Themes_Type')
CREATE NONCLUSTERED INDEX [IX_Themes_Type] ON [dbo].[Themes] 
(
	[Type] ASC
)WITH (IGNORE_DUP_KEY = OFF)
GO
/****** Object:  Table [dbo].[Tracks]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tracks]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Tracks](
	[id] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[Name] [uniqueidentifier] NOT NULL,
	[Description] [uniqueidentifier] NOT NULL,
	[Students] [uniqueidentifier] NOT NULL,
	[Trainings] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Tracks] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (IGNORE_DUP_KEY = OFF)
)
END
GO
/****** Object:  Table [dbo].[Trainings]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Trainings]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Trainings](
	[id] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[Code] [nvarchar](50) COLLATE Cyrillic_General_CI_AS NULL,
	[Name] [uniqueidentifier] NOT NULL,
	[Comment] [uniqueidentifier] NOT NULL,
	[Course] [uniqueidentifier] NOT NULL,
	[isPublic] [bit] NOT NULL,
	[isActive] [bit] NOT NULL,
	[Instructors] [uniqueidentifier] NOT NULL,
	[Curators] [uniqueidentifier] NOT NULL,
	[Students] [uniqueidentifier] NOT NULL,
	[TimeStrict] [bit] NOT NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[TestOnly] [bit] NOT NULL,
	[Expires] [bit] NOT NULL,
 CONSTRAINT [PK_Trainings] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (IGNORE_DUP_KEY = OFF)
)
END
GO
/****** Object:  Table [dbo].[VTerms]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[VTerms]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[VTerms](
	[id] [uniqueidentifier] NOT NULL,
	[Name] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[Text] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_VTerms] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (IGNORE_DUP_KEY = OFF)
)
END
GO
/****** Object:  Table [dbo].[Vocabulary]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Vocabulary]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Vocabulary](
	[id] [uniqueidentifier] NOT NULL,
	[Course] [uniqueidentifier] NOT NULL,
	[Term] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Vocabulary] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (IGNORE_DUP_KEY = OFF)
)
END
GO
/****** Object:  Table [dbo].[UserProfileData]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserProfileData]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[UserProfileData](
	[UserID] [uniqueidentifier] NOT NULL,
	[AnonymousID] [int] NULL,
	[WebProfileXmlData] [xml] NOT NULL
)
END
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[UserProfileData]') AND name = N'IX_UserProfileData')
CREATE UNIQUE NONCLUSTERED INDEX [IX_UserProfileData] ON [dbo].[UserProfileData] 
(
	[UserID] ASC
)WITH (IGNORE_DUP_KEY = OFF)
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[UserProfileData]') AND name = N'IX_UserProfileData_1')
CREATE NONCLUSTERED INDEX [IX_UserProfileData_1] ON [dbo].[UserProfileData] 
(
	[AnonymousID] ASC
)WITH (IGNORE_DUP_KEY = OFF)
GO
/****** Object:  StoredProcedure [dbo].[dcetools_Students_GetAllStudents_WithNULL]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Students_GetAllStudents_WithNULL]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dcetools_Students_GetAllStudents_WithNULL]
(
	@homeRegion uniqueidentifier
)
AS

BEGIN
	SET NOCOUNT ON;
	
	INSERT INTO #Students
    EXEC dbo.dcetools_Students_GetAllStudents @homeRegion
        
    INSERT INTO #Students( [ID], FullName )
    VALUES(NULL, ''(none)'')
    
    SELECT *
    FROM #Students
    ORDER BY FullName
	
	RETURN
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[MakeContentCopy]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MakeContentCopy]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[MakeContentCopy] 
	@dest uniqueidentifier,      -- dest     eid
	@source uniqueidentifier   -- source eid
as

-- На момент выполнения процедуры должны быть 
-- созданы соответствующие записи в таблице Entities

declare Contents_Cursor cursor for 
select id from dbo.Content where eid = @source 

open Contents_Cursor

declare @oldContentId uniqueidentifier

fetch next from Contents_Cursor into @oldContentId
while (@@FETCH_STATUS <> -1)
begin
	if (@@FETCH_STATUS <> -2)
	begin
		insert into Content select NEWID(), @dest, Type, Lang, DataStr, 
		Data, TData, ParentEid, COrder from Content where id = @oldContentId
	end

	fetch next from Contents_Cursor into @oldContentId
end

close Contents_Cursor
deallocate Contents_Cursor
' 
END
GO
/****** Object:  Table [dbo].[UserRoles]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserRoles]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[UserRoles](
	[UserID] [uniqueidentifier] NOT NULL,
	[Role] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK__UserRoles__467D75B8] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (IGNORE_DUP_KEY = OFF)
)
END
GO
/****** Object:  UserDefinedFunction [dbo].[IsChildTheme]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[IsChildTheme]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'


/* is @child theme a child of course or theme @parent */

CREATE FUNCTION [dbo].[IsChildTheme] (@parent uniqueidentifier, @child uniqueidentifier )  

RETURNS bit AS  

BEGIN 

 

if ( select id from themes where parent=@parent and ( id = @child or dbo.IsChildTheme(id, @child) = 1) ) is not null

      return 1

return 0

END

 

' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[AllSubThemes]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AllSubThemes]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'
CREATE FUNCTION [dbo].[AllSubThemes] (@themeid uniqueidentifier, @lang int)  
RETURNS @members TABLE (parentid uniqueidentifier , id uniqueidentifier,  name nvarchar(255) ) AS  
BEGIN 

/* getting Subthemes */
DECLARE cur cursor 
FOR SELECT t.id, dbo.GetStrContent(t.Name,@lang) from Themes t 
	WHERE t.Parent = @themeid ORDER BY t.TOrder
OPEN cur
DECLARE @subthemeid uniqueidentifier
DECLARE @themename nvarchar(255)

FETCH NEXT FROM cur INTO @subthemeid ,@themename 

WHILE @@FETCH_STATUS <> -1
BEGIN
	IF (@@FETCH_STATUS = 0)
	BEGIN
		INSERT @members VALUES (@themeid ,@subthemeid ,@themename)
		INSERT @members SELECT *  from AllSubThemes(@subthemeid,@lang)
	END
	FETCH NEXT FROM cur INTO @subthemeid ,@themename 
END
CLOSE cur
DEALLOCATE cur

return 
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[dcetools_Helpers_CheckObjectIDNull]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Helpers_CheckObjectIDNull]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dcetools_Helpers_CheckObjectIDNull]
(
	@objectID uniqueidentifier,
	@parameterName nvarchar(255)
)
AS

BEGIN
	SET NOCOUNT ON;

    IF @objectID IS NULL
    BEGIN
    
        DECLARE
            @errorMessage nvarchar(255)
            
        SELECT
            @errorMessage = ''Parameter ''+ISNULL(@parameterName,'''') +'' is NULL.''

        RAISERROR (@errorMessage, 16, 1)
        ROLLBACK TRAN
                
    END  
	
	RETURN
END' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[GetStrContentOrderer]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetStrContentOrderer]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'
CREATE FUNCTION [dbo].[GetStrContentOrderer] (@id uniqueidentifier, @lang int, @order int)  
RETURNS nvarchar(255) AS  
BEGIN 
	return  (SELECT top 1 DataStr from dbo.Content where eid=@id and Lang=@lang and COrder=@order)
END



' 
END
GO
/****** Object:  Table [dbo].[TestWriteTable]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TestWriteTable]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[TestWriteTable](
	[Dummy] [nvarchar](255) COLLATE Cyrillic_General_CI_AS NULL
)
END
GO
/****** Object:  UserDefinedFunction [dbo].[IdIsNotNull]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[IdIsNotNull]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'
CREATE FUNCTION [dbo].[IdIsNotNull] (@id uniqueidentifier)  
RETURNS bit AS  
BEGIN 
	IF (@id = NULL)
		return 0
	return 1
END


' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[IsEmptyString]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[IsEmptyString]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'
CREATE FUNCTION [dbo].[IsEmptyString] (@sourceStr nvarchar(1024), @defStr nvarchar(1024))  
RETURNS nvarchar(255) AS  
BEGIN 
if @sourceStr='''' or @sourceStr=null
	return  @defStr
return  @sourceStr
END



' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[isIdEqual]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[isIdEqual]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'
CREATE FUNCTION [dbo].[isIdEqual] (@one uniqueidentifier, @two  uniqueidentifier)  
RETURNS bit AS  
BEGIN 
if  (@one = @two)
	return 1
return 0
END

' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[dcetools_Fn_Regions_RegionCanReadByHome]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Fn_Regions_RegionCanReadByHome]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE FUNCTION [dbo].[dcetools_Fn_Regions_RegionCanReadByHome]
(
	@regionID uniqueidentifier,
	@homeRegion uniqueidentifier
)
RETURNS bit
AS

BEGIN
	RETURN
	    (SELECT
	        CASE
	            WHEN @regionID IS NULL
	            THEN 1
	            
	            WHEN @homeRegion IS NULL
	            THEN 1
	            
	            WHEN @regionID=@homeRegion
	            THEN 1
	            
	            ELSE 0
	        END)
END' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[dcetools_Fn_Regions_RegionCanWriteByHome]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Fn_Regions_RegionCanWriteByHome]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE FUNCTION [dbo].[dcetools_Fn_Regions_RegionCanWriteByHome]
(
	@regionID uniqueidentifier,
	@homeRegion uniqueidentifier
)
RETURNS bit
AS

BEGIN
	RETURN
	    (SELECT
	        CASE
	        
	            -- Cannot change Global region from non-global user
	            --WHEN @regionID IS NULL
	            --THEN 1
	            
	            WHEN @homeRegion IS NULL
	            THEN 1
	            
	            WHEN @regionID=@homeRegion
	            THEN 1
	            
	            ELSE 0
	        END)
END' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[GetMaxCOrder]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetMaxCOrder]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'
CREATE FUNCTION [dbo].[GetMaxCOrder] ()  
RETURNS int AS  
BEGIN 
	return (SELECT 1+ISNULL(MAX(COrder),0) from dbo.Content)
END



' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[UserName]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserName]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'
/* return full user name. if @eng==1 return on English */
CREATE FUNCTION [dbo].[UserName] (@id uniqueidentifier, @eng bit)  
RETURNS nvarchar(255) AS  
BEGIN 

declare @Name nvarchar(255)

declare @FirstName nvarchar(255)
declare @LastName nvarchar(255)
declare @Patronymic nvarchar(255)
declare @FirstNameEng nvarchar(255)
declare @LastNameEng nvarchar(255)

select @FirstName=ISNULL(FirstName,''''), @LastName=ISNULL(LastName,''''), @Patronymic=ISNULL(Patronymic,''''),
	@FirstNameEng=ISNULL(FirstNameEng,''''), @LastNameEng=ISNULL(LastNameEng,'''') from dbo.Users where id=@id

set @Name = @FirstNameEng + '' '' +  @LastNameEng
if @Name != null and @eng =1
	return LTRIM(@Name)
else
	set @Name = @LastName + '' '' + @FirstName + '' '' + @Patronymic

return LTRIM(@Name)
END





' 
END
GO
/****** Object:  Table [dbo].[Regions]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Regions]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Regions](
	[ID] [uniqueidentifier] NULL,
	[Name] [nvarchar](250) COLLATE Cyrillic_General_CI_AS NOT NULL,
	[CodesCommaSeparated] [nvarchar](250) COLLATE Cyrillic_General_CI_AS NULL,
 CONSTRAINT [IX_Regions] UNIQUE NONCLUSTERED 
(
	[ID] ASC
)WITH (IGNORE_DUP_KEY = OFF)
)
END
GO
INSERT [dbo].[Regions] ([ID], [Name], [CodesCommaSeparated]) VALUES (N'bbbbbbbb-d9b7-42e1-b6a1-ffffb0e29b01', N'Київська РД', N'kv,kcf')
INSERT [dbo].[Regions] ([ID], [Name], [CodesCommaSeparated]) VALUES (N'bbbbbbbb-d9b7-42e1-b6a1-ffffb0e29b02', N'Одеська ОД', N'od')
INSERT [dbo].[Regions] ([ID], [Name], [CodesCommaSeparated]) VALUES (N'bbbbbbbb-d9b7-42e1-b6a1-ffffb0e29b03', N'Кримська ОД', N'cr')
INSERT [dbo].[Regions] ([ID], [Name], [CodesCommaSeparated]) VALUES (N'bbbbbbbb-d9b7-42e1-b6a1-ffffb0e29b04', N'Вінницька ОД', N'vn')
INSERT [dbo].[Regions] ([ID], [Name], [CodesCommaSeparated]) VALUES (N'bbbbbbbb-d9b7-42e1-b6a1-ffffb0e29b05', N'Волиньска ОД', N'lt')
INSERT [dbo].[Regions] ([ID], [Name], [CodesCommaSeparated]) VALUES (N'bbbbbbbb-d9b7-42e1-b6a1-ffffb0e29b06', N'Дніпропетровська ОД', N'dp')
INSERT [dbo].[Regions] ([ID], [Name], [CodesCommaSeparated]) VALUES (N'bbbbbbbb-d9b7-42e1-b6a1-ffffb0e29b07', N'Донецька ОД', N'dn')
INSERT [dbo].[Regions] ([ID], [Name], [CodesCommaSeparated]) VALUES (N'bbbbbbbb-d9b7-42e1-b6a1-ffffb0e29b08', N'Житомирська ОД', N'zt')
INSERT [dbo].[Regions] ([ID], [Name], [CodesCommaSeparated]) VALUES (N'bbbbbbbb-d9b7-42e1-b6a1-ffffb0e29b09', N'Закарпатська ОД', N'uz')
INSERT [dbo].[Regions] ([ID], [Name], [CodesCommaSeparated]) VALUES (N'bbbbbbbb-d9b7-42e1-b6a1-ffffb0e29b10', N'Запоріжська ОД', N'zp')
INSERT [dbo].[Regions] ([ID], [Name], [CodesCommaSeparated]) VALUES (N'bbbbbbbb-d9b7-42e1-b6a1-ffffb0e29b11', N'Івано-Франковська ОД', N'if')
INSERT [dbo].[Regions] ([ID], [Name], [CodesCommaSeparated]) VALUES (N'bbbbbbbb-d9b7-42e1-b6a1-ffffb0e29b12', N'Кировоградська ОД', N'kr')
INSERT [dbo].[Regions] ([ID], [Name], [CodesCommaSeparated]) VALUES (N'bbbbbbbb-d9b7-42e1-b6a1-ffffb0e29b13', N'Луганська ОД', N'lg')
INSERT [dbo].[Regions] ([ID], [Name], [CodesCommaSeparated]) VALUES (N'bbbbbbbb-d9b7-42e1-b6a1-ffffb0e29b14', N'Львівська ОД', N'lv')
INSERT [dbo].[Regions] ([ID], [Name], [CodesCommaSeparated]) VALUES (N'bbbbbbbb-d9b7-42e1-b6a1-ffffb0e29b15', N'Миколаївська ОД', N'mk')
INSERT [dbo].[Regions] ([ID], [Name], [CodesCommaSeparated]) VALUES (N'bbbbbbbb-d9b7-42e1-b6a1-ffffb0e29b16', N'Полтавська ОД', N'pl')
INSERT [dbo].[Regions] ([ID], [Name], [CodesCommaSeparated]) VALUES (N'bbbbbbbb-d9b7-42e1-b6a1-ffffb0e29b17', N'Ровненська ОД', N'rv')
INSERT [dbo].[Regions] ([ID], [Name], [CodesCommaSeparated]) VALUES (N'bbbbbbbb-d9b7-42e1-b6a1-ffffb0e29b18', N'Сумська ОД', N'sm')
INSERT [dbo].[Regions] ([ID], [Name], [CodesCommaSeparated]) VALUES (N'bbbbbbbb-d9b7-42e1-b6a1-ffffb0e29b19', N'Тернопільска ОД', N'tr')
INSERT [dbo].[Regions] ([ID], [Name], [CodesCommaSeparated]) VALUES (N'bbbbbbbb-d9b7-42e1-b6a1-ffffb0e29b20', N'Харьківська ОД', N'kh')
INSERT [dbo].[Regions] ([ID], [Name], [CodesCommaSeparated]) VALUES (N'bbbbbbbb-d9b7-42e1-b6a1-ffffb0e29b21', N'Херсонська ОД', N'ks')
INSERT [dbo].[Regions] ([ID], [Name], [CodesCommaSeparated]) VALUES (N'bbbbbbbb-d9b7-42e1-b6a1-ffffb0e29b22', N'Хмельницька ОД', N'km')
INSERT [dbo].[Regions] ([ID], [Name], [CodesCommaSeparated]) VALUES (N'bbbbbbbb-d9b7-42e1-b6a1-ffffb0e29b23', N'Черкасська ОД', N'ck')
INSERT [dbo].[Regions] ([ID], [Name], [CodesCommaSeparated]) VALUES (N'bbbbbbbb-d9b7-42e1-b6a1-ffffb0e29b24', N'Чернигівська ОД', N'cn')
INSERT [dbo].[Regions] ([ID], [Name], [CodesCommaSeparated]) VALUES (N'bbbbbbbb-d9b7-42e1-b6a1-ffffb0e29b25', N'Чернівецька ОД', N'cv')
INSERT [dbo].[Regions] ([ID], [Name], [CodesCommaSeparated]) VALUES (N'bbbbbbbb-d9b7-42e1-b6a1-ffffb0e29b26', N'Севастопольска ОФ', N'sb')
INSERT [dbo].[Regions] ([ID], [Name], [CodesCommaSeparated]) VALUES (NULL, N'По всієї Україні', NULL)
/****** Object:  UserDefinedFunction [dbo].[dcetools_Fn_Util_CombineFullName]    Script Date: 03/17/2007 20:01:32 ******/
SET ANSI_NULLS ON
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Roles]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Roles](
	[ID] [uniqueidentifier] NULL,
	[Name] [nvarchar](250) COLLATE Cyrillic_General_CI_AS NOT NULL,
	[CodeName] [nvarchar](250) COLLATE Cyrillic_General_CI_AS NOT NULL,
	[IsGlobal] [bit] NOT NULL,
 CONSTRAINT [IX_Roles] UNIQUE NONCLUSTERED 
(
	[ID] ASC
)WITH (IGNORE_DUP_KEY = OFF)
)
END
GO
INSERT [dbo].[Roles] ([ID], [Name], [CodeName], [IsGlobal]) VALUES (N'aaaaaaaa-d9b7-42e1-b6a1-ffffb0e29b01', N'Адміністратор', N'Administrator', 1)
INSERT [dbo].[Roles] ([ID], [Name], [CodeName], [IsGlobal]) VALUES (N'aaaaaaaa-d9b7-42e1-b6a1-ffffb0e29b02', N'Тютор', N'Tutor', 1)
INSERT [dbo].[Roles] ([ID], [Name], [CodeName], [IsGlobal]) VALUES (N'aaaaaaaa-d9b7-42e1-b6a1-ffffb0e29b03', N'Тютор регіональний', N'TutorRegional', 0)
INSERT [dbo].[Roles] ([ID], [Name], [CodeName], [IsGlobal]) VALUES (N'aaaaaaaa-d9b7-42e1-b6a1-ffffb0e29b04', N'Бізнес-Тютор', N'BusinessTutor', 1)
INSERT [dbo].[Roles] ([ID], [Name], [CodeName], [IsGlobal]) VALUES (N'aaaaaaaa-d9b7-42e1-b6a1-ffffb0e29b05', N'Бізнес-Тютор регіональний', N'BusinessTutorRegional', 0)
INSERT [dbo].[Roles] ([ID], [Name], [CodeName], [IsGlobal]) VALUES (NULL, N'Студент', N'Student', 0)
/****** Object:  Table [dbo].[ObjectRegions]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ObjectRegions]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ObjectRegions](
	[ObjectID] [uniqueidentifier] NOT NULL,
	[RegionID] [uniqueidentifier] NULL,
 CONSTRAINT [PK__ObjectRegions__44952D46] PRIMARY KEY CLUSTERED 
(
	[ObjectID] ASC
)WITH (IGNORE_DUP_KEY = OFF)
)
END
GO
/****** Object:  UserDefinedFunction [dbo].[dcereports_Tests_GetTestResultCount]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcereports_Tests_GetTestResultCount]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE FUNCTION [dbo].[dcereports_Tests_GetTestResultCount]
(
	@courseID uniqueidentifier
)
RETURNS int
AS
BEGIN
	RETURN 
	    (
	        SELECT COUNT(dbo.dcetools_Fn_Courses_GetTestResultIdList(@courseID))
	    )
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[dceaccess_GetUserPasswordInfo]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dceaccess_GetUserPasswordInfo]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dceaccess_GetUserPasswordInfo]
(
	@login nvarchar(255)
)
AS

BEGIN
	SET NOCOUNT ON;

    SELECT
        Users.PasswordHash as PasswordHash,
        Users.PasswordHashSalt as PasswordHashSalt
        
    FROM
        dbo.Users
        
    WHERE
        Users.Login = @login
	
	RETURN
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[dceaccess_SetUserPasswordInfo]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dceaccess_SetUserPasswordInfo]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dceaccess_SetUserPasswordInfo]
(
	@login nvarchar(255),
	@passwordHash uniqueidentifier,
	@passwordHashSalt uniqueidentifier
)
AS

BEGIN
	SET NOCOUNT ON;

    IF NOT EXISTS(SELECT [ID] FROM Users WHERE Login=@login)
    BEGIN

        DECLARE @noUsersErrorMessage nvarchar(255)
        SELECT @noUsersErrorMessage =
            ''No user found for login ''+ISNULL(@login,''(null)'')+''.''
                
        RAISERROR (@noUsersErrorMessage, 16, 1)
        ROLLBACK TRAN
                
    END

    UPDATE dbo.Users
    SET
        Users.PasswordHash = @passwordHash,
        Users.PasswordHashSalt = @passwordHashSalt
        
    WHERE
        Users.Login = @login

	
	RETURN
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[dceaccess_UpdateLastLogin]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dceaccess_UpdateLastLogin]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dceaccess_UpdateLastLogin]
(
	@login nvarchar(255)
)
AS

BEGIN
	SET NOCOUNT ON;

    IF NOT EXISTS(SELECT [ID] FROM Users WHERE Login=@login)
    BEGIN

        DECLARE @noUsersErrorMessage nvarchar(255)
        SELECT @noUsersErrorMessage =
            ''No user found for login ''+ISNULL(@login,''(null)'')+''.''
                
        RAISERROR (@noUsersErrorMessage, 16, 1)
        ROLLBACK TRAN
                
    END

    UPDATE dbo.Users
    SET
        Users.LastLogin = GetDate()
        
    WHERE
        Users.Login = @login

	
	RETURN
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[dcelegacy_Security_UpdateLastLogin]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcelegacy_Security_UpdateLastLogin]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dcelegacy_Security_UpdateLastLogin]
(
	@login nvarchar(255)
)
AS

BEGIN
	SET NOCOUNT ON;

    IF NOT EXISTS(SELECT [ID] FROM Users WHERE Login=@login)
    BEGIN

        DECLARE @noUsersErrorMessage nvarchar(255)
        SELECT @noUsersErrorMessage =
            ''No user found for login ''+ISNULL(@login,''(null)'')+''.''
                
        RAISERROR (@noUsersErrorMessage, 16, 1)
        ROLLBACK TRAN
                
    END

    UPDATE dbo.Users
    SET
        Users.LastLogin = GetDate()
        
    WHERE
        Users.Login = @login

	
	RETURN
END' 
END
GO
/****** Object:  Table [dbo].[Content]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Content]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Content](
	[id] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[eid] [uniqueidentifier] NOT NULL,
	[Type] [int] NOT NULL,
	[Lang] [int] NULL,
	[DataStr] [nvarchar](255) COLLATE Cyrillic_General_CI_AS NULL,
	[Data] [varbinary](max) NULL,
	[TData] [nvarchar](max) COLLATE Cyrillic_General_CI_AS NULL,
	[ParentEid] [uniqueidentifier] NULL,
	[COrder] [int] NULL,
 CONSTRAINT [PK_Content] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (IGNORE_DUP_KEY = OFF)
)
END
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Content]') AND name = N'eid_Content')
CREATE NONCLUSTERED INDEX [eid_Content] ON [dbo].[Content] 
(
	[eid] ASC
)WITH (IGNORE_DUP_KEY = OFF)
GO
/****** Object:  StoredProcedure [dbo].[dcereports_TutorDiary_Table1]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcereports_TutorDiary_Table1]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dcereports_TutorDiary_Table1]
(
	@startDate datetime,
	@endDate datetime,
	@courseTypeID uniqueidentifier,
	@regionID uniqueidentifier,
	@studentID uniqueidentifier
)
AS

BEGIN
	SET NOCOUNT ON;

    SELECT
        Trainings.RegionName,
        
        MAX(Trainings.CourseName) as CourseName,
        
        COUNT(Trainings.TrainingID) as TrainingsCount,
        
        MIN(Trainings.StartDate) as StartDate,
        MAX(Trainings.EndDate) as EndDate,
        
        COUNT(Trainings.StudentCount) StudentCount,
        COUNT(Trainings.PassedFinalTest) as PassedFinalTest
        
    FROM
        dbo.dcereports_Fn_TutorDiary_GetTrainingsOfCourseType(@courseTypeID)
        as Trainings
        
    WHERE
        (Trainings.StartDate>=@startDate AND Trainings.StartDate<=@endDate)
    OR
        (Trainings.EndDate>=@startDate AND Trainings.EndDate<=@endDate)
        
    GROUP BY
        Trainings.RegionName
	
	RETURN
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[dcelegacy_Security_GetUserByLogin]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcelegacy_Security_GetUserByLogin]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dcelegacy_Security_GetUserByLogin]
(
	@login nvarchar(255)
)
AS

BEGIN
	SET NOCOUNT ON;

    SELECT
        Users.[ID] as [ID],
        Users.Login as Login,
        Users.FirstName as FirstName,
        Users.Patronymic as Patronymic,
        Users.LastName as LastName,
        Users.Email as EMail,
        Users.JobPosition as JobPosition,
        Users.Comments as Comments,
        
        Users.CreateDate as CreateDate,
        Users.LastModifyDate as LastModify
        
    FROM
        dbo.Users
        
    WHERE
        Users.Login = @login
	
	RETURN
END' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[StudentName]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[StudentName]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'

/* return full Student name. if @eng==1 return on English */
CREATE FUNCTION [dbo].[StudentName] (@id uniqueidentifier, @eng bit)  
RETURNS nvarchar(255) AS  
BEGIN 

declare @Name nvarchar(255)

declare @FirstName nvarchar(255)
declare @LastName nvarchar(255)
declare @Patronymic nvarchar(255)
declare @FirstNameEng nvarchar(255)
declare @LastNameEng nvarchar(255)

select @FirstName=ISNULL(FirstName,''''), @LastName=ISNULL(LastName,''''), @Patronymic=ISNULL(Patronymic,''''),
	@FirstNameEng=ISNULL(FirstNameEng,''''), @LastNameEng=ISNULL(LastNameEng,'''') from dbo.Students where id=@id

set @Name = @FirstNameEng + '' '' +  @LastNameEng
if @Name is not null and @Name != '''' and @eng =1
	return LTRIM(@Name)
else
	set @Name = @LastName + '' '' + @FirstName + '' '' + @Patronymic

return LTRIM(@Name)
END

' 
END
GO
/****** Object:  Table [dbo].[BulletinBoard]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BulletinBoard]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[BulletinBoard](
	[id] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[Author] [uniqueidentifier] NOT NULL,
	[PostDate] [datetime] NOT NULL,
	[Message] [uniqueidentifier] NOT NULL,
	[Training] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_BulletinBoard] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (IGNORE_DUP_KEY = OFF)
)
END
GO
/****** Object:  Table [dbo].[CTracks]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CTracks]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[CTracks](
	[id] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[Name] [uniqueidentifier] NOT NULL,
	[Description] [uniqueidentifier] NOT NULL,
	[Courses] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_CTracks] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (IGNORE_DUP_KEY = OFF)
)
END
GO
/****** Object:  Table [dbo].[CourseDomain]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CourseDomain]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[CourseDomain](
	[id] [uniqueidentifier] NOT NULL,
	[Parent] [uniqueidentifier] NULL,
	[Name] [uniqueidentifier] NOT NULL
)
END
GO
/****** Object:  Table [dbo].[CourseType]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CourseType]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[CourseType](
	[id] [uniqueidentifier] NOT NULL,
	[Name] [uniqueidentifier] NOT NULL
)
END
GO
/****** Object:  Table [dbo].[Courses]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Courses]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Courses](
	[id] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[Version] [nvarchar](2) COLLATE Cyrillic_General_CI_AS NOT NULL,
	[Code] [nvarchar](15) COLLATE Cyrillic_General_CI_AS NOT NULL,
	[Name] [uniqueidentifier] NOT NULL,
	[Area] [uniqueidentifier] NULL,
	[Type] [uniqueidentifier] NULL,
	[Cost1] [float] NOT NULL,
	[CostType1] [uniqueidentifier] NULL,
	[Cost2] [float] NOT NULL,
	[CostType2] [uniqueidentifier] NULL,
	[Author] [uniqueidentifier] NOT NULL,
	[CPublic] [bit] NOT NULL,
	[DescriptionShort] [uniqueidentifier] NOT NULL,
	[DescriptionLong] [uniqueidentifier] NOT NULL,
	[Requirements] [uniqueidentifier] NOT NULL,
	[Keywords] [uniqueidentifier] NOT NULL,
	[Additions] [uniqueidentifier] NOT NULL,
	[Instructors] [uniqueidentifier] NOT NULL,
	[StartQuestionnaire] [uniqueidentifier] NULL,
	[FinishQuestionnaire] [uniqueidentifier] NULL,
	[DiskFolder] [nvarchar](128) COLLATE Cyrillic_General_CI_AS NOT NULL,
	[isReady] [bit] NOT NULL,
	[CourseLanguage] [int] NOT NULL,
 CONSTRAINT [PK_Courses] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (IGNORE_DUP_KEY = OFF)
)
END
GO
/****** Object:  Table [dbo].[Currency]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Currency]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Currency](
	[id] [uniqueidentifier] NOT NULL,
	[Name] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Currency] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (IGNORE_DUP_KEY = OFF)
)
END
GO
/****** Object:  Table [dbo].[Entities]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Entities]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Entities](
	[id] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[Parent] [uniqueidentifier] NULL,
	[Type] [int] NOT NULL,
 CONSTRAINT [PK_Entities] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (IGNORE_DUP_KEY = OFF)
)
END
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Entities]') AND name = N'id')
CREATE UNIQUE NONCLUSTERED INDEX [id] ON [dbo].[Entities] 
(
	[id] ASC
)WITH (IGNORE_DUP_KEY = ON)
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Entities]') AND name = N'Type')
CREATE NONCLUSTERED INDEX [Type] ON [dbo].[Entities] 
(
	[Type] ASC
)WITH (IGNORE_DUP_KEY = OFF)
GO
/****** Object:  Table [dbo].[Groups]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Groups]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Groups](
	[id] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[Name] [nvarchar](100) COLLATE Cyrillic_General_CI_AS NULL,
	[Description] [nvarchar](255) COLLATE Cyrillic_General_CI_AS NULL,
	[Type] [int] NULL,
 CONSTRAINT [PK_Groups] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (IGNORE_DUP_KEY = OFF)
)
END
GO
/****** Object:  Table [dbo].[Languages]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Languages]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Languages](
	[id] [int] NOT NULL,
	[NameEng] [nvarchar](50) COLLATE Cyrillic_General_CI_AS NULL,
	[NameNative] [nvarchar](50) COLLATE Cyrillic_General_CI_AS NULL,
	[Abbr] [nchar](3) COLLATE Cyrillic_General_CI_AS NULL,
 CONSTRAINT [IX_Languages] UNIQUE NONCLUSTERED 
(
	[id] ASC
)WITH (IGNORE_DUP_KEY = OFF)
)
END
GO
/****** Object:  Table [dbo].[News]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[News]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[News](
	[id] [uniqueidentifier] NOT NULL,
	[NewsDate] [datetime] NOT NULL,
	[Head] [uniqueidentifier] NOT NULL,
	[Short] [uniqueidentifier] NOT NULL,
	[Text] [uniqueidentifier] NOT NULL,
	[MoreHref] [nvarchar](128) COLLATE Cyrillic_General_CI_AS NULL,
	[MoreText] [uniqueidentifier] NOT NULL,
	[CourseCode] [nvarchar](15) COLLATE Cyrillic_General_CI_AS NULL,
	[Image] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_News] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (IGNORE_DUP_KEY = OFF)
)
END
GO
/****** Object:  Table [dbo].[TestQuestions]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TestQuestions]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[TestQuestions](
	[id] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[Test] [uniqueidentifier] NOT NULL,
	[Content] [uniqueidentifier] NOT NULL,
	[ShortHint] [uniqueidentifier] NOT NULL,
	[LongHint] [uniqueidentifier] NOT NULL,
	[Points] [int] NOT NULL,
	[Answer] [uniqueidentifier] NOT NULL,
	[Type] [int] NOT NULL,
	[QOrder] [int] NOT NULL,
	[Theme] [uniqueidentifier] NULL,
 CONSTRAINT [PK_TestQuestions] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (IGNORE_DUP_KEY = OFF)
)
END
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[TestQuestions]') AND name = N'Test_TestQuestions')
CREATE NONCLUSTERED INDEX [Test_TestQuestions] ON [dbo].[TestQuestions] 
(
	[Test] ASC
)WITH (IGNORE_DUP_KEY = OFF)
GO
/****** Object:  Table [dbo].[TestResults]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TestResults]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[TestResults](
	[id] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[Test] [uniqueidentifier] NOT NULL,
	[Student] [uniqueidentifier] NOT NULL,
	[Complete] [bit] NOT NULL,
	[CompletionDate] [datetime] NULL,
	[Tries] [int] NULL,
	[AllowTries] [int] NULL,
	[TryStart] [datetime] NULL,
	[Skipped] [bit] NOT NULL,
 CONSTRAINT [PK_TestResults] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (IGNORE_DUP_KEY = OFF)
)
END
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[TestResults]') AND name = N'Test_Student_TestResults')
CREATE NONCLUSTERED INDEX [Test_Student_TestResults] ON [dbo].[TestResults] 
(
	[Test] ASC,
	[Student] ASC
)WITH (IGNORE_DUP_KEY = OFF)
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[TestResults]') AND name = N'Test_TestResults')
CREATE NONCLUSTERED INDEX [Test_TestResults] ON [dbo].[TestResults] 
(
	[Test] ASC
)WITH (IGNORE_DUP_KEY = OFF)
GO
/****** Object:  Table [dbo].[TrainingBlocking]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TrainingBlocking]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[TrainingBlocking](
	[id] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[Student] [uniqueidentifier] NOT NULL,
	[Training] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_TrainingStats] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (IGNORE_DUP_KEY = OFF)
)
END
GO
/****** Object:  Table [dbo].[ForumTopics]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ForumTopics]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ForumTopics](
	[id] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[Training] [uniqueidentifier] NOT NULL,
	[Author] [uniqueidentifier] NOT NULL,
	[Student] [uniqueidentifier] NULL,
	[Topic] [ntext] COLLATE Cyrillic_General_CI_AS NULL,
	[Message] [ntext] COLLATE Cyrillic_General_CI_AS NULL,
	[PostDate] [datetime] NOT NULL,
	[Blocked] [bit] NULL,
 CONSTRAINT [PK_ForumTopics] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (IGNORE_DUP_KEY = OFF)
)
END
GO
/****** Object:  Table [dbo].[Schedule]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Schedule]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Schedule](
	[id] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[Training] [uniqueidentifier] NULL,
	[Theme] [uniqueidentifier] NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[isOpen] [bit] NOT NULL,
	[Mandatory] [bit] NOT NULL,
 CONSTRAINT [PK_TrainingSchedule] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (IGNORE_DUP_KEY = OFF)
)
END
GO
/****** Object:  Table [dbo].[Tasks]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tasks]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Tasks](
	[id] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[Training] [uniqueidentifier] NULL,
	[Creator] [uniqueidentifier] NULL,
	[Type] [int] NULL,
	[Name] [uniqueidentifier] NULL,
	[Description] [uniqueidentifier] NULL,
	[TaskTime] [datetime] NULL,
 CONSTRAINT [PK_Tasks] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (IGNORE_DUP_KEY = OFF)
)
END
GO
/****** Object:  Table [dbo].[CDPath]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CDPath]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[CDPath](
	[id] [uniqueidentifier] NOT NULL,
	[studentId] [uniqueidentifier] NOT NULL,
	[trainingId] [uniqueidentifier] NOT NULL,
	[cdPath] [nvarchar](120) COLLATE Cyrillic_General_CI_AS NULL,
	[useCDLib] [bit] NOT NULL,
 CONSTRAINT [PK_CDPath] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (IGNORE_DUP_KEY = OFF)
)
END
GO
/****** Object:  Table [dbo].[ForumReplies]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ForumReplies]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ForumReplies](
	[id] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[Topic] [uniqueidentifier] NOT NULL,
	[Author] [uniqueidentifier] NOT NULL,
	[Message] [ntext] COLLATE Cyrillic_General_CI_AS NULL,
	[PostDate] [datetime] NOT NULL,
 CONSTRAINT [PK_ForumReplies] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (IGNORE_DUP_KEY = OFF)
)
END
GO
/****** Object:  Table [dbo].[TaskSolutions]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TaskSolutions]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[TaskSolutions](
	[id] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[Task] [uniqueidentifier] NOT NULL,
	[Student] [uniqueidentifier] NOT NULL,
	[Complete] [int] NOT NULL,
	[Solution] [ntext] COLLATE Cyrillic_General_CI_AS NULL,
	[SDate] [datetime] NULL,
 CONSTRAINT [PK_TaskCompletion] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (IGNORE_DUP_KEY = OFF)
)
END
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[TaskSolutions]') AND name = N'Student_TaskCompletion')
CREATE NONCLUSTERED INDEX [Student_TaskCompletion] ON [dbo].[TaskSolutions] 
(
	[Student] ASC
)WITH (IGNORE_DUP_KEY = OFF)
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[TaskSolutions]') AND name = N'Task_Student_TaskCompletion')
CREATE UNIQUE NONCLUSTERED INDEX [Task_Student_TaskCompletion] ON [dbo].[TaskSolutions] 
(
	[Task] ASC,
	[Student] ASC
)WITH (IGNORE_DUP_KEY = ON)
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[TaskSolutions]') AND name = N'Task_TaskCompletion')
CREATE NONCLUSTERED INDEX [Task_TaskCompletion] ON [dbo].[TaskSolutions] 
(
	[Task] ASC
)WITH (IGNORE_DUP_KEY = OFF)
GO
/****** Object:  Table [dbo].[TestAnswers]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TestAnswers]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[TestAnswers](
	[id] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[TestResults] [uniqueidentifier] NOT NULL,
	[Question] [uniqueidentifier] NOT NULL,
	[Answer] [ntext] COLLATE Cyrillic_General_CI_AS NULL,
	[AnswerTime] [int] NULL,
	[Points] [int] NOT NULL,
 CONSTRAINT [PK_TestAnswers] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (IGNORE_DUP_KEY = OFF)
)
END
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[TestAnswers]') AND name = N'Test_TestAnswers')
CREATE NONCLUSTERED INDEX [Test_TestAnswers] ON [dbo].[TestAnswers] 
(
	[TestResults] ASC
)WITH (IGNORE_DUP_KEY = OFF)
GO
/****** Object:  Table [dbo].[CTrackRequest]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CTrackRequest]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[CTrackRequest](
	[id] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[Student] [uniqueidentifier] NOT NULL,
	[CTrack] [uniqueidentifier] NOT NULL,
	[RequestDate] [datetime] NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[Comments] [nvarchar](255) COLLATE Cyrillic_General_CI_AS NULL,
 CONSTRAINT [PK_CTrackRequest] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (IGNORE_DUP_KEY = OFF),
 CONSTRAINT [IX_CTrackRequest_Unique] UNIQUE NONCLUSTERED 
(
	[Student] ASC,
	[CTrack] ASC
)WITH (IGNORE_DUP_KEY = OFF)
)
END
GO
/****** Object:  Table [dbo].[CourseRequest]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CourseRequest]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[CourseRequest](
	[id] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[Student] [uniqueidentifier] NOT NULL,
	[Course] [uniqueidentifier] NOT NULL,
	[RequestDate] [datetime] NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[Comments] [nvarchar](255) COLLATE Cyrillic_General_CI_AS NULL,
 CONSTRAINT [PK_CourseRequest] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (IGNORE_DUP_KEY = OFF)
)
END
GO
/****** Object:  Table [dbo].[Rights]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Rights]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Rights](
	[id] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[eid] [uniqueidentifier] NOT NULL,
	[permid] [uniqueidentifier] NOT NULL,
	[read] [bit] NULL,
	[write] [bit] NULL,
	[delete] [bit] NULL,
 CONSTRAINT [PK_Rights] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (IGNORE_DUP_KEY = OFF)
)
END
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Rights]') AND name = N'Rights_eid')
CREATE NONCLUSTERED INDEX [Rights_eid] ON [dbo].[Rights] 
(
	[eid] ASC
)WITH (IGNORE_DUP_KEY = OFF)
GO
/****** Object:  Table [dbo].[GroupMembers]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GroupMembers]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[GroupMembers](
	[mid] [uniqueidentifier] NOT NULL,
	[id] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[MGroup] [uniqueidentifier] NULL,
 CONSTRAINT [PK_GroupMembers] PRIMARY KEY CLUSTERED 
(
	[mid] ASC
)WITH (IGNORE_DUP_KEY = OFF)
)
END
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[GroupMembers]') AND name = N'IX_GroupMembers')
CREATE NONCLUSTERED INDEX [IX_GroupMembers] ON [dbo].[GroupMembers] 
(
	[MGroup] ASC
)WITH (IGNORE_DUP_KEY = OFF)
GO
/****** Object:  UserDefinedFunction [dbo].[IsTestMandatory]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[IsTestMandatory]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'
CREATE FUNCTION [dbo].[IsTestMandatory] (@id uniqueidentifier, @trid uniqueidentifier)  
RETURNS bit AS  
BEGIN 
declare @m bit
set @m = (select (sc.Mandatory) as  Mandatory from Themes t, Tests ts, dbo.Schedule sc where ts.Parent=t.id and ts.id=@id and sc.Training=@trid and sc.Theme=t.id)
return ISNULL(@m,1)
END





' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[IsCourseTestComplete]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[IsCourseTestComplete]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'
CREATE FUNCTION [dbo].[IsCourseTestComplete] (@trainingId uniqueidentifier , @studentid uniqueidentifier)  
RETURNS int AS  
BEGIN 
	RETURN (SELECT Count(*) from Tests t, TestResults tr, Trainings tra where
		t.id = tr.Test and tr.Complete =1 and tra.id = @trainingId and tra.Course = t.Parent and t.Type=1 and tr.Student = @studentid)
END


' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[GetTestCourse]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetTestCourse]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'
CREATE FUNCTION [dbo].[GetTestCourse] (@id uniqueidentifier)  
RETURNS uniqueidentifier AS  
BEGIN 
declare @parent uniqueidentifier
set @parent = (select parent from Tests where id=@id)
declare @Type int
set @Type = (select e.Type from Entities e where e.id=@parent)
while (@Type <> 6) /* Course */
BEGIN /* for theme */
	set @parent = (select parent from Themes where id=@parent)
	set @Type = (select e.Type from Entities e where e.id=@parent)
END
return @parent

END


' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[TrainingTests]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TrainingTests]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE FUNCTION [dbo].[TrainingTests] (@id uniqueidentifier)  
RETURNS @members TABLE (id uniqueidentifier, Type int) AS  
BEGIN 

INSERT @members  SELECT t.id , t.Type from Tests t, Themes tm, Trainings tr WHERE 
 tm.Parent = tr.Course and t.Parent =tm.id and tr.id = @id 

INSERT @members SELECT t.id , t.Type from Tests t, Trainings tr  WHERE
 t.Parent = tr.Course and tr.id = @id

declare @course uniqueidentifier
SET @course = (select Course from Trainings where id=@id)
/* locating tests from subthemes */

DECLARE tcur cursor 
FOR SELECT t.id from Themes t  WHERE t.Parent = @course

OPEN tcur
DECLARE @themeid uniqueidentifier

FETCH NEXT FROM tcur INTO @themeid 

WHILE @@FETCH_STATUS <> -1
BEGIN
 IF (@@FETCH_STATUS = 0)
 BEGIN
  INSERT @members SELECT t.id , t.Type  from dbo.AllSubThemes(@themeid,1)  st , Tests t where t.Parent = st.id
 END
 FETCH NEXT FROM tcur INTO @themeid 
END
CLOSE tcur
DEALLOCATE tcur

return 
END

' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[CourseTests]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CourseTests]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'
CREATE FUNCTION [dbo].[CourseTests] (@id uniqueidentifier)  
RETURNS @members TABLE (id uniqueidentifier, Type int) AS  
BEGIN 

INSERT @members SELECT t.id , t.Type from Tests t, Themes tm WHERE 
 tm.Parent = @id and t.Parent =tm.id
INSERT @members SELECT t.id , t.Type from Tests t  WHERE
 t.Parent = @id

/* locating tests from subthemes */

DECLARE tcur cursor 
FOR SELECT t.id from Themes t  WHERE t.Parent = @id

OPEN tcur
DECLARE @themeid uniqueidentifier

FETCH NEXT FROM tcur INTO @themeid 

WHILE @@FETCH_STATUS <> -1
BEGIN
 IF (@@FETCH_STATUS = 0)
 BEGIN
  INSERT @members SELECT t.id , t.Type  from dbo.AllSubThemes(@themeid,1)  st , Tests t where t.Parent = st.id
 END
 FETCH NEXT FROM tcur INTO @themeid 
END
CLOSE tcur
DEALLOCATE tcur

return 
END

' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[GetThemeName]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetThemeName]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'
CREATE FUNCTION [dbo].[GetThemeName] (@id uniqueidentifier, @lang int)  
RETURNS nvarchar(255) AS  
BEGIN 

DECLARE @type int
SET @type = (SELECT Type FROM Entities WHERE id=@id)

DECLARE @Name uniqueidentifier
IF (@type = 11) /* Theme */ 
	SET @Name = (SELECT Name from Themes where id=@id)
ELSE 	/* Course */
	SET @Name = (SELECT Name from Courses  where id=@id)

/* Name */
DECLARE @strr nvarchar(255) 
SET @strr = (SELECT top 1 c.DataStr from dbo.Content c, Languages L where eid=@Name and c.Lang=@lang)
IF (@strr = NULL or @strr='''')
	SET @strr = (SELECT top 1 c.DataStr from dbo.Content c where eid=@Name and c.Lang <> @lang 
			and c.DataStr is not NULL and c.DataStr <>'''' order by c.Lang)
return @strr
END

' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[CourseOfTheme]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CourseOfTheme]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'
CREATE FUNCTION [dbo].[CourseOfTheme] (@id uniqueidentifier)  
RETURNS uniqueidentifier AS  
BEGIN 
declare @Course uniqueidentifier
declare @Parent uniqueidentifier

set @Parent=(select Parent from Themes where id=@id)
while (@Parent is not NULL)
begin
	set @Course = @Parent
	set @Parent=(select Parent from Themes where id=@Course)
end

return @Course
END

' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[dcetools_Fn_Themes_GetThemeParent]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Fn_Themes_GetThemeParent]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE FUNCTION [dbo].[dcetools_Fn_Themes_GetThemeParent]
(
	@themeID uniqueidentifier
)
RETURNS uniqueidentifier
AS

BEGIN
	RETURN
	    (SELECT
	        Themes.Parent
	    FROM
	        dbo.Themes
	    WHERE
	        Themes.[ID] = @themeID)
END' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[CourseDuration]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CourseDuration]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'
CREATE FUNCTION [dbo].[CourseDuration] (@id uniqueidentifier)  
RETURNS int AS 
BEGIN 
	return (select SUM( Duration) from Themes where Parent=@id)
END
' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[dcetools_Fn_Trainings_Students_GetStudentTrainingIDList]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Fn_Trainings_Students_GetStudentTrainingIDList]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE FUNCTION [dbo].[dcetools_Fn_Trainings_Students_GetStudentTrainingIDList] 
(
    @studentID uniqueidentifier
)
RETURNS TABLE
AS
	
RETURN
        SELECT
            Trainings.[ID] as [ID]

        FROM
            Trainings,
            Groups,
            GroupMembers

        WHERE
            GroupMembers.[ID] = @studentID
        AND
            Trainings.Students = Groups.ID
        AND
            GroupMembers.MGroup = Groups.ID' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[GetUserRole]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetUserRole]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'
CREATE FUNCTION [dbo].[GetUserRole] (@userId uniqueidentifier, @trId  uniqueidentifier)  
RETURNS int AS  
BEGIN 
if (select m.id from GroupMembers m, Trainings t where m.id=@userId and m.MGroup= t.Curators and t.id=@trId) != null
	return 2
if (select m.id from GroupMembers m, Trainings t where m.id=@userId and m.MGroup= t.Instructors and t.id=@trId) != null
	return 1
return 0
END




' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[dcetools_Fn_Trainings_GetCourseID]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Fn_Trainings_GetCourseID]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE FUNCTION [dbo].[dcetools_Fn_Trainings_GetCourseID]
(
	@trainingID uniqueidentifier
)
RETURNS uniqueidentifier
AS

BEGIN
	RETURN
	    (SELECT
                Trainings.Course
            FROM
                Trainings
            WHERE
                Trainings.ID = @trainingID)
END' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[dcetools_Fn_Trainings_Students_GetIDList]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Fn_Trainings_Students_GetIDList]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE FUNCTION [dbo].[dcetools_Fn_Trainings_Students_GetIDList] 
(
    @trainingID uniqueidentifier
)
RETURNS TABLE
AS
	
RETURN
        SELECT
            GroupMembers.[ID] as [ID]

        FROM
            Trainings,
            Groups,
            GroupMembers

        WHERE
            Trainings.ID = @trainingID
        AND
            Trainings.Students = Groups.ID
        AND
            GroupMembers.MGroup = Groups.ID
' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[dcetools_Fn_Trainings_Students_GetTrainingStudentCount]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Fn_Trainings_Students_GetTrainingStudentCount]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE FUNCTION [dbo].[dcetools_Fn_Trainings_Students_GetTrainingStudentCount] 
(
	@trainingID uniqueidentifier
)
RETURNS int
AS

BEGIN
	RETURN 
	    (SELECT
	        COUNT(GroupMembers.[ID])

        FROM
            Trainings,
            Groups,
            GroupMembers

        WHERE
            Trainings.ID = @trainingID
        AND
            Trainings.Students = Groups.ID
        AND
            GroupMembers.MGroup = Groups.ID)
END' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[CourseIsActive]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CourseIsActive]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'
CREATE FUNCTION [dbo].[CourseIsActive] (@id uniqueidentifier)  
RETURNS bit AS 
BEGIN 
return (select count(*) from Trainings where Course=@id and isActive=1)
END



' 
END
GO
/****** Object:  StoredProcedure [dbo].[GetOrdersWithRegionalPolicy]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetOrdersWithRegionalPolicy]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[GetOrdersWithRegionalPolicy]
	@homeRegion uniqueidentifier
AS
BEGIN

SET NOCOUNT ON

-- Select from CourseRequest
SELECT
    ID,
    Student as StudentID,
    Course as CourseID,
    RequestDate,
    Comments

FROM
    CourseRequest

WHERE
        (
            SELECT
                ObjectRegions.RegionID
            FROM ObjectRegions
            WHERE ObjectID=CourseRequest.Student
        ) = @homeRegion
        
        OR
        @homeRegion IS NULL



-- Select from Students (all students restricted by region)
SELECT
    Students.ID,
    RTrim(
        RTrim(
            LTrim(RTrim(Students.FirstName)) + '' '' + LTrim(Students.Patronymic)
                    ) + '' '' +LTrim(Students.LastName)) as FullName,
    Students.EMail,
    Students.JobPosition as Position,
    (SELECT ObjectRegions.RegionID from ObjectRegions WHERE  ObjectRegions.ObjectID = Students.ID) as Region

FROM
    Students

WHERE
-- Restrict students to home region
    (SELECT ObjectRegions.RegionID from ObjectRegions WHERE  ObjectRegions.ObjectID = Students.ID) = @homeRegion
OR
    @homeRegion IS NULL


-- Fill #Regions
SELECT DISTINCT RegionID
INTO #Regions
FROM dbo.ObjectRegions
---------------------------------------------
SELECT
    RegionID as RegionID,
    (
        ISNULL( ISNULL( ISNULL(
                    (SELECT [Content].DataStr FROM dbo.[Content] WHERE [Content].eid = #Regions.RegionID AND [Content].Lang = 0),
                    (SELECT [Content].DataStr FROM dbo.[Content] WHERE [Content].eid = #Regions.RegionID AND [Content].Lang = 1)),
                (SELECT [Content].DataStr FROM dbo.[Content] WHERE [Content].eid = #Regions.RegionID AND [Content].Lang = 2)),
            (SELECT TOP 1 [Content].DataStr FROM dbo.[Content] WHERE [Content].eid = #Regions.RegionID AND NOT ([Content].DataStr IS NULL)) )
    ) as [Name]
FROM
    #Regions
    
-- Select Courses
SELECT
    ID,
    (
        ISNULL( ISNULL( ISNULL(
                    (SELECT [Content].DataStr FROM dbo.[Content] WHERE [Content].eid = Courses.[Name] AND [Content].Lang = 0) ,
                    (SELECT [Content].DataStr FROM dbo.[Content] WHERE [Content].eid = Courses.[Name] AND [Content].Lang = 1)) ,
                (SELECT [Content].DataStr FROM dbo.[Content] WHERE [Content].eid = Courses.[Name] AND [Content].Lang = 2)) ,
            (SELECT TOP 1 [Content].DataStr FROM dbo.[Content] WHERE [Content].eid = Courses.[Name] AND NOT ([Content].DataStr IS NULL)) )
    ) as [Name]
FROM dbo.Courses




-- Fill #Trainings (...)

SELECT
    Trainings.ID,
    Trainings.Code,
    (
        ISNULL( ISNULL( ISNULL(
                    (SELECT [Content].DataStr FROM dbo.[Content] WHERE [Content].eid = Trainings.[Name] AND [Content].Lang = 0) ,
                    (SELECT [Content].DataStr FROM dbo.[Content] WHERE [Content].eid = Trainings.[Name] AND [Content].Lang = 1)) ,
                (SELECT [Content].DataStr FROM dbo.[Content] WHERE [Content].eid = Trainings.[Name] AND [Content].Lang = 2)) ,
            (SELECT TOP 1 [Content].DataStr FROM dbo.[Content] WHERE [Content].eid = Trainings.[Name] AND NOT ([Content].DataStr IS NULL)) )
    ) as [Name],
    (
        ISNULL( ISNULL( ISNULL(
                    (SELECT [Content].DataStr FROM dbo.[Content] WHERE [Content].eid = Trainings.Comment AND [Content].Lang = 0) ,
                    (SELECT [Content].DataStr FROM dbo.[Content] WHERE [Content].eid = Trainings.Comment AND [Content].Lang = 1)) ,
                (SELECT [Content].DataStr FROM dbo.[Content] WHERE [Content].eid = Trainings.Comment AND [Content].Lang = 2)) ,
            (SELECT TOP 1 [Content].DataStr FROM dbo.[Content] WHERE [Content].eid = Trainings.Comment AND NOT ([Content].DataStr IS NULL)) )
    ) as Comment,
    Trainings.Course,            -- ref

    Trainings.IsPublic,
    Trainings.IsActive,
    Trainings.TimeStrict,

    Trainings.StartDate,
    Trainings.EndDate,

    Trainings.TestOnly,
    Trainings.Expires,
    
    (  SELECT
            ObjectRegions.RegionID
        FROM
            ObjectRegions
        WHERE
            ObjectRegions.ObjectID = Trainings.ID
        AND
            (@homeRegion IS NULL OR @homeRegion=ObjectRegions.RegionID)
    ) as Region


FROM dbo.Trainings

END' 
END
GO
/****** Object:  StoredProcedure [dbo].[GetTrainingForumWithRegionalPolicy]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetTrainingForumWithRegionalPolicy]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Oleg Mihailik
-- Create date: 29.11.2005
-- Description:	Get training''s forum with details.
-- =============================================
CREATE PROCEDURE [dbo].[GetTrainingForumWithRegionalPolicy]
	@homeRegion uniqueidentifier,
    @trainingID uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;


-- Fill #Forums
SELECT
    ForumTopics.ID as ID,
    ForumTopics.Training as TrainingID,
    ForumTopics.Author as AuthorID,
    ForumTopics.Student as StudentID,
    ForumTopics.Topic as Topic,
    ForumTopics.[Message] as [Message],
    ForumTopics.PostDate as PostDate,
    ForumTopics.Blocked as Blocked
INTO #ForumTopics

FROM
    ForumTopics,
    Trainings

WHERE
    Trainings.ID = @trainingID
AND
    ForumTopics.Training = Trainings.ID

-- Restrict training to home region
AND
    (
        (SELECT ObjectRegions.RegionID FROM ObjectRegions WHERE ObjectID=Trainings.ID) = @homeRegion
        OR
        @homeRegion IS NULL
    )
---------------------------------------------
SELECT * FROM #ForumTopics



-- Fill #ForumReplies
SELECT
    ForumReplies.ID as ID,
    ForumReplies.Topic as TopicID,
    ForumReplies.Author as AuthorID,
    ForumReplies.[Message] as [Message],
    ForumReplies.PostDate as PostDate

INTO
    #ForumReplies

FROM
    ForumReplies
---------------------------------------------
SELECT * FROM #ForumReplies




-- Select from Students (all students restricted by forum TOPIC or REPlY authorship)
SELECT
    Students.ID,
    RTrim(
        RTrim(
            LTrim(RTrim(Students.FirstName)) + '' '' + LTrim(Students.Patronymic)
                    ) + '' '' +LTrim(Students.LastName)) as FullName,
    Students.EMail,
    Students.JobPosition as Position,
    (SELECT ObjectRegions.RegionID from ObjectRegions WHERE  ObjectRegions.ObjectID = Students.ID) as Region

FROM
    Students

WHERE
-- Restrict students to home region
    Students.ID IN (SELECT #ForumTopics.AuthorID FROM #ForumTopics)
OR
    Students.ID IN (SELECT #ForumTopics.StudentID FROM #ForumTopics)
OR
    Students.ID IN (SELECT #ForumReplies.AuthorID FROM #ForumReplies)



END' 
END
GO
/****** Object:  StoredProcedure [dbo].[CreateTraining]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CreateTraining]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Oleg Mihailik
-- Create date: 29.11.2005
-- Description:	Create training with details.
-- =============================================
CREATE PROCEDURE [dbo].[CreateTraining]
    @trainingID uniqueidentifier,
    @trainingRegion uniqueidentifier,
    @code nvarchar(250),
    @name nvarchar(250),
    @comment nvarchar(250),
    @courseID uniqueidentifier,
    @isPublic bit,
    @isActive bit,
    @timeStrict bit,
    @startDate datetime,
    @endDate datetime,
    @testOnly bit,
    @expires bit

AS
BEGIN
	SET NOCOUNT ON;

DECLARE
    @nameID uniqueidentifier,
    @commentID uniqueidentifier

SELECT 
    --@trainingID = newid(),
    @nameID = newid(),
    @commentID = newid() 

INSERT INTO dbo.[Content] ( eid, [Type], Lang, DataStr )
VALUES (@nameID, 1, 1, @name )

INSERT INTO dbo.[Content] ( eid, [Type], Lang, DataStr )
VALUES (@commentID, 1, 1, @comment )


INSERT INTO dbo.Trainings
    (
        ID,

        Code,

        [Name],
        Comment,

        Course,

        isPublic,
        isActive,
        TimeStrict,

        StartDate,
        EndDate,

        TestOnly,
        Expires
    )
VALUES
    (
        @trainingID,

        @code,

        @nameID,
        @commentID,

        @courseID,

        @isPublic,
        @isActive,
        @timeStrict,

        @startDate,
        @endDate,

        @testOnly,
        @expires
    )


END
' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[dcetools_Fn_Trainings_Students_IsTrainingContainsStudent]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Fn_Trainings_Students_IsTrainingContainsStudent]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE FUNCTION [dbo].[dcetools_Fn_Trainings_Students_IsTrainingContainsStudent]
(
    @trainingID uniqueidentifier,
    @studentID uniqueidentifier
)
RETURNS bit
AS
BEGIN

    IF EXISTS(
        SELECT
            GroupMembers.[ID] as [ID]

        FROM
            Trainings,
            Groups,
            GroupMembers

        WHERE
            Trainings.ID = @trainingID
        AND
            Trainings.Students = Groups.ID
        AND
            GroupMembers.MGroup = Groups.ID
        AND
            GroupMembers.[ID] = @studentID)
    BEGIN
        RETURN 1
    END
    
    RETURN 0
    
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[GetTrainingStudentsWithRegionalPolicy]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetTrainingStudentsWithRegionalPolicy]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Oleg Mihailik
-- Create date: 29.11.2005
-- Description:	Get trainings with details.
-- =============================================
CREATE PROCEDURE [dbo].[GetTrainingStudentsWithRegionalPolicy]
	@homeRegion uniqueidentifier,
    @trainingID uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;


-- Fill #TrainingStudents
SELECT
    Trainings.ID as TrainingID,
    GroupMembers.ID as StudentID
INTO #TrainingStudents

FROM
    Trainings,
    Groups,
    GroupMembers

WHERE
    Trainings.ID = @trainingID
AND
    Trainings.Students = Groups.ID
AND
    GroupMembers.MGroup = Groups.ID

-- Restrict training to home region
AND
    (
        (SELECT ObjectRegions.RegionID FROM ObjectRegions WHERE ObjectID=Trainings.ID) = @homeRegion
        OR
        @homeRegion IS NULL
    )
---------------------------------------------
SELECT * FROM #TrainingStudents


-- Select from Students (all students restricted by region)
SELECT
    Students.ID,
    RTrim(
        RTrim(
            LTrim(RTrim(Students.FirstName)) + '' '' + LTrim(Students.Patronymic)
                    ) + '' '' +LTrim(Students.LastName)) as FullName,
    Students.EMail,
    Students.JobPosition as Position,
    (SELECT ObjectRegions.RegionID from ObjectRegions WHERE  ObjectRegions.ObjectID = Students.ID) as Region

FROM
    Students

WHERE
-- Restrict students to home region
    (SELECT ObjectRegions.RegionID from ObjectRegions WHERE  ObjectRegions.ObjectID = Students.ID) = @homeRegion
OR
    @homeRegion IS NULL


-- Fill #Regions
SELECT DISTINCT RegionID
INTO #Regions
FROM dbo.ObjectRegions
---------------------------------------------
SELECT
    RegionID as RegionID,
    (
        ISNULL( ISNULL( ISNULL(
                    (SELECT [Content].DataStr FROM dbo.[Content] WHERE [Content].eid = #Regions.RegionID AND [Content].Lang = 0),
                    (SELECT [Content].DataStr FROM dbo.[Content] WHERE [Content].eid = #Regions.RegionID AND [Content].Lang = 1)),
                (SELECT [Content].DataStr FROM dbo.[Content] WHERE [Content].eid = #Regions.RegionID AND [Content].Lang = 2)),
            (SELECT TOP 1 [Content].DataStr FROM dbo.[Content] WHERE [Content].eid = #Regions.RegionID AND NOT ([Content].DataStr IS NULL)) )
    ) as [Name]
FROM
    #Regions

END' 
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateTraining]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UpdateTraining]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Oleg Mihailik
-- Create date: 29.11.2005
-- Description:	Create training with details.
-- =============================================
CREATE PROCEDURE [dbo].[UpdateTraining]
    @trainingID uniqueidentifier,
    @code nvarchar(250),
    @name nvarchar(250),
    @comment nvarchar(250),
    @courseID uniqueidentifier,
    @isPublic bit,
    @isActive bit,
    @timeStrict bit,
    @startDate datetime,
    @endDate datetime,
    @testOnly bit,
    @expires bit

AS
BEGIN
	SET NOCOUNT ON;

DECLARE
    @nameID uniqueidentifier,
    @commentID uniqueidentifier

SELECT
    @nameID = [Name],
    @commentID = Comment
FROM
    dbo.Trainings
WHERE
    Trainings.id = @trainingID

UPDATE dbo.[Content]
SET [Content].DataStr = @name
WHERE eid = @nameID

UPDATE dbo.[Content]
SET [Content].DataStr = @comment
WHERE eid = @commentID




UPDATE dbo.Trainings
SET
        Code = @code,

        [Name] = @nameID,
        Comment = @commentID,

        Course = @courseID,

        isPublic = @isPublic,
        isActive = @isActive,
        TimeStrict = @timeStrict,

        StartDate = @startDate,
        EndDate = @endDate,

        TestOnly = @testOnly,
        Expires = @expires

WHERE
        Trainings.ID = @trainingID


END
' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[dcelegacy_Fn_Subscribe_GetBestTrainingID]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcelegacy_Fn_Subscribe_GetBestTrainingID]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE FUNCTION [dbo].[dcelegacy_Fn_Subscribe_GetBestTrainingID]
(
	@courseID uniqueidentifier,
	@requestDate datetime
)
RETURNS uniqueidentifier
AS

BEGIN
    -- TODO: add parameter homeRegion and restrict search to home region + global region

    
    DECLARE
        @diffRequestDate int
        
    SELECT
        @diffRequestDate = MIN( ABS(DATEDIFF(
            dd,
            @requestDate,
            Trainings.StartDate)) )
        
    FROM
        dbo.Trainings
    WHERE
        Trainings.Course = @courseID
        
        
    IF @diffRequestDate IS NULL
    BEGIN
        RETURN NULL
    END
    
	RETURN
	    (SELECT TOP 1
	        Trainings.[ID]
	    FROM
            dbo.Trainings
	    WHERE
	        ABS(DATEDIFF(
                dd,
                @requestDate,
                Trainings.StartDate)) = @diffRequestDate)
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[GetTrainingsWithRegionalPolicy]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetTrainingsWithRegionalPolicy]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Oleg Mihailik
-- Create date: 29.11.2005
-- Description:	Get trainings with details.
-- =============================================
CREATE PROCEDURE [dbo].[GetTrainingsWithRegionalPolicy] 
	@homeRegion uniqueidentifier 
AS
BEGIN
	SET NOCOUNT ON;

-- Fill #Trainings (...)

SELECT
    Trainings.ID,
    Trainings.Code,
    (
        ISNULL( ISNULL( ISNULL(
                    (SELECT [Content].DataStr FROM dbo.[Content] WHERE [Content].eid = Trainings.[Name] AND [Content].Lang = 0) ,
                    (SELECT [Content].DataStr FROM dbo.[Content] WHERE [Content].eid = Trainings.[Name] AND [Content].Lang = 1)) ,
                (SELECT [Content].DataStr FROM dbo.[Content] WHERE [Content].eid = Trainings.[Name] AND [Content].Lang = 2)) ,
            (SELECT TOP 1 [Content].DataStr FROM dbo.[Content] WHERE [Content].eid = Trainings.[Name] AND NOT ([Content].DataStr IS NULL)) )
    ) as [Name],
    (
        ISNULL( ISNULL( ISNULL(
                    (SELECT [Content].DataStr FROM dbo.[Content] WHERE [Content].eid = Trainings.Comment AND [Content].Lang = 0) ,
                    (SELECT [Content].DataStr FROM dbo.[Content] WHERE [Content].eid = Trainings.Comment AND [Content].Lang = 1)) ,
                (SELECT [Content].DataStr FROM dbo.[Content] WHERE [Content].eid = Trainings.Comment AND [Content].Lang = 2)) ,
            (SELECT TOP 1 [Content].DataStr FROM dbo.[Content] WHERE [Content].eid = Trainings.Comment AND NOT ([Content].DataStr IS NULL)) )
    ) as Comment,
    Trainings.Course,            -- ref

    Trainings.IsPublic,
    Trainings.IsActive,
    Trainings.TimeStrict,

    Trainings.StartDate,
    Trainings.EndDate,

    Trainings.TestOnly,
    Trainings.Expires,
    
    (  SELECT
            ObjectRegions.RegionID
        FROM
            ObjectRegions
        WHERE
            ObjectRegions.ObjectID = Trainings.ID
        AND
            (@homeRegion IS NULL OR @homeRegion=ObjectRegions.RegionID)
    ) as Region


INTO #Trainings
FROM dbo.Trainings
--------------------------------------------
SELECT * FROM #Trainings


-- Select Courses
SELECT
    ID,
    (
        ISNULL( ISNULL( ISNULL(
                    (SELECT [Content].DataStr FROM dbo.[Content] WHERE [Content].eid = Courses.[Name] AND [Content].Lang = 0) ,
                    (SELECT [Content].DataStr FROM dbo.[Content] WHERE [Content].eid = Courses.[Name] AND [Content].Lang = 1)) ,
                (SELECT [Content].DataStr FROM dbo.[Content] WHERE [Content].eid = Courses.[Name] AND [Content].Lang = 2)) ,
            (SELECT TOP 1 [Content].DataStr FROM dbo.[Content] WHERE [Content].eid = Courses.[Name] AND NOT ([Content].DataStr IS NULL)) )
    ) as [Name]
FROM dbo.Courses



-- Fill #Schedule (by training)
SELECT
    Schedule.ID,
    Schedule.Theme,
    Schedule.Training,
    Schedule.StartDate,
    Schedule.EndDate,
    Schedule.IsOpen,
    Schedule.Mandatory
INTO #Schedule
FROM Schedule, #Trainings
WHERE Schedule.Training = #Trainings.ID




SELECT
    Themes.ID as ThemeID,
    Themes.Parent,
    (
        ISNULL( ISNULL( ISNULL(
                    (SELECT [Content].DataStr FROM dbo.[Content] WHERE [Content].eid = Themes.[Name] AND [Content].Lang = 0),
                    (SELECT [Content].DataStr FROM dbo.[Content] WHERE [Content].eid = Themes.[Name] AND [Content].Lang = 1)),
                (SELECT [Content].DataStr FROM dbo.[Content] WHERE [Content].eid = Themes.[Name] AND [Content].Lang = 2)),
            (SELECT TOP 1 [Content].DataStr FROM dbo.[Content] WHERE [Content].eid = Themes.[Name] AND NOT ([Content].DataStr IS NULL)) )
    ) as ThemeName,
    Themes.Duration as ThemeDuration,
    Themes.Mandatory as IsThemeMandatory,
    Themes.[Type] as ThemeType,
    #Schedule.StartDate as StartDate,
    #Schedule.EndDate as EndDate,
    #Schedule.isOpen as IsScheduleOpen,
    #Schedule.Mandatory as IsScheduleMandatory,
    #Schedule.Training,
    Themes.TOrder as [Order]
FROM
    Themes, #Schedule
WHERE
    #Schedule.Theme = Themes.ID
    









-- Fill #Regions
SELECT DISTINCT RegionID
INTO #Regions
FROM dbo.ObjectRegions
---------------------------------------------
SELECT
    RegionID as RegionID,
    (
        ISNULL( ISNULL( ISNULL(
                    (SELECT [Content].DataStr FROM dbo.[Content] WHERE [Content].eid = #Regions.RegionID AND [Content].Lang = 0),
                    (SELECT [Content].DataStr FROM dbo.[Content] WHERE [Content].eid = #Regions.RegionID AND [Content].Lang = 1)),
                (SELECT [Content].DataStr FROM dbo.[Content] WHERE [Content].eid = #Regions.RegionID AND [Content].Lang = 2)),
            (SELECT TOP 1 [Content].DataStr FROM dbo.[Content] WHERE [Content].eid = #Regions.RegionID AND NOT ([Content].DataStr IS NULL)) )
    ) as [Name]
FROM
    #Regions




END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[MakeVocabularyCopy]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MakeVocabularyCopy]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[MakeVocabularyCopy] 
	@newCourseId uniqueidentifier, 
	@oldCourseId uniqueidentifier
as
declare Terms_Cursor cursor local for 
select id from dbo.Vocabulary where Course = @oldCourseId 

open Terms_Cursor

declare @oldTermId uniqueidentifier
declare @newTermId uniqueidentifier

fetch next from Terms_Cursor into @oldTermId
while (@@FETCH_STATUS <> -1)
begin
	if (@@FETCH_STATUS <> -2)
	begin
		set @newTermId = newid()

		insert into Vocabulary select @newTermId, @newCourseId, Term  from Vocabulary 
		where id = @oldTermId
	end

	fetch next from Terms_Cursor into @oldTermId
end

close Terms_Cursor
deallocate Terms_Cursor
' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[dcetools_Fn_Orders_GetCourseID]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Fn_Orders_GetCourseID]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE FUNCTION [dbo].[dcetools_Fn_Orders_GetCourseID]
(
	@orderID uniqueidentifier
)
RETURNS uniqueidentifier
AS

BEGIN
	RETURN
	    (SELECT
	        CourseRequest.Course as CourseID
	    FROM
	        dbo.CourseRequest
	    WHERE
	        CourseRequest.[ID] = @orderID)
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[dcelegacy_Subscribe_PlaceOrder]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcelegacy_Subscribe_PlaceOrder]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dcelegacy_Subscribe_PlaceOrder]
(
	@studentID uniqueidentifier,
	@courseID uniqueidentifier,
	@requestDate datetime,
	@comments nvarchar(255)
)
AS

BEGIN
	SET NOCOUNT ON;
	
	DECLARE
	    @orderID uniqueidentifier
	    
	SELECT
	    @orderID = CourseRequest.[ID]
	FROM
	    dbo.CourseRequest
	WHERE
	    CourseRequest.Student = @studentID
	AND
	    CourseRequest.Course = @courseID
	    
	IF @orderID IS NULL
	BEGIN
	    SELECT
	        @orderID = newid()
	
        INSERT INTO dbo.CourseRequest(
            [ID],
            Student,
            Course,
            RequestDate,
            StartDate,
            Comments)
            
        VALUES(
            @orderID,
            @studentID,
            @courseID,
            @requestDate,
            GetDate(),
            @comments)
	END
	ELSE
	BEGIN
	    UPDATE dbo.CourseRequest
	    SET
	        CourseRequest.RequestDate = @requestDate,
	        CourseRequest.StartDate = GetDate(),
	        CourseRequest.Comments = @comments
	    WHERE
	        CourseRequest.[ID] = @orderID
	END	
	
	
	RETURN
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[dcelegacy_Subscribe_RemoveOrder]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcelegacy_Subscribe_RemoveOrder]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dcelegacy_Subscribe_RemoveOrder]
(
	@orderID uniqueidentifier
)
AS

BEGIN
	SET NOCOUNT ON;
	
    DELETE FROM dbo.CourseRequest
    WHERE CourseRequest.[ID] = @orderID
	
	
	RETURN
END' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[dcetools_Fn_Orders_GetStudentID]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Fn_Orders_GetStudentID]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE FUNCTION [dbo].[dcetools_Fn_Orders_GetStudentID]
(
	@orderID uniqueidentifier
)
RETURNS uniqueidentifier
AS

BEGIN
	RETURN
	    (SELECT
	        CourseRequest.Student as StudentID
	    FROM
	        dbo.CourseRequest
	    WHERE
	        CourseRequest.[ID] = @orderID)
END' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[TopicAuthor]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TopicAuthor]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'
CREATE FUNCTION [dbo].[TopicAuthor] (@id uniqueidentifier)  
RETURNS nvarchar(767) AS  
BEGIN 
DECLARE @type int
DECLARE @author uniqueidentifier
SELECT @type = e.Type, @author = f.Author from Entities e, ForumTopics f where f.id=@id and f.Author= e.id
	
IF (@type = 1)  /* student */
	return (SELECT LastName+'' ''+FirstName+'' ''+Patronymic from Students where id = @author)
ELSE
	return (SELECT LastName+'' ''+FirstName+'' ''+Patronymic from Users where id = @author)
return ''''
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[GetGroupMembers]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetGroupMembers]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[GetGroupMembers] @id uniqueidentifier  AS

DECLARE group_cursor CURSOR
FOR
	SELECT gm.id,  e.Type FROM GroupMembers gm left join Entities e on (e.id = gm.id)  WHERE
	gm.MGroup = @id

DECLARE @eid uniqueidentifier
DECLARE @type int
OPEN group_cursor

FETCH NEXT FROM group_cursor INTO @eid, @type

WHILE (@@FETCH_STATUS <> -1)
BEGIN
	IF  (@@FETCH_STATUS <> -2)
	BEGIN
		SELECT id = @eid
		FETCH NEXT FROM group_cursor INTO @eid, @type
	END
END

CLOSE group_cursor
DEALLOCATE group_cursor
' 
END
GO
/****** Object:  StoredProcedure [dbo].[dceaccess_GetUserRoles]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dceaccess_GetUserRoles]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dceaccess_GetUserRoles]
(
	@login nvarchar(255)
)
AS

BEGIN
	SET NOCOUNT ON;
	
	DECLARE
	    @studentID uniqueidentifier
	    
	SELECT @studentID = Students.[ID]
	FROM dbo.Students
	WHERE Students.Login = @login

    SELECT
        Groups.[id] as [ID],
        Groups.[Name] as [Name],
        Groups.[Type] as [Type]
        
    FROM
        dbo.Groups, dbo.GroupMembers
        
    WHERE
        GroupMembers.mid = @studentID
    AND
        GroupMembers.MGroup = Groups.[id]
	
	RETURN
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[dceaccess_AddUserToRole]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dceaccess_AddUserToRole]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dceaccess_AddUserToRole]
(
	@login nvarchar(255),
	@roleID uniqueidentifier
)
AS

BEGIN
	SET NOCOUNT ON;
	
	DECLARE
	    @studentID uniqueidentifier
	    
	SELECT @studentID = Students.[ID]
	FROM dbo.Students
	WHERE Students.Login = @login

    IF NOT EXISTS(
        SELECT GroupMembers.[ID]
        FROM dbo.GroupMembers
        WHERE GroupMembers.mid=@studentID AND GroupMembers.MGroup=@roleID)
    BEGIN

        INSERT INTO GroupMembers( [ID], mid, MGroup )
        VALUES ( newid(), @studentID, @roleID )

    END 
	
	RETURN
END' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[AllGroupMembers]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AllGroupMembers]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'
CREATE FUNCTION [dbo].[AllGroupMembers] (@id uniqueidentifier)  
RETURNS @members TABLE (mid uniqueidentifier, id uniqueidentifier, Type int, MGroup uniqueidentifier, name nvarchar(100)) AS  
BEGIN 
/* getting subgroups items */
DECLARE cur cursor 
FOR 	SELECT DISTINCT  gm.mid, g.id , g.Name from GroupMembers gm , Entities e , Groups g
	WHERE e.Type = 10 and gm.MGroup = @id and gm.id = e.id
	and gm.id = g.id
OPEN cur

DECLARE @gmid uniqueidentifier
DECLARE @gid uniqueidentifier
DECLARE @groupName nvarchar(100)

FETCH NEXT FROM cur INTO @gmid, @gid, @groupName
WHILE @@FETCH_STATUS <> -1
BEGIN
	IF (@@FETCH_STATUS = 0)
	BEGIN
		INSERT @members VALUES(@gmid, @gid, 10 , @id, @groupName)
		INSERT @members SELECT * from AllGroupMembers(@gid)
	END
	FETCH NEXT FROM cur INTO @gmid,@gid,@groupName
END
CLOSE cur
DEALLOCATE cur

/* getting non-group items */
INSERT @members SELECT gm.mid, e.id ,e.Type, @id, NULL from GroupMembers gm , Entities e WHERE
	e.Type <> 10 and gm.MGroup = @id and gm.id = e.id
return 
END


' 
END
GO
/****** Object:  StoredProcedure [dbo].[dceaccess_RemoveAllUsersFromRole]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dceaccess_RemoveAllUsersFromRole]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dceaccess_RemoveAllUsersFromRole]
(
    @roleID uniqueidentifier
)
AS

BEGIN
	SET NOCOUNT ON;
	
    DELETE FROM dbo.GroupMembers
    WHERE GroupMembers.MGroup=@roleID
	
	RETURN
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[dceaccess_RemoveUserFromRole]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dceaccess_RemoveUserFromRole]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dceaccess_RemoveUserFromRole]
(
	@login nvarchar(255),
	@roleID uniqueidentifier
)
AS

BEGIN
	SET NOCOUNT ON;
	
	
	DECLARE
	    @studentID uniqueidentifier
	    
	SELECT @studentID = Students.[ID]
	FROM dbo.Students
	WHERE Students.Login = @login

    IF EXISTS(
        SELECT GroupMembers.[ID]
        FROM dbo.GroupMembers
        WHERE GroupMembers.mid=@studentID AND GroupMembers.MGroup=@roleID)
    BEGIN

        DELETE FROM dbo.GroupMembers
        WHERE GroupMembers.mid=@studentID AND GroupMembers.MGroup=@roleID

    END 
	
	RETURN
END' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[dcetools_Fn_Trainings_Schedule_GetTrainingThemeScheduleID]    Script Date: 03/17/2007 19:50:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Fn_Trainings_Schedule_GetTrainingThemeScheduleID]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE FUNCTION [dbo].[dcetools_Fn_Trainings_Schedule_GetTrainingThemeScheduleID]
(
    @trainingID uniqueidentifier,
	@themeID uniqueidentifier
)
RETURNS uniqueidentifier
AS

BEGIN
	RETURN
	    (SELECT
	        Schedule.[ID]
	    FROM
	        dbo.Schedule
	    WHERE
	        Schedule.Theme = @themeID
	    AND
	        Schedule.Training = @trainingID)
END' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[dcetools_Fn_Trainings_Schedule_GetTrainingScheduleIDList]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Fn_Trainings_Schedule_GetTrainingScheduleIDList]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE FUNCTION [dbo].[dcetools_Fn_Trainings_Schedule_GetTrainingScheduleIDList]
(
	@trainingID uniqueidentifier
)
RETURNS TABLE --@TrainingSchedule TABLE ([ID] uniqueidentifier)
AS
	
RETURN
	    SELECT Schedule.[ID]
	    FROM dbo.Schedule
	    WHERE Schedule.Training = @trainingID
' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[CountCompleteSolutions]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CountCompleteSolutions]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'
CREATE FUNCTION [dbo].[CountCompleteSolutions] (@trainingId uniqueidentifier , @studentid uniqueidentifier)  
RETURNS int AS  
BEGIN 
	RETURN (SELECT count(*) from dbo.TaskSolutions s , Tasks t where t.Training = @trainingId and s.Complete =1 and s.Student =@studentid and s.Task = t.id)
END





' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[CountStudentSolutions]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CountStudentSolutions]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'
CREATE FUNCTION [dbo].[CountStudentSolutions] (@trainingId uniqueidentifier , @studentid uniqueidentifier)  
RETURNS int AS  
BEGIN 
	RETURN (SELECT count(*) from dbo.TaskSolutions s , Tasks t where t.Training = @trainingId and s.Student =@studentid and s.Task = t.id)
END






' 
END
GO
/****** Object:  StoredProcedure [dbo].[MakeQuestionCopy]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MakeQuestionCopy]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[MakeQuestionCopy] 
	@newid uniqueidentifier, 
	@oldid uniqueidentifier,
	@parentid uniqueidentifier
as

--------------------------------------------------------------
-- Создание новой записи вопроса --
--------------------------------------------------------------

Declare @oldTestContent uniqueidentifier
Set @oldTestContent = (select Content from TestQuestions where id = @oldid)
Declare @newTestContent uniqueidentifier
Set @newTestContent = NEWID( )

Declare @oldShortHint uniqueidentifier
Set @oldShortHint = (select ShortHint from TestQuestions where id = @oldid)
Declare @newShortHint uniqueidentifier
Set @newShortHint = NEWID( )

Declare @oldLongHint uniqueidentifier
Set @oldLongHint = (select LongHint from TestQuestions where id = @oldid)
Declare @newLongHint uniqueidentifier
Set @newLongHint = NEWID( )

Declare @oldAnswer uniqueidentifier
Set @oldAnswer = (select Answer from TestQuestions where id = @oldid)
Declare @newAnswer uniqueidentifier
Set @newAnswer = NEWID( )

-- запись в TestQuestions

insert into dbo.TestQuestions select @newid, @parentid, @newTestContent, 
@newShortHint, @newLongHint, Points, @newAnswer, Type, QOrder, Theme -- копируем старую ссылку (новой еще нет!)
from dbo.TestQuestions where id = @oldid

-- копирование контента вопроса
execute MakeContentCopy @newTestContent, @oldTestContent

-- копирование контента короткой подсказки 
execute MakeContentCopy @newShortHint, @oldShortHint

-- копирование контента длинной подсказки 
execute MakeContentCopy @newLongHint, @oldLongHint

-- копирование контента ответов 
execute MakeContentCopy @newAnswer, @oldAnswer
' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[TestPoints]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TestPoints]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'
CREATE FUNCTION [dbo].[TestPoints] (@id uniqueidentifier)  
RETURNS int AS 
BEGIN 
	return ISNULL((select SUM( Points) from TestQuestions where Test=@id), 0)
END


' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[CountCompleteTests]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CountCompleteTests]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'
CREATE FUNCTION [dbo].[CountCompleteTests] (@trainingId uniqueidentifier , @studentid uniqueidentifier)  
RETURNS int AS  
BEGIN 
	RETURN (SELECT  count(*) from dbo.TrainingTests(@trainingId) t, TestResults tr where tr.Test = t.id and tr.Student = @studentid
		and tr.Complete = 1 and t.Type=1)
END




' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[CountCompletePractice]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CountCompletePractice]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'
CREATE FUNCTION [dbo].[CountCompletePractice] (@trainingId uniqueidentifier , @studentid uniqueidentifier)  
RETURNS int AS  
BEGIN 
	RETURN (SELECT  count(*) from dbo.TrainingTests(@trainingId) t, TestResults tr where tr.Test = t.id and tr.Student = @studentid
		and tr.Complete = 1 and t.Type=2)
END


' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[CountTestResults]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CountTestResults]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'
CREATE FUNCTION [dbo].[CountTestResults](@test uniqueidentifier)  
RETURNS int AS  
BEGIN 
	return (select count(*) from TestResults where Test=@test)
END
' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[dcetools_Fn_Trainings_Forum_GetTopicLastPostDate]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Fn_Trainings_Forum_GetTopicLastPostDate]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE FUNCTION [dbo].[dcetools_Fn_Trainings_Forum_GetTopicLastPostDate] 
(
    @topicID uniqueidentifier
)
RETURNS datetime
AS

BEGIN
	RETURN
	    (SELECT
	        MAX(PostDate) as LastPostDate
	    FROM
	        dbo.ForumReplies
	    WHERE
	        ForumReplies.Topic = @topicID)
END' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[dcetools_Fn_Trainings_Forum_GetTopicMessageCount]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Fn_Trainings_Forum_GetTopicMessageCount]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE FUNCTION [dbo].[dcetools_Fn_Trainings_Forum_GetTopicMessageCount]
(
    @topicID uniqueidentifier
)
RETURNS int
AS

BEGIN
	RETURN
	    (SELECT
	        COUNT([ID]) as MessageCount
	    FROM
	        dbo.ForumReplies
	    WHERE
	        ForumReplies.Topic = @topicID)
END' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[NumReplies]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[NumReplies]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'
CREATE FUNCTION [dbo].[NumReplies] (@topic uniqueidentifier)  
RETURNS int AS  
BEGIN 
	return (select Count(*) from ForumReplies 
	where Topic = @topic)

END

' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[TestResultPoints]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TestResultPoints]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'
CREATE FUNCTION [dbo].[TestResultPoints] (@id uniqueidentifier)
RETURNS int AS  
BEGIN 
	return ISNULL( (SELECT SUM(Points)  FROM   dbo.TestAnswers  WHERE TestResults = @id), 0)
END


' 
END
GO
/****** Object:  StoredProcedure [dbo].[dcelegacy_Trainings_GetTrainingStudentCDPath]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcelegacy_Trainings_GetTrainingStudentCDPath]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dcelegacy_Trainings_GetTrainingStudentCDPath]
(
	@studentID uniqueidentifier,
	@trainingID uniqueidentifier
)
AS

BEGIN
	SET NOCOUNT ON;

        SELECT
            CAST( 
            CASE WHEN ISNULL(CDPath.useCDLib,0) = 0
                THEN CDPath.cdPath
                ELSE NULL
            END as nvarchar(255)) as CDPath
        FROM
            dbo.CDPath
        WHERE
            CDPath.studentId = @studentID
        AND
            CDPath.trainingId=@trainingID
            
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[dcetools_Access_Users_SetUserRole]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Access_Users_SetUserRole]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dcetools_Access_Users_SetUserRole]
(
    -- Cannot change role even for own region''s users if not administrator!
    -- @homeRegion uniqueidentifier,
	@userID uniqueidentifier,
	@roleID uniqueidentifier
)
AS

BEGIN
	SET NOCOUNT ON;
	
    -- EXEC dbo.dcetools_Regions_CheckObjectID_Write @userID, @homeRegion	
	

    IF @roleID IS NULL
    BEGIN
    
        DELETE FROM dbo.UserRoles
        WHERE UserRoles.UserID = @userID
        
        
    
    END
    ELSE
    BEGIN
    
        IF EXISTS (SELECT [Role] FROM dbo.UserRoles WHERE UserID = @userID)
        BEGIN
        
            UPDATE dbo.UserRoles
            SET
                [Role] = @roleID
            WHERE
                UserID = @userID
                
        
        END
        ELSE
        BEGIN
        
            INSERT INTO dbo.UserRoles(UserID, [Role])
            VALUES( @userID, @roleID )
        
        END
    
    END
	
	RETURN
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[LoginGetUserInfo]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LoginGetUserInfo]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Oleg Mihailik
-- Create date: 29.11.2005
-- Description:	Login and get User details. Fill into tables: MyUserInfo, Content
-- =============================================
CREATE PROCEDURE [dbo].[LoginGetUserInfo] 
	@username nvarchar(250), 
	@password nvarchar(250) OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

DECLARE @userID uniqueidentifier
DECLARE @userRoleID uniqueidentifier
DECLARE @userRegionID uniqueidentifier

-- Authenticate, get @userID, role and region
SELECT @userID = ID, @password = Password FROM dbo.Users
WHERE Login = @username

SELECT @userRoleID = Role FROM dbo.UserRoles
WHERE UserID = @userID

SELECT @userRegionID = RegionID FROM dbo.ObjectRegions
WHERE ObjectID = @userID

IF @userID is NULL
BEGIN
    RAISERROR  16 ''Bad username or password.''
    ROLLBACK TRAN
    RETURN
END
-------------------------------------------------------





-- Select {MyUserInfo} (UserID, FullName, RoleID, Region, RoleName)
SELECT
    @userID as ID,
    RTrim(RTrim(LTrim(RTrim( ISNULL(FirstName,'''') )) + '' '' +LTrim( ISNULL(Patronymic,'''') )) + '' ''+LTrim( ISNULL(LastName,'''') )) as FullName,
    @userRoleID as RoleID,
    @userRegionID as Region,
    (
        SELECT TOP 1
            DataStr as [Name]
        FROM dbo.[Content]
        WHERE eid=@userRoleID
    ) as RoleName
FROM
    dbo.Users
WHERE
    id = @userID
-------------------------------------------------------



-- Select from Content/Regions
SELECT TOP 1
    @userRegionID as RegionID,
    DataStr as [Name]
FROM dbo.[Content]
WHERE eid = @userRegionID

END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[dcetools_Access_TestDatabaseWrite]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Access_TestDatabaseWrite]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dcetools_Access_TestDatabaseWrite]
AS

BEGIN
	SET NOCOUNT ON;

    INSERT INTO TestWriteTable([Dummy])
    VALUES (CAST(newid() as nvarchar(255)))
    
    DELETE FROM TestWriteTable
	
	RETURN
END' 
END
GO
/*EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[5] 4[56] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Students"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 121
               Right = 207
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Users'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Users'
GO*/
/****** Object:  UserDefinedFunction [dbo].[dcetools_Fn_Users_GetUserFullName]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Fn_Users_GetUserFullName]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE FUNCTION [dbo].[dcetools_Fn_Users_GetUserFullName]
(
    @userID uniqueidentifier
)
RETURNS nvarchar(255)
AS

BEGIN
	RETURN
	    (SELECT
	        dbo.dcetools_Fn_Util_CombineFullName(
	            Users.FirstName,
	            Users.Patronymic,
	            Users.LastName) as FullName
        FROM
            dbo.Users
        WHERE
            Users.[ID] = @userID)
END' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[dcetools_Fn_Students_GetStudentFullName]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Fn_Students_GetStudentFullName]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE FUNCTION [dbo].[dcetools_Fn_Students_GetStudentFullName]
(
    @studentID uniqueidentifier
)
RETURNS nvarchar(255)
AS

BEGIN
	RETURN
	    (SELECT
	        dbo.dcetools_Fn_Util_CombineFullName(
	            Students.FirstName,
	            Students.Patronymic,
	            Students.LastName) as FullName
        FROM
            dbo.Students
        WHERE
            Students.[ID] = @studentID)
END' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[dcetools_Fn_Regions_GetIdList]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Fn_Regions_GetIdList]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE FUNCTION [dbo].[dcetools_Fn_Regions_GetIdList] ()
RETURNS TABLE
AS
	
RETURN
	    SELECT
	        Regions.[ID] as [ID]
	    FROM
	        dbo.Regions
	    WHERE
	        Regions.[ID] IS NOT NULL' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[dcetools_Fn_Regions_GetObjectRegionName]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Fn_Regions_GetObjectRegionName]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE FUNCTION [dbo].[dcetools_Fn_Regions_GetObjectRegionName] 
(
	@objectID uniqueidentifier
)
RETURNS nvarchar(255)
AS

BEGIN

	RETURN
	    (SELECT
	        Regions.Name
	    FROM
	        dbo.ObjectRegions,
	        dbo.Regions
	    WHERE
	        ObjectID = @objectID
	    AND
	        Regions.[ID] = ObjectRegions.RegionID)
END' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[dcetools_Fn_Regions_GetIdNameList]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Fn_Regions_GetIdNameList]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE FUNCTION [dbo].[dcetools_Fn_Regions_GetIdNameList] ()
RETURNS TABLE
AS
	
RETURN
	    SELECT
	        Regions.[ID] as [ID],
	        Regions.[Name] as RegionName
	    FROM
	        dbo.Regions
	    WHERE
	        Regions.[ID] IS NOT NULL' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[dcetools_Fn_Regions_GetName]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Fn_Regions_GetName]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE FUNCTION [dbo].[dcetools_Fn_Regions_GetName]
(
	@regionID uniqueidentifier
)
RETURNS nvarchar(255)
AS

BEGIN

	RETURN
	    (SELECT
	        Regions.Name
	    FROM
	        dbo.Regions
	    WHERE
	        Regions.[ID] = @regionID)
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[dcetools_Access_Roles_WithNULL]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Access_Roles_WithNULL]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dcetools_Access_Roles_WithNULL]
AS

BEGIN
	SET NOCOUNT ON;

    SELECT
        Roles.[ID],
        Roles.[Name]
    INTO
        #Roles
    FROM
        dbo.Roles
            
    INSERT INTO #Roles([ID], [Name])
    VALUES(NULL,''(none)'')
    
    SELECT * FROM #Roles
	
	RETURN
END' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[dcetools_Fn_Regions_GetObjectRegionID]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Fn_Regions_GetObjectRegionID]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE FUNCTION [dbo].[dcetools_Fn_Regions_GetObjectRegionID] 
(
	@objectID uniqueidentifier
)
RETURNS uniqueidentifier
AS

BEGIN

	RETURN
	    (SELECT RegionID
	    FROM dbo.ObjectRegions
	    WHERE ObjectID = @objectID)
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[GetNewsWithRegionalPolicy]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetNewsWithRegionalPolicy]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Oleg Mihailik
-- Create date: 29.11.2005
-- Description:	Get news restricted by regions.
-- =============================================
CREATE PROCEDURE [dbo].[GetNewsWithRegionalPolicy]
	@homeRegion uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;





-- Fill #News (ID, NewsDate, Head, Short, Text, MoreText, Image and Region as subquery)
    
SELECT
    News.ID,
    News.NewsDate,
    (
        ISNULL( ISNULL( ISNULL(
                    (SELECT [Content].DataStr FROM dbo.[Content] WHERE [Content].eid = News.Head AND [Content].Lang = 0),
                    (SELECT [Content].DataStr FROM dbo.[Content] WHERE [Content].eid = News.Head AND [Content].Lang = 1)),
                (SELECT [Content].DataStr FROM dbo.[Content] WHERE [Content].eid = News.Head AND [Content].Lang = 2)),
            (SELECT TOP 1 [Content].DataStr FROM dbo.[Content] WHERE [Content].eid = News.Head AND NOT ([Content].DataStr IS NULL)) )
    ) as Head,
    (
        ISNULL( ISNULL( ISNULL(
                    (SELECT [Content].DataStr FROM dbo.[Content] WHERE [Content].eid = News.Short AND [Content].Lang = 0),
                    (SELECT [Content].DataStr FROM dbo.[Content] WHERE [Content].eid = News.Short AND [Content].Lang = 1)),
                (SELECT [Content].DataStr FROM dbo.[Content] WHERE [Content].eid = News.Short AND [Content].Lang = 2)),
            (SELECT TOP 1 [Content].DataStr FROM dbo.[Content] WHERE [Content].eid = News.Short AND NOT ([Content].DataStr IS NULL)) )
    ) as Short,
    (
        ISNULL( ISNULL( ISNULL(
                    (SELECT [Content].TData FROM dbo.[Content] WHERE [Content].eid = News.[Text] AND [Content].Lang = 0),
                    (SELECT [Content].TData FROM dbo.[Content] WHERE [Content].eid = News.[Text] AND [Content].Lang = 1)),
                (SELECT [Content].TData FROM dbo.[Content] WHERE [Content].eid = News.[Text] AND [Content].Lang = 2)),
            (SELECT TOP 1 [Content].TData FROM dbo.[Content] WHERE [Content].eid = News.[Text] AND NOT ([Content].TData IS NULL)) )
    ) as [Text],
    (
        ISNULL( ISNULL( ISNULL(
                    (SELECT [Content].TData FROM dbo.[Content] WHERE [Content].eid = News.MoreText AND [Content].Lang = 0),
                    (SELECT [Content].TData FROM dbo.[Content] WHERE [Content].eid = News.MoreText AND [Content].Lang = 1)),
                (SELECT [Content].TData FROM dbo.[Content] WHERE [Content].eid = News.MoreText AND [Content].Lang = 2)),
            (SELECT TOP 1 [Content].TData FROM dbo.[Content] WHERE [Content].eid = News.MoreText AND NOT ([Content].TData IS NULL)) )
    ) as MoreText,
    (
        ISNULL( ISNULL( ISNULL(
                    (SELECT [Content].Data FROM dbo.[Content] WHERE [Content].eid = News.[Image] AND [Content].Lang = 0),
                    (SELECT [Content].Data FROM dbo.[Content] WHERE [Content].eid = News.[Image] AND [Content].Lang = 1)),
                (SELECT [Content].Data FROM dbo.[Content] WHERE [Content].eid = News.[Image] AND [Content].Lang = 2)),
            (SELECT TOP 1 [Content].Data FROM dbo.[Content] WHERE [Content].eid = News.[Image] AND NOT ([Content].Data IS NULL)) )
    ) as [Image],
    
    (  SELECT
            ObjectRegions.RegionID
        FROM
            ObjectRegions
        WHERE
            ObjectRegions.ObjectID = News.ID
        AND
            (@homeRegion IS NULL OR @homeRegion=ObjectRegions.RegionID)
    ) as Region,

    News.MoreHRef

INTO #News
FROM dbo.News
--------------------------------------------
SELECT * FROM #News


-- Fill #Regions
SELECT DISTINCT RegionID
INTO #Regions
FROM dbo.ObjectRegions
---------------------------------------------
SELECT
    RegionID as RegionID,
    (
        ISNULL( ISNULL( ISNULL(
                    (SELECT [Content].DataStr FROM dbo.[Content] WHERE [Content].eid = #Regions.RegionID AND [Content].Lang = 0),
                    (SELECT [Content].DataStr FROM dbo.[Content] WHERE [Content].eid = #Regions.RegionID AND [Content].Lang = 1)),
                (SELECT [Content].DataStr FROM dbo.[Content] WHERE [Content].eid = #Regions.RegionID AND [Content].Lang = 2)),
            (SELECT TOP 1 [Content].DataStr FROM dbo.[Content] WHERE [Content].eid = #Regions.RegionID AND NOT ([Content].DataStr IS NULL)) )
    ) as [Name]
FROM
    #Regions

END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[SetObjectRegion]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SetObjectRegion]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SetObjectRegion]
	(
	@objectID uniqueidentifier,
	@regionID uniqueidentifier
	)
AS
BEGIN

    SET NOCOUNT ON
    
    IF @regionID IS NULL
        BEGIN

            DELETE FROM ObjectRegions
            WHERE ObjectID = @objectID

        END
        ELSE
        BEGIN
        
            IF EXISTS(SELECT ObjectRegions.ObjectID FROM dbo.ObjectRegions WHERE ObjectRegions.ObjectID = @objectID)
                UPDATE ObjectRegions
                SET ObjectRegions.RegionID = @regionID
            ELSE
                INSERT ObjectRegions(ObjectID,RegionID)
                VALUES(@objectID,@regionID)   
       
        END
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[dceaccess_CreateUser]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dceaccess_CreateUser]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dceaccess_CreateUser]
(
	@login nvarchar(255)
)
AS

BEGIN
	SET NOCOUNT ON;
	
	DECLARE
	    @studentID uniqueidentifier
	    
	SELECT @studentID = newid()

    INSERT INTO dbo.Students( [ID], Login, FirstName, Patronymic, LastName )
    VALUES( @studentID, @login, ''*'', ''*'', @login )

	
	RETURN
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[dcetools_Access_Roles]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Access_Roles]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dcetools_Access_Roles]
AS

BEGIN
	SET NOCOUNT ON;

    SELECT
        Content.eid as [ID],
        Content.DataStr as [Name]
        
    FROM
        dbo.Content
        
    WHERE
        eid IN (
            ''AAAAAAAA-D9B7-42E1-B6A1-FFFFB0E29B01'',
            ''AAAAAAAA-D9B7-42E1-B6A1-FFFFB0E29B02'',
            ''AAAAAAAA-D9B7-42E1-B6A1-FFFFB0E29B03'',
            ''AAAAAAAA-D9B7-42E1-B6A1-FFFFB0E29B04'',
            ''AAAAAAAA-D9B7-42E1-B6A1-FFFFB0E29B05'')
	
	RETURN
END' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[dcetools_Fn_Content_GetText]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Fn_Content_GetText]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE FUNCTION [dbo].[dcetools_Fn_Content_GetText]
( @contentID uniqueidentifier )
RETURNS nvarchar(max)
AS

BEGIN
	RETURN        
	    ISNULL( ISNULL( ISNULL(
                    (SELECT TOP 1 [Content].TData FROM dbo.[Content] WHERE [Content].eid = @contentID AND [Content].Lang = 0),
                    (SELECT TOP 1 [Content].TData FROM dbo.[Content] WHERE [Content].eid = @contentID AND [Content].Lang = 1)),
                (SELECT TOP 1 [Content].TData FROM dbo.[Content] WHERE [Content].eid = @contentID AND [Content].Lang = 2)),
            (SELECT TOP 1 [Content].TData FROM dbo.[Content] WHERE [Content].eid = @contentID AND NOT ([Content].TData IS NULL)) )

END' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[dcetools_Fn_Content_GetImage]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Fn_Content_GetImage]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE FUNCTION [dbo].[dcetools_Fn_Content_GetImage]
( @contentID uniqueidentifier )
RETURNS varbinary(max)
AS

BEGIN
	RETURN        
	    ISNULL( ISNULL( ISNULL(
                    (SELECT TOP 1 [Content].Data FROM dbo.[Content] WHERE [Content].eid = @contentID AND [Content].Lang = 0),
                    (SELECT TOP 1 [Content].Data FROM dbo.[Content] WHERE [Content].eid = @contentID AND [Content].Lang = 1)),
                (SELECT TOP 1 [Content].Data FROM dbo.[Content] WHERE [Content].eid = @contentID AND [Content].Lang = 2)),
            (SELECT TOP 1 [Content].Data FROM dbo.[Content] WHERE [Content].eid = @contentID AND NOT ([Content].Data IS NULL)) )

END' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[dcetools_Fn_Content_GetString]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Fn_Content_GetString]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'
CREATE FUNCTION [dbo].[dcetools_Fn_Content_GetString]
( @contentID uniqueidentifier )
RETURNS nvarchar(255)
AS

BEGIN
	RETURN        
	    ISNULL( ISNULL( ISNULL(
                    (SELECT TOP 1 [Content].DataStr FROM dbo.[Content] WHERE [Content].eid = @contentID AND [Content].Lang = 0),
                    (SELECT TOP 1 [Content].DataStr FROM dbo.[Content] WHERE [Content].eid = @contentID AND [Content].Lang = 1)),
                (SELECT TOP 1 [Content].DataStr FROM dbo.[Content] WHERE [Content].eid = @contentID AND [Content].Lang = 2)),
            (SELECT TOP 1 [Content].DataStr FROM dbo.[Content] WHERE [Content].eid = @contentID AND NOT ([Content].DataStr IS NULL)) )

END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[dcetools_Content_SetText]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Content_SetText]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dcetools_Content_SetText]
( @contentID uniqueidentifier, @text nvarchar(max) )
AS

BEGIN
    
    IF EXISTS(
        SELECT ID
        FROM dbo.Content
        WHERE eid=@contentID)
        
    BEGIN    
        UPDATE [Content]
        SET TData = @text
        WHERE eid = @contentID
    END    
    ELSE
    BEGIN
        INSERT INTO dbo.[Content] ( eid, [Type], Lang, TData )
        VALUES (@contentID, 2, 1, @text )
    END

END' 
END
GO
/****** Object:  StoredProcedure [dbo].[dcetools_Content_SetImage]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Content_SetImage]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dcetools_Content_SetImage]
( @contentID uniqueidentifier, @image varbinary(max) )
AS

BEGIN
    
    IF EXISTS(
        SELECT ID
        FROM dbo.Content
        WHERE eid=@contentID)
        
    BEGIN    
        UPDATE [Content]
        SET Data = @image
        WHERE eid = @contentID
    END    
    ELSE
    BEGIN
        INSERT INTO dbo.[Content] ( eid, [Type], Lang, Data )
        VALUES (@contentID, 4, 1, @image )    
    END

END' 
END
GO
/****** Object:  StoredProcedure [dbo].[dcetools_Content_SetString]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Content_SetString]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dcetools_Content_SetString]
( @contentID uniqueidentifier, @str nvarchar(255) )
AS

BEGIN
    
    IF EXISTS(
        SELECT ID
        FROM dbo.Content
        WHERE eid=@contentID)
        
    BEGIN    
        UPDATE [Content]
        SET DataStr = @str
        WHERE eid = @contentID
    END    
    ELSE
    BEGIN
        INSERT INTO dbo.[Content] ( eid, [Type], Lang, DataStr )
        VALUES (@contentID, 1, 1, @str )
    END

END' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[dcetools_Fn_CourseDomains_GetDomainParent]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Fn_CourseDomains_GetDomainParent]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE FUNCTION [dbo].[dcetools_Fn_CourseDomains_GetDomainParent]
(
	@domainID uniqueidentifier
)
RETURNS uniqueidentifier
AS

BEGIN
	RETURN
	    (SELECT
	        CourseDomain.Parent
	    FROM
	        dbo.CourseDomain
	    WHERE
	        CourseDomain.[ID] = @domainID)
END' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[isCourseOfArea]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[isCourseOfArea]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'
CREATE FUNCTION [dbo].[isCourseOfArea] (@cid uniqueidentifier, @areaid uniqueidentifier)  
RETURNS bit AS 
BEGIN 
declare @parentid uniqueidentifier

select @parentid = Area  from dbo.Courses where id=@cid

if (@parentid = @areaid)
	return 1

while (@parentid is not NULL)
begin

select @parentid = Parent  from dbo.CourseDomain where id=@parentid

if (@parentid = @areaid)
	return 1
end

return 0
END



' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[dcelegacy_Fn_Courses_AreaTopmostParentID]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcelegacy_Fn_Courses_AreaTopmostParentID]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE FUNCTION [dbo].[dcelegacy_Fn_Courses_AreaTopmostParentID]
(
	@areaID uniqueidentifier
)
RETURNS nvarchar(255)
AS
BEGIN

    DECLARE
        @areaParentID uniqueidentifier

    SELECT
        @areaParentID = CourseDomain.Parent
    FROM
        dbo.CourseDomain
    WHERE
        CourseDomain.[ID] = @areaID
        
    IF @areaParentID IS NULL
    BEGIN
        SELECT
            @areaParentID = @areaID
    END
    ELSE
    BEGIN
        SELECT
            @areaParentID = dbo.dcelegacy_Fn_Courses_AreaTopmostParentID(@areaParentID)
    END
    
    RETURN @areaParentID                            
END' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[isAreaHasCourses]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[isAreaHasCourses]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'
CREATE FUNCTION [dbo].[isAreaHasCourses] (@id uniqueidentifier)  
RETURNS bit AS 
BEGIN 
declare  @ccount int
set @ccount = (select count(*) from dbo.Courses where Area=@id and isReady=1 and CPublic=0)
if (@ccount > 0)
	return 1

set @ccount = (select SUM(CONVERT(int, dbo.isAreaHasCourses(id))) from dbo.CourseDomain where Parent=@id)

if (@ccount > 0)
	return 1
return 0
END




' 
END
GO
/****** Object:  StoredProcedure [dbo].[dceaccess_UpdateRoleName]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dceaccess_UpdateRoleName]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dceaccess_UpdateRoleName]
(
    @roleID uniqueidentifier,
    @roleName nvarchar(255)
)
AS

BEGIN
	SET NOCOUNT ON;
	
    IF NOT EXISTS(SELECT [ID] FROM dbo.Groups WHERE [ID]=@roleID)
    BEGIN

        DECLARE @noRoleErrorMessage nvarchar(255)
        SELECT @noRoleErrorMessage =
            ''No role found for ID ''+ISNULL(CAST(@roleID as nvarchar(255)),''(null)'')+''.''
                
        RAISERROR (@noRoleErrorMessage, 16, 1)
        ROLLBACK TRAN
                
    END
	
    UPDATE dbo.Groups
    SET
        [Name] = @roleName
    WHERE [ID]=@roleID
	
	RETURN
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[dcetools_Access_Groups_DELETE]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Access_Groups_DELETE]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dcetools_Access_Groups_DELETE]
(
    @id uniqueidentifier
)
AS

BEGIN
	SET NOCOUNT ON;

    DELETE FROM dbo.Groups
    WHERE [ID] = @id

	
	RETURN
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[dcetools_Access_Groups_INSERT]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Access_Groups_INSERT]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dcetools_Access_Groups_INSERT]
(
    @name nvarchar(255)
)
AS

BEGIN
	SET NOCOUNT ON;
	
	DECLARE
	    @id uniqueidentifier
	    
	SELECT
	    @id = newid()
	    
    INSERT Groups( [ID], [Name], Type )
    VALUES( @id, @name, 14 )
	
	RETURN
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[dcetools_Access_Groups_UPDATE]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Access_Groups_UPDATE]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dcetools_Access_Groups_UPDATE]
(
    @id uniqueidentifier,
    @name nvarchar(255)
)
AS

BEGIN
	SET NOCOUNT ON;
	
	UPDATE dbo.Groups
	SET
	    [Name] = @name
	WHERE
	    [ID] = @id

	RETURN
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[dcetools_Access_Users_GetAllGroups]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Access_Users_GetAllGroups]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dcetools_Access_Users_GetAllGroups]
AS

BEGIN
	SET NOCOUNT ON;
	
    SELECT
        Groups.[id] as [ID],
        Groups.[Name] as [Name],
        Groups.[Type] as [Type]
        
    FROM
        dbo.Groups
        
    WHERE
        Groups.Type>3
	
	RETURN
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[dcetools_Access_Users_GetGroupDetails]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Access_Users_GetGroupDetails]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dcetools_Access_Users_GetGroupDetails]
(
    @groupID uniqueidentifier
)
AS

BEGIN
	SET NOCOUNT ON;
	
    SELECT
        Groups.[id] as [ID],
        Groups.[Name] as [Name],
        Groups.[Type] as [Type]
        
    FROM
        dbo.Groups
        
    WHERE
        Groups.[id] = @groupID
	
	RETURN
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[dceaccess_GetAllRoles]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dceaccess_GetAllRoles]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dceaccess_GetAllRoles]
AS

BEGIN
	SET NOCOUNT ON;
	
    SELECT
        Groups.[id] as [ID],
        Groups.[Name] as [Name],
        Groups.[Type] as [Type]
        
    FROM
        dbo.Groups
	
	RETURN
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[dceaccess_GetRole]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dceaccess_GetRole]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dceaccess_GetRole]
(
    @roleID uniqueidentifier
)
AS

BEGIN
	SET NOCOUNT ON;
	
    SELECT
        Groups.[id] as [ID],
        Groups.[Name] as [Name],
        Groups.[Type] as [Type]
        
    FROM
        dbo.Groups        
        
    WHERE
        Groups.[ID] = @roleID 
	
	RETURN
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[dceaccess_CreateRole]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dceaccess_CreateRole]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dceaccess_CreateRole]
(
    @roleID uniqueidentifier,
    @roleName nvarchar(255),
    @type int
)
AS

BEGIN
	SET NOCOUNT ON;
	
    INSERT INTO dbo.Groups( [ID], [Name], [Type] )
    VALUES( @roleID, @roleName, @type )

	
	RETURN
END' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[TargetLang]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TargetLang]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'
CREATE FUNCTION [dbo].[TargetLang] (@id uniqueidentifier, @lang nchar(3), @deflang nchar(3))  
RETURNS nvarchar(255) AS  
BEGIN 
	DECLARE @strr nchar(3) 
	SET @strr = (SELECT c.DataStr from dbo.Content c, Languages L where eid=@id and L.id=c.Lang and L.Abbr=@lang)
	IF (@strr = NULL or @strr='''')
		return @deflang
	return @lang
END



' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[GetContentAlt]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetContentAlt]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'
CREATE FUNCTION [dbo].[GetContentAlt] (@id uniqueidentifier, @lang nchar(3), @deflang nchar(3))  
RETURNS nvarchar(4000) AS  
BEGIN 
	DECLARE @strr nvarchar(4000) 
	SELECT top 1 @strr=
	case
		when Type=2 or Type=6 or Type=8 then CAST(c.TData  as nvarchar(4000) )
		else DataStr
	end
	from dbo.Content c, Languages L where eid=@id and L.id=c.Lang and L.Abbr=@lang

	IF (@strr = NULL or @strr='''')
		SELECT top 1 @strr=
		case
			when Type=2 or Type=6 or Type=8 then CAST(c.TData  as nvarchar(4000) )
			else c.DataStr
		end
		 from dbo.Content c, Languages L where eid=@id and L.id=c.Lang and L.Abbr=@deflang
	return @strr
END



' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[GetTDataContentAlt]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetTDataContentAlt]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'
CREATE FUNCTION [dbo].[GetTDataContentAlt] (@id uniqueidentifier, @lang nchar(3), @deflang nchar(3))  
RETURNS  nvarchar(4000) AS  
BEGIN 

	DECLARE @strr nvarchar(4000) 
	SET @strr = ( SELECT top 1 CAST(c.TData  as nvarchar(4000) ) from dbo.Content c, Languages L where eid=@id and L.id=c.Lang and L.Abbr=@lang)
	IF (@strr = NULL or @strr='''')
		SET @strr = (SELECT top 1  CAST(c.TData as nvarchar(4000) ) from dbo.Content c, Languages L where eid=@id and L.id=c.Lang and L.Abbr=@deflang)
	return @strr
END






' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[GetTContentId]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetTContentId]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'

CREATE FUNCTION [dbo].[GetTContentId] (@id uniqueidentifier, @lang
nchar(3), @deflang nchar(3))  
RETURNS  uniqueidentifier AS  
BEGIN 
	DECLARE @strr nvarchar(4000) 
	DECLARE @cid uniqueidentifier
	SELECT top 1 @strr=CAST(c.TData  as nvarchar(4000) ),  @cid=c.id
from dbo.Content c, Languages L where eid=@id and L.id=c.Lang and
L.Abbr=@lang
	IF (@strr = NULL or @strr='''')
		SELECT top 1 @strr=CAST(c.TData  as nvarchar(4000) ),
@cid=c.id from dbo.Content c, Languages L where eid=@id and L.id=c.Lang
and L.Abbr=@deflang
	return @cid
END

' 
END
GO
/****** Object:  StoredProcedure [dbo].[CreateNewsItem]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CreateNewsItem]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[CreateNewsItem]
	(
	    @newsID uniqueidentifier,
	    @newsRegion uniqueidentifier,
	    @newsDate datetime,
	    @head varchar(250),
	    @short varchar(250),
	    @textData ntext,
	    @moreText ntext,
	    @imageData image,
	    @moreHRef varchar(250)
	)
AS

BEGIN
	SET NOCOUNT ON
	RETURN

DECLARE
    @courseCode uniqueidentifier
    
SELECT @courseCode = NULL    
	
DECLARE
    @headContentID uniqueidentifier,	
    @shortContentID uniqueidentifier,
    @textContentID uniqueidentifier,
    @moreTextContentID uniqueidentifier,
    @imageContentID uniqueidentifier

    
SELECT
    @headContentID = newid(),
    @shortContentID = newid(),
    @textContentID = newid(),
    @moreTextContentID = newid(),
    @imageContentID = newid()
    
INSERT INTO dbo.[Content] ( eid, [Type], Lang, DataStr )
VALUES (@headContentID, 1, 1, @head )
        
INSERT INTO dbo.[Content] ( eid, [Type], Lang, DataStr )
VALUES (@shortContentID, 1, 1, @short )

INSERT INTO dbo.[Content] ( eid, [Type], Lang, DataStr )
VALUES (@textContentID, 2, 1, @textData )

INSERT INTO dbo.[Content] ( eid, [Type], Lang, TData )
VALUES (@moreTextContentID, 2, 1, @moreText )

INSERT INTO dbo.[Content] ( eid, [Type], Lang, Data )
VALUES (@imageContentID, 4, 1, @imageData )
	
INSERT INTO dbo.[News]
    (
        [id],
        
        [NewsDate],
        
        [Head],
        [Short],
        [Text],
        [MoreHRef],
        [MoreText],
        
        [CourseCode],
        
        [Image]
    )
VALUES
    (
        @newsID,
        @newsDate,
        @headContentID,
        @shortContentID,
        @textContentID,
        @moreHRef,
        @moreTextContentID,
        
        @courseCode,
        
        @imageContentID        
    )    	
	
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateNewsItem]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UpdateNewsItem]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[UpdateNewsItem]
	(
	    @newsID uniqueidentifier,
	    @newsDate datetime,
	    @head varchar(250),
	    @short varchar(250),
	    @textData ntext,
	    @moreText ntext,
	    @imageData image,
	    @moreHRef varchar(250)
	)
AS

BEGIN
	SET NOCOUNT ON
	
	
DECLARE
    @headContentID uniqueidentifier,	
    @shortContentID uniqueidentifier,
    @textContentID uniqueidentifier,
    @moreTextContentID uniqueidentifier,
    @imageContentID uniqueidentifier

SELECT
    @headContentID = News.[Head],
    @shortContentID = News.[Short],
    @textContentID = News.[Text],
    @moreTextContentID = News.[MoreText],
    @imageContentID = News.[Image]
FROM
    [News]
WHERE
    News.[id] = @newsID    
	
	
UPDATE [Content]
SET DataStr = @head
WHERE eid = @headContentID
	
UPDATE [Content]
SET DataStr = @short
WHERE eid = @shortContentID

UPDATE [Content]
SET TData = @textData
WHERE eid = @textContentID

UPDATE [Content]
SET TData = @moreText
WHERE eid = @moreTextContentID

UPDATE [Content]
SET Data = @imageData
WHERE eid = @imageContentID

UPDATE [News]
SET
    NewsDate = @newsDate,
    MoreHRef = @moreHRef
WHERE
    [id] = @newsID


	RETURN
END' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[CourseOfTest]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CourseOfTest]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'
CREATE FUNCTION [dbo].[CourseOfTest] (@id uniqueidentifier)  
RETURNS uniqueidentifier AS  
BEGIN 

declare @Course uniqueidentifier
declare @Parent uniqueidentifier

-- определение родителя теста
set @Parent = (select Parent from Tests where id = @id)

-- поиск родителя среди всех курсов
set @Course = (select id from Courses where id = @Parent)

-- если родитель является не курсом
if (@Course is null)
begin
	-- поиск родителя среди всех тем (темы, практические работы)	
	set @Parent = (select id from Themes where id = @Parent)
	-- определение курса темы
	set @Course = (dbo.CourseOfTheme(@Parent))
end

return @Course

END




' 
END
GO
/****** Object:  StoredProcedure [dbo].[MakeTestCopy]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MakeTestCopy]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[MakeTestCopy]
	@newid uniqueidentifier,
	@oldid uniqueidentifier,
	@newParentid uniqueidentifier

as

---------------------------------------------------------
-- Создание новой записи теста --
---------------------------------------------------------

insert into dbo.Tests select @newid, Type, @newParentid, Duration, Points, Show, Split, AutoFinish, 
DefLanguage, CanSwitchLang, Hints, InternalName, ShowThemes from Tests where id = @oldid

declare Questions_Cursor cursor local for 
select id from dbo.TestQuestions where Test = @oldid 
order by QOrder /* important for save order*/

open Questions_Cursor

declare @oldQuestionId uniqueidentifier
declare @newQuestionId uniqueidentifier

fetch next from Questions_Cursor into @oldQuestionId
while (@@FETCH_STATUS <> -1)
begin
	if (@@FETCH_STATUS <> -2)
	begin
		set @newQuestionId = newid()
		execute MakeQuestionCopy @newQuestionId, @oldQuestionId, @newid
	end

	fetch next from Questions_Cursor into @oldQuestionId
end

close Questions_Cursor
deallocate Questions_Cursor
' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[dcetools_Fn_Themes_GetThemeName]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Fn_Themes_GetThemeName]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE FUNCTION [dbo].[dcetools_Fn_Themes_GetThemeName]
(
    @themeID uniqueidentifier
)  
RETURNS nvarchar(255) AS  

BEGIN 

    RETURN
        (SELECT
            dbo.dcetools_Fn_Content_GetString( Themes.[Name] ) as ThemeName
        FROM
            dbo.Themes
        WHERE
            Themes.[ID] = @themeID)
            
END

' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[AllTrainingStudents]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AllTrainingStudents]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'
CREATE FUNCTION [dbo].[AllTrainingStudents] (@id uniqueidentifier)  
RETURNS @members TABLE (mid uniqueidentifier, id uniqueidentifier, Type int, MGroup uniqueidentifier, name nvarchar(100)) AS  
BEGIN 

DECLARE @students uniqueidentifier

SET @students = (SELECT Students FROM dbo.Trainings where id =@id)

/* getting subgroups items */
INSERT @members SELECT * from AllGroupMembers(@students)

/* getting all student groups, which belong to tracks, for training belonging to same tracks */

DECLARE cur cursor 
FOR  SELECT sg.id , sg.Name from Tracks t, GroupMembers gm, Groups sg
where
  gm.id = @id and gm.MGroup = t.Trainings and sg.id = t.Students

OPEN cur

DECLARE @gid uniqueidentifier
DECLARE @groupName nvarchar(100)

FETCH NEXT FROM cur INTO @gid, @groupName
WHILE @@FETCH_STATUS <> -1
BEGIN
	IF (@@FETCH_STATUS = 0)
	BEGIN
/* inserting track group */
		INSERT @members VALUES(NULL, @gid, 10, NULL, @groupName)
/* inserting track students */
		INSERT @members SELECT * from AllGroupMembers(@gid)
	END
	FETCH NEXT FROM cur INTO @gid,@groupName
END
CLOSE cur
DEALLOCATE cur

return 
END

' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[AllStudentTrainings]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AllStudentTrainings]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'
CREATE FUNCTION [dbo].[AllStudentTrainings] (@id uniqueidentifier)  
RETURNS @members TABLE (id uniqueidentifier) AS  
BEGIN 

DECLARE @students uniqueidentifier

SET @students = (SELECT Students FROM dbo.Trainings where id =@id)

/* track trainings */
INSERT @members SELECT DISTINCT gm.id from GroupMembers gm , Tracks t where
t.Trainings = gm.MGroup and @id in (select id from GroupMembers where MGroup = t.Students)

/*INSERT @members SELECT a.id from Trainings t INNER JOIN  AllGroupMembers(t.Students) as a */

/* getting all student groups, which belong to trainings */
DECLARE cur cursor 
FOR  SELECT id, Students from Trainings
OPEN cur

DECLARE @gid uniqueidentifier
DECLARE @tid uniqueidentifier
DECLARE @cnt int

FETCH NEXT FROM cur INTO @tid,@gid
WHILE @@FETCH_STATUS <> -1
BEGIN
	IF (@@FETCH_STATUS = 0)
	BEGIN
		SET @cnt = (SELECT COUNT(*) from AllGroupMembers(@gid) where id = @id)
		IF (@cnt >0)
			INSERT @members values(@tid)
	END
	FETCH NEXT FROM cur INTO @tid,@gid
END
CLOSE cur
DEALLOCATE cur

return 
END


' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[dcetools_Fn_Trainings_GetTrainingName]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Fn_Trainings_GetTrainingName]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE FUNCTION [dbo].[dcetools_Fn_Trainings_GetTrainingName]
(
	@trainingID uniqueidentifier
)
RETURNS TABLE
AS
	
RETURN
	    SELECT
	        dbo.dcetools_Fn_Content_GetString(Trainings.Name) as [Name]
	    FROM
            dbo.Trainings
        WHERE
            Trainings.[ID] = @trainingID 
' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[TrainingTests_TrainingsFromTest]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TrainingTests_TrainingsFromTest]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE FUNCTION [dbo].[TrainingTests_TrainingsFromTest] 
(
	@testID uniqueidentifier
)
RETURNS TABLE
AS
	RETURN
	    SELECT *
	    FROM dbo.Trainings
	    WHERE EXISTS(SELECT * FROM TrainingTests(NULL))
' 
END
GO
/****** Object:  StoredProcedure [dbo].[dcelegacy_Trainings_GetDetails]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcelegacy_Trainings_GetDetails]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dcelegacy_Trainings_GetDetails]
(
	@studentID uniqueidentifier,
	@trainingID uniqueidentifier
)
AS

BEGIN
	SET NOCOUNT ON;

    IF dbo.dcetools_Fn_Trainings_Students_IsTrainingContainsStudent(
            @trainingID,
            @studentID)=0
    BEGIN
        
        DECLARE @studentNotInTrainingErrorMessage nvarchar(255)
        SELECT @studentNotInTrainingErrorMessage =
            ''Student not subscribed to this Training.''
                
        RAISERROR (@studentNotInTrainingErrorMessage, 16, 1)
        ROLLBACK TRAN
        
    END
        
    SELECT
        Trainings.ID as [ID],
        Trainings.Code as Code,
        dbo.dcetools_Fn_Content_GetString(Trainings.Name) as [Name],
        
        dbo.dcetools_Fn_Content_GetText(Trainings.Comment) as Comments,
        
        Trainings.Course as CourseID,
        (SELECT dbo.dcetools_Fn_Content_GetString(Courses.[Name])
        FROM dbo.Courses
        WHERE Courses.[ID] = Trainings.Course) as CourseName,
        
        Trainings.StartDate as StartDate,
        Trainings.EndDate as EndDate,
        
        Trainings.isPublic as IsPublic,
        Trainings.isActive as IsActive,
        Trainings.TestOnly as TestOnly,
        Trainings.Expires as Expires,        

        dbo.dcetools_Fn_Regions_GetObjectRegionID( Trainings.[ID] ) as RegionID,
        dbo.dcetools_Fn_Regions_GetObjectRegionName( Trainings.[ID] ) as RegionName
        
    FROM
        dbo.Trainings
        
    WHERE
        Trainings.ID = @trainingID

	
	RETURN
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[dcelegacy_Trainings_GetSubscribedTrainingList]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcelegacy_Trainings_GetSubscribedTrainingList]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dcelegacy_Trainings_GetSubscribedTrainingList]
(
    @studentID uniqueidentifier
)
AS

BEGIN
	SET NOCOUNT ON;

    SELECT
        Trainings.ID as [ID],
        Trainings.Code as Code,
        dbo.dcetools_Fn_Content_GetString(Trainings.Name) as [Name],
        
        dbo.dcetools_Fn_Content_GetText(Trainings.Comment) as Comments,
        
        Trainings.Course as CourseID,
        (SELECT dbo.dcetools_Fn_Content_GetString(Courses.[Name])
        FROM dbo.Courses
        WHERE Courses.[ID] = Trainings.Course) as CourseName,
        
        Trainings.StartDate as StartDate,
        Trainings.EndDate as EndDate,
        
        Trainings.isPublic as IsPublic,
        Trainings.isActive as IsActive,
        Trainings.TestOnly as TestOnly,
        Trainings.Expires as Expires,        

        dbo.dcetools_Fn_Regions_GetObjectRegionID( Trainings.[ID] ) as RegionID,
        dbo.dcetools_Fn_Regions_GetObjectRegionName( Trainings.[ID] ) as RegionName
        
    FROM
        dbo.Trainings

    WHERE
        dbo.dcetools_Fn_Trainings_Students_IsTrainingContainsStudent(
            Trainings.[ID],
            @studentID) <>0
        
        
    ORDER BY
        Trainings.StartDate DESC, Trainings.EndDate DESC

            
            	
	RETURN
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[dcelegacy_Trainings_GetSchedule]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcelegacy_Trainings_GetSchedule]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dcelegacy_Trainings_GetSchedule]
AS

BEGIN
	SET NOCOUNT ON;

    SELECT
        dbo.dcetools_Fn_Content_GetString(Trainings.[Name]) as [Name],
        Trainings.isActive,
        (SELECT MIN(Schedule.StartDate)
        FROM dbo.Schedule
        WHERE Schedule.Training=Trainings.id) as StartDate,
               
        (SELECT MAX(Schedule.EndDate)
        FROM dbo.Schedule
        WHERE Schedule.Training=Trainings.id) as EndDate
        
    FROM
        dbo.Trainings,
        dbo.Courses
        
    INNER JOIN
        dbo.Languages
    
    ON
        Languages.[id]=Courses.CourseLanguage
        
    WHERE
        Trainings.Course=Courses.[id]
    AND
        Trainings.isActive=1
    AND
        Trainings.Expires=0
    AND
        (SELECT MIN(Schedule.StartDate) FROM Schedule WHERE Schedule.Training=Trainings.id)
            > {fn NOW()}
            
    ORDER BY
        Courses.Type
            
            	
	RETURN
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[dcetools_Trainings_Tests_GetTestResultDetails]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Trainings_Tests_GetTestResultDetails]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dcetools_Trainings_Tests_GetTestResultDetails]
(
	@homeRegion uniqueidentifier,
	@testID as uniqueidentifier
)
AS

BEGIN
	SET NOCOUNT ON;
	
    SELECT
        TestAnswers.[ID] as AnswerID,
        TestAnswers.Answer as AnswerXml,
        TestAnswers.AnswerTime as AnswerTime,
        TestAnswers.Points as AnswerPoints,
        TestAnswers.Question as QuestionID,
        dbo.dcetools_Fn_Content_GetText(TestQuestions.[Content]) as QuestionText,
        
        dbo.dcetools_Fn_Students_GetStudentFullName(TestResults.Student) as StudentName
        

    FROM
        dbo.TestAnswers,
        dbo.TestQuestions,
        dbo.TestResults

    WHERE
        TestAnswers.Question = TestQuestions.id
    AND
        TestAnswers.TestResults = TestResults.id
    AND
        TestResults.Test=TestQuestions.Test
    AND
        TestResults.[id]=@testID

    ORDER BY TestAnswers.AnswerTime ASC
	
	
	RETURN
END' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[dcereports_Fn_Tests_GetStudentTestResults]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcereports_Fn_Tests_GetStudentTestResults]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE FUNCTION [dbo].[dcereports_Fn_Tests_GetStudentTestResults]
(
	@studentID uniqueidentifier
)
RETURNS TABLE
AS
	
RETURN
	    SELECT
	        TestResults.*,
	        Users.Comments,
	        Users.FullName,
	        Users.JobPosition,
	        Users.[ID] as UserID

        FROM
            dbo.TestResults,
            dbo.Users

        WHERE
            @studentID IS NULL OR TestResults.Student = @studentID
        AND
        ( @studentID IS NULL OR Users.[ID] = @studentID )
        AND
        ( TestResults.Student = Users.[ID] )            ' 
END
GO
/****** Object:  StoredProcedure [dbo].[dceaccess_GetAllUsers]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dceaccess_GetAllUsers]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dceaccess_GetAllUsers]
AS

BEGIN
	SET NOCOUNT ON;

    SELECT
        Users.[ID] as [ID],
        Users.Login as Login,
        Users.FirstName as FirstName,
        Users.Patronymic as Patronymic,
        Users.LastName as LastName,
        Users.Email as EMail,
        Users.JobPosition as JobPosition,
        Users.Comments as Comments,
        
        Users.CreateDate as CreateDate,
        Users.LastModifyDate as LastModify,
        
        dbo.dcetools_Fn_Regions_GetObjectRegionID( Users.[ID] ) as RegionID,
        dbo.dcetools_Fn_Regions_GetObjectRegionName( Users.[ID] ) as RegionName,
        
        (SELECT UserRoles.[Role] FROM dbo.UserRoles WHERE UserRoles.UserID = Users.[ID])
            as RoleID,
        (SELECT dbo.dcetools_Fn_Content_GetString(UserRoles.[Role]) FROM dbo.UserRoles WHERE UserRoles.UserID = Users.[ID])
            as RoleName
        
    FROM
        dbo.Users
	
	RETURN
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[dceaccess_GetUserByID]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dceaccess_GetUserByID]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dceaccess_GetUserByID]
(
	@userID uniqueidentifier
)
AS

BEGIN
	SET NOCOUNT ON;

    SELECT
        Users.[ID] as [ID],
        Users.Login as Login,
        Users.FirstName as FirstName,
        Users.Patronymic as Patronymic,
        Users.LastName as LastName,
        Users.Email as EMail,
        Users.JobPosition as JobPosition,
        Users.Comments as Comments,
        
        Users.CreateDate as CreateDate,
        Users.LastModifyDate as LastModify,
        
        dbo.dcetools_Fn_Regions_GetObjectRegionID( Users.[ID] ) as RegionID,
        dbo.dcetools_Fn_Regions_GetObjectRegionName( Users.[ID] ) as RegionName,
        
        (SELECT UserRoles.[Role] FROM dbo.UserRoles WHERE UserRoles.UserID = Users.[ID])
            as RoleID,
        (SELECT dbo.dcetools_Fn_Content_GetString(UserRoles.[Role]) FROM dbo.UserRoles WHERE UserRoles.UserID = Users.[ID])
            as RoleName
        
    FROM
        dbo.Users
        
    WHERE
        Users.[ID] = @userID
	
	RETURN
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[dceaccess_GetUserByLogin]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dceaccess_GetUserByLogin]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dceaccess_GetUserByLogin]
(
	@login nvarchar(255)
)
AS

BEGIN
	SET NOCOUNT ON;

    SELECT
        Users.[ID] as [ID],
        Users.Login as Login,
        Users.FirstName as FirstName,
        Users.Patronymic as Patronymic,
        Users.LastName as LastName,
        Users.Email as EMail,
        Users.JobPosition as JobPosition,
        Users.Comments as Comments,
        
        Users.CreateDate as CreateDate,
        Users.LastModifyDate as LastModify,
        
        dbo.dcetools_Fn_Regions_GetObjectRegionID( Users.[ID] ) as RegionID,
        dbo.dcetools_Fn_Regions_GetObjectRegionName( Users.[ID] ) as RegionName,
        
        (SELECT UserRoles.[Role] FROM dbo.UserRoles WHERE UserRoles.UserID = Users.[ID])
            as RoleID,
        (SELECT dbo.dcetools_Fn_Content_GetString(UserRoles.[Role]) FROM dbo.UserRoles WHERE UserRoles.UserID = Users.[ID])
            as RoleName
        
    FROM
        dbo.Users
        
    WHERE
        Users.Login = @login
	
	RETURN
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[dcetools_Access_Users_FindUsers]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Access_Users_FindUsers]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dcetools_Access_Users_FindUsers]
(
    @homeRegion uniqueidentifier,
    @searchString nvarchar(255)
)
AS

BEGIN
	SET NOCOUNT ON;

    SELECT
        Users.[ID] as [ID],
        Users.Login as Login,
        Users.FirstName as FirstName,
        Users.Patronymic as Patronymic,
        Users.LastName as LastName,
        Users.Email as EMail,
        Users.JobPosition as JobPosition,
        Users.Comments as Comments,
        
        Users.CreateDate as CreateDate,
        Users.LastModifyDate as LastModify,
        
        dbo.dcetools_Fn_Regions_GetObjectRegionName( Users.[ID] ) as RegionName,
        
        (SELECT dbo.dcetools_Fn_Content_GetString(UserRoles.[Role])
        FROM dbo.UserRoles
        WHERE UserRoles.UserID = Users.[ID]) as RoleName,
        
        Users.FullName
        
    FROM
        dbo.Users
        
    WHERE
    (
        @homeRegion IS NULL
        OR
        dbo.dcetools_Fn_Regions_GetObjectRegionID(Users.[ID]) = @homeRegion
    )
    AND
    (    
        @searchString IS NULL
    OR
        LTrim(RTrim(@searchString)) = ''''
        
    OR
    
        ISNULL(Users.Login,'''') LIKE ''%''+@searchString+''%''
    OR
        ISNULL(dbo.dcetools_Fn_Users_GetUserFullName(Users.[ID]),'''') LIKE ''%''+@searchString+''%''
    OR
        ISNULL(Users.EMail,'''') LIKE ''%''+@searchString+''%''
    OR
        ISNULL(Users.Comments,'''') LIKE ''%''+@searchString+''%''
    OR
        ISNULL(dbo.dcetools_Fn_Regions_GetObjectRegionName( Users.[ID] ),'''') LIKE ''%''+@searchString+''%''
    OR
        ISNULL((SELECT dbo.dcetools_Fn_Content_GetString(UserRoles.[Role])
        FROM dbo.UserRoles
        WHERE UserRoles.UserID = Users.[ID]),'''') LIKE ''%''+@searchString+''%''
    )
        
    ORDER BY

        CASE
            WHEN
                ISNULL(dbo.dcetools_Fn_Users_GetUserFullName(Users.[ID]),'''') = @searchString
            THEN 1
            ELSE 0
        END DESC,

        CASE
            WHEN
                ISNULL(Users.EMail,'''') = @searchString
            THEN 1
            ELSE 0
        END DESC,

        CASE
            WHEN
                ISNULL(dbo.dcetools_Fn_Users_GetUserFullName(Users.[ID]),'''') LIKE ''%''+@searchString+''%''
            THEN 1
            ELSE 0
        END DESC,

        CASE
            WHEN
                ISNULL(Users.EMail,'''') LIKE ''%''+@searchString+''%''
            THEN 1
            ELSE 0
        END DESC,
        
        ISNULL(Users.FirstName,''''),
        ISNULL(Users.Patronymic,''''),
        ISNULL(Users.LastName,''''),
        ISNULL(Users.Login,'''')
        
	
	RETURN
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[dcetools_Regions_GetList_ByPolicy]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Regions_GetList_ByPolicy]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dcetools_Regions_GetList_ByPolicy]
(
    @homeRegion uniqueidentifier
)
AS

BEGIN
	SET NOCOUNT ON;
	
SELECT *
INTO #Regions
FROM dbo.dcetools_Fn_Regions_GetIdNameList ()
WHERE
    @homeRegion IS NULL
OR
    ID = @homeRegion

INSERT INTO #Regions([ID], RegionName)
VALUES(NULL,''(глобальний)'')

SELECT *
FROM #Regions
ORDER BY RegionName
	
	RETURN
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[dcetools_Regions_GetList]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Regions_GetList]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dcetools_Regions_GetList]
AS

BEGIN
	SET NOCOUNT ON;

SELECT *
INTO #Regions
FROM dbo.dcetools_Fn_Regions_GetIdNameList ()

INSERT INTO #Regions([ID], RegionName)
VALUES(NULL,''(глобальний)'')

SELECT *
FROM #Regions
ORDER BY RegionName
	
	RETURN
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[dcetools_Access_GetCanObjectID_Read]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Access_GetCanObjectID_Read]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dcetools_Access_GetCanObjectID_Read]
(
	@homeRegion uniqueidentifier,
	@objectID uniqueidentifier
)
AS

BEGIN
	SET NOCOUNT ON;

    DECLARE
        @objectRegionID uniqueidentifier
        
    SELECT @objectRegionID = dbo.dcetools_Fn_Regions_GetObjectRegionID(@objectID)

    SELECT dbo.dcetools_Fn_Regions_RegionCanReadByHome(@objectRegionID,@homeRegion)        	
	
	RETURN
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[dcetools_Access_CheckObjectRegionID_Read]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Access_CheckObjectRegionID_Read]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dcetools_Access_CheckObjectRegionID_Read]
(
	@homeRegion uniqueidentifier,
	@objectRegionID uniqueidentifier
)
AS

BEGIN
	SET NOCOUNT ON;

    IF 
        dbo.dcetools_Fn_Regions_RegionCanReadByHome(@objectRegionID,@homeRegion)
            = 0
    BEGIN

        DECLARE @regionAccessDeniedErrorMessage nvarchar(255)
        SELECT @regionAccessDeniedErrorMessage =
            ''Region READ access denied: home region '' +
            ISNULL(
                CAST( @homeRegion as nvarchar(255))+
                ISNULL( ''-'' + dbo.dcetools_Fn_Regions_GetName(@homeRegion), '''' ),
                ''(null)'')+
            '' object region ''+
            ISNULL(CAST( @objectRegionID as nvarchar(255) ),''(null)'')+''.''
                
        RAISERROR (@regionAccessDeniedErrorMessage, 16, 1)
        ROLLBACK TRAN
                
    END
        	
	
	RETURN
END' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[dcetools_Fn_Regions_ObjectCanReadByHome]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Fn_Regions_ObjectCanReadByHome]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE FUNCTION [dbo].[dcetools_Fn_Regions_ObjectCanReadByHome]
(
	@objectID uniqueidentifier,
	@homeRegion uniqueidentifier
)
RETURNS bit
AS

BEGIN
	RETURN
	        dbo.dcetools_Fn_Regions_RegionCanReadByHome(
	            dbo.dcetools_Fn_Regions_GetObjectRegionID(@objectID), @homeRegion)
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[dcelegacy_News_GetList]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcelegacy_News_GetList]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dcelegacy_News_GetList]
(
	@homeRegion uniqueidentifier
)
AS

BEGIN
	SET NOCOUNT ON;

    SELECT
        News.[ID] as [ID], 
        News.NewsDate as [Date], 
        dbo.dcetools_Fn_Content_GetString(News.Head) as Title,
        dbo.dcetools_Fn_Content_GetText(News.[Text]) as ContentText,
        
        dbo.dcetools_Fn_Content_GetImage(News.[Image]) as [Image],
        
        News.MoreHref as Href,
        
        dbo.dcetools_Fn_Regions_GetObjectRegionID( News.[ID] ) as RegionID,
        dbo.dcetools_Fn_Regions_GetObjectRegionName( News.[ID] ) as RegionName
        
    FROM
        dbo.News
        
    WHERE
        (@homeRegion IS NULL
    OR 
        dbo.dcetools_Fn_Regions_GetObjectRegionID( News.[ID] ) = @homeRegion)
	    
	    
	
	
	RETURN
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[dcetools_Access_GetCanObjectID_Write]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Access_GetCanObjectID_Write]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dcetools_Access_GetCanObjectID_Write]
(
	@homeRegion uniqueidentifier,
	@objectID uniqueidentifier
)
AS

BEGIN
	SET NOCOUNT ON;

    DECLARE
        @objectRegionID uniqueidentifier
        
    SELECT @objectRegionID = dbo.dcetools_Fn_Regions_GetObjectRegionID(@objectID)

    SELECT dbo.dcetools_Fn_Regions_RegionCanWriteByHome(@objectRegionID,@homeRegion)        	
	
	RETURN
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[dcetools_Access_CheckObjectRegionID_Write]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Access_CheckObjectRegionID_Write]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dcetools_Access_CheckObjectRegionID_Write]
(
	@homeRegion uniqueidentifier,
	@objectRegionID uniqueidentifier
)
AS

BEGIN
	SET NOCOUNT ON;

    IF 
        dbo.dcetools_Fn_Regions_RegionCanWriteByHome(@objectRegionID, @homeRegion)
            = 0
    BEGIN

        DECLARE @regionAccessDeniedErrorMessage nvarchar(255)
        SELECT @regionAccessDeniedErrorMessage =
            ''Region WRITE access denied: home region '' +
            ISNULL(
                CAST( @homeRegion as nvarchar(255))+
                ISNULL( ''-'' + dbo.dcetools_Fn_Regions_GetName(@homeRegion), '''' ),
                ''(null)'')+
            '' object region ''+
            ISNULL(CAST( @objectRegionID as nvarchar(255) ),''(null)'')+''.''
                
        RAISERROR (@regionAccessDeniedErrorMessage, 16, 1)
        ROLLBACK TRAN
                
    END
        	
	
	RETURN
END' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[dcetools_Fn_Regions_ObjectCanWriteByHome]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Fn_Regions_ObjectCanWriteByHome]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE FUNCTION [dbo].[dcetools_Fn_Regions_ObjectCanWriteByHome]
(
	@objectID uniqueidentifier,
	@homeRegion uniqueidentifier
)
RETURNS bit
AS

BEGIN
	RETURN
	        dbo.dcetools_Fn_Regions_RegionCanWriteByHome(
	            dbo.dcetools_Fn_Regions_GetObjectRegionID(@objectID), @homeRegion)
END' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[dcetools_Fn_Users_GetStudentOrUserFullName]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Fn_Users_GetStudentOrUserFullName]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE FUNCTION [dbo].[dcetools_Fn_Users_GetStudentOrUserFullName]
(
    @userID uniqueidentifier
)
RETURNS nvarchar(255)
AS

BEGIN
	RETURN
	    ISNULL(
	        dbo.dcetools_Fn_Students_GetStudentFullName(@userID),
	        dbo.dcetools_Fn_Users_GetUserFullName(@userID))
END' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[dcetools_Fn_Themes_IsThemeChildAnyDeep]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Fn_Themes_IsThemeChildAnyDeep]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE FUNCTION [dbo].[dcetools_Fn_Themes_IsThemeChildAnyDeep]
(
	@themeID uniqueidentifier,
	@parentCheckID uniqueidentifier
)
RETURNS bit
AS

BEGIN
    DECLARE
        @immediateParentID uniqueidentifier
        
    SELECT
        @immediateParentID = dbo.dcetools_Fn_Themes_GetThemeParent(@themeID)
        
    IF @immediateParentID IS NULL
        RETURN 0
   
    IF @immediateParentID = @parentCheckID
        RETURN 1

    RETURN
        dbo.dcetools_Fn_Themes_IsThemeChildAnyDeep(@immediateParentID, @parentCheckID)      
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateLdapUserExists]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UpdateLdapUserExists]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[UpdateLdapUserExists]
	(
	@login varchar(250),
	@regionCode nvarchar(250)
	)
AS
BEGIN

------------- Get region ID, generate new ID for new user/student

    DECLARE @regionID as uniqueidentifier
    IF @regionCode IS NULL
        SELECT @regionID = NULL
    ELSE
        SELECT @regionID = RegionCodes.RegionID
        FROM dbo.RegionCodes 
        WHERE RegionCodes.RegionCode = @regionCode


    DECLARE @newUserOrStudentID as uniqueidentifier
    SELECT @newUserOrStudentID  = NULL



---------- Students

    IF NOT EXISTS(SELECT Students.Login FROM dbo.Students WHERE Students.Login = @login)
    BEGIN

        SELECT @newUserOrStudentID = newid()

        INSERT dbo.Students(ID, Login, FirstName)
        VALUES(@newUserOrStudentID, @login, @login)
        
    END
    ELSE
    BEGIN

        DECLARE @studentID as uniqueidentifier
        SELECT @studentID = Students.ID
        FROM dbo.Students
        WHERE Students.Login = @login 
        
       exec dbo.SetObjectRegion @studentID, @regionID
        
    END
    
    
    
------------ Users    

    IF NOT EXISTS(SELECT Users.Login FROM dbo.Users WHERE Users.Login = @login)
    BEGIN

        SELECT @newUserOrStudentID = newid()

        INSERT dbo.Users(ID, Login, FirstName)
        VALUES(@newUserOrStudentID, @login, @login)
        
    END
    ELSE
    BEGIN

        DECLARE @userID as uniqueidentifier
        SELECT @userID = Users.ID
        FROM dbo.Users
        WHERE Users.Login = @login 
        
       exec dbo.SetObjectRegion @userID, @regionID
        
    END
    
    IF NOT (@newUserOrStudentID IS NULL)
        exec dbo.SetObjectRegion @newUserOrStudentID, @regionID

    RETURN
END' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[dcetools_Fn_CourseDomains_IsDomainChildAnyDeep]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Fn_CourseDomains_IsDomainChildAnyDeep]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE FUNCTION [dbo].[dcetools_Fn_CourseDomains_IsDomainChildAnyDeep]
(
	@domainID uniqueidentifier,
	@parentCheckID uniqueidentifier
)
RETURNS bit
AS

BEGIN
    DECLARE
        @immediateParentID uniqueidentifier
        
    SELECT
        @immediateParentID = dbo.dcetools_Fn_CourseDomains_GetDomainParent(@domainID)
        
    IF @immediateParentID IS NULL
        RETURN 0
   
    IF @immediateParentID = @parentCheckID
        RETURN 1

    RETURN
        dbo.dcetools_Fn_CourseDomains_IsDomainChildAnyDeep(@immediateParentID, @parentCheckID)      
END' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[GetStrContentA]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetStrContentA]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE FUNCTION [dbo].[GetStrContentA] (@id uniqueidentifier, @lang nchar(3))  
RETURNS nvarchar(255) AS  
BEGIN 
	return  dbo.dcetools_Fn_Content_GetString(@id)
END




' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[GetStrContentAlt2]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetStrContentAlt2]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE FUNCTION [dbo].[GetStrContentAlt2] (@id uniqueidentifier, @lang int, @deflang nchar(3))  
RETURNS nvarchar(255) AS  
BEGIN 
	return dbo.dcetools_Fn_Content_GetString(@id)
END










' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[GetStrContentAlt]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetStrContentAlt]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE FUNCTION [dbo].[GetStrContentAlt] (@id uniqueidentifier, @lang nchar(3), @deflang nchar(3))  
RETURNS nvarchar(255) AS  
BEGIN 
	return dbo.dcetools_Fn_Content_GetString(@id)
END

' 
END
GO
/****** Object:  StoredProcedure [dbo].[dcetools_Courses_GetTitles]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Courses_GetTitles]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dcetools_Courses_GetTitles]
(
	@homeRegion uniqueidentifier
)
AS

BEGIN
	SET NOCOUNT ON;
	
    SELECT
        Courses.ID as [ID],
        Courses.Code as Code,
        dbo.dcetools_Fn_Content_GetString(Courses.Name) as [Name]
        
    FROM
        dbo.Courses
        
    ORDER BY
        Courses.Code
	
	
	RETURN
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[dcetools_Courses_GetTitles_WithNULL]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Courses_GetTitles_WithNULL]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dcetools_Courses_GetTitles_WithNULL]
(
	@homeRegion uniqueidentifier
)
AS

BEGIN
	SET NOCOUNT ON;
	
    SELECT
        Courses.ID as [ID],
        Courses.Code as Code,
        dbo.dcetools_Fn_Content_GetString(Courses.Name) as [Name]
        
    INTO #Courses
    FROM
        dbo.Courses
        
    WHERE
        @homeRegion IS NULL
    OR 
        dbo.dcetools_Fn_Regions_GetObjectRegionID( Courses.[ID] ) = @homeRegion
        
    ORDER BY
        Courses.Code
	
	INSERT INTO #Courses([ID], Code, [Name])
	VALUES (NULL, ''(null)'',''(none)'')
	
	SELECT *
	FROM #Courses
	
	RETURN
END' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[GetStrContent]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetStrContent]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE FUNCTION [dbo].[GetStrContent] (@id uniqueidentifier, @lang int)  
RETURNS nvarchar(255) AS  
BEGIN 
    return dbo.dcetools_Fn_Content_GetString(@id)
END


' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[dcetools_Fn_Courses_GetCourseName]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Fn_Courses_GetCourseName]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE FUNCTION [dbo].[dcetools_Fn_Courses_GetCourseName] 
(
	@courseID uniqueidentifier
)
RETURNS nvarchar(255)
AS

BEGIN
	RETURN
	    (SELECT
	        dbo.dcetools_Fn_Content_GetString(Courses.[Name])
	    FROM
	        dbo.Courses
	    WHERE
	        Courses.[ID] = @courseID)
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[dcetools_Courses_GetCourseTypeTitles_WithNULL]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Courses_GetCourseTypeTitles_WithNULL]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dcetools_Courses_GetCourseTypeTitles_WithNULL]
AS

BEGIN
	SET NOCOUNT ON;
	
    SELECT
        CASE WHEN Host_Name()=''123123123123123123123123123123'' THEN NULL
        ELSE CourseType.ID END as [ID],
        dbo.dcetools_Fn_Content_GetString(CourseType.Name) as [Name]
        
    INTO #CourseTypes
    FROM
        dbo.CourseType
        
    ORDER BY
        CourseType.[Name]
	
	INSERT INTO #CourseTypes([ID], [Name])
	VALUES (NULL, ''(none)'')
	
	SELECT *
	FROM #CourseTypes
	
	RETURN
END' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[dcelegacy_Fn_Courses_GetAreaName]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcelegacy_Fn_Courses_GetAreaName]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE FUNCTION [dbo].[dcelegacy_Fn_Courses_GetAreaName]
(
    @areaID uniqueidentifier
)
RETURNS nvarchar(255)
AS 
BEGIN

    RETURN
        (SELECT
            dbo.dcetools_Fn_Content_GetString(CourseDomain.[Name]) as AreaName
        FROM
            dbo.CourseDomain
        WHERE
            CourseDomain.[ID] = @areaID)
END' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[dcelegacy_Fn_Courses_GetCourseAreaID]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcelegacy_Fn_Courses_GetCourseAreaID]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE FUNCTION [dbo].[dcelegacy_Fn_Courses_GetCourseAreaID]
(
    @courseID uniqueidentifier
)
RETURNS uniqueidentifier
AS 
BEGIN

    RETURN
        (SELECT
            dbo.dcelegacy_Fn_Courses_AreaTopmostParentID(Courses.Area)
        FROM
            dbo.Courses
        WHERE
            Courses.[ID] = @courseID)
    
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[dcetools_Access_Groups_Select]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Access_Groups_Select]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dcetools_Access_Groups_Select]
AS

BEGIN
	SET NOCOUNT ON;
	
	EXEC dbo.dcetools_Access_Users_GetAllGroups
	
	
	RETURN
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[dcelegacy_Subscribe_GetCourseDetails]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcelegacy_Subscribe_GetCourseDetails]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dcelegacy_Subscribe_GetCourseDetails]
(
	@courseID uniqueidentifier
)
AS

BEGIN
	SET NOCOUNT ON;

    SELECT
        Courses.[ID] as [ID],
        Courses.Code as Code,
        dbo.dcetools_Fn_Content_GetString(Courses.[Name]) as [Name],
        dbo.dcetools_Fn_Content_GetString(Courses.DescriptionShort) as Description,
        
        (SELECT Trainings.StartDate
        FROM dbo.Trainings
        WHERE Trainings.[ID] = dbo.dcelegacy_Fn_Subscribe_GetBestTrainingID(Courses.[ID], GetDate()))
        as BestTrainingStartDate,

        (SELECT Trainings.EndDate
        FROM dbo.Trainings
        WHERE Trainings.[ID] = dbo.dcelegacy_Fn_Subscribe_GetBestTrainingID(Courses.[ID], GetDate()))
        as BestTrainingEndDate,
        
        dbo.dcelegacy_Fn_Trainings_GetStudentCount(
            dbo.dcelegacy_Fn_Subscribe_GetBestTrainingID( Courses.[ID], GetDate() )) as StudentCount,
                    
        
        
        dbo.dcelegacy_Fn_Courses_GetCourseAreaID(Courses.[ID]) as AreaID,
        dbo.dcelegacy_Fn_Courses_GetAreaName(dbo.dcelegacy_Fn_Courses_GetCourseAreaID(Courses.id)) as AreaName
        
    FROM
        dbo.Courses
        
    WHERE
        Courses.CPublic=0
    AND
        Courses.isReady=1
    AND
        Courses.[ID] = @courseID 
        
    ORDER BY
        dbo.dcelegacy_Fn_Courses_GetAreaName(dbo.dcelegacy_Fn_Courses_GetCourseAreaID(Courses.id)),
        dbo.dcetools_Fn_Content_GetString(Courses.Name)
        
        
            
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[MakeTestOnlyCopy]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MakeTestOnlyCopy]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[MakeTestOnlyCopy]
	@newParentid uniqueidentifier,
	@oldParentid uniqueidentifier

as

Declare @oldTestId uniqueidentifier
Set @oldTestId = (select top 1 id from Tests where Parent = @oldParentid and Type = 1) 

-- Если тест со значением @oldParentid  в поле Parent существует
if (@oldTestId is not null)
begin
	Declare @newTestId uniqueidentifier
	Set @newTestId = NEWID( )

	execute MakeTestCopy @newTestId, @oldTestId, @newParentid
end
' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[CountStudentsPassedTest]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CountStudentsPassedTest]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'
CREATE FUNCTION [dbo].[CountStudentsPassedTest] (@training uniqueidentifier)  
RETURNS @passed TABLE(id uniqueidentifier, total int , avgpoints int ) AS  
BEGIN 
/* returns count of students passed the test and avg points for each test in training */

	insert @passed select t.id, count(*) , avg(dbo.TestResultPoints(tr.id)) from TestResults tr , Tests t where tr.Test = t.id
	and  t.id in (select id from dbo.TrainingTests(@training))
	and tr.Student in (select id from dbo.AllTrainingStudents(@training)) and tr.Complete = 1 
	group by t.id

	return 
END




' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[GetTestCourseName]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetTestCourseName]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'
CREATE FUNCTION [dbo].[GetTestCourseName] (@id uniqueidentifier, @lang int)  
RETURNS nvarchar(255) AS  
BEGIN 
declare @parent uniqueidentifier
set @parent = (select parent from Tests where id=@id)
declare @Type int
set @Type = (select e.Type from Entities e where e.id=@parent)
if (@Type <> 6) /* Course */
BEGIN /* for theme */
	set @parent = (select parent from Themes where id=@parent)
END

return (select dbo.GetStrContent(c.Name,@lang) from Courses c where id=@parent)

END


' 
END
GO
/****** Object:  StoredProcedure [dbo].[dcetools_Trainings_Tests_GetTestResults]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Trainings_Tests_GetTestResults]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dcetools_Trainings_Tests_GetTestResults]
(
	@homeRegion uniqueidentifier,
	@trainingID uniqueidentifier
)
AS

BEGIN
	SET NOCOUNT ON;
	
    SELECT
        TestResults.[id] as [ID],
        
        dbo.dcetools_Fn_Students_GetStudentFullName(TestResults.Student) as StudentName,
        ISNULL(
            dbo.dcetools_Fn_Themes_GetThemeName(Tests.Parent),
            dbo.dcetools_Fn_Courses_GetCourseName(Tests.Parent)) as ThemeName,
        
        TestResults.Complete as IsCompleted,
        ISNULL(
            (SELECT
                SUM(TestAnswers.Points)
            FROM
                dbo.TestAnswers
            WHERE
                TestAnswers.TestResults = TestResults.[ID]),
            0) as PointsCollected,
        Tests.Points as PointsRequired,
        
        TestResults.CompletionDate as CompletionDate,
        TestResults.Tries as TryCount,
        TestResults.AllowTries as RestTryCount,
        TestResults.Skipped as Skipped

    FROM
        dbo.Tests,
        dbo.TestResults

    WHERE
        TestResults.Test = Tests.[id]

    AND
    (
        @trainingID IS NULL
    OR
        Tests.[id] IN
        (SELECT [ID]
        FROM dbo.TrainingTests(@trainingID))
    )

    AND
        TestResults.Student IN
        (SELECT [ID]
        FROM dcetools_Fn_Trainings_Students_GetIDList(@trainingID))
        
    AND
    (
        dbo.dcetools_Fn_Regions_ObjectCanWriteByHome( TestResults.Student, @homeRegion ) = 1 
    ) 
	
	
	RETURN
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[MakeThemeCopy]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MakeThemeCopy]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[MakeThemeCopy] 
	@newid uniqueidentifier, 
	@oldid uniqueidentifier,
	@parentid uniqueidentifier
as
-----------------------------------------------------------
-- Создание новой записи темы --
-----------------------------------------------------------

Declare @newName uniqueidentifier
Set @newName = NEWID( )
Declare @oldName uniqueidentifier
Set @oldName = (select Name from Themes where id = @oldid)

Declare @newContent uniqueidentifier
Set @newContent = NEWID( )
Declare @oldContent uniqueidentifier
Set @oldContent = (select Content from Themes where id = @oldid)

Declare @oldPractice uniqueidentifier
Set @oldPractice = (select Practice from Themes where id = @oldid)
Declare @newPractice uniqueidentifier

if  (@oldPractice) is null
begin
	Set @newPractice = NULL
end
else
begin
	Set @newPractice = NEWID( )
end

insert into Themes select @newid, Type, @newName, @parentid, Duration,
Mandatory, @newContent, 0, @newPractice from Themes where id = @oldid

-- Копирование контента названия --
execute MakeContentCopy @newName, @oldName

-- Копирование содержимого темы --
execute MakeContentCopy @newContent, @oldContent

if (@oldPractice) is not NULL
begin
	-- данная тема есть Практика
	execute MakeTestCopy @newPractice, @oldPractice, @newid
end
else
begin
	-- Копирование теста связанного с темой (если он есть)--
	execute MakeTestOnlyCopy @newid, @oldid
end

-- Коррекция ссылки на тему вопросов родительской темы/курса
update dbo.TestQuestions set Theme = @newid 
where test in (select id from dbo.tests where parent = @parentid) and theme = @oldid
' 
END
GO
/****** Object:  StoredProcedure [dbo].[dcelegacy_Trainings_GetThemeTree]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcelegacy_Trainings_GetThemeTree]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dcelegacy_Trainings_GetThemeTree]
(
    @studentID uniqueidentifier,
    @trainingID uniqueidentifier    
)
AS

BEGIN
	SET NOCOUNT ON;

    IF dbo.dcetools_Fn_Trainings_Students_IsTrainingContainsStudent(
            @trainingID,
            @studentID)=0
    BEGIN
        
        DECLARE @studentNotInTrainingErrorMessage nvarchar(255)
        SELECT @studentNotInTrainingErrorMessage =
            ''Student not subscribed to this Training.''
                
        RAISERROR (@studentNotInTrainingErrorMessage, 16, 1)
        ROLLBACK TRAN
        
    END
    
    DECLARE
        @courseID uniqueidentifier
        
    SELECT @courseID = Trainings.Course
    FROM dbo.Trainings
    WHERE Trainings.[ID] = @trainingID
    

    SELECT
        Themes.[ID] as [ID],
        (SELECT ParentThemes.[ID]
        FROM dbo.Themes ParentThemes
        WHERE ParentThemes.[ID] = Themes.Parent) as ParentThemeID,
        Themes.Type as ThemeType,
        
        dbo.dcetools_Fn_Content_GetString(Themes.[Name]) as [Name],
        Themes.TOrder as ThemeOrderIndex,
        
        dbo.dcetools_Fn_Content_GetText(Themes.Content) as ContentText,
        dbo.dcetools_Fn_Content_GetString(Themes.Content) as ContentRef,
        
        (SELECT Schedule.StartDate
        FROM dbo.Schedule
        WHERE
            Schedule.Theme = Themes.[ID]
        AND
            Schedule.Training = @trainingID) as StartDate,

        (SELECT Schedule.EndDate
        FROM dbo.Schedule
        WHERE
            Schedule.Theme = Themes.[ID]
        AND
            Schedule.Training = @trainingID) as EndDate,
        
        (SELECT Schedule.isOpen
        FROM dbo.Schedule
        WHERE
            Schedule.Theme = Themes.[ID]
        AND
            Schedule.Training = @trainingID) as IsOpen,
        
        (SELECT Schedule.Mandatory
        FROM dbo.Schedule
        WHERE
            Schedule.Theme = Themes.[ID]
        AND
            Schedule.Training = @trainingID) as Mandatory,
        
        
        Themes.Practice as PracticeID
        
    FROM
        dbo.Themes
        
    WHERE
        dbo.dcetools_Fn_Themes_IsThemeChildAnyDeep(Themes.[ID],@courseID) <> 0
        
    ORDER BY
        Themes.TOrder
        
            	
	RETURN
END' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[dcetools_Fn_Courses_Themes_GetCourseThemeIDList]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Fn_Courses_Themes_GetCourseThemeIDList]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE FUNCTION [dbo].[dcetools_Fn_Courses_Themes_GetCourseThemeIDList]
(
	@courseID uniqueidentifier
)
RETURNS TABLE
AS
	
RETURN
	    SELECT
            Themes.[ID] as [ID]

        FROM
            dbo.Themes

        WHERE
            dbo.dcetools_Fn_Themes_IsThemeChildAnyDeep(Themes.ID, @courseID) = 1

' 
END
GO
/****** Object:  StoredProcedure [dbo].[dcelegacy_ContentView_Content]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcelegacy_ContentView_Content]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dcelegacy_ContentView_Content]
(
    @coursesRoot nvarchar(255),
    @croot nvarchar(255),
    @themeId uniqueidentifier
)
AS

BEGIN
	SET NOCOUNT ON;

    SELECT
        @coursesRoot as cRoot,
        @croot as PublicRoot,
        ct.DataStr AS url,
        c.DiskFolder,
        l.Abbr AS lang,
        cl.Abbr as DefLang,
        dbo.GetStrContentAlt(t.Name,''"+lang+ @"'',cl.Abbr) as Name,
        c.CPublic
        
    FROM
        Content ct
        
    INNER JOIN
        Themes t
        
    ON
        ct.eid = t.Content
        
    INNER JOIN
        Languages l
        
    ON
        ct.Lang = l.id
        
    INNER JOIN
        Courses c
        
    ON
        c.id=dbo.CourseofTheme(t.id)
        
    INNER JOIN
        Languages cl
        
    ON
        c.CourseLanguage = cl.id
        
    WHERE
        t.id = @themeId
    AND
        (l.Abbr = ''RU''
            OR
        l.Abbr = cl.Abbr)
        
    ORDER BY
        ct.COrder,
        ct.Lang
            
            	
	RETURN
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[dcereports_TutorDiary_Table2]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcereports_TutorDiary_Table2]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dcereports_TutorDiary_Table2]
(
	@startDate datetime,
	@endDate datetime,
	@courseTypeID uniqueidentifier,
	@regionID uniqueidentifier,
	@studentID uniqueidentifier
)
AS

BEGIN
	SET NOCOUNT ON;
	
	DECLARE
	    @showDemo as bit
	    
	SELECT @showDemo = 1

    SELECT
        MAX(dbo.dcetools_Fn_Content_GetString(Trainings.[Name]))
        as TrainingName,
        
        MAX(TestResults.FullName)
        as StudentName,
        
        MAX(TestResults.JobPosition)
        as JobPosition,
        
        MAX(TestResults.Comments)
        as Department,
        
        MAX(dbo.dcetools_Fn_Regions_GetObjectRegionName(TestResults.UserID))
        as RegionName,
        
        MAX(TestResults.CompletionDate)
        as FinalTestDate,
        
        COUNT(Tries)
        as TestTryCount,
        
        (
            SELECT
                COUNT(TestQuestions.[ID])
            FROM
                dbo.TestQuestions
            WHERE
                TestQuestions.Test = CAST( MAX(CAST(TestResults.Test as nvarchar(255)) ) as uniqueidentifier)
        )
        as QuestionCount,
        
        COUNT(TestResults.AllowTries)
        as CorrectResponseCount,

        (CASE
            WHEN COUNT(TestResults.AllowTries) = 0
            THEN 0
            
            ELSE COUNT(TestResults.Tries) / COUNT(TestResults.AllowTries)
        END)
        as CorrectResponseRate,
        
        COUNT(TestResults.AllowTries)
        as CorrectResponseCount


    
    FROM
        dbo.Trainings,
        dbo.dcereports_Fn_Tests_GetStudentTestResults(@studentID) TestResults
        
    WHERE    
    ( @studentID IS NULL OR Trainings.[ID] IN (SELECT [ID] FROM dbo.dcetools_Fn_Trainings_Students_GetStudentTrainingIDList(@studentID)) )
   
    AND    
    ( @studentID IS NULL OR TestResults.UserID = @studentID )

    AND
    ( @startDate IS NULL OR (Trainings.EndDate>=@startDate) )
    AND
    ( @endDate IS NULL OR (Trainings.StartDate<=@endDate) )
   
   GROUP BY
        TestResults.UserID, Trainings.ID 
   
	
	RETURN
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[dcelegacy_ContentView_GetTrainingTitle]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcelegacy_ContentView_GetTrainingTitle]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dcelegacy_ContentView_GetTrainingTitle]
(
    @trainingId uniqueidentifier
)
AS

BEGIN
	SET NOCOUNT ON;

    SELECT
        dbo.GetStrContentAlt(c.Name, ''" + lang + @"'', l.Abbr) as tName
        
    FROM
        dbo.Trainings t
    
    INNER JOIN
        dbo.Courses c
    
    INNER JOIN
        dbo.Languages l
    
    ON
        l.id=c.CourseLanguage
    ON
        t.Course = c.id
        
    WHERE
        t.id = @trainingID
            
            	
	RETURN
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[dcetools_Trainings_GetTitles]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Trainings_GetTitles]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dcetools_Trainings_GetTitles]
(
	@homeRegion uniqueidentifier
)
AS

BEGIN
	SET NOCOUNT ON;
	
    SELECT
        Trainings.ID as [ID],
        Trainings.Code as Code,
        dbo.dcetools_Fn_Content_GetString(Trainings.Name) as [Name],
        
        Trainings.StartDate as StartDate,
        Trainings.EndDate as EndDate,

        dbo.dcetools_Fn_Regions_GetObjectRegionID( Trainings.[ID] ) as RegionID,
        dbo.dcetools_Fn_Regions_GetObjectRegionName( Trainings.[ID] ) as RegionName
        
    FROM
        dbo.Trainings
        
    WHERE
        dbo.dcetools_Fn_Regions_ObjectCanReadByHome( Trainings.[ID], @homeRegion ) = 1
        
    ORDER BY
        Trainings.StartDate DESC, Trainings.EndDate DESC
	
	
	RETURN
END' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[dcetools_Fn_Orders_GetCourseAvailableTrainingsWithDates]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Fn_Orders_GetCourseAvailableTrainingsWithDates]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE FUNCTION [dbo].[dcetools_Fn_Orders_GetCourseAvailableTrainingsWithDates]
(
    @homeRegion uniqueidentifier,
	@courseID uniqueidentifier
)
RETURNS TABLE
AS
	
RETURN
	    SELECT
	        Trainings.[ID] as [ID],
	        Trainings.StartDate as StartDate,
	        Trainings.EndDate as EndDate
	    FROM
	        dbo.Trainings
	    WHERE
	        Trainings.Course = @courseID	        
	    AND
	        Trainings.EndDate > GetDate()
	        
	    AND
	        dbo.dcetools_Fn_Regions_ObjectCanWriteByHome(
	            Trainings.[ID],
	            @homeRegion ) <> 0' 
END
GO
/****** Object:  StoredProcedure [dbo].[dcelegacy_Subscribe_GetAreaList]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcelegacy_Subscribe_GetAreaList]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dcelegacy_Subscribe_GetAreaList]
(
	@userID uniqueidentifier
)
AS

BEGIN
	SET NOCOUNT ON;

    DECLARE
        @homeRegion uniqueidentifier
    SELECT
        @homeRegion = dbo.dcetools_Fn_Regions_GetObjectRegionID(@userID)
        
    SELECT
        Courses.[ID] as [ID],
        Courses.Code as Code,
        dbo.dcetools_Fn_Content_GetString(Courses.[Name]) as [Name],

        dbo.dcetools_Fn_Content_GetString(Courses.DescriptionShort) as Description,
        
        dbo.dcelegacy_Fn_Courses_GetCourseAreaID(Courses.[ID]) as AreaID,
        dbo.dcelegacy_Fn_Courses_GetAreaName(dbo.dcelegacy_Fn_Courses_GetCourseAreaID(Courses.id)) as AreaName
        
    FROM
        dbo.Courses
        
    WHERE
        Courses.[ID] NOT IN
        (SELECT CourseRequest.Course
        FROM dbo.CourseRequest
        where CourseRequest.Student = @userID)
    AND
        Courses.CPublic=0
    AND
        Courses.isReady=1
        
    ORDER BY
        dbo.dcelegacy_Fn_Courses_GetAreaName(dbo.dcelegacy_Fn_Courses_GetCourseAreaID(Courses.id)),
        dbo.dcetools_Fn_Content_GetString(Courses.Name)
        
        
            
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[dcetools_Access_Users_GetGroupUserList]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Access_Users_GetGroupUserList]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dcetools_Access_Users_GetGroupUserList]
(
    @homeRegion uniqueidentifier,
	@groupID uniqueidentifier
)
AS

BEGIN
	SET NOCOUNT ON;
	
    SELECT
        Users.[id] as [ID],
        dbo.dcetools_Fn_Students_GetStudentFullName(Users.ID) as UserName
        
    FROM
        dbo.Groups,
        dbo.GroupMembers,
        dbo.Users
        
    WHERE
        GroupMembers.[ID] = Users.[ID]
    AND
        GroupMembers.MGroup = Groups.[id]
    AND
        Groups.[ID] = @groupID
        
    AND
    (
        dbo.dcetools_Fn_Regions_ObjectCanReadByHome(Users.[id],@homeRegion) = 1
    )
	
	RETURN
END' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[CourseOfTestQuestion]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CourseOfTestQuestion]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'
CREATE FUNCTION [dbo].[CourseOfTestQuestion] (@id uniqueidentifier)  
RETURNS uniqueidentifier AS  
BEGIN 
declare @Course uniqueidentifier
declare @Test uniqueidentifier

set @Test = (select Test from TestQuestions where id = @id)
set @Course = dbo.CourseOfTest(@Test)

return @Course
END





' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[CountTestTries]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CountTestTries]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'
CREATE FUNCTION [dbo].[CountTestTries] (@training uniqueidentifier , @test uniqueidentifier)  
RETURNS int AS  
BEGIN 
	return (select SUM(Tries) from TestResults tr where tr.Test = @test 
	and tr.Student in (select id from dbo.AllTrainingStudents(@training)) )
END

' 
END
GO
/****** Object:  StoredProcedure [dbo].[dcereports_Student_LoadAllRelatedTables]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcereports_Student_LoadAllRelatedTables]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dcereports_Student_LoadAllRelatedTables]
(
    @homeRegion uniqueidentifier,
	@studentID uniqueidentifier
)
AS

BEGIN
	SET NOCOUNT ON;
	
	

    -- заполнение #TestResults
    SELECT
        TestResults.[ID] as [ID],
        TestResults.Test as TestID,
        TestResults.Student as StudentID,
        TestResults.Complete as IsCompleted,
        TestResults.CompletionDate as CompletionDate,
        TestResults.Tries as TryCount,
        TestResults.AllowTries as AllowedTryCount,
        TestResults.TryStart as StartTime,
        TestResults.Skipped as Skipped,
        dbo.TestResultPoints(TestResults.[ID]) as Points
    
    INTO #TestResults
    
    FROM
        dbo.TestResults
        
    WHERE
        TestResults.Student = @studentID
    OR
        (@studentID IS NULL AND dbo.dcetools_Fn_Regions_ObjectCanReadByHome( TestResults.Student, @homeRegion )=1)
        
    
    
	-- заполнение #Students
	SELECT
	    Users.[ID] as [ID],
	    Users.FullName as FullName,
	    dbo.dcetools_Fn_Regions_GetObjectRegionName(Users.[ID]) as RegionName,
	    Users.JobPosition as JobPosition,
	    Users.Comments as Comments
	    
	INTO #Students
	    
	FROM
	    dbo.Users, #TestResults
	WHERE
	    #TestResults.StudentID = Users.[ID]
	    
	ORDER BY dbo.dcetools_Fn_Regions_GetObjectRegionName(Users.[ID]), Users.FullName
	
	    
	-- заполнение #StudentGroup
	SELECT
        GroupMembers.MGroup as GroupID,
        #Students.[ID] as StudentID
	
	INTO #StudentGroup
	
    FROM
        dbo.GroupMembers, #Students
        
    WHERE
        GroupMembers.[ID] = #Students.[ID]

    -- заполнение #Groups
    SELECT
        Groups.[id] as [ID],
        Groups.[Name] as [Name]
        
    INTO #Groups
        
    FROM
        dbo.Groups, #StudentGroup
        
    WHERE
        Groups.[id] = #StudentGroup.GroupID
    AND
        Groups.Type > 4 


    
    
        
    -- заполнение #Tests
    SELECT DISTINCT
        Tests.[ID] as [ID],
        Tests.[Type] as [Type],
        
        CAST(NULL as uniqueidentifier) as CourseID,
        Tests.Parent as ThemeID,
        -- Если ParentThemeID окажется, что ссылается на курс,
        -- то переставить значение соответственно
        
        Tests.Duration as AllowedDurationSeconds,
        Tests.Show as ShowResults,
        Tests.AutoFinish as AutoFinishOnPointsCollected,
        Tests.Points as Points,
        Tests.Hints as HintCount,
        Tests.ShowThemes as ShowThemes
        
    INTO #Tests
    
    FROM
        dbo.Tests, #TestResults
        
    WHERE
        #TestResults.TestID = Tests.[ID]


    
    
    
    
    
    
    
       
      -- заполнение #TestQuestions
    SELECT DISTINCT
        TestQuestions.[ID] as [ID],
        TestQuestions.Test as TestID,
        TestQuestions.QOrder as OrderIndex,
        TestQuestions.[Type] as [Type],
        dbo.dcetools_Fn_Content_GetText(TestQuestions.Content) as ContentText,
        TestQuestions.Points as Points,
        dbo.dcetools_Fn_Content_GetText(TestQuestions.Answer) as AnswerText
        
    INTO #TestQuestions
    
    FROM
        dbo.TestQuestions, #Tests
        
    WHERE
        TestQuestions.Test = #Tests.[ID]
        
        
    -- заполнение #TestAnswers
    SELECT
        TestAnswers.[ID] as [ID],
        TestAnswers.TestResults as ResultID,
        TestAnswers.Question as QuestionID,
        TestAnswers.Answer as AnswerText,
        TestAnswers.AnswerTime as AnswerTimeSeconds,
        TestAnswers.Points as Points
        
    INTO #TestAnswers
        
    FROM
        dbo.TestAnswers
        
    WHERE
         TestAnswers.TestResults IN (SELECT [ID] FROM #TestResults)
    OR
         TestAnswers.Question IN (SELECT [ID] FROM #TestQuestions)
        
        
        
        
    -- заполнение #Themes
    -- — здесь только темы, у которых есть тесты,
    -- к ним далее достраиваются вверх темы более верхнего уровня
    SELECT DISTINCT
        Themes.[ID] as [ID],
        CAST(NULL as uniqueidentifier) as CourseID,
        Themes.Parent as ParentThemeID,
        Themes.TOrder as OrderIndex,
        Themes.[Type] as [Type],
        dbo.dcetools_Fn_Content_GetString(Themes.[Name]) as [Name],
        Themes.Duration as DurationDays,
        Themes.Mandatory as IsMandatory
        
    INTO #Themes
    
    FROM
        dbo.Themes, #Tests
        
    WHERE
        Themes.[ID] = #Tests.ThemeID
    
    
    -- достройка дерева тем "вверх"
    
    INSERT INTO #Themes
    
    SELECT DISTINCT
        Themes.[ID] as [ID],
        CAST(NULL as uniqueidentifier) as CourseID,
        Themes.Parent as ParentThemeID,
        Themes.TOrder as OrderIndex,
        Themes.[Type] as [Type],
        dbo.dcetools_Fn_Content_GetString(Themes.[Name]) as [Name],
        Themes.Duration as DurationDays,
        Themes.Mandatory as IsMandatory
    
    FROM
        dbo.Themes, #Themes
        
    WHERE
        dbo.dcetools_Fn_Themes_IsThemeChildAnyDeep(#Themes.[ID], Themes.[ID]) = 1
        
        
        
        
        
        
        
        
    -- заполнение #Course по темам
        
    SELECT DISTINCT
        Courses.[ID] as [ID],
        Courses.Area as DomainID,
        Courses.Version as Version,
        Courses.Code as Code,
        dbo.dcetools_Fn_Content_GetString(Courses.[Name]) as [Name],
        Courses.[Type] as [Type],
        Courses.CPublic as IsPublic
        
    INTO #Courses
        
    FROM
        dbo.Courses, #Themes
        
    WHERE
        Courses.[ID] = #Themes.ParentThemeID

        
    -- заполнение #Course по курсовым тестам

    INSERT INTO #Courses
    SELECT DISTINCT
        Courses.[ID] as [ID],
        Courses.Area as DomainID,
        Courses.Version as Version,
        Courses.Code as Code,
        dbo.dcetools_Fn_Content_GetString(Courses.[Name]) as [Name],
        Courses.[Type] as [Type],
        Courses.CPublic as IsPublic
        
    FROM
        dbo.Courses, #Tests
        
    WHERE
        Courses.[ID] = #Tests.ThemeID
          
        
        
        
        
        
    -- установка CourseID, очищение ParentThemeID
    -- для тем верхнего уровня
    UPDATE #Themes
    SET
        #Themes.CourseID = #Courses.[ID],
        #Themes.ParentThemeID = NULL
        
    FROM
        #Courses
        
    WHERE
        #Themes.ParentThemeID = #Courses.[ID]


    -- установка CourseID
    -- для тем второго и других уровней
    UPDATE #Themes
    SET
        #Themes.CourseID = #Courses.[ID],
        #Themes.ParentThemeID = NULL
        
    FROM
        #Courses
        
    WHERE
        dbo.dcetools_Fn_Themes_IsThemeChildAnyDeep(#Themes.ParentThemeID, #Courses.[ID]) = 1
        
        
        
    -- присваивание CourseID
    -- для всех тестов, относящихся к темам
    -- (бывают ещё курсовые тесты, они обрабатываются ниже)
    UPDATE #Tests
    SET
        #Tests.CourseID = #Themes.CourseID
        
    FROM
        #Themes
        
    WHERE
        #Tests.ThemeID = #Themes.[ID]
        
    -- присваивание CourseID и обнуление ThemeID
    -- для курсовых тестов
    UPDATE #Tests
    SET
        #Tests.CourseID = #Courses.ID,
        #Tests.ThemeID = NULL
    
    FROM
        #Courses
        
    WHERE
        #Tests.ThemeID = #Courses.[ID]
                
        
        
        
        
    -- заполнение CourseDomain
    SELECT DISTINCT
        CourseDomain.[ID] as [ID],
        CourseDomain.Parent as ParentID,
        dbo.dcetools_Fn_Content_GetString(CourseDomain.[Name]) as [Name]
    
    INTO #CourseDomains
    
    FROM
        dbo.CourseDomain, #Courses
        
    WHERE
        CourseDomain.ID = #Courses.DomainID
        
        
    -- достройка областей тем "вверх"
    
    INSERT INTO #CourseDomains
    
    SELECT DISTINCT
        CourseDomain.[ID] as [ID],
        CourseDomain.Parent as ParentID,
        dbo.dcetools_Fn_Content_GetString(CourseDomain.[Name]) as [Name]
    
    FROM
        dbo.CourseDomain, #CourseDomains
        
    WHERE
        dbo.dcetools_Fn_CourseDomains_IsDomainChildAnyDeep(#CourseDomains.[ID], CourseDomain.[ID]) = 1
        
        
        
    
    
    
    
    -- Вырезание битых ссылок
    
    IF 1=1 -- off this block to show broken links too
    BEGIN

    DELETE FROM #Courses
    WHERE NOT EXISTS(
        SELECT #CourseDomains.[ID]
        FROM #CourseDomains
        WHERE #Courses.DomainID = #CourseDomains.[ID])

    DELETE FROM #Themes
    WHERE NOT EXISTS(
        SELECT #Courses.[ID]
        FROM #Courses
        WHERE #Themes.CourseID = #Courses.[ID])

    DELETE FROM #Tests
    WHERE NOT EXISTS(
        SELECT #Courses.[ID]
        FROM #Courses
        WHERE #Tests.CourseID = #Courses.[ID])
    AND NOT EXISTS(
        SELECT #Themes.[ID]
        FROM #Themes
        WHERE #Tests.ThemeID = #Themes.[ID])

    DELETE FROM #TestQuestions
    WHERE NOT EXISTS(
        SELECT #Tests.[ID]
        FROM #Tests
        WHERE #TestQuestions.TestID = #Tests.[ID])

    DELETE FROM #TestResults
    WHERE NOT EXISTS(
        SELECT #Tests.[ID]
        FROM #Tests
        WHERE #TestResults.TestID = #Tests.[ID])

    DELETE FROM #TestAnswers
    WHERE NOT EXISTS(
        SELECT #TestQuestions.[ID]
        FROM #TestQuestions
        WHERE #TestAnswers.QuestionID = #TestQuestions.[ID])
    OR NOT EXISTS(
        SELECT #TestResults.[ID]
        FROM #TestResults
        WHERE #TestAnswers.ResultID = #TestResults.[ID])
        
    DELETE FROM #Students
    WHERE NOT EXISTS(
        SELECT #TestResults.[ID]
        FROM #TestResults
        WHERE #TestResults.StudentID = #Students.[ID])  

    DELETE FROM #StudentGroup
    WHERE NOT EXISTS(
        SELECT #Students.[ID]
        FROM #Students
        WHERE #Students.[ID] = #StudentGroup.StudentID)
        
    DELETE FROM #StudentGroup
    WHERE NOT EXISTS(
        SELECT #Groups.[ID]
        FROM #Groups
        WHERE #Groups.[ID] = #StudentGroup.GroupID)
        
    DELETE FROM #Groups
    WHERE NOT EXISTS(
        SELECT #StudentGroup.GroupID
        FROM #StudentGroup
        WHERE #StudentGroup.GroupID = #Groups.[ID])

    END

    
    
    
    -- Вывод результата:

    IF 1=1
    BEGIN
        SELECT ''cd'',  #CourseDomains.* FROM #CourseDomains
        SELECT ''c'',    #Courses.* FROM #Courses
        SELECT ''th'',   #Themes.* FROM #Themes
        SELECT ''t'',     #Tests.* FROM #Tests
        SELECT ''tq'',    #TestQuestions.* FROM #TestQuestions
        SELECT ''tr'',     #TestResults.* FROM #TestResults
        SELECT ''ta'',     #TestAnswers.* FROM #TestAnswers

        SELECT ''st'',  #Students.* FROM #Students
        SELECT ''sg'', #StudentGroup.* FROM #StudentGroup
        SELECT ''g'', #Groups.* FROM #Groups
    END    
    ELSE
    BEGIN
        SELECT  #CourseDomains.* FROM #CourseDomains
        SELECT #Courses.* FROM #Courses
        SELECT  #Themes.* FROM #Themes
        SELECT  #Tests.* FROM #Tests
        SELECT   #TestQuestions.* FROM #TestQuestions
        SELECT    #TestResults.* FROM #TestResults
        SELECT    #TestAnswers.* FROM #TestAnswers
        
        SELECT #Students.* FROM #Students
        SELECT #StudentGroup.* FROM #StudentGroup
        SELECT #Groups.* FROM #Groups
    END
                    	
	RETURN
END' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[dcetools_Fn_Access_FindUsersByAny]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Fn_Access_FindUsersByAny]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE FUNCTION [dbo].[dcetools_Fn_Access_FindUsersByAny]
(
	@homeRegion uniqueidentifier,
	@searchString nvarchar(255)
)
RETURNS @Users TABLE(
    [ID] uniqueidentifier,
    Login nvarchar(255),
    FullName nvarchar(255),
    EMail nvarchar(255),
    JobPosition nvarchar(255),
    Comments nvarchar(255),
    
    CreateDate datetime,
    LastModifyDate datetime,
    
    RegionID uniqueidentifier,
    RegionName nvarchar(255),
    
    RoleID uniqueidentifier,
    RoleName nvarchar(255)
)
AS
BEGIN
INSERT INTO @Users
				
SELECT
        Users.[ID] as [ID],
        Users.Login as Login,
        Users.FullName as FullName,
        Users.Email as EMail,
        Users.JobPosition as JobPosition,
        Users.Comments as Comments,
        
        Users.CreateDate as CreateDate,
        Users.LastModifyDate as LastModify,
        
        dbo.dcetools_Fn_Regions_GetObjectRegionID( Users.[ID] ) as RegionID,
        dbo.dcetools_Fn_Regions_GetObjectRegionName( Users.[ID] ) as RegionName,
        
        (SELECT UserRoles.[Role]
        FROM dbo.UserRoles
        WHERE UserRoles.UserID = Users.[ID]) as RoleID,
        
        (SELECT dbo.dcetools_Fn_Content_GetString(UserRoles.[Role])
        FROM dbo.UserRoles
        WHERE UserRoles.UserID = Users.[ID]) as RoleName
        
    FROM
        dbo.Users
        
    WHERE
    (
        dbo.dcetools_Fn_Regions_ObjectCanReadByHome( Users.[ID], @homeRegion ) = 1
    )
    AND
    (    
        @searchString IS NULL
    OR
        LTrim(RTrim(@searchString)) = ''''
        
    OR
    
        ISNULL(Users.Login,'''') LIKE ''%''+@searchString+''%''
    OR
        ISNULL(dbo.dcetools_Fn_Users_GetUserFullName(Users.[ID]),'''') LIKE ''%''+@searchString+''%''
    OR
        ISNULL(Users.EMail,'''') LIKE ''%''+@searchString+''%''
    OR
        ISNULL(Users.Comments,'''') LIKE ''%''+@searchString+''%''
    OR
        ISNULL(dbo.dcetools_Fn_Regions_GetObjectRegionName( Users.[ID] ),'''') LIKE ''%''+@searchString+''%''
    OR
        ISNULL((SELECT dbo.dcetools_Fn_Content_GetString(UserRoles.[Role])
        FROM dbo.UserRoles
        WHERE UserRoles.UserID = Users.[ID]),'''') LIKE ''%''+@searchString+''%''
    )
        
    ORDER BY

        CASE
            WHEN
                ISNULL(dbo.dcetools_Fn_Users_GetUserFullName(Users.[ID]),'''') = @searchString
            THEN 1
            ELSE 0
        END DESC,

        CASE
            WHEN
                ISNULL(Users.EMail,'''') = @searchString
            THEN 1
            ELSE 0
        END DESC,

        CASE
            WHEN
                ISNULL(dbo.dcetools_Fn_Users_GetUserFullName(Users.[ID]),'''') LIKE ''%''+@searchString+''%''
            THEN 1
            ELSE 0
        END DESC,

        CASE
            WHEN
                ISNULL(Users.EMail,'''') LIKE ''%''+@searchString+''%''
            THEN 1
            ELSE 0
        END DESC,
        
        ISNULL(Users.FirstName,''''),
        ISNULL(Users.Patronymic,''''),
        ISNULL(Users.LastName,''''),
        ISNULL(Users.Login,'''')				

	RETURN
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[dceweb_News_GetDetails]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dceweb_News_GetDetails]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dceweb_News_GetDetails]
(
	@userID uniqueidentifier
)
AS

BEGIN
	SET NOCOUNT ON;
	
	DECLARE
	    @homeRegion uniqueidentifier
	    
	SELECT
	    @homeRegion = dbo.dcetools_Fn_Regions_GetObjectRegionID(@userID)

    SELECT
        News.[ID] as [ID], 
        News.NewsDate as [Date], 
        dbo.dcetools_Fn_Content_GetString(News.Head) as Title,
        dbo.dcetools_Fn_Content_GetText(News.[Text]) as ContentText,
        
        dbo.dcetools_Fn_Content_GetImage(News.[Image]) as [Image],
        
        News.MoreHref as Href,
        
        dbo.dcetools_Fn_Regions_GetObjectRegionID( News.[ID] ) as RegionID,
        dbo.dcetools_Fn_Regions_GetObjectRegionName( News.[ID] ) as RegionName
        
    FROM
        dbo.News
        
    WHERE
    
        dbo.dcetools_Fn_Regions_ObjectCanReadByHome( News.[ID], @homeRegion ) = 1
        
    ORDER BY
        News.NewsDate desc
	
	
	RETURN
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[dcetools_News_GetTitles]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_News_GetTitles]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dcetools_News_GetTitles]
(
	@homeRegion uniqueidentifier
)
AS

BEGIN
	SET NOCOUNT ON;
	
    SELECT
        News.ID as [ID],
        News.NewsDate as [Date],
        dbo.dcetools_Fn_Content_GetString(News.Head) as Title,
        dbo.dcetools_Fn_Content_GetText(News.Text) as ContentText,
        
        dbo.dcetools_Fn_Regions_GetObjectRegionID( News.[ID] ) as RegionID,
        dbo.dcetools_Fn_Regions_GetObjectRegionName( News.[ID] ) as RegionName
        
    FROM
        dbo.News
        
    WHERE
        dbo.dcetools_Fn_Regions_ObjectCanReadByHome(News.[ID], @homeRegion) = 1
        
    ORDER BY
        News.NewsDate desc
	
	
	RETURN
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[dcetools_Students_GetAllStudents]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Students_GetAllStudents]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dcetools_Students_GetAllStudents]
(
	@homeRegion uniqueidentifier
)
AS

BEGIN
	SET NOCOUNT ON;
	
    SELECT
        Students.ID,
        RTrim(
            RTrim(
                LTrim(RTrim(Students.FirstName)) + '' '' + LTrim(Students.Patronymic)
                        ) + '' '' +LTrim(Students.LastName)) as FullName,
        Students.EMail,
        Students.JobPosition as [Position],
        
        dbo.dcetools_Fn_Regions_GetObjectRegionID(Students.[ID]) as RegionID,
        dbo.dcetools_Fn_Regions_GetObjectRegionName( Students.[ID] ) as RegionName

    FROM
        Students

    WHERE
        dbo.dcetools_Fn_Regions_ObjectCanReadByHome(
            Students.[ID],
            @homeRegion ) = 1
	
	RETURN
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[dcetools_Access_CheckObjectID_Read]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Access_CheckObjectID_Read]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dcetools_Access_CheckObjectID_Read]
(
	@homeRegion uniqueidentifier,
	@objectID uniqueidentifier
)
AS

BEGIN
	SET NOCOUNT ON;

    IF @objectID IS NULL
    BEGIN

        DECLARE @parameterNullErrorMessage nvarchar(255)
        SELECT @parameterNullErrorMessage =
            ''Parameter is NULL: @objectID.''
                
        RAISERROR (@parameterNullErrorMessage, 16, 1)
        ROLLBACK TRAN
                
    END  


    DECLARE
        @objectRegionID uniqueidentifier
        
    SELECT @objectRegionID = dbo.dcetools_Fn_Regions_GetObjectRegionID(@objectID)

    EXEC dbo.dcetools_Access_CheckObjectRegionID_Read @homeRegion, @objectRegionID
        	
	
	RETURN
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[dcetools_Regions_SetObjectRegion]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Regions_SetObjectRegion]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dcetools_Regions_SetObjectRegion] 
(
    @homeRegion uniqueidentifier,
	@objectID uniqueidentifier,
	@regionID uniqueidentifier
)
AS

BEGIN
	SET NOCOUNT ON;
	
    EXEC dbo.dcetools_Access_CheckObjectRegionID_Write
        @homeRegion,
        @regionID

	IF EXISTS(
	    SELECT ObjectID
	    FROM dbo.ObjectRegions
	    WHERE ObjectID = @objectID )
	BEGIN
	
	    IF @regionID IS NULL
	        DELETE FROM dbo.ObjectRegions
	        WHERE ObjectRegions.ObjectID = @objectID
	    ELSE
	        UPDATE dbo.ObjectRegions
	        SET RegionID = @regionID
	        WHERE ObjectRegions.ObjectID = @objectID
	
	END
	ELSE
	BEGIN

	    IF @regionID IS NOT NULL
	        INSERT INTO dbo.ObjectRegions(ObjectID, RegionID)
	        VALUES(@objectID, @regionID)
	
	END
	
	    
	    
	
	RETURN
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[dcetools_Access_CheckObjectID_Write]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Access_CheckObjectID_Write]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dcetools_Access_CheckObjectID_Write]
(
	@homeRegion uniqueidentifier,
	@objectID uniqueidentifier
)
AS

BEGIN
	SET NOCOUNT ON;

    IF @objectID IS NULL
    BEGIN

        DECLARE @parameterNullErrorMessage nvarchar(255)
        SELECT @parameterNullErrorMessage =
            ''Parameter is NULL: @objectID.''
                
        RAISERROR (@parameterNullErrorMessage, 16, 1)
        ROLLBACK TRAN
                
    END  


    DECLARE
        @objectRegionID uniqueidentifier
        
    SELECT @objectRegionID = dbo.dcetools_Fn_Regions_GetObjectRegionID(@objectID)

    EXEC dbo.dcetools_Access_CheckObjectRegionID_Write @homeRegion, @objectRegionID
        	
	
	RETURN
END' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[AllDistinctTrainingStudents]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AllDistinctTrainingStudents]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'
CREATE FUNCTION [dbo].[AllDistinctTrainingStudents] (@id uniqueidentifier)  
RETURNS @members TABLE (id uniqueidentifier) AS  
BEGIN 

   INSERT @members SELECT DISTINCT al.id from AllTrainingStudents(@id) al where al.Type=1

return 
END



' 
END
GO
/****** Object:  StoredProcedure [dbo].[dceaccess_UpdateUser]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dceaccess_UpdateUser]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dceaccess_UpdateUser]
(
    @userID uniqueidentifier,
	@login nvarchar(255),
	
	@firstName nvarchar(255),
	@patronymic nvarchar(255),
	@lastName nvarchar(255),
	@email nvarchar(255),
	@jobPosition nvarchar(255),
	@comments nvarchar(255),
	@regionID uniqueidentifier,
	@roleID uniqueidentifier
	
)
AS

BEGIN
	SET NOCOUNT ON;

    DECLARE
        @existingUserID uniqueidentifier
	
	SELECT
	    @userID = Users.[ID]
	FROM
	    dbo.Users
	WHERE
	    Users.Login = @login
	    
	IF @login IS NULL
	BEGIN
	
        DECLARE @noUserFoundErrorMessage nvarchar(255)
        SELECT @noUserFoundErrorMessage =
            ''Unknown Login '' + ISNULL(@login,''(null)'')+''.''
                
        RAISERROR (@noUserFoundErrorMessage, 16, 1)
        ROLLBACK TRAN
	
	END	

    IF @existingUserID <> @userID
    BEGIN
	
        DECLARE @loginUserIDDoesNotMatchErrorMessage nvarchar(255)
        SELECT @noUserFoundErrorMessage =
            ''Login and UserID does not match.''
                
        RAISERROR (@loginUserIDDoesNotMatchErrorMessage, 16, 1)
        ROLLBACK TRAN
	
    END

    UPDATE dbo.Students
    SET
        Students.FirstName = ISNULL(@firstName,Students.FirstName),
        Students.Patronymic = ISNULL(@patronymic,Students.Patronymic),
        Students.LastName = ISNULL(@lastName,Students.LastName),
        Students.EMail = ISNULL(@email,Students.EMail),
        Students.JobPosition = ISNULL(@jobPosition,Students.JobPosition),
        Students.Comments = ISNULL(@comments,Students.Comments),
        
        Students.LastModifyDate = GetDate()
        
    WHERE
        Students.Login = @login
        
    EXEC dbo.dcetools_Regions_SetObjectRegion NULL, @userID, @regionID
    
    EXEC dbo.dcetools_Access_Users_SetUserRole @userID, @roleID
	
	RETURN
END' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[dcetools_Fn_Courses_GetTestIdList]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Fn_Courses_GetTestIdList]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE FUNCTION [dbo].[dcetools_Fn_Courses_GetTestIdList]
(
	@courseID uniqueidentifier
)
RETURNS TABLE
AS
	
RETURN
	    SELECT
            Tests.[ID]

        FROM
            dbo.Tests

        WHERE
            Tests.Parent = @courseID
        OR
            Tests.Parent IN 
            (
                SELECT *
                FROM dbo.dcetools_Fn_Courses_Themes_GetCourseThemeIDList(@courseID)
            ) 

' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[dcetools_Fn_Courses_Themes_GetCourseThemes]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Fn_Courses_Themes_GetCourseThemes]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE FUNCTION [dbo].[dcetools_Fn_Courses_Themes_GetCourseThemes]
(
	@courseID uniqueidentifier
)
RETURNS TABLE
AS
	
RETURN
        SELECT
            Themes.[ID] as [ID],
            Themes.[Type] as [Type],
            dbo.dcetools_Fn_Content_GetString( Themes.[Name] ) as [Name],
            (SELECT
                ParentThemes.[ID]
            FROM
                dbo.Themes ParentThemes
            WHERE
                ParentThemes.[ID] = Themes.Parent ) as ParentThemeID,
            Themes.Duration as Duration,
            Themes.Mandatory as Mandatory,
            dbo.dcetools_Fn_Content_GetString( Themes.[Content] ) as ContentHref,
            Themes.TOrder as OrderIndex
            

        FROM
            dbo.Themes

        WHERE
            Themes.[ID] IN (SELECT [ID] FROM dcetools_Fn_Courses_Themes_GetCourseThemeIDList( @courseID ))' 
END
GO
/****** Object:  StoredProcedure [dbo].[MakeThemesCopy]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MakeThemesCopy]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[MakeThemesCopy] 
	@newParentid uniqueidentifier,
	@oldParentid uniqueidentifier
as

declare Themes_Cursor cursor local for 
select id from dbo.Themes where Parent = @oldParentid 
order by TOrder /* important for save order*/

open Themes_Cursor

declare @oldThemeId uniqueidentifier
declare @newThemeId uniqueidentifier

fetch next from Themes_Cursor into @oldThemeId
while (@@FETCH_STATUS <> -1)
begin
	if (@@FETCH_STATUS <> -2)
	begin
		set @newThemeId = newid()
		-- копирование содержимого темы
		execute MakeThemeCopy @newThemeId, @oldThemeId, @newParentid
		
		-- копирование всех подтем
		execute MakeThemesCopy @newThemeId, @oldThemeId
	end

	fetch next from Themes_Cursor into @oldThemeId
end

close Themes_Cursor
deallocate Themes_Cursor
' 
END
GO
/****** Object:  StoredProcedure [dbo].[dcetools_Trainings_UpdateDetails]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Trainings_UpdateDetails]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dcetools_Trainings_UpdateDetails]
(
	@homeRegion uniqueidentifier,
	@trainingID uniqueidentifier,
	
	@code nvarchar(250),
	@name nvarchar(250),
	@comments nvarchar(max),

    @startDate datetime,
    @endDate datetime,
   
    @isPublic bit,
    @isActive bit,
    @testOnly bit,
    @expires bit,
   
    @regionID uniqueidentifier      
	
)
AS

BEGIN
	SET NOCOUNT ON;

    EXEC dbo.dcetools_Access_CheckObjectID_Write
        @homeRegion,
        @trainingID
        
    DECLARE
        @regionBefore uniqueidentifier
        
    SELECT
        @regionBefore = dbo.dcetools_Fn_Regions_GetObjectRegionID(@trainingID)

    EXEC dbo.dcetools_Access_CheckObjectRegionID_Write
        @homeRegion,
        @regionBefore

    EXEC dbo.dcetools_Access_CheckObjectRegionID_Write
        @homeRegion,
        @regionID
        
        
        
    IF @endDate<@startDate
    BEGIN
        DECLARE @tmpDate datetime
        SELECT
            @tmpDate = @endDate
        SELECT
            @endDate = @startDate
        SELECT
            @startDate = @tmpDate
    END
        
        
    DECLARE
        @nameContentID uniqueidentifier, 
        @commentContentID uniqueidentifier
        
    SELECT
        @nameContentID = Trainings.[Name],
        @commentContentID = Trainings.Comment
    FROM
        dbo.Trainings
    WHERE
        Trainings.ID = @trainingID
        
    EXEC dbo.dcetools_Content_SetString @nameContentID, @name
    EXEC dbo.dcetools_Content_SetText @commentContentID, @comments
        
	
    UPDATE dbo.Trainings
    SET
        Code = @code,
        StartDate = @startDate,
        EndDate = @endDate,
        
        isPublic = @isPublic,
        isActive = @isActive,
        TestOnly = @testOnly,
        Expires = @expires
    WHERE
        Trainings.ID = @trainingID
        
    EXEC dbo.dcetools_Regions_SetObjectRegion @homeRegion, @trainingID, @regionID
        
	RETURN
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[dcetools_News_CreateDetails]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_News_CreateDetails]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dcetools_News_CreateDetails]
(
	@homeRegion uniqueidentifier,
	
	@date datetime,
	@title nvarchar(255),
	@contentText nvarchar(max),
	@href nvarchar(255),
	
	@regionID uniqueidentifier
)
AS

BEGIN
	SET NOCOUNT ON;

        
        
    EXEC dbo.dcetools_Access_CheckObjectRegionID_Write
        @homeRegion,
        @regionID


    DECLARE
	    @newsID uniqueidentifier,

        @headContentID uniqueidentifier,
        @textContentID uniqueidentifier
        
   SELECT 
        @newsID = newid(),

        @headContentID = newid(),
        @textContentID = newid()
        


    EXEC dbo.dcetools_Content_SetString @headContentID, @title
    EXEC dbo.dcetools_Content_SetText @textContentID, @contentText


    INSERT INTO dbo.News(
        [ID],
        NewsDate,
        Head,
        [Text],
        MoreHref )
        
    VALUES(
        @newsID, 
        @date,
        @headContentID,
        @textContentID,
        @href )
	
	EXEC dbo.dcetools_Regions_SetObjectRegion @homeRegion, @newsID, @regionID
	
	RETURN
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[dcetools_News_UpdateDetails]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_News_UpdateDetails]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dcetools_News_UpdateDetails]
(
	@homeRegion uniqueidentifier,
	@id uniqueidentifier,
	
	@date datetime,
	@title nvarchar(255),
	@contentText nvarchar(max),
	@href nvarchar(255),
	
	@regionID uniqueidentifier
)
AS

BEGIN
	SET NOCOUNT ON;

        
    DECLARE
        @newsID uniqueidentifier
        
    SELECT
        @newsID = @id  
        
        
    DECLARE
        @currentNewsRegionID uniqueidentifier,

        @headContentID uniqueidentifier,
        @textContentID uniqueidentifier
        
    SELECT @currentNewsRegionID = dbo.dcetools_Fn_Regions_GetObjectRegionID( @newsID )
   
   SELECT 
        @headContentID = News.Head,
        @textContentID = News.[Text]
    FROM
        dbo.News 
    WHERE
        News.[ID] = @newsID
        

    EXEC dbo.dcetools_Access_CheckObjectRegionID_Write
        @homeRegion,
        @currentNewsRegionID

    EXEC dbo.dcetools_Access_CheckObjectRegionID_Write
        @homeRegion,
        @regionID


    EXEC dbo.dcetools_Content_SetString @headContentID, @title
    EXEC dbo.dcetools_Content_SetText @textContentID, @contentText


    UPDATE dbo.News
    SET
        News.NewsDate = @date,
        News.MoreHref = @href
    WHERE
        News.[ID] = @newsID

    EXEC dbo.dcetools_Regions_SetObjectRegion @homeRegion, @newsID, @regionID
	
	
	RETURN
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[dcetools_Trainings_CreateTraning]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Trainings_CreateTraning]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[dcetools_Trainings_CreateTraning]
(
	@homeRegion uniqueidentifier,
	
	@name nvarchar(255),
	@code nvarchar(255),

	@courseID uniqueidentifier,
	
	@startDate datetime,
	@endDate datetime,
	
	@isPublic bit,
	@isActive bit,
	@testOnly bit,
	@expires bit,
	
	@regionID uniqueidentifier
	
)
AS

BEGIN
	SET NOCOUNT ON;
	
    EXEC dbo.dcetools_Access_CheckObjectRegionID_Write
        @homeRegion,
        @regionID

    DECLARE
	    @trainingID uniqueidentifier,
        @nameContentID uniqueidentifier,
        
        @instructorsID uniqueidentifier,
        @curatorsID uniqueidentifier,
        @studentsID uniqueidentifier

   SELECT 
        @trainingID = newid(),
        @nameContentID = newid(),
        
        @instructorsID = newid(),
        @curatorsID = newid(),
        @studentsID = newid()


	EXEC dbo.dcetools_Content_SetString @nameContentID, @name


--    PRINT ''INSERT INTO Groups(...) VALUES(''+CAST(@instructorsID as nvarchar(255)) + '', ..., 2)''
--    INSERT INTO Groups(
--        [id],
--        [Name],
--        Type )
--    VALUES(
--        @instructorsID,
--        ''Instructors [webtools]'',
--        2)
--    PRINT ''OK''  
--
--
--    PRINT ''INSERT INTO Groups(...) VALUES(''+CAST(@curatorsID as nvarchar(255))+ '', ..., 2)''
--    INSERT INTO Groups(
--        [id],
--        [Name],
--        Type )
--    VALUES(
--        @curatorsID,
--        ''Curators [webtools]'',
--        2) 
--    PRINT ''OK''  
--
--    PRINT ''INSERT INTO Groups(...) VALUES(''+CAST(@studentsID as nvarchar(255))+ '', ..., 1)''
--    INSERT INTO Groups(
--        [id],
--        [Name],
--        Type )
--    VALUES(
--        @studentsID,
--        ''Students [webtools]'',
--        1)  
--    PRINT ''OK''  


    INSERT INTO Trainings(
        [ID],
        [Name],
        Code,
        Course,
        StartDate,
        EndDate,
        isPublic,
        isActive,
        TestOnly,
        Expires,
        
        Instructors,
        Curators,
        Students )
        
    VALUES(
        @trainingID,
        @nameContentID,
        @code,
        @courseID,
        @startDate,
        @endDate,
        @isPublic,
        @isActive,
        @testOnly,
        @expires,
        
        @instructorsID,
        @curatorsID,
        @studentsID )
        
        
    EXEC dbo.dcetools_Regions_SetObjectRegion @homeRegion, @trainingID, @regionID
        

	
	
	RETURN
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[dcetools_Trainings_Announcements_UPDATE]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Trainings_Announcements_UPDATE]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dcetools_Trainings_Announcements_UPDATE]
(
	@homeRegion uniqueidentifier,
	@trainingID uniqueidentifier,
	
	@id as uniqueidentifier,
	@messageText as nvarchar(MAX),
	@authorID as uniqueidentifier
)
AS

BEGIN
	SET NOCOUNT ON;
	
    EXEC dbo.dcetools_Access_CheckObjectID_Write
        @homeRegion,
        @trainingID
        
    DECLARE
        @messageTextContentID uniqueidentifier,
        @bulletinTrainingID uniqueidentifier
        
    SELECT
        @messageTextContentID = BulletinBoard.Message,
        @bulletinTrainingID = BulletinBoard.Training
    FROM
        dbo.BulletinBoard
    WHERE
        BulletinBoard.[ID] = @id
        
    IF @bulletinTrainingID <> @trainingID
    BEGIN
    
        DECLARE @bulletinTrainingNotCorrelatedErrorMessage nvarchar(255)
        SELECT @bulletinTrainingNotCorrelatedErrorMessage =
            ''Training does not contains Announcement ID specified.''                
        RAISERROR (@bulletinTrainingNotCorrelatedErrorMessage, 16, 1)
        ROLLBACK TRAN
    
    END
        
    EXEC dbo.dcetools_Content_SetString @messageTextContentID, @messageText
	
    UPDATE dbo.BulletinBoard
    SET
        PostDate = GetDate(),
        Author = @authorID
    WHERE
        BulletinBoard.[ID] = @id
	
	RETURN
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[dcetools_Trainings_Announcements_INSERT]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Trainings_Announcements_INSERT]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dcetools_Trainings_Announcements_INSERT]
(
	@homeRegion uniqueidentifier,
	@trainingID uniqueidentifier,
	
	@messageText as nvarchar(MAX),
	@authorID as uniqueidentifier
)
AS

BEGIN
	SET NOCOUNT ON;
	
	EXEC dbo.dcetools_Helpers_CheckObjectIDNull @trainingID, ''@trainingID''
	EXEC dbo.dcetools_Helpers_CheckObjectIDNull @authorID, ''@authorID''
	
    EXEC dbo.dcetools_Access_CheckObjectID_Write
        @homeRegion,
        @trainingID
        
    DECLARE
        @id uniqueidentifier,
        @messageTextContentID uniqueidentifier,
        @bulletinTrainingID uniqueidentifier
        
    SELECT
        @messageTextContentID = newid(),
        @id = newid()
        
    EXEC dbo.dcetools_Content_SetString @messageTextContentID, @messageText
	
    INSERT INTO dbo.BulletinBoard([ID], PostDate, Author, Message, Training )
    VALUES(@id, GetDate(), @authorID, @messageTextContentID, @trainingID )
	
	RETURN
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[dcetools_Regions_CreateRegion]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Regions_CreateRegion]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dcetools_Regions_CreateRegion]
(
    @regionID uniqueidentifier,
    @regionName nvarchar(255)
)
AS

BEGIN
	SET NOCOUNT ON;
	
	DECLARE @decl uniqueidentifier
	
	
    EXEC dbo.dcetools_Content_SetString @regionID, @regionName
    SELECT @decl = newid()
    EXEC dbo.dcetools_Regions_SetObjectRegion NULL, @decl, @regionID

END' 
END
GO
/****** Object:  StoredProcedure [dbo].[dcetools_Trainings_Tasks_CreateTask]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Trainings_Tasks_CreateTask]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dcetools_Trainings_Tasks_CreateTask]
(
	@homeRegion uniqueidentifier,
	@trainingID uniqueidentifier,
	@userID uniqueidentifier,
	@name nvarchar(255),
	@description nvarchar(255)
)
AS

BEGIN
	SET NOCOUNT ON;
	
    EXEC dbo.dcetools_Access_CheckObjectID_Write
        @homeRegion,
        @trainingID
        
    DECLARE
        @nameContentID uniqueidentifier,
        @descriptionContentID uniqueidentifier
        
    SELECT
        @nameContentID = newid(),
        @descriptionContentID = newid()
        
    EXEC dbo.dcetools_Content_SetString @nameContentID, @name
    EXEC dbo.dcetools_Content_SetString @descriptionContentID, @description

    INSERT INTO dbo.Tasks(
        [ID],
        Training,
        Creator,
        
        [Type],
        
        [Name],
        Description )
    VALUES(
        newid(),
        
        @trainingID,
        @userID,
        
        NULL,
        
        @nameContentID,
        @descriptionContentID )
     
	
	RETURN
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[dcetools_Orders_GetOrderAvailableTrainingList]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Orders_GetOrderAvailableTrainingList]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dcetools_Orders_GetOrderAvailableTrainingList]
	@homeRegion uniqueidentifier,
	@orderID uniqueidentifier
AS

BEGIN

SET NOCOUNT ON

    DECLARE
        @courseID uniqueidentifier,
        @studentID uniqueidentifier
        
    SELECT
        @courseID = dbo.dcetools_Fn_Orders_GetCourseID(@orderID),
        @studentID = dbo.dcetools_Fn_Orders_GetStudentID(@orderID)
        
    EXEC dbo.dcetools_Access_CheckObjectID_Read @homeRegion, @studentID
          

    SELECT
        Trainings.[ID] as [ID],
        dbo.dcetools_Fn_Content_GetString(Trainings.Name) as [Name],
        Trainings.StartDate as StartDate,
        Trainings.EndDate as EndDate

    FROM
        dbo.dcetools_Fn_Orders_GetCourseAvailableTrainingsWithDates(@homeRegion, @courseID) AvailableTrainings,
        Trainings
    WHERE
        Trainings.[ID] = AvailableTrainings.[ID]

END' 
END
GO
/****** Object:  StoredProcedure [dbo].[dcetools_Trainings_Students_SubscribeStudent]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Trainings_Students_SubscribeStudent]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dcetools_Trainings_Students_SubscribeStudent]
(
	@homeRegion uniqueidentifier,
	@trainingID uniqueidentifier,
	@studentID uniqueidentifier
)
AS

BEGIN
	SET NOCOUNT ON;
	
    -- Regional tutor can subscribe users of own region to global training
    EXEC dbo.dcetools_Access_CheckObjectID_Read
        @homeRegion,
        @trainingID

    EXEC dbo.dcetools_Access_CheckObjectID_Write
        @homeRegion,
        @studentID


DECLARE
    @trainingStudentsRef uniqueidentifier

SELECT @trainingStudentsRef  = Trainings.Students
FROM dbo.Trainings
WHERE Trainings.ID = @trainingID

INSERT INTO GroupMembers(MGroup, ID, mid)
VALUES (
    @trainingStudentsRef,
    @studentID,
    newid())



	RETURN
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[dcetools_Trainings_Students_UnsubscribeStudent]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Trainings_Students_UnsubscribeStudent]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dcetools_Trainings_Students_UnsubscribeStudent]
(
	@homeRegion uniqueidentifier,
	@trainingID uniqueidentifier,
	@studentID uniqueidentifier
)
AS

BEGIN
	SET NOCOUNT ON;
	
DECLARE
    @trainingStudentsRef uniqueidentifier

    -- Regional tutor can subscribe users of own region to global training
    EXEC dbo.dcetools_Access_CheckObjectID_Read
        @homeRegion,
        @trainingID

    EXEC dbo.dcetools_Access_CheckObjectID_Write
        @homeRegion,
        @studentID


    SELECT @trainingStudentsRef  = Trainings.Students
    FROM dbo.Trainings
    WHERE Trainings.ID = @trainingID
   
   
    IF NOT EXISTS(
        SELECT [ID] FROM dbo.GroupMembers
        WHERE
            GroupMembers.MGroup = @trainingStudentsRef
        AND
            (GroupMembers.mid = @studentID OR GroupMembers.[ID] = @studentID))
    BEGIN

        DECLARE @studentNotSubscribedErrorMessage nvarchar(255)
        SELECT @studentNotSubscribedErrorMessage =
            ''Student not subscribed to Trainings: ''+
            ''@studentID=''+CAST(@studentID as nvarchar(255))+'', ''+
            ''@trainingID=''+CAST(@trainingID as nvarchar(255))
                
        RAISERROR (@studentNotSubscribedErrorMessage, 16, 1)
        ROLLBACK TRAN

    END

    DELETE FROM dbo.GroupMembers
    WHERE
        GroupMembers.MGroup = @trainingStudentsRef
    AND
        (GroupMembers.mid = @studentID OR GroupMembers.[ID] = @studentID)



	RETURN
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[dcetools_Trainings_GetDetails]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Trainings_GetDetails]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dcetools_Trainings_GetDetails]
(
	@homeRegion uniqueidentifier,
	@trainingID uniqueidentifier
)
AS

BEGIN
	SET NOCOUNT ON;

    EXEC dbo.dcetools_Access_CheckObjectID_Read
        @homeRegion,
        @trainingID
        
    SELECT
        Trainings.ID as [ID],
        Trainings.Code as Code,
        dbo.dcetools_Fn_Content_GetString(Trainings.Name) as [Name],
        
        dbo.dcetools_Fn_Content_GetText(Trainings.Comment) as Comments,
        
        Trainings.Course as CourseID,
        (SELECT dbo.dcetools_Fn_Content_GetString(Courses.[Name])
        FROM dbo.Courses
        WHERE Courses.[ID] = Trainings.Course) as CourseName,
        
        Trainings.StartDate as StartDate,
        Trainings.EndDate as EndDate,
        
        Trainings.isPublic as IsPublic,
        Trainings.isActive as IsActive,
        Trainings.TestOnly as TestOnly,
        Trainings.Expires as Expires,        

        dbo.dcetools_Fn_Regions_GetObjectRegionID( Trainings.[ID] ) as RegionID,
        dbo.dcetools_Fn_Regions_GetObjectRegionName( Trainings.[ID] ) as RegionName
        
    FROM
        dbo.Trainings
        
    WHERE
        Trainings.ID = @trainingID

	
	RETURN
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[dcetools_Orders_RejectOrder]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Orders_RejectOrder]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dcetools_Orders_RejectOrder]
(
	@homeRegion uniqueidentifier,
	@orderID uniqueidentifier
)
AS

BEGIN
	SET NOCOUNT ON;
	
    DECLARE
        @studentID uniqueidentifier

    SELECT
        @studentID = dbo.dcetools_Fn_Orders_GetStudentID(@orderID)
        
    EXEC dbo.dcetools_Access_CheckObjectID_Write
        @homeRegion,
        @studentID

    DELETE FROM dbo.CourseRequest
    WHERE CourseRequest.[ID] = @orderID

	RETURN
END' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[dcetools_Fn_Orders_GetBestTrainingDate]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Fn_Orders_GetBestTrainingDate]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE FUNCTION [dbo].[dcetools_Fn_Orders_GetBestTrainingDate]
(
	@homeRegion uniqueidentifier,
	@orderID uniqueidentifier
)
RETURNS nvarchar(255)
AS

BEGIN
    
    DECLARE
        @requestDate datetime,
        @courseID uniqueidentifier,
        @diffRequestDate int
        
    SELECT
        @requestDate = CourseRequest.RequestDate,
        @courseID = CourseRequest.Course
    FROM
        dbo.CourseRequest
    WHERE
        @orderID = CourseRequest.[ID]         


    SELECT
        @diffRequestDate = MIN( ABS(DATEDIFF(
            dd,
            @requestDate,
            AvailableTrainings.StartDate)) )
        
    FROM
        dcetools_Fn_Orders_GetCourseAvailableTrainingsWithDates( @homeRegion, @courseID ) AvailableTrainings
    
	RETURN
	    (SELECT
	        MIN( AvailableTrainings.StartDate )
	    FROM
            dcetools_Fn_Orders_GetCourseAvailableTrainingsWithDates( @homeRegion, @courseID ) AvailableTrainings
	    WHERE
	        ABS(DATEDIFF(
                dd,
                @requestDate,
                AvailableTrainings.StartDate)) = @diffRequestDate)
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[dcetools_Trainings_Forum_PostMessage]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Trainings_Forum_PostMessage]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dcetools_Trainings_Forum_PostMessage]
	@homeRegion uniqueidentifier,
    @trainingID uniqueidentifier,
    @topicID uniqueidentifier,
   
    @authorID uniqueidentifier,
    @messageText nvarchar(max)

AS

BEGIN
	SET NOCOUNT ON;

    -- Forum is open for write for all users that can see training
    EXEC dbo.dcetools_Access_CheckObjectID_Read
        @homeRegion,
        @trainingID
        
    IF NOT EXISTS(
        SELECT [ID]
        FROM dbo.ForumTopics
        WHERE
            ForumTopics.[ID] = @topicID
        AND
            ForumTopics.Training = @trainingID)
    BEGIN

        DECLARE @topicNotInForumErrorMessage nvarchar(255)
        SELECT @topicNotInForumErrorMessage =
            ''Topic is not from Forum specified. ''+
            ''@topicID: ''+CAST(@topicID AS nvarchar(255))+'', ''+
            ''@trainingID: ''+CAST(@trainingID AS nvarchar(255))+''.''

        RAISERROR (@topicNotInForumErrorMessage, 16, 1)
        ROLLBACK TRAN

    END 
   
    DECLARE
        @messageID uniqueidentifier
        
    SELECT
        @messageID = newid()
    

    INSERT INTO dbo.ForumReplies(
        [ID],
        Topic,
        Author,
        [Message],
        PostDate )
        
    VALUES( 
        @messageID,
        @topicID,
        @authorID,
        @messageText,
        GetDate() )

END' 
END
GO
/****** Object:  StoredProcedure [dbo].[dcetools_Trainings_Forum_GetTopicMessages]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Trainings_Forum_GetTopicMessages]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dcetools_Trainings_Forum_GetTopicMessages]
	@homeRegion uniqueidentifier,
    @trainingID uniqueidentifier,
    @topicID uniqueidentifier 
AS

BEGIN
	SET NOCOUNT ON;

    EXEC dbo.dcetools_Access_CheckObjectID_Read
        @homeRegion,
        @trainingID
        
    IF NOT EXISTS(
        SELECT [ID]
        FROM dbo.ForumTopics
        WHERE
            ForumTopics.[ID] = @topicID
        AND
            ForumTopics.Training = @trainingID)
    BEGIN

        DECLARE @topicNotInForumErrorMessage nvarchar(255)
        SELECT @topicNotInForumErrorMessage =
            ''Topic is not from Forum specified. ''+
            ''@topicID: ''+CAST(@topicID AS nvarchar(255))+'', ''+
            ''@trainingID: ''+CAST(@trainingID AS nvarchar(255))+''.''

        RAISERROR (@topicNotInForumErrorMessage, 16, 1)
        ROLLBACK TRAN

    END 

    SELECT
        ForumReplies.[ID] as [ID],
        
        ForumReplies.Author as AuthorID,
        dbo.dcetools_Fn_Users_GetStudentOrUserFullName(ForumReplies.Author) as AuthorName,
        
        ForumReplies.[Message] as [Message],
        ForumReplies.PostDate as PostDate

    FROM
        dbo.ForumReplies
        
    WHERE
        ForumReplies.Topic = @topicID
        
    ORDER BY
        PostDate desc 

END' 
END
GO
/****** Object:  StoredProcedure [dbo].[dcetools_Trainings_Forum_GetTopicFirstMessage]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Trainings_Forum_GetTopicFirstMessage]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dcetools_Trainings_Forum_GetTopicFirstMessage]
	@homeRegion uniqueidentifier,
    @trainingID uniqueidentifier,
    @topicID uniqueidentifier
AS

BEGIN
	SET NOCOUNT ON;

    EXEC dbo.dcetools_Access_CheckObjectID_Read
        @homeRegion,
        @trainingID

    SELECT
        ForumTopics.[ID] as [ID],
        
        ISNULL(ForumTopics.Student, ForumTopics.Author) as StudentID,
        ISNULL(
        dbo.dcetools_Fn_Students_GetStudentFullName(ForumTopics.Student),
        dbo.dcetools_Fn_Users_GetStudentOrUserFullName(ForumTopics.Author)) as StudentFullName,
        
        ForumTopics.Topic as Topic,
        ForumTopics.[Message] as [Message],
        ISNULL(
            dbo.dcetools_Fn_Trainings_Forum_GetTopicLastPostDate(ForumTopics.[ID]),
            ForumTopics.PostDate) as LastPostDate,
        
        dbo.dcetools_Fn_Trainings_Forum_GetTopicMessageCount(ForumTopics.[ID]) as MessageCount,
        
        ForumTopics.Blocked as Blocked

    FROM
        ForumTopics

    WHERE
        ForumTopics.Training = @trainingID
    AND
        ForumTopics.[ID] = @topicID 

END' 
END
GO
/****** Object:  StoredProcedure [dbo].[dcetools_Trainings_Forum_GetTopicList]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Trainings_Forum_GetTopicList]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dcetools_Trainings_Forum_GetTopicList]
	@homeRegion uniqueidentifier,
    @trainingID uniqueidentifier
AS

BEGIN
	SET NOCOUNT ON;

    EXEC dbo.dcetools_Access_CheckObjectID_Read
        @homeRegion,
        @trainingID

    SELECT
        ForumTopics.[ID] as [ID],
        
        ISNULL(ForumTopics.Student, ForumTopics.Author) as StudentID,
        ISNULL(
        dbo.dcetools_Fn_Students_GetStudentFullName(ForumTopics.Student),
        dbo.dcetools_Fn_Users_GetStudentOrUserFullName(ForumTopics.Author)) as StudentFullName,
        
        ForumTopics.Topic as Topic,
        ForumTopics.[Message] as [Message],
        ISNULL(
            dbo.dcetools_Fn_Trainings_Forum_GetTopicLastPostDate(ForumTopics.[ID]),
            ForumTopics.PostDate) as LastPostDate,
        
        dbo.dcetools_Fn_Trainings_Forum_GetTopicMessageCount(ForumTopics.[ID]) as MessageCount,
        
        ForumTopics.Blocked as Blocked

    FROM
        ForumTopics

    WHERE
        ForumTopics.Training = @trainingID
        
    ORDER BY
        LastPostDate desc 

END' 
END
GO
/****** Object:  StoredProcedure [dbo].[dcetools_Trainings_Forum_CreateTopic]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Trainings_Forum_CreateTopic]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dcetools_Trainings_Forum_CreateTopic]
	@homeRegion uniqueidentifier,
    @trainingID uniqueidentifier,
    @title nvarchar(255),
    @text nvarchar(MAX),
    @authorID uniqueidentifier 
AS

BEGIN
	SET NOCOUNT ON;

    EXEC dbo.dcetools_Access_CheckObjectID_Read
        @homeRegion,
        @trainingID

    INSERT INTO dbo.ForumTopics([ID], Topic, [Message], Author, Training)
    VALUES( newid(), @title, @text, @authorID, @trainingID )

END' 
END
GO
/****** Object:  StoredProcedure [dbo].[dcetools_Access_Users_AddUserToGroup]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Access_Users_AddUserToGroup]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dcetools_Access_Users_AddUserToGroup]
(
    @homeRegion uniqueidentifier,
	@userID uniqueidentifier,
	@groupID uniqueidentifier
)
AS

BEGIN
	SET NOCOUNT ON;

    EXEC dbo.dcetools_Access_CheckObjectID_Write @homeRegion, @userID
	
	
    IF NOT EXISTS(
        SELECT GroupMembers.[ID]
        FROM dbo.GroupMembers
        WHERE GroupMembers.mid=@userID AND GroupMembers.MGroup=@groupID)
    BEGIN

        INSERT INTO GroupMembers( [ID], mid, MGroup )
        VALUES ( @userID, newid(), @groupID )

    END 
	
	RETURN
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[dcetools_Access_Users_GetUserNotMemberGroups]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Access_Users_GetUserNotMemberGroups]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dcetools_Access_Users_GetUserNotMemberGroups]
(
	@homeRegion uniqueidentifier,
	@userID uniqueidentifier
)
AS

BEGIN
	SET NOCOUNT ON;
	
	EXEC dbo.dcetools_Access_CheckObjectID_Read @homeRegion, @userID
	
	DECLARE
	    @studentID uniqueidentifier
	    
    SELECT
        Groups.[id] as [ID],
        Groups.[Name] as [Name],
        Groups.[Type] as [Type]
        
    FROM
        dbo.Groups
        
    WHERE
        Groups.[ID] NOT IN (
            SELECT GroupMembers.MGroup
            FROM dbo.GroupMembers
            WHERE GroupMembers.[ID] = @userID)
    AND
        Groups.Type>3
	
	RETURN
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[dcetools_Access_Users_GetUserGroups]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Access_Users_GetUserGroups]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dcetools_Access_Users_GetUserGroups]
(
    @homeRegion uniqueidentifier,
	@userID uniqueidentifier
)
AS

BEGIN
	SET NOCOUNT ON;
	
	EXEC dbo.dcetools_Access_CheckObjectID_Read @homeRegion, @userID
	
	DECLARE
	    @studentID uniqueidentifier
	    
    SELECT
        Groups.[id] as [ID],
        Groups.[Name] as [Name],
        Groups.[Type] as [Type]
        
    FROM
        dbo.Groups, dbo.GroupMembers
        
    WHERE
        GroupMembers.[ID] = @userID
    AND
        GroupMembers.MGroup = Groups.[id]
    AND
        Groups.Type>3
	
	RETURN
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[dcetools_Access_Users_RemoveUserFromGroup]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Access_Users_RemoveUserFromGroup]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dcetools_Access_Users_RemoveUserFromGroup]
(
    @homeRegion uniqueidentifier,
	@userID uniqueidentifier,
	@roleID uniqueidentifier
)
AS

BEGIN
	SET NOCOUNT ON;
	
    EXEC dbo.dcetools_Access_CheckObjectID_Write @homeRegion, @userID



    IF EXISTS(
        SELECT GroupMembers.[ID]
        FROM dbo.GroupMembers
        WHERE GroupMembers.[ID]=@userID AND GroupMembers.MGroup=@roleID)
    BEGIN

        DELETE FROM dbo.GroupMembers
        WHERE GroupMembers.[ID]=@userID AND GroupMembers.MGroup=@roleID

    END 
    ELSE
    BEGIN
    
        DECLARE @userNotInGroupErrorMessage nvarchar(255)
        SELECT @userNotInGroupErrorMessage =
            ''User is not a member of Group.'' +
            ''@userID = ''+CAST(@userID as nvarchar(255)) +'', ''+
            ''@roleID = ''+CAST(@roleID as nvarchar(255)) +''.''
                
        RAISERROR (@userNotInGroupErrorMessage, 16, 1)
        ROLLBACK TRAN
    
    END
	
	RETURN
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[dcetools_Trainings_Schedule_UpdateThemeSchedule]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Trainings_Schedule_UpdateThemeSchedule]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dcetools_Trainings_Schedule_UpdateThemeSchedule]
(
	@homeRegion uniqueidentifier,
	@trainingID uniqueidentifier,
	@id uniqueidentifier,
	@startDate datetime,
	@endDate datetime,
	@isOpen bit,
	@mandatory bit
)
AS

BEGIN
	SET NOCOUNT ON;
	
    EXEC dbo.dcetools_Access_CheckObjectID_Write
        @homeRegion,
        @trainingID
        
        
    IF @endDate<@startDate
    BEGIN
        DECLARE @tmpDate datetime
        SELECT
            @tmpDate = @endDate
        SELECT
            @endDate = @startDate
        SELECT
            @startDate = @tmpDate
    END
        
        
    DECLARE
        @themeID uniqueidentifier,
        @scheduleID uniqueidentifier
        
    SELECT 
        @themeID = @id,
        @scheduleID = dbo. dcetools_Fn_Trainings_Schedule_GetTrainingThemeScheduleID(
                @trainingID,
                @themeID)
   
    IF @scheduleID IS NULL
    BEGIN
        SELECT
            @scheduleID = newid()
    
        INSERT INTO Schedule(
            [ID],
            StartDate,
            EndDate,
            Mandatory,
            isOpen,
            
            Theme,
            Training)
        VALUES(
            @scheduleID,
            @startDate,
            @endDate,
            @mandatory,
            @isOpen,
            
            @themeID,
            @trainingID )
        
    END
    ELSE   
    BEGIN
        UPDATE dbo.Schedule
        SET
            Schedule.StartDate = @startDate,
            Schedule.EndDate = @endDate,
            Schedule.Mandatory = @mandatory,
            Schedule.isOpen = @isOpen
        WHERE 
            Schedule.[ID] = @scheduleID
    END
   
	RETURN
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[dcetools_Trainings_Tasks_GetTaskList]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Trainings_Tasks_GetTaskList]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dcetools_Trainings_Tasks_GetTaskList]
(
	@homeRegion uniqueidentifier,
	@trainingID uniqueidentifier
)
AS

BEGIN
	SET NOCOUNT ON;
	
    EXEC dbo.dcetools_Access_CheckObjectID_Read
        @homeRegion,
        @trainingID

    SELECT
        Tasks.[id] as [ID],
        dbo.dcetools_Fn_Content_GetString(Tasks.[Name]) as [Name],
        dbo.dcetools_Fn_Content_GetText(Tasks.Description) as Description,
        Tasks.TaskTime as TaskTime
    FROM
        dbo.Tasks
    WHERE
        Training=@trainingID
	
	RETURN
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[dcetools_Trainings_Tasks_GetTaskSolutions]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Trainings_Tasks_GetTaskSolutions]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dcetools_Trainings_Tasks_GetTaskSolutions]
(
	@homeRegion uniqueidentifier,
	@taskID uniqueidentifier
)
AS

BEGIN
	SET NOCOUNT ON;
	
    DECLARE
        @trainingID uniqueidentifier
        
    SELECT
        @trainingID = Tasks.Training
    FROM
        dbo.Tasks
    WHERE
        Tasks.[ID] = @taskID
        
    EXEC dbo.dcetools_Access_CheckObjectID_Read
        @homeRegion,
        @trainingID

    SELECT     
        TaskSolutions.[id] as [ID],
        TaskSolutions.Student as StudentID,
        dbo.dcetools_Fn_Students_GetStudentFullName(TaskSolutions.Student) as StudentName,
        TaskSolutions.Complete as Complete,
        TaskSolutions.Solution as SolutionText,
        TaskSolutions.SDate as SolutionDate
    FROM         
        dbo.TaskSolutions
    WHERE
        TaskSolutions.Task = @taskID
        
	
	RETURN
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[dcetools_Trainings_Tests_AllowRetry]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Trainings_Tests_AllowRetry]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dcetools_Trainings_Tests_AllowRetry]
(
	@homeRegion uniqueidentifier,
	@testResultID as uniqueidentifier
)
AS

BEGIN
	SET NOCOUNT ON;
	
	DECLARE
	    @studentID uniqueidentifier
	    
	SELECT
	    @studentID = TestResults.Student
	FROM
	    dbo.TestResults
	WHERE
	    @testResultID = TestResults.[ID]
	
	EXEC dbo.dcetools_Access_CheckObjectID_Write
        @homeRegion,
        @studentID
		
	
	UPDATE dbo.TestResults
	SET
	    TestResults.AllowTries = TestResults.Tries+1
	WHERE
        TestResults.AllowTries <= TestResults.Tries
    AND
        TestResults.[ID] = @testResultID
    AND 
        TestResults.Student = @studentID 
	
	
	RETURN
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[dcetools_Access_Users_UserTitle]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Access_Users_UserTitle]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dcetools_Access_Users_UserTitle]
(
    @homeRegion uniqueidentifier,
	@userID uniqueidentifier
)
AS

BEGIN
	SET NOCOUNT ON;

    EXEC dbo.dcetools_Access_CheckObjectID_Read @homeRegion, @userID

    SELECT
        Users.Login as Login,
        dbo.dcetools_Fn_Students_GetStudentFullName(Users.[ID]) as FullName,
        Users.Email as EMail,
        Users.JobPosition as JobPosition,
        Users.Comments as Comments,
        
        Users.CreateDate as CreateDate,
        Users.LastModifyDate as LastModify,
        
        dbo.dcetools_Fn_Regions_GetObjectRegionName( Users.[ID] ) as RegionName,
        
        (SELECT dbo.dcetools_Fn_Content_GetString(UserRoles.[Role])
        FROM dbo.UserRoles
        WHERE UserRoles.UserID = Users.[ID]) as RoleName
        
    FROM
        dbo.Users
        
    WHERE
        Users.[ID] = @userID
	
	RETURN
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[dcetools_Access_Users_GetUserRole]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Access_Users_GetUserRole]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dcetools_Access_Users_GetUserRole]
(
    @homeRegion uniqueidentifier,
	@userID uniqueidentifier
)
AS

BEGIN
	SET NOCOUNT ON;
	
	EXEC dbo.dcetools_Access_CheckObjectID_Read @homeRegion, @userID
	

    SELECT
        UserRoles.[Role] as [ID],
        dbo.dcetools_Fn_Content_GetString(UserRoles.[Role]) as [Name]
    
    FROM dbo.UserRoles
    
    WHERE UserRoles.UserID = @userID
	
	RETURN
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[dcetools_Trainings_Announcements_DELETE]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Trainings_Announcements_DELETE]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dcetools_Trainings_Announcements_DELETE]
(
	@homeRegion uniqueidentifier,
	@announcementID uniqueidentifier
)
AS

BEGIN
	SET NOCOUNT ON;
	
	EXEC dbo.dcetools_Helpers_CheckObjectIDNull @announcementID, ''@announcementID''
	
	DECLARE
	    @trainingID uniqueidentifier
	    
	SELECT @trainingID = BulletinBoard.Training
	FROM dbo.BulletinBoard
	WHERE BulletinBoard.[ID] = @announcementID
	
    EXEC dbo.dcetools_Access_CheckObjectID_Write
        @homeRegion,
        @trainingID
        
    DELETE FROM dbo.BulletinBoard
    WHERE BulletinBoard.[ID] = @announcementID
	
	RETURN
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[dcetools_News_GetDetails]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_News_GetDetails]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dcetools_News_GetDetails]
(
	@homeRegion uniqueidentifier,
	@newsID uniqueidentifier
)
AS

BEGIN
	SET NOCOUNT ON;

    EXEC dbo.dcetools_Access_CheckObjectID_Read
        @homeRegion,
        @newsID

    SELECT
        News.[ID] as [ID], 
        News.NewsDate as [Date], 
        dbo.dcetools_Fn_Content_GetString(News.Head) as Title,
        dbo.dcetools_Fn_Content_GetText(News.[Text]) as ContentText,
        
        dbo.dcetools_Fn_Content_GetImage(News.[Image]) as [Image],
        
        News.MoreHref as Href,
        
        dbo.dcetools_Fn_Regions_GetObjectRegionID( News.[ID] ) as RegionID,
        dbo.dcetools_Fn_Regions_GetObjectRegionName( News.[ID] ) as RegionName
        
    FROM
        dbo.News
        
    WHERE
        News.[ID] = @newsID
    AND
    
       dbo.dcetools_Fn_Regions_ObjectCanReadByHome( News.[ID], @homeRegion ) = 1
	
	
	RETURN
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[dcetools_Trainings_Students_GetTrainingStudents]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Trainings_Students_GetTrainingStudents]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dcetools_Trainings_Students_GetTrainingStudents]
(
	@homeRegion uniqueidentifier,
	@trainingID uniqueidentifier
)
AS

BEGIN
	SET NOCOUNT ON;
	
    EXEC dbo.dcetools_Access_CheckObjectID_Read
        @homeRegion,
        @trainingID
	
    SELECT
        Students.ID,
        dbo.dcetools_Fn_Util_CombineFullName(
            Students.FirstName,
            Students.Patronymic,
            Students.LastName) as FullName,
        Students.EMail,
        Students.JobPosition as [Position],
        
        dbo.dcetools_Fn_Regions_GetObjectRegionID(Students.[ID]) as RegionID,
        dbo.dcetools_Fn_Regions_GetObjectRegionName( Students.[ID] ) as RegionName

    FROM
        Students

    WHERE
    (
        @homeRegion IS NULL
    OR
        dbo.dcetools_Fn_Regions_GetObjectRegionID(Students.[ID]) = @homeRegion
    )
        
    AND
        Students.[ID] IN (SELECT [ID] FROM dbo.dcetools_Fn_Trainings_Students_GetIDList(@trainingID))
	
	RETURN
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[dcetools_Trainings_Students_GetNonTrainingStudents]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Trainings_Students_GetNonTrainingStudents]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dcetools_Trainings_Students_GetNonTrainingStudents]
(
	@homeRegion uniqueidentifier,
	@trainingID uniqueidentifier
)
AS

BEGIN
	SET NOCOUNT ON;
	
    EXEC dbo.dcetools_Access_CheckObjectID_Read
        @homeRegion,
        @trainingID
	
    SELECT
        Students.ID,
        dbo.dcetools_Fn_Util_CombineFullName(
            Students.FirstName,
            Students.Patronymic,
            Students.LastName) as FullName,
        Students.EMail,
        Students.JobPosition as [Position],
        
        dbo.dcetools_Fn_Regions_GetObjectRegionID(Students.[ID]) as RegionID,
        dbo.dcetools_Fn_Regions_GetObjectRegionName( Students.[ID] ) as RegionName

    FROM
        Students

    WHERE
        dbo.dcetools_Fn_Regions_ObjectCanReadByHome(Students.[ID], @homeRegion) = 1
        
    AND
        Students.[ID] NOT IN (SELECT [ID] FROM dbo.dcetools_Fn_Trainings_Students_GetIDList(@trainingID))
	
	RETURN
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[dcetools_Trainings_Announcements_GetAnnouncementList]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Trainings_Announcements_GetAnnouncementList]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dcetools_Trainings_Announcements_GetAnnouncementList]
(
	@homeRegion uniqueidentifier,
	@trainingID uniqueidentifier
)
AS

BEGIN
	SET NOCOUNT ON;
	
    EXEC dbo.dcetools_Access_CheckObjectID_Read
        @homeRegion,
        @trainingID
	
    SELECT
        BulletinBoard.[ID] as [ID],
        BulletinBoard.PostDate as PostDate,
        dbo.dcetools_Fn_Students_GetStudentFullName(Author) as AuthorName,
        ISNULL(dbo.dcetools_Fn_Content_GetString(BulletinBoard.Message),''-'') as MessageText
    
    FROM
        dbo.BulletinBoard
    WHERE
        BulletinBoard.Training = @trainingID
    OR
        @trainingID IS NULL
        
    ORDER BY
        BulletinBoard.PostDate desc        
	
	
	RETURN
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[dcetools_Trainings_Announcements_GetDetails]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Trainings_Announcements_GetDetails]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dcetools_Trainings_Announcements_GetDetails]
(
	@homeRegion uniqueidentifier,
	@trainingID uniqueidentifier,
	@announcementID uniqueidentifier
)
AS

BEGIN
	SET NOCOUNT ON;
	
    EXEC dbo.dcetools_Access_CheckObjectID_Read
        @homeRegion,
        @trainingID
        
    DECLARE
        @messageTextContentID uniqueidentifier,
        @bulletinTrainingID uniqueidentifier
        
    SELECT
        @messageTextContentID = BulletinBoard.Message,
        @bulletinTrainingID = BulletinBoard.Training
    FROM
        dbo.BulletinBoard
    WHERE
        BulletinBoard.[ID] = @announcementID
        
    IF @bulletinTrainingID <> @trainingID
    BEGIN
    
        DECLARE @bulletinTrainingNotCorrelatedErrorMessage nvarchar(255)
        SELECT @bulletinTrainingNotCorrelatedErrorMessage =
            ''Training does not contains Announcement ID specified.''                
        RAISERROR (@bulletinTrainingNotCorrelatedErrorMessage, 16, 1)
        ROLLBACK TRAN
    
    END
	
    SELECT
        BulletinBoard.[ID] as [ID],
        BulletinBoard.PostDate as PostDate,
        dbo.dcetools_Fn_Students_GetStudentFullName(Author) as AuthorName,
        dbo.dcetools_Fn_Content_GetString(BulletinBoard.Message) as MessageText
    
    FROM
        dbo.BulletinBoard
    WHERE
        BulletinBoard.[ID] = @announcementID
	
	
	RETURN
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[dcetools_Trainings_Students_FindNonTrainingStudents]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Trainings_Students_FindNonTrainingStudents]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dcetools_Trainings_Students_FindNonTrainingStudents]
(
	@homeRegion uniqueidentifier,
	@trainingID uniqueidentifier,
	@searchString nvarchar(255)
)
AS

BEGIN
	SET NOCOUNT ON;
	
    EXEC dbo.dcetools_Access_CheckObjectID_Read
        @homeRegion,
        @trainingID
	
    SELECT
        Students.*

    FROM
        dbo.dcetools_Fn_Access_FindUsersByAny(@homeRegion,@searchString) Students

    WHERE
    (
        @homeRegion IS NULL
    OR
        dbo.dcetools_Fn_Regions_GetObjectRegionID(Students.[ID]) = @homeRegion
    )
        
    AND
        Students.[ID] NOT IN (SELECT [ID] FROM dbo.dcetools_Fn_Trainings_Students_GetIDList(@trainingID))
	
	RETURN
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[dcetools_Trainings_Students_FindTrainingStudents]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Trainings_Students_FindTrainingStudents]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dcetools_Trainings_Students_FindTrainingStudents]
(
	@homeRegion uniqueidentifier,
	@trainingID uniqueidentifier,
	@searchString nvarchar(255)
)
AS

BEGIN
	SET NOCOUNT ON;
	
    EXEC dbo.dcetools_Access_CheckObjectID_Read
        @homeRegion,
        @trainingID
	
    SELECT
        Students.*

    FROM
        dbo.dcetools_Fn_Access_FindUsersByAny(@homeRegion,@searchString) Students

    WHERE
    (
        @homeRegion IS NULL
    OR
        dbo.dcetools_Fn_Regions_GetObjectRegionID(Students.[ID]) = @homeRegion
    )
        
    AND
        Students.[ID] IN (SELECT [ID] FROM dbo.dcetools_Fn_Trainings_Students_GetIDList(@trainingID))
	
	RETURN
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[dcetools_News_Delete]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_News_Delete]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dcetools_News_Delete]
(
	@homeRegion uniqueidentifier,
	@newsID uniqueidentifier
)
AS

BEGIN
	SET NOCOUNT ON;

    EXEC dbo.dcetools_Access_CheckObjectID_Write
        @homeRegion,
        @newsID
        
    IF NOT EXISTS( SELECT News.[ID] FROM dbo.News WHERE News.[ID] = @newsID)
    BEGIN

        DECLARE @noNewsFoundErrorMessage nvarchar(255)
        SELECT @noNewsFoundErrorMessage =
            ''News.ID not found: ''+ISNULL(CAST(@newsID as nvarchar(255)),''(null)'')+''.''
                
        RAISERROR (@noNewsFoundErrorMessage, 16, 1)
        ROLLBACK TRAN
                
    END

    DELETE FROM dbo.News
    WHERE
        News.[ID] = @newsID
	
	
	RETURN
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[dcetools_Orders_AcceptOrder]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Orders_AcceptOrder]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dcetools_Orders_AcceptOrder]
(
	@homeRegion uniqueidentifier,
	@orderID uniqueidentifier,
	@trainingID uniqueidentifier
)
AS

BEGIN
	SET NOCOUNT ON;
	
DECLARE
    @courseID uniqueidentifier,
    @studentID uniqueidentifier, 
    @trainingStudentsRef uniqueidentifier

    SELECT
        @courseID = dbo.dcetools_Fn_Orders_GetCourseID(@orderID),
        @studentID = dbo.dcetools_Fn_Orders_GetStudentID(@orderID)
        
    IF (SELECT Trainings.Course FROM dbo.Trainings WHERE Trainings.[ID] = @trainingID)
        <> @courseID
    BEGIN

        DECLARE @orderTrainingCourseMismatchErrorMessage nvarchar(255)
        SELECT @orderTrainingCourseMismatchErrorMessage =
            ''Order and Training reference different Course: ''+
            ''Order.Course=''+CAST(@courseID as nvarchar(255))+'', ''+
            ''Training.Course=''+CAST(
                (SELECT Trainings.Course FROM dbo.Trainings WHERE Trainings.[ID] = @trainingID)
                as nvarchar(255))
                
        RAISERROR (@orderTrainingCourseMismatchErrorMessage, 16, 1)
        ROLLBACK TRAN
                
    END  
        
    EXEC dbo.dcetools_Access_CheckObjectID_Write
        @homeRegion,
        @studentID

    -- Regional user can suscribe users of own region to global training.
    EXEC dbo.dcetools_Access_CheckObjectID_Read
        @homeRegion,
        @trainingID


    EXEC dcetools_Trainings_Students_SubscribeStudent @homeRegion, @trainingID, @studentID
   
    DELETE FROM dbo.CourseRequest
    WHERE CourseRequest.[ID] = @orderID

	RETURN
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[dcelegacy_Subscribe_GetMyOrderList]    Script Date: 03/16/2007 20:07:02 ******/
/*SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcelegacy_Subscribe_GetMyOrderList]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dcelegacy_Subscribe_GetMyOrderList]
	@studentID uniqueidentifier
AS

BEGIN

SET NOCOUNT ON

    SELECT
        CourseRequest.[ID] as [ID],
        
        CourseRequest.Student as StudentID,
        dbo.dcetools_Fn_Students_GetStudentFullName(CourseRequest.Student) as StudentName,
        
        CourseRequest.Course as CourseID,
        dbo.dcetools_Fn_Courses_GetCourseName(CourseRequest.Course) as CourseName,
        
        CourseRequest.RequestDate as RequestDate,
        CourseRequest.Comments as Comments,
        
        dbo.dcetools_Fn_Orders_GetBestTrainingDate(CourseRequest.[ID]) as BestAvailableTrainingDate

    FROM
        dbo.CourseRequest

    WHERE
        CourseRequest.Student = @studentID
        
    ORDER BY
        CASE
            WHEN dbo.dcetools_Fn_Orders_GetBestTrainingDate(CourseRequest.[ID]) IS NULL THEN 0
            ELSE 1
        END,
        dbo.dcetools_Fn_Orders_GetBestTrainingDate(CourseRequest.[ID]),
        CourseRequest.RequestDate

END' 
END
GO*/
/****** Object:  StoredProcedure [dbo].[dcetools_Orders_GetList]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Orders_GetList]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dcetools_Orders_GetList]
	@homeRegion uniqueidentifier
AS

BEGIN

SET NOCOUNT ON

    SELECT
        CourseRequest.[ID] as [ID],
        
        CourseRequest.Student as StudentID,
        dbo.dcetools_Fn_Students_GetStudentFullName(CourseRequest.Student) as StudentName,
        
        CourseRequest.Course as CourseID,
        dbo.dcetools_Fn_Courses_GetCourseName(CourseRequest.Course) as CourseName,
        
        CourseRequest.RequestDate as RequestDate,
        CourseRequest.Comments as Comments,
        
        dbo.dcetools_Fn_Orders_GetBestTrainingDate(@homeRegion, CourseRequest.[ID]) as BestAvailableTrainingDate

    FROM
        dbo.CourseRequest

    WHERE
        dbo.dcetools_Fn_Regions_ObjectCanWriteByHome(
            dbo.dcetools_Fn_Orders_GetStudentID( CourseRequest.[ID] ),
            @homeRegion ) = 1
        
    ORDER BY
        CASE
            WHEN dbo.dcetools_Fn_Orders_GetBestTrainingDate(@homeRegion,CourseRequest.[ID]) IS NULL THEN 0
            ELSE 1
        END,
        dbo.dcetools_Fn_Orders_GetBestTrainingDate(@homeRegion,CourseRequest.[ID]),
        CourseRequest.RequestDate

END' 
END
GO
/****** Object:  StoredProcedure [dbo].[dcetools_Orders_GetOrderDetails]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Orders_GetOrderDetails]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dcetools_Orders_GetOrderDetails]
	@homeRegion uniqueidentifier,
	@orderID uniqueidentifier
AS

BEGIN

SET NOCOUNT ON

    DECLARE
        @studentID uniqueidentifier

    SELECT
        @studentID = dbo.dcetools_Fn_Orders_GetStudentID(@orderID)
        
    EXEC dbo.dcetools_Access_CheckObjectID_Read
        @homeRegion,
        @studentID

    SELECT
        CourseRequest.[ID] as [ID],
        
        CourseRequest.Student as StudentID,
        dbo.dcetools_Fn_Students_GetStudentFullName(CourseRequest.Student) as StudentName,
        
        CourseRequest.Course as CourseID,
        dbo.dcetools_Fn_Courses_GetCourseName(CourseRequest.Course) as CourseName,
        
        CourseRequest.RequestDate as RequestDate,
        CourseRequest.Comments as Comments,
        
        dbo.dcetools_Fn_Orders_GetBestTrainingDate(@homeRegion, CourseRequest.[ID]) as BestAvailableTrainingDate

    FROM
        dbo.CourseRequest

    WHERE
        CourseRequest.[ID] = @orderID
        
    ORDER BY
        CASE
            WHEN dbo.dcetools_Fn_Orders_GetBestTrainingDate(@homeRegion, CourseRequest.[ID]) IS NULL THEN 0
            ELSE 1
        END,
        dbo.dcetools_Fn_Orders_GetBestTrainingDate(@homeRegion, CourseRequest.[ID]),
        CourseRequest.RequestDate

END' 
END
GO
/****** Object:  StoredProcedure [dbo].[dcetools_Trainings_Schedule_GetSchedule]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Trainings_Schedule_GetSchedule]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dcetools_Trainings_Schedule_GetSchedule]
(
	@homeRegion uniqueidentifier,
	@trainingID uniqueidentifier
)
AS

BEGIN
	SET NOCOUNT ON;
	
EXEC dbo.dcetools_Access_CheckObjectID_Read
    @homeRegion,
    @trainingID
   
DECLARE
    @courseID uniqueidentifier

SELECT
    @courseID = dbo.dcetools_Fn_Trainings_GetCourseID(@trainingID)

    SELECT
        TrainingCourseThemes.[ID] as [ID],
        TrainingCourseThemes.[Type] as [Type],
        TrainingCourseThemes.[Name] as [Name],
        TrainingCourseThemes.ParentThemeID as ParentThemeID,
        TrainingCourseThemes.Duration as Duration,
        TrainingCourseThemes.ContentHref as ContentHref,
        TrainingCourseThemes.OrderIndex as OrderIndex,
        
        (SELECT
            Schedule.StartDate
        FROM
            dbo.Schedule
        WHERE
            Schedule.[ID] =dbo. dcetools_Fn_Trainings_Schedule_GetTrainingThemeScheduleID(
                @trainingID,
                TrainingCourseThemes.[ID])) as StartDate,

        (SELECT
            Schedule.EndDate
        FROM
            dbo.Schedule
        WHERE
            Schedule.[ID] =dbo. dcetools_Fn_Trainings_Schedule_GetTrainingThemeScheduleID(
                @trainingID,
                TrainingCourseThemes.[ID])) as EndDate,

        (SELECT
            Schedule.isOpen
        FROM
            dbo.Schedule
        WHERE
            Schedule.[ID] =dbo. dcetools_Fn_Trainings_Schedule_GetTrainingThemeScheduleID(
                @trainingID,
                TrainingCourseThemes.[ID])) as IsOpen,
        
        ISNULL(
            (SELECT
                Schedule.Mandatory
            FROM
                dbo.Schedule
            WHERE
                Schedule.[ID] =dbo. dcetools_Fn_Trainings_Schedule_GetTrainingThemeScheduleID(
                    @trainingID,
                    TrainingCourseThemes.[ID])),
        TrainingCourseThemes.Mandatory ) as Mandatory

    FROM
        dbo.dcetools_Fn_Courses_Themes_GetCourseThemes( @courseID ) TrainingCourseThemes
        
    ORDER BY TrainingCourseThemes.OrderIndex
        

	
	RETURN
END
' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[dcetools_Fn_Courses_GetTestResultIdList]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcetools_Fn_Courses_GetTestResultIdList]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE FUNCTION [dbo].[dcetools_Fn_Courses_GetTestResultIdList]
(
	@courseID uniqueidentifier
)
RETURNS TABLE
AS
	
RETURN
	    SELECT
	        TestResults.[ID]

        FROM
            dbo.TestResults

        WHERE
            TestResults.Test IN
            (
                SELECT *
                FROM dbo.dcetools_Fn_Courses_GetTestIdList(@courseID)
            )' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[dcereports_Fn_Tests_GetTestResults]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcereports_Fn_Tests_GetTestResults]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE FUNCTION [dbo].[dcereports_Fn_Tests_GetTestResults]
(
	@courseID uniqueidentifier,
	@studentID uniqueidentifier
)
RETURNS TABLE
AS
	
RETURN
	    SELECT
	        TestResults.*,
	        Users.Comments,
	        Users.FullName,
	        Users.JobPosition,
	        Users.[ID] as UserID

        FROM
            dbo.TestResults,
            dbo.Users

        WHERE
        ( @courseID IS NULL OR
            TestResults.Test IN
            (
                SELECT *
                FROM dbo.dcetools_Fn_Courses_GetTestIdList(@courseID)
            )
        )
        AND
        ( @studentID IS NULL OR Users.[ID] = @studentID )
        AND
        ( TestResults.Student = Users.[ID] )' 
END
GO
/****** Object:  StoredProcedure [dbo].[MakeCourseCopy]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MakeCourseCopy]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[MakeCourseCopy]  
	@newid uniqueidentifier, 
	@oldid uniqueidentifier,
	@newVersion nvarchar(2),
	@newDiskFolder nvarchar(128)
AS

-----------------------------------------------------------
-- Создание новой записи курса --
-----------------------------------------------------------

Declare @newName uniqueidentifier
Set @newName = NEWID( )
Declare @oldName uniqueidentifier
Set @oldName = (select Name from Courses where id = @oldid)

Declare @newAuthor uniqueidentifier
Set @newAuthor = NEWID( )
Declare @oldAuthor uniqueidentifier
Set @oldAuthor = (select Author from Courses where id = @oldid)

Declare @newDescriptionShort uniqueidentifier
Set @newDescriptionShort = NEWID( )
Declare @oldDescriptionShort uniqueidentifier
Set @oldDescriptionShort = (select DescriptionShort from Courses where id = @oldid)

Declare @newDescriptionLong uniqueidentifier
Set @newDescriptionLong = NEWID( )
Declare @oldDescriptionLong uniqueidentifier
Set @oldDescriptionLong = (select DescriptionLong from Courses where id = @oldid)

Declare @newRequirements uniqueidentifier
Set @newRequirements = NEWID( )
Declare @oldRequirements uniqueidentifier
Set @oldRequirements = (select Requirements from Courses where id = @oldid)

Declare @newKeywords uniqueidentifier
Set @newKeywords = NEWID( )
Declare @oldKeywords uniqueidentifier
Set @oldKeywords = (select Keywords from Courses where id = @oldid)

Declare @newAdditions uniqueidentifier
Set @newAdditions = NEWID( )
Declare @oldAdditions uniqueidentifier
Set @oldAdditions = (select Additions from Courses where id = @oldid)

insert into dbo.Courses select @newid, @newVersion, code, @newName, area, type, cost1,costType1, 
cost2, costType2, @newAuthor, cpublic, @newDescriptionShort, @newDescriptionLong, 
@newRequirements, @newKeywords,  @newAdditions, NEWID(), StartQuestionnaire, FinishQuestionnaire, 
@newDiskFolder, 0, CourseLanguage  from Courses where id = @oldid

-- Копирование контента названия --
execute MakeContentCopy @newName, @oldName

-- Копирование контента содержащего имя автора(-ов) --
execute MakeContentCopy @newAuthor, @oldAuthor

-- Копирование контента содержащего короткую подсказку --
execute MakeContentCopy @newDescriptionShort, @oldDescriptionShort

-- Копирование контента содержащего длинную подсказку --
execute MakeContentCopy @newDescriptionLong, @oldDescriptionLong

-- Копирование контента содержащего требования --
execute MakeContentCopy @newRequirements, @oldRequirements

-- Копирование контента содержащего ключевые слова --
execute MakeContentCopy @newKeywords, @oldKeywords

-- Копирование контента содержащего дополнения --
execute MakeContentCopy @newAdditions, @oldAdditions

-----------------------------------------------------------------------------------------------
-- Копирование Заключительного теста (если он есть)  --
-----------------------------------------------------------------------------------------------
execute MakeTestOnlyCopy @newid, @oldid

-------------------------------------------------------
-- Копирование всех тем курса --
-------------------------------------------------------
execute MakeThemesCopy  @newid, @oldid

-------------------------------------------
-- Копирование словаря --
-------------------------------------------
execute MakeVocabularyCopy @newid, @oldid
' 
END
GO
/****** Object:  StoredProcedure [dbo].[tmp_CreateRegions]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tmp_CreateRegions]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[tmp_CreateRegions]
AS

BEGIN
	--SET NOCOUNT ON;
	
	DECLARE @decl uniqueidentifier
	
	
    EXEC dbo.dcetools_Regions_CreateRegion ''BBBBBBBB-D9B7-42E1-B6A1-FFFFB0E29B03'', ''Крым''
	EXEC dbo.dcetools_Regions_CreateRegion ''BBBBBBBB-D9B7-42E1-B6A1-FFFFB0E29B04'', ''Винница''

	EXEC dbo.dcetools_Regions_CreateRegion ''BBBBBBBB-D9B7-42E1-B6A1-FFFFB0E29B05'', ''Волынская''
    EXEC dbo.dcetools_Regions_CreateRegion ''BBBBBBBB-D9B7-42E1-B6A1-FFFFB0E29B06'', ''Днепропетровск''
EXEC dbo.dcetools_Regions_CreateRegion ''BBBBBBBB-D9B7-42E1-B6A1-FFFFB0E29B07'', ''Донецк''
EXEC dbo.dcetools_Regions_CreateRegion ''BBBBBBBB-D9B7-42E1-B6A1-FFFFB0E29B08'', ''Запорожье''
EXEC dbo.dcetools_Regions_CreateRegion ''BBBBBBBB-D9B7-42E1-B6A1-FFFFB0E29B09'', ''Ивано-Франковск''
EXEC dbo.dcetools_Regions_CreateRegion ''BBBBBBBB-D9B7-42E1-B6A1-FFFFB0E29B10'', ''Кировоград''



END' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[dcereports_Tests_GetMaxTestResultDate]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcereports_Tests_GetMaxTestResultDate]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE FUNCTION [dbo].[dcereports_Tests_GetMaxTestResultDate]
(
	@courseID uniqueidentifier
)
RETURNS datetime
AS
BEGIN
	RETURN 
	    (
	        SELECT MAX(TestResults.CompletionDate)
	        FROM
	            dbo.TestResults
	        WHERE
	            TestResults.[ID] IN 
	            (
	                SELECT *
	                FROM dbo.dcetools_Fn_Courses_GetTestResultIdList(@courseID)
	            )
	        AND
	            TestResults.Complete <>0 
	    )
END' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[dcereports_Tests_GetStudentMaxTestResultDate]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcereports_Tests_GetStudentMaxTestResultDate]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE FUNCTION [dbo].[dcereports_Tests_GetStudentMaxTestResultDate]
(
	@courseID uniqueidentifier,
	@studentID uniqueidentifier
)
RETURNS datetime
AS
BEGIN
	RETURN 
	    (
	        SELECT MAX(TestResults.CompletionDate)
	        FROM
	            dbo.TestResults
	        WHERE
	            TestResults.[ID] IN 
	            (
	                SELECT *
	                FROM dbo.dcetools_Fn_Courses_GetTestResultIdList(@courseID)
	            )
	        AND
	            TestResults.Complete <>0 
	        AND
	            TestResults.Student = @studentID 
	    )
END' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[dcereports_Tests_GetTestResultPassedCount]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcereports_Tests_GetTestResultPassedCount]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE FUNCTION [dbo].[dcereports_Tests_GetTestResultPassedCount]
(
	@courseID uniqueidentifier
)
RETURNS int
AS
BEGIN
	RETURN 
	    (
	        SELECT COUNT(TestResults.[ID])
	        FROM
	            dbo.TestResults
	        WHERE
	            TestResults.[ID] IN 
	            (
	                SELECT *
	                FROM dbo.dcetools_Fn_Courses_GetTestResultIdList(@courseID)
	            )
	        AND
	            TestResults.Complete <>0 
	    )
END' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[dcereports_Fn_TutorDiary_GetTrainingsOfCourseType]    Script Date: 03/16/2007 20:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dcereports_Fn_TutorDiary_GetTrainingsOfCourseType]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE FUNCTION [dbo].[dcereports_Fn_TutorDiary_GetTrainingsOfCourseType] 
(
	@courseTypeID uniqueidentifier
)
RETURNS TABLE
AS
	
RETURN
	(SELECT 
	    [ID] as TrainingID,
	    
        ISNULL(
            dbo.dcetools_Fn_Regions_GetObjectRegionName(Trainings.[ID]),
            ''Глобальний'') as RegionName,
	    
	    (SELECT dbo.dcetools_Fn_Content_GetString(Courses.Name)
	    FROM dbo.Courses
	    WHERE Trainings.Course =  Courses.[ID]) as CourseName,
	    
	    dbo.dcetools_Fn_Trainings_Students_GetTrainingStudentCount (Trainings.ID) as StudentCount,
	    
	    Trainings.StartDate as StartDate,
        Trainings.EndDate as EndDate,


	    dbo.dcereports_Tests_GetTestResultPassedCount(Trainings.Course)
         as PassedFinalTest
	    
        FROM dbo.Trainings
        WHERE
            Trainings.Course IN
            (SELECT [ID]
            FROM dbo.Courses
            WHERE
                @courseTypeID IS NULL
            OR
                Courses.Type =  @courseTypeID)
                )' 
END
GO
/****** Object:  Default [DF_BulletinBoard_id]    Script Date: 03/16/2007 20:07:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_BulletinBoard_id]') AND parent_object_id = OBJECT_ID(N'[dbo].[BulletinBoard]'))
Begin
ALTER TABLE [dbo].[BulletinBoard] ADD  CONSTRAINT [DF_BulletinBoard_id]  DEFAULT (newid()) FOR [id]

End
GO
/****** Object:  Default [DF_BulletinBoard_PostDate]    Script Date: 03/16/2007 20:07:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_BulletinBoard_PostDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[BulletinBoard]'))
Begin
ALTER TABLE [dbo].[BulletinBoard] ADD  CONSTRAINT [DF_BulletinBoard_PostDate]  DEFAULT (getdate()) FOR [PostDate]

End
GO
/****** Object:  Default [DF_CDPath_id]    Script Date: 03/16/2007 20:07:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_CDPath_id]') AND parent_object_id = OBJECT_ID(N'[dbo].[CDPath]'))
Begin
ALTER TABLE [dbo].[CDPath] ADD  CONSTRAINT [DF_CDPath_id]  DEFAULT (newid()) FOR [id]

End
GO
/****** Object:  Default [DF_CDPath_useCD]    Script Date: 03/16/2007 20:07:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_CDPath_useCD]') AND parent_object_id = OBJECT_ID(N'[dbo].[CDPath]'))
Begin
ALTER TABLE [dbo].[CDPath] ADD  CONSTRAINT [DF_CDPath_useCD]  DEFAULT (0) FOR [useCDLib]

End
GO
/****** Object:  Default [DF_Content_id]    Script Date: 03/16/2007 20:07:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Content_id]') AND parent_object_id = OBJECT_ID(N'[dbo].[Content]'))
Begin
ALTER TABLE [dbo].[Content] ADD  CONSTRAINT [DF_Content_id]  DEFAULT (newid()) FOR [id]

End
GO
/****** Object:  Default [DF_Content_Type]    Script Date: 03/16/2007 20:07:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Content_Type]') AND parent_object_id = OBJECT_ID(N'[dbo].[Content]'))
Begin
ALTER TABLE [dbo].[Content] ADD  CONSTRAINT [DF_Content_Type]  DEFAULT ((1)) FOR [Type]

End
GO
/****** Object:  Default [DF_CourseRequest_id]    Script Date: 03/16/2007 20:07:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_CourseRequest_id]') AND parent_object_id = OBJECT_ID(N'[dbo].[CourseRequest]'))
Begin
ALTER TABLE [dbo].[CourseRequest] ADD  CONSTRAINT [DF_CourseRequest_id]  DEFAULT (newid()) FOR [id]

End
GO
/****** Object:  Default [DF_CourseRequest_RequestDate]    Script Date: 03/16/2007 20:07:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_CourseRequest_RequestDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[CourseRequest]'))
Begin
ALTER TABLE [dbo].[CourseRequest] ADD  CONSTRAINT [DF_CourseRequest_RequestDate]  DEFAULT (getdate()) FOR [RequestDate]

End
GO
/****** Object:  Default [DF_CourseRequest_StartDate]    Script Date: 03/16/2007 20:07:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_CourseRequest_StartDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[CourseRequest]'))
Begin
ALTER TABLE [dbo].[CourseRequest] ADD  CONSTRAINT [DF_CourseRequest_StartDate]  DEFAULT (getdate()) FOR [StartDate]

End
GO
/****** Object:  Default [DF_Courses_id]    Script Date: 03/16/2007 20:07:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Courses_id]') AND parent_object_id = OBJECT_ID(N'[dbo].[Courses]'))
Begin
ALTER TABLE [dbo].[Courses] ADD  CONSTRAINT [DF_Courses_id]  DEFAULT (newid()) FOR [id]

End
GO
/****** Object:  Default [DF_Courses_Name]    Script Date: 03/16/2007 20:07:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Courses_Name]') AND parent_object_id = OBJECT_ID(N'[dbo].[Courses]'))
Begin
ALTER TABLE [dbo].[Courses] ADD  CONSTRAINT [DF_Courses_Name]  DEFAULT (newid()) FOR [Name]

End
GO
/****** Object:  Default [DF_Courses_Cost1]    Script Date: 03/16/2007 20:07:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Courses_Cost1]') AND parent_object_id = OBJECT_ID(N'[dbo].[Courses]'))
Begin
ALTER TABLE [dbo].[Courses] ADD  CONSTRAINT [DF_Courses_Cost1]  DEFAULT (0) FOR [Cost1]

End
GO
/****** Object:  Default [DF_Courses_Cost2]    Script Date: 03/16/2007 20:07:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Courses_Cost2]') AND parent_object_id = OBJECT_ID(N'[dbo].[Courses]'))
Begin
ALTER TABLE [dbo].[Courses] ADD  CONSTRAINT [DF_Courses_Cost2]  DEFAULT (0) FOR [Cost2]

End
GO
/****** Object:  Default [DF_Courses_Author]    Script Date: 03/16/2007 20:07:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Courses_Author]') AND parent_object_id = OBJECT_ID(N'[dbo].[Courses]'))
Begin
ALTER TABLE [dbo].[Courses] ADD  CONSTRAINT [DF_Courses_Author]  DEFAULT (newid()) FOR [Author]

End
GO
/****** Object:  Default [DF_Courses_CPublic]    Script Date: 03/16/2007 20:07:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Courses_CPublic]') AND parent_object_id = OBJECT_ID(N'[dbo].[Courses]'))
Begin
ALTER TABLE [dbo].[Courses] ADD  CONSTRAINT [DF_Courses_CPublic]  DEFAULT (0) FOR [CPublic]

End
GO
/****** Object:  Default [DF_Courses_DescriptionShort]    Script Date: 03/16/2007 20:07:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Courses_DescriptionShort]') AND parent_object_id = OBJECT_ID(N'[dbo].[Courses]'))
Begin
ALTER TABLE [dbo].[Courses] ADD  CONSTRAINT [DF_Courses_DescriptionShort]  DEFAULT (newid()) FOR [DescriptionShort]

End
GO
/****** Object:  Default [DF_Courses_DescriptionLong]    Script Date: 03/16/2007 20:07:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Courses_DescriptionLong]') AND parent_object_id = OBJECT_ID(N'[dbo].[Courses]'))
Begin
ALTER TABLE [dbo].[Courses] ADD  CONSTRAINT [DF_Courses_DescriptionLong]  DEFAULT (newid()) FOR [DescriptionLong]

End
GO
/****** Object:  Default [DF_Courses_Requirements]    Script Date: 03/16/2007 20:07:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Courses_Requirements]') AND parent_object_id = OBJECT_ID(N'[dbo].[Courses]'))
Begin
ALTER TABLE [dbo].[Courses] ADD  CONSTRAINT [DF_Courses_Requirements]  DEFAULT (newid()) FOR [Requirements]

End
GO
/****** Object:  Default [DF_Courses_Keywords]    Script Date: 03/16/2007 20:07:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Courses_Keywords]') AND parent_object_id = OBJECT_ID(N'[dbo].[Courses]'))
Begin
ALTER TABLE [dbo].[Courses] ADD  CONSTRAINT [DF_Courses_Keywords]  DEFAULT (newid()) FOR [Keywords]

End
GO
/****** Object:  Default [DF_Courses_Additions]    Script Date: 03/16/2007 20:07:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Courses_Additions]') AND parent_object_id = OBJECT_ID(N'[dbo].[Courses]'))
Begin
ALTER TABLE [dbo].[Courses] ADD  CONSTRAINT [DF_Courses_Additions]  DEFAULT (newid()) FOR [Additions]

End
GO
/****** Object:  Default [DF_Courses_Instructors]    Script Date: 03/16/2007 20:07:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Courses_Instructors]') AND parent_object_id = OBJECT_ID(N'[dbo].[Courses]'))
Begin
ALTER TABLE [dbo].[Courses] ADD  CONSTRAINT [DF_Courses_Instructors]  DEFAULT (newid()) FOR [Instructors]

End
GO
/****** Object:  Default [DF_Courses_isReady]    Script Date: 03/16/2007 20:07:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Courses_isReady]') AND parent_object_id = OBJECT_ID(N'[dbo].[Courses]'))
Begin
ALTER TABLE [dbo].[Courses] ADD  CONSTRAINT [DF_Courses_isReady]  DEFAULT (0) FOR [isReady]

End
GO
/****** Object:  Default [DF_Courses_CourseLanguage]    Script Date: 03/16/2007 20:07:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Courses_CourseLanguage]') AND parent_object_id = OBJECT_ID(N'[dbo].[Courses]'))
Begin
ALTER TABLE [dbo].[Courses] ADD  CONSTRAINT [DF_Courses_CourseLanguage]  DEFAULT (1) FOR [CourseLanguage]

End
GO
/****** Object:  Default [DF_CTrackRequest_id]    Script Date: 03/16/2007 20:07:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_CTrackRequest_id]') AND parent_object_id = OBJECT_ID(N'[dbo].[CTrackRequest]'))
Begin
ALTER TABLE [dbo].[CTrackRequest] ADD  CONSTRAINT [DF_CTrackRequest_id]  DEFAULT (newid()) FOR [id]

End
GO
/****** Object:  Default [DF_CTrackRequest_RequestDate]    Script Date: 03/16/2007 20:07:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_CTrackRequest_RequestDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[CTrackRequest]'))
Begin
ALTER TABLE [dbo].[CTrackRequest] ADD  CONSTRAINT [DF_CTrackRequest_RequestDate]  DEFAULT (getdate()) FOR [RequestDate]

End
GO
/****** Object:  Default [DF_CTrackRequest_StartDate]    Script Date: 03/16/2007 20:07:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_CTrackRequest_StartDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[CTrackRequest]'))
Begin
ALTER TABLE [dbo].[CTrackRequest] ADD  CONSTRAINT [DF_CTrackRequest_StartDate]  DEFAULT (getdate()) FOR [StartDate]

End
GO
/****** Object:  Default [DF_CTracks_id]    Script Date: 03/16/2007 20:07:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_CTracks_id]') AND parent_object_id = OBJECT_ID(N'[dbo].[CTracks]'))
Begin
ALTER TABLE [dbo].[CTracks] ADD  CONSTRAINT [DF_CTracks_id]  DEFAULT (newid()) FOR [id]

End
GO
/****** Object:  Default [DF_CTracks_Name]    Script Date: 03/16/2007 20:07:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_CTracks_Name]') AND parent_object_id = OBJECT_ID(N'[dbo].[CTracks]'))
Begin
ALTER TABLE [dbo].[CTracks] ADD  CONSTRAINT [DF_CTracks_Name]  DEFAULT (newid()) FOR [Name]

End
GO
/****** Object:  Default [DF_CTracks_Description]    Script Date: 03/16/2007 20:07:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_CTracks_Description]') AND parent_object_id = OBJECT_ID(N'[dbo].[CTracks]'))
Begin
ALTER TABLE [dbo].[CTracks] ADD  CONSTRAINT [DF_CTracks_Description]  DEFAULT (newid()) FOR [Description]

End
GO
/****** Object:  Default [DF_CTracks_Courses]    Script Date: 03/16/2007 20:07:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_CTracks_Courses]') AND parent_object_id = OBJECT_ID(N'[dbo].[CTracks]'))
Begin
ALTER TABLE [dbo].[CTracks] ADD  CONSTRAINT [DF_CTracks_Courses]  DEFAULT (newid()) FOR [Courses]

End
GO
/****** Object:  Default [DF_Entities_aaa]    Script Date: 03/16/2007 20:07:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Entities_aaa]') AND parent_object_id = OBJECT_ID(N'[dbo].[Entities]'))
Begin
ALTER TABLE [dbo].[Entities] ADD  CONSTRAINT [DF_Entities_aaa]  DEFAULT (newid()) FOR [id]

End
GO
/****** Object:  Default [DF_ForumReplies_id]    Script Date: 03/16/2007 20:07:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_ForumReplies_id]') AND parent_object_id = OBJECT_ID(N'[dbo].[ForumReplies]'))
Begin
ALTER TABLE [dbo].[ForumReplies] ADD  CONSTRAINT [DF_ForumReplies_id]  DEFAULT (newid()) FOR [id]

End
GO
/****** Object:  Default [DF_ForumReplies_PostDate]    Script Date: 03/16/2007 20:07:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_ForumReplies_PostDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[ForumReplies]'))
Begin
ALTER TABLE [dbo].[ForumReplies] ADD  CONSTRAINT [DF_ForumReplies_PostDate]  DEFAULT (getdate()) FOR [PostDate]

End
GO
/****** Object:  Default [DF_ForumTopics_id]    Script Date: 03/16/2007 20:07:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_ForumTopics_id]') AND parent_object_id = OBJECT_ID(N'[dbo].[ForumTopics]'))
Begin
ALTER TABLE [dbo].[ForumTopics] ADD  CONSTRAINT [DF_ForumTopics_id]  DEFAULT (newid()) FOR [id]

End
GO
/****** Object:  Default [DF_ForumTopics_PostDate]    Script Date: 03/16/2007 20:07:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_ForumTopics_PostDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[ForumTopics]'))
Begin
ALTER TABLE [dbo].[ForumTopics] ADD  CONSTRAINT [DF_ForumTopics_PostDate]  DEFAULT (getdate()) FOR [PostDate]

End
GO
/****** Object:  Default [DF_ForumTopics_Blocked]    Script Date: 03/16/2007 20:07:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_ForumTopics_Blocked]') AND parent_object_id = OBJECT_ID(N'[dbo].[ForumTopics]'))
Begin
ALTER TABLE [dbo].[ForumTopics] ADD  CONSTRAINT [DF_ForumTopics_Blocked]  DEFAULT (0) FOR [Blocked]

End
GO
/****** Object:  Default [DF_GroupMembers_mid]    Script Date: 03/16/2007 20:07:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_GroupMembers_mid]') AND parent_object_id = OBJECT_ID(N'[dbo].[GroupMembers]'))
Begin
ALTER TABLE [dbo].[GroupMembers] ADD  CONSTRAINT [DF_GroupMembers_mid]  DEFAULT (newid()) FOR [mid]

End
GO
/****** Object:  Default [DF_GroupMembers_id]    Script Date: 03/16/2007 20:07:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_GroupMembers_id]') AND parent_object_id = OBJECT_ID(N'[dbo].[GroupMembers]'))
Begin
ALTER TABLE [dbo].[GroupMembers] ADD  CONSTRAINT [DF_GroupMembers_id]  DEFAULT (newid()) FOR [id]

End
GO
/****** Object:  Default [DF_Groups_id]    Script Date: 03/16/2007 20:07:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Groups_id]') AND parent_object_id = OBJECT_ID(N'[dbo].[Groups]'))
Begin
ALTER TABLE [dbo].[Groups] ADD  CONSTRAINT [DF_Groups_id]  DEFAULT (newid()) FOR [id]

End
GO
/****** Object:  Default [DF_News_id]    Script Date: 03/16/2007 20:07:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_News_id]') AND parent_object_id = OBJECT_ID(N'[dbo].[News]'))
Begin
ALTER TABLE [dbo].[News] ADD  CONSTRAINT [DF_News_id]  DEFAULT (newid()) FOR [id]

End
GO
/****** Object:  Default [DF_News_NewsDate]    Script Date: 03/16/2007 20:07:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_News_NewsDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[News]'))
Begin
ALTER TABLE [dbo].[News] ADD  CONSTRAINT [DF_News_NewsDate]  DEFAULT (getdate()) FOR [NewsDate]

End
GO
/****** Object:  Default [DF_News_Head]    Script Date: 03/16/2007 20:07:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_News_Head]') AND parent_object_id = OBJECT_ID(N'[dbo].[News]'))
Begin
ALTER TABLE [dbo].[News] ADD  CONSTRAINT [DF_News_Head]  DEFAULT (newid()) FOR [Head]

End
GO
/****** Object:  Default [DF_News_Short]    Script Date: 03/16/2007 20:07:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_News_Short]') AND parent_object_id = OBJECT_ID(N'[dbo].[News]'))
Begin
ALTER TABLE [dbo].[News] ADD  CONSTRAINT [DF_News_Short]  DEFAULT (newid()) FOR [Short]

End
GO
/****** Object:  Default [DF_News_Text]    Script Date: 03/16/2007 20:07:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_News_Text]') AND parent_object_id = OBJECT_ID(N'[dbo].[News]'))
Begin
ALTER TABLE [dbo].[News] ADD  CONSTRAINT [DF_News_Text]  DEFAULT (newid()) FOR [Text]

End
GO
/****** Object:  Default [DF_News_MoreText]    Script Date: 03/16/2007 20:07:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_News_MoreText]') AND parent_object_id = OBJECT_ID(N'[dbo].[News]'))
Begin
ALTER TABLE [dbo].[News] ADD  CONSTRAINT [DF_News_MoreText]  DEFAULT (newid()) FOR [MoreText]

End
GO
/****** Object:  Default [DF_News_Image]    Script Date: 03/16/2007 20:07:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_News_Image]') AND parent_object_id = OBJECT_ID(N'[dbo].[News]'))
Begin
ALTER TABLE [dbo].[News] ADD  CONSTRAINT [DF_News_Image]  DEFAULT (newid()) FOR [Image]

End
GO
/****** Object:  Default [DF_Rights_id]    Script Date: 03/16/2007 20:07:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Rights_id]') AND parent_object_id = OBJECT_ID(N'[dbo].[Rights]'))
Begin
ALTER TABLE [dbo].[Rights] ADD  CONSTRAINT [DF_Rights_id]  DEFAULT (newid()) FOR [id]

End
GO
/****** Object:  Default [DF_TrainingSchedule_id]    Script Date: 03/16/2007 20:07:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_TrainingSchedule_id]') AND parent_object_id = OBJECT_ID(N'[dbo].[Schedule]'))
Begin
ALTER TABLE [dbo].[Schedule] ADD  CONSTRAINT [DF_TrainingSchedule_id]  DEFAULT (newid()) FOR [id]

End
GO
/****** Object:  Default [DF_Students_id]    Script Date: 03/16/2007 20:07:02 ******/
/*IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Students_id]') AND parent_object_id = OBJECT_ID(N'[dbo].[Students]'))
Begin
ALTER TABLE [dbo].[Students] ADD  CONSTRAINT [DF_Students_id]  DEFAULT (newid()) FOR [id]

End
GO*/
/****** Object:  Default [DF_Students_Photo]    Script Date: 03/16/2007 20:07:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Students_Photo]') AND parent_object_id = OBJECT_ID(N'[dbo].[Students]'))
Begin
ALTER TABLE [dbo].[Students] ADD  CONSTRAINT [DF_Students_Photo]  DEFAULT (newid()) FOR [Photo]

End
GO
/****** Object:  Default [DF_Students_TotalLogins]    Script Date: 03/16/2007 20:07:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Students_TotalLogins]') AND parent_object_id = OBJECT_ID(N'[dbo].[Students]'))
Begin
ALTER TABLE [dbo].[Students] ADD  CONSTRAINT [DF_Students_TotalLogins]  DEFAULT ((0)) FOR [TotalLogins]

End
GO
/****** Object:  Default [DF_Students_useDceLib]    Script Date: 03/16/2007 20:07:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Students_useDceLib]') AND parent_object_id = OBJECT_ID(N'[dbo].[Students]'))
Begin
ALTER TABLE [dbo].[Students] ADD  CONSTRAINT [DF_Students_useDceLib]  DEFAULT ((0)) FOR [useCDLib]

End
GO
/****** Object:  Default [DF_Students_CreateDate]    Script Date: 03/16/2007 20:07:02 ******/
/*IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Students_CreateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Students]'))
Begin
ALTER TABLE [dbo].[Students] ADD  CONSTRAINT [DF_Students_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]

End
GO*/
/****** Object:  Default [DF_Students_LastModifyDate]    Script Date: 03/16/2007 20:07:02 ******/
/*IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Students_LastModifyDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Students]'))
Begin
ALTER TABLE [dbo].[Students] ADD  CONSTRAINT [DF_Students_LastModifyDate]  DEFAULT (getdate()) FOR [LastModifyDate]

End
GO*/
/****** Object:  Default [DF_Tasks_id]    Script Date: 03/16/2007 20:07:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Tasks_id]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tasks]'))
Begin
ALTER TABLE [dbo].[Tasks] ADD  CONSTRAINT [DF_Tasks_id]  DEFAULT (newid()) FOR [id]

End
GO
/****** Object:  Default [DF_Tasks_TaskTime]    Script Date: 03/16/2007 20:07:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Tasks_TaskTime]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tasks]'))
Begin
ALTER TABLE [dbo].[Tasks] ADD  CONSTRAINT [DF_Tasks_TaskTime]  DEFAULT (getdate()) FOR [TaskTime]

End
GO
/****** Object:  Default [DF_TaskCompletion_id]    Script Date: 03/16/2007 20:07:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_TaskCompletion_id]') AND parent_object_id = OBJECT_ID(N'[dbo].[TaskSolutions]'))
Begin
ALTER TABLE [dbo].[TaskSolutions] ADD  CONSTRAINT [DF_TaskCompletion_id]  DEFAULT (newid()) FOR [id]

End
GO
/****** Object:  Default [DF_TestAnswers_id]    Script Date: 03/16/2007 20:07:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_TestAnswers_id]') AND parent_object_id = OBJECT_ID(N'[dbo].[TestAnswers]'))
Begin
ALTER TABLE [dbo].[TestAnswers] ADD  CONSTRAINT [DF_TestAnswers_id]  DEFAULT (newid()) FOR [id]

End
GO
/****** Object:  Default [DF_TestAnswers_Points]    Script Date: 03/16/2007 20:07:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_TestAnswers_Points]') AND parent_object_id = OBJECT_ID(N'[dbo].[TestAnswers]'))
Begin
ALTER TABLE [dbo].[TestAnswers] ADD  CONSTRAINT [DF_TestAnswers_Points]  DEFAULT (0) FOR [Points]

End
GO
/****** Object:  Default [DF_TestQuestions_id]    Script Date: 03/16/2007 20:07:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_TestQuestions_id]') AND parent_object_id = OBJECT_ID(N'[dbo].[TestQuestions]'))
Begin
ALTER TABLE [dbo].[TestQuestions] ADD  CONSTRAINT [DF_TestQuestions_id]  DEFAULT (newid()) FOR [id]

End
GO
/****** Object:  Default [DF_TestQuestions_QOrder]    Script Date: 03/16/2007 20:07:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_TestQuestions_QOrder]') AND parent_object_id = OBJECT_ID(N'[dbo].[TestQuestions]'))
Begin
ALTER TABLE [dbo].[TestQuestions] ADD  CONSTRAINT [DF_TestQuestions_QOrder]  DEFAULT (0) FOR [QOrder]

End
GO
/****** Object:  Default [DF_TestResults_id]    Script Date: 03/16/2007 20:07:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_TestResults_id]') AND parent_object_id = OBJECT_ID(N'[dbo].[TestResults]'))
Begin
ALTER TABLE [dbo].[TestResults] ADD  CONSTRAINT [DF_TestResults_id]  DEFAULT (newid()) FOR [id]

End
GO
/****** Object:  Default [DF_TestResults_Complete]    Script Date: 03/16/2007 20:07:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_TestResults_Complete]') AND parent_object_id = OBJECT_ID(N'[dbo].[TestResults]'))
Begin
ALTER TABLE [dbo].[TestResults] ADD  CONSTRAINT [DF_TestResults_Complete]  DEFAULT (0) FOR [Complete]

End
GO
/****** Object:  Default [DF_TestResults_AllowTries]    Script Date: 03/16/2007 20:07:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_TestResults_AllowTries]') AND parent_object_id = OBJECT_ID(N'[dbo].[TestResults]'))
Begin
ALTER TABLE [dbo].[TestResults] ADD  CONSTRAINT [DF_TestResults_AllowTries]  DEFAULT (2) FOR [AllowTries]

End
GO
/****** Object:  Default [DF_TestResults_SkipTest]    Script Date: 03/16/2007 20:07:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_TestResults_SkipTest]') AND parent_object_id = OBJECT_ID(N'[dbo].[TestResults]'))
Begin
ALTER TABLE [dbo].[TestResults] ADD  CONSTRAINT [DF_TestResults_SkipTest]  DEFAULT (0) FOR [Skipped]

End
GO
/****** Object:  Default [DF_Tests_id]    Script Date: 03/16/2007 20:07:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Tests_id]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tests]'))
Begin
ALTER TABLE [dbo].[Tests] ADD  CONSTRAINT [DF_Tests_id]  DEFAULT (newid()) FOR [id]

End
GO
/****** Object:  Default [DF_Tests_Hints]    Script Date: 03/16/2007 20:07:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Tests_Hints]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tests]'))
Begin
ALTER TABLE [dbo].[Tests] ADD  CONSTRAINT [DF_Tests_Hints]  DEFAULT (0) FOR [Hints]

End
GO
/****** Object:  Default [DF_Tests_ShowThemes]    Script Date: 03/16/2007 20:07:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Tests_ShowThemes]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tests]'))
Begin
ALTER TABLE [dbo].[Tests] ADD  CONSTRAINT [DF_Tests_ShowThemes]  DEFAULT (1) FOR [ShowThemes]

End
GO
/****** Object:  Default [DF_Themes_id]    Script Date: 03/16/2007 20:07:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Themes_id]') AND parent_object_id = OBJECT_ID(N'[dbo].[Themes]'))
Begin
ALTER TABLE [dbo].[Themes] ADD  CONSTRAINT [DF_Themes_id]  DEFAULT (newid()) FOR [id]

End
GO
/****** Object:  Default [DF_Themes_Type]    Script Date: 03/16/2007 20:07:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Themes_Type]') AND parent_object_id = OBJECT_ID(N'[dbo].[Themes]'))
Begin
ALTER TABLE [dbo].[Themes] ADD  CONSTRAINT [DF_Themes_Type]  DEFAULT (1) FOR [Type]

End
GO
/****** Object:  Default [DF_Themes_Name]    Script Date: 03/16/2007 20:07:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Themes_Name]') AND parent_object_id = OBJECT_ID(N'[dbo].[Themes]'))
Begin
ALTER TABLE [dbo].[Themes] ADD  CONSTRAINT [DF_Themes_Name]  DEFAULT (newid()) FOR [Name]

End
GO
/****** Object:  Default [DF_Themes_Duration]    Script Date: 03/16/2007 20:07:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Themes_Duration]') AND parent_object_id = OBJECT_ID(N'[dbo].[Themes]'))
Begin
ALTER TABLE [dbo].[Themes] ADD  CONSTRAINT [DF_Themes_Duration]  DEFAULT (1) FOR [Duration]

End
GO
/****** Object:  Default [DF_Themes_Mandatory]    Script Date: 03/16/2007 20:07:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Themes_Mandatory]') AND parent_object_id = OBJECT_ID(N'[dbo].[Themes]'))
Begin
ALTER TABLE [dbo].[Themes] ADD  CONSTRAINT [DF_Themes_Mandatory]  DEFAULT (1) FOR [Mandatory]

End
GO
/****** Object:  Default [DF_Themes_Content]    Script Date: 03/16/2007 20:07:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Themes_Content]') AND parent_object_id = OBJECT_ID(N'[dbo].[Themes]'))
Begin
ALTER TABLE [dbo].[Themes] ADD  CONSTRAINT [DF_Themes_Content]  DEFAULT (newid()) FOR [Content]

End
GO
/****** Object:  Default [DF_Themes_TOrder]    Script Date: 03/16/2007 20:07:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Themes_TOrder]') AND parent_object_id = OBJECT_ID(N'[dbo].[Themes]'))
Begin
ALTER TABLE [dbo].[Themes] ADD  CONSTRAINT [DF_Themes_TOrder]  DEFAULT (0) FOR [TOrder]

End
GO
/****** Object:  Default [DF_Tracks_id]    Script Date: 03/16/2007 20:07:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Tracks_id]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tracks]'))
Begin
ALTER TABLE [dbo].[Tracks] ADD  CONSTRAINT [DF_Tracks_id]  DEFAULT (newid()) FOR [id]

End
GO
/****** Object:  Default [DF_Tracks_Name]    Script Date: 03/16/2007 20:07:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Tracks_Name]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tracks]'))
Begin
ALTER TABLE [dbo].[Tracks] ADD  CONSTRAINT [DF_Tracks_Name]  DEFAULT (newid()) FOR [Name]

End
GO
/****** Object:  Default [DF_Tracks_Description]    Script Date: 03/16/2007 20:07:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Tracks_Description]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tracks]'))
Begin
ALTER TABLE [dbo].[Tracks] ADD  CONSTRAINT [DF_Tracks_Description]  DEFAULT (newid()) FOR [Description]

End
GO
/****** Object:  Default [DF_Tracks_Students]    Script Date: 03/16/2007 20:07:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Tracks_Students]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tracks]'))
Begin
ALTER TABLE [dbo].[Tracks] ADD  CONSTRAINT [DF_Tracks_Students]  DEFAULT (newid()) FOR [Students]

End
GO
/****** Object:  Default [DF_Tracks_Trainings]    Script Date: 03/16/2007 20:07:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Tracks_Trainings]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tracks]'))
Begin
ALTER TABLE [dbo].[Tracks] ADD  CONSTRAINT [DF_Tracks_Trainings]  DEFAULT (newid()) FOR [Trainings]

End
GO
/****** Object:  Default [DF_TrainingStats_id]    Script Date: 03/16/2007 20:07:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_TrainingStats_id]') AND parent_object_id = OBJECT_ID(N'[dbo].[TrainingBlocking]'))
Begin
ALTER TABLE [dbo].[TrainingBlocking] ADD  CONSTRAINT [DF_TrainingStats_id]  DEFAULT (newid()) FOR [id]

End
GO
/****** Object:  Default [DF_Trainings_id]    Script Date: 03/16/2007 20:07:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Trainings_id]') AND parent_object_id = OBJECT_ID(N'[dbo].[Trainings]'))
Begin
ALTER TABLE [dbo].[Trainings] ADD  CONSTRAINT [DF_Trainings_id]  DEFAULT (newid()) FOR [id]

End
GO
/****** Object:  Default [DF_Trainings_Name]    Script Date: 03/16/2007 20:07:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Trainings_Name]') AND parent_object_id = OBJECT_ID(N'[dbo].[Trainings]'))
Begin
ALTER TABLE [dbo].[Trainings] ADD  CONSTRAINT [DF_Trainings_Name]  DEFAULT (newid()) FOR [Name]

End
GO
/****** Object:  Default [DF_Trainings_Comment]    Script Date: 03/16/2007 20:07:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Trainings_Comment]') AND parent_object_id = OBJECT_ID(N'[dbo].[Trainings]'))
Begin
ALTER TABLE [dbo].[Trainings] ADD  CONSTRAINT [DF_Trainings_Comment]  DEFAULT (newid()) FOR [Comment]

End
GO
/****** Object:  Default [DF_Trainings_isPublic]    Script Date: 03/16/2007 20:07:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Trainings_isPublic]') AND parent_object_id = OBJECT_ID(N'[dbo].[Trainings]'))
Begin
ALTER TABLE [dbo].[Trainings] ADD  CONSTRAINT [DF_Trainings_isPublic]  DEFAULT (0) FOR [isPublic]

End
GO
/****** Object:  Default [DF_Trainings_isActive]    Script Date: 03/16/2007 20:07:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Trainings_isActive]') AND parent_object_id = OBJECT_ID(N'[dbo].[Trainings]'))
Begin
ALTER TABLE [dbo].[Trainings] ADD  CONSTRAINT [DF_Trainings_isActive]  DEFAULT (0) FOR [isActive]

End
GO
/****** Object:  Default [DF_Trainings_Instructors]    Script Date: 03/16/2007 20:07:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Trainings_Instructors]') AND parent_object_id = OBJECT_ID(N'[dbo].[Trainings]'))
Begin
ALTER TABLE [dbo].[Trainings] ADD  CONSTRAINT [DF_Trainings_Instructors]  DEFAULT (newid()) FOR [Instructors]

End
GO
/****** Object:  Default [DF_Trainings_Curators]    Script Date: 03/16/2007 20:07:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Trainings_Curators]') AND parent_object_id = OBJECT_ID(N'[dbo].[Trainings]'))
Begin
ALTER TABLE [dbo].[Trainings] ADD  CONSTRAINT [DF_Trainings_Curators]  DEFAULT (newid()) FOR [Curators]

End
GO
/****** Object:  Default [DF_Trainings_Students]    Script Date: 03/16/2007 20:07:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Trainings_Students]') AND parent_object_id = OBJECT_ID(N'[dbo].[Trainings]'))
Begin
ALTER TABLE [dbo].[Trainings] ADD  CONSTRAINT [DF_Trainings_Students]  DEFAULT (newid()) FOR [Students]

End
GO
/****** Object:  Default [DF_Trainings_TimeStrict]    Script Date: 03/16/2007 20:07:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Trainings_TimeStrict]') AND parent_object_id = OBJECT_ID(N'[dbo].[Trainings]'))
Begin
ALTER TABLE [dbo].[Trainings] ADD  CONSTRAINT [DF_Trainings_TimeStrict]  DEFAULT (0) FOR [TimeStrict]

End
GO
/****** Object:  Default [DF_Trainings_TestOnly]    Script Date: 03/16/2007 20:07:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Trainings_TestOnly]') AND parent_object_id = OBJECT_ID(N'[dbo].[Trainings]'))
Begin
ALTER TABLE [dbo].[Trainings] ADD  CONSTRAINT [DF_Trainings_TestOnly]  DEFAULT (0) FOR [TestOnly]

End
GO
/****** Object:  Default [DF_Trainings_Expires]    Script Date: 03/16/2007 20:07:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Trainings_Expires]') AND parent_object_id = OBJECT_ID(N'[dbo].[Trainings]'))
Begin
ALTER TABLE [dbo].[Trainings] ADD  CONSTRAINT [DF_Trainings_Expires]  DEFAULT (0) FOR [Expires]

End
GO
/****** Object:  Default [DF_CourseTerms_id]    Script Date: 03/16/2007 20:07:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_CourseTerms_id]') AND parent_object_id = OBJECT_ID(N'[dbo].[Vocabulary]'))
Begin
ALTER TABLE [dbo].[Vocabulary] ADD  CONSTRAINT [DF_CourseTerms_id]  DEFAULT (newid()) FOR [id]

End
GO
/****** Object:  Default [DF_Vocabulary_id]    Script Date: 03/16/2007 20:07:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Vocabulary_id]') AND parent_object_id = OBJECT_ID(N'[dbo].[VTerms]'))
Begin
ALTER TABLE [dbo].[VTerms] ADD  CONSTRAINT [DF_Vocabulary_id]  DEFAULT (newid()) FOR [id]

End
GO
/****** Object:  Default [DF_VTerms_Name]    Script Date: 03/16/2007 20:07:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_VTerms_Name]') AND parent_object_id = OBJECT_ID(N'[dbo].[VTerms]'))
Begin
ALTER TABLE [dbo].[VTerms] ADD  CONSTRAINT [DF_VTerms_Name]  DEFAULT (newid()) FOR [Name]

End
GO
/****** Object:  Default [DF_Vocabulary_Text]    Script Date: 03/16/2007 20:07:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Vocabulary_Text]') AND parent_object_id = OBJECT_ID(N'[dbo].[VTerms]'))
Begin
ALTER TABLE [dbo].[VTerms] ADD  CONSTRAINT [DF_Vocabulary_Text]  DEFAULT (newid()) FOR [Text]

End
GO
/****** Object:  ForeignKey [FK_CDPath_Students]    Script Date: 03/16/2007 20:07:02 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CDPath_Students]') AND parent_object_id = OBJECT_ID(N'[dbo].[CDPath]'))
ALTER TABLE [dbo].[CDPath]  WITH CHECK ADD  CONSTRAINT [FK_CDPath_Students] FOREIGN KEY([studentId])
REFERENCES [dbo].[Students] ([id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
/****** Object:  ForeignKey [FK_CDPath_Trainings]    Script Date: 03/16/2007 20:07:02 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CDPath_Trainings]') AND parent_object_id = OBJECT_ID(N'[dbo].[CDPath]'))
ALTER TABLE [dbo].[CDPath]  WITH CHECK ADD  CONSTRAINT [FK_CDPath_Trainings] FOREIGN KEY([trainingId])
REFERENCES [dbo].[Trainings] ([id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
/****** Object:  ForeignKey [Courses.id=>CourseRequest.Course]    Script Date: 03/16/2007 20:07:02 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[Courses.id=>CourseRequest.Course]') AND parent_object_id = OBJECT_ID(N'[dbo].[CourseRequest]'))
ALTER TABLE [dbo].[CourseRequest]  WITH CHECK ADD  CONSTRAINT [Courses.id=>CourseRequest.Course] FOREIGN KEY([Course])
REFERENCES [dbo].[Courses] ([id])
ON DELETE CASCADE
GO
/****** Object:  ForeignKey [CTracks.id=>CTrackRequest.CTrack]    Script Date: 03/16/2007 20:07:02 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[CTracks.id=>CTrackRequest.CTrack]') AND parent_object_id = OBJECT_ID(N'[dbo].[CTrackRequest]'))
ALTER TABLE [dbo].[CTrackRequest]  WITH CHECK ADD  CONSTRAINT [CTracks.id=>CTrackRequest.CTrack] FOREIGN KEY([CTrack])
REFERENCES [dbo].[CTracks] ([id])
ON DELETE CASCADE
GO
/****** Object:  ForeignKey [ForumTopics.id=>ForumReplies.Topic]    Script Date: 03/16/2007 20:07:02 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[ForumTopics.id=>ForumReplies.Topic]') AND parent_object_id = OBJECT_ID(N'[dbo].[ForumReplies]'))
ALTER TABLE [dbo].[ForumReplies]  WITH CHECK ADD  CONSTRAINT [ForumTopics.id=>ForumReplies.Topic] FOREIGN KEY([Topic])
REFERENCES [dbo].[ForumTopics] ([id])
ON DELETE CASCADE
GO
/****** Object:  ForeignKey [Trainings.id=>ForumTopics.Training]    Script Date: 03/16/2007 20:07:02 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[Trainings.id=>ForumTopics.Training]') AND parent_object_id = OBJECT_ID(N'[dbo].[ForumTopics]'))
ALTER TABLE [dbo].[ForumTopics]  WITH CHECK ADD  CONSTRAINT [Trainings.id=>ForumTopics.Training] FOREIGN KEY([Training])
REFERENCES [dbo].[Trainings] ([id])
ON DELETE CASCADE
GO
/****** Object:  ForeignKey [Groups.id=>GroupMembers.MGroup]    Script Date: 03/16/2007 20:07:02 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[Groups.id=>GroupMembers.MGroup]') AND parent_object_id = OBJECT_ID(N'[dbo].[GroupMembers]'))
ALTER TABLE [dbo].[GroupMembers]  WITH CHECK ADD  CONSTRAINT [Groups.id=>GroupMembers.MGroup] FOREIGN KEY([MGroup])
REFERENCES [dbo].[Groups] ([id])
ON DELETE CASCADE
GO
/****** Object:  ForeignKey [Entities.id=>Rights.eid]    Script Date: 03/16/2007 20:07:02 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[Entities.id=>Rights.eid]') AND parent_object_id = OBJECT_ID(N'[dbo].[Rights]'))
ALTER TABLE [dbo].[Rights]  WITH CHECK ADD  CONSTRAINT [Entities.id=>Rights.eid] FOREIGN KEY([eid])
REFERENCES [dbo].[Entities] ([id])
ON DELETE CASCADE
GO
/****** Object:  ForeignKey [Trainings.id=>TrainingSchedule.Training]    Script Date: 03/16/2007 20:07:02 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[Trainings.id=>TrainingSchedule.Training]') AND parent_object_id = OBJECT_ID(N'[dbo].[Schedule]'))
ALTER TABLE [dbo].[Schedule]  WITH CHECK ADD  CONSTRAINT [Trainings.id=>TrainingSchedule.Training] FOREIGN KEY([Training])
REFERENCES [dbo].[Trainings] ([id])
ON DELETE CASCADE
GO
/****** Object:  ForeignKey [Trainings.id => Tasks.Training]    Script Date: 03/16/2007 20:07:02 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[Trainings.id => Tasks.Training]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tasks]'))
ALTER TABLE [dbo].[Tasks]  WITH CHECK ADD  CONSTRAINT [Trainings.id => Tasks.Training] FOREIGN KEY([Training])
REFERENCES [dbo].[Trainings] ([id])
ON DELETE CASCADE
GO
/****** Object:  ForeignKey [Students.id=>TaskCompletion.Student]    Script Date: 03/16/2007 20:07:02 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[Students.id=>TaskCompletion.Student]') AND parent_object_id = OBJECT_ID(N'[dbo].[TaskSolutions]'))
ALTER TABLE [dbo].[TaskSolutions]  WITH CHECK ADD  CONSTRAINT [Students.id=>TaskCompletion.Student] FOREIGN KEY([Student])
REFERENCES [dbo].[Students] ([id])
ON DELETE CASCADE
GO
/****** Object:  ForeignKey [Tasks.id=>TaskSolution.Task]    Script Date: 03/16/2007 20:07:02 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[Tasks.id=>TaskSolution.Task]') AND parent_object_id = OBJECT_ID(N'[dbo].[TaskSolutions]'))
ALTER TABLE [dbo].[TaskSolutions]  WITH CHECK ADD  CONSTRAINT [Tasks.id=>TaskSolution.Task] FOREIGN KEY([Task])
REFERENCES [dbo].[Tasks] ([id])
ON DELETE CASCADE
GO
/****** Object:  ForeignKey [TestQuestions.id=>TestAnswers.Question]    Script Date: 03/16/2007 20:07:02 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[TestQuestions.id=>TestAnswers.Question]') AND parent_object_id = OBJECT_ID(N'[dbo].[TestAnswers]'))
ALTER TABLE [dbo].[TestAnswers]  WITH CHECK ADD  CONSTRAINT [TestQuestions.id=>TestAnswers.Question] FOREIGN KEY([Question])
REFERENCES [dbo].[TestQuestions] ([id])
ON DELETE CASCADE
GO
/****** Object:  ForeignKey [Test.id=>TestQuestions.Test]    Script Date: 03/16/2007 20:07:02 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[Test.id=>TestQuestions.Test]') AND parent_object_id = OBJECT_ID(N'[dbo].[TestQuestions]'))
ALTER TABLE [dbo].[TestQuestions]  WITH CHECK ADD  CONSTRAINT [Test.id=>TestQuestions.Test] FOREIGN KEY([Test])
REFERENCES [dbo].[Tests] ([id])
ON DELETE CASCADE
GO
/****** Object:  ForeignKey [Tests.id=>TestResults.Test]    Script Date: 03/16/2007 20:07:02 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[Tests.id=>TestResults.Test]') AND parent_object_id = OBJECT_ID(N'[dbo].[TestResults]'))
ALTER TABLE [dbo].[TestResults]  WITH CHECK ADD  CONSTRAINT [Tests.id=>TestResults.Test] FOREIGN KEY([Test])
REFERENCES [dbo].[Tests] ([id])
ON DELETE CASCADE
GO
/****** Object:  ForeignKey [Students.id=>TrainingBlocking.Student]    Script Date: 03/16/2007 20:07:02 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[Students.id=>TrainingBlocking.Student]') AND parent_object_id = OBJECT_ID(N'[dbo].[TrainingBlocking]'))
ALTER TABLE [dbo].[TrainingBlocking]  WITH CHECK ADD  CONSTRAINT [Students.id=>TrainingBlocking.Student] FOREIGN KEY([Student])
REFERENCES [dbo].[Students] ([id])
ON DELETE CASCADE
GO
/****** Object:  ForeignKey [Trainings.id=>TrainingBlocking.Training]    Script Date: 03/16/2007 20:07:02 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[Trainings.id=>TrainingBlocking.Training]') AND parent_object_id = OBJECT_ID(N'[dbo].[TrainingBlocking]'))
ALTER TABLE [dbo].[TrainingBlocking]  WITH CHECK ADD  CONSTRAINT [Trainings.id=>TrainingBlocking.Training] FOREIGN KEY([Training])
REFERENCES [dbo].[Trainings] ([id])
ON DELETE CASCADE
GO
GRANT SELECT ON [dbo].[AllDistinctTrainingStudents] TO [public]
GO
GRANT SELECT ON [dbo].[AllGroupMembers] TO [public]
GO
GRANT SELECT ON [dbo].[AllStudentTrainings] TO [public]
GO
GRANT SELECT ON [dbo].[AllSubThemes] TO [public]
GO
GRANT SELECT ON [dbo].[AllTrainingStudents] TO [public]
GO
GRANT EXECUTE ON [dbo].[CountCompletePractice] TO [public]
GO
GRANT EXECUTE ON [dbo].[CountCompleteSolutions] TO [public]
GO
GRANT EXECUTE ON [dbo].[CountCompleteTests] TO [public]
GO
GRANT EXECUTE ON [dbo].[CountStudentSolutions] TO [public]
GO
GRANT SELECT ON [dbo].[CountStudentsPassedTest] TO [public]
GO
GRANT EXECUTE ON [dbo].[CountTestResults] TO [public]
GO
GRANT EXECUTE ON [dbo].[CountTestTries] TO [public]
GO
GRANT EXECUTE ON [dbo].[CourseDuration] TO [public]
GO
GRANT EXECUTE ON [dbo].[CourseIsActive] TO [public]
GO
GRANT EXECUTE ON [dbo].[CourseOfTest] TO [public]
GO
GRANT EXECUTE ON [dbo].[CourseOfTestQuestion] TO [public]
GO
GRANT EXECUTE ON [dbo].[CourseOfTheme] TO [public]
GO
GRANT SELECT ON [dbo].[CourseTests] TO [public]
GO
GRANT EXECUTE ON [dbo].[GetContentAlt] TO [public]
GO
GRANT EXECUTE ON [dbo].[GetMaxCOrder] TO [public]
GO
GRANT EXECUTE ON [dbo].[GetStrContent] TO [public]
GO
GRANT EXECUTE ON [dbo].[GetStrContentA] TO [public]
GO
GRANT EXECUTE ON [dbo].[GetStrContentAlt] TO [public]
GO
GRANT EXECUTE ON [dbo].[GetStrContentAlt2] TO [public]
GO
GRANT EXECUTE ON [dbo].[GetStrContentOrderer] TO [public]
GO
GRANT EXECUTE ON [dbo].[GetTContentId] TO [public]
GO
GRANT EXECUTE ON [dbo].[GetTDataContentAlt] TO [public]
GO
GRANT EXECUTE ON [dbo].[GetTestCourse] TO [public]
GO
GRANT EXECUTE ON [dbo].[GetTestCourseName] TO [public]
GO
GRANT EXECUTE ON [dbo].[GetThemeName] TO [public]
GO
GRANT EXECUTE ON [dbo].[GetUserRole] TO [public]
GO
GRANT EXECUTE ON [dbo].[IdIsNotNull] TO [public]
GO
GRANT EXECUTE ON [dbo].[isAreaHasCourses] TO [public]
GO
GRANT EXECUTE ON [dbo].[IsChildTheme] TO [public]
GO
GRANT EXECUTE ON [dbo].[isCourseOfArea] TO [public]
GO
GRANT EXECUTE ON [dbo].[IsCourseTestComplete] TO [public]
GO
GRANT EXECUTE ON [dbo].[IsEmptyString] TO [public]
GO
GRANT EXECUTE ON [dbo].[isIdEqual] TO [public]
GO
GRANT EXECUTE ON [dbo].[IsTestMandatory] TO [public]
GO
GRANT EXECUTE ON [dbo].[NumReplies] TO [public]
GO
GRANT EXECUTE ON [dbo].[StudentName] TO [public]
GO
GRANT EXECUTE ON [dbo].[TargetLang] TO [public]
GO
GRANT EXECUTE ON [dbo].[TestPoints] TO [public]
GO
GRANT EXECUTE ON [dbo].[TestResultPoints] TO [public]
GO
GRANT EXECUTE ON [dbo].[TopicAuthor] TO [public]
GO
GRANT SELECT ON [dbo].[TrainingTests] TO [public]
GO
GRANT EXECUTE ON [dbo].[UserName] TO [public]
GO
GRANT EXECUTE ON [dbo].[GetGroupMembers] TO [public]
GO
GRANT EXECUTE ON [dbo].[MakeContentCopy] TO [public]
GO
GRANT EXECUTE ON [dbo].[MakeCourseCopy] TO [public]
GO
GRANT EXECUTE ON [dbo].[MakeQuestionCopy] TO [public]
GO
GRANT EXECUTE ON [dbo].[MakeTestCopy] TO [public]
GO
GRANT EXECUTE ON [dbo].[MakeTestOnlyCopy] TO [public]
GO
GRANT EXECUTE ON [dbo].[MakeThemeCopy] TO [public]
GO
GRANT EXECUTE ON [dbo].[MakeThemesCopy] TO [public]
GO
GRANT EXECUTE ON [dbo].[MakeVocabularyCopy] TO [public]
GO
