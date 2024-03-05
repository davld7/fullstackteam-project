CREATE TABLE [UserManagement].[Roles]
(
	[RoleId] INT NOT NULL PRIMARY KEY IDENTITY,
	[RoleName] NVARCHAR(50) NOT NULL,
	[Description] NVARCHAR(100),
)
