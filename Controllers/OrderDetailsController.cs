using KCompanyWebApp.Interface;
using KCompanyWebApp.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;

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
            var RoleAccess = HttpContext.Session.GetListObjectFromSession<List<MsPage>>("PageAccess");
            this.ViewBag.RoleAccess = RoleAccess;

            this.ViewBag.OrderNo = OrderNo;
            this.ViewBag.EmployeeID = HttpContext.Session.GetString("EmployeeID");
            this.ViewBag.EmployeeRole = HttpContext.Session.GetString("EmployeeRole");
            this.ViewBag.EmployeeArea = HttpContext.Session.GetString("EmployeeArea");

            var dbContext = new MyDBContext();
            this.ViewBag.OrderHdr = dbContext.TrOrderHdrs.Where(f => f.OrderNo == OrderNo).FirstOrDefault<TrOrderHdr>();
            this.ViewBag.OrderDtl = dbContext.TrOrderDtls.Where(f => f.OrderNo == OrderNo).ToList<TrOrderDtl>();
            this.ViewBag.Message = null;
            return View();
        }

        public IActionResult Add(string OrderNo)
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetString("EmployeeID")))
            {
                LogOut();
            }
            var RoleAccess = HttpContext.Session.GetListObjectFromSession<List<MsPage>>("PageAccess");
            this.ViewBag.RoleAccess = RoleAccess;
            this.ViewBag.OrderNo = OrderNo;

            var newOrderDtl = new TrOrderDtl();
            newOrderDtl.OrderNo = OrderNo;
            newOrderDtl.Quantity = 0;
            newOrderDtl.Price = 0;
            newOrderDtl.Total = 0;
            newOrderDtl.ActiveFlag = "Y";

            this.ViewBag.Message = null;
            return View("Add", newOrderDtl); //Default Edit
        }

        [HttpPost]
        public IActionResult Create(TrOrderDtl NewOrderDtl)
        {
            var RoleAccess = HttpContext.Session.GetListObjectFromSession<List<MsPage>>("PageAccess");
            this.ViewBag.RoleAccess = RoleAccess;

            var OrderNo = "";
            if (NewOrderDtl != null)
            {
                OrderNo = NewOrderDtl.OrderNo;
                var dbContext = new MyDBContext();
                dbContext.TrOrderDtls.Add(NewOrderDtl);
                dbContext.SaveChanges();

                var ExistingOrder = dbContext.TrOrderHdrs.Where(f => f.ActiveFlag == "Y" && f.OrderNo == OrderNo).FirstOrDefault();
                if (ExistingOrder != null)
                {
                    ExistingOrder.GrandTotal = ExistingOrder.GrandTotal + NewOrderDtl.Total;
                    dbContext.TrOrderHdrs.Update(ExistingOrder);
                    dbContext.SaveChanges();
                }
            }

            ViewBag.Message = "Save Successful";
            return RedirectToAction("Index", "OrderDetails", new { OrderNo = OrderNo });
        }

        public IActionResult Edit(string OrderNo, int OrderDtlID)
        {
            if (HttpContext.Session.GetString("EmployeeID") == null)
            {
                LogOut();
            }
            var RoleAccess = HttpContext.Session.GetListObjectFromSession<List<MsPage>>("PageAccess");
            this.ViewBag.OrderNo = OrderNo;
            this.ViewBag.RoleAccess = RoleAccess;

            var dbContext = new MyDBContext();
            var OrderDtl = dbContext.TrOrderDtls.Where(f => f.ActiveFlag == "Y" && f.OrderNo == OrderNo && f.OrderDtlID == OrderDtlID).FirstOrDefault();
            this.ViewBag.Message = null;
            return View("Edit", OrderDtl); //Default Edit
        }

        [HttpPost]
        public IActionResult Update(TrOrderDtl RevisedOrderDtl)
        {
            var RoleAccess = HttpContext.Session.GetListObjectFromSession<List<MsPage>>("PageAccess");
            this.ViewBag.RoleAccess = RoleAccess;

            var OrderNo = "";
            if (RevisedOrderDtl != null)
            {
                OrderNo = RevisedOrderDtl.OrderNo;

                var dbContext = new MyDBContext();
                dbContext.TrOrderDtls.Update(RevisedOrderDtl);
                dbContext.SaveChanges();

                //counting total
                decimal? grandTotal = 0;
                var ExistOrderDtl = dbContext.TrOrderDtls.Where(f => f.ActiveFlag == "Y" && f.OrderNo == OrderNo);
                if (ExistOrderDtl != null){
                    foreach(var b in ExistOrderDtl.ToList<TrOrderDtl>())
                    {
                        grandTotal += b.Total;
                    }
                }

                var ExistingOrder = dbContext.TrOrderHdrs.Where(f => f.ActiveFlag == "Y" && f.OrderNo == OrderNo).FirstOrDefault();
                if (ExistingOrder != null)
                {
                    ExistingOrder.GrandTotal = grandTotal;
                    dbContext.TrOrderHdrs.Update(ExistingOrder);
                    dbContext.SaveChanges();
                }
            }

            ViewBag.Message = "Update Successful";
            return RedirectToAction("Index", "OrderDetails", new { OrderNo = OrderNo });
        }

        public IActionResult Delete(int OrderDtlID)
        {
            var dbContext = new MyDBContext();
            var OrderDtl = dbContext.TrOrderDtls.Where(f => f.ActiveFlag == "Y" && f.OrderDtlID == OrderDtlID).FirstOrDefault();
            var OrderNo = "";

            if (OrderDtl != null)
            {
                OrderNo = OrderDtl.OrderNo;
                var ExistingOrder = dbContext.TrOrderHdrs.Where(f => f.ActiveFlag == "Y" && f.OrderNo == OrderNo).FirstOrDefault();
                if (ExistingOrder != null)
                {
                    ExistingOrder.GrandTotal = ExistingOrder.GrandTotal - OrderDtl.Total;
                    dbContext.TrOrderHdrs.Update(ExistingOrder);
                    dbContext.SaveChanges();
                }

                dbContext.TrOrderDtls.Remove(OrderDtl);
                dbContext.SaveChanges();
            }

            ViewBag.Message = "Remove Successful";
            return RedirectToAction("Index", "OrderDetails", new { OrderNo = OrderNo });
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