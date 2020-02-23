using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamWallet.Models;
using XamWallet.Views.Main;
using XamWallet.Views.SignIn;

namespace XamWallet
{
    public partial class App : Application
    {
        public static string DatabaseLocation = string.Empty;
        public static User user = new User();

        public App()
        {
            InitializeComponent();

            MainPage = new MainShell();
        }

        public App(string databaseLocation)
        {
            InitializeComponent();

            MainPage = new SignInShell();
            DatabaseLocation = databaseLocation;
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
