using Hotel.Domain.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Core.Services.Interfaces
{
    public interface ILoginService:IService
    {
        string CustomerRegister(Customer custom);
        string HotelRegister(Domain.Domains.Hotel hotel);
        Customer CustomerLogin(Customer custom);        
        Domain.Domains.Hotel HotelLogin(Domain.Domains.Hotel hotel);
        IEnumerable<Area> GetAreas();
        void AddRedis();
        Customer GetCustomer();
    }
}
