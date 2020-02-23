using System.Collections.Generic;

namespace XamWalletWebApi.Models
{
    public class UserModel
    {
        public string Email { get;  set; }
        public bool EmailConfirmed { get;  set; }
        public bool LockoutEnabled { get;  set; }
        public IList<string> Roles { get;  set; }
    }
}
