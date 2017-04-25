using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;


namespace Rad.Models
{
    public class Inventory
    {
        [PrimaryKey, AutoIncrement]
        public int Item_ID { get; set; }

        [Unique]
        public string Name { get; set; }

        public double Cost { get; set; }

        public string Vendor { get; set; }

        public double Weight { get; set; }

        public DateTime CreatedOn { get; set; }

        public Inventory()
        {
        }
    }

}