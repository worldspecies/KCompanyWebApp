using KCompanyWebApp.Interface;
using KCompanyWebApp.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System;

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
            this.ViewBag.Orders = dbContext.TrOrderHdrs.Where(f => f.ActiveFlag == "Y").ToList<TrOrderHdr>();
            this.ViewBag.Message = null;
            return View();
        }

        public IActionResult Edit(string OrderNo)
        {
            if (HttpContext.Session.GetString("EmployeeID") == null)
            {
                LogOut();
            }
            var RoleAccess = HttpContext.Session.GetListObjectFromSession<List<MsPage>>("PageAccess");
            this.ViewBag.OrderNo = OrderNo;
            this.ViewBag.RoleAccess = RoleAccess;

            var dbContext = new MyDBContext();
            this.ViewBag.OrderHdr = dbContext.TrOrderHdrs.Where(f => f.OrderNo == OrderNo).FirstOrDefault<TrOrderHdr>();
            this.ViewBag.Message = null;
            return View(); //Default Edit
        }

        [HttpPost]
        public IActionResult Update(TrOrderHdr RevisedOrder)
        {
            if (RevisedOrder != null)
            {
                var dbContext = new MyDBContext();
                dbContext.TrOrderHdrs.Update(RevisedOrder);
                dbContext.SaveChanges();
            }

            ViewBag.Message = "UpdateSuccessful";
            return View("Edit"); //Default Edit
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