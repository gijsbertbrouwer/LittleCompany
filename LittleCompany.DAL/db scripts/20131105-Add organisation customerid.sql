CREATE TABLE [dbo].[Organisation] (
    [Id]   INT            IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (250) NULL,
    [CustomerId] INT NOT NULL, 
    PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_organisation_To_Customer] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customer] ([Id]),
);


CREATE TABLE [dbo].[DataTypes] (
    [Id]               INT NOT NULL,
    [TableName]        NVARCHAR (250) NOT NULL,
    [TableNameCaption] NVARCHAR (250) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

