/****** Object:  Table [dbo].[Regions]    Script Date: 02/15/2007 15:52:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Regions](
	[ID] [uniqueidentifier] NULL,
	[Name] [nvarchar](250) COLLATE Cyrillic_General_CI_AS NOT NULL,
	[CodesCommaSeparated] [nvarchar](250) COLLATE Cyrillic_General_CI_AS NULL,
 CONSTRAINT [IX_Regions] UNIQUE NONCLUSTERED 
(
	[ID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO

INSERT INTO [dbo].[Regions]([ID],[Name],[CodesCommaSeparated])
VALUES('bbbbbbbb-d9b7-42e1-b6a1-ffffb0e29b01','Київська РД','kv,kcf')
INSERT INTO [dbo].[Regions]([ID],[Name],[CodesCommaSeparated])
VALUES('bbbbbbbb-d9b7-42e1-b6a1-ffffb0e29b02','Одеська ОД','od')
INSERT INTO [dbo].[Regions]([ID],[Name],[CodesCommaSeparated])
VALUES('bbbbbbbb-d9b7-42e1-b6a1-ffffb0e29b03','Кримська ОД','cr')
INSERT INTO [dbo].[Regions]([ID],[Name],[CodesCommaSeparated])
VALUES('bbbbbbbb-d9b7-42e1-b6a1-ffffb0e29b04','Вінницька ОД','vn')
INSERT INTO [dbo].[Regions]([ID],[Name],[CodesCommaSeparated])
VALUES('bbbbbbbb-d9b7-42e1-b6a1-ffffb0e29b05','Волинська ОД','lt')
INSERT INTO [dbo].[Regions]([ID],[Name],[CodesCommaSeparated])
VALUES('bbbbbbbb-d9b7-42e1-b6a1-ffffb0e29b06','Дніпропетровська ОД','dp')
INSERT INTO [dbo].[Regions]([ID],[Name],[CodesCommaSeparated])
VALUES('bbbbbbbb-d9b7-42e1-b6a1-ffffb0e29b07','Донецька ОД','dn')
INSERT INTO [dbo].[Regions]([ID],[Name],[CodesCommaSeparated])
VALUES('bbbbbbbb-d9b7-42e1-b6a1-ffffb0e29b08','Житомирська ОД','zt')
INSERT INTO [dbo].[Regions]([ID],[Name],[CodesCommaSeparated])
VALUES('bbbbbbbb-d9b7-42e1-b6a1-ffffb0e29b09','Закарпатська ОД','uz')
INSERT INTO [dbo].[Regions]([ID],[Name],[CodesCommaSeparated])
VALUES('bbbbbbbb-d9b7-42e1-b6a1-ffffb0e29b10','Запорізька ОД','zp')
INSERT INTO [dbo].[Regions]([ID],[Name],[CodesCommaSeparated])
VALUES('bbbbbbbb-d9b7-42e1-b6a1-ffffb0e29b11','Івано-Франківська ОД','if')
INSERT INTO [dbo].[Regions]([ID],[Name],[CodesCommaSeparated])
VALUES('bbbbbbbb-d9b7-42e1-b6a1-ffffb0e29b12','Кіровоградська ОД','kr')
INSERT INTO [dbo].[Regions]([ID],[Name],[CodesCommaSeparated])
VALUES('bbbbbbbb-d9b7-42e1-b6a1-ffffb0e29b13','Луганська ОД','lg')
INSERT INTO [dbo].[Regions]([ID],[Name],[CodesCommaSeparated])
VALUES('bbbbbbbb-d9b7-42e1-b6a1-ffffb0e29b14','Львівська ОД','lv')
INSERT INTO [dbo].[Regions]([ID],[Name],[CodesCommaSeparated])
VALUES('bbbbbbbb-d9b7-42e1-b6a1-ffffb0e29b15','Миколаївська ОД','mk')
INSERT INTO [dbo].[Regions]([ID],[Name],[CodesCommaSeparated])
VALUES('bbbbbbbb-d9b7-42e1-b6a1-ffffb0e29b16','Полтавська ОД','pl')
INSERT INTO [dbo].[Regions]([ID],[Name],[CodesCommaSeparated])
VALUES('bbbbbbbb-d9b7-42e1-b6a1-ffffb0e29b17','Рівненська ОД','rv')
INSERT INTO [dbo].[Regions]([ID],[Name],[CodesCommaSeparated])
VALUES('bbbbbbbb-d9b7-42e1-b6a1-ffffb0e29b18','Сумська ОД','sm')
INSERT INTO [dbo].[Regions]([ID],[Name],[CodesCommaSeparated])
VALUES('bbbbbbbb-d9b7-42e1-b6a1-ffffb0e29b19','Тернопільска ОД','tr')
INSERT INTO [dbo].[Regions]([ID],[Name],[CodesCommaSeparated])
VALUES('bbbbbbbb-d9b7-42e1-b6a1-ffffb0e29b20','Харківська ОД','kh')
INSERT INTO [dbo].[Regions]([ID],[Name],[CodesCommaSeparated])
VALUES('bbbbbbbb-d9b7-42e1-b6a1-ffffb0e29b21','Херсонська ОД','ks')
INSERT INTO [dbo].[Regions]([ID],[Name],[CodesCommaSeparated])
VALUES('bbbbbbbb-d9b7-42e1-b6a1-ffffb0e29b22','Хмельницька ОД','km')
INSERT INTO [dbo].[Regions]([ID],[Name],[CodesCommaSeparated])
VALUES('bbbbbbbb-d9b7-42e1-b6a1-ffffb0e29b23','Черкаська ОД','ck')
INSERT INTO [dbo].[Regions]([ID],[Name],[CodesCommaSeparated])
VALUES('bbbbbbbb-d9b7-42e1-b6a1-ffffb0e29b24','Чернігівська ОД','cn')
INSERT INTO [dbo].[Regions]([ID],[Name],[CodesCommaSeparated])
VALUES('bbbbbbbb-d9b7-42e1-b6a1-ffffb0e29b25','Чернівецька ОД','cv')
INSERT INTO [dbo].[Regions]([ID],[Name],[CodesCommaSeparated])
VALUES('bbbbbbbb-d9b7-42e1-b6a1-ffffb0e29b26','Севастопольска ОФ','sb')
INSERT INTO [dbo].[Regions]([ID],[Name],[CodesCommaSeparated])
VALUES(NULL,'по Україні',NULL)