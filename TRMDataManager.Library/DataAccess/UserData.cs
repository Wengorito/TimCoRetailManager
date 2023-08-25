using System.Collections.Generic;
using TRMDataManager.Library.Internals.DataAccess;
using TRMDataManager.Library.Models;

namespace TRMDataManager.Library.DataAccess
{
    public class UserData : IUserData
    {
        private readonly ISqlDataAccess _sql;

        public UserData(ISqlDataAccess sql)
        {
            _sql = sql;
        }

        public List<UserModel> GetUserById(string Id)
        {
            var output = _sql.LoadData<UserModel, dynamic>("spUserLookup", new { Id }, "TRMData");

            return output;
        }

        public List<UserModel> GetAll()
        {
            var output = _sql.LoadData<UserModel, dynamic>("spUser_GetAll", new { }, "TRMData");

            return output;
        }
    }
}
