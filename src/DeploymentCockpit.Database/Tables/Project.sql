CREATE TABLE [dbo].[Project]
(
    [ProjectID] SMALLINT NOT NULL IDENTITY, 
    [Name] NVARCHAR(100) NOT NULL, 
    [Description] NVARCHAR(MAX) NULL, 
    [ApiCode] NVARCHAR(100) NULL, 

    CONSTRAINT [PK_Project] PRIMARY KEY ([ProjectID]), 
    CONSTRAINT [UK_Project_Name] UNIQUE ([Name]), 
    CONSTRAINT [UK_Project_ApiCode] UNIQUE ([ApiCode]) 
)
