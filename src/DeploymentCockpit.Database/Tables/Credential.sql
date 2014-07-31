CREATE TABLE [dbo].[Credential]
(
    [CredentialID] SMALLINT NOT NULL IDENTITY, 
    [Name] NVARCHAR(100) NOT NULL, 
    [Username] NVARCHAR(100) NOT NULL, 
    [Password] NVARCHAR(1000) NULL, 
    [Domain] NVARCHAR(100) NULL, 
    CONSTRAINT [PK_Credential] PRIMARY KEY ([CredentialID]), 
    CONSTRAINT [UK_Credential_Name] UNIQUE ([Name]) 
)
