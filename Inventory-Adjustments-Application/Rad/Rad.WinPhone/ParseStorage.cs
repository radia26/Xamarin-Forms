using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Parse;
using Rad.Models;
using Rad.ViewModel;

namespace Rad.WinPhone
{
    public class ParseStorage : IParseStorage
    {

        async public Task<List<Warehouse>> RefreshWarehouseAsync()
        {
            var query = ParseObject.GetQuery("Warehouse");
            var results = await query.FindAsync();

            var whs = new List<Warehouse>();
            foreach (var item in results)
            {
                whs.Add(FromParsewhsObject(item));
            }

            return whs;
        }


        static Warehouse FromParsewhsObject(ParseObject po)
        {
            var t = new Warehouse();
            t.Warehouse_ID = po.ObjectId;
            t.WareHouseName = Convert.ToString(po["Warehouse_Name"]);
            t.WareHouseLocation = Convert.ToString(po["Warehouse_Location"]);

            return t;
        }


        static ParseStorage todoServiceInstance = new ParseStorage();

        public static ParseStorage Default { get { return todoServiceInstance; } }

        public List<Inventory> Items { get; private set; }

        protected ParseStorage()
        {
            Items = new List<Inventory>();

            // https://parse.com/apps/YOUR_APP_NAME/edit#app_keys
            // ApplicationId, Windows/.NET/Client key
            //ParseClient.Initialize ("APPLICATION_ID", ".NET_KEY");
            ParseClient.Initialize(Constants.ApplicationId, Constants.Key);
        }

        ParseObject ToParseObject(Inventory item)
        {
            var po = new ParseObject("Inventory");
            if (item.Item_ID != string.Empty)
            {
                po.ObjectId = item.Item_ID;
            }
            po["Name"] = item.Name;
            po["Cost"] = item.Cost;
            po["Vendor"] = item.Vendor;
            po["Weight"] = item.Weight;
            return po;
        }

        static Inventory FromParseObject(ParseObject po)
        {
            var t = new Inventory();
            t.Item_ID = po.ObjectId;
            t.Name = Convert.ToString(po["Name"]);
            t.Cost = Convert.ToDouble(po["Cost"]);
            t.Vendor = Convert.ToString(po["Vendor"]);
            t.Weight = Convert.ToDouble(po["Weight"]);
            return t;
        }

        async public Task<List<Inventory>> RefreshDataAsync()
        {
            var query = ParseObject.GetQuery("Inventory");
            var results = await query.FindAsync();

            var Items = new List<Inventory>();
            foreach (var item in results)
            {
                Items.Add(FromParseObject(item));
            }

            return Items;
        }

        public async Task SaveInventoryAsync(Inventory todoItem)
        {
            try
            {
                await ToParseObject(todoItem).SaveAsync();
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(@"				ERROR {0}", e.Message);
            }
        }

        public async Task DeleteInventoryAsync(Inventory item)
        {
            try
            {
                await ToParseObject(item).DeleteAsync();
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(@"				ERROR {0}", e.Message);
            }
        }

        public async Task<bool> ExistInventoryAsync(Inventory item)
        {
            try
            {

                bool IS_Exist = false;
                var query = ParseObject.GetQuery("Inventory");
                var results = await query.FindAsync();


                foreach (var itemm in results)
                {
                    if (FromParseObject(itemm).Name == item.Name)
                    {
                        IS_Exist = true;
                    }
                }

                return IS_Exist;
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(@"				ERROR {0}", e.Message);
                return false;
            }
        }


        public async Task EditItemWarehouseAsync(InventoryWarehouse item, int qty)
        {
            try
            {
                var query = ParseObject.GetQuery("InventoryWarehouse").WhereEqualTo("item_id", item.Item_ID).WhereEqualTo("warehouse_name", item.Warehouse_Name);
                var results = await query.FirstAsync();

                if (results.Any())
                {
                    results["qty"] = qty;
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(@"				ERROR {0}", e.Message);
            }
        }



        #region inv-whs

        ParseObject ToParseIWObject(InventoryWarehouse item)
        {
            var po = new ParseObject("InventoryWarehouse");
            if (item.Item_ID != string.Empty)
            {
                po.ObjectId = item.ItemWhse_ID;
            }

            po["item_id"] = item.Item_ID;
            po["qty"] = item.Quantity;
            po["warehouse_name"] = item.Warehouse_Name;
            return po;
        }

        static InventoryWarehouse FromParseIWObject(ParseObject po)
        {
            var item = new InventoryWarehouse();
            item.ItemWhse_ID = po.ObjectId;
            item.Item_ID = Convert.ToString(po["item_id"]);
            item.Quantity = Convert.ToInt32(po["qty"]);
            item.Warehouse_Name = Convert.ToString(po["warehouse_name"]);

            return item;
        }

        public async Task SaveInventoryWarehouseAsync(InventoryWarehouse item)
        {
            try
            {
                await ToParseIWObject(item).SaveAsync();
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(@"				ERROR {0}", e.Message);
            }
        }

        public async Task DeleteInventoryWarehouseAsync(InventoryWarehouse item)
        {
            try
            {
                await ToParseIWObject(item).DeleteAsync();
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(@"				ERROR {0}", e.Message);
            }
        }

        public async Task<InventoryWarehouse> ExistItemWarehouseAsync(string itemId, string whs)
        {
            try
            {
                bool IS_Exist = false;
                var query = ParseObject.GetQuery("InventoryWarehouse").WhereEqualTo("item_id", itemId).WhereEqualTo("warehouse_name", whs);
                //IEnumerable<ParseObject> results = await query.FindAsync();
                var results = await query.FirstAsync();

                if (results.Any())
                {
                    IS_Exist = true;
                    return FromParseIWObject(results);
                }


                return null;
                //return IS_Exist;


            }
            catch (Exception e)
            {
                Console.Error.WriteLine(@"				ERROR {0}", e.Message);
                return null;
            }
        }



        public async Task AdjustInOutItemWarehouseAsync(InventoryWarehouse item, int qty, bool adjustIn)
        {
            try
            {
                var query = ParseObject.GetQuery("InventoryWarehouse").WhereEqualTo("item_id", item.Item_ID).WhereEqualTo("warehouse_name", item.Warehouse_Name);
                var results = await query.FirstAsync();
                if (results.Any())
                {
                    if (adjustIn)
                    {
                        results["qty"] = qty + Convert.ToInt32(results["qty"]);
                    }
                    else
                    {
                        results["qty"] = Convert.ToInt32(results["qty"]) - qty;
                    }
                    await results.SaveAsync();
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(@"				ERROR {0}", e.Message);
            }
        }

        public async Task AdjustTransferItemWarehouseAsync(InventoryWarehouse item, int qty, string toWarehouse)
        {
            try
            {
                var query = ParseObject.GetQuery("InventoryWarehouse").WhereEqualTo("item_id", item.Item_ID).WhereEqualTo("warehouse_name", item.Warehouse_Name);
                var results = await query.FirstAsync();
                if (results.Any())
                {

                    results["qty"] = qty + Convert.ToInt32(results["qty"]);

                    await results.SaveAsync();
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(@"				ERROR {0}", e.Message);
            }
        }


        //public async Task<InventoryWarehouse> GetInvWhsFromInv(Inventory item, string whs)
        //{
        //    try
        //    {

        //        var query = ParseObject.GetQuery("InventoryWarehouse");
        //        var results = await query.FindAsync();
        //        InventoryWarehouse invwhs = new InventoryWarehouse();

        //        foreach (var itemm in results)
        //        {
        //            if (FromParseIWObject(itemm).Item_ID == item.Item_ID && FromParseIWObject(itemm).Warehouse_Name == whs)
        //            {

        //            }
        //        }

        //        return IS_Exist;
        //    }
        //    catch (Exception e)
        //    {
        //        Console.Error.WriteLine(@"				ERROR {0}", e.Message);
        //        return false;
        //    }
        //}



        #endregion



    
    
    
    
    
    }
}