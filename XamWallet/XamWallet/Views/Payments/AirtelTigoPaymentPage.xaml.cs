using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace XamWallet.Views.Payments
{
    public partial class AirtelTigoPaymentPage : PopupPage
    {
        public AirtelTigoPaymentPage()
        {
            InitializeComponent();
            this.BindingContext = this;

            this.IsBusy = false;
        }

        public void CancelBtn_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync(true);
        }

        public void submitBtn_Clicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
