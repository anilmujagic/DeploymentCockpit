CREATE TABLE [dbo].[User]
(
    [UserID] SMALLINT NOT NULL IDENTITY, 
    [Name] NVARCHAR(100) NOT NULL, 
    [Username] NVARCHAR(100) NOT NULL, 
    CONSTRAINT [PK_User] PRIMARY KEY ([UserID]), 
    CONSTRAINT [UK_User_Name] UNIQUE ([Name]),
    CONSTRAINT [UK_User_Username] UNIQUE ([Username])
)
