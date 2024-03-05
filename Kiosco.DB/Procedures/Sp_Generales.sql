CREATE PROCEDURE [Runtime].[CreateAccount]
(
    @BusinessUUID UNIQUEIDENTIFIER,
    @BusinessName NVARCHAR(50),
    @EmailBusiness NVARCHAR(50),
    @PhoneBusiness NVARCHAR(9),
    @Address NVARCHAR(50),
    @ImgUrl NVARCHAR(MAX),
    @RUC NVARCHAR(19),
    @DepartmentId INT,
    @BusinessTypeId INT,
    @OwnerUUID UNIQUEIDENTIFIER,
    @FullName NVARCHAR(50),
    @EmailOwner NVARCHAR(50),
    @PhoneOwner NVARCHAR(9),
    @Identification NVARCHAR(16),
    @PasswordHashed VARBINARY(32),
    @PasswordSalt VARBINARY(32),
    @RowCount INT OUTPUT
)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @OwnerId INT;

    INSERT INTO [UserManagement].[OwnersBusiness]
    (
        [OwnerUUID],
        [FullName],
        [Email],
        [Phone],
        [Identification],
        [PasswordHashed],
        [PasswordSalt]
    )
    VALUES
    (
        @OwnerUUID,
        @FullName,
        @EmailOwner,
        @PhoneOwner,
        @Identification,
        @PasswordHashed,
        @PasswordSalt
    );

	SET @OwnerId = SCOPE_IDENTITY();

    
    INSERT INTO [BusinessData].[Business]
    (    
        [BusinessUUID],
        [OwnerId],
        [OwnerUUID],
        [BusinessName],
        [Email],
        [Phone],
        [Address],
        [ImgUrl],
        [RUC],
        [DepartmentId],
        [BusinessTypeId]
    )
    VALUES
    (
        @BusinessUUID,
        @OwnerId,
		@OwnerUUID,
        @BusinessName,
        @EmailBusiness,
        @PhoneBusiness,
        @Address,
        @ImgUrl,
        @RUC,
        @DepartmentId,
        @BusinessTypeId
    );

	SET @RowCount = @@ROWCOUNT;
END;

GO

CREATE PROCEDURE [Runtime].[DeleteAccount]
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
