using Hotel.Domain.Domains.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Domains
{
    public class Area:BaseEntity
    {
        public string Name { get; set; }
        public Country Country { get; set; }
        public int CountryId { get; set; }

    }
}
