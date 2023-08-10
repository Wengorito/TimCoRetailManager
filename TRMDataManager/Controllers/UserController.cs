using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Web.Http;
using TRMDataManager.Library.DataAccess;
using TRMDataManager.Library.Models;

namespace TRMDataManager.Controllers
{
    [Authorize]
    public class UserController : ApiController
    {
        public List<UserModel> GetById()
        {
            UserData data = new UserData("TRMData");

            var userId = RequestContext.Principal.Identity.GetUserId();

            var output = data.GetUserById(userId);

            return output;
        }
    }
}
