using Eticaret.Core.SessionSetting;
using Hotel.Core.Repository;
using Hotel.Core.Services;
using Hotel.Domain.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hotel.Admin.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {            
            return View();
        }

        public ActionResult ReservationRequests()
        {
           
            return View();
        }

        public ActionResult GetRes()
        {
            BaseRepository<Reservation> rp = new BaseRepository<Reservation>();
            var model = rp.GetAllReservation();
            ViewBag.count = model.Count();
            return PartialView("_GetReservation",model);
        }


        [HttpPost]
        public ActionResult Approve(int id)
        {
            Service.RabbitMQService.UpdateReservation(id, true);
            return RedirectToAction("ReservationRequests");
        }
        
        [HttpPost]
        public ActionResult Reject(int id)
        {
            Service.RabbitMQService.UpdateReservation(id, false);
            return RedirectToAction("ReservationRequests");
        }
    }
}