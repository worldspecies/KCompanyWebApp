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
            if (String.IsNullOrEmpty(HttpContext.Session.GetString("EmployeeID")))
            {
                return RedirectToAction("Index", "Home");
            }
            var RoleAccess = HttpContext.Session.GetListObjectFromSession<List<MsPage>>("PageAccess");
            this.ViewBag.EmployeeID = HttpContext.Session.GetString("EmployeeID");
            this.ViewBag.EmployeeRole = HttpContext.Session.GetString("EmployeeRole");
            this.ViewBag.EmployeeArea = HttpContext.Session.GetString("EmployeeArea");
            this.ViewBag.RoleAccess = RoleAccess;

            var dbContext = new MyDBContext();
            this.ViewBag.Products = dbContext.MsProductSpareparts.Where(f => f.ActiveFlag == "Y").ToList<ProductServiceModel>();
            this.ViewBag.Message = null;
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}