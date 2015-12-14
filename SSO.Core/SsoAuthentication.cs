using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;

namespace SSO.Core
{
    public class SsoAuthentication
    {
        static readonly string cookieName = "EACToken";
        static readonly string hashSplitter = "|";

        public SsoAuthentication()
        {
        }

        public static string GetAppKey(int appId)
        {
            //string cmdText = @"select * from ";
            return string.Empty;
        }

        public static string GetAppKey()
        {
            return "22362E7A9285DD53A0BBC2932F9733C505DC04EDBFE00D70";
        }

        public static string GetAppIv()
        {

            return "1E7FA9231E7FA923";
        }
    }
}
