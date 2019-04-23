using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace BlazorMVVM.Shared
{
    public class InventoryItem
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
