BEGIN TRANSACTION;
GO

ALTER TABLE [Catalog].[NhomHoSoDienTus] ADD [IDCongDan] nvarchar(max) NULL;
GO

ALTER TABLE [Catalog].[LoaiHoSoDienTus] ADD [IDCongDan] nvarchar(max) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20221115103359_Update_64', N'6.0.2');
GO

COMMIT;
GO

