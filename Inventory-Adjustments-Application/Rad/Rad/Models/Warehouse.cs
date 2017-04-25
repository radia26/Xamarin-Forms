using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;


namespace Rad.Models
{
    public class Warehouse
    {
        [PrimaryKey, AutoIncrement]
        public int Warehouse_ID { get; set; }

        [Unique]
        public string Name { get; set; }

        public int Item_ID { get; set; }


        public Warehouse()
        {
        }
    }
}
