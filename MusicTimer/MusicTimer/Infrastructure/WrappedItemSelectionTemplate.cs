using Xamarin.Forms;

namespace MusicTimer.Infrastructure
{
    public class WrappedItemSelectionTemplate : ViewCell
    {
        public WrappedItemSelectionTemplate()
        {
            var name = new Label();
            name.SetBinding(Label.TextProperty, new Binding("Item.Name"));
            var mainSwitch = new Switch();
            mainSwitch.SetBinding(Switch.IsToggledProperty, new Binding("IsSelected"));
            var layout = new RelativeLayout();
            layout.Children.Add(name,
                Constraint.Constant(5),
                Constraint.Constant(5),
                Constraint.RelativeToParent(p => p.Width - 60),
                Constraint.RelativeToParent(p => p.Height - 10)
            );
            layout.Children.Add(mainSwitch,
                Constraint.RelativeToParent(p => p.Width - 55),
                Constraint.Constant(5),
                Constraint.Constant(50),
                Constraint.RelativeToParent(p => p.Height - 10)
            );
            View = layout;
        }
    }
}
