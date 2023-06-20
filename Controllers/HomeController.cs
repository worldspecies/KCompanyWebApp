using KCompanyWebApp.Interface;
using KCompanyWebApp.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;

namespace KCompanyWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ILogin _loginUser;

        public HomeController(ILogger<HomeController> logger, ILogin loguser)
        {
            _logger = logger;
            _loginUser = loguser;
        }

        public IActionResult Index()
        {
            this.ViewBag.Message = null;
            return View();
        }

        [HttpPost]
        public IActionResult Login(string UserId, string Password)
        {
            var issuccess = _loginUser.AuthenticateUser(UserId, Password);
            if (issuccess != null)
            {
                var RoleNo = (String.IsNullOrEmpty(issuccess.Role) ? "" : issuccess.Role.ToString());
                var AreaNo = (String.IsNullOrEmpty(issuccess.AreaNo) ? "" : issuccess.AreaNo.ToString());
                HttpContext.Session.SetString("EmployeeID", UserId);
                HttpContext.Session.SetString("EmployeeRole", RoleNo);
                HttpContext.Session.SetString("EmployeeArea", AreaNo);

                var dbContext = new MyDBContext();
                var roleAccesses = dbContext.MsRoleAccesses.Where(f => f.Role == RoleNo);
                var pageAccesses = dbContext.MsPages.Where(f => f.ActiveFlag == "Y" && roleAccesses.Any(ra => ra.PageNo == f.PageNo)).ToList<MsPage>();
                HttpContext.Session.SetListObjectInSession<MsPage>("PageAccess", pageAccesses);

                ViewBag.Message = string.Format("Successfully logged-in", UserId);
                return RedirectToAction("Index", "Landing");
            }
            else
            {
                ViewBag.Message = string.Format("Login Failed ", UserId);
                return RedirectToAction(nameof(Index));
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}