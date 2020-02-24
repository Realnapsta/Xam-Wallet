using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamWallet.Helpers;
using XamWallet.Models;
using XamWallet.ViewModels;
using XamWallet.Views.Main;

namespace XamWallet.Views.SignIn
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();

            this.BindingContext = this;

            this.IsBusy = false;
        }

        public async void RegisterUserHttp(User user)
        {
            if (!NetworkCheck.IsInternet())
                await DisplayAlert("NO Internet", "No network is available please check you internet connection and try again", "OK");
            else
            {
                var client = new HttpClient();
               // var response = await client.PostAsync(" ",user);
            }
        }


        async void Registerbtn_Clicked(object sender, System.EventArgs e)
        {
            var db = new SQLiteConnection(App.DatabaseLocation);
            db.CreateTable<User>();

            this.IsBusy = true;
            bool isFirstnameEmpty = string.IsNullOrEmpty(firstName.Text);
            bool isLastnameEmpty = string.IsNullOrEmpty(lastName.Text);
            bool isPhoneNumberEmpty = string.IsNullOrEmpty(phoneNumber.Text);
            bool isPasswordEmpty = string.IsNullOrEmpty(password.Text);
            bool isComfirmPasswordEmpty = string.IsNullOrEmpty(confirmPassword.Text);
            registerbtn.IsEnabled = false;

            try
            {
                if (isFirstnameEmpty || isLastnameEmpty || isPhoneNumberEmpty || isPasswordEmpty || isComfirmPasswordEmpty)
                {
                    await DisplayAlert("Failed", "please fill all entries", "Try again");
                    registerbtn.IsEnabled = true;
                }
                else
                {
                    if (confirmPassword.Text != password.Text)
                    {
                        await DisplayAlert("Failed", "password do not match", "try Again");
                        registerbtn.IsEnabled = true;

                    }
                    else
                    {
                        User newuser = new User()
                        {
                            UserName = email.Text,
                            Email =email.Text,
                            PhoneNumber = phoneNumber.Text,
                            Password = password.Text,

                        };

                        db.Insert(newuser);
                        db.Close();

                        App.user = newuser;
                      //  await App.MobileService.GetTable<SecondaryUser>().InsertAsync(user);

                        await DisplayAlert("Success", "Account has sucessfully been created", "OK");

                        Settings.LastUserName = newuser.UserName;

                        Application.Current.MainPage = new MainShell();

                         registerbtn.IsEnabled = true;

                    }

                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("ERROR", "Something went wrong", "Cancel", "try again");
                string error_text = ex.ToString();
                registerbtn.IsEnabled = true;

            }

            this.IsBusy = false;
            registerbtn.IsEnabled = true;

        }
    }
}