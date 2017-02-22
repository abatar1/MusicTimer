using Plugin.Settings;
using Plugin.Settings.Abstractions;
namespace MusicTimer.Settings
{
    public static class Settings
    {
        private static ISettings AppSettings => CrossSettings.Current;

        #region Setting Constants

        private const string FirstStartKey = "spath_key";
        private static readonly bool FirstStartDefault = true;

        #endregion

        public static bool IsFirstStart
        {
            get
            {
                return AppSettings.GetValueOrDefault(FirstStartKey, FirstStartDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(FirstStartKey, value);
            }
        }
    }
}