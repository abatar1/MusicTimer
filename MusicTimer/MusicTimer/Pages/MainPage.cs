using System;
using MusicTimer.Domain;
using MusicTimer.Domain.Files;
using MusicTimer.Infrastructure;
using Xamarin.Forms;

namespace MusicTimer.Pages
{
    public class MainPage : ContentPage
    {
        private const int Hid = 24;
        private const int Mih = 60;

        private Palletizer _palletizer;
        public StorageController StorageController;
        private SelectMultipleTagPage _multiPage;
        private SettingsPage _settingsPage;              

        private async void OnProcessButtonClick(object sender, EventArgs eventArgs)
        {

        }

        public MainPage(StorageController storageController)
        {
            StorageController = storageController;
            _palletizer = new Palletizer();

            var mLabel = GuiHelper.CreatePlayerPageLabel("Minutes");
            var hLabel = GuiHelper.CreatePlayerPageLabel("Hours");

            var mPicker = GuiHelper.CreateRangePicker("Minutes", 1, Mih);
            var hPicker = GuiHelper.CreateRangePicker("Hours", 1, Hid);

            var tagButton = GuiHelper.CreateButton("Add tags", LayoutOptions.Center, LayoutOptions.Center);
            tagButton.Clicked += async (sender, e) =>
            {
                if (_multiPage == null)
                {
                    _multiPage = new SelectMultipleTagPage(_palletizer.Tags) { Title = "Tags" };
                }
                await Navigation.PushAsync(_multiPage);
            };

            var processButton = GuiHelper.CreateButton("Process", LayoutOptions.Center, LayoutOptions.End);
            processButton.Clicked += OnProcessButtonClick;

            ToolbarItems.Add(new ToolbarItem("Settings", null,
                async () =>
                {
                    if (_settingsPage == null) _settingsPage = new SettingsPage { Title = "Settings" };
                    await Navigation.PushAsync(_settingsPage);
                }, 
                ToolbarItemOrder.Primary));

            Content = new Grid
            {
                Children =
                {
                    new StackLayout
                    {
                         Children =
                         {
                            new StackLayout
                            {
                                Children =
                                {
                                    hLabel,
                                    hPicker
                                },
                                Orientation = StackOrientation.Vertical,
                                VerticalOptions = LayoutOptions.Center
                            },
                            new StackLayout
                            {
                                Children =
                                {
                                    mLabel,
                                    mPicker
                                },
                                Orientation = StackOrientation.Vertical,
                                VerticalOptions = LayoutOptions.Center
                            }
                         },
                         Orientation = StackOrientation.Horizontal,
                         HorizontalOptions = LayoutOptions.Center,
                         VerticalOptions = LayoutOptions.Start
                    },
                    tagButton,
                    processButton
                },
            };            
        }
    }
}
