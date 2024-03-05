CREATE TABLE [BusinessData].[BusinessConfigurations]
(
    [ConfigId] INT NOT NULL PRIMARY KEY IDENTITY,
    [BusinessId] INT NOT NULL,
    [IsActiveIVA] BIT NOT NULL DEFAULT 0,
    [IVA] DECIMAL(5, 2) NOT NULL DEFAULT 0.00,
    [IsActivePayCash] BIT NOT NULL DEFAULT 0,
    [IsActiveCardPayment] BIT NOT NULL DEFAULT 0,
    [USDExchange] DECIMAL(5, 3) NOT NULL DEFAULT 0.00,
    [TokenCommerce] NVARCHAR(30),
    [ServiceCommerce] NVARCHAR(30), 

);
