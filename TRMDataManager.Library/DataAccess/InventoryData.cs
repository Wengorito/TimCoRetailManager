using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using TRMDataManager.Library.Internals.DataAccess;
using TRMDataManager.Library.Models;

namespace TRMDataManager.Library.DataAccess
{
    public class InventoryData
    {

        private readonly string _connectionStringName;
        private readonly IConfiguration _config;

        public InventoryData(string connectionStringName, IConfiguration config)
        {
            _connectionStringName = connectionStringName;
            _config = config;
        }

        public List<InventoryModel> GetInventory()
        {
            SqlDataAccess sql = new SqlDataAccess(_config);

            var output = sql.LoadData<InventoryModel, dynamic>("spInventory_GetAll", new { }, _connectionStringName);

            return output;
        }

        public void SaveInventoryRecord(InventoryModel item)
        {
            SqlDataAccess sql = new SqlDataAccess(_config);

            sql.SaveData("spInventory_Insert", item, _connectionStringName);
        }
    }
}
