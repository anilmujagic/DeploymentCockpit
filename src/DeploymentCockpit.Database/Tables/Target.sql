CREATE TABLE [dbo].[Target]
(
    [TargetID] SMALLINT NOT NULL IDENTITY, 
    [Name] NVARCHAR(100) NOT NULL, 
    [Description] NVARCHAR(MAX) NULL, 
    [ComputerName] NVARCHAR(200) NOT NULL, 
    [PortNumber] INT NOT NULL,
    [TargetKey] NVARCHAR(200) NOT NULL, 
    [CredentialID] SMALLINT NOT NULL, 
    CONSTRAINT [PK_Target] PRIMARY KEY ([TargetID]), 
    CONSTRAINT [UK_Target_Name] UNIQUE ([Name]),
    CONSTRAINT [FK_Target_Credential] FOREIGN KEY ([CredentialID]) REFERENCES [Credential]([CredentialID])
)

GO

CREATE INDEX [IX_Target_CredentialID] ON [dbo].[Target] ([CredentialID])
