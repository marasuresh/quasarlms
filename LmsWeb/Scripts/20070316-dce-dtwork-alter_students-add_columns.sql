/****** Object:  Table [dbo].[Students]    Script Date: 03/15/2007 19:09:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER TABLE [dbo].[Students] ADD

	[PasswordHash] [uniqueidentifier] NULL,
	[PasswordHashSalt] [uniqueidentifier] NULL,
	[CreateDate] [datetime] NOT NULL DEFAULT GETDATE(),
	[LastModifyDate] [datetime] NOT NULL DEFAULT GETDATE();
GO