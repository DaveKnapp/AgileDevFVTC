--Delete all data in tables
BEGIN TRANSACTION 

	DELETE FROM UserRating
	DELETE FROM UserSocialJunc
	DELETE FROM UserIntigration
	DELETE FROM UserGameJunc
	DELETE FROM "User"
	DELETE FROM UserLogin
	DELETE FROM UserType
	DELETE FROM Nationality
	DELETE FROM Game
	DELETE FROM IntigrationType
	DELETE FROM SocialMediaType
	DELETE FROM GameCategory
	DELETE FROM Rating

COMMIT TRANSACTION

GO
--ADD Test Data
BEGIN TRANSACTION
	SET IDENTITY_INSERT Nationality ON;

	INSERT INTO Nationality (ID, "Description")
		VALUES(1, 'US and A')
	SET IDENTITY_INSERT Nationality OFF;

	SET IDENTITY_INSERT UserType ON;
	INSERT INTO UserType(ID, "Description")
		VALUES(1, 'User')
	SET IDENTITY_INSERT UserType OFF;

	INSERT INTO UserLogin(UserID, "Password")
		VALUES(1,'Password'),
			  (2,'Password'),
			  (3,'Password'),
			  (4,'Password'),
			  (5,'Password')

	SET IDENTITY_INSERT "User" ON;
	INSERT INTO "User" (ID, UserName, Email, Bio, ProfileImagePath, DateJoined, DOB, Gender, UserTypeID, NationalityID)
		VALUES(1, 'TestUserOne', 'Testing123@yahoo.com', 'This is my bio', '../Images/TestUserOne/profile.png','2-20-17', '11-12-1988', 'M', 1,1),
			  (2, 'TestUserTwo', 'TestingUser2@yahoo.com', 'Hello I am the second test user', '../Images/TestUserTwo/Profile.png','2-22-17', '1-27-1980', 'F', 1,1),
			  (3, 'TestUserThree', 'TestingUser3@yahoo.com', 'Hello I am the Third test user', '../Images/TestUserThree/Profile.png','1-5-17', '6-1-1990', 'M', 1,1),
			  (4, 'TestUserFour', 'TestingUser4@yahoo.com', 'Hello I am the Fourth test user', '../Images/TestUserFour/Profile.png','1-13-17', '4-27-1991', 'M', 1,1),
			  (5, 'TestUserFive', 'TestingUser5@yahoo.com', 'Hello I am the Fifth test user', '../Images/TestUserFive/Profile.png','1-19-17', '7-9-1962', 'F', 1,1)
	SET IDENTITY_INSERT "User" OFF;

	SET IDENTITY_INSERT IntigrationType ON;
	INSERT INTO IntigrationType(ID,"Description")
		VALUES(1, 'Twitch')
	SET IDENTITY_INSERT IntigrationType OFF;

	SET IDENTITY_INSERT Rating ON;
		INSERT INTO Rating(ID, "Description")
		VALUES(1, 'One Star'),
			  (2, 'Two Stars'),
			  (3, 'Three Stars'),
			  (4, 'Four Stars'),
			  (5, 'Five Stars')
	SET IDENTITY_INSERT Rating OFF;

	SET IDENTITY_INSERT UserRating ON;
		INSERT INTO UserRating(RaterUserID, UserBeingRatedID, Comment, RatingID)
			VALUES(1,2,'10/10 would watch again', 5),
				  (3,2,'Okay', 4),
				  (4,2,'Elbows too pointy', 1),
				  (2,1,'Alright', 3),
				  (3,4,'Fun times', 4),
				  (4,5,'5/5 would watch again', 5),
				  (5,3,'Best stream ever', 5),
				  (3,1,'boring', 2)
	SET IDENTITY_INSERT UserRating OFF;

	SET IDENTITY_INSERT SocialMediaType ON;
	INSERT INTO SocialMediaType(ID, "Description")
		VALUES(1, 'Youtube'),
			  (2, 'Twitter'),
			  (3, 'Instagram')
	SET IDENTITY_INSERT SocialMediaType OFF;

	INSERT INTO UserSocialJunc(UserID, SocialMediaTypeID, "URL")
		VALUES (1,1, 'youtube.com/channel/TestUserOne'),
			   (1,2, 'twitter.com/TestUserOne'),
			   (1,3, 'instagram.com/TestUserOne')

COMMIT TRANSACTION

