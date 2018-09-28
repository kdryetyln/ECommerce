using Hotel.Domain.Domains.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Domains
{
    public class Room: BaseEntity
    {
        public Hotel Otel { get; set; }
        public int OtelId { get; set; }

        public RoomType RoomType { get; set; }
        public int RoomTypeId { get; set; }

        public int Stock { get; set; }
        public decimal Price { get; set; }

        public ICollection<RoomImage> roomImages { get; set; }


    }
}
