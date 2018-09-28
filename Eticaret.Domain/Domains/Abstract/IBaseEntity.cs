using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Domains.Abstract
{
    public interface IBaseEntity
    {
        int Id { get; set; }
        DateTime CreateTime { get; set; }
        bool IsDeleted { get; set; }
    }
}
