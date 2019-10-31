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
        StoreViewModel AddProduct(AddProductViewModel userProduct);
        StoreViewModel DeleteProduct(int ID);
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

            //var products = new List<StoreProduct>();

            //foreach (var dalStoreProduct in dalStoreProducts)
            //{
            //    var storeProduct = new StoreProduct();
            //    storeProduct.ProductID = dalStoreProduct.ProductID;
            //    storeProduct.ProductName = dalStoreProduct.ProductName;
            //    storeProduct.Price = dalStoreProduct.Price;
            //    storeProduct.Quantity = dalStoreProduct.Quantity;
            //    products.Add(storeProduct);
            //}

            //var storeViewModel = new StoreViewModel();
            //storeViewModel.ListOfProducts = products;

            return MapDalToProduct(dalStoreProducts);
        }

        public StoreViewModel AddProduct(AddProductViewModel userProduct)
        {
            var dalModel = new StoreDALModel();
            dalModel.ProductName = userProduct.ProductName;
            dalModel.Quantity = userProduct.Quantity;
            dalModel.Price = userProduct.Price;
            _inventoryStore.InsertNewProduct(dalModel);
            var dalProducts = _inventoryStore.SelectAllProducts();
            return MapDalToProduct(dalProducts);

        }

        public StoreViewModel DeleteProduct(int ID)
        {
            var dalProduct = _inventoryStore.SelectProductID(ID);
            _inventoryStore.DeleteProduct(dalProduct);
            var dalProducts = _inventoryStore.SelectAllProducts();

            return MapDalToProduct(dalProducts);
        }

        private StoreViewModel MapDalToProduct(IEnumerable<StoreDALModel> dalModel)
        {
            var products = new List<StoreProduct>();

            foreach (var dalStoreProduct in dalModel)
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
