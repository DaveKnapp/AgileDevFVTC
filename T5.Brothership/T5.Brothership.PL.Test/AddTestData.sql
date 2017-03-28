--Delete all data in tables
BEGIN TRANSACTION

	DELETE FROM UserRatings
	DELETE FROM UserSocialJuncs
	DELETE FROM GameCategoryJunc
	DELETE FROM UserIntegrations
	DELETE FROM UserGameJunc
	DELETE FROM Users
	DELETE FROM UserLogins
	DELETE FROM UserTypes
	DELETE FROM Nationalities
	DELETE FROM Games
	DELETE FROM GameCategories
	DELETE FROM IntegrationTypes
	DELETE FROM SocialMediaTypes
	DELETE FROM Ratings
	DELETE FROM Genders

COMMIT TRANSACTION

GO
--ADD Test Data
BEGIN TRANSACTION
	SET IDENTITY_INSERT Nationalities ON;

	INSERT INTO Nationalities (ID, "Description")
		VALUES(1, 'US and A'),
			  (2, 'Earth'),
			  (3, 'Canada')

	SET IDENTITY_INSERT Nationalities OFF;

	SET IDENTITY_INSERT Genders ON;
	INSERT INTO Genders (ID, "Description")
		VALUES(1, 'Male'),
			  (2, 'Female'),
			  (3, 'Human')
	SET IDENTITY_INSERT Genders OFF;

	SET IDENTITY_INSERT UserTypes ON;
	INSERT INTO UserTypes(ID, "Description")
		VALUES(1, 'User'),
			  (2, 'Featured User')
	SET IDENTITY_INSERT UserTypes OFF;

SET IDENTITY_INSERT UserLogins ON;
--Passwords are Password
	INSERT INTO UserLogins(UserID, "PasswordHash",Salt)
		VALUES(1,'5Efg7nxAjJdkjIsZECyAWGA10mMixUnUiatbAgfcX3g=','b9qo1clGZ0q/99JkBJevOJGjU6JGUhmy'),
			  (2,'qaNdZwpUFt18tcaJtAJBr4rTkwmy6uwvB1zlm4MLh7g= ', 'QBKzLfLzbtRIS19vkbguqgPakJ+BKQre'),
			  (3,'/HOXKid5g4YaNZNitnwyYnnoy7CecL6lxaDil4fjHmE=', 'xbiS4ItBbOzkl/9PfDJzvs8IYK6aiH6q'),
			  (4,'zAhNMBQ4/Ld4Qg19Sm3vukDyyu+rYnRAgIBw5t2wjTM=', 'yA2zLBkEQzjGT8wBUoNf6OcVt+J+/2gV'),
			  (5,'VCexSa7lVH7IvZ4qsABqRcnjWJLte24mPCaTK4DbHNY=', 'VuQePlIuVbuwkygTSwbHCjkTsqy5cLgB')
SET IDENTITY_INSERT UserLogins OFF;

	INSERT INTO Users (ID, UserName, Email, Bio, ProfileImagePath, DateJoined, DOB, GenderId, UserTypeID, NationalityID)
		VALUES(1, 'TestUserOne', 'Testing123@yahoo.com', 'This is my bio', '../Images/TestUserOne/profile.png','2-20-17', '11-12-1988', 1, 1,1),
			  (2, 'TestUserTwo', 'TestingUser2@yahoo.com', 'Hello I am the second test user', '../Images/TestUserTwo/Profile.png','2-22-17', '1-27-1980', 2, 2,1),
			  (3, 'TestUserThree', 'TestingUser3@yahoo.com', 'Hello I am the Third test user', '../Images/TestUserThree/Profile.png','1-5-17', '6-1-1990', 1, 2,1),
			  (4, 'TestUserFour', 'TestingUser4@yahoo.com', 'Hello I am the Fourth test user', '../Images/TestUserFour/Profile.png','1-13-17', '4-27-1991', 1, 1,1),
			  (5, 'TestUserFive', 'TestingUser5@yahoo.com', 'Hello I am the Fifth test user', '../Images/TestUserFive/Profile.png','1-19-17', '7-9-1962', 2, 1,1)

	SET IDENTITY_INSERT IntegrationTypes ON;
	INSERT INTO IntegrationTypes(ID,"Description")
		VALUES(1, 'Twitch')
	SET IDENTITY_INSERT IntegrationTypes OFF;

	INSERT INTO UserIntegrations(UserID, IntegrationTypeID, Token)
		VALUES(1,1,'lkjlk23jkl2332l32kj4'),
			  (3,1,'lkjlkjlk;jlkjlk3jlkjlkj')

	SET IDENTITY_INSERT Ratings ON;
		INSERT INTO Ratings(ID, "Description")
		VALUES(1, 'One Star'),
			  (2, 'Two Stars'),
			  (3, 'Three Stars'),
			  (4, 'Four Stars'),
			  (5, 'Five Stars')
	SET IDENTITY_INSERT Ratings OFF;

		INSERT INTO UserRatings(RaterUserID, UserBeingRatedID, Comment, RatingID)
			VALUES(1,2,'10/10 would watch again', 5),
				  (3,2,'Okay', 4),
				  (4,2,'Elbows too pointy', 1),
				  (2,1,'Alright', 3),
				  (3,4,'Fun times', 4),
				  (4,5,'5/5 would watch again', 5),
				  (5,3,'Best stream ever', 5),
				  (3,1,'boring', 2)


	SET IDENTITY_INSERT SocialMediaTypes ON;
	INSERT INTO SocialMediaTypes(ID, "Description")
		VALUES(1, 'Youtube'),
			  (2, 'Twitter'),
			  (3, 'Instagram')
	SET IDENTITY_INSERT SocialMediaTypes OFF;

	INSERT INTO UserSocialJuncs(UserID, SocialMediaTypeID, "URL")
		VALUES (1,1, 'youtube.com/channel/TestUserOne'),
			   (1,2, 'twitter.com/TestUserOne'),
			   (1,3, 'instagram.com/TestUserOne'),
			   (2,1, 'instagram.com/TestUserTwo'),
			   (2,2, 'instagram.com/TestUserTwo'),
			   (3,3, 'instagram.com/TestUserThree')

	SET IDENTITY_INSERT GameCategories ON;
	INSERT INTO GameCategories(ID, "Description")
	 VALUES (33,'Arcade'),
		    (32,'Indie'),
			(31,'Adventure'),
			(30,'Pinball'),
			(26,'Quiz/Trivia'),
			(25,'Hack and slash/Beat ''em up'),
			(24,'Tactical'),
			(16,'Turn-based strategy (TBS)'),
			(15,'Strategy'),
			(14,'Sport'),
			(13,'Simulator'),
			(12,'Role-playing (RPG)'),
			(11,'Real Time Strategy (RTS)'),
			(10,'Racing'),
			(9,'Puzzle'),
			(8,'Platform'),
			(7,'Music'),
			(5,'Shooter'),
			(4,'Fighting'),
			(2,'Point-and-click')
	SET IDENTITY_INSERT GameCategories OFF;

	SET IDENTITY_INSERT Games ON;
	INSERT INTO Games(ID, Title, igdbID)
		VALUES	-- Fighting Games
				(1, 'Street Fighter V', null),
				(2, 'Mortal Kombat X', null),
				(3, 'Injustice: Gods Among Us', null),
				-- Action/Adventure Games
				(4, 'Fallout 4', null),
				(5, 'Uncharted 4', null),
				(6, 'Grand Theft Auto V', null),
				-- RPG Games
				(7, 'The Witcher 3: Wild Hunt', null),
				(8, 'Final Fantasy XV', null),
				(9, 'Elder Scrolls V: Skyrim', null),
				-- First-Person Shooter Games
				(10, 'Battlefield 1', null),
				(11, 'Overwatch', null),
				(12, 'Counter-Strike: Global Offensive', null),
				-- Survival Horror Games
				(13, 'Resident Evil 7', null),
				(14, 'Alien Isolation', null),
				(15, 'Dying Light: The Following', null),
				-- Strategy Games
				(16, 'Civilization V', null),
				(17, 'Starcraft II', null),
				(18, 'XCOM 2', null),
				-- Sports Games
				(19, 'Madden 17', null),
				(20, 'NBA 2K17', null),
				(21, 'Rocket League', null),
				-- Racing Games
				(22, 'DiRT Rally', null),
				(23, 'F1 2016', null),
				(24, 'Need for Speed', null),
				-- Stealth Games
				(25, 'Dishonored 2', null),
				(26, 'Metal Gear Solid V: The Phantom Pain', null),
				(27, 'Hitman', null),
				-- Simulation Games
				(28, 'Arma III', null),
				(29, 'The Sims 4', null),
				(30, 'Planet Coaster', null),
				-- MOBA Games
				(31, 'League of Legends', 115),
				(32, 'DOTA 2', null),
				(33, 'Heroes of the Storm', null),
				-- MMO Games
				(34, 'World of Warcraft', null),
				(35, 'Elder Scrolls Online', null),
				(36, 'Guild Wars 2', null),
				-- Puzzle Games
				(37, 'Limbo', 1331),
				(38, 'Portal 2', null),
				(39, 'Scribblenauts Unlimited', null),
				-- Tower Defense Games
				(40, 'Plants vs. Zombies', 1277),
				(41, 'Kingdom Rush', null),
				(42, 'Sanctum 2', null),
				-- Hack and Slash Games
				(43, 'Diablo 3', null),
				(44, 'Path of Exile', null),
				(45, 'Grim Dawn', null)
	SET IDENTITY_INSERT Games OFF;

	INSERT INTO GameCategoryJunc(Games_ID, GameCategories_ID)
		VALUES (1,4),
			   (2,4),
			   (3,4),
			   (4,31),
			   (5,31),
			   (6,31),
			   (7,12),
			   (8,12),
			   (9,12),
			   (10,5),
			   (11,5),
			   (12,5),
			   (13,31),
			   (14,31),
			   (15,31),
			   (16,15),
			   (17,15),
			   (18,15),
			   (19,14),
			   (20,14),
			   (21,14),
			   (22,10),
			   (23,10),
			   (24,10),
			   (25,31),
			   (26,31),
			   (27,31),
			   (28,13),
			   (29,13),
			   (30,13),
			   (31,11),
			   (32,11),
			   (33,11),
			   (34,12),
			   (35,12),
			   (36,12),
			   (37,9),
			   (38,9),
			   (39,9),
			   (40,11),
			   (41,11),
			   (42,11),
			   (43,25),
			   (44,25),
			   (45,25)
	INSERT INTO UserGameJunc(Users_ID, Games_ID)
		VALUES(1,40),
			(1,37),
			(1,31)
COMMIT TRANSACTION
