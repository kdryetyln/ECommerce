using Hotel.Domain.Domains.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Domains
{
    public class Customer:BaseEntity
    {
        public string Email { get; set; }
        public int Password { get; set; }
        public string Name { get; set; }

    }
}
