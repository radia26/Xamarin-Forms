using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rad.ViewModel;
using Xamarin.Forms;

namespace Rad.Views.InventoryView
{
    public class InventoryListPage : ContentPage
    {
        private DataBaseManager _database;
        private ListView _InventoryList;

        public InventoryListPage(DataBaseManager database)
        {
            BackgroundColor = Color.Teal;
            _database = database;
            Title = "Items";
            var inventories = _database.Getinventories();

            _InventoryList = new ListView();
            _InventoryList.ItemsSource = inventories;
            _InventoryList.ItemTemplate = new DataTemplate(typeof(TextCell));
            _InventoryList.ItemTemplate.SetBinding(TextCell.TextProperty, "Name");
            _InventoryList.ItemTemplate.SetBinding(TextCell.DetailProperty, "CreatedOn");
            _InventoryList.ItemSelected += InventoryList_OnItemSelect;



            var toolbarItem = new ToolbarItem
            {
                Name = "Add",
                Command = new Command(() => Navigation.PushAsync(new InventoryEditPage(this, _database))),

            };

            ToolbarItems.Add(toolbarItem);

            Content = _InventoryList;
        }

        public void Refresh()
        {
            _InventoryList.ItemsSource = _database.Getinventories();
        }

        public void InventoryList_OnItemSelect(object sender, SelectedItemChangedEventArgs e)
        {
            Navigation.PushAsync(new InventoryPage(this, _database, (Models.Inventory)e.SelectedItem));
            //Navigation.PushAsync(new InventoryEditPage(this, _database, (Models.Inventory)e.SelectedItem));
        }
    }
}