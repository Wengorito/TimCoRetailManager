CREATE PROCEDURE [dbo].[spUser_GetAll]
AS
begin
	set nocount on;

	select FirstName, LastName, EmailAddress, CreatedDate
	from [User]
end
