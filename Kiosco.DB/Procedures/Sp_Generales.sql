CREATE PROCEDURE [Runtime].[CreateAccount]
(
    @BusinessId UNIQUEIDENTIFIER,
    @BusinessName NVARCHAR(50),
    @EmailBusiness NVARCHAR(50),
    @PhoneBusiness NVARCHAR(9),
    @Address NVARCHAR(50),
    @ImgUrl NVARCHAR(MAX),
    @RUC NVARCHAR(19),
    @DepartmentId UNIQUEIDENTIFIER,
    @BusinessTypeId UNIQUEIDENTIFIER,
    @OwnerId UNIQUEIDENTIFIER,
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

    DECLARE @OwnerIdentifier INT;
    DECLARE @BusinessIdentifier INT;

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
        @OwnerId,
        @FullName,
        @EmailOwner,
        @PhoneOwner,
        @Identification,
        @PasswordHashed,
        @PasswordSalt
    );

	SET @OwnerIdentifier = SCOPE_IDENTITY();

    
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
        @BusinessId,
        @OwnerIdentifier,
		@OwnerId,
        @BusinessName,
        @EmailBusiness,
        @PhoneBusiness,
        @Address,
        @ImgUrl,
        @RUC,
        @DepartmentId,
        @BusinessTypeId
    );

    SET @BusinessIdentifier = SCOPE_IDENTITY();

    INSERT INTO [BusinessData].[BusinessConfigurations] (BusinessId) VALUES (@BusinessIdentifier);

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

GO

CREATE PROCEDURE [Runtime].[GetUserInfo]
(
    @Email  NVARCHAR(50)
)
AS
BEGIN
    SELECT 
        CASE 
            WHEN OB.Email = @Email THEN OB.OwnerId
            ELSE U.UserId 
        END AS UserId,
        CASE 
            WHEN OB.Email = @Email THEN OB.PasswordHashed 
            ELSE U.PasswordHashed 
        END AS PasswordHashed,
        CASE 
            WHEN OB.Email = @Email THEN OB.PasswordSalt 
            ELSE U.PasswordSalt 
        END AS PasswordSalt
    FROM 
        [UserManagement].[OwnersBusiness] OB
    INNER JOIN 
        [BusinessData].[Business] B ON OB.OwnerId = B.OwnerId
    LEFT JOIN 
        [UserManagement].[Users] U ON B.BusinessId = U.BusinessId
    WHERE 
        (OB.[Email] = @Email OR U.[Email] = @Email)
        AND OB.[EmailConfirmed] = 1 
        AND OB.[IsActive] = 1 
        AND U.[IsActive] = 1;
END
