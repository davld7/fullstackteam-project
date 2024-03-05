--Business Table
ALTER TABLE [BusinessData].[Business]
	ADD CONSTRAINT [FK_Business_Department] 
	FOREIGN KEY ([DepartmentId]) 
	REFERENCES [SystemConfig].[Department] ([DepartmentId]);
GO
ALTER TABLE [BusinessData].[Business]
	ADD CONSTRAINT [FK_Business_Owner] 
	FOREIGN KEY ([OwnerId]) 
	REFERENCES [UserManagement].[OwnersBusiness] ([OwnerId]);

GO
ALTER TABLE [BusinessData].[Business]
	ADD CONSTRAINT [FK_Business_BusinessType] 
	FOREIGN KEY ([BusinessTypeId]) 
	REFERENCES [SystemConfig].[BusinessTypes] ([BusinessTypeId]);
GO

--Users Table
ALTER TABLE [UserManagement].[Users]
	ADD CONSTRAINT [FK_Users_Roles] 
	FOREIGN KEY ([RoleId]) 
	REFERENCES [UserManagement].[Roles] ([RoleId]);
GO
ALTER TABLE [UserManagement].[Users]
	ADD CONSTRAINT [FK_Users_Business] 
	FOREIGN KEY ([BusinessId]) 
	REFERENCES [BusinessData].[Business] ([BusinessId]) ON DELETE CASCADE;
GO

-- Categories Table
ALTER TABLE [BusinessData].[Categories]
    ADD CONSTRAINT [FK_Categories_Business]
    FOREIGN KEY ([BusinessId])
    REFERENCES [BusinessData].[Business]([BusinessId]) ON DELETE CASCADE;
GO

-- Transactions Table
ALTER TABLE [BusinessData].[Transactions]
    ADD CONSTRAINT FK_Transaction_Business
    FOREIGN KEY ([BusinessId])
    REFERENCES [BusinessData].[Business] ([BusinessId]) ON DELETE CASCADE;
GO
ALTER TABLE [BusinessData].[Transactions]
    ADD CONSTRAINT FK_Transaction_Currency
    FOREIGN KEY ([CurrencyId])
    REFERENCES [SystemConfig].[Currencies] ([CurrencyId]);
GO
ALTER TABLE [BusinessData].[Transactions]
    ADD CONSTRAINT FK_Transaction_PaymentType
    FOREIGN KEY ([PaymentTypeId])
    REFERENCES [SystemConfig].[PaymentTypes] ([PaymentId]);
GO

ALTER TABLE [BusinessData].[TransactionDetails]
	ADD CONSTRAINT [FK_TransactionDetails_Transaction] 
	FOREIGN KEY ([TransactionUUID]) REFERENCES [BusinessData].[Transactions]([TransactionUUID]);
GO

-- BusinessConfigurations
ALTER TABLE [BusinessData].[BusinessConfigurations]
    ADD CONSTRAINT [FK_BusinessConfigurations_Business] 
    FOREIGN KEY ([BusinessId]) 
    REFERENCES [BusinessData].[Business]([BusinessId]) ON DELETE CASCADE;
GO

-- Products
ALTER TABLE [BusinessData].[Products]
	ADD CONSTRAINT [FK_Products_Business] 
    FOREIGN KEY ([BusinessId]) 
    REFERENCES [BusinessData].[Business]([BusinessId]) ON DELETE CASCADE;
GO

ALTER TABLE [BusinessData].[Products]
	ADD CONSTRAINT [FK_Products_Categories]
	FOREIGN KEY ([CategoryId])
	REFERENCES [BusinessData].[Categories]([CategoryId])
GO

-- OwnersBusiness
ALTER TABLE [UserManagement].[OwnersBusiness]
	ADD CONSTRAINT [FK_OwnersBusiness_Roles]
	FOREIGN KEY ([RoleId])
	REFERENCES [UserManagement].[Roles]([RoleId])


