using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Common.Models.General
{
    public class ServiceResult
    {

        public bool IsSuccess { get; set; }
        public string Message { get; set; }

        public object Data { get; set; }
    }
}
