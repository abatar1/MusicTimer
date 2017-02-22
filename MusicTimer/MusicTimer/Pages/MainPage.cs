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
        private static readonly int _hid = 24;
        private static readonly int _mih = 60;
        private static readonly int _pageSpacing = 10;

        private Palletizer _palletizer;
        private SelectMultipleBasePage<Tag> _multiPage;

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

        private async void OnSettingsButtonClick()
        {

        }

        public MainPage()
        {
            _palletizer = new Palletizer();          
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var mLabel = InitPlayerPageLabel("Minutes");
            var hLabel = InitPlayerPageLabel("Hours");

            var mPicker = InitRangePicker("Minutes", 1, _mih);
            var hPicker = InitRangePicker("Hours", 1, _hid);

            var tagButton = new Button
            {
                Text = "Select tags",
                HorizontalOptions = LayoutOptions.Center
            };
            tagButton.Clicked += async (sender, e) =>
            {
                if (_multiPage == null) _multiPage = new SelectMultipleBasePage<Tag>(_palletizer.Tags) { Title = "Tags" };
                await Navigation.PushAsync(_multiPage);
            };

            var processButton = new Button
            {
                Text = "Start",
                HorizontalOptions = LayoutOptions.Center
            };
            processButton.Clicked += OnProcessButtonClick;

            Content = new StackLayout
            {
                Children =
                {
                    new BoxView
                    {
                        Color = Color.Black,
                        HeightRequest = 1
                    },
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
                            new BoxView
                            {
                                WidthRequest = 20,
                                Color = Color.Transparent
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
                         HorizontalOptions = LayoutOptions.CenterAndExpand,
                    },
                    new BoxView
                    {
                        Color = Color.Black,
                        HeightRequest = 1
                    },
                    tagButton,
                    processButton
                },
                Spacing = _pageSpacing
            };

            ToolbarItems.Add(new ToolbarItem("Settings", null, OnSettingsButtonClick, ToolbarItemOrder.Primary));
        }
    }
}
