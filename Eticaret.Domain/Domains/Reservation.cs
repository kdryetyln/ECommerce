using Hotel.Domain.Domains.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Domains
{
    public class Reservation:BaseEntity
    {
        public Customer Customer { get; set; }
        public int CustomerId { get; set; }

        public Room Room { get; set; }
        public int RoomId { get; set; }

        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalPrice { get; set; }
        public bool Status { get; set; }

    }
}
