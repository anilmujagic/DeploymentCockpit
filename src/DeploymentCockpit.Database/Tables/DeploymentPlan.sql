CREATE TABLE [dbo].[DeploymentPlan]
(
    [DeploymentPlanID] SMALLINT NOT NULL IDENTITY, 
    [ProjectID] SMALLINT NOT NULL,
    [Name] NVARCHAR(100) NOT NULL, 
    [Description] NVARCHAR(MAX) NULL, 
    CONSTRAINT [PK_DeploymentPlan] PRIMARY KEY ([DeploymentPlanID]),
    CONSTRAINT [UK_DeploymentPlan_ProjectID_Name] UNIQUE ([ProjectID], [Name]),
    CONSTRAINT [FK_DeploymentPlan_Project] FOREIGN KEY ([ProjectID])
        REFERENCES [Project]([ProjectID]) ON DELETE CASCADE
)

GO

CREATE INDEX [IX_DeploymentPlan_ProjectID] ON [dbo].[DeploymentPlan] ([ProjectID])
