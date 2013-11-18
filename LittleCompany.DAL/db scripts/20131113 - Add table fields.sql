
-- add fields to organisation
CREATE TABLE [dbo].[Organisation] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [CustomerId] INT            NOT NULL,
	[Name]       NVARCHAR (250) NULL,
    [Phonenumber] NVARCHAR(50) NULL, 
    [Emailaddress] NVARCHAR(250) NULL, 
    [Notes] NVARCHAR(MAX) NULL, 
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_organisation_To_Customer] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customer] ([Id])
);

