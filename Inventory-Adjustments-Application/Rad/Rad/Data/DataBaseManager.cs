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
        private SQLiteConnection _connection;

        public DataBaseManager()
        {
            _connection = DependencyService.Get<ISQLite>().GetConnection();
            _connection.CreateTable<Inventory>();
            _connection.CreateTable<Warehouse>();
            _connection.CreateTable<InventoryWarehouse>();

            InitializeWareHouses();
        }

        public void InitializeWareHouses()
        {
            AddWarehouse("East");
            AddWarehouse("West");
            AddWarehouse("X300");
            AddWarehouse("X320");
            AddWarehouse("X350");
        }

        public IEnumerable<Inventory> Getinventories()
        {
            return (from t in _connection.Table<Inventory>() select t).ToList();
        }

        public IEnumerable<Warehouse> GetWareHouses()
        {
            return (from t in _connection.Table<Warehouse>() select t).ToList();
        }


        public Inventory GetInventory(int id)
        {
            return _connection.Table<Inventory>().FirstOrDefault(t => t.Item_ID == id);
        }

        public Inventory GetInventory(string name)
        {
            return _connection.Table<Inventory>().FirstOrDefault(t => t.Name == name);
        }
        public Warehouse GetWarehouse(string name)
        {
            return _connection.Table<Warehouse>().FirstOrDefault(t => t.Name == name);
        }

        public Warehouse GetWareHouses(int id)
        {
            return _connection.Table<Warehouse>().FirstOrDefault(t => t.Warehouse_ID == id);
        }



        public void DeleteInventory(int id)
        {
            _connection.Delete<Inventory>(id);
        }

        public void DeleteWarehouse(int id)
        {
            _connection.Delete<Warehouse>(id);
        }

        public bool CheckInventory(string name)
        {
            var inv = GetInventory(name);
            if (inv == null)
                return false;
            else
                return true;
        }
        public bool CheckWarehouse(string name)
        {
            var whs = GetWarehouse(name);
            if (whs == null)
                return false;
            else
                return true;
        }

        public void AddInventory(string name, string cost, string weight, string vendor, out bool Inventory_Exist)
        {
            Inventory_Exist = CheckInventory(name);
            if (!Inventory_Exist)
            {
                var newInventory = new Inventory
                {
                    Name = name,
                    Cost = Convert.ToDouble(cost),
                    Weight = Convert.ToDouble(weight),
                    Vendor = vendor,
                    CreatedOn = DateTime.Now
                };

                _connection.Insert(newInventory);
            }
        }

        public void EditInventory(Inventory item, string name, double cost, double weight, string vendor)
        {
            bool x;
            DeleteInventory(item.Item_ID);
            AddInventory(name, Convert.ToString(cost), Convert.ToString(weight), vendor, out x);


        }

        public void AddWarehouse(string Warehouse)
        {
            bool whs_Exist = CheckWarehouse(Warehouse);
            if (!whs_Exist)
            {
                var newWarehouse = new Warehouse
                {
                    Name = Warehouse,
                };

                _connection.Insert(newWarehouse);
            }
        }

        /*****************************/

        public InventoryWarehouse GetInventoryWarehouse(int W_ID, int I_ID)
        {
            return _connection.Table<InventoryWarehouse>().FirstOrDefault(t => t.Warehouse_ID == W_ID && t.Item_ID == I_ID);
        }

        public IEnumerable<InventoryWarehouse> GetInventoryWarehouses(int item)
        {
            return ((from t in _connection.Table<InventoryWarehouse>() select t).Where(t => t.Item_ID == item)).ToList();
        }

        public void Adjust(string item, string quantity, string toWarehouse = "", string fromWarehouse = "", bool transfer = false)
        {
            var Inv = GetInventory(item);
            InventoryWarehouse w11 = new InventoryWarehouse();
            InventoryWarehouse w22 = new InventoryWarehouse();



            InventoryWarehouse ToInvWhs = new InventoryWarehouse();
            int ToQty = 0, ToID = 0;
            if (!string.IsNullOrEmpty(toWarehouse))
            {
                var toWhs = GetWarehouse(toWarehouse);
                ToInvWhs = GetInventoryWarehouse(Inv.Item_ID, toWhs.Warehouse_ID);
                if (ToInvWhs != null)
                {
                    ToQty = ToInvWhs.Quantity;
                    ToID = ToInvWhs.Warehouse_ID;
                    _connection.Delete<InventoryWarehouse>(ToInvWhs.WhsItem_ID);
                }
                var w1 = new InventoryWarehouse
                {
                    Item_ID = Inv.Item_ID,
                    Warehouse_ID = ToID,
                    Warehouse_Name = ToInvWhs.Warehouse_Name,
                    Quantity = ToQty + Convert.ToInt32(quantity)

                };
                //_connection.Insert(w1);
                w11 = w1;
            }


            InventoryWarehouse FromInvWhs = new InventoryWarehouse();
            int FromQty = 0, FromID = 0;
            if (!string.IsNullOrEmpty(fromWarehouse))
            {
                var fromWhs = GetWarehouse(fromWarehouse);
                FromInvWhs = GetInventoryWarehouse(Inv.Item_ID, fromWhs.Warehouse_ID);
                if (FromInvWhs != null)
                {
                    FromQty = FromInvWhs.Quantity;
                    FromID = FromInvWhs.Warehouse_ID;
                    _connection.Delete<InventoryWarehouse>(FromInvWhs.WhsItem_ID);
                }
                var w2 = new InventoryWarehouse
                {
                    Item_ID = Inv.Item_ID,
                    Warehouse_ID = FromID,
                    Warehouse_Name = FromInvWhs.Warehouse_Name,
                    Quantity = FromQty + Convert.ToInt32(quantity)
                };
                // _connection.Insert(w2);
                w22 = w2;
            }


            if (transfer)
            {
                w11.Quantity = ToQty + Convert.ToInt32(quantity);
                w22.Quantity = FromQty - Convert.ToInt32(quantity);
                _connection.Insert(w11);
                _connection.Insert(w22);
            }
            else if (string.IsNullOrEmpty(toWarehouse))
            {
                w22.Quantity = FromQty - Convert.ToInt32(quantity);
                _connection.Insert(FromInvWhs);

            }
            else
            {
                w11.Quantity = ToQty + Convert.ToInt32(quantity);
                _connection.Insert(ToInvWhs);
            }



        }

    }
}
