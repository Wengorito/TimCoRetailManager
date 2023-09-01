using System.Threading.Tasks;
using System.Web.Mvc;
using TRMDesktopUI.Library.Api;
using TRMDesktopUI.Library.Models;
using TRMWebUI_NET4._7._2.Models;

namespace TRMWebUI_NET4._7._2.Controllers
{
    public class AccountController : Controller
    {
        private readonly IApiHelper _apiHelper;
        private readonly ILoggedInUserModel _loggedInUserModel;

        public AccountController(IApiHelper apiHelper, ILoggedInUserModel loggedInUserModel)
        {
            _apiHelper = apiHelper;
            _loggedInUserModel = loggedInUserModel;
        }

        // GET: ACcount
        public ActionResult Index()
        {
            return View();
        }

        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _apiHelper.Authenticate(model.Email, model.Password);
            await _apiHelper.GetLoggedInUserInfo(result.Access_Token);

            if (string.IsNullOrEmpty(_loggedInUserModel.Token))
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View(model);
            }

            return RedirectToAction("Index", "Product");


            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            //var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            //switch (result)
            //{
            //    case SignInStatus.Success:
            //        return RedirectToLocal(returnUrl);
            //    case SignInStatus.LockedOut:
            //        return View("Lockout");
            //    case SignInStatus.RequiresVerification:
            //        return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
            //    case SignInStatus.Failure:
            //    default:
            //        ModelState.AddModelError("", "Invalid login attempt.");
            //        return View(model);
            //}
        }
    }
}