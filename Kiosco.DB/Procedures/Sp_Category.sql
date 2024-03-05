CREATE PROCEDURE [Runtime].[InsertCategory]
    @BusinessUUID UNIQUEIDENTIFIER,
    @CategoryName NVARCHAR(50),
    @RowCount INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @BusinessId INT;

    SET @BusinessId = (SELECT TOP 1[BusinessId] FROM [BusinessData].[Business] WHERE [BusinessUUID] = @BusinessUUID);

    INSERT INTO [BusinessData].[Categories] (
        [BusinessId],
        [CategoryName]
    )
    VALUES (
        @BusinessId,
        @CategoryName
    )

    SET @RowCount = @@ROWCOUNT
END;

GO

CREATE PROCEDURE [Runtime].[UpdateCategory]
    @CategoryUUID UNIQUEIDENTIFIER,
    @CategoryName NVARCHAR(50),
    @RowCount INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [BusinessData].[Categories]
    SET
        [CategoryName] = @CategoryName,
        [ModifiedDate] = CURRENT_TIMESTAMP
    WHERE
        [CategoryUUID] = @CategoryUUID AND [IsActive] = 1

    SET @RowCount = @@ROWCOUNT
END;

GO

CREATE PROCEDURE [Runtime].[DeleteCategory]
    @CategoryUUID UNIQUEIDENTIFIER,
    @RowCount INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [BusinessData].[Categories]
	SET [IsActive] = 0, [DeletedDate] = CURRENT_TIMESTAMP
    WHERE
        [CategoryUUID] = @CategoryUUID AND [IsActive] = 1

    SET @RowCount = @@ROWCOUNT
END;

GO

CREATE PROCEDURE [Runtime].[GetAllCategories]
	@BusinessUUID UNIQUEIDENTIFIER
AS
BEGIN
	SET NOCOUNT ON;

    SELECT C.[CategoryUUID],
		   C.[CategoryName]
    FROM [BusinessData].[Categories] AS C
    INNER JOIN [BusinessData].[Business] AS B ON C.[BusinessId] = B.[BusinessId]
	WHERE B.[BusinessUUID] = @BusinessUUID AND C.[IsActive] = 1

END;


