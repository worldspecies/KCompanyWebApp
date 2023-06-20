using KCompanyWebApp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace KCompanyWebApp.Controllers
{
    public class ReportController : Controller
    {
        private readonly ILogger<LandingController> _logger;

        public ReportController(ILogger<LandingController> logger)
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
            this.ViewBag.Message = null;
            return View();
        }

        [HttpGet]
        public IActionResult GetSalesReport()
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetString("EmployeeID")))
            {
                return RedirectToAction("Index", "Home");
            }
            var RoleAccess = HttpContext.Session.GetListObjectFromSession<List<MsPage>>("PageAccess");
            this.ViewBag.RoleAccess = RoleAccess;

            var EmployeeArea = HttpContext.Session.GetString("EmployeeArea");
            var dbContext = new MyDBContext();
            this.ViewBag.ReportData = (from hdr in dbContext.TrOrderHdrs
                                      join dtl in dbContext.TrOrderDtls on hdr.OrderNo equals dtl.OrderNo
                                      join prod in dbContext.MsProductSpareparts on dtl.ProductNo equals prod.ProductNo
                                      where hdr.AreaNo == ( String.IsNullOrEmpty(EmployeeArea) ? hdr.AreaNo : EmployeeArea )
                                      orderby hdr.OrderNo, hdr.OrderDate, prod.ProductDesc
                                      select new
                                      {
                                          OrderNo = hdr.OrderNo,
                                          OrderDate = hdr.OrderDate,
                                          StoreNo = hdr.StoreNo,
                                          CustomerNo = hdr.CustomerNo,
                                          ProductNo = dtl.ProductNo,
                                          ProductDesc = prod.ProductDesc,
                                          ProductType = prod.ProductType,
                                          ProductBrand = prod.ProductBrand,
                                          UoM = prod.UoM,
                                          Cogs = prod.Cogs,
                                          Price = dtl.Price,
                                          Quantity = dtl.Quantity,
                                          Total = dtl.Total
                                      }).ToList();
            this.ViewBag.Message = null;
            return View("SalesReport");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}