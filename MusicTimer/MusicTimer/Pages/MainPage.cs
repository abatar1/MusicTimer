using System;
using System.Linq;
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
        private StorageController _storageController;
        private SelectMultipleBasePage _multiPage;
        private SettingsPage _settingsPage;
              
        private static CustomPicker InitRangePicker(string title, int start, int count)
        {
            var picker = new CustomPicker
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
            };
            foreach (var min in Enumerable.Range(start, count).Select(m => m.ToString()))
            {
                picker.Items.Add(min);
            }
            return picker;
        }

        private static Label InitPlayerPageLabel(string text)
        {
            return new Label
            {
                Text = text,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center
            };
        }

        private async void OnProcessButtonClick(object sender, EventArgs eventArgs)
        {

        }

        public MainPage(StorageController storageController)
        {
            _storageController = storageController;
            _palletizer = new Palletizer();

            var mLabel = InitPlayerPageLabel("Minutes");
            var hLabel = InitPlayerPageLabel("Hours");

            var mPicker = InitRangePicker("Minutes", 1, Mih);
            var hPicker = InitRangePicker("Hours", 1, Hid);

            var tagButton = new Button
            {
                Text = "Select tags",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };
            tagButton.Clicked += async (sender, e) =>
            {
                if (_multiPage == null)
                {
                    _multiPage = new SelectMultipleBasePage(_palletizer.Tags) { Title = "Tags" };
                }
                await Navigation.PushAsync(_multiPage);
            };

            var processButton = new Button
            {
                Text = "Start",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.End
            };
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
