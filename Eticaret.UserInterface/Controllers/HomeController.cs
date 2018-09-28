using Eticaret.Common.Models.Hotel;
using Eticaret.Core.SessionSetting;
using Hotel.Core.Repository;
using Hotel.Core.Services;
using Hotel.Domain.Domains;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Eticaret.UserInterface.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            Service.ElasticSearch.CreateIndex();
            
            
            var customer = Service.LoginService.GetCustomer();

            Session["name"] = customer.Name;
            var model = Service.RoomService.hotelRooms();
            return View(model);
        }

        public ActionResult GoToHotel(int id)
        {
            var model = Service.RoomService.hotelRooms();
            var hotels = new HotelRoomDto();
            foreach (var item in model)
            {
                if(item.hotelId==id)
                {
                    hotels = item;
                    break;
                }
            }

            return View(hotels);

        }

        [HttpPost]
        public ActionResult AddRezervation(Reservation reservation)
        {
            decimal price;
            int hotelID;

            using (BaseRepository<Room> _br=new BaseRepository<Room>())
            {
                var room = _br.Query<Room>().FirstOrDefault(k => k.Id == reservation.RoomId);
                price = _br.Query<Room>().FirstOrDefault(k => k.Id == reservation.RoomId).Price;
                var hotel = _br.Query<Hotel.Domain.Domains.Hotel>().FirstOrDefault(k => k.Id == room.OtelId);
                hotelID = hotel.Id;
            }

            reservation.CustomerId = Service.LoginService.GetCustomer().Id;
            reservation.TotalPrice = price;
            Service.RabbitMQService.AddReservation(reservation);
            //Service.RabbitMQService.GetReservation(hotelID);
            return RedirectToAction("Index");

        }

        [HttpGet]
        public ActionResult Search(string key)
        {
            var model=Service.ElasticSearch.Search(key);
            return Json(model,JsonRequestBehavior.AllowGet);

        }


    }
}