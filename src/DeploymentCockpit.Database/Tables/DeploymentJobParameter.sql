CREATE TABLE [dbo].[DeploymentJobParameter]
(
    [DeploymentJobParameterID] INT NOT NULL IDENTITY, 
    [ProjectID] SMALLINT NOT NULL,
    [Name] NVARCHAR(100) NOT NULL, 
    CONSTRAINT [PK_DeploymentJobParameter] PRIMARY KEY ([DeploymentJobParameterID]),
    CONSTRAINT [UK_DeploymentJobParameter_ProjectID_Name] UNIQUE ([ProjectID], [Name]),
    CONSTRAINT [FK_DeploymentJobParameter_Project] FOREIGN KEY ([ProjectID])
        REFERENCES [Project]([ProjectID]) ON DELETE CASCADE
)

GO

CREATE INDEX [IX_DeploymentJobParameter_ProjectID] ON [dbo].[DeploymentJobParameter] ([ProjectID])
