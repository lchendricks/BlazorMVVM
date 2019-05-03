using BlazorMVVM.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorMVVM.Client.Models
{
    public interface IShoppingCart_Model
    {
        Dictionary<int, InventoryItem> CartItems { get; }

        void AddItem(int id, InventoryItem item);
        void RemoveItem(int id, InventoryItem item);
    }

    public class ShoppingCart_Model : IShoppingCart_Model
    {
        private Dictionary<int, InventoryItem> _cartItems;

        public Dictionary<int, InventoryItem> CartItems { get => _cartItems; private set => _cartItems = value; }

        public ShoppingCart_Model()
        {
            _cartItems = new Dictionary<int, InventoryItem>();
        }

        public void AddItem(int id, InventoryItem item)
        {
            if (!_cartItems.ContainsKey(id))
            {
                _cartItems.Add(id, item);
            }
            else
            {
                _cartItems[id].Quantity += item.Quantity;
            }
        }

        public void RemoveItem(int id, InventoryItem item)
        {
            if (_cartItems[id].Quantity > 1)
            {
                _cartItems[id].Quantity -= 1;
            }
            else
            {
                _cartItems.Remove(id);
            }
        }
    }
}
