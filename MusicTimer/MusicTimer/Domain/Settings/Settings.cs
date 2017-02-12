using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace MusicTimer.Settings
{
    public static class Settings
    {
        private static ISettings AppSettings => CrossSettings.Current;

        #region Setting Constants

        private const string SPathKey = "spath_key";
        private static readonly string SPathDefault = string.Empty;

        #endregion

        public static string GeneralSettings
        {
            get
            {
                return AppSettings.GetValueOrDefault(SPathKey, SPathDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(SPathKey, value);
            }
        }
    }
}