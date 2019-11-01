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
        StoreProduct GetProductById(int ID);
        ProductDetailsViewModel GetProductDetails(int ID);
        ProductDetailsViewModel UpdateProduct(UpdateProductViewModel userUpdates);
        
    }

    public class InventoryService: IInventoryService
    {
        private readonly IInventoryStore _inventoryStore;
        private readonly ICartStore _cartStore;

        public InventoryService(IInventoryStore inventoryStore, ICartStore cartStore)
        {
            _inventoryStore = inventoryStore;
            _cartStore = cartStore;
        }

        public StoreViewModel GetAllProducts()
        {
            var dalStoreProducts = _inventoryStore.SelectAllProducts();

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
            var cartDalProduct = _cartStore.SelectFromCartById(ID);
            _cartStore.DeleteFromCart(cartDalProduct);
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

        public StoreProduct GetProductById(int ID)
        {
            var dalProduct = _inventoryStore.SelectProductID(ID);
            var storeProduct = new StoreProduct();
            storeProduct.ProductID = dalProduct.ProductID;
            storeProduct.ProductName = dalProduct.ProductName;
            storeProduct.Quantity = dalProduct.Quantity;
            storeProduct.Price = dalProduct.Price;

            return storeProduct;
        }

        public ProductDetailsViewModel GetProductDetails(int ID)
        {
            var dalProduct = _inventoryStore.SelectProductID(ID);
            var productDetails = new ProductDetailsViewModel();
            productDetails.ProductID = dalProduct.ProductID;
            productDetails.ProductName = dalProduct.ProductName;
            productDetails.Quantity = dalProduct.Quantity;
            productDetails.Price = dalProduct.Price;

            return productDetails;
        }

        public ProductDetailsViewModel UpdateProduct(UpdateProductViewModel userModel)
        {
            var dalModel = new StoreDALModel();
            dalModel.ProductID = userModel.ProductID;
            dalModel.ProductName = userModel.ProductName;
            dalModel.Quantity = userModel.Quantity;
            dalModel.Price = userModel.Price;
            _inventoryStore.UpdateProduct(dalModel);

            var cartDAL = new CartDALModel();
            cartDAL.ProductID = userModel.ProductID;
            cartDAL.ProductName = userModel.ProductName;
            cartDAL.Quantity = userModel.Quantity;
            cartDAL.Price = userModel.Price;
            _cartStore.UpdateOtherProperties(cartDAL);

            var productDetails = new ProductDetailsViewModel();
            productDetails.ProductID = dalModel.ProductID;
            productDetails.ProductName = dalModel.ProductName;
            productDetails.Quantity = dalModel.Quantity;
            productDetails.Price = dalModel.Price;

            return productDetails;

        }
    }
}
