using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using TRMDataManager.Library.DataAccess;
using TRMDataManager.Library.Models;

namespace TRMApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SaleController : ControllerBase
    {
        [Authorize(Roles = "Cashier")]
        public void Post(SaleModel sale)
        {
            SaleData data = new SaleData("TRMData");
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); //User.FindFirstValue(ClaimTypes.NameIdentifier);

            data.SaveSale(sale, userId);
        }

        [Authorize(Roles = "Admin,Manager")]
        [Route("api/GetSaleReport")]
        public List<SaleReportModel> GetSaleReport()
        {
            //if (RequestContext.Principal.IsInRole("Admin"))
            //{
            //    // Do Admin stuff
            //}
            //else if (RequestContext.Principal.IsInRole("Manager"))
            //{
            //    // Do Managerial stufff
            //}

            SaleData data = new SaleData("TRMData");
            //TODO: AppSettings for passing the db name

            return data.GetSaleReport();
        }
    }
}
