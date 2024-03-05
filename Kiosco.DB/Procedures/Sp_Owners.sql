CREATE PROCEDURE [UserManagement].[InsertOwnerBusiness]
(
    @OwnerUUID UNIQUEIDENTIFIER,
    @FullName NVARCHAR(50),
    @Email NVARCHAR(50),
    @Phone NVARCHAR(9),
    @Identification NVARCHAR(16),
    @PasswordHashed VARBINARY(32),
    @PasswordSalt VARBINARY(32),
    @OwnerId INT OUTPUT
)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO [UserManagement].[OwnersBusiness]
    (
        [OwnerUUID],
        [FullName],
        [Email],
        [Phone],
        [Identification],
        [PasswordHashed],
        [PasswordSalt],
        [RoleId],
        [IsActive],
        [CreatedDate]
    )
    VALUES
    (
        @OwnerUUID,
        @FullName,
        @Email,
        @Phone,
        @Identification,
        @PasswordHashed,
        @PasswordSalt
    );

	SET @OwnerId = SCOPE_IDENTITY();
END;


GO


CREATE PROCEDURE [UserManagement].[DeactivateOwner]
(
    @OwnerUUID UNIQUEIDENTIFIER,
    @RowCount INT OUTPUT
)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [UserManagement].[OwnersBusiness]
    SET [IsActive] = 0, 
    [EmailConfirmed] = 0, 
    [DeletedDate] = CURRENT_TIMESTAMP
    WHERE [OwnerUUID] = @OwnerUUID;

	UPDATE [BusinessData].[Business]
	SET [IsActive] = 0,
	    [DeletedDate] = CURRENT_TIMESTAMP
    WHERE [OwnerUUID] = @OwnerUUID;

	SET @RowCount = @@ROWCOUNT;

END;


