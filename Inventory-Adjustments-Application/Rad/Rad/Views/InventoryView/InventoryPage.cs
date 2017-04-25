using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Rad.Models;
using Rad.ViewModel;
using Rad.Views.AdjusmentView;

namespace Rad.Views.InventoryView
{
    public class InventoryPage : TabbedPage
    {
        private Models.Inventory _item;
        private DataBaseManager _database;
        private InventoryListPage _parent;


        public InventoryPage(InventoryListPage parent, DataBaseManager database, Models.Inventory item)//(Models.Inventory item)
        {
            _parent = parent;
            _database = database;
            _item = item;
            this.Title = _item.Name;

            this.Children.Add(new InventoryEditPage(_parent, _database, _item));
            this.Children.Add(new InventoryWhsPage(_database, _item));
            this.Children.Add(new AdjustmentLanchPage(_database, _item));// ,Icon = "Today.png"});

        }
    }
}
