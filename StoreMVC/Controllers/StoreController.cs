using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
    }
}
