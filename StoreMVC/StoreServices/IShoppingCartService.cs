﻿using StoreMVC.DAL;
using StoreMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreMVC.StoreServices
{
    public interface IShoppingCartService
    {
        ShoppingCartViewModel AddToCart(int ID);
    }

    public class ShoppingCartService:IShoppingCartService
    {
        private readonly ICartStore _cartStore;
        private readonly IInventoryStore _inventoryStore;

        public ShoppingCartService(ICartStore cartStore, IInventoryStore inventoryStore)
        {
            _cartStore = cartStore;
            _inventoryStore = inventoryStore;
        }

        public ShoppingCartViewModel AddToCart(int ID)
        {
            var cartDalModel = _inventoryStore.SelectProductID(ID);
            _cartStore.InsertIntoCart(cartDalModel);

            var dalModels = _cartStore.SelectAllInCart();
            var products = new  List<StoreProduct>();

            foreach (var dalModel in dalModels)
            {
                var storeCartProduct = new StoreProduct();
                storeCartProduct.ProductID = dalModel.ProductID;
                storeCartProduct.ProductName = dalModel.ProductName;
                storeCartProduct.Quantity = dalModel.Quantity;
                storeCartProduct.Price = dalModel.Price;
                products.Add(storeCartProduct);
            }

            var shoppingCartViewModel = new ShoppingCartViewModel();
            shoppingCartViewModel.ListOfShoppingCartProducts = products;

            return shoppingCartViewModel;
        }
    }
}