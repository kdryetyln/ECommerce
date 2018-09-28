using Hotel.Common.Models.General;
using Hotel.Domain.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eticaret.Common.Models.RoomDtos
{
    public class RoomDtos:BaseDto
    {

        public Room Room { get; set; }
        public RoomType RoomType { get; set; }
        public RoomImage RoomImage { get; set; }
    }
}
