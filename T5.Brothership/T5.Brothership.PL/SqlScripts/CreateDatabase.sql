----  THIS IS AN OLD SCRIPT USE Brothership.edmx.sql
GO
--Drop Foreign keys
IF OBJECT_ID('User') IS NOT NULL
BEGIN
	AlTER TABLE "User"
	DROP CONSTRAINT FK_User_UserTypeID, FK_User_Nationality, FK_User_UserID
END

IF OBJECT_ID('UserSocialJunc') IS NOT NULL
BEGIN
	ALTER TABLE UserSocialJunc
	DROP CONSTRAINT FK_UserSocial_User, FK_UserSocial_SocialType
END

IF OBJECT_ID('UserIntegration') IS NOT NULL
BEGIN
	AlTER TABLE UserIntegration
	DROP CONSTRAINT FK_UserIntegration_UserID, FK_UserIntegration_IntegrationType
END

IF OBJECT_ID('UserGameJunc') IS NOT NULL
BEGIN
	ALTER TABLE UserGameJunc
	DROP CONSTRAINT FK_UserID_ID, FK_Game_ID
END

IF OBJECT_ID('GameCategory') IS NOT NULL
BEGIN
	ALTER TABLE Game
	DROP CONSTRAINT FK_Game_Category
END

IF OBJECT_ID('UserRating') IS NOT NULL
BEGIN
	ALTER TABLE UserRating
	DROP CONSTRAINT FK_UserRater_UserID, FK_UserRated_UserID, FK_Rating_RatingID
END

GO

--Drop Tables
IF OBJECT_ID('User') IS NOT NULL
	DROP TABLE "User"

IF OBJECT_ID('UserSocialJunc') IS NOT NULL
	DROP TABLE UserSocialJunc

IF OBJECT_ID('SocialMediaType') IS NOT NULL
	DROP TABLE SocialMediaType

IF OBJECT_ID('UserType') IS NOT NULL
	DROP TABLE UserType

IF OBJECT_ID('Nationality') IS NOT NULL
	DROP TABLE Nationality

IF OBJECT_ID('UserIntegration') IS NOT NULL
	DROP TABLE UserIntegration

IF OBJECT_ID('IntegrationType') IS NOT NULL
	DROP TABLE IntegrationType

IF OBJECT_ID('UserRating') IS NOT NULL
	DROP TABLE UserRating

IF OBJECT_ID('Rating') IS NOT NULL
	DROP TABLE Rating
IF OBJECT_ID('UserGameJunc') IS NOT NULL
	DROP TABLE UserGameJunc

IF OBJECT_ID('Game') IS NOT NULL
	DROP TABLE Game

IF OBJECT_ID('GameCategory') IS NOT NULL
	DROP TABLE GameCategory

IF OBJECT_ID('UserLogin') IS NOT NULL
	DROP TABLE UserLogin

	GO
--Create Tables
CREATE TABLE "User"
   (ID int IDENTITY PRIMARY KEY NOT NULL,
	UserName varchar(25) NOT NULL,
	Email varchar(45) NOT NULL,
	Bio varchar(350) NOT NULL,
	ProfileImagePath varchar(60) NOT NULL,
	DateJoined DATETIME NOT NULL,
	DOB DATETIME NOT NULL,
	Gender Char(1) NOT NULL,
	UserTypeID int NOT NULL,
	NationalityID int NOT NULL,
	)

CREATE TABLE Nationality
	(ID int IDENTITY PRIMARY KEY NOT NULL,
	 "Description" varchar(45) NOT NULL
	)

CREATE TABLE UserSocialJunc
	(UserID int NOT NULL,
	SocialMediaTypeID int NOT NULL,
	"URL" varchar(70) NOT NULL,
	PRIMARY KEY(UserID, SocialMediaTypeID)
	)

CREATE TABLE SocialMediaType
	(ID int IDENTITY PRIMARY KEY NOT NULL,
	"Description" varchar(45)
	)

CREATE TABLE UserLogin
	(UserID int PRIMARY KEY NOT NULL,
	 "Password" varchar(25) NOT NULL
	)

CREATE TABLE UserType
   (ID int IDENTITY PRIMARY KEY NOT NULL,
	"Description" varchar(45) NOT NULL
	)

CREATE TABLE IntegrationType
   (ID int IDENTITY PRIMARY KEY NOT NULL,
	"Description" varchar(45) NOT NULL
	)

CREATE TABLE UserRating
   (RaterUserID int IDENTITY NOT NULL,
	UserBeingRatedID int NOT NULL,
	Comment Varchar(255),
	RatingID int NOT NULL
	PRIMARY KEY(RaterUserID, UserBeingRatedID)
	)

CREATE TABLE Rating
   (ID int IDENTITY PRIMARY KEY NOT NULL,
	"Description" Varchar(45)
	)


CREATE TABLE UserGameJunc
   (UserID int NOT NULL,
	GameID int NOT NULL,
	PRIMARY KEY(UserID, GameID)
	)


CREATE TABLE Game
   (ID int IDENTITY PRIMARY KEY NOT NULL,
	Title varchar(70) NOT NULL,
	igdbID int,
	CategoryID int NOT NULL
	)

CREATE TABLE GameCategory
	(ID int IDENTITY PRIMARY KEY,
	"Description" varchar(45) NOT NULL
	)

CREATE TABLE UserIntegration
   (UserID int  NOT NULL,
	IntegrationTypeID int NOT NULL,
	Token varchar(80) NOT NULL,
	PRIMARY KEY(UserID, IntegrationTypeID)
	)
	GO

--Create Foreign keys

AlTER TABLE "USER"
ADD CONSTRAINT FK_User_UserTypeID FOREIGN KEY(UserTypeID) REFERENCES UserType(ID) ON DELETE CASCADE,
	CONSTRAINT FK_User_Nationality FOREIGN KEY(NationalityID) REFERENCES Nationality(ID),
	CONSTRAINT FK_User_UserID FOREIGN KEY(ID) REFERENCES UserLogin(UserID)

ALTER TABLE UserSocialJunc
ADD CONSTRAINT FK_UserSocial_User FOREIGN KEY(userID) REFERENCES "User"(ID),
	CONSTRAINT FK_UserSocial_SocialType FOREIGN KEY(SocialMediaTypeID) REFERENCES SocialMediaType(ID)

AlTER TABLE UserIntegration
ADD CONSTRAINT FK_UserIntegration_UserID FOREIGN KEY(UserID) REFERENCES "User"(ID),
	CONSTRAINT FK_UserIntegration_IntegrationType FOREIGN KEY(IntegrationTypeID) REFERENCES IntegrationType(ID)

ALTER TABLE UserGameJunc
ADD CONSTRAINT FK_UserID_ID FOREIGN KEY(UserID) REFERENCES "User"(ID),
	CONSTRAINT FK_Game_ID	FOREIGN KEY(GameID) REFERENCES Game(ID)

ALTER TABLE Game
ADD CONSTRAINT FK_Game_Category FOREIGN KEY(CategoryID) REFERENCES GameCategory(ID)

ALTER TABLE UserRating
ADD CONSTRAINT FK_UserRater_UserID FOREIGN KEY(RaterUserID) REFERENCES "USER"(ID),
	CONSTRAINT FK_UserRated_UserID	FOREIGN KEY(UserBeingRatedID) REFERENCES "USER"(ID),
	CONSTRAINT FK_Rating_RatingID FOREIGN KEY(RatingID) REFERENCES Rating(ID)