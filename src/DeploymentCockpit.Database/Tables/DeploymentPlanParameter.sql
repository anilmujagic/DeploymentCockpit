CREATE TABLE [dbo].[DeploymentPlanParameter]
(
    [DeploymentPlanParameterID] INT NOT NULL IDENTITY, 
    [DeploymentPlanID] SMALLINT NOT NULL,
    [Name] NVARCHAR(100) NOT NULL, 
    CONSTRAINT [PK_DeploymentPlanParameter] PRIMARY KEY ([DeploymentPlanParameterID]),
    CONSTRAINT [UK_DeploymentPlanParameter_DeploymentPlanID_Name] UNIQUE ([DeploymentPlanID], [Name]),
    CONSTRAINT [FK_DeploymentPlanParameter_DeploymentPlan] FOREIGN KEY ([DeploymentPlanID])
        REFERENCES [DeploymentPlan]([DeploymentPlanID]) ON DELETE CASCADE
)

GO

CREATE INDEX [IX_DeploymentPlanParameter_DeploymentPlanID] ON [dbo].[DeploymentPlanParameter] ([DeploymentPlanID])
