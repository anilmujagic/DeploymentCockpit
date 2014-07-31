CREATE TABLE [dbo].[TargetGroup]
(
    [TargetGroupID] SMALLINT NOT NULL IDENTITY, 
    [ProjectID] SMALLINT NOT NULL,
    [Name] NVARCHAR(100) NOT NULL, 
    [Description] NVARCHAR(MAX) NULL, 
    CONSTRAINT [PK_TargetGroup] PRIMARY KEY ([TargetGroupID]),
    CONSTRAINT [UK_TargetGroup_ProjectID_Name] UNIQUE ([ProjectID], [Name]),
    CONSTRAINT [FK_TargetGroup_Project] FOREIGN KEY ([ProjectID])
        REFERENCES [Project]([ProjectID]) ON DELETE CASCADE
)

GO

CREATE INDEX [IX_TargetGroup_ProjectID] ON [dbo].[TargetGroup] ([ProjectID])
