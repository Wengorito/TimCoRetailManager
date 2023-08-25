using System.Collections.Generic;
using TRMDataManager.Library.Models;

namespace TRMDataManager.Library.DataAccess
{
    public interface IUserData
    {
        List<UserModel> GetAll();
        List<UserModel> GetUserById(string Id);
    }
}