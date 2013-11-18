

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





CREATE PROCEDURE [dbo].Organisation_Create
	@name nvarchar(250),
	@customerid int
AS
	insert into Organisation (Name, CustomerId)Values(@name, @customerid)
	select @@identity










CREATE PROCEDURE [dbo].Person_Create
	@customerid int = 0,
	@organisationid int = null,
	@name nvarchar(250)
AS
	insert into Person (OrganisationId, CustomerId, Name)Values(
	@organisationid,
	@customerid,
	@name)







CREATE PROCEDURE [dbo].Search_QuickAccess_Person
	@query nvarchar(250),
	@customerid int

AS
	SELECT top(10) Id, Name, 3 as datatypeId from Person 
	where  
	CustomerId = @customerid and
	Name like '%' +  @query + '%'



	CREATE PROCEDURE [dbo].Search_QuickAccess_File
	@query nvarchar(250),
	@customerid int

AS
	SELECT top(10) Id, Name, 1 as datatypeId from Files
	where  
	CustomerId = @customerid and
	Name like '%' +  @query + '%'






CREATE PROCEDURE [dbo].Search_QuickAccess_Organisation
	@query nvarchar(250),
	@customerid int

AS
	SELECT top(10) Id, Name, 2 as datatypeId from Organisation 
	where  
	CustomerId = @customerid and
	Name like '%' +  @query + '%'

CREATE PROCEDURE [dbo].Search_QuickAccess_Main
	@query nvarchar(250),
	@customerid int,
	@datatypeid int
AS
	
-- Create a temp table for storing the results
CREATE TABLE #sp_SearchQuick_Items
( 
    Id int, 
    name nvarchar(250) NULL,
	datatypeId int
) 



 if @datatypeid is null or @datatypeid < 1 or @datatypeid = 2
 BEGIN
	INSERT #sp_SearchQuick_Items EXEC Search_QuickAccess_File @query, @customerid
 END 


 if @datatypeid is null or @datatypeid < 1 or @datatypeid = 2
 BEGIN
	INSERT #sp_SearchQuick_Items EXEC Search_QuickAccess_Organisation @query, @customerid
 END 

 if @datatypeid is null or @datatypeid < 1 or @datatypeid = 3
 BEGIN
	INSERT #sp_SearchQuick_Items EXEC Search_QuickAccess_Person @query, @customerid
 END 






SELECT top(10) Id, Name, datatypeId FROM #sp_SearchQuick_Items  order by name

 

 -- Drop temporary tables
DROP TABLE #sp_SearchQuick_Items





CREATE PROCEDURE Security_Authenticate
	@token nvarchar(250)

AS


	SELECT loginid, customerid  from securitytokens where token  = @token and expirationDate > getdate()