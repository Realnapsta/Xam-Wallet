using System;
using System.Collections.Generic;
using System.Linq;
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
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();



            this.IsBusy = false;
        }


        async void Loginbtn_Clicked(object sender, System.EventArgs e)
        {


            this.IsBusy = true;
            bool isPrimarykeyEmpty = string.IsNullOrEmpty(primaryKey.Text);
            bool isPasswordEmpty = string.IsNullOrEmpty(password.Text);
            loginbtn.IsEnabled = false;

            var db = new SQLiteConnection(App.DatabaseLocation);

            try
            {
                if (isPasswordEmpty || isPrimarykeyEmpty)
                {
                    await DisplayAlert("Failed", "Please fill all entries", "try again");
                }
                else
                {

                    var user = db.Table<User>().Where(u => u.Email == primaryKey.Text || u.PhoneNumber == primaryKey.Text && u.Password == password.Text).FirstOrDefault();
                    db.Close();
                    // var user = (await App.MobileService.GetTable<PrimaryUser>()
                    //           .Where(u => u.UserName == primaryKey.Text || u.Phonenumber == primaryKey.Text)
                    //          .ToListAsync()).FirstOrDefault();

               

                    if (user != null)
                    {
                        if (user.Password == password.Text)
                        {
                          //  App.User = user;
                            Settings.LastUserName = user.Email;
                            Application.Current.MainPage = new MainShell();


                        }

                        else
                        {
                            await DisplayAlert("ERROR", "Username, Phonenumber or Password incorrect", "try again");
                            IsBusy = false;
                            loginbtn.IsEnabled = true;
                        }
                    }
                    else
                    {
                        await DisplayAlert("ERROR", "There was a problem logging in", "try again");
                        IsBusy = false;
                        loginbtn.IsEnabled = true;
                    }


                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("FAILED", "Something went wrong, please check your details and try again", "OK");
                string error_text = ex.ToString();
            }
            this.IsBusy = false;
            loginbtn.IsEnabled = true;

        }

       
    }

    }
