-- Enables FULLTEXT support for YAF
-- Must be MANUALLY run against the YAF DB

if (select DATABASEPROPERTY(DB_NAME(), N'IsFullTextEnabled')) <> 1 
	exec sp_fulltext_database N'enable' 
GO

if (select DATABASEPROPERTY(DB_NAME(), N'IsFullTextEnabled')) = 1
BEGIN
	if not exists (select * from dbo.sysfulltextcatalogs where name = N'YafSearch')
	BEGIN
		EXEC sp_fulltext_catalog N'YafSearch', N'create'
		EXEC sp_fulltext_table N'[dbo].[yaf_Message]', N'create', N'YafSearch', N'PK_yaf_Message'	
		EXEC sp_fulltext_column N'[dbo].[yaf_Message]', N'Message', N'add'
		EXEC sp_fulltext_table N'[dbo].[yaf_Message]', N'activate' 
		EXEC sp_fulltext_table N'[dbo].[yaf_Message]', N'Start_change_tracking'
		EXEC sp_fulltext_table N'[dbo].[yaf_Message]', N'Start_background_updateindex'


		EXEC sp_fulltext_table N'[dbo].[yaf_Topic]', N'create', N'YafSearch', N'PK_yaf_Topic'
		EXEC sp_fulltext_column N'[dbo].[yaf_Topic]', N'Topic', N'add'
		EXEC sp_fulltext_table N'[dbo].[yaf_Topic]', N'activate' 
		EXEC sp_fulltext_table N'[dbo].[yaf_Topic]', N'Start_change_tracking'
		EXEC sp_fulltext_table N'[dbo].[yaf_Topic]', N'Start_background_updateindex'
		
		-- enable in yaf_Registry as a default
		IF EXISTS ( SELECT 1 FROM yaf_Registry where [Name] = N'usefulltextsearch' )
			UPDATE yaf_Registry SET [Value] = '1' WHERE [Name] = N'usefulltextsearch'
		ELSE
			INSERT INTO yaf_Registry ([Name],[Value],[BoardID]) VALUES (N'usefulltextsearch','1',NULL);
	END
END
GO
