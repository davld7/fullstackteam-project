CREATE TABLE [UserManagement].[Users]
(
	[UserId] INT NOT NULL PRIMARY KEY IDENTITY,
	[UserUUID] UNIQUEIDENTIFIER NOT NULL,
	[BusinessId] INT NOT NULL,
	[BusinessUUID] UNIQUEIDENTIFIER NOT NULL,
	[FullName] NVARCHAR(50) NOT NULL,
	[Email] NVARCHAR(50) NOT NULL,
	[PasswordHashed] VARBINARY(32) NOT NULL,
	[PasswordSalt] VARBINARY(32) NOT NULL,
	[Identification] NVARCHAR(16) NOT NULL,
	[RoleId] INT NOT NULL,
	[IsActive] BIT NOT NULL DEFAULT 1,
	[CreatedDate] DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
	[ModifiedDate] DATETIME,
	[DeletedDate] DATETIME,

	CONSTRAINT [UQ_Users_Email] UNIQUE ([Email]),
	CONSTRAINT [UQ_Users_Identification] UNIQUE ([Identification]),
)
GO
CREATE NONCLUSTERED INDEX [IX_Users_Identification] ON [UserManagement].[Users] ([Identification]);
GO
CREATE NONCLUSTERED INDEX [IX_Users_UserUUID] ON [UserManagement].[Users] ([UserUUID]);
GO
CREATE NONCLUSTERED INDEX [IX_Users_Email] ON [UserManagement].[Users] ([Email]);
