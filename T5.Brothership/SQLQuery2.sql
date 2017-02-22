--Delete all data in tables
BEGIN TRANSACTION 
	DELETE FROM "User"
	DELETE FROM UserSocialJunc
	DELETE FROM SocialMediaType
	DELETE FROM UserType
	DELETE FROM Nationality
	DELETE FROM UserIntigration
	DELETE FROM IntigrationType
	DELETE FROM UserRating
	DELETE FROM UserGameJunc
	DELETE FROM Game
	DELETE FROM GameCategory
	DELETE FROM UserLogin
COMMIT TRANSACTION

--ADD Test Data

SET IDENTITY_INSERT OFF