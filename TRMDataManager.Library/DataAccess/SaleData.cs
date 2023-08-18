using System;
using System.Collections.Generic;
using System.Linq;
using TRMDataManager.Library.Internals.DataAccess;
using TRMDataManager.Library.Models;

namespace TRMDataManager.Library.DataAccess
{
    public class SaleData
    {
        private readonly string _connectionStringName;

        public SaleData(string connectionStringName)
        {
            _connectionStringName = connectionStringName;
        }

        public void SaveSale(SaleModel saleModel, string cashierId)
        {
            // A List storing sought Product Ids (to minimalize DB roundtrips)
            // var ids = sale.SaleDetails.Select(x => x.ProductId).ToList();

            // Not going to work that simply. For further investigation
            // C# SQL Server - Passing a list to a stored procedure
            // https://stackoverflow.com/questions/7097079/c-sharp-sql-server-passing-a-list-to-a-stored-procedure

            var taxRate = ConfigHelper.GetTaxRate();
            var productData = new ProductData(_connectionStringName);
            var products = productData.GetProducts();
            var details = new List<SaleDetailDBModel>();

            // Create SaleDetails list with available information
            foreach (var item in saleModel.SaleDetails)
            {
                var detail = new SaleDetailDBModel
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    //PurchasePrice = products
                    //    .FirstOrDefault(x => x.Id == item.ProductId)
                    //    .RetailPrice * item.Quantity,
                    //Tax = products
                    //    .FirstOrDefault(x => x.Id == item.ProductId)
                    //    .RetailPrice * item.Quantity * ConfigHelper.GetTaxRate()
                };

                var productInfo = products.SingleOrDefault(x => x.Id == detail.ProductId);

                if (productInfo == null)
                {
                    throw new Exception($"The product Id of {detail.ProductId} could not be found in database.");
                }

                detail.PurchasePrice = item.Quantity * productInfo.RetailPrice;

                if (productInfo.IsTaxable)
                {
                    detail.Tax = detail.PurchasePrice * taxRate;
                }

                details.Add(detail);
            }

            // Create Sale with calculated sums
            var sale = new SaleDBModel
            {
                CashierId = cashierId,
                SubTotal = details.Sum(x => x.PurchasePrice),
                Tax = details.Sum(x => x.Tax),
            };

            sale.Total = sale.SubTotal + sale.Tax;

            using (var sql = new SqlDataAccess())
            {
                try
                {
                    sql.StartTransaction(_connectionStringName);
                    sql.SaveDataInTransaction("spSale_Insert", sale);

                    var p = new { sale.CashierId, sale.SaleDate };
                    int? saleId = sql.LoadDataInTransaction<int, dynamic>("spSale_Lookup", p).FirstOrDefault();

                    if (saleId == null)
                    {
                        throw new Exception($"Sale could not be found in database.");
                    }

                    // update saledetailsdbmodels saleIds
                    foreach (var item in details)
                    {
                        item.SaleId = saleId.Value;

                        // save sale details model to db
                        sql.SaveDataInTransaction("spSaleDetail_Insert", item);

                        // Fix roundtrips with table valued parameter - watch Tims advanced video on dapper
                        // Verify which way is faster
                    }

                    sql.CommitTransaction();
                }
                catch
                {
                    sql.RollbackTransaction();
                    throw;
                }
            }
        }

        public List<SaleReportModel> GetSaleReport()
        {
            var sql = new SqlDataAccess();

            return sql.LoadData<SaleReportModel, dynamic>("spSale_SaleReport", new { }, _connectionStringName);
        }
    }
}
