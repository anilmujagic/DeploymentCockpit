CREATE TABLE [dbo].[DeploymentPlanStep]
(
    [DeploymentPlanStepID] INT NOT NULL IDENTITY, 
    [DeploymentPlanID] SMALLINT NOT NULL,
    [Name] NVARCHAR(100) NOT NULL, 
    [Description] NVARCHAR(MAX) NULL, 
    [ExecutionOrder] SMALLINT NOT NULL,
    [ScriptID] SMALLINT NOT NULL,
    [AllTargetGroups] BIT NOT NULL,
    [TargetGroupID] SMALLINT NULL,
    [RemoteExecution] BIT NOT NULL,
    CONSTRAINT [PK_DeploymentPlanStep] PRIMARY KEY ([DeploymentPlanStepID]),
    CONSTRAINT [UK_DeploymentPlanStep_DeploymentPlanID_Name] UNIQUE ([DeploymentPlanID], [Name]),
    CONSTRAINT [UK_DeploymentPlanStep_DeploymentPlanID_ExecutionOrder] UNIQUE ([DeploymentPlanID], [ExecutionOrder]),
    CONSTRAINT [FK_DeploymentPlanStep_DeploymentPlan] FOREIGN KEY ([DeploymentPlanID])
        REFERENCES [DeploymentPlan]([DeploymentPlanID]) ON DELETE CASCADE,
    CONSTRAINT [FK_DeploymentPlanStep_Script] FOREIGN KEY ([ScriptID])
        REFERENCES [Script]([ScriptID]),
    CONSTRAINT [FK_DeploymentPlanStep_TargetGroup] FOREIGN KEY ([TargetGroupID])
        REFERENCES [TargetGroup]([TargetGroupID])
)

GO

CREATE INDEX [IX_DeploymentPlanStep_DeploymentPlanID] ON [dbo].[DeploymentPlanStep] ([DeploymentPlanID])
GO
CREATE INDEX [IX_DeploymentPlanStep_ScriptID] ON [dbo].[DeploymentPlanStep] ([ScriptID])
