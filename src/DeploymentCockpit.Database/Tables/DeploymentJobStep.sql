CREATE TABLE [dbo].[DeploymentJobStep]
(
    [DeploymentJobStepID] INT NOT NULL IDENTITY, 
    [DeploymentJobID] INT NOT NULL, 
    [DeploymentPlanStepID] INT NOT NULL, 
    [StatusKey] NVARCHAR(50) NOT NULL, 
    [StartTime] DATETIME2 NULL, 
    [EndTime] DATETIME2 NULL, 
    [ExecutedScript] NVARCHAR(MAX) NULL,
    [ExecutionOutput] NVARCHAR(MAX) NULL,
    [ExecutionReference] UNIQUEIDENTIFIER NOT NULL, 
    CONSTRAINT [PK_DeploymentJobStep] PRIMARY KEY ([DeploymentJobStepID]), 
    CONSTRAINT [UK_DeploymentJobStep_ExecutionReference] UNIQUE ([ExecutionReference]),
    CONSTRAINT [FK_DeploymentJobStep_DeploymentJob] FOREIGN KEY ([DeploymentJobID])
        REFERENCES [DeploymentJob]([DeploymentJobID]) ON DELETE CASCADE,
    CONSTRAINT [FK_DeploymentJobStep_DeploymentPlanStep] FOREIGN KEY ([DeploymentPlanStepID])
        REFERENCES [DeploymentPlanStep]([DeploymentPlanStepID])
)
