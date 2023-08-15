﻿CREATE PROCEDURE [dbo].[spSale_Insert]
	@CashierId nvarchar(128),
	@SaleDate datetime2(7),
	@SubTotal money,
	@Tax money,
	@Total money
AS
begin
set nocount on;

	insert into Sale (CashierId, SaleDate, SubTotal, Tax, Total)
	values (@CashierId, @SaleDate, @SubTotal, @Tax, @Total);
end
