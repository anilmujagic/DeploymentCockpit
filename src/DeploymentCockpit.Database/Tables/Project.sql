CREATE TABLE [dbo].[Project]
(
    [ProjectID] SMALLINT NOT NULL IDENTITY, 
    [Name] NVARCHAR(100) NOT NULL, 
    [Description] NVARCHAR(MAX) NULL, 
    [ApiCode] NVARCHAR(100) NULL, 

    CONSTRAINT [PK_Project] PRIMARY KEY ([ProjectID]), 
    CONSTRAINT [UK_Project_Name] UNIQUE ([Name])
)
GO

CREATE UNIQUE NONCLUSTERED INDEX [UK_Project_ApiCode]
ON [dbo].[Project] ([ApiCode])
WHERE [ApiCode] IS NOT NULL
GO
