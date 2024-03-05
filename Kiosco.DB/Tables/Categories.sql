CREATE TABLE [BusinessData].[Categories]
(
	[CategoryId] INT NOT NULL PRIMARY KEY IDENTITY,
	[CategoryUUID] UNIQUEIDENTIFIER NOT NULL,
	[BusinessId] INT NOT NULL,
	[CategoryName] NVARCHAR(50) NOT NULL,
	[IsActive] BIT NOT NULL DEFAULT 1,
	[CreatedDate] DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
	[ModifiedDate] DATETIME,
	[DeletedDate] DATETIME,
)
GO
CREATE NONCLUSTERED INDEX [IX_Categories_BusinessId] ON [BusinessData].[Categories] ([BusinessId]);
GO
CREATE NONCLUSTERED INDEX [IX_Categories_CategoryUUID] ON [BusinessData].[Categories] ([CategoryUUID]);

