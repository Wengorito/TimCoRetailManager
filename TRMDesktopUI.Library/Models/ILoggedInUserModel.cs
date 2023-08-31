﻿using System;

namespace TRMDesktopUI.Library.Models
{
    public interface ILoggedInUserModel
    {
        string Id { get; set; }
        string EmailAddress { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string Token { get; set; }
        DateTime CreatedDate { get; set; }

        void ResetUserModel();
    }
}