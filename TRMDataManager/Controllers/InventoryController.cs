using System.Collections.Generic;
using System.Web.Http;
using TRMDataManager.Library.DataAccess;
using TRMDataManager.Library.Models;

namespace TRMDataManager.Controllers
{
    [Authorize]
    public class InventoryController : ApiController
    {
        [Authorize(Roles = "Admin")]
        public void Post(InventoryModel item)
        {
            InventoryData data = new InventoryData("TRMData");

            data.SaveInventoryRecord(item);
        }

        [Authorize(Roles = "Admin,Manager")]
        public List<InventoryModel> Get()
        {
            InventoryData data = new InventoryData("TRMData");
            //TODO: AppSettings for passing the db name

            return data.GetInventory();
        }
    }
}
