CREATE TABLE [SystemConfig].[Currencies]
(
	[CurrencyId] INT NOT NULL PRIMARY KEY IDENTITY,
	[CurrencyName] NVARCHAR(20) NOT NULL,
	[CurrencyCode] NVARCHAR(3) NOT NULL,
	[IsActive] BIT NOT NULL DEFAULT 1
)
