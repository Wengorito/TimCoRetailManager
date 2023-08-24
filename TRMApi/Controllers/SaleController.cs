﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
        private readonly IConfiguration _config;

        public SaleController(IConfiguration config)
        {
            _config = config;
        }

        [Authorize(Roles = "Cashier")]
        [HttpPost]
        public void Post(SaleModel sale)
        {
            SaleData data = new SaleData("TRMData", _config);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); //User.FindFirstValue(ClaimTypes.NameIdentifier);

            data.SaveSale(sale, userId);
        }

        [Authorize(Roles = "Admin,Manager")]
        [Route("/GetSaleReport")]
        [HttpGet]
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

            SaleData data = new SaleData("TRMData", _config);
            //TODO: AppSettings for passing the db name

            return data.GetSaleReport();
        }
    }
}
