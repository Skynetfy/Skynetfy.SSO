using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using SSO.Core;
using SSO.HostWebSite.AppCore;

namespace SSO.HostWebSite
{
    /// <summary>
    /// Summary description for TokenService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class TokenService : System.Web.Services.WebService
    {
        public string msg = "";
        //声明重写的SOAP标头
        public SSOSoapHeader mySoapHeader = new SSOSoapHeader();
        /// <summary>
        /// 根据令牌获取用户凭证
        /// </summary>
        /// <param name="tokenValue">令牌</param>
        /// <returns></returns>
        [WebMethod]
        [SoapHeader("mySoapHeader")]
        public object TokenGetCredence(string tokenValue)
        {
            if (!mySoapHeader.IsValid())
            {
                return null;
            }
            object obj = null;
            DataTable dt = CacheManager.GetCacheTable();
            if (dt != null)
            {
                DataRow[] dr = dt.Select("token = '" + tokenValue + "'");
                if (dr.Length > 0)
                {
                    obj = dr[0]["info"];
                }
            }
            return obj;
        }

        /// <summary>
        /// 清除令牌
        /// </summary>
        /// <param name="tokenValue">令牌</param>
        [WebMethod]
        [SoapHeader("mySoapHeader")]
        public object ClearToken(string tokenValue)
        {
            if (!mySoapHeader.IsValid())
            {
                return msg;
            }
            DataTable dt = CacheManager.GetCacheTable();
            if (dt != null)
            {
                DataRow[] dr = dt.Select("token = '" + tokenValue + "'");
                if (dr.Length > 0)
                {
                    dt.Rows.Remove(dr[0]);
                }
            }
            return null;
        }
    }
}
