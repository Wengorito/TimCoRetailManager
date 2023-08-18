using System.Collections.Generic;
using TRMDataManager.Library.Internals.DataAccess;
using TRMDataManager.Library.Models;

namespace TRMDataManager.Library.DataAccess
{
    public class InventoryData
    {

        private readonly string _connectionStringName;

        public InventoryData(string connectionStringName)
        {
            _connectionStringName = connectionStringName;
        }

        public List<InventoryModel> GetInventory()
        {
            SqlDataAccess sql = new SqlDataAccess();

            var output = sql.LoadData<InventoryModel, dynamic>("spInventory_GetAll", new { }, _connectionStringName);

            return output;
        }

        public void SaveInventoryRecord(InventoryModel item)
        {
            SqlDataAccess sql = new SqlDataAccess();

            sql.SaveData("spInventory_Insert", item, _connectionStringName);
        }
    }
}
