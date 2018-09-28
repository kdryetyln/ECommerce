using Hotel.Common.Models.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eticaret.Common.Models.Hotel
{
    public class HotelRoomDto:BaseDto
    {
       public ICollection<RoomDto> roomDtos { get; set; }
        
        public int Id { get; set; }
        public int hotelId { get; set; }
        public string Email { get; set; }
        public string TelNum { get; set; }
        public string HotelName { get; set; }
        public string Address { get; set; }
        public bool Park { get; set; }
        public bool Restaurant { get; set; }
        public bool HotelBar { get; set; }
        public bool Spa { get; set; }
        public int AreaId { get; set; }
        public bool Terrace { get; set; }
        public bool WashingMachine { get; set; }
        public bool RoomService { get; set; }
        public bool Gym { get; set; }
        public bool Pool { get; set; }

    }

    public class RoomDto
    {
        public int OtelId { get; set; }
        public int RoomId { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public string Name { get; set; }
        public int PersonCount { get; set; }
        public bool Wifi { get; set; }
        public bool TV { get; set; }
        public bool Bathroom { get; set; }
        public bool airConditioning { get; set; }
        public bool Fund { get; set; }
        public bool Telephone { get; set; }
        public bool MiniBar { get; set; }
        public bool Jakuzi { get; set; }
        public ICollection<RoomImageDto> roomImageDtos { get; set; }
    }

    public class RoomImageDto
    {
        public string Title { get; set; }
        public string Path { get; set; }
        public bool IsActive { get; set; }
    }
}
