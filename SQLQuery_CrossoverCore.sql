CREATE TABLE [dbo].[Roles]
(
	[Id] INT NOT NULL IDENTITY(1, 1),
	[Name] VARCHAR(32) NOT NULL,
)
GO

ALTER TABLE [dbo].[Roles]	
	ADD CONSTRAINT PK_Roles_Id PRIMARY KEY([Id])
GO

ALTER TABLE [dbo].[Roles]
	ADD CONSTRAINT UK_Roles_Name UNIQUE ([Name])
GO

CREATE TABLE [dbo].[Users]
(
	[Id] INT NOT NULL IDENTITY(1, 1),
	[Email] NVARCHAR(32) NOT NULL,
	[Password] VARCHAR(32) NOT NULL,
	[RoleId] INT NOT NULL
)
GO

ALTER TABLE [dbo].[Users]	
	ADD CONSTRAINT PK_Users_Id PRIMARY KEY([Id])
GO

ALTER TABLE [dbo].[Users]
	ADD CONSTRAINT UK_Users_Email UNIQUE ([Email])
GO

ALTER TABLE [dbo].[Users]
	ADD CONSTRAINT FK_Users_RoleId FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Roles]([Id])
GO

CREATE TABLE [dbo].[Features]
(
	[Id] INT NOT NULL IDENTITY(1, 1),
	[Name] VARCHAR(32) NOT NULL,
	[RouteUrl] VARCHAR(MAX) NOT NULL
)
GO

ALTER TABLE [dbo].[Features]
	ADD CONSTRAINT PK_Features_Id PRIMARY KEY([Id])
GO

ALTER TABLE [dbo].[Features]
	ADD CONSTRAINT UK_Features_Name UNIQUE ([Name])
GO

CREATE TABLE [dbo].[RoleFeatures]
(
	[Id] INT NOT NULL IDENTITY(1, 1),
	[RoleId] INT NOT NULL,
	[FeatureId] INT NOT NULL
)
GO

ALTER TABLE [dbo].[RoleFeatures]
	ADD CONSTRAINT PK_RoleFeatures_Id PRIMARY KEY([Id])
GO

ALTER TABLE [dbo].[RoleFeatures]
	ADD CONSTRAINT FK_RoleFeatures_RoleId FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Roles]([Id])
GO

ALTER TABLE [dbo].[RoleFeatures]
	ADD CONSTRAINT FK_RoleFeatures_FeatureId FOREIGN KEY ([FeatureId]) REFERENCES [dbo].[Features]([Id])
GO