using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Investments.API.Identity
{
    public class RegisterUserViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }

    public class UserTokenViewModel
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public IEnumerable<ClaimsViewModel> Claims { get; set; }
    }

    public class ClaimsViewModel
    {
        public string Type { get; set; }
        public string Value { get; set; }
    }

    public class LoginViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class TokenResponseViewModel
    {
        public string AccessToken { get; set; }
        public double ExpiratioIn { get; set; }
        public UserTokenViewModel UserToken { get; set; }
    }


}