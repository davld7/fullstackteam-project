CREATE TABLE [BusinessData].[TransactionDetails]
(
	[TransactionDetailsId] INT NOT NULL PRIMARY KEY,
	[TransactionUUID] UNIQUEIDENTIFIER NOT NULL,
	[ProductName] NVARCHAR(50) NOT NULL,
	[Quantity] INT NOT NULL,
	[Price] FLOAT NOT NULL,
	[Discount] FLOAT ,
	[IVAmount] FLOAT, 

   
)
