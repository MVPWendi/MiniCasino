USE [WUBank]
GO
/****** Object:  StoredProcedure [dbo].[CreateUser]    Script Date: 24.01.2023 20:23:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[CreateUser] @name NVARCHAR(MAX), @steamid NVARCHAR(22) AS

BEGIN
IF (NOT EXISTS(SELECT TOP(1) SteamID FROM Users WHERE SteamID = @steamid))
	BEGIN
		INSERT INTO Users (UserName, SteamID, Balance)
		VALUES (@name, @steamid, 0)
	END
END