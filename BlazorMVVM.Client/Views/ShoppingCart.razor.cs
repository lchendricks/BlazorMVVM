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

        protected override void OnInit()
        {
            ViewModel.AddItemToCart(1);
            ViewModel.AddItemToCart(2);
            ViewModel.AddItemToCart(3);
            ViewModel.UpdateCart();

            ViewModel.StateHasChangedDelegate = StateHasChanged;
        }

        private void DoNothing()
        { }
        //protected override void StateHasChanged()
        //{

        //}

        protected override void OnAfterRender()
        {
            base.OnAfterRender();
            Console.WriteLine("Shopping Cart View OnAfterRender");       
        }

    }
}
