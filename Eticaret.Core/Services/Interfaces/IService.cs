using Hotel.Common.Models.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Core.Services.Interfaces
{
    public interface IService
    {
        UnitOfWork UnitOfWork { get; set; }
        ServiceResult Result { get; set; }
    }
}
