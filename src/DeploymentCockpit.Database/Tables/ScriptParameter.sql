CREATE TABLE [dbo].[ScriptParameter]
(
    [ScriptParameterID] INT NOT NULL IDENTITY, 
    [ScriptID] SMALLINT NOT NULL,
    [Name] NVARCHAR(100) NOT NULL, 
    [DefaultValue] NVARCHAR(MAX) NULL, 
    [IsMandatory] BIT NOT NULL,
    CONSTRAINT [PK_ScriptParameter] PRIMARY KEY ([ScriptParameterID]),
    CONSTRAINT [UK_ScriptParameter_ScriptID_Name] UNIQUE ([ScriptID], [Name]),
    CONSTRAINT [FK_ScriptParameter_Script] FOREIGN KEY ([ScriptID])
        REFERENCES [Script]([ScriptID]) ON DELETE CASCADE
)

GO

CREATE INDEX [IX_ScriptParameter_ScriptID] ON [dbo].[ScriptParameter] ([ScriptID])
