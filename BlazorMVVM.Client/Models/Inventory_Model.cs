using BlazorMVVM.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorMVVM.Client.Models
{
    public interface IInventory_Model
    {
        Dictionary<int, InventoryItem> Inventory { get; }

        bool HasAvailableInventory(int id, int qty);
    }

    public class Inventory_Model : IInventory_Model
    {
        private Dictionary<int, InventoryItem> _inventory;

        public Dictionary<int, InventoryItem> Inventory { get => _inventory; private set => _inventory = value; }

        public Inventory_Model()
        {
            _inventory = new Dictionary<int, InventoryItem>();
            StockInventory(1, "Intel Core i9-7980XE Skylake X 18", 5, 2049.99m);
            StockInventory(2, "ASUS ROG Strix X99-E", 5, 299.99m);
            StockInventory(3, "EVGA GeForce RTX 2080 Ti", 5, 1069.99m);
            StockInventory(4, "G.SKILL Ripjaws V 32GB DDR 3200", 6, 199.99m);
            StockInventory(5, "Cooler Master Cosmos II Full Tower", 4, 199.99m);
            StockInventory(6, "Samsung 970 EVO Plus M.2 1TB", 3, 247.49m);
        }

        private void StockInventory(int id, string name, int qty, decimal price)
        {
            var newItem = new InventoryItem { Name = name, Quantity = qty, Price = price };
            _inventory.Add(id, newItem);
        }

        public bool HasAvailableInventory(int id, int qty)
        {
            if (_inventory[id].Quantity >= qty)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
