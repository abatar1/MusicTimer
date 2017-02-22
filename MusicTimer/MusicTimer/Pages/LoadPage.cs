using Xamarin.Forms;

namespace MusicTimer.Pages
{
    public class LoadPage : ContentPage
    {
        public LoadPage()
        {
            
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (Settings.Settings.IsFirstStart)
            {
                await Navigation.PushModalAsync(new FirstStartPage());
            }
            else
            {
                await Navigation.PushModalAsync(new MainPage());
            }
        }
    }
}
