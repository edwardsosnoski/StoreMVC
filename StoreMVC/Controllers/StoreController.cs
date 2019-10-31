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

        public StoreController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
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
    }
}
