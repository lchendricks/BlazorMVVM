using BlazorMVVM.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorMVVM.Client.ViewModels
{
    public interface IInventoryCatalog_ViewModel
    {
        Dictionary<int, InventoryItem> CatalogItems { get; }
        Action AddItemsButtonClickDelegate { get; set; }
        
        Action<int> AddSelectedItemToCartDelegate { get; set; }
        KeyValuePair<int, InventoryItem> SelectedItem { get; set; }
  
        void ClickCancel();
        void PopulateCatalog(Dictionary<int, InventoryItem> inventory);
        void SelectItem(int id);
    }

    public class InventoryCatalog_ViewModel : IInventoryCatalog_ViewModel
    {
        private Dictionary<int, InventoryItem> _catalogItems;

        public Dictionary<int, InventoryItem> CatalogItems { get => _catalogItems; private set => _catalogItems = value; }
        public Action AddItemsButtonClickDelegate { get; set; }
        public Action<int> AddSelectedItemToCartDelegate { get; set; }

        public KeyValuePair<int, InventoryItem> SelectedItem { get; set; }
   

        public InventoryCatalog_ViewModel()
        {
            _catalogItems = new Dictionary<int, InventoryItem>();
        }

        public void PopulateCatalog(Dictionary<int, InventoryItem> inventory)
        {
            _catalogItems = new Dictionary<int, InventoryItem>();
            foreach (var kvp in inventory)
            {
                var newItem = new InventoryItem { Name = kvp.Value.Name, Quantity = kvp.Value.Quantity, Price = kvp.Value.Price };
                _catalogItems.Add(kvp.Key, newItem);
            }
        }

        public void SelectItem(int id)
        {
            SelectedItem = new KeyValuePair<int, InventoryItem>(id, CatalogItems[id]);
        }

        public void ClickCancel()
        {
            SelectedItem = new KeyValuePair<int, InventoryItem>(0, null);
        }
    }
}
