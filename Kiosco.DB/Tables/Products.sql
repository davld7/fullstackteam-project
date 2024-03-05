﻿CREATE TABLE [BusinessData].[Products]
(
	[ProductId] INT NOT NULL PRIMARY KEY IDENTITY,
	[ProductUUID] UNIQUEIDENTIFIER NOT NULL,
	[BusinessId] INT NOT NULL,
	[ProductName] NVARCHAR(50) NOT NULL,
	[Price] DECIMAL(18,2) NOT NULL,
	[Stock] INT NOT NULL,
	[Discount] DECIMAL(5,2) DEFAULT 0.00,
	[CategoryId] INT NOT NULL,
	[ImgUrl] NVARCHAR(MAX) NOT NULL,
	[Description] NVARCHAR(MAX) NOT NULL,
	[IsActive] BIT NOT NULL DEFAULT 1,	
	[CreatedDate] DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
	[ModifiedDate] DATETIME,
	[DeletedDate] DATETIME,
)

GO
CREATE NONCLUSTERED INDEX [IX_Products_ProductUUID] ON [BusinessData].[Products] ([ProductUUID]);
