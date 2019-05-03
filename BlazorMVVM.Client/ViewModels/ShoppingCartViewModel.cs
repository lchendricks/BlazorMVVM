using BlazorMVVM.Shared;
using System.Collections.Generic;
using System.Globalization;
using BlazorMVVM.Client.Models;
using System;
using System.ComponentModel;

namespace BlazorMVVM.Client.ViewModels
{
    //public delegate void ButtonClickDelegate();
    //public delegate void ButtonClickWithIdDelegate(int id);
    //public delegate void StateHasChangedDelegate();

    public interface IShoppingCartViewModel
    {
        Dictionary<int, InventoryItem> Cart { get; set; }
        int ItemCount { get; }
        decimal TotalPrice { get; }
        IInventoryCatalog_ViewModel InventoryCatalogViewModel { get; }
        Action StateHasChangedDelegate { get; set; }

        void AddItemsButtonClick();
        void AddItemToCart(int id);       
        void UpdateCart();
    }

    public class ShoppingCartViewModel : IShoppingCartViewModel
    {      
        private Dictionary<int, InventoryItem> _contents;
        private IInventory_Model _inventoryModel;
        private IShoppingCart_Model _shoppingCartModel;
        private int _itemCount;
        private decimal _totalPrice;
        private IInventoryCatalog_ViewModel _inventoryCatalogViewModel;
  

        public ShoppingCartViewModel(IInventory_Model inventory_Model, IShoppingCart_Model shoppingCart_Model, 
            IInventoryCatalog_ViewModel inventoryCatalog_ViewModel)
        {
            _contents = new Dictionary<int, InventoryItem>();
            _inventoryModel = inventory_Model;
            _shoppingCartModel = shoppingCart_Model;
            _inventoryCatalogViewModel = inventoryCatalog_ViewModel;
            _inventoryCatalogViewModel.AddItemsButtonClickDelegate = AddItemsButtonClick;
            _inventoryCatalogViewModel.AddSelectedItemToCartDelegate = CheckInventoryAndAddItemToCart;                   
        }

        public Dictionary<int, InventoryItem> Cart { get => _contents; set => _contents = value; }
        public int ItemCount { get => _itemCount; private set => _itemCount = value; }
        public decimal TotalPrice { get => _totalPrice; private set => _totalPrice = value; }
        public IInventoryCatalog_ViewModel InventoryCatalogViewModel { get => _inventoryCatalogViewModel; private set => _inventoryCatalogViewModel = value; }
        public Action StateHasChangedDelegate { get; set; }

        public void AddItemToCart(int id)
        {
            var newItem = new InventoryItem { Name = _inventoryModel.Inventory[id].Name, Quantity = 1, Price = _inventoryModel.Inventory[id].Price };
            _shoppingCartModel.AddItem(id, newItem);            
        }   

        public void UpdateCart()
        {           
            var newCart = new Dictionary<int, InventoryItem>();
            _itemCount = 0;
            _totalPrice = 0m;
            foreach (var kvp in _shoppingCartModel.CartItems)
            {
                newCart.Add(kvp.Key, kvp.Value);
                _itemCount += kvp.Value.Quantity;
                _totalPrice += kvp.Value.Price * kvp.Value.Quantity;
            }
            Cart = newCart;
            StateHasChangedDelegate?.Invoke();
        }      
        
        public void AddItemsButtonClick()
        {
            if (_inventoryCatalogViewModel.CatalogItems.Count == 0)
            {
                _inventoryCatalogViewModel.PopulateCatalog(_inventoryModel.Inventory);
            }
            else
            {
                _inventoryCatalogViewModel.CatalogItems.Clear();
            }
        }

        public void CheckInventoryAndAddItemToCart(int id)
        {
            Console.WriteLine("Check Inventory And Add Item  To Cart: " + id.ToString());
            Console.WriteLine(_contents.Count.ToString() + " items in cart");
            //check inventory
            AddItemToCart(id);
            UpdateCart();
            Console.WriteLine(_contents.Count.ToString() + " items in cart");                       
        }
    }

    public static class ShoppingCartExtensions
    {
        public static string DisplayPrice(this decimal price)
        {
            var numberFormat = (NumberFormatInfo)CultureInfo.CurrentUICulture.NumberFormat.Clone();
            numberFormat.CurrencySymbol = "$";
            return price.ToString("C", numberFormat);
        }
    }
}
