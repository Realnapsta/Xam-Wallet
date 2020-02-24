using System;
using System.Collections.Generic;

using Xamarin.Forms;
using XamWallet.Views.SignIn;

namespace XamWallet.Views.Main
{
    public partial class AccountPage : ContentPage
    {
        public AccountPage()
        {
            InitializeComponent();
        }


        public  void Logoutbtn_Clicked(object sender,EventArgs e)
        {
              App.user = null;
             Application.Current.MainPage = new SignInShell();
        }
    }
}
