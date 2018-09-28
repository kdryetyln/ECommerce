using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace Eticaret.Core
{
    public class ReservationHub : Hub
    {
        public ReservationHub()
        {

        }
        private static string conString = ConfigurationManager.ConnectionStrings["EticaretConnStr"].ToString();

        public void Hello()
        {
            Clients.All.hello();
        }

        [HubMethodName("getReservation")]
        public static void GetReservation()
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<ReservationHub>();
            context.Clients.All.updateMessages();
        }
    }
}