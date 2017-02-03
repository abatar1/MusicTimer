using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MusicTimer.Pages
{
    public class SelectMultipleBasePage<T> : ContentPage
    {
        public class WrappedSelection<T1> : INotifyPropertyChanged
        {
            public T1 Item { get; set; }
            private bool _isSelected;

            public bool IsSelected
            {
                get
                {
                    return _isSelected;
                }
                set
                {
                    if (_isSelected == value) return;
                    _isSelected = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("IsSelected"));
                }
            }
            public event PropertyChangedEventHandler PropertyChanged = delegate { };
        }

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

        public List<WrappedSelection<T>> WrappedItems;

        public SelectMultipleBasePage(IEnumerable<T> items)
        {
            WrappedItems = items.Select(item => new WrappedSelection<T> { Item = item, IsSelected = false }).ToList();
            var mainList = new ListView
            {
                ItemsSource = WrappedItems,
                ItemTemplate = new DataTemplate(typeof(WrappedItemSelectionTemplate)),
            };

            mainList.ItemSelected += (sender, e) => {
                if (e.SelectedItem == null) return;
                var o = (WrappedSelection<T>)e.SelectedItem;
                o.IsSelected = !o.IsSelected;
                (sender as ListView).SelectedItem = null;
            };
            Content = mainList;
                      
            ToolbarItems.Add(new ToolbarItem("All", null, SelectAll, ToolbarItemOrder.Primary));
            ToolbarItems.Add(new ToolbarItem("None", null, SelectNone, ToolbarItemOrder.Primary));
            
        }

        public void SelectAll()
        {
            foreach (var wi in WrappedItems)
            {
                wi.IsSelected = true;
            }
        }

        public void SelectNone()
        {
            foreach (var wi in WrappedItems)
            {
                wi.IsSelected = false;
            }
        }

        public List<T> GetSelection()
        {
            return WrappedItems.Where(item => item.IsSelected).Select(wrappedItem => wrappedItem.Item).ToList();
        }
    }
}
