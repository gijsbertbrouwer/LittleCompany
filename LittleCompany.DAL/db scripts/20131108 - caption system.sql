CREATE PROCEDURE [dbo].Captions_GetAll

AS
	SELECT id, code, caption, languagecode from Captions  order by languagecode
RETURN 0