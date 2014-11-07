CREATE TABLE [dbo].[TargetGroupEnvironment]
(
    [TargetGroupEnvironmentID] INT NOT NULL, 
    [TargetGroupID] SMALLINT NOT NULL, 
    [ProjectEnvironmentID] SMALLINT NOT NULL, 
    CONSTRAINT [PK_TargetGroupEnvironment] PRIMARY KEY ([TargetGroupEnvironmentID]), 
    CONSTRAINT [UK_TargetGroupEnvironment_TargetGroupID_ProjectEnvironmentID]
        UNIQUE ([TargetGroupID], [ProjectEnvironmentID]), 
    CONSTRAINT [FK_TargetGroupEnvironment_TargetGroup] FOREIGN KEY ([TargetGroupID])
        REFERENCES [TargetGroup]([TargetGroupID]) ON DELETE CASCADE, 
    CONSTRAINT [FK_TargetGroupEnvironment_ProjectEnvironment] FOREIGN KEY ([ProjectEnvironmentID])
        REFERENCES [ProjectEnvironment]([ProjectEnvironmentID]) 
)

GO

CREATE INDEX [IX_TargetGroupEnvironment_TargetGroupID] ON [dbo].[TargetGroupEnvironment] ([TargetGroupID])
GO

CREATE INDEX [IX_TargetGroupEnvironment_ProjectEnvironmentID] ON [dbo].[TargetGroupEnvironment] ([ProjectEnvironmentID])
GO