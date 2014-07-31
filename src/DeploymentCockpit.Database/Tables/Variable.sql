CREATE TABLE [dbo].[Variable]
(
    [VariableID] INT NOT NULL IDENTITY, 
    [ScopeKey] NVARCHAR(50) NOT NULL,
    [ScopeID] INT NOT NULL,
    [Name] NVARCHAR(100) NOT NULL, 
    [Value] NVARCHAR(MAX) NULL, 
    CONSTRAINT [PK_Variable] PRIMARY KEY ([VariableID]),
    CONSTRAINT [UK_Variable_ScopeKey_ScopeID_Name] UNIQUE ([ScopeKey], [ScopeID], [Name])
)

GO

CREATE INDEX [IX_Variable_ScopeKey_ScopeID] ON [dbo].[Variable] ([ScopeKey], [ScopeID])