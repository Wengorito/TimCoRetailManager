CREATE PROCEDURE [dbo].[spSale_Lookup]
	@CashierId nvarchar(128),
	@SaleDate datetime2(7)
AS
begin
	set nocount on;

	select Id
	from Sale
	where CashierId = @CashierId and SaleDate = @SaleDate;
end
