using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamWallet.Views.SignIn;

namespace XamWallet
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new SignInShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
