CREATE PROCEDURE [Runtime].[InsertProduct]
    @ProductUUID UNIQUEIDENTIFIER,
    @BusinessUUID UNIQUEIDENTIFIER,
    @ProductName NVARCHAR(50),
    @Price DECIMAL(18,2),
    @Stock INT,
    @Discount DECIMAL(5,2),
    @CategoryId INT,
    @ImgUrl NVARCHAR(MAX),
    @Description NVARCHAR(MAX),
	@RowCount INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @BusinessId INT

    SET @BusinessId = (
    SELECT TOP 1 [BusinessId]
    FROM [BusinessData].[Business]
    WHERE [BusinessUUID] = @BusinessUUID)

    INSERT INTO [BusinessData].[Products] (
        [ProductUUID],
        [BusinessId],
        [ProductName],
        [Price],
        [Stock],
        [Discount],
        [CategoryId],
        [ImgUrl],
        [Description]
    )
    VALUES (
        @ProductUUID,
        @BusinessId,
        @ProductName,
        @Price,
        @Stock,
        @Discount,
        @CategoryId,
        @ImgUrl,
        @Description
    )

    SET @RowCount = @@ROWCOUNT
END

GO

CREATE PROCEDURE [Runtime].[UpdateProduct]
    @ProductUUID UNIQUEIDENTIFIER,
    @ProductName NVARCHAR(50),
    @Price DECIMAL(18,2),
    @Stock INT,
    @Discount DECIMAL(5,2),
    @CategoryId INT,
    @ImgUrl NVARCHAR(MAX),
    @Description NVARCHAR(MAX),
    @RowCount INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [BusinessData].[Products]
    SET
        [ProductName] = @ProductName,
        [Price] = @Price,
        [Stock] = @Stock,
        [Discount] = @Discount,
        [CategoryId] = @CategoryId,
        [ImgUrl] = @ImgUrl,
        [Description] = @Description,
        [ModifiedDate] = CURRENT_TIMESTAMP
    WHERE
        [ProductUUID] = @ProductUUID AND [IsActive] = 1

    SET @RowCount = @@ROWCOUNT
END

GO

CREATE PROCEDURE [Runtime].[DeleteProduct]
    @ProductUUID UNIQUEIDENTIFIER,
    @RowCount INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [BusinessData].[Products]
	SET [IsActive] = 0,
        [DeletedDate] = CURRENT_TIMESTAMP
    WHERE [ProductUUID] = @ProductUUID AND [IsActive] = 1

    SET @RowCount = @@ROWCOUNT
END;

GO

CREATE PROCEDURE [Runtime].[GetAllProducts]
	@BusinessUUID UNIQUEIDENTIFIER
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		P.[ProductUUID] AS ID,
		P.[ProductName],
		P.[Price],
		P.[Stock],
		P.[Discount],
		C.[CategoryUUID] AS CategoryID,
		C.[CategoryName],
		P.[ImgUrl],
		P.[Description]
	FROM [BusinessData].[Products] AS P
	INNER JOIN [BusinessData].[Business] AS B ON P.[BusinessId] = B.[BusinessId]
    INNER JOIN [BusinessData].[Categories] AS C ON P.[CategoryId] = C.[CategoryId]
	WHERE [BusinessUUID] = @BusinessUUID AND P.[IsActive] = 1
END


