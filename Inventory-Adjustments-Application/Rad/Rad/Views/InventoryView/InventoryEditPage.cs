using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rad.Factories;
using Rad.ViewModel;
using Xamarin.Forms;

namespace Rad.Views.InventoryView
{
    public class InventoryEditPage : ContentPage
    {
        private InventoryListPage _parent;
        private DataBaseManager _database;
        private Models.Inventory _item;
        private bool IsNew;

        public InventoryEditPage(InventoryListPage parent, DataBaseManager database, Models.Inventory item = null)
        {
            IsNew = item == null;
            _item = item;
            _parent = parent;
            _database = database;
            Title = IsNew ? "Add an item" : "Properties";

            Entry Name, Cost, Weight, Vendor;
            var NameStack = ControlFactory.GetLabelEntryStackLayout("Name:", out Name);
            var CostStack = ControlFactory.GetLabelEntryStackLayout(" Cost:", out Cost);
            var QtyStack = ControlFactory.GetLabelEntryStackLayout("Weight:", out Weight);
            var WarehouseStack = ControlFactory.GetLabelEntryStackLayout("Vendor:", out Vendor);


            if (!IsNew)
            {
                Name.Text = _item.Name;
                Cost.Text = Convert.ToString(_item.Cost);
                Weight.Text = Convert.ToString(_item.Weight);
                Vendor.Text = _item.Vendor;
            }



            var button = ControlFactory.GetButton(IsNew ? "Add" : "Save");
            button.VerticalOptions = LayoutOptions.EndAndExpand;



            button.Clicked += async (object sender, EventArgs e) =>
            {
                var itemName = Name.Text;
                var itemCost = Cost.Text;
                var itemWeight = Weight.Text;
                var itemVendor = Vendor.Text;
                bool Inventory_Exist;
                if (IsNew)
                {
                    _database.AddInventory(itemName, itemCost, itemWeight, itemVendor, out Inventory_Exist);
                    if (Inventory_Exist)
                        await DisplayAlert("Alert", "Item with the same name already exists", "OK");
                    else
                    {
                        await Navigation.PopAsync();
                        _parent.Refresh();
                    }
                }
                else
                {
                    _database.EditInventory(_item, itemName, Convert.ToDouble(itemCost), Convert.ToDouble(itemWeight), itemVendor);
                    await DisplayAlert("Done", "Item Changes Saved", "OK");
                }


            };

            var stack = ControlFactory.GetStacksLayout(NameStack, CostStack, QtyStack, WarehouseStack);
            stack.Children.Add(button);
            Content = stack;
        }


        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            _parent.Refresh();
        }

    }
}