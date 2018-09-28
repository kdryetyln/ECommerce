using Hotel.Domain.Domains.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Domains
{
    public class Campaign:BaseEntity
    {
        public RoomType RoomType { get; set; }
        public int RoomTypeId { get; set; }

        public int Sale { get; set; }

    }
}
