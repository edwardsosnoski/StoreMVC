using StoreMVC.DAL;
using StoreMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreMVC.StoreServices
{
    public interface IInventoryService
    {
        StoreViewModel GetAllProducts();
    }

    public class InventoryService: IInventoryService
    {
        private readonly IInventoryStore _inventoryStore;

        public InventoryService(IInventoryStore inventoryStore)
        {
            _inventoryStore = inventoryStore;
        }

        public StoreViewModel GetAllProducts()
        {
            var dalStoreProducts = _inventoryStore.SelectAllProducts();

            var products = new List<StoreProduct>();

            foreach (var dalStoreProduct in dalStoreProducts)
            {
                var storeProduct = new StoreProduct();
                storeProduct.ProductID = dalStoreProduct.ProductID;
                storeProduct.ProductName = dalStoreProduct.ProductName;
                storeProduct.Price = dalStoreProduct.Price;
                storeProduct.Quantity = dalStoreProduct.Quantity;
                products.Add(storeProduct);
            }

            var storeViewModel = new StoreViewModel();
            storeViewModel.ListOfProducts = products;

            return storeViewModel;
        }
    }
}
