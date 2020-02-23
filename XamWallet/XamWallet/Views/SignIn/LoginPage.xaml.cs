using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamWallet.Views.Main;

namespace XamWallet.Views.SignIn
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }


        void Loginbtn_Clicked(object sender, System.EventArgs e)
        {
            Application.Current.MainPage = new MainShell();
        }

       public async void registerbtn_clicked(object sender, System.EventArgs e)
        {
           

          await Navigation.PushAsync(new RegisterPage());
        }
    }
}