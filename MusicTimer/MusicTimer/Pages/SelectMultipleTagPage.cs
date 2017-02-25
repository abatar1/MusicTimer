using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MusicTimer.Domain;
using MusicTimer.Infrastructure;
using Xamarin.Forms;

namespace MusicTimer.Pages
{
    public class SelectMultipleTagPage : ContentPage
    {              
        private readonly ObservableCollection<WrappedSelection<Tag>> _wrappedItems;

        public SelectMultipleTagPage(IEnumerable<Tag> items)
        {
            _wrappedItems = new ObservableCollection<WrappedSelection<Tag>>(items.Select(item => new WrappedSelection<Tag> { Item = item, IsSelected = false }));
            var mainList = new ListView
            {
                ItemsSource = _wrappedItems,
                ItemTemplate = new DataTemplate(typeof(WrappedItemSelectionTemplate)),
            };

            mainList.ItemSelected += (sender, e) => 
            {
                if (e.SelectedItem == null) return;
                var o = (WrappedSelection<Tag>)e.SelectedItem;
                o.IsSelected = !o.IsSelected;
                ((ListView)sender).SelectedItem = null;
            };
            var addButton = GuiHelper.CreateButton("Add tag", LayoutOptions.Center, LayoutOptions.Center);
            addButton.Clicked += (sender, e) =>
            {
                var entry = new Entry
                {
                    Placeholder = "Enter some tag name here."
                };

                var finishButton = GuiHelper.CreateButton("Add tag", LayoutOptions.Center, LayoutOptions.Center);
                finishButton.Clicked += (sender1, e1) =>
                {
                    var text = (sender as Entry)?.Text;
                    var tag = new Tag(text);
                    var ws = new WrappedSelection<Tag> {Item = tag, IsSelected = false};
                    _wrappedItems.Add(ws);                   
                    Navigation.PopAsync();                  
                };

                var trackButton = GuiHelper.CreateButton("Add tag", LayoutOptions.Center, LayoutOptions.Center);
                var tagPage = new ContentPage
                {
                    Title = "Adding tag",
                    Content = new StackLayout
                    {
                        Children =
                        {
                            entry,
                            trackButton,
                            finishButton
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
