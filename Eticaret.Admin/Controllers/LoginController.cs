using Hotel.Core.Services;
using Hotel.Domain.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eticaret.Admin.Controllers
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

        public PartialViewResult _GetAreas()
        {
            var listArea = Service.LoginService.GetAreas();
            return PartialView(listArea);
        }


        [HttpPost]
        public ActionResult Login(Hotel.Domain.Domains.Hotel hotel)
        {
            var result = Service.LoginService.HotelLogin(hotel);
            if (result != null)
            {
                Core.SessionSetting.SessionSetting.SessionSet<Hotel.Domain.Domains.Hotel>.Set(result, "LoginHotel");
                var hotelOwner = Core.SessionSetting.SessionSetting.SessionSet<Hotel.Domain.Domains.Hotel>.Get("LoginHotel");
                Session["nameHotel"] = hotelOwner.HotelName;
                if(hotelOwner.Id!=null)
                {
                    Service.LoginService.AddRedis();
                    
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return View();
                }
               
            }

            else
            {
                ModelState.AddModelError("", "Failed to login. Please try again.");
                return RedirectToAction("Login");
            }

        }

        [HttpPost]
        public ActionResult Register(Hotel.Domain.Domains.Hotel hotel)
        {
            var result = Service.LoginService.HotelRegister(hotel);
            if (result == "Success")
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