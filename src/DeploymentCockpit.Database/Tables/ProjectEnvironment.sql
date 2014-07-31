CREATE TABLE [dbo].[ProjectEnvironment]
(
    [ProjectEnvironmentID] SMALLINT NOT NULL IDENTITY, 
    [ProjectID] SMALLINT NOT NULL,
    [Name] NVARCHAR(100) NOT NULL, 
    [Description] NVARCHAR(MAX) NULL, 
    CONSTRAINT [PK_ProjectEnvironment] PRIMARY KEY ([ProjectEnvironmentID]),
    CONSTRAINT [UK_ProjectEnvironment_ProjectID_Name] UNIQUE ([ProjectID], [Name]),
    CONSTRAINT [FK_ProjectEnvironment_Project] FOREIGN KEY ([ProjectID])
        REFERENCES [Project]([ProjectID]) ON DELETE CASCADE
)

GO

CREATE INDEX [IX_ProjectEnvironment_ProjectID] ON [dbo].[ProjectEnvironment] ([ProjectID])
