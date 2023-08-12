using System;

namespace TRMDesktopUI.Library.Models
{
    public interface ILoggedInUserModel
    {
        string EmailAddress { get; set; }
        string FirstName { get; set; }
        int Id { get; set; }
        string LastName { get; set; }
        string Token { get; set; }
        DateTime CreatedDate { get; set; }
    }
}