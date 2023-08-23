using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using TRMDataManager.Library.Internals.DataAccess;
using TRMDataManager.Library.Models;

namespace TRMDataManager.Library.DataAccess
{
    public class ProductData
    {
        private readonly string _connectionStringName;
        private readonly IConfiguration _config;

        public ProductData(string connectionStringName, IConfiguration config)
        {
            _connectionStringName = connectionStringName;
            _config = config;
        }

        public List<ProductModel> GetProducts()
        {
            SqlDataAccess sql = new SqlDataAccess(_config);

            var output = sql.LoadData<ProductModel, dynamic>("spProduct_GetAll", new { }, _connectionStringName);

            return output;
        }
    }
}
