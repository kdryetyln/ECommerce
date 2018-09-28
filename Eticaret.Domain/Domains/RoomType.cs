using Hotel.Domain.Domains.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Domains
{
    public class RoomType: BaseEntity
    {
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

    }
}

