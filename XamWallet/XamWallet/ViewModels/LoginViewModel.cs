using System;
using System.ComponentModel.DataAnnotations;

namespace XamWallet.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        [Required]
        [EmailAddress]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }



        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public LoginViewModel()
        {


        }
    }
}
