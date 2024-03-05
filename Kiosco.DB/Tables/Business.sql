CREATE TABLE [BusinessData].[Business]
(
	[BusinessId] INT NOT NULL PRIMARY KEY IDENTITY,
	[BusinessUUID] UNIQUEIDENTIFIER NOT NULL,
	[OwnerId] INT NOT NULL,
	[OwnerUUID] UNIQUEIDENTIFIER NOT NULL,
	[BusinessName] NVARCHAR(50) NOT NULL,
	[Email] NVARCHAR(50) NOT NULL,
	[Phone] NVARCHAR(9) NOT NULL,
	[Address] NVARCHAR(50) NOT NULL,
	[ImgUrl] NVARCHAR(MAX) NOT NULL,
	[RUC] NVARCHAR(19),
	[DepartmentId] INT NOT NULL,
	[BusinessTypeId] INT NOT NULL,
	[IsActive] BIT NOT NULL DEFAULT 0,
	[CreatedDate] DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
	[ModifiedDate] DATETIME,
	[DeletedDate] DATETIME,


	CONSTRAINT [UQ_Business_Email] UNIQUE ([Email]),
	CONSTRAINT [UQ_Business_Phone] UNIQUE ([Phone]),
	CONSTRAINT [UQ_Business_RUC] UNIQUE ([RUC]), 


)

GO
CREATE NONCLUSTERED INDEX [IX_Business_Email] ON [BusinessData].[Business] ([Email]);
GO
CREATE NONCLUSTERED INDEX [IX_Business_Phone] ON [BusinessData].[Business] ([Phone]);
GO
CREATE NONCLUSTERED INDEX [IX_Business_Name] ON [BusinessData].[Business] ([BusinessName]);
GO
CREATE NONCLUSTERED INDEX [IX_Business_RUC] ON [BusinessData].[Business] ([RUC]);
GO
CREATE NONCLUSTERED INDEX [IX_Business_UIID] ON [BusinessData].[Business] ([BusinessUUID]);
