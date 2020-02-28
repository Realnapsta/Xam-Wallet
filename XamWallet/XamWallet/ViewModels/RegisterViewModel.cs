using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using XamWallet.Services;

namespace XamWallet.ViewModels
{
    class RegisterViewModel
    {
        IdentityRequest identity = new IdentityRequest();


        public string UserName { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        [Required]
        [EmailAddress]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        public string Message;



        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

      public ICommand RegisterCommand
        {
            get
            {
                return new Command(async() =>
                    {
                     var isSuccess = await identity.RegisterUserAsync(Email, Password, ConfirmPassword, PhoneNumber, FirstName, LastName, UserName);
                        if (isSuccess)
                            Message = "Registered Sucessfully";
                        else
                            Message = "Something Went Wrong";
                });
            }
        }
            
    }
}
