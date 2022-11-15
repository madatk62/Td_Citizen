BEGIN TRANSACTION;
GO

ALTER TABLE [Catalog].[Seat] ADD [TicketId] int NULL;
GO

CREATE TABLE [Catalog].[Ticket] (
    [Id] int NOT NULL IDENTITY,
    [Phone] nvarchar(max) NULL,
    [BookedDate] datetime2 NULL,
    [TripId] uniqueidentifier NULL,
    CONSTRAINT [PK_Ticket] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Ticket_Trips_TripId] FOREIGN KEY ([TripId]) REFERENCES [Catalog].[Trips] ([Id])
);
GO

CREATE INDEX [IX_Seat_TicketId] ON [Catalog].[Seat] ([TicketId]);
GO

CREATE INDEX [IX_Ticket_TripId] ON [Catalog].[Ticket] ([TripId]);
GO

ALTER TABLE [Catalog].[Seat] ADD CONSTRAINT [FK_Seat_Ticket_TicketId] FOREIGN KEY ([TicketId]) REFERENCES [Catalog].[Ticket] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220830080111_Update_54_UpdateTripTicket', N'6.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DROP TABLE [Catalog].[Seat];
GO

DROP TABLE [Catalog].[Ticket];
GO

CREATE TABLE [Catalog].[Passengers] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(256) NOT NULL,
    [Email] nvarchar(256) NULL,
    [Phone] nvarchar(256) NULL,
    [TenantId] nvarchar(64) NOT NULL,
    [CreatedBy] nvarchar(max) NULL,
    [CreatedOn] datetime2 NULL,
    [LastModifiedBy] nvarchar(max) NULL,
    [LastModifiedOn] datetime2 NULL,
    [DeletedOn] datetime2 NULL,
    [DeletedBy] nvarchar(max) NULL,
    CONSTRAINT [PK_Passengers] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Catalog].[Tickets] (
    [Id] uniqueidentifier NOT NULL,
    [TripId] uniqueidentifier NULL,
    [PassengerId] uniqueidentifier NULL,
    [Seat] nvarchar(max) NULL,
    [Date] datetime2 NULL,
    [TenantId] nvarchar(64) NOT NULL,
    [CreatedBy] nvarchar(max) NULL,
    [CreatedOn] datetime2 NULL,
    [LastModifiedBy] nvarchar(max) NULL,
    [LastModifiedOn] datetime2 NULL,
    [DeletedOn] datetime2 NULL,
    [DeletedBy] nvarchar(max) NULL,
    CONSTRAINT [PK_Tickets] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Tickets_Passengers_PassengerId] FOREIGN KEY ([PassengerId]) REFERENCES [Catalog].[Passengers] ([Id]),
    CONSTRAINT [FK_Tickets_Trips_TripId] FOREIGN KEY ([TripId]) REFERENCES [Catalog].[Trips] ([Id])
);
GO

CREATE INDEX [IX_Tickets_PassengerId] ON [Catalog].[Tickets] ([PassengerId]);
GO

CREATE INDEX [IX_Tickets_TripId] ON [Catalog].[Tickets] ([TripId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220926033042_Update_55', N'6.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Catalog].[Trips] ADD [Seat] int NULL;
GO

ALTER TABLE [Catalog].[Trips] ADD [Type] nvarchar(max) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220927035059_Update_56', N'6.0.2');
GO

COMMIT;
GO

