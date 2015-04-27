﻿CREATE TABLE USERS [dbo].[Table]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [NAME] NVARCHAR(150) NOT NULL, 
    [EMAIL] NVARCHAR(150) NOT NULL, 
    [PASSWORD] NVARCHAR(50) NOT NULL, 
    [BIRTHDAY] DATETIME NULL, 
    [COUNTRY] NVARCHAR(50) NULL, 
    [AVATAR] NVARCHAR(50) NULL
)