using Hotel.Domain.Domains.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Domains
{
    public class RoomImage:BaseEntity
    {
        public string Title { get; set; }
        public string Path { get; set; }
        public bool IsActive { get; set; }

        public Room Room { get; set; }
        public int RoomId { get; set; }

    }
}
