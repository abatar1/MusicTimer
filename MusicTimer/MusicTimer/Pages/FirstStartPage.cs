using System;
using System.Collections.Generic;
using MusicTimer.Domain.Files;
using Xamarin.Forms;

namespace MusicTimer.Pages
{
    public class FirstStartPage : ContentPage
    {
        private List<string> _directoriesList;

        public FirstStartPage()
        {
            _directoriesList = new List<string>{DependencyService.Get<IFileHelper>().DefaultMusicFolder()};
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var addButton = new Button
            {
                Text = "Add directory",
                HorizontalOptions = LayoutOptions.EndAndExpand,
                FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label))
            };
            addButton.Clicked += OnAddButtonClick;

            var nextButton = new Button
            {
                Text = "Next",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.End
            };
            nextButton.Clicked += async (sender, e) =>
            {
                await Navigation.PushModalAsync(new MainPage());
            };

            var folderView = new StackLayout
            {
                Children =
                {
                    new Label
                    {
                        Text =
                            "Please select directories where you collect your tracks or just press next and use default.",
                        HorizontalOptions = LayoutOptions.Center
                    }
                }
            };
            var dirName = GetDirNameFrame(DependencyService.Get<IFileHelper>().DefaultMusicFolder());
            folderView.Children.Add(dirName);

            Content = new StackLayout
            {
                Children = {
                    new Label
                    {
                        Text = "Welcome to the Music.Timer.",
                        HorizontalOptions = LayoutOptions.Center
                    },
                    folderView,
                    addButton,
                    nextButton
                },
                VerticalOptions = LayoutOptions.FillAndExpand
            };
        }

        private static Frame GetDirNameFrame(string dirName)
        {
            return new Frame
            {
                Content = new Label
                {
                    Text = "/" + dirName,
                    BackgroundColor = Color.Gray,
                },
                OutlineColor = Color.Gray
            };
        }
        
        private void OnAddButtonClick(object sender, EventArgs eventArgs)
        {
        }
    }
}
