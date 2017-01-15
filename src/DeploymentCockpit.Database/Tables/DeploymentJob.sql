CREATE TABLE [dbo].[DeploymentJob]
(
    [DeploymentJobID] INT NOT NULL IDENTITY, 
    [ProjectID] SMALLINT NOT NULL, 
    [SubmissionTime] DATETIME2 NOT NULL, 
    [SubmittedBy] NVARCHAR(100) NULL, 
    [StartTime] DATETIME2 NULL, 
    [EndTime] DATETIME2 NULL, 
    [StatusKey] NVARCHAR(50) NOT NULL, 
    [ProductVersion] NVARCHAR(100) NOT NULL, 
    [ProjectEnvironmentID] SMALLINT NOT NULL, 
    [DeploymentPlanID] SMALLINT NOT NULL, 
    [Notes] NVARCHAR(MAX) NULL, 
    [Errors] NVARCHAR(MAX) NULL, 
    CONSTRAINT [PK_DeploymentJob] PRIMARY KEY ([DeploymentJobID]), 
    CONSTRAINT [FK_DeploymentJob_Project] FOREIGN KEY ([ProjectID])
        REFERENCES [Project]([ProjectID]) ON DELETE CASCADE, 
    CONSTRAINT [FK_DeploymentJob_ProjectEnvironment] FOREIGN KEY ([ProjectEnvironmentID])
        REFERENCES [ProjectEnvironment]([ProjectEnvironmentID]), 
    CONSTRAINT [FK_DeploymentJob_DeploymentPlan] FOREIGN KEY ([DeploymentPlanID])
        REFERENCES [DeploymentPlan]([DeploymentPlanID]) 
)
GO

CREATE INDEX [IX_DeploymentJob_ProjectID] ON [dbo].[DeploymentJob] ([ProjectID])
GO

CREATE INDEX [IX_DeploymentJob_ProjectEnvironmentID] ON [dbo].[DeploymentJob] ([ProjectEnvironmentID])
GO

CREATE INDEX [IX_DeploymentJob_DeploymentPlanID] ON [dbo].[DeploymentJob] ([DeploymentPlanID])
