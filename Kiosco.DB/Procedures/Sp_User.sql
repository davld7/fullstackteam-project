CREATE PROCEDURE [Runtime].[InsertUser]
    @UserUUID UNIQUEIDENTIFIER,
    @BusinessUUID UNIQUEIDENTIFIER,
    @FullName NVARCHAR(50),
    @Email NVARCHAR(50),
    @PasswordHashed VARBINARY(32),
    @PasswordSalt VARBINARY(32),
    @Identification NVARCHAR(16),
    @RoleId INT,
	@RowCount INT OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
    DECLARE @BusinessId INT

    SET @BusinessId = (
    SELECT TOP 1 [BusinessId]
    FROM [BusinessData].[Business]
    WHERE [BusinessUUID] = @BusinessUUID)

    INSERT INTO [UserManagement].[Users] ([UserUUID], [BusinessId], [FullName], [Email], [PasswordHashed], [PasswordSalt], [Identification], [RoleId])
    VALUES (@UserUUID, @BusinessId, @FullName, @Email, @PasswordHashed, @PasswordSalt, @Identification, @RoleId)

    SET @RowCount = @@ROWCOUNT;  
END;

GO

CREATE PROCEDURE [Runtime].[UpdateUser]
	@UserUUID UNIQUEIDENTIFIER,
	@FullName NVARCHAR(50),
	@Email NVARCHAR(50),
	@PasswordHashed VARBINARY(32),
	@PasswordSalt VARBINARY(32),
	@Identification NVARCHAR(16),
	@RoleId INT,
	@RowCount INT OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE [UserManagement].[Users]
	SET [FullName] = @FullName,
		[Email] = @Email,
		[PasswordHashed] = @PasswordHashed,
		[PasswordSalt] = @PasswordSalt,
		[Identification] = @Identification,
		[RoleId] = @RoleId,
		[ModifiedDate] = CURRENT_TIMESTAMP
	WHERE [UserUUID] = @UserUUID;

	SET @RowCount = @@ROWCOUNT;
END;

GO

CREATE PROCEDURE [Runtime].[DeleteUser]
	@UserUUID UNIQUEIDENTIFIER,
	@RowCount INT OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE [UserManagement].[Users]
	SET [IsActive] = 0, [DeletedDate] = CURRENT_TIMESTAMP
	WHERE [UserUUID] = @UserUUID;

	SET @RowCount = @@ROWCOUNT;
END;

GO

CREATE PROCEDURE [Runtime].[GetAllUsers]
	@BusinessUUID UNIQUEIDENTIFIER
AS
BEGIN
	SELECT U.[UserUUID] AS ID, U.[FullName], U.[Email], U.[Identification], U.[RoleId], R.[RoleName], CASE WHEN U.[IsActive] = 1 THEN 'Activo' ELSE 'Inactivo' END AS Status
	FROM [UserManagement].[Users] AS U
	INNER JOIN [UserManagement].[Roles] AS R ON U.RoleId = R.RoleId
	INNER JOIN [BusinessData].[Business] AS B ON U.BusinessId = B.BusinessId
	WHERE B.BusinessUUID = @BusinessUUID AND U.[IsActive] = 1;
END;
