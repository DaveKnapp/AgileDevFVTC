
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 03/11/2017 23:10:16
-- Generated from EDMX file: C:\Users\zzdia\Source\Repos\AgileDevFVTC\T5.Brothership\T5.Brothership.PL\Brothership.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [brothership];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Game_Category]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Games] DROP CONSTRAINT [FK_Game_Category];
GO
IF OBJECT_ID(N'[dbo].[FK_UserIntegration_IntegrationType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserIntegrations] DROP CONSTRAINT [FK_UserIntegration_IntegrationType];
GO
IF OBJECT_ID(N'[dbo].[FK_User_Nationality]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Users] DROP CONSTRAINT [FK_User_Nationality];
GO
IF OBJECT_ID(N'[dbo].[FK_Rating_RatingID]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserRatings] DROP CONSTRAINT [FK_Rating_RatingID];
GO
IF OBJECT_ID(N'[dbo].[FK_UserSocial_SocialType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserSocialJuncs] DROP CONSTRAINT [FK_UserSocial_SocialType];
GO
IF OBJECT_ID(N'[dbo].[FK_UserIntegration_UserID]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserIntegrations] DROP CONSTRAINT [FK_UserIntegration_UserID];
GO
IF OBJECT_ID(N'[dbo].[FK_User_UserID]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Users] DROP CONSTRAINT [FK_User_UserID];
GO
IF OBJECT_ID(N'[dbo].[FK_UserRated_UserID]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserRatings] DROP CONSTRAINT [FK_UserRated_UserID];
GO
IF OBJECT_ID(N'[dbo].[FK_UserRater_UserID]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserRatings] DROP CONSTRAINT [FK_UserRater_UserID];
GO
IF OBJECT_ID(N'[dbo].[FK_User_UserTypeID]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Users] DROP CONSTRAINT [FK_User_UserTypeID];
GO
IF OBJECT_ID(N'[dbo].[FK_UserSocial_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserSocialJuncs] DROP CONSTRAINT [FK_UserSocial_User];
GO
IF OBJECT_ID(N'[dbo].[FK_UserGameJunc_Game]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserGameJunc] DROP CONSTRAINT [FK_UserGameJunc_Game];
GO
IF OBJECT_ID(N'[dbo].[FK_UserGameJunc_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserGameJunc] DROP CONSTRAINT [FK_UserGameJunc_User];
GO
IF OBJECT_ID(N'[dbo].[FK_GenderUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Users] DROP CONSTRAINT [FK_GenderUser];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[GameCategories]', 'U') IS NOT NULL
    DROP TABLE [dbo].[GameCategories];
GO
IF OBJECT_ID(N'[dbo].[Games]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Games];
GO
IF OBJECT_ID(N'[dbo].[IntegrationTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[IntegrationTypes];
GO
IF OBJECT_ID(N'[dbo].[Nationalities]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Nationalities];
GO
IF OBJECT_ID(N'[dbo].[Ratings]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Ratings];
GO
IF OBJECT_ID(N'[dbo].[SocialMediaTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SocialMediaTypes];
GO
IF OBJECT_ID(N'[dbo].[UserIntegrations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserIntegrations];
GO
IF OBJECT_ID(N'[dbo].[UserLogins]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserLogins];
GO
IF OBJECT_ID(N'[dbo].[UserRatings]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserRatings];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[dbo].[UserSocialJuncs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserSocialJuncs];
GO
IF OBJECT_ID(N'[dbo].[UserTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserTypes];
GO
IF OBJECT_ID(N'[dbo].[Genders]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Genders];
GO
IF OBJECT_ID(N'[dbo].[UserGameJunc]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserGameJunc];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'GameCategories'
CREATE TABLE [dbo].[GameCategories] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Description] varchar(45)  NOT NULL
);
GO

-- Creating table 'Games'
CREATE TABLE [dbo].[Games] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Title] varchar(70)  NOT NULL,
    [igdbID] int  NULL,
    [CategoryID] int  NOT NULL,
    [CoverImgUrl] nvarchar(max)  NULL
);
GO

-- Creating table 'IntegrationTypes'
CREATE TABLE [dbo].[IntegrationTypes] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Description] varchar(45)  NOT NULL
);
GO

-- Creating table 'Nationalities'
CREATE TABLE [dbo].[Nationalities] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Description] varchar(45)  NOT NULL
);
GO

-- Creating table 'Ratings'
CREATE TABLE [dbo].[Ratings] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Description] varchar(45)  NULL
);
GO

-- Creating table 'SocialMediaTypes'
CREATE TABLE [dbo].[SocialMediaTypes] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Description] varchar(45)  NULL
);
GO

-- Creating table 'UserIntegrations'
CREATE TABLE [dbo].[UserIntegrations] (
    [UserID] int  NOT NULL,
    [IntegrationTypeID] int  NOT NULL,
    [Token] varchar(80)  NOT NULL
);
GO

-- Creating table 'UserLogins'
CREATE TABLE [dbo].[UserLogins] (
    [UserID] int IDENTITY(1,1) NOT NULL,
    [PasswordHash] varchar(64)  NOT NULL,
    [Salt] nvarchar(64)  NOT NULL
);
GO

-- Creating table 'UserRatings'
CREATE TABLE [dbo].[UserRatings] (
    [RaterUserID] int  NOT NULL,
    [UserBeingRatedID] int  NOT NULL,
    [Comment] varchar(255)  NULL,
    [RatingID] int  NOT NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [ID] int  NOT NULL,
    [UserName] varchar(25)  NOT NULL,
    [Email] varchar(45)  NOT NULL,
    [Bio] varchar(350)  NOT NULL,
    [ProfileImagePath] varchar(60)  NOT NULL,
    [DateJoined] datetime  NOT NULL,
    [DOB] datetime  NOT NULL,
    [UserTypeID] int  NOT NULL,
    [NationalityID] int  NOT NULL,
    [GenderId] smallint  NOT NULL
);
GO

-- Creating table 'UserSocialJuncs'
CREATE TABLE [dbo].[UserSocialJuncs] (
    [UserID] int  NOT NULL,
    [SocialMediaTypeID] int  NOT NULL,
    [URL] varchar(70)  NOT NULL
);
GO

-- Creating table 'UserTypes'
CREATE TABLE [dbo].[UserTypes] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Description] varchar(45)  NOT NULL
);
GO

-- Creating table 'Genders'
CREATE TABLE [dbo].[Genders] (
    [Id] smallint IDENTITY(1,1) NOT NULL,
    [Description] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'UserGameJunc'
CREATE TABLE [dbo].[UserGameJunc] (
    [Games_ID] int  NOT NULL,
    [Users_ID] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ID] in table 'GameCategories'
ALTER TABLE [dbo].[GameCategories]
ADD CONSTRAINT [PK_GameCategories]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Games'
ALTER TABLE [dbo].[Games]
ADD CONSTRAINT [PK_Games]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'IntegrationTypes'
ALTER TABLE [dbo].[IntegrationTypes]
ADD CONSTRAINT [PK_IntegrationTypes]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Nationalities'
ALTER TABLE [dbo].[Nationalities]
ADD CONSTRAINT [PK_Nationalities]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Ratings'
ALTER TABLE [dbo].[Ratings]
ADD CONSTRAINT [PK_Ratings]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'SocialMediaTypes'
ALTER TABLE [dbo].[SocialMediaTypes]
ADD CONSTRAINT [PK_SocialMediaTypes]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [UserID], [IntegrationTypeID] in table 'UserIntegrations'
ALTER TABLE [dbo].[UserIntegrations]
ADD CONSTRAINT [PK_UserIntegrations]
    PRIMARY KEY CLUSTERED ([UserID], [IntegrationTypeID] ASC);
GO

-- Creating primary key on [UserID] in table 'UserLogins'
ALTER TABLE [dbo].[UserLogins]
ADD CONSTRAINT [PK_UserLogins]
    PRIMARY KEY CLUSTERED ([UserID] ASC);
GO

-- Creating primary key on [RaterUserID], [UserBeingRatedID] in table 'UserRatings'
ALTER TABLE [dbo].[UserRatings]
ADD CONSTRAINT [PK_UserRatings]
    PRIMARY KEY CLUSTERED ([RaterUserID], [UserBeingRatedID] ASC);
GO

-- Creating primary key on [ID] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [UserID], [SocialMediaTypeID] in table 'UserSocialJuncs'
ALTER TABLE [dbo].[UserSocialJuncs]
ADD CONSTRAINT [PK_UserSocialJuncs]
    PRIMARY KEY CLUSTERED ([UserID], [SocialMediaTypeID] ASC);
GO

-- Creating primary key on [ID] in table 'UserTypes'
ALTER TABLE [dbo].[UserTypes]
ADD CONSTRAINT [PK_UserTypes]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [Id] in table 'Genders'
ALTER TABLE [dbo].[Genders]
ADD CONSTRAINT [PK_Genders]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Games_ID], [Users_ID] in table 'UserGameJunc'
ALTER TABLE [dbo].[UserGameJunc]
ADD CONSTRAINT [PK_UserGameJunc]
    PRIMARY KEY CLUSTERED ([Games_ID], [Users_ID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [CategoryID] in table 'Games'
ALTER TABLE [dbo].[Games]
ADD CONSTRAINT [FK_Game_Category]
    FOREIGN KEY ([CategoryID])
    REFERENCES [dbo].[GameCategories]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Game_Category'
CREATE INDEX [IX_FK_Game_Category]
ON [dbo].[Games]
    ([CategoryID]);
GO

-- Creating foreign key on [IntegrationTypeID] in table 'UserIntegrations'
ALTER TABLE [dbo].[UserIntegrations]
ADD CONSTRAINT [FK_UserIntegration_IntegrationType]
    FOREIGN KEY ([IntegrationTypeID])
    REFERENCES [dbo].[IntegrationTypes]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserIntegration_IntegrationType'
CREATE INDEX [IX_FK_UserIntegration_IntegrationType]
ON [dbo].[UserIntegrations]
    ([IntegrationTypeID]);
GO

-- Creating foreign key on [NationalityID] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [FK_User_Nationality]
    FOREIGN KEY ([NationalityID])
    REFERENCES [dbo].[Nationalities]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_User_Nationality'
CREATE INDEX [IX_FK_User_Nationality]
ON [dbo].[Users]
    ([NationalityID]);
GO

-- Creating foreign key on [RatingID] in table 'UserRatings'
ALTER TABLE [dbo].[UserRatings]
ADD CONSTRAINT [FK_Rating_RatingID]
    FOREIGN KEY ([RatingID])
    REFERENCES [dbo].[Ratings]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Rating_RatingID'
CREATE INDEX [IX_FK_Rating_RatingID]
ON [dbo].[UserRatings]
    ([RatingID]);
GO

-- Creating foreign key on [SocialMediaTypeID] in table 'UserSocialJuncs'
ALTER TABLE [dbo].[UserSocialJuncs]
ADD CONSTRAINT [FK_UserSocial_SocialType]
    FOREIGN KEY ([SocialMediaTypeID])
    REFERENCES [dbo].[SocialMediaTypes]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserSocial_SocialType'
CREATE INDEX [IX_FK_UserSocial_SocialType]
ON [dbo].[UserSocialJuncs]
    ([SocialMediaTypeID]);
GO

-- Creating foreign key on [UserID] in table 'UserIntegrations'
ALTER TABLE [dbo].[UserIntegrations]
ADD CONSTRAINT [FK_UserIntegration_UserID]
    FOREIGN KEY ([UserID])
    REFERENCES [dbo].[Users]
        ([ID])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [ID] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [FK_User_UserID]
    FOREIGN KEY ([ID])
    REFERENCES [dbo].[UserLogins]
        ([UserID])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [UserBeingRatedID] in table 'UserRatings'
ALTER TABLE [dbo].[UserRatings]
ADD CONSTRAINT [FK_UserRated_UserID]
    FOREIGN KEY ([UserBeingRatedID])
    REFERENCES [dbo].[Users]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserRated_UserID'
CREATE INDEX [IX_FK_UserRated_UserID]
ON [dbo].[UserRatings]
    ([UserBeingRatedID]);
GO

-- Creating foreign key on [RaterUserID] in table 'UserRatings'
ALTER TABLE [dbo].[UserRatings]
ADD CONSTRAINT [FK_UserRater_UserID]
    FOREIGN KEY ([RaterUserID])
    REFERENCES [dbo].[Users]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [UserTypeID] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [FK_User_UserTypeID]
    FOREIGN KEY ([UserTypeID])
    REFERENCES [dbo].[UserTypes]
        ([ID])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_User_UserTypeID'
CREATE INDEX [IX_FK_User_UserTypeID]
ON [dbo].[Users]
    ([UserTypeID]);
GO

-- Creating foreign key on [UserID] in table 'UserSocialJuncs'
ALTER TABLE [dbo].[UserSocialJuncs]
ADD CONSTRAINT [FK_UserSocial_User]
    FOREIGN KEY ([UserID])
    REFERENCES [dbo].[Users]
        ([ID])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Games_ID] in table 'UserGameJunc'
ALTER TABLE [dbo].[UserGameJunc]
ADD CONSTRAINT [FK_UserGameJunc_Game]
    FOREIGN KEY ([Games_ID])
    REFERENCES [dbo].[Games]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Users_ID] in table 'UserGameJunc'
ALTER TABLE [dbo].[UserGameJunc]
ADD CONSTRAINT [FK_UserGameJunc_User]
    FOREIGN KEY ([Users_ID])
    REFERENCES [dbo].[Users]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserGameJunc_User'
CREATE INDEX [IX_FK_UserGameJunc_User]
ON [dbo].[UserGameJunc]
    ([Users_ID]);
GO

-- Creating foreign key on [GenderId] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [FK_GenderUser]
    FOREIGN KEY ([GenderId])
    REFERENCES [dbo].[Genders]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GenderUser'
CREATE INDEX [IX_FK_GenderUser]
ON [dbo].[Users]
    ([GenderId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------