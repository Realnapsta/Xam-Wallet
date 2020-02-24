using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace XamWallet.Helpers
{
    /// <summary>
    /// This is the Settings static class that can be used in your Core solution or in any
    /// of your client applications. All settings are laid out the same exact way with getters
    /// and setters. 
    /// </summary>
    public static class Settings
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        #region Setting Constants

        private const string LastUserNameKey = "last_username_key";
        private static readonly string SettingsDefault = string.Empty;



        #endregion


        public static string LastUserName
        {
            get
            {
                return AppSettings.GetValueOrDefault(LastUserNameKey, SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(LastUserNameKey, value);
            }
        }


    }
}
