CREATE PROCEDURE [dbo].[spUserLookup]
	@Id nvarchar(128)
AS
begin
	select FirstName, LastName, EmailAddress, CreatedDate
	from [User]
	where Id = @Id;
end

RETURN 0
