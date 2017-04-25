using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rad.Factories;
using Rad.Models;
using Rad.ViewModel;
using Rad.Views.InventoryView;
using Xamarin.Forms;

namespace Rad.Views.AdjustmentView
{
    public enum Adjustment_Type
    {
        Adjust_In,
        Adjust_Out,
        Transfer
    }
    public class InventoryAdjustmentPage : ContentPage
    {
        private Models.Inventory _item;
        private DataBaseManager _database;
        private bool is_AdjustIn;
        private bool is_AdjustOut;
        private bool is_Transfer;



        public InventoryAdjustmentPage(Adjustment_Type AdjustType, DataBaseManager database, Models.Inventory item)
        {
            Title = item.Name + ": " + AdjustType;
            _item = item;
            _database = database;

            is_AdjustIn = false;
            is_AdjustOut = false;
            is_Transfer = false;
            if (AdjustType == Adjustment_Type.Adjust_In)
                is_AdjustIn = true;
            else if (AdjustType == Adjustment_Type.Adjust_Out)
                is_AdjustOut = true;
            else
                is_Transfer = true;

            Entry Name, Qty;
            var NameStack = ControlFactory.GetLabelEntryStackLayout("Name:", out Name);
            var QtyStack = ControlFactory.GetLabelEntryStackLayout("QTY:", out Qty);
            Name.Text = item.Name;
            Qty.Text = "0";

            Picker FromWarehouse, ToWarehouse;
            var FromWarehouseStack = ControlFactory.GetLabelPickerStackLayout("From:", out FromWarehouse);
            var ToWarehouseStack = ControlFactory.GetLabelPickerStackLayout("To:", out ToWarehouse);

            IEnumerable<Warehouse> warehouses = _database.GetWareHouses();
            foreach (var warehouse in warehouses)
            {
                FromWarehouse.Items.Add(warehouse.Name);
                ToWarehouse.Items.Add(warehouse.Name);
            }

            if (is_AdjustIn)
                FromWarehouseStack.IsVisible = false;
            else if (is_AdjustOut)
                ToWarehouse.IsVisible = false;

            var stack = ControlFactory.GetStacksLayout(NameStack, QtyStack, FromWarehouseStack, ToWarehouseStack);

            var button = ControlFactory.GetButton("Adjust Item");
            button.VerticalOptions = LayoutOptions.EndAndExpand;
            stack.Children.Add(button);

            Content = stack;

            button.Clicked += async (object sender, EventArgs e) =>
            {
                var itemName = Name.Text;
                var itemQty = Qty.Text;

                string fromWarehouse = "", toWarehouse = "";
                if (is_AdjustIn || is_Transfer)
                {
                    toWarehouse = ToWarehouse.Items[ToWarehouse.SelectedIndex];

                }
                if (is_AdjustOut || is_Transfer)
                {
                    fromWarehouse = FromWarehouse.Items[FromWarehouse.SelectedIndex];
                }

                if (is_AdjustIn)
                {
                    _database.Adjust(itemName, itemQty, toWarehouse);
                    await DisplayAlert("Done", "Adjust In is done", "OK");
                }
                else if (is_AdjustOut)
                {
                    _database.Adjust(itemName, itemQty, "", fromWarehouse);
                    await DisplayAlert("Done", "Adjust Out is done", "OK");
                }
                else
                {
                    _database.Adjust(itemName, itemQty, toWarehouse, fromWarehouse, true);
                    await DisplayAlert("Done", "Transfer is done", "OK");
                }

                this.Navigation.PopToRootAsync();

                Navigation.PushAsync(new InventoryListPage(_database));


            };
        }
    }
}
