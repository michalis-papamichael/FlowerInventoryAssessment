using App.Helpers;
using App.Models;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using ServiceLayer.ServiceDtos.Flowers;
using ServiceLayer.ServiceResponder;
using ServiceLayer.Services;

namespace App.Controllers
{
    public class InventoryController : Controller
    {
        private readonly FlowersServices _flowersServices;
        public InventoryController(FlowersServices flowersServices)
        {
            _flowersServices = flowersServices;
        }
        public IActionResult Details()
        {
            ViewData["Layout"] = "_InventoryLayout";
            return View();
        }
}
