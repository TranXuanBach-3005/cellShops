IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [AppConfigs] (
    [Key] nvarchar(450) NOT NULL,
    [Value] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_AppConfigs] PRIMARY KEY ([Key])
);
GO

CREATE TABLE [Categories] (
    [Id] int NOT NULL IDENTITY,
    [SortOrder] int NOT NULL,
    [IsShowOnHome] bit NOT NULL,
    [ParentId] int NULL,
    [Status] int NOT NULL DEFAULT 1,
    CONSTRAINT [PK_Categories] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Contacts] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(200) NOT NULL,
    [Email] nvarchar(200) NOT NULL,
    [PhoneNumber] nvarchar(200) NOT NULL,
    [Message] nvarchar(max) NOT NULL,
    [Status] int NOT NULL,
    CONSTRAINT [PK_Contacts] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Languages] (
    [Id] varchar(5) NOT NULL,
    [Name] nvarchar(20) NOT NULL,
    [IsDefault] bit NOT NULL,
    CONSTRAINT [PK_Languages] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Products] (
    [Id] int NOT NULL IDENTITY,
    [Price] decimal(18,2) NOT NULL,
    [OriginaPrice] decimal(18,2) NOT NULL,
    [Stock] int NOT NULL,
    [ViewCount] int NOT NULL DEFAULT 0,
    [DateCreated] datetime2 NOT NULL,
    [SeoAlias] nvarchar(max) NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Promotions] (
    [Id] int NOT NULL IDENTITY,
    [FromDate] datetime2 NOT NULL,
    [ToDate] datetime2 NOT NULL,
    [ApplyForAll] bit NOT NULL,
    [DiscountPercent] int NULL,
    [DiscountAmount] decimal(18,2) NULL,
    [ProductIds] nvarchar(max) NOT NULL,
    [ProductCategoryIds] nvarchar(max) NOT NULL,
    [Status] int NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Promotions] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [RoleClaims] (
    [Id] int NOT NULL IDENTITY,
    [RoleId] uniqueidentifier NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_RoleClaims] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Roles] (
    [Id] uniqueidentifier NOT NULL,
    [Description] nvarchar(300) NOT NULL,
    [Name] nvarchar(max) NULL,
    [NormalizedName] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    CONSTRAINT [PK_Roles] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Slides] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(200) NOT NULL,
    [Description] nvarchar(200) NOT NULL,
    [Url] nvarchar(200) NOT NULL,
    [Image] nvarchar(200) NOT NULL,
    [SortOrder] int NOT NULL,
    [Status] int NOT NULL,
    CONSTRAINT [PK_Slides] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [UserClaims] (
    [Id] int NOT NULL IDENTITY,
    [UserId] uniqueidentifier NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_UserClaims] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [UserLogins] (
    [UserId] uniqueidentifier NOT NULL,
    [LoginProvider] nvarchar(max) NULL,
    [ProviderKey] nvarchar(max) NULL,
    [ProviderDisplayName] nvarchar(max) NULL,
    CONSTRAINT [PK_UserLogins] PRIMARY KEY ([UserId])
);
GO

CREATE TABLE [UserRoles] (
    [UserId] uniqueidentifier NOT NULL,
    [RoleId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_UserRoles] PRIMARY KEY ([UserId], [RoleId])
);
GO

CREATE TABLE [Users] (
    [Id] uniqueidentifier NOT NULL,
    [FistName] nvarchar(max) NOT NULL,
    [LastName] nvarchar(max) NOT NULL,
    [BirthDay] datetime2 NOT NULL,
    [UserName] nvarchar(max) NULL,
    [NormalizedUserName] nvarchar(max) NULL,
    [Email] nvarchar(max) NULL,
    [NormalizedEmail] nvarchar(max) NULL,
    [EmailConfirmed] bit NOT NULL,
    [PasswordHash] nvarchar(max) NULL,
    [SecurityStamp] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [PhoneNumberConfirmed] bit NOT NULL,
    [TwoFactorEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset NULL,
    [LockoutEnabled] bit NOT NULL,
    [AccessFailedCount] int NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [UserTokens] (
    [UserId] uniqueidentifier NOT NULL,
    [LoginProvider] nvarchar(max) NULL,
    [Name] nvarchar(max) NULL,
    [Value] nvarchar(max) NULL,
    CONSTRAINT [PK_UserTokens] PRIMARY KEY ([UserId])
);
GO

CREATE TABLE [CategoryTranslations] (
    [Id] int NOT NULL IDENTITY,
    [CategoryId] int NOT NULL,
    [Name] nvarchar(200) NOT NULL,
    [SeoDescription] nvarchar(600) NOT NULL,
    [SeoTitle] nvarchar(300) NOT NULL,
    [LanguageId] varchar(5) NOT NULL,
    [SeoAlias] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_CategoryTranslations] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_CategoryTranslations_Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Categories] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_CategoryTranslations_Languages_LanguageId] FOREIGN KEY ([LanguageId]) REFERENCES [Languages] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [ProductImages] (
    [Id] int NOT NULL IDENTITY,
    [ProductId] int NOT NULL,
    [ImagePath] nvarchar(max) NOT NULL,
    [Caption] nvarchar(max) NOT NULL,
    [IsDefault] bit NOT NULL,
    [DateCreated] datetime2 NOT NULL,
    [SortOrder] int NOT NULL,
    [FileSize] bigint NOT NULL,
    CONSTRAINT [PK_ProductImages] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ProductImages_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [ProductInCategories] (
    [ProductId] int NOT NULL,
    [CategoryId] int NOT NULL,
    CONSTRAINT [PK_ProductInCategories] PRIMARY KEY ([ProductId], [CategoryId]),
    CONSTRAINT [FK_ProductInCategories_Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Categories] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ProductInCategories_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [ProductTranslations] (
    [Id] int NOT NULL IDENTITY,
    [ProductId] int NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [Details] nvarchar(500) NOT NULL,
    [SeoDescription] nvarchar(max) NOT NULL,
    [SeoTitle] nvarchar(max) NOT NULL,
    [SeoAlias] nvarchar(200) NOT NULL,
    [LanguageId] varchar(5) NOT NULL,
    CONSTRAINT [PK_ProductTranslations] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ProductTranslations_Languages_LanguageId] FOREIGN KEY ([LanguageId]) REFERENCES [Languages] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ProductTranslations_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Cart] (
    [Id] int NOT NULL IDENTITY,
    [ProductId] int NOT NULL,
    [Quantity] int NOT NULL,
    [Price] decimal(18,2) NOT NULL,
    [UserId] uniqueidentifier NOT NULL,
    [DateCreated] datetime2 NOT NULL,
    CONSTRAINT [PK_Cart] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Cart_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Cart_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Orders] (
    [Id] int NOT NULL IDENTITY,
    [OrderDate] datetime2 NOT NULL,
    [UserId] uniqueidentifier NOT NULL,
    [ShipName] nvarchar(200) NOT NULL,
    [ShipAddress] nvarchar(200) NOT NULL,
    [ShipEmail] varchar(50) NOT NULL,
    [ShipPhoneNumber] nvarchar(200) NOT NULL,
    [Status] int NOT NULL,
    CONSTRAINT [PK_Orders] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Orders_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Transactions] (
    [Id] int NOT NULL IDENTITY,
    [TransactionDate] datetime2 NOT NULL,
    [ExternalTransactionId] nvarchar(max) NOT NULL,
    [Amount] decimal(18,2) NOT NULL,
    [Fee] decimal(18,2) NOT NULL,
    [Result] nvarchar(max) NOT NULL,
    [Message] nvarchar(max) NOT NULL,
    [Status] int NOT NULL,
    [Provider] nvarchar(max) NOT NULL,
    [UserId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Transactions] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Transactions_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [OrderDetails] (
    [OrderId] int NOT NULL,
    [ProductId] int NOT NULL,
    [Quantity] int NOT NULL,
    [Price] decimal(18,2) NOT NULL,
    CONSTRAINT [PK_OrderDetails] PRIMARY KEY ([OrderId], [ProductId]),
    CONSTRAINT [FK_OrderDetails_Orders_OrderId] FOREIGN KEY ([OrderId]) REFERENCES [Orders] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_OrderDetails_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE CASCADE
);
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Key', N'Value') AND [object_id] = OBJECT_ID(N'[AppConfigs]'))
    SET IDENTITY_INSERT [AppConfigs] ON;
INSERT INTO [AppConfigs] ([Key], [Value])
VALUES (N'HomeDescription', N'This is description of eShopSolution'),
(N'HomeKeyword', N'This is keyword of eShopSolution'),
(N'HomeTitle', N'This is home page of eShopSolution');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Key', N'Value') AND [object_id] = OBJECT_ID(N'[AppConfigs]'))
    SET IDENTITY_INSERT [AppConfigs] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'IsShowOnHome', N'ParentId', N'SortOrder', N'Status') AND [object_id] = OBJECT_ID(N'[Categories]'))
    SET IDENTITY_INSERT [Categories] ON;
INSERT INTO [Categories] ([Id], [IsShowOnHome], [ParentId], [SortOrder], [Status])
VALUES (1, CAST(1 AS bit), NULL, 1, 1),
(2, CAST(1 AS bit), NULL, 2, 1);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'IsShowOnHome', N'ParentId', N'SortOrder', N'Status') AND [object_id] = OBJECT_ID(N'[Categories]'))
    SET IDENTITY_INSERT [Categories] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'IsDefault', N'Name') AND [object_id] = OBJECT_ID(N'[Languages]'))
    SET IDENTITY_INSERT [Languages] ON;
INSERT INTO [Languages] ([Id], [IsDefault], [Name])
VALUES ('en', CAST(0 AS bit), N'English'),
('vi', CAST(1 AS bit), N'Tiếng Việt');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'IsDefault', N'Name') AND [object_id] = OBJECT_ID(N'[Languages]'))
    SET IDENTITY_INSERT [Languages] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'DateCreated', N'OriginaPrice', N'Price', N'SeoAlias', N'Stock') AND [object_id] = OBJECT_ID(N'[Products]'))
    SET IDENTITY_INSERT [Products] ON;
INSERT INTO [Products] ([Id], [DateCreated], [OriginaPrice], [Price], [SeoAlias], [Stock])
VALUES (1, '2022-08-30T22:32:06.7098912+07:00', 100000.0, 200000.0, NULL, 0);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'DateCreated', N'OriginaPrice', N'Price', N'SeoAlias', N'Stock') AND [object_id] = OBJECT_ID(N'[Products]'))
    SET IDENTITY_INSERT [Products] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ConcurrencyStamp', N'Description', N'Name', N'NormalizedName') AND [object_id] = OBJECT_ID(N'[Roles]'))
    SET IDENTITY_INSERT [Roles] ON;
INSERT INTO [Roles] ([Id], [ConcurrencyStamp], [Description], [Name], [NormalizedName])
VALUES ('55402ddf-8528-4dda-a2cc-2d2fa73909fe', N'cc5163d8-0b1e-41ac-b3e6-b6cbd03fa438', N'Administrator role', N'admin', N'admin');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ConcurrencyStamp', N'Description', N'Name', N'NormalizedName') AND [object_id] = OBJECT_ID(N'[Roles]'))
    SET IDENTITY_INSERT [Roles] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Description', N'Image', N'Name', N'SortOrder', N'Status', N'Url') AND [object_id] = OBJECT_ID(N'[Slides]'))
    SET IDENTITY_INSERT [Slides] ON;
INSERT INTO [Slides] ([Id], [Description], [Image], [Name], [SortOrder], [Status], [Url])
VALUES (1, N'Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus. Nullam id dolor id nibh ultricies vehicula ut id elit.', N'/themes/images/carousel/1.png', N'Second Thumbnail label', 1, 1, N'#'),
(2, N'Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus. Nullam id dolor id nibh ultricies vehicula ut id elit.', N'/themes/images/carousel/2.png', N'Second Thumbnail label', 2, 1, N'#'),
(3, N'Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus. Nullam id dolor id nibh ultricies vehicula ut id elit.', N'/themes/images/carousel/3.png', N'Second Thumbnail label', 3, 1, N'#'),
(4, N'Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus. Nullam id dolor id nibh ultricies vehicula ut id elit.', N'/themes/images/carousel/4.png', N'Second Thumbnail label', 4, 1, N'#'),
(5, N'Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus. Nullam id dolor id nibh ultricies vehicula ut id elit.', N'/themes/images/carousel/5.png', N'Second Thumbnail label', 5, 1, N'#'),
(6, N'Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus. Nullam id dolor id nibh ultricies vehicula ut id elit.', N'/themes/images/carousel/6.png', N'Second Thumbnail label', 6, 1, N'#');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Description', N'Image', N'Name', N'SortOrder', N'Status', N'Url') AND [object_id] = OBJECT_ID(N'[Slides]'))
    SET IDENTITY_INSERT [Slides] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'RoleId', N'UserId') AND [object_id] = OBJECT_ID(N'[UserRoles]'))
    SET IDENTITY_INSERT [UserRoles] ON;
INSERT INTO [UserRoles] ([RoleId], [UserId])
VALUES ('55402ddf-8528-4dda-a2cc-2d2fa73909fe', '8049a3c9-f944-425a-b991-ffc5c2594218');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'RoleId', N'UserId') AND [object_id] = OBJECT_ID(N'[UserRoles]'))
    SET IDENTITY_INSERT [UserRoles] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AccessFailedCount', N'BirthDay', N'ConcurrencyStamp', N'Email', N'EmailConfirmed', N'FistName', N'LastName', N'LockoutEnabled', N'LockoutEnd', N'NormalizedEmail', N'NormalizedUserName', N'PasswordHash', N'PhoneNumber', N'PhoneNumberConfirmed', N'SecurityStamp', N'TwoFactorEnabled', N'UserName') AND [object_id] = OBJECT_ID(N'[Users]'))
    SET IDENTITY_INSERT [Users] ON;
INSERT INTO [Users] ([Id], [AccessFailedCount], [BirthDay], [ConcurrencyStamp], [Email], [EmailConfirmed], [FistName], [LastName], [LockoutEnabled], [LockoutEnd], [NormalizedEmail], [NormalizedUserName], [PasswordHash], [PhoneNumber], [PhoneNumberConfirmed], [SecurityStamp], [TwoFactorEnabled], [UserName])
VALUES ('8049a3c9-f944-425a-b991-ffc5c2594218', 0, '2001-01-31T00:00:00.0000000', N'132affef-10b4-4faa-ab53-503e40c1f620', N'bachtran3005@gmail.com', CAST(1 AS bit), N'Xuan', N'Bach', CAST(0 AS bit), NULL, N'bachtran3005@gmail.com', N'admin', N'AQAAAAEAACcQAAAAEF+9kkVpxyDDc1PMaRrCRqgIQ/jwX6X6lAPzUEsrw7Zd/TD9JXAsQB4OJnccr8fFqg==', NULL, CAST(0 AS bit), N'', CAST(0 AS bit), N'admin');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AccessFailedCount', N'BirthDay', N'ConcurrencyStamp', N'Email', N'EmailConfirmed', N'FistName', N'LastName', N'LockoutEnabled', N'LockoutEnd', N'NormalizedEmail', N'NormalizedUserName', N'PasswordHash', N'PhoneNumber', N'PhoneNumberConfirmed', N'SecurityStamp', N'TwoFactorEnabled', N'UserName') AND [object_id] = OBJECT_ID(N'[Users]'))
    SET IDENTITY_INSERT [Users] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CategoryId', N'LanguageId', N'Name', N'SeoAlias', N'SeoDescription', N'SeoTitle') AND [object_id] = OBJECT_ID(N'[CategoryTranslations]'))
    SET IDENTITY_INSERT [CategoryTranslations] ON;
INSERT INTO [CategoryTranslations] ([Id], [CategoryId], [LanguageId], [Name], [SeoAlias], [SeoDescription], [SeoTitle])
VALUES (1, 1, 'vi', N'Áo nam', N'ao-nam', N'Sản phẩm áo thời trang nam', N'Sản phẩm áo thời trang nam'),
(2, 1, 'en', N'Men Shirt', N'men-shirt', N'The shirt products for men', N'The shirt products for men'),
(3, 2, 'vi', N'Áo nữ', N'ao-nu', N'Sản phẩm áo thời trang nữ', N'Sản phẩm áo thời trang women'),
(4, 2, 'en', N'Women Shirt', N'women-shirt', N'The shirt products for women', N'The shirt products for women');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CategoryId', N'LanguageId', N'Name', N'SeoAlias', N'SeoDescription', N'SeoTitle') AND [object_id] = OBJECT_ID(N'[CategoryTranslations]'))
    SET IDENTITY_INSERT [CategoryTranslations] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'CategoryId', N'ProductId') AND [object_id] = OBJECT_ID(N'[ProductInCategories]'))
    SET IDENTITY_INSERT [ProductInCategories] ON;
INSERT INTO [ProductInCategories] ([CategoryId], [ProductId])
VALUES (1, 1);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'CategoryId', N'ProductId') AND [object_id] = OBJECT_ID(N'[ProductInCategories]'))
    SET IDENTITY_INSERT [ProductInCategories] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Description', N'Details', N'LanguageId', N'Name', N'ProductId', N'SeoAlias', N'SeoDescription', N'SeoTitle') AND [object_id] = OBJECT_ID(N'[ProductTranslations]'))
    SET IDENTITY_INSERT [ProductTranslations] ON;
INSERT INTO [ProductTranslations] ([Id], [Description], [Details], [LanguageId], [Name], [ProductId], [SeoAlias], [SeoDescription], [SeoTitle])
VALUES (1, N'Áo sơ mi nam trắng Việt Tiến', N'Áo sơ mi nam trắng Việt Tiến', 'vi', N'Áo sơ mi nam trắng Việt Tiến', 1, N'ao-so-mi-nam-trang-viet-tien', N'Áo sơ mi nam trắng Việt Tiến', N'Áo sơ mi nam trắng Việt Tiến'),
(2, N'Viet Tien Men T-Shirt', N'Viet Tien Men T-Shirt', 'en', N'Viet Tien Men T-Shirt', 1, N'viet-tien-men-t-shirt', N'Viet Tien Men T-Shirt', N'Viet Tien Men T-Shirt');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Description', N'Details', N'LanguageId', N'Name', N'ProductId', N'SeoAlias', N'SeoDescription', N'SeoTitle') AND [object_id] = OBJECT_ID(N'[ProductTranslations]'))
    SET IDENTITY_INSERT [ProductTranslations] OFF;
GO

CREATE INDEX [IX_Cart_ProductId] ON [Cart] ([ProductId]);
GO

CREATE INDEX [IX_Cart_UserId] ON [Cart] ([UserId]);
GO

CREATE INDEX [IX_CategoryTranslations_CategoryId] ON [CategoryTranslations] ([CategoryId]);
GO

CREATE INDEX [IX_CategoryTranslations_LanguageId] ON [CategoryTranslations] ([LanguageId]);
GO

CREATE INDEX [IX_OrderDetails_ProductId] ON [OrderDetails] ([ProductId]);
GO

CREATE INDEX [IX_Orders_UserId] ON [Orders] ([UserId]);
GO

CREATE INDEX [IX_ProductImages_ProductId] ON [ProductImages] ([ProductId]);
GO

CREATE INDEX [IX_ProductInCategories_CategoryId] ON [ProductInCategories] ([CategoryId]);
GO

CREATE INDEX [IX_ProductTranslations_LanguageId] ON [ProductTranslations] ([LanguageId]);
GO

CREATE INDEX [IX_ProductTranslations_ProductId] ON [ProductTranslations] ([ProductId]);
GO

CREATE INDEX [IX_Transactions_UserId] ON [Transactions] ([UserId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220830153207_addDatabase', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

UPDATE [Products] SET [DateCreated] = '2022-10-03T09:02:58.9510824+07:00'
WHERE [Id] = 1;
SELECT @@ROWCOUNT;

GO

UPDATE [Roles] SET [ConcurrencyStamp] = N'e5395ed0-0263-4b9e-a3f6-1ecfa447ddf0'
WHERE [Id] = '55402ddf-8528-4dda-a2cc-2d2fa73909fe';
SELECT @@ROWCOUNT;

GO

UPDATE [Users] SET [ConcurrencyStamp] = N'7cc6f701-4ee9-436f-9db9-e30c5177509d', [PasswordHash] = N'AQAAAAEAACcQAAAAEKDMaa4iBAeTkPd2yw3q8Ik3JZk92G4+9/JRphwszdgkTgNdyF0Q3hxvX8iijH5CZw=='
WHERE [Id] = '8049a3c9-f944-425a-b991-ffc5c2594218';
SELECT @@ROWCOUNT;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20221003020259_UpdateDatabase', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

UPDATE [Products] SET [DateCreated] = '2022-10-07T14:44:52.0172077+07:00'
WHERE [Id] = 1;
SELECT @@ROWCOUNT;

GO

UPDATE [Roles] SET [ConcurrencyStamp] = N'9ec204c5-fd5d-45b7-9076-ed6611f3d831'
WHERE [Id] = '55402ddf-8528-4dda-a2cc-2d2fa73909fe';
SELECT @@ROWCOUNT;

GO

UPDATE [Users] SET [ConcurrencyStamp] = N'1e6c62b0-bbff-44e4-9711-e6d58bffa43b', [PasswordHash] = N'AQAAAAEAACcQAAAAEP6vrVAviiNr2uABPYFY110S+9xxnqBEkUvr87lO0TK86Yfwikh2LXCoNz6Q6zMafA=='
WHERE [Id] = '8049a3c9-f944-425a-b991-ffc5c2594218';
SELECT @@ROWCOUNT;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20221007074452_cellShop', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Products] ADD [IsFeatured] bit NULL;
GO

UPDATE [Products] SET [DateCreated] = '2022-10-11T21:50:02.7026344+07:00'
WHERE [Id] = 1;
SELECT @@ROWCOUNT;

GO

UPDATE [Roles] SET [ConcurrencyStamp] = N'c88eac1d-3915-4ed0-b07b-abfc6048eaa6'
WHERE [Id] = '55402ddf-8528-4dda-a2cc-2d2fa73909fe';
SELECT @@ROWCOUNT;

GO

UPDATE [Users] SET [ConcurrencyStamp] = N'0e2fafc8-8192-4c08-a32c-16be09fe4a51', [PasswordHash] = N'AQAAAAEAACcQAAAAEOunMadIKctO9sPx2v4ASPkol5Y2xmCjhdBSYMG+oxq51bW+WDkLnvb+WGZ1Mc/YBQ=='
WHERE [Id] = '8049a3c9-f944-425a-b991-ffc5c2594218';
SELECT @@ROWCOUNT;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20221011145003_UpdateCodeSile', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ProductTranslations]') AND [c].[name] = N'SeoAlias');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [ProductTranslations] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [ProductTranslations] ALTER COLUMN [SeoAlias] nvarchar(500) NOT NULL;
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ProductTranslations]') AND [c].[name] = N'Details');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [ProductTranslations] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [ProductTranslations] ALTER COLUMN [Details] nvarchar(max) NOT NULL;
GO

UPDATE [Products] SET [DateCreated] = '2022-11-15T10:22:09.5881106+07:00'
WHERE [Id] = 1;
SELECT @@ROWCOUNT;

GO

UPDATE [Roles] SET [ConcurrencyStamp] = N'15b0e130-1012-4581-962e-15d284589d1e'
WHERE [Id] = '55402ddf-8528-4dda-a2cc-2d2fa73909fe';
SELECT @@ROWCOUNT;

GO

UPDATE [Users] SET [ConcurrencyStamp] = N'23099cac-5fbb-4465-ad22-69d904a0232e', [PasswordHash] = N'AQAAAAEAACcQAAAAEIO9+Lr73D5Qo4SL3VYPrmaWqBFJDrocFuHkwZGwRnEBKHh3rX+/4cuk0p+A0lKPww=='
WHERE [Id] = '8049a3c9-f944-425a-b991-ffc5c2594218';
SELECT @@ROWCOUNT;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20221115032210_updateDetails', N'6.0.7');
GO

COMMIT;
GO

