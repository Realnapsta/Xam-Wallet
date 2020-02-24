using System;
using Plugin.Connectivity;

namespace XamWallet.Helpers
{
    public class NetworkCheck
    {
        public static bool IsInternet()
        {
            if (CrossConnectivity.Current.IsConnected)
                return true;
            else
                return false;
            
        }

    }
}
