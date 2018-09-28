using Hotel.Core.Services;
using Hotel.Domain.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eticaret.Admin.Controllers
{
    public class RoomController : Controller
    {
        // GET: Room
        public ActionResult Room()
        {
            var hotelId=Core.SessionSetting.SessionSetting.SessionSet<Hotel.Domain.Domains.Hotel>.Get("LoginHotel").Id;
            if(hotelId!=null)
            {
                var listRoomType = Service.RoomService.GetRoomTypes();
                Service.LoginService.AddRedis();
                return View(listRoomType);
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
            
        }       

        [HttpPost]
        public ActionResult AddRoom(Room room, HttpPostedFileBase image)
        {
            room.OtelId = Core.SessionSetting.SessionSetting.SessionSet<Hotel.Domain.Domains.Hotel>.Get("LoginHotel").Id;
            Service.RoomService.AddRoom(room,image);
            var listRoomType = Service.RoomService.GetRoomTypes();
            //Service.ElasticSearch.CreateIndex();
            return View("Room",listRoomType);
        }

         [HttpPost]
        public ActionResult AddRoomType(RoomType roomType)
        {
            Service.RoomService.AddRoomType(roomType);
            return RedirectToAction("Room");
        }

        public PartialViewResult _SetRoomTypes()
        {
            return PartialView();
        }

        public PartialViewResult _GetRoomList()
        {
            var listRoom = Service.RoomService.GetRoomList();
            return PartialView(listRoom);
        }
        
        public ActionResult DeleteRoom(int id)
        {
            Service.RoomService.DeleteRoom(id);
            return RedirectToAction("Room");
        }

        public ActionResult UpdateRoom(Room room, HttpPostedFileBase image)
        {
            Service.RoomService.UpdateRoom(room,image);
            return RedirectToAction("Room");
        }

      
    }
}