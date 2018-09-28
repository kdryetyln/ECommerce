using Hotel.Common.Models.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eticaret.Common.Models.Hotel
{
    public class ReservationDto:BaseDto
    {
        public int CustomerId { get; set; }
        public int RoomId { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalPrice { get; set; }
        public bool Status { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
    }
}
