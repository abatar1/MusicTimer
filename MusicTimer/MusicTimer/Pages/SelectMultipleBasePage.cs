using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using MusicTimer.Domain;
using MusicTimer.Infrastructure;
using Xamarin.Forms;

namespace MusicTimer.Pages
{
    public class SelectMultipleBasePage : ContentPage
    {
        public event EventHandler<TagEventArgs> TagAddedEvent;

        public class WrappedSelection : INotifyPropertyChanged
        {
            public Tag Item { get; set; }
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

        private HashSet<WrappedSelection> _wrappedItems;

        public SelectMultipleBasePage(IEnumerable<Tag> items)
        {
            _wrappedItems = new HashSet<WrappedSelection>(items.Select(item => new WrappedSelection { Item = item, IsSelected = false }));
            var mainList = new ListView
            {
                ItemsSource = _wrappedItems,
                ItemTemplate = new DataTemplate(typeof(WrappedItemSelectionTemplate)),
            };

            mainList.ItemSelected += (sender, e) => 
            {
                if (e.SelectedItem == null) return;
                var o = (WrappedSelection)e.SelectedItem;
                o.IsSelected = !o.IsSelected;
                (sender as ListView).SelectedItem = null;
            };

            var addButton = new Button
            {
                Text = "Add tags",
                HorizontalOptions = LayoutOptions.Center
            };
            addButton.Clicked += (sender, e) =>
            {
                var entry = new Entry
                {
                    Placeholder = "Enter tag name here."
                };

                var button = new Button
                {
                    Text = "Add",
                    HorizontalOptions = LayoutOptions.Center,
                };
                button.Clicked += (sender1, e1) =>
                {
                    var text = (sender as Entry)?.Text;
                    var tag = new Tag(text);
                    var ws = new WrappedSelection {Item = tag, IsSelected = false};
                    _wrappedItems.Add(ws);
                    Navigation.PopAsync();                  
                };

                var tagPage = new ContentPage
                {
                    Title = "Adding tag",
                    Content = new StackLayout
                    {
                        Children =
                        {
                            entry,
                            button
                        }
                    }
                };
                Navigation.PushAsync(tagPage);
            };
            Content = new StackLayout
            {
                Children =
                {
                    mainList,
                    addButton
                }
            };                       

            ToolbarItems.Add(new ToolbarItem("All", null, SelectAll, ToolbarItemOrder.Primary));
            ToolbarItems.Add(new ToolbarItem("None", null, SelectNone, ToolbarItemOrder.Primary));
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

        public List<Tag> GetSelection()
        {
            return _wrappedItems.Where(item => item.IsSelected).Select(wrappedItem => wrappedItem.Item).ToList();
        }
    }
}
