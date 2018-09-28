using Eticaret.Core.Services;
using Eticaret.Core.Services.Interfaces;
using Hotel.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Core.Services
{
    public class Service
    {
        public static ILoginService LoginService
        {
            get
            {
                //Todo: Injection
                return (ILoginService)new LoginService();
            }
        }
        public static IRoomService RoomService
        {
            get
            {
                //Todo: Injection
                return (IRoomService)new RoomService();
            }
        }
        public static IElasticSearch ElasticSearch
        {
            get
            {
                //Todo: Injection
                return (IElasticSearch)new ElasticSearchService();
            }
        }
        public static IRabbitMQService RabbitMQService
        {
            get
            {
                //Todo: Injection
                return (IRabbitMQService)new RabbitMQService();
            }
        }
    }
}
