using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rad.Models;
using SQLite;
using Xamarin.Forms;

namespace Rad.ViewModel
{
    public class DataBaseManager
    {
        IParseStorage storage;

        public DataBaseManager(IParseStorage parseStorage)
		{
			storage = parseStorage;
		}

        public Task<List<Inventory>> GetInventoriesAsync()
		{
			return storage.RefreshDataAsync ();
		}

		public Task SaveTaskAsync (Inventory item)
		{
			return storage.SaveInventoryAsync (item);
		}

		public Task DeleteTaskAsync (Inventory item)
		{
			return storage.DeleteInventoryAsync (item);
		}

        public Task <bool>InventoryExistTaskAsync(Inventory item)
        {
            return storage.ExistInventoryAsync(item);
        }

        public async void EditTaskAsync(Inventory item,string name, double cost, double weight,string vendor)
        {
            await DeleteTaskAsync(item);
            Inventory i = new Inventory();
            i.Name = name;
            i.Cost =cost;
            i.Vendor = vendor;
            i.Weight = weight;
            await storage.SaveInventoryAsync(i);
        }


        //*****************************************************************//

        public Task<bool> ItemWarehouseExistTaskAsync(InventoryWarehouse item, string whs)
        {
            return  storage.ExistItemWarehouseAsync(item,whs);
        }

        public Task SaveWITaskAsync(InventoryWarehouse item)
        {
            return storage.SaveInventoryWarehouseAsync(item);
        }

        public Task DeleteWITaskAsync(InventoryWarehouse item)
        {
            return storage.DeleteInventoryWarehouseAsync(item);
        }

        public async void EditWITaskAsync(Inventory item, string item_id, string warehouse_name, int qty)
        {
            await DeleteWITaskAsync(item);
            InventoryWarehouse i = new InventoryWarehouse();
            i.Warehouse_Name = warehose_name;
            i.Item_ID= item_id;
            i.Quantity = qty;
            await storage.SaveInventoryWarehouseAsync(i);
        }

        public async void AdjustIn(Inventory item,int qty,string towarehouse)
        {
            bool x = await storage.ExistItemWarehouseAsync(item, towarehouse);
            if (x)
            {
                 EditWITaskAsync(item, towarehouse, item.Item_ID, item., qty + item.Quantity);
            }
            else
            {
               await  SaveWITaskAsync(item);
            }
        }


        public  void AdjustOut(InventoryWarehouse item, int qty, string fromowarehouse)
        {
                 EditWITaskAsync(item, fromowarehouse, item.Item_ID, item.Warehouse_ID, qty - item.Quantity);
        }
    }
}
