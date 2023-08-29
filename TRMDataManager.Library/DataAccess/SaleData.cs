using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using TRMDataManager.Library.Internals.DataAccess;
using TRMDataManager.Library.Models;

namespace TRMDataManager.Library.DataAccess
{
    public class SaleData : ISaleData
    {
        private readonly IConfiguration _config;
        private readonly IProductData _productData;
        private readonly ISqlDataAccess _sql;

        public SaleData(IConfiguration config, IProductData productData, ISqlDataAccess sql)
        {
            _config = config;
            _productData = productData;
            _sql = sql;
        }

        public void SaveSale(SaleModel saleModel, string cashierId)
        {
            // A List storing sought Product Ids (to minimalize DB roundtrips)
            // var ids = sale.SaleDetails.Select(x => x.ProductId).ToList();

            // Not going to work that simply. For further investigation
            // C# _sql Server - Passing a list to a stored procedure
            // https://stackoverflow.com/questions/7097079/c-sharp-_sql-server-passing-a-list-to-a-stored-procedure

            var taxRate = _config.GetSection("Constants:TaxRate").Value;
            bool isValidTaxRate = decimal.TryParse(taxRate, out decimal decimalTaxRate);
            if (!isValidTaxRate)
            {
                throw new ConfigurationErrorsException("The tax rate is not set up properly");
            }

            var products = _productData.GetProducts();
            var details = new List<SaleDetailDBModel>();

            // Create SaleDetails list with available information
            foreach (var item in saleModel.SaleDetails)
            {
                var detail = new SaleDetailDBModel
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                };

                var productInfo = products.SingleOrDefault(x => x.Id == detail.ProductId);

                if (productInfo == null)
                {
                    throw new Exception($"The product Id of {detail.ProductId} could not be found in database.");
                }

                detail.PurchasePrice = item.Quantity * productInfo.RetailPrice;

                if (productInfo.IsTaxable)
                {
                    detail.Tax = detail.PurchasePrice * decimalTaxRate;
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

            try
            {
                _sql.StartTransaction("TRMData");
                _sql.SaveDataInTransaction("spSale_Insert", sale);

                var param1 = new { sale.CashierId, sale.SaleDate };
                int? saleId = _sql.LoadDataInTransaction<int, dynamic>("spSale_Lookup", param1).FirstOrDefault();

                if (saleId == null)
                {
                    throw new Exception($"Sale could not be found in database.");
                }

                // update saledetailsdbmodels saleIds
                foreach (var item in details)
                {
                    item.SaleId = saleId.Value;

                    // save sale details model to db
                    _sql.SaveDataInTransaction("spSaleDetail_Insert", item);

                    var Quantity = 0 - item.Quantity;

                    var param2 = new { item.ProductId, Quantity };
                    _sql.SaveDataInTransaction<dynamic>("spProduct_UpdateStock", param2);

                    // Fix roundtrips with table valued parameter - watch Tims advanced video on dapper
                    // Verify which way is faster
                }

                _sql.CommitTransaction();
            }
            catch
            {
                _sql.RollbackTransaction();
                throw;
            }
        }

        public List<SaleReportModel> GetSaleReport()
        {
            return _sql.LoadData<SaleReportModel, dynamic>("spSale_SaleReport", new { }, "TRMData");
        }
    }
}
