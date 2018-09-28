using Hotel.Core.Services;
using Hotel.Domain.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eticaret.UserInterface.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        public PartialViewResult _Login()
        {
            return PartialView();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Customer custom)
        {
            var result=Service.LoginService.CustomerLogin(custom);
            if (result!=null)
            {
                var customer = Service.LoginService.GetCustomer();
                Session["name"] = customer.Name;
                return RedirectToAction("Index", "Home");
            }
               
            else
            {
                ModelState.AddModelError("", "Failed to login. Please try again.");
                return RedirectToAction("Login");
            }
                
        }

        [HttpPost]
        public ActionResult Register(Customer custom)
        {
            var result=Service.LoginService.CustomerRegister(custom);
            if(result== "Success")
            {
                return RedirectToAction("Login");
            }
            else
            {
                ModelState.AddModelError("", "No Success!! Try Again with new email.");
                return View();
            }
            
        }

    }
}