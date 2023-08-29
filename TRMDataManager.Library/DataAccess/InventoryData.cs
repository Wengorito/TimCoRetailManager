using System.Collections.Generic;
using TRMDataManager.Library.Internals.DataAccess;
using TRMDataManager.Library.Models;

namespace TRMDataManager.Library.DataAccess
{
    public class InventoryData : IInventoryData
    {
        private readonly ISqlDataAccess _sql;

        public InventoryData(ISqlDataAccess sql)
        {
            _sql = sql;
        }

        public List<InventoryModel> GetInventory()
        {
            var output = _sql.LoadData<InventoryModel, dynamic>("spInventory_GetAll", new { }, "TRMData");

            return output;
        }

        public void SaveInventoryRecord(InventoryModel model)
        {
            try
            {
                _sql.StartTransaction("TRMData");
                _sql.SaveDataInTransaction("spInventory_Insert", model);

                var p = new { model.ProductId, model.Quantity };
                _sql.SaveDataInTransaction<dynamic>("spProduct_UpdateStock", p);

                _sql.CommitTransaction();
            }
            catch
            {
                _sql.RollbackTransaction();
                throw;
            }
        }
    }
}
