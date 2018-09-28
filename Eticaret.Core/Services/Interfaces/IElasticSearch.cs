using Eticaret.Common.Models.Hotel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eticaret.Core.Services.Interfaces
{
    public interface IElasticSearch
    {
        void CreateIndex();
        List<HotelRoomDto> Search(string key);

    }
}
