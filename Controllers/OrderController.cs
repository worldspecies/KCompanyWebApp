using KCompanyWebApp.Interface;
using KCompanyWebApp.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text.RegularExpressions;

namespace KCompanyWebApp.Controllers
{
    public class OrderController : Controller
    {
        private readonly ILogger<OrderController> _logger;

        public OrderController(ILogger<OrderController> logger)
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
            this.ViewBag.Orders = dbContext.TrOrderHdrs.Where(f => f.ActiveFlag == "Y").ToList<TrOrderHdr>();
            this.ViewBag.Message = null;
            return View();
        }

        public IActionResult Add()
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetString("EmployeeID")))
            {
                return RedirectToAction("Index", "Home");
            }
            var RoleAccess = HttpContext.Session.GetListObjectFromSession<List<MsPage>>("PageAccess");
            this.ViewBag.RoleAccess = RoleAccess;

            var dbContext = new MyDBContext();
            var newOrder = new TrOrderHdr();

            var prevOrder = dbContext.TrOrderHdrs.OrderByDescending(f => f.OrderNo).FirstOrDefault();
            
            var newOrderNumb = "00001";
            var employeeID = HttpContext.Session.GetString("EmployeeID");
            if (prevOrder != null)
            {
                Regex regexObj = new Regex(@"[^\d]");
                newOrderNumb = regexObj.Replace(prevOrder.OrderNo, "");
            }

            int parsedOrderNumb = Convert.ToInt32(newOrderNumb);
            parsedOrderNumb += 1;
            newOrderNumb = "" + parsedOrderNumb;

            newOrder.OrderNo = "ORD" + newOrderNumb.PadLeft(5, '0');
            newOrder.OrderDate = DateTime.Now;
            newOrder.GrandTotal = 0;
            newOrder.CrtUsrId = String.IsNullOrEmpty(employeeID) ? "" : employeeID;
            newOrder.TsCrt = DateTime.Now;
            newOrder.ModUsrId = String.IsNullOrEmpty(employeeID) ? "" : employeeID;
            newOrder.TsMod = DateTime.Now;
            newOrder.ActiveFlag = "Y";

            this.ViewBag.Message = null;
            return View("Add", newOrder); //Default Edit
        }

        [HttpPost]
        public IActionResult Create(TrOrderHdr NewOrder)
        {
            var RoleAccess = HttpContext.Session.GetListObjectFromSession<List<MsPage>>("PageAccess");
            this.ViewBag.RoleAccess = RoleAccess;

            if (NewOrder != null)
            {
                var dbContext = new MyDBContext();
                dbContext.TrOrderHdrs.Add(NewOrder);
                dbContext.SaveChanges();
            }

            ViewBag.Message = "Save Successful";
            return View("Index"); //Default Edit
        }

        public IActionResult Edit(string OrderNo)
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetString("EmployeeID")))
            {
                return RedirectToAction("Index", "Home");
            }
            var RoleAccess = HttpContext.Session.GetListObjectFromSession<List<MsPage>>("PageAccess");
            this.ViewBag.OrderNo = OrderNo;
            this.ViewBag.RoleAccess = RoleAccess;

            var dbContext = new MyDBContext();
            var Order = dbContext.TrOrderHdrs.Where(f => f.ActiveFlag == "Y" && f.OrderNo == OrderNo).FirstOrDefault();
            this.ViewBag.Message = null;
            return View("Edit", Order); //Default Edit
        }

        [HttpPost]
        public IActionResult Update(TrOrderHdr RevisedOrder)
        {
            var RoleAccess = HttpContext.Session.GetListObjectFromSession<List<MsPage>>("PageAccess");
            this.ViewBag.RoleAccess = RoleAccess;

            if (RevisedOrder != null)
            {
                var employeeID = HttpContext.Session.GetString("EmployeeID");
                RevisedOrder.ModUsrId = String.IsNullOrEmpty(employeeID) ? "" : employeeID;
                RevisedOrder.TsMod = DateTime.Now;

                var dbContext = new MyDBContext();
                dbContext.TrOrderHdrs.Update(RevisedOrder);
                dbContext.SaveChanges();
            }

            ViewBag.Message = "Update Successful";
            return View("Edit", RevisedOrder); //Default Edit
        }

        public IActionResult Void(string OrderNo)
        {
            var dbContext = new MyDBContext();
            var Order = dbContext.TrOrderHdrs.Where(f => f.ActiveFlag == "Y" && f.OrderNo == OrderNo).FirstOrDefault();

            if (Order != null)
            {
                var employeeID = HttpContext.Session.GetString("EmployeeID");
                Order.ModUsrId = String.IsNullOrEmpty(employeeID) ? "" : employeeID;
                Order.TsMod = DateTime.Now;
                Order.ActiveFlag = "N";

                dbContext.TrOrderHdrs.Update(Order);
                dbContext.SaveChanges();
            }

            ViewBag.Message = "Void Successful";
            return RedirectToAction("Index", "Order");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}