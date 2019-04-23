using BlazorMVVM.Shared;
using System.Collections.Generic;
using System.Globalization;

namespace BlazorMVVM.Client.ViewModels
{
    public interface IShoppingCartViewModel
    {
        List<InventoryItem> Cart { get; set; }

        void AddItemToCart(string name, int qty, decimal price);
        string DisplayPrice(decimal price);
        InventoryItem DisplayTotal();
    }

    public class ShoppingCartViewModel : IShoppingCartViewModel
    {
        private List<InventoryItem> _cart;

        public ShoppingCartViewModel()
        {
            _cart = new List<InventoryItem>();
        }

        public List<InventoryItem> Cart { get => _cart; set => _cart = value; }

        public void AddItemToCart(string name, int qty, decimal price)
        {
            var newItem = new InventoryItem { Name = name, Quantity = qty, Price = price };
            _cart.Add(newItem);
        }

        public InventoryItem DisplayTotal()
        {
            InventoryItem result = new InventoryItem();
            result.Name = "Total";

            foreach(var item in Cart)
            {
                result.Quantity += item.Quantity;
                result.Price += item.Price;
            }
            return result;
        }

        public string DisplayPrice(decimal price)
        {          
            var numberFormat = (NumberFormatInfo)CultureInfo.CurrentUICulture.NumberFormat.Clone();
            numberFormat.CurrencySymbol = "$";
            return price.ToString("C", numberFormat);
        }

    }
}
