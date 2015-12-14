using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using SSO.Core;

namespace SSO.HostWebSite.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        public ActionResult AuthLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AuthLogin(string userName, string passWord)
        {
            if (userName.Equals("admin") && passWord.Equals("admin"))
            {
                //产生令牌
                string tokenValue = this.GetGuidString();
                HttpCookie tokenCookie = new HttpCookie("Token");
                tokenCookie.Values.Add("Value", tokenValue);
                Response.AppendCookie(tokenCookie);

                //产生主站凭证
                object info = userName + "$" + passWord;
                CacheManager.TokenInsert(tokenValue, info, DateTime.Now.AddMinutes(20));

                //跳转回分站
                if (Request.QueryString["BackURL"] != null)
                {
                    Response.Redirect(Server.UrlDecode(Request.QueryString["BackURL"]));
                }
            }
            return View();
        }

        public ActionResult GetToken()
        {
            if (Request.QueryString["BackURL"] != null)
            {
                //获取URL参数（回调地址）
                string backURL = Server.UrlDecode(Request.QueryString["BackURL"]);
                //获取Cookie（令牌）---- 登录认证时保存的Cookie
                HttpCookie tokenCookie = Request.Cookies["Token"];
                if (tokenCookie != null)
                {
                    //令牌存在则替换回调地址中的$Token$
                    backURL = backURL.Replace("$Token$", tokenCookie.Values["Value"].ToString());
                }

                Response.Redirect(backURL);
            }
            return Content("");
        }

        /// <summary>
        /// 产生绝对唯一字符串，用于令牌
        /// </summary>
        /// <returns></returns>
        string GetGuidString()
        {
            return Guid.NewGuid().ToString();
        }


    }
}