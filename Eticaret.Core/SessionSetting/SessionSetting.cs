using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Eticaret.Core.SessionSetting
{
    public class SessionSetting
    {
        

        public static class SessionSet<T> where T : class, new()
        {

            public static T CurrentUser(string key)
            {
                return Get(key);
            }
            public static void Set(T model, string key)
            {
                HttpContext.Current.Session[key] = Newtonsoft.Json.JsonConvert.SerializeObject(model);
                HttpContext.Current.Session.Timeout = 360;
            }


            public static T Get(string key)
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(HttpContext.Current.Session[key].ToString());
            }


            public static void Remove(string key)
            {
                HttpContext.Current.Session.Remove(key);
            }

        }
    }
}
