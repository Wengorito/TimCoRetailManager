using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TRMDataManager.Library.DataAccess;
using TRMDataManager.Library.Models;

namespace TRMApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InventoryController : ControllerBase
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
