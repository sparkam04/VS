
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 06/07/2015 11:44:15
-- Generated from EDMX file: C:\Users\Alexander\documents\visual studio 2013\Projects\News_MVC\News_MVC\Models\Model_EDM.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [News_MVC];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Articles_AspNetUsers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Articles] DROP CONSTRAINT [FK_Articles_AspNetUsers];
GO
IF OBJECT_ID(N'[dbo].[FK_ArticleTags_Articles]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ArticleTags] DROP CONSTRAINT [FK_ArticleTags_Articles];
GO
IF OBJECT_ID(N'[dbo].[FK_ArticleTags_Tags]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ArticleTags] DROP CONSTRAINT [FK_ArticleTags_Tags];
GO
IF OBJECT_ID(N'[dbo].[FK_UserInfo_AspNetUsers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserInfo] DROP CONSTRAINT [FK_UserInfo_AspNetUsers];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Articles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Articles];
GO
IF OBJECT_ID(N'[dbo].[AspNetUsers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUsers];
GO
IF OBJECT_ID(N'[dbo].[Tags]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Tags];
GO
IF OBJECT_ID(N'[dbo].[UserInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserInfo];
GO
IF OBJECT_ID(N'[dbo].[ArticleTags]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ArticleTags];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Articles'
CREATE TABLE [dbo].[Articles] (
    [ArticleID] int IDENTITY(1,1) NOT NULL,
    [AuthorID] nvarchar(128)  NOT NULL,
    [ArticleName] nvarchar(250)  NULL,
    [ArticleContent] varchar(max)  NULL,
    [CreationDate] datetime  NOT NULL,
    [ToHomePage] bit  NOT NULL,
    [ToArchive] bit  NOT NULL,
    [DateModitied] datetime  NULL,
    [Priority] int  NULL
);
GO

-- Creating table 'AspNetUsers'
CREATE TABLE [dbo].[AspNetUsers] (
    [Id] nvarchar(128)  NOT NULL,
    [Email] nvarchar(256)  NULL,
    [EmailConfirmed] bit  NOT NULL,
    [PasswordHash] nvarchar(max)  NULL,
    [SecurityStamp] nvarchar(max)  NULL,
    [PhoneNumber] nvarchar(max)  NULL,
    [PhoneNumberConfirmed] bit  NOT NULL,
    [TwoFactorEnabled] bit  NOT NULL,
    [LockoutEndDateUtc] datetime  NULL,
    [LockoutEnabled] bit  NOT NULL,
    [AccessFailedCount] int  NOT NULL,
    [UserName] nvarchar(256)  NOT NULL
);
GO

-- Creating table 'Tags'
CREATE TABLE [dbo].[Tags] (
    [TagID] int IDENTITY(1,1) NOT NULL,
    [TagName] nvarchar(50)  NULL
);
GO

-- Creating table 'UserInfo'
CREATE TABLE [dbo].[UserInfo] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [GUID] nvarchar(128)  NOT NULL,
    [FirstName] nvarchar(50)  NULL,
    [LastName] nvarchar(50)  NULL,
    [Address] nvarchar(50)  NULL
);
GO

-- Creating table 'ArticleTags'
CREATE TABLE [dbo].[ArticleTags] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ArticleID] int  NOT NULL,
    [TagID] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ArticleID] in table 'Articles'
ALTER TABLE [dbo].[Articles]
ADD CONSTRAINT [PK_Articles]
    PRIMARY KEY CLUSTERED ([ArticleID] ASC);
GO

-- Creating primary key on [Id] in table 'AspNetUsers'
ALTER TABLE [dbo].[AspNetUsers]
ADD CONSTRAINT [PK_AspNetUsers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [TagID] in table 'Tags'
ALTER TABLE [dbo].[Tags]
ADD CONSTRAINT [PK_Tags]
    PRIMARY KEY CLUSTERED ([TagID] ASC);
GO

-- Creating primary key on [ID] in table 'UserInfo'
ALTER TABLE [dbo].[UserInfo]
ADD CONSTRAINT [PK_UserInfo]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [Id] in table 'ArticleTags'
ALTER TABLE [dbo].[ArticleTags]
ADD CONSTRAINT [PK_ArticleTags]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [AuthorID] in table 'Articles'
ALTER TABLE [dbo].[Articles]
ADD CONSTRAINT [FK_Articles_AspNetUsers]
    FOREIGN KEY ([AuthorID])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Articles_AspNetUsers'
CREATE INDEX [IX_FK_Articles_AspNetUsers]
ON [dbo].[Articles]
    ([AuthorID]);
GO

-- Creating foreign key on [ArticleID] in table 'ArticleTags'
ALTER TABLE [dbo].[ArticleTags]
ADD CONSTRAINT [FK_ArticleTags_Articles]
    FOREIGN KEY ([ArticleID])
    REFERENCES [dbo].[Articles]
        ([ArticleID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ArticleTags_Articles'
CREATE INDEX [IX_FK_ArticleTags_Articles]
ON [dbo].[ArticleTags]
    ([ArticleID]);
GO

-- Creating foreign key on [TagID] in table 'ArticleTags'
ALTER TABLE [dbo].[ArticleTags]
ADD CONSTRAINT [FK_ArticleTags_Tags]
    FOREIGN KEY ([TagID])
    REFERENCES [dbo].[Tags]
        ([TagID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ArticleTags_Tags'
CREATE INDEX [IX_FK_ArticleTags_Tags]
ON [dbo].[ArticleTags]
    ([TagID]);
GO

-- Creating foreign key on [GUID] in table 'UserInfo'
ALTER TABLE [dbo].[UserInfo]
ADD CONSTRAINT [FK_UserInfo_AspNetUsers]
    FOREIGN KEY ([GUID])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserInfo_AspNetUsers'
CREATE INDEX [IX_FK_UserInfo_AspNetUsers]
ON [dbo].[UserInfo]
    ([GUID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------