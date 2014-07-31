CREATE TABLE [dbo].[DeploymentJobStepTarget]
(
    [DeploymentJobStepTargetID] INT NOT NULL IDENTITY, 
    [DeploymentJobStepID] INT NOT NULL, 
    [TargetID] SMALLINT NOT NULL, 
    [StatusKey] NVARCHAR(50) NOT NULL, 
    [StartTime] DATETIME2 NULL, 
    [EndTime] DATETIME2 NULL, 
    [ExecutedScript] NVARCHAR(MAX) NULL,
    [ExecutionOutput] NVARCHAR(MAX) NULL,
    [ExecutionReference] UNIQUEIDENTIFIER NOT NULL, 
    CONSTRAINT [PK_DeploymentJobStepTarget] PRIMARY KEY ([DeploymentJobStepTargetID]), 
    CONSTRAINT [UK_DeploymentJobStepTarget_ExecutionReference] UNIQUE ([ExecutionReference]),
    CONSTRAINT [FK_DeploymentJobStepTarget_DeploymentJobStep] FOREIGN KEY ([DeploymentJobStepID])
        REFERENCES [DeploymentJobStep]([DeploymentJobStepID]) ON DELETE CASCADE,
    CONSTRAINT [FK_DeploymentJobStepTarget_Target] FOREIGN KEY ([TargetID])
        REFERENCES [Target]([TargetID])
)
