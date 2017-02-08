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

        private readonly List<WrappedSelection<T>> _wrappedItems;

        public SelectMultipleBasePage(IEnumerable<T> items)
        {
            _wrappedItems = items.Select(item => new WrappedSelection<T> { Item = item, IsSelected = false }).ToList();
            var mainList = new ListView
            {
                ItemsSource = _wrappedItems,
                ItemTemplate = new DataTemplate(typeof(WrappedItemSelectionTemplate)),
            };

            mainList.ItemSelected += (sender, e) => {
                if (e.SelectedItem == null) return;
                var o = (WrappedSelection<T>)e.SelectedItem;
                o.IsSelected = !o.IsSelected;
                (sender as ListView).SelectedItem = null;
            };
            Content = mainList;

            ToolbarItems.Add(new ToolbarItem("Add", null, AddSelection, ToolbarItemOrder.Primary));
            ToolbarItems.Add(new ToolbarItem("All", null, SelectAll, ToolbarItemOrder.Primary));
            ToolbarItems.Add(new ToolbarItem("None", null, SelectNone, ToolbarItemOrder.Primary));
        }

        private void AddSelection()
        {
            // TODO Как-то по нажатию должны добавлять Tag в базу и на экран
        }

        private void SelectAll()
        {
            foreach (var wi in _wrappedItems)
            {
                wi.IsSelected = true;
            }
        }

        private void SelectNone()
        {
            foreach (var wi in _wrappedItems)
            {
                wi.IsSelected = false;
            }
        }

        public List<T> GetSelection()
        {
            return _wrappedItems.Where(item => item.IsSelected).Select(wrappedItem => wrappedItem.Item).ToList();
        }
    }
}
