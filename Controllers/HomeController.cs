using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Silverbear1.Database;
using Silverbear1.Models;

namespace Silverbear1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult ProductList()
        {
            return View();
        }

        public JsonResult Products()
        {
            var context = new SilverbearContext();
            context.FetchAll();

            var products = context.Products.Select(p => new Models.Product
            {
                Id = p.Id,
                Cpu = p.Cpu.Maker + " " + p.Cpu.Name + (p.Cpu.ClockSpeed == null ? "" : " " + p.Cpu.ClockSpeed),
                Gpu = p.Gpu.Maker + " " + p.Gpu.Name,
                MemoryInGiB = p.MemoryInMib / 1024.0,
                Memory = p.MemoryInMib < 1024
                    ? p.MemoryInMib + " MiB"
                    : (p.MemoryInMib / 1024) + " GiB",
                Psu = p.PsuInW + " W",
                Storage = p.Storage.Capacity + " " + p.Storage.StorageType,
                Weight = p.WeightInKg + " Kg",
                Usb = (p.Usb2 > 0 ? p.Usb2 + " x USB 2.0": "").AppendNeatly
                      (p.Usb3 > 0 ? p.Usb3 + " x USB 3.0" : "").AppendNeatly
                      (p.UsbC > 0 ? p.UsbC + " x USB C" : "")
            });
            return Json(products.ToList());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }

    public static class StringUtils
    {
        public static string AppendNeatly(this string existing, string extra)
        {
            if (extra.Length == 0) return existing;
            var inter = existing.Length == 0 ? "" : "; ";
            return existing + inter + extra;
        }
    }
}