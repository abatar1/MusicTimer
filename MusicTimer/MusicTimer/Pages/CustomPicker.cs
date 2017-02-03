using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MusicTimer.Pages
{
    public class CustomPicker : Picker
    {
        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            
            if (propertyName == SelectedIndexProperty.PropertyName)
            {
                InvalidateMeasure();               
            }
        }
    }
}
