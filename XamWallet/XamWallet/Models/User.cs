using System;
using System.Collections.Generic;
using System.Text;

namespace XamWallet.Models
{
    class User
    {
        public string userName { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public string password { get; set; }
        public string confirmPassword { get; set; }
    }
}
