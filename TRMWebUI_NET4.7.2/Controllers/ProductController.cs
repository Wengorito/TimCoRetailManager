using AutoMapper;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using TRMDesktopUI.Library.Api;
using TRMDesktopUI.Library.Models;
using TRMWebUI_NET4._7._2.Models;

namespace TRMWebUI_NET4._7._2.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductEndpoint _productEndpoint;
        private readonly ILoggedInUserModel _loggedInUserModel;

        // ninject

        public ProductController(IProductEndpoint productEndpoint, ILoggedInUserModel loggedInUserModel)
        {
            _productEndpoint = productEndpoint;
            _loggedInUserModel = loggedInUserModel;
        }

        // GET: Product
        public async Task<ActionResult> Index()
        {
            if (string.IsNullOrEmpty(_loggedInUserModel.Token))
            {
                return RedirectToAction("Login", "Account");
            }

            var products = await _productEndpoint.GetAll();

            //foreach (var product in products)
            //{
            //    product.RetailPrice = (decimal)product.RetailPrice;
            //}

            //var dec = products.FirstOrDefault().RetailPrice;

            return View(products.Select(Mapper.Map<ProductModel, ProductViewModel>));
        }

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Product/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
