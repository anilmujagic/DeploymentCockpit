CREATE TABLE [dbo].[ProjectTarget]
(
    [ProjectTargetID] INT NOT NULL IDENTITY, 
    [TargetGroupID] SMALLINT NOT NULL,
    [ProjectEnvironmentID] SMALLINT NOT NULL,
    [TargetID] SMALLINT NOT NULL, 
    CONSTRAINT [PK_ProjectTarget] PRIMARY KEY ([ProjectTargetID]),
    CONSTRAINT [UK_ProjectTarget_TargetGroupID_ProjectEnvironmentID_TargetID]
        UNIQUE ([TargetGroupID], [ProjectEnvironmentID], [TargetID]),
    CONSTRAINT [FK_ProjectTarget_TargetGroup] FOREIGN KEY ([TargetGroupID])
        REFERENCES [TargetGroup]([TargetGroupID]) ON DELETE CASCADE,
    CONSTRAINT [FK_ProjectTarget_ProjectEnvironment] FOREIGN KEY ([ProjectEnvironmentID])
        REFERENCES [ProjectEnvironment]([ProjectEnvironmentID]),  -- ON DELETE CASCADE
                                                                  -- Should be set, but SQL server says:
                                                                  -- "may cause cycles or multiple cascade paths".
    CONSTRAINT [FK_ProjectTarget_Target] FOREIGN KEY ([TargetID])
        REFERENCES [Target]([TargetID])
)

GO
