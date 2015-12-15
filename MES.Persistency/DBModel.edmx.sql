
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 06/25/2015 16:47:00
-- Generated from EDMX file: E:\D\home\v-rawang\Documents\MAB\SourceCode\MES\MES.Persistency\DBModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO

IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_DataIdentifierDataItem]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DBDataItems] DROP CONSTRAINT [FK_DataIdentifierDataItem];
GO
IF OBJECT_ID(N'[dbo].[FK_DataItemDataParameter]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DBDataParameters] DROP CONSTRAINT [FK_DataItemDataParameter];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[DBDataIdentifiers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DBDataIdentifiers];
GO
IF OBJECT_ID(N'[dbo].[DBDataItems]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DBDataItems];
GO
IF OBJECT_ID(N'[dbo].[DBDataParameters]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DBDataParameters];
GO
IF OBJECT_ID(N'[dbo].[ProductKeyIDSerialNumberPairs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProductKeyIDSerialNumberPairs];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'DBDataIdentifiers'
CREATE TABLE [dbo].[DBDataIdentifiers] (
    [DataUniqueID] nvarchar(200)  NOT NULL,
    [DataType] int  NULL,
    [RawData] varbinary(max)  NULL
);
GO

-- Creating table 'DBDataItems'
CREATE TABLE [dbo].[DBDataItems] (
    [DataItemID] nvarchar(200)  NOT NULL,
    [DataUniqueID] nvarchar(200)  NULL,
    [DataBytes] varbinary(max)  NULL,
    [Description] nvarchar(500)  NULL,
    [Size] bigint  NULL,
    [DeviceID] nvarchar(200)  NULL,
    [LocationID] nvarchar(200)  NULL,
    [CreationTime] datetime  NOT NULL,
    [DataIdentifierDataUniqueID] nvarchar(200)  NOT NULL
);
GO

-- Creating table 'DBDataParameters'
CREATE TABLE [dbo].[DBDataParameters] (
    [DataItemID] nvarchar(200)  NOT NULL,
    [Name] nvarchar(250)  NOT NULL,
    [Value] nvarchar(300)  NULL,
    [ID] nvarchar(200)  NOT NULL,
    [DataItemDataItemID] nvarchar(200)  NOT NULL
);
GO

-- Creating table 'ProductKeyIDSerialNumberPairs'
CREATE TABLE [dbo].[ProductKeyIDSerialNumberPairs] (
    [PairID] uniqueidentifier  NOT NULL,
    [TransactionID] nvarchar(150)  NULL,
    [ProductKeyID] bigint  NULL,
    [SerialNumber] nvarchar(200)  NOT NULL,
    [SeriesID] nvarchar(200)  NULL,
    [SeriesName] nvarchar(250)  NULL,
    [ModelID] nvarchar(200)  NULL,
    [ModelName] nvarchar(250)  NULL,
    [DeviceID] nvarchar(200)  NULL,
    [DeviceName] nvarchar(250)  NULL,
    [OperatorID] nvarchar(200)  NULL,
    [OperatorName] nvarchar(250)  NULL,
    [StationID] nvarchar(200)  NULL,
    [StationName] nvarchar(250)  NULL,
    [LineID] nvarchar(200)  NULL,
    [LineName] nvarchar(250)  NULL,
    [BusinessID] nvarchar(200)  NULL,
    [BusinessName] nvarchar(250)  NULL,
    [OrderID] nvarchar(200)  NULL,
    [ReadCount] int  NULL,
    [CreationTime] datetime  NULL,
    [ModificationTime] datetime  NULL,
    [Remarks] nvarchar(500)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [DataUniqueID] in table 'DBDataIdentifiers'
ALTER TABLE [dbo].[DBDataIdentifiers]
ADD CONSTRAINT [PK_DBDataIdentifiers]
    PRIMARY KEY CLUSTERED ([DataUniqueID] ASC);
GO

-- Creating primary key on [DataItemID] in table 'DBDataItems'
ALTER TABLE [dbo].[DBDataItems]
ADD CONSTRAINT [PK_DBDataItems]
    PRIMARY KEY CLUSTERED ([DataItemID] ASC);
GO

-- Creating primary key on [ID] in table 'DBDataParameters'
ALTER TABLE [dbo].[DBDataParameters]
ADD CONSTRAINT [PK_DBDataParameters]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [PairID] in table 'ProductKeyIDSerialNumberPairs'
ALTER TABLE [dbo].[ProductKeyIDSerialNumberPairs]
ADD CONSTRAINT [PK_ProductKeyIDSerialNumberPairs]
    PRIMARY KEY CLUSTERED ([PairID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [DataIdentifierDataUniqueID] in table 'DBDataItems'
ALTER TABLE [dbo].[DBDataItems]
ADD CONSTRAINT [FK_DataIdentifierDataItem]
    FOREIGN KEY ([DataIdentifierDataUniqueID])
    REFERENCES [dbo].[DBDataIdentifiers]
        ([DataUniqueID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_DataIdentifierDataItem'
CREATE INDEX [IX_FK_DataIdentifierDataItem]
ON [dbo].[DBDataItems]
    ([DataIdentifierDataUniqueID]);
GO

-- Creating foreign key on [DataItemDataItemID] in table 'DBDataParameters'
ALTER TABLE [dbo].[DBDataParameters]
ADD CONSTRAINT [FK_DataItemDataParameter]
    FOREIGN KEY ([DataItemDataItemID])
    REFERENCES [dbo].[DBDataItems]
        ([DataItemID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_DataItemDataParameter'
CREATE INDEX [IX_FK_DataItemDataParameter]
ON [dbo].[DBDataParameters]
    ([DataItemDataItemID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------