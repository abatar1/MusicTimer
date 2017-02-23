using Xamarin.Forms;

namespace MusicTimer.Infrastructure
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
