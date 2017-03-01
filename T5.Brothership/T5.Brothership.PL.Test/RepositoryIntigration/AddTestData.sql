--Delete all data in tables
BEGIN TRANSACTION 

	DELETE FROM UserRatings
	DELETE FROM UserSocialJuncs
	DELETE FROM UserIntegrations
	DELETE FROM UserGameJunc
	DELETE FROM Users
	DELETE FROM UserLogins
	DELETE FROM UserTypes
	DELETE FROM Nationalities
	DELETE FROM Games
	DELETE FROM IntegrationTypes
	DELETE FROM SocialMediaTypes
	DELETE FROM GameCategories
	DELETE FROM Ratings

COMMIT TRANSACTION

GO
--ADD Test Data
BEGIN TRANSACTION
	SET IDENTITY_INSERT Nationalities ON;

	INSERT INTO Nationalities (ID, "Description")
		VALUES(1, 'US and A')
	SET IDENTITY_INSERT Nationalities OFF;

	SET IDENTITY_INSERT UserTypes ON;
	INSERT INTO UserTypes(ID, "Description")
		VALUES(1, 'User')
	SET IDENTITY_INSERT UserTypes OFF;

SET IDENTITY_INSERT UserLogins ON;
	INSERT INTO UserLogins(UserID, "PasswordHash",Salt)
		VALUES(1,'Password','none'),
			  (2,'Password', 'none'),
			  (3,'Password', 'none'),
			  (4,'Password', 'none'),
			  (5,'Password', 'none')
SET IDENTITY_INSERT UserLogins OFF;



	INSERT INTO Users (ID, UserName, Email, Bio, ProfileImagePath, DateJoined, DOB, Gender, UserTypeID, NationalityID)
		VALUES(1, 'TestUserOne', 'Testing123@yahoo.com', 'This is my bio', '../Images/TestUserOne/profile.png','2-20-17', '11-12-1988', 'M', 1,1),
			  (2, 'TestUserTwo', 'TestingUser2@yahoo.com', 'Hello I am the second test user', '../Images/TestUserTwo/Profile.png','2-22-17', '1-27-1980', 'F', 1,1),
			  (3, 'TestUserThree', 'TestingUser3@yahoo.com', 'Hello I am the Third test user', '../Images/TestUserThree/Profile.png','1-5-17', '6-1-1990', 'M', 1,1),
			  (4, 'TestUserFour', 'TestingUser4@yahoo.com', 'Hello I am the Fourth test user', '../Images/TestUserFour/Profile.png','1-13-17', '4-27-1991', 'M', 1,1),
			  (5, 'TestUserFive', 'TestingUser5@yahoo.com', 'Hello I am the Fifth test user', '../Images/TestUserFive/Profile.png','1-19-17', '7-9-1962', 'F', 1,1)

	SET IDENTITY_INSERT IntegrationTypes ON;
	INSERT INTO IntegrationTypes(ID,"Description")
		VALUES(1, 'Twitch')
	SET IDENTITY_INSERT IntegrationTypes OFF;

	SET IDENTITY_INSERT Ratings ON;
		INSERT INTO Ratings(ID, "Description")
		VALUES(1, 'One Star'),
			  (2, 'Two Stars'),
			  (3, 'Three Stars'),
			  (4, 'Four Stars'),
			  (5, 'Five Stars')
	SET IDENTITY_INSERT Ratings OFF;

	SET IDENTITY_INSERT UserRatings ON;
		INSERT INTO UserRatings(RaterUserID, UserBeingRatedID, Comment, RatingID)
			VALUES(1,2,'10/10 would watch again', 5),
				  (3,2,'Okay', 4),
				  (4,2,'Elbows too pointy', 1),
				  (2,1,'Alright', 3),
				  (3,4,'Fun times', 4),
				  (4,5,'5/5 would watch again', 5),
				  (5,3,'Best stream ever', 5),
				  (3,1,'boring', 2)
	SET IDENTITY_INSERT UserRatings OFF;

	SET IDENTITY_INSERT SocialMediaTypes ON;
	INSERT INTO SocialMediaTypes(ID, "Description")
		VALUES(1, 'Youtube'),
			  (2, 'Twitter'),
			  (3, 'Instagram')
	SET IDENTITY_INSERT SocialMediaTypes OFF;

	INSERT INTO UserSocialJuncs(UserID, SocialMediaTypeID, "URL")
		VALUES (1,1, 'youtube.com/channel/TestUserOne'),
			   (1,2, 'twitter.com/TestUserOne'),
			   (1,3, 'instagram.com/TestUserOne')

	-- BEGIN GAME DATA INSERTS (To be updated or removed later if game API can be obtained)
	SET IDENTITY_INSERT GameCategories ON;
	INSERT INTO GameCategories(ID, "Description")
		VALUES	(1, 'Fighting'),
				(2, 'Action/Adventure'),
				(3, 'RPG'),
				(4, 'First-Person Shooter'),
				(5, 'Survival Horror'),
				(6, 'Strategy'),
				(7, 'Sports'),
				(8, 'Racing'),
				(9, 'Stealth'),
				(10, 'Simulation'),
				(11, 'MOBA'),
				(12, 'MMO'),
				(13, 'Puzzle'),
				(14, 'Tower Defense'),
				(15, 'Hack and Slash')

	SET IDENTITY_INSERT GameCategories OFF;

	SET IDENTITY_INSERT Games ON;
	INSERT INTO Games(ID, Title, igdbID, CategoryID)
		VALUES	-- Fighting Games
				(1, 'Street Figher V', null, 1),
				(2, 'Mortal Kombat X', null, 1),
				(3, 'Injustice: Gods Among Us', null, 1),
				-- Action/Adventure Games
				(4, 'Fallout 4', null, 2),
				(5, 'Uncharted 4', null, 2),
				(6, 'Grand Theft Auto V', null, 2),
				-- RPG Games
				(7, 'The Witcher 3: Wild Hunt', null, 3),
				(8, 'Final Fantasy XV', null, 3),
				(9, 'Elder Scrolls V: Skyrim', null, 3),
				-- First-Person Shooter Games
				(10, 'Battlefield 1', null, 4),
				(11, 'Overwatch', null, 4),
				(12, 'Counter-Strike: Global Offensive', null, 4),
				-- Survival Horror Games
				(13, 'Resident Evil 7', null, 5),
				(14, 'Alien Isolation', null, 5),
				(15, 'Dying Light: The Following', null, 5),
				-- Strategy Games
				(16, 'Civilization V', null, 6),
				(17, 'Starcraft II', null, 6),
				(18, 'XCOM 2', null, 6),
				-- Sports Games
				(19, 'Madden 17', null, 7),
				(20, 'NBA 2K17', null, 7),
				(21, 'Rocket League', null, 7),
				-- Racing Games
				(22, 'DiRT Rally', null, 8),
				(23, 'F1 2016', null, 8),
				(24, 'Need for Speed', null, 8),
				-- Stealth Games
				(25, 'Dishonored 2', null, 9),
				(26, 'Metal Gear Solid V: The Phantom Pain', null, 9),
				(27, 'Hitman', null, 9),
				-- Simulation Games
				(28, 'Arma III', null, 10),
				(29, 'The Sims 4', null, 10),
				(30, 'Planet Coaster', null, 10),
				-- MOBA Games
				(31, 'League of Legends', null, 11),
				(32, 'DOTA 2', null, 11),
				(33, 'Heroes of the Storm', null, 11),
				-- MMO Games
				(34, 'World of Warcraft', null, 12),
				(35, 'Elder Scrolls Online', null, 12),
				(36, 'Guild Wars 2', null, 12),
				-- Puzzle Games
				(37, 'Limbo', null, 13),
				(38, 'Portal 2', null, 13),
				(39, 'Scribblenauts Unlimited', null, 13),
				-- Tower Defense Games
				(40, 'Plants vs. Zombies', null, 14),
				(41, 'Kingdom Rush', null, 14),
				(42, 'Sanctum 2', null, 14),
				-- Hack and Slash Games
				(43, 'Diablo 3', null, 15),
				(44, 'Path of Exile', null, 15),
				(45, 'Grim Dawn', null, 15)
	SET IDENTITY_INSERT Games OFF;

COMMIT TRANSACTION
