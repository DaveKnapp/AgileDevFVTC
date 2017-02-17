
--Drop Foreign keys 
IF OBJECT_ID('User') IS NOT NULL
BEGIN
	AlTER TABLE "USER"
	DROP CONSTRAINT FK_User_UserTypeID
END


IF OBJECT_ID('UserIntigration') IS NOT NULL
BEGIN
	AlTER TABLE UserIntigration
	DROP CONSTRAINT FK_UserIntigration_UserID, FK_UserIntigration_IntigrationType
END

IF OBJECT_ID('UserGameJunc') IS NOT NULL
BEGIN
	ALTER TABLE UserGameJunc
	DROP CONSTRAINT FK_UserID_ID, FK_Game_ID
END

IF OBJECT_ID('UserRating') IS NOT NULL
BEGIN
	ALTER TABLE UserRating
	DROP CONSTRAINT FK_UserRater_UserID, FK_UserRated_UserID, FK_Rating_RatingID
END



--Drop Tables
IF OBJECT_ID('User') IS NOT NULL
	DROP TABLE "User"

IF OBJECT_ID('UserType') IS NOT NULL
	DROP TABLE UserType


IF OBJECT_ID('UserIntigration') IS NOT NULL
	DROP TABLE UserIntigration

IF OBJECT_ID('IntigrationType') IS NOT NULL
	DROP TABLE IntigrationType

IF OBJECT_ID('UserRating') IS NOT NULL
	DROP TABLE UserRating


IF OBJECT_ID('Rating') IS NOT NULL
	DROP TABLE Rating
IF OBJECT_ID('UserGameJunc') IS NOT NULL
	DROP TABLE UserGameJunc

IF OBJECT_ID('Game') IS NOT NULL
	DROP TABLE Game


--Create Tables
CREATE TABLE "User"
   (ID int IDENTITY PRIMARY KEY NOT NULL,
	UserName varchar(25) NOT NULL,
	Email varchar(45) NOT NULL,
	Bio varchar(350) NOT NULL,
	ProfileImagePath varchar(60) NOT NULL,
	UserTypeID int NOT NULL,
	)

CREATE TABLE UserType
   (ID int PRIMARY KEY NOT NULL,
	Description varchar(45) NOT NULL
	)



CREATE TABLE IntigrationType
   (ID int PRIMARY KEY NOT NULL,
	Description varchar(45) NOT NULL
	)



CREATE TABLE UserRating
   (RaterUserID int NOT NULL,
	UserBeingRatedID int NOT NULL,
	Comment Varchar(255),
	RatingID int NOT NULL
	PRIMARY KEY(RaterUserID, UserBeingRatedID)
	)



CREATE TABLE Rating
   (ID int PRIMARY KEY NOT NULL,
	Description Varchar(45)
	)


CREATE TABLE UserGameJunc
   (UserID int NOT NULL,
	GameID int NOT NULL,
	PRIMARY KEY(UserID, GameID)
	)


CREATE TABLE Game
   (ID int IDENTITY PRIMARY KEY NOT NULL,
	Title varchar(70) NOT NULL,
	igdbID int
	)


CREATE TABLE UserIntigration
   (UserID int  NOT NULL,
	IntigrationTypeID int NOT NULL,
	Token varchar(80) NOT NULL,
	PRIMARY KEY(UserID, IntigrationTypeID)
	)


--Create Foreign keys

AlTER TABLE "USER"
ADD CONSTRAINT FK_User_UserTypeID FOREIGN KEY(UserTypeID) REFERENCES UserType(ID)

AlTER TABLE UserIntigration
ADD CONSTRAINT FK_UserIntigration_UserID FOREIGN KEY(UserID) REFERENCES "User"(ID),
    CONSTRAINT FK_UserIntigration_IntigrationType FOREIGN KEY(IntigrationTypeID) REFERENCES IntigrationType(ID)

ALTER TABLE UserGameJunc
ADD CONSTRAINT FK_UserID_ID FOREIGN KEY(UserID) REFERENCES "User"(ID),
	CONSTRAINT FK_Game_ID	FOREIGN KEY(GameID) REFERENCES Game(ID)

ALTER TABLE UserRating
ADD CONSTRAINT FK_UserRater_UserID FOREIGN KEY(RaterUserID) REFERENCES "USER"(ID),
	CONSTRAINT FK_UserRated_UserID	FOREIGN KEY(UserBeingRatedID) REFERENCES "USER"(ID),
	CONSTRAINT FK_Rating_RatingID FOREIGN KEY(RatingID) REFERENCES Rating(ID)

