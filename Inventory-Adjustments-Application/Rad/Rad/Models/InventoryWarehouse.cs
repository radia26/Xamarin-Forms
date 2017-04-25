using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;


namespace Rad.Models
{
    public class InventoryWarehouse
    {
        [PrimaryKey, AutoIncrement]
        public int WhsItem_ID { get; set; }


        public int Item_ID { get; set; }
        public int Warehouse_ID { get; set; }
        public string Warehouse_Name { get; set; }
        public int Quantity { get; set; }


        public InventoryWarehouse()
        {
        }
    }
}
