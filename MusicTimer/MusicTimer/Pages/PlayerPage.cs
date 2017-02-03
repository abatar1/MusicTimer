using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace MusicTimer.Pages
{
    public class PlayerPage : ContentPage
    {
        private static readonly int _hid = 24;
        private static readonly int _mih = 60;
        private static readonly int _pageSpacing = 10;

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

        public PlayerPage()
        {        
            var headerLabel = InitPlayerPageLabel("Music.Timer");
            var mLabel = InitPlayerPageLabel("Minutes");
            var hLabel = InitPlayerPageLabel("Hours");

            var mPicker = InitRangePicker("Minutes", 1, _mih);
            var hPicker = InitRangePicker("Hours", 1, _hid);

            Content = new StackLayout
            {               
                Children =
                {
                    headerLabel,
                    new BoxView
                    {
                        WidthRequest = 20,
                        Color = Color.Transparent
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
                         BackgroundColor = Color.FromHex("#f44")
                    },
                       
                },
                Spacing = _pageSpacing
            };
        }
    }
}
