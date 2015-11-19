SET IDENTITY_INSERT [Catagory] ON
INSERT INTO [Catagory] ([Id],[Name], [Description]) VALUES(1,'Klassik','Klassik');
INSERT INTO [Catagory] ([Id],[Name], [Description]) VALUES(2,'Sport','Sport');
SET IDENTITY_INSERT [Catagory] OFF

SET IDENTITY_INSERT [Artist] ON
INSERT INTO [Artist] ([Id],[Email],Name , Link, [Catagory])
SELECT 1 as Id,'l.page@gmx.com' as Email,'Larry Page' as Name,'' as Link,[Catagory].[Id] as Catagory FROM [Catagory] WHERE [Catagory].[Name] = 'Klassik'
INSERT INTO [Artist] ([Id],[Email],Name , Link, [Catagory])
SELECT 2 as Id,'m.page@gmx.com' as Email,'Marry Page' as Name,'' as Link,[Catagory].[Id] as Catagory FROM [Catagory] WHERE [Catagory].[Name] = 'Sport'
INSERT INTO [Artist] ([Id],[Email],Name , Link, [Catagory])
SELECT 3 as Id,'todelete@test.at' as Email,'TO DELETE' as Name,'' as Link,[Catagory].[Id] as Catagory FROM [Catagory] WHERE [Catagory].[Name] = 'Sport'
SET IDENTITY_INSERT [Artist] OFF

SET IDENTITY_INSERT [User] ON
INSERT INTO [User] ([Id],[Email],[FirstName],[LastName],[Password]) VALUES (1,'admin@test.at','admin','admin','123');
INSERT INTO [User] ([Id],[Email],[FirstName],[LastName],[Password]) VALUES (2,'user@test.at','user','user','123');
SET IDENTITY_INSERT [User] OFF

SET IDENTITY_INSERT [Venue] ON
INSERT INTO [Venue] ([Id],[Description],[ShortDescription],[Longitude],[Latitude])
VALUES(1,'Stage1','Stage1',1,1);
INSERT INTO [Venue] ([Id],[Description],[ShortDescription],[Longitude],[Latitude])
VALUES(2,'Stage2','Stage2',2,2);
SET IDENTITY_INSERT [Venue] OFF

SET IDENTITY_INSERT [Performance] ON
INSERT INTO [Performance] ([Id],[StagingTime],[Artist],[Venue])
Values(1,CURRENT_TIMESTAMP,1,1);
INSERT INTO [Performance] ([Id],[StagingTime],[Artist],[Venue])
Values(2,CURRENT_TIMESTAMP,2,2);
SET IDENTITY_INSERT [Performance] OFF

GO