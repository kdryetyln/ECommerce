using Hotel.Domain.Domains.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Domains
{
    public class Hotel : BaseEntity
    {
        public string Email { get; set; }
        public int Password { get; set; }
        public string TelNum { get; set; }
        public string HotelName { get; set; }
        public string Address { get; set; }
        public bool Park { get; set; }
        public bool Restaurant { get; set; }
        public bool HotelBar { get; set; }
        public bool Spa { get; set; }
        public Area Area { get; set; }
        public int AreaId { get; set; }
        public bool Terrace { get; set; }
        public bool WashingMachine { get; set; }
        public bool RoomService { get; set; }
        public bool Gym { get; set; }
        public bool Pool { get; set; }
        public ICollection<Room> rooms { get; set; }


    }
}
