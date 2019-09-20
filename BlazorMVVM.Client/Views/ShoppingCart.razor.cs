using Microsoft.AspNetCore.Components;
using BlazorMVVM.Client.ViewModels;
using BlazorMVVM.Shared;
using BlazorMVVM.Client.Components;
using System;

namespace BlazorMVVM.Client.Views
{
    public class ShoppingCartBase : ComponentBase
    {
        [Inject]
        public IShoppingCartViewModel ViewModel { get; set; }

        protected override void OnInitialized()
        {
            ViewModel.AddItemToCart(1);
            ViewModel.AddItemToCart(2);
            ViewModel.AddItemToCart(3);
            ViewModel.UpdateCart();
        }

        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);
            Console.WriteLine("Shopping Cart View OnAfterRender");       
        }

    }
}
