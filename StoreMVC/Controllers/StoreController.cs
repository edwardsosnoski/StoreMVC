using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StoreMVC.Models;
using StoreMVC.StoreServices;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StoreMVC.Controllers
{
    public class StoreController : Controller
    {
        private readonly IInventoryService _inventoryService;
        private readonly IShoppingCartService _shoppingCartService;

        public StoreController(IInventoryService inventoryService, IShoppingCartService shoppingCartService)
        {
            _inventoryService = inventoryService;
            _shoppingCartService = shoppingCartService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetAllStoreProducts()
        {
            var storeItems = _inventoryService.GetAllProducts();
            return View(storeItems);
        }

        public IActionResult AddProduct()
        {
            return View();
        }

        public IActionResult ProductDetails(int ID)
        {
            var productDetails = _inventoryService.GetProductDetails(ID);

            return View(productDetails);
        }

        public IActionResult AddProductResult(AddProductViewModel userProduct)
        {
            var productViewModel = _inventoryService.AddProduct(userProduct);
            if (!ModelState.IsValid)
            {
                return View("Error", new ErrorViewModel
                { ErrorMessage = "Error product was not added correctly" });
            }
            else
            {
                return View("GetAllStoreProducts", productViewModel);
            }
        }

        public IActionResult SelectDeleteProduct()
        {
            var storeViewModel = _inventoryService.GetAllProducts();
            return View(storeViewModel);
        }

        public IActionResult DeleteResult(int ID)
        {
            var productViewModel = _inventoryService.DeleteProduct(ID);

            if (!ModelState.IsValid)
            {
                return View("Error", new ErrorViewModel
                { ErrorMessage = "ERROR the product was not deleted correctly" });
            }

            else
            {
                return View("GetAllStoreProducts", productViewModel);
            }
        }

        public IActionResult SelectProductToUpdate()
        {
            var storeViewModel = _inventoryService.GetAllProducts();
            return View(storeViewModel);
        }

        public IActionResult UpdateProductInfo(int ID)
        {
            var oldProduct = _inventoryService.GetProductById(ID);
            var updatedProduct = new UpdateProductViewModel();
            updatedProduct.ProductID = oldProduct.ProductID;
            updatedProduct.ProductName = oldProduct.ProductName;
            updatedProduct.Quantity = oldProduct.Quantity;
            updatedProduct.Price = oldProduct.Price;

            return View(updatedProduct);
        }

        public IActionResult UpdateResult(UpdateProductViewModel userEdits)
        {
            var results = _inventoryService.UpdateProduct(userEdits);

            if (!ModelState.IsValid)
            {
                return View("Error", new ErrorViewModel
                { ErrorMessage = "ERROR product was not added correctly" });
            }

            else
            {
                return View("ProductDetails", results);
            }
        }

        public IActionResult AddToCart(int ID)
        {
            var result = _shoppingCartService.AddToCart(ID);
            return View(result);
        }

       public IActionResult ViewShoppingCart()
        {
            var shoppingCartItems = _shoppingCartService.ViewCart();
            return View(shoppingCartItems);
        }
    }
}
