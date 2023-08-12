using System.Collections.Generic;
using TRMDataManager.Library.Internals.DataAccess;
using TRMDataManager.Library.Models;

namespace TRMDataManager.Library.DataAccess
{
    public class ProductData
    {
        private readonly string _connectionStringName;

        public ProductData(string connectionStringName)
        {
            _connectionStringName = connectionStringName;
        }

        public List<ProductModel> GetProducts()
        {
            SqlDataAccess sql = new SqlDataAccess();

            var output = sql.LoadData<ProductModel, dynamic>("spProduct_GetAll", new { }, _connectionStringName);

            return output;
        }
    }
}
