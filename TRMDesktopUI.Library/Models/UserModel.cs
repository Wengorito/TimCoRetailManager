using System.Collections.Generic;
using System.Linq;

namespace TRMDesktopUI.Library.Models
{
    public class UserModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        public Dictionary<string, string> Roles { get; set; } = new Dictionary<string, string>();
        public string RoleList
        {
            get
            {
                //var roles = Roles.Select(x => x.Value);
                var r = Roles.Values.ToList();
                return string.Join(", ", r);
            }
        }
    }
}