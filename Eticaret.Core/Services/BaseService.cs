using Hotel.Common.Models.General;
using Hotel.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Core.Services
{
    public abstract class BaseService : IService
    {
        public BaseService()
        {
            //Todo: Injection
            this.UnitOfWork = new UnitOfWork();
        }

        public ServiceResult Result { get; set; }
        public UnitOfWork UnitOfWork { get; set; }

        void SetResultSuccess(string message)
        {
            this.Result = new ServiceResult { IsSuccess = true, Message = message };
        }
        void SetResultSuccess(string message, object data)
        {
            this.Result = new ServiceResult { IsSuccess = true, Message = message, Data = data };
        }

        void SetResultFail(string message, object data)
        {
            this.Result = new ServiceResult { IsSuccess = false, Message = message, Data = data };
        }
        void SetResultFail(string message)
        {
            this.Result = new ServiceResult { IsSuccess = false, Message = message };
        }
    }
}
