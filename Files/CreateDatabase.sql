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

-- Create the new tables
CREATE TABLE [dbo].[VehicleType] (
    [Id]   INT           IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (20) NOT NULL,
    CONSTRAINT [PK_VehicleType] PRIMARY KEY CLUSTERED ([Id] ASC)
);

GO

CREATE UNIQUE NONCLUSTERED INDEX [Uq_VehicleType_Name]
    ON [dbo].[VehicleType]([Name] ASC);

GO

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

