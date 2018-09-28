using Hotel.Domain.Domains;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Core.Database
{
    class EticaretDbContext:DbContext
    {
        public EticaretDbContext():base("EticaretConnStr")
        {

        }

        DbSet<Area> Areas { get; set; }
        DbSet<Campaign> Campaigns { get; set; }
        DbSet<Country> Countries { get; set; }
        DbSet<Customer> Customers { get; set; }
        DbSet<Domain.Domains.Hotel> Otels { get; set; }
        DbSet<Reservation> Reservations { get; set; }
        DbSet<Room> Rooms { get; set; }
        DbSet<RoomImage> RoomImages { get; set; }
        DbSet<RoomType> RoomTypes { get; set; }




    }
}
