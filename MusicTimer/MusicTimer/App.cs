using MusicTimer.Domain.Files;
using MusicTimer.Pages;
using Xamarin.Forms;

namespace MusicTimer
{
    public class App : Application
    {
        public App()
        {
            var storageController = new StorageController();
            storageController.Load();
            MainPage = new NavigationPage(new MainPage(storageController));
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
