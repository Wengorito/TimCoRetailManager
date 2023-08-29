CREATE PROCEDURE [dbo].[spProduct_UpdateStock]
	@ProductId int,
	@Quantity int
AS
begin
	set nocount on;

	update Product
	set QuantityInStock = (QuantityInStock + @Quantity)
	where Id = @ProductId;
end
