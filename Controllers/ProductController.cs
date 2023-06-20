using KCompanyWebApp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace KCompanyWebApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;

        public ProductController(ILogger<ProductController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if(HttpContext.Session.GetString("EmployeeID")  == null)
            {
                LogOut();
            }
            var RoleAccess = HttpContext.Session.GetListObjectFromSession<List<MsPage>>("PageAccess");
            this.ViewBag.EmployeeID = HttpContext.Session.GetString("EmployeeID");
            this.ViewBag.EmployeeRole = HttpContext.Session.GetString("EmployeeRole");
            this.ViewBag.EmployeeArea = HttpContext.Session.GetString("EmployeeArea");
            this.ViewBag.RoleAccess = RoleAccess;

            var dbContext = new MyDBContext();
            this.ViewBag.Products = dbContext.MsProductSpareparts.Where(f => f.ActiveFlag == "Y").ToList<MsProductSparepart>();
            this.ViewBag.Message = null;
            return View();
        }

        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}