using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Web.Http;
using TRMDataManager.Library.DataAccess;
using TRMDataManager.Library.Models;

namespace TRMDataManager.Controllers
{
    [Authorize]
    public class SaleController : ApiController
    {
        [Authorize(Roles = "Cashier")]
        public void Post(SaleModel sale)
        {
            SaleData data = new SaleData("TRMData");
            var userId = RequestContext.Principal.Identity.GetUserId();

            data.SaveSale(sale, userId);
        }

        [Authorize(Roles = "Admin,Manager")]
        [Route("api/GetSaleReport")]
        public List<SaleReportModel> GetSaleReport()
        {
            if (RequestContext.Principal.IsInRole("admin"))
            {
                // Do Admin stuff
            }
            else if (RequestContext.Principal.IsInRole("Manager"))
            {
                // Do Managerial stufff
            }

            SaleData data = new SaleData("TRMData");
            //TODO: AppSettings for passing the db name

            return data.GetSaleReport();
        }
    }
}
