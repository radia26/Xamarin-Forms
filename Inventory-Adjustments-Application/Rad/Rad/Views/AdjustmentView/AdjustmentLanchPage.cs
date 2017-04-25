using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rad.Factories;
using Rad.ViewModel;
using Rad.Views.AdjustmentView;
using Rad.Views.InventoryView;
using Xamarin.Forms;

namespace Rad.Views.AdjusmentView
{
    class AdjustmentLanchPage : ContentPage
    {

        private Models.Inventory _item;
        private DataBaseManager _database;

        public AdjustmentLanchPage(DataBaseManager database, Models.Inventory item)
        {

            Title = "Adjustments";
            _item = item;
            _database = database;

            var btnAdjustIn = ControlFactory.GetButton("Adjust In");
            var btnAdjustOut = ControlFactory.GetButton("Adjust Out");
            var btnAdjustTransfer = ControlFactory.GetButton("Transfer");


            btnAdjustIn.Clicked += async (object sender, EventArgs e) =>
            {
                await Navigation.PushAsync(new InventoryAdjustmentPage(Adjustment_Type.Adjust_In, _database, _item));
            };
            btnAdjustOut.Clicked += async (object sender, EventArgs e) =>
            {
                await Navigation.PushAsync(new InventoryAdjustmentPage(Adjustment_Type.Adjust_Out, _database, _item));
            };
            btnAdjustTransfer.Clicked += async (object sender, EventArgs e) =>
            {
                await Navigation.PushAsync(new InventoryAdjustmentPage(Adjustment_Type.Transfer, _database, _item));

            };

            var stack = new StackLayout()
            {
                Padding = 10,
                Spacing = 5,
                Orientation = StackOrientation.Vertical,
                Children = { btnAdjustIn, btnAdjustOut, btnAdjustTransfer },
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            Content = stack;
        }
    }
}
