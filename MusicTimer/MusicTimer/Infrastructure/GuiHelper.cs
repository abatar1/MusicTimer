using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MusicTimer.Infrastructure
{
    public static class GuiHelper
    {
        public static CustomPicker CreateRangePicker(string title, int start, int count)
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

        public static Label CreatePlayerPageLabel(string text)
        {
            return new Label
            {
                Text = text,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center
            };
        }

        public static Button CreateButton(string text, LayoutOptions hor, LayoutOptions vert)
        {
            return new Button
            {
                Text = text,
                HorizontalOptions = hor,
                VerticalOptions = vert
            };
        }
    }
}
