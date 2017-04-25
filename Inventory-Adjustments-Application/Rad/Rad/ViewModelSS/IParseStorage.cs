using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rad.Models;

namespace Rad.ViewModel
{
    public interface IParseStorage
    {
        #region Inventory

        Task<List<Inventory>> RefreshDataAsync();

        Task SaveInventoryAsync(Inventory item);

        Task DeleteInventoryAsync(Inventory id);

        Task<bool> ExistInventoryAsync(Inventory item);

        #endregion


        #region InventoryWarehouse

        Task<bool> ExistItemWarehouseAsync(InventoryWarehouse item, string whs);

        Task SaveInventoryWarehouseAsync(InventoryWarehouse item);

        Task DeleteInventoryWarehouseAsync(InventoryWarehouse item);

      

        #endregion


    }
}
