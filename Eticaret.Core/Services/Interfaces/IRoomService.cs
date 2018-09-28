using Eticaret.Common.Models.Hotel;
using Eticaret.Common.Models.RoomDtos;
using Hotel.Core.Services.Interfaces;
using Hotel.Domain.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using RoomDto = Eticaret.Common.Models.Hotel.RoomDto;

namespace Eticaret.Core.Services.Interfaces
{
    public interface IRoomService:IService
    {
        void AddRoom(Room room, HttpPostedFileBase image);
        IEnumerable<RoomType> GetRoomTypes();
        IEnumerable<RoomDtos> GetRoomList();
        void AddRoomType(RoomType roomType);
        void DeleteRoom(int id);
        void UpdateRoom(Room room, HttpPostedFileBase image);
        List<HotelRoomDto> hotelRooms();
    }
}
