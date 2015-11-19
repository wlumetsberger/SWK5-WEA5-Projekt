CREATE TABLE [Catagory] (
    [Id]          INT IDENTITY (1, 1) NOT NULL,
    [Name]        VARCHAR (50)  NULL,
    [Description] VARCHAR (255) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [Artist] (
    [Id]		INT IDENTITY(1,1) NOT NULL,
	[Email]     VARCHAR (255) NOT NULL,
    [Name] VARCHAR (255) NULL,
    [Link]  VARCHAR (255) NULL,
    [Picture]   VARCHAR (MAX)  NULL,
    [Catagory]  INT            NULL,
    [Country]   NVARCHAR (3)   NULL,
	[Deleted] BIT NOT NULL DEFAULT 0, 
    PRIMARY KEY CLUSTERED (Id ASC),
	CONSTRAINT fk_artist_catagory FOREIGN KEY([Catagory]) REFERENCES [Catagory](Id),
);

CREATE TABLE [Venue](
	[Id] INT IDENTITY(1,1) NOT NULL,
	[Description] VARCHAR(MAX) NULL,
	[ShortDescription] VARCHAR(255) NULL,
	[Address] VARCHAR(255) NULL,
	[Longitude] INT NULL,
	[Latitude] INT NULL,
	PRIMARY KEY CLUSTERED([Id] ASC)
);

CREATE TABLE [Performance](
	[Id] INT IDENTITY(1,1) NOT NULL,
	[StagingTime] DATETIME NULL,
	[Artist] INT NULL,
	[Venue] INT NULL,
	[Canceld] BIT NOT NULL DEFAULT 0, 
	PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT fk_performance_artist FOREIGN KEY (Artist) REFERENCES Artist(Id),
	CONSTRAINT fk_performance_venue FOREIGN KEY (Venue) REFERENCES Venue(Id)
);



CREATE TABLE [User](
	[Id] INT IDENTITY(1,1) NOT NULL,
	[Email] VARCHAR(255) NOT NULL,
	[FirstName] VARCHAR(255) NOT NULL,
	[LastName] VARCHAR(255) NOT NULL,
	[Password] VARCHAR(255) NOT NULL,
	PRIMARY KEY CLUSTERED ([Id] ASC)
);