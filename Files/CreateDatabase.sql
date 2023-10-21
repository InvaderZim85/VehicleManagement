-- Delete tables if exists
IF EXISTS (SELECT TOP (1) 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Vehicle')
BEGIN
    DROP TABLE dbo.Vehicle;
END

GO

IF EXISTS (SELECT TOP (1) 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'VehicleType')
BEGIN
    DROP TABLE dbo.VehicleType;
END

GO

IF EXISTS (SELECT TOP (1) 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Finances')
BEGIN
    DROP TABLE dbo.Finances;
END

GO

IF EXISTS (SELECT TOP (1) 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'FinancesType')
BEGIN
    DROP TABLE dbo.FinancesType;
END

GO

IF EXISTS (SELECT TOP (1) 1 FROM sys.triggers WHERE [name] = '')
BEGIN
    DROP TRIGGER dbo.ScheduleType_Delete;
END

GO

IF EXISTS (SELECT TOP (1) 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'ScheduleType')
BEGIN
    DROP TABLE dbo.ScheduleType;
END

GO

IF EXISTS (SELECT TOP (1) 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Contact')
BEGIN
    DROP TABLE dbo.Contact;
END

GO

IF EXISTS (SELECT TOP (1) 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Settings')
BEGIN
    DROP TABLE dbo.Settings;
END

GO

-- Create the new tables
-- Vehicle type
CREATE TABLE [dbo].[VehicleType] (
    [Id]   INT           IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (20) NOT NULL,
    CONSTRAINT [PK_VehicleType] PRIMARY KEY CLUSTERED ([Id] ASC)
);

GO

CREATE UNIQUE NONCLUSTERED INDEX [Uq_VehicleType_Name]
    ON [dbo].[VehicleType]([Name] ASC);

GO

-- Vehicle
CREATE TABLE [dbo].[Vehicle] (
    [Id]                      INT           IDENTITY (1, 1) NOT NULL,
    [Manufacturer]            NVARCHAR (50) NOT NULL,
    [Model]                   NVARCHAR (50) NOT NULL,
    [Vin]                     NVARCHAR (20) NOT NULL,
    [TypeId]                  INT           NOT NULL,
    [LicensePlate]            NVARCHAR (10) NOT NULL,
    [InitialRegistrationDate] DATE          NOT NULL,
    [BuyDate]                 DATE          NOT NULL,
    [CreationDateTime]        DATETIME2 (7) CONSTRAINT [DEFAULT_Vehicle_CreationDateTime] DEFAULT (sysdatetime()) NOT NULL,
    [ModifiedDateTime]        DATETIME2 (7) CONSTRAINT [DEFAULT_Vehicle_ModifiedDateTime] DEFAULT (sysdatetime()) NOT NULL,
    CONSTRAINT [PK_Vehicle] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Vehicle_VehicleType] FOREIGN KEY ([TypeId]) REFERENCES [dbo].[VehicleType] ([Id])
);

-- Schedule type
CREATE TABLE [dbo].[ScheduleType] (
    [Id]   INT           IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (20) NOT NULL,
    CONSTRAINT [PK_ScheduleType] PRIMARY KEY CLUSTERED ([Id] ASC)
);

GO

CREATE UNIQUE NONCLUSTERED INDEX [Uq_ScheduleType_Name]
    ON [dbo].[ScheduleType]([Name] ASC);

GO

-- Add the default values

INSERT INTO dbo.ScheduleType
VALUES
    ('One time'),
    ('Monthly'),
    ('Per quarter'),
    ('Annual')

GO

-- Add the delete trigger
CREATE TRIGGER dbo.ScheduleType_Delete
    ON dbo.ScheduleType
INSTEAD OF DELETE
AS
BEGIN
    DECLARE @id INT;

    SET @id = 
    (
        SELECT TOP (1) 
            Id
        FROM
            deleted
    );

    IF (@id <= 4)
    BEGIN
        RAISERROR('You are not allowed to delete a default entry.', 16, 1);
        RETURN;
    END
    ELSE
    BEGIN
        DELETE FROM dbo.ScheduleType
        WHERE
            Id = @id;
    END
END

GO

-- Contact
CREATE TABLE [dbo].[Contact] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [Name]             NVARCHAR (100) NOT NULL,
    [Mail]             NVARCHAR (100) NOT NULL,
    [Phone]            NVARCHAR (100) NOT NULL,
    [AdditionalInfo]   NVARCHAR (MAX) NOT NULL,
    [CreationDateTime] DATETIME2 (7)  CONSTRAINT [DEFAULT_Contact_CreationDateTime] DEFAULT SYSDATETIME() NOT NULL,
    [ModifiedDateTime] DATETIME2 (7)  CONSTRAINT [DEFAULT_Contact_ModifiedDateTime] DEFAULT SYSDATETIME() NOT NULL,
    CONSTRAINT [PK_Contact] PRIMARY KEY CLUSTERED ([Id] ASC)
);

GO

-- Finance types
CREATE TABLE [dbo].[FinancesType] (
    [Id]   INT           NOT NULL,
    [Name] NVARCHAR (20) NOT NULL,
    CONSTRAINT [PK_FinancesType] PRIMARY KEY CLUSTERED ([Id] ASC)
);

GO

CREATE UNIQUE NONCLUSTERED INDEX [Uq_FinacesType_Name]
    ON [dbo].[FinancesType]([Name] ASC);

GO

-- Finance
CREATE TABLE [dbo].[Finances] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [Name]             NVARCHAR (50)  NOT NULL,
    [Description]      NVARCHAR (MAX) NOT NULL,
    [TypeId]           INT            NOT NULL,
    [ScheduleTypeId]   INT            NOT NULL,
    [Cost]             DECIMAL (9, 2) NOT NULL,
    [ContractDate]     DATE           NOT NULL,
    [ContactId]        INT            CONSTRAINT [DEFAULT_Finances_ContactId] DEFAULT ((0)) NULL,
    [CreationDateTime] DATETIME2 (7)  CONSTRAINT [DEFAULT_Finances_CreationDateTime] DEFAULT (sysdatetime()) NOT NULL,
    [ModifiedDateTime] DATETIME2 (7)  CONSTRAINT [DEFAULT_Finances_ModifiedDateTime] DEFAULT (sysdatetime()) NOT NULL,
    CONSTRAINT [PK_Finances] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Finances_Contact] FOREIGN KEY ([ContactId]) REFERENCES [dbo].[Contact] ([Id]),
    CONSTRAINT [FK_Finances_FinancesType] FOREIGN KEY ([TypeId]) REFERENCES [dbo].[FinancesType] ([Id]),
    CONSTRAINT [FK_Finances_ScheduleType] FOREIGN KEY ([ScheduleTypeId]) REFERENCES [dbo].[ScheduleType] ([Id])
);

GO

-- Settings
CREATE TABLE [dbo].[Settings] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [Key]              INT            NOT NULL,
    [Value]            NVARCHAR (MAX) NOT NULL,
    [Description]      NVARCHAR (MAX) NOT NULL,
    [CreationDateTime] DATETIME2 (7)  CONSTRAINT [DEFAULT_Settings_CreationDateTime] DEFAULT SYSDATETIME() NOT NULL,
    [ModifiedDateTime] DATETIME2 (7)  CONSTRAINT [DEFAULT_Settings_ModifiedDateTime] DEFAULT SYSDATETIME() NOT NULL,
    CONSTRAINT [PK_Settings] PRIMARY KEY CLUSTERED ([Id] ASC)
);

GO

CREATE UNIQUE NONCLUSTERED INDEX [Uq_Settings_Key]
    ON [dbo].[Settings]([Key] ASC);