CREATE TABLE [UserManagement].[OwnersBusiness]
(
	[OwnerId] INT NOT NULL PRIMARY KEY IDENTITY,
	[OwnerUUID] UNIQUEIDENTIFIER NOT NULL,
	[FullName] NVARCHAR(50) NOT NULL,
	[Email] NVARCHAR(50) NOT NULL,
	[EmailConfirmed] BIT NOT NULL DEFAULT 0,  
	[Phone] NVARCHAR(9) NOT NULL,
	[Identification] NVARCHAR(16) NOT NULL,
	[PasswordHashed] VARBINARY(32) NOT NULL,
	[PasswordSalt] VARBINARY(32) NOT NULL,
	[RoleId] INT NOT NULL DEFAULT 1,
	[IsActive] BIT NOT NULL DEFAULT 0,
	[CreatedDate] DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
	[ModifiedDate] DATETIME,
	[DeletedDate] DATETIME,
	[LastLoginDate] DATETIME,
	[LastPasswordChangedDate] DATETIME,
	[LastActivityDate] DATETIME,

	CONSTRAINT [UQ_Owners_Email] UNIQUE ([Email]),
	CONSTRAINT [UQ_Owners_Identification] UNIQUE ([Identification]), 
)
GO
CREATE NONCLUSTERED INDEX [IX_Owners_Identification] ON [UserManagement].[OwnersBusiness] ([Identification]);
GO
CREATE NONCLUSTERED INDEX [IX_Owners_Email] ON [UserManagement].[OwnersBusiness] ([Email]);
GO
CREATE NONCLUSTERED INDEX [IX_Owners_UUID] ON [UserManagement].[OwnersBusiness] ([OwnerUUID]);
