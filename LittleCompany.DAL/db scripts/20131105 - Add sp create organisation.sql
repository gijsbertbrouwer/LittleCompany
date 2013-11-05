CREATE PROCEDURE [dbo].Organisation_Create
	@name nvarchar(250)
AS
	insert into Organisation (Name)Values(@name)
	select @@identity
