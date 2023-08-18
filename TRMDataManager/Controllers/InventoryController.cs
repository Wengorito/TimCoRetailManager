using System.Collections.Generic;
using System.Web.Http;
using TRMDataManager.Library.DataAccess;
using TRMDataManager.Library.Models;

namespace TRMDataManager.Controllers
{
    [Authorize]
    public class InventoryController : ApiController
    {
        public void Post(InventoryModel item)
        {
            InventoryData data = new InventoryData("TRMData");

            data.SaveInventoryRecord(item);
        }

        public List<InventoryModel> Get()
        {
            InventoryData data = new InventoryData("TRMData");
            //TODO: AppSettings for passing the db name

            return data.GetInventory();
        }
    }
}
