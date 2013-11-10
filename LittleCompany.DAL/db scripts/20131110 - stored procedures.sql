CREATE PROCEDURE [dbo].File_Get
	@fileid int,
	@customerid int
AS
	SELECT id, name, organisationid, personid, customerid, [path], dateupload, [version], [guid], [password]
	From Files
	where CustomerId = @customerid and id = @fileid
RETURN 0