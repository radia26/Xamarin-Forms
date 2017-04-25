using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rad.ViewModel;
using Xamarin.Forms;

namespace Rad.Views.InventoryView
{
    public class InventoryWhsPage : ContentPage
    {
        private DataBaseManager _database;
        private ListView _InventoryList;

        public InventoryWhsPage(DataBaseManager database, Models.Inventory item)
        {

            _database = database;
            Title = "WareHouses";
            var whss = _database.GetInventoryWarehouses(item.Item_ID);

            _InventoryList = new ListView();
            _InventoryList.ItemsSource = whss;
            _InventoryList.ItemTemplate = new DataTemplate(typeof(TextCell));
            _InventoryList.ItemTemplate.SetBinding(TextCell.TextProperty, "Warehouse_Name");
            _InventoryList.ItemTemplate.SetBinding(TextCell.DetailProperty, "Quantity");




            //var toolbarItem = new ToolbarItem
            //{
            //    Name = "Add",
            //    Command = new Command(() => Navigation.PushAsync(new InventoryEditPage(this, _database))),

            //};

            //ToolbarItems.Add(toolbarItem);

            Content = _InventoryList;
        }

        public void Refresh()
        {
            _InventoryList.ItemsSource = _database.Getinventories();
        }
    }
}
