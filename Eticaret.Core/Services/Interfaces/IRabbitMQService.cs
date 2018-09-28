using Eticaret.Common.Models.Hotel;
using Hotel.Core.Services.Interfaces;
using Hotel.Domain.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eticaret.Core.Services.Interfaces
{
    public interface IRabbitMQService:IService
    {
        void AddReservation(Reservation reservation);
        void GetReservation(int id);
        List<ReservationDto> GetReservationDb();
        void UpdateReservation(int id, bool key);

    }
}
