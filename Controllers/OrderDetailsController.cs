using KCompanyWebApp.Interface;
using KCompanyWebApp.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace KCompanyWebApp.Controllers
{
    public class OrderDetailsController : Controller
    {
        private readonly ILogger<LandingController> _logger;

        public OrderDetailsController(ILogger<LandingController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(string OrderNo)
        {
            if(HttpContext.Session.GetString("EmployeeID")  == null)
            {
                LogOut();
            }
            var RoleAccess = HttpContext.Session.GetListObjectFromSession<List<MsPage>>("PageAccess");
            this.ViewBag.OrderNo = OrderNo;
            this.ViewBag.EmployeeID = HttpContext.Session.GetString("EmployeeID");
            this.ViewBag.EmployeeRole = HttpContext.Session.GetString("EmployeeRole");
            this.ViewBag.EmployeeArea = HttpContext.Session.GetString("EmployeeArea");
            this.ViewBag.RoleAccess = RoleAccess;

            var dbContext = new MyDBContext();
            this.ViewBag.OrderHdr = dbContext.TrOrderHdrs.Where(f => f.OrderNo == OrderNo).FirstOrDefault<TrOrderHdr>();
            this.ViewBag.OrderDtl = dbContext.TrOrderDtls.Where(f => f.OrderNo == OrderNo).ToList<TrOrderDtl>();
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