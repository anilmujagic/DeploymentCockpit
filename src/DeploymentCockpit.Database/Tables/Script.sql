CREATE TABLE [dbo].[Script]
(
    [ScriptID] SMALLINT NOT NULL IDENTITY,
    [Name] NVARCHAR(100) NOT NULL,
    [Description] NVARCHAR(MAX) NULL,
    [ScriptType] NVARCHAR(50) NOT NULL,
    [Body] NVARCHAR(MAX) NULL,
    [SuccessKeywords] NVARCHAR(MAX) NULL,
    [FailureKeywords] NVARCHAR(MAX) NULL,
    [ProjectID] SMALLINT NULL,
    CONSTRAINT [PK_Script] PRIMARY KEY ([ScriptID]),
    CONSTRAINT [UK_Script_Name] UNIQUE ([Name]),
    CONSTRAINT [FK_Script_Project] FOREIGN KEY ([ProjectID]) REFERENCES [Project]([ProjectID])
)

GO

CREATE INDEX [IX_Script_ProjectID] ON [dbo].[Script] ([ProjectID])
