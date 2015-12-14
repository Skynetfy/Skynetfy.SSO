using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using SSO.WebSite2.localhost;

namespace SSO.WebSite1.Controllers
{
    public class AuthBase : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Session["Token"] != null)
            {
                //分站凭证存在
                Response.Write("恭喜，分站凭证存在，您被授权访问该页面！");
            }
            else
            {
                //令牌验证结果
                if (Request.QueryString["Token"] != null)
                {
                    if (Request.QueryString["Token"] != "$Token$")
                    {
                        //持有令牌
                        string tokenValue = Request.QueryString["Token"];
                        ////调用WebService获取主站凭证
                        TokenService tokenService = new TokenService();
                        SSOSoapHeader header = new SSOSoapHeader();
                        header.UserID = "admin";
                        header.PassWord = "admin";
                        tokenService.SSOSoapHeaderValue = header;
                        object o = tokenService.TokenGetCredence(tokenValue);
                        if (o != null)
                        {
                            //令牌正确
                            Session["Token"] = o;
                            Response.Write("恭喜，令牌存在，您被授权访问该页面！");
                        }
                        else
                        {
                            //令牌错误
                            Response.Redirect(this.ReplaceToken());
                        }
                    }
                    else
                    {
                        //未持有令牌
                        Response.Redirect(this.ReplaceToken());
                    }
                }
                //未进行令牌验证，去主站验证
                else
                {
                    Response.Redirect(this.getTokenURL());
                }
            }

            base.OnActionExecuting(filterContext);
        }
        /// <summary>
        /// 获取带令牌请求的URL
        /// </summary>
        /// <returns></returns>
        private string getTokenURL()
        {
            string url = Request.Url.AbsoluteUri;
            Regex reg = new Regex(@"^.*\?.+=.+$");
            if (reg.IsMatch(url))
                url += "&Token=$Token$";
            else
                url += "?Token=$Token$";

            return "http://localhost:64944/Home/GetToken?BackURL=" + Server.UrlEncode(url);
        }

        /// <summary>
        /// 去掉URL中的令牌
        /// </summary>
        /// <returns></returns>
        private string ReplaceToken()
        {
            string url = Request.Url.AbsoluteUri;
            url = Regex.Replace(url, @"(\?|&)Token=.*", "", RegexOptions.IgnoreCase);
            return "http://localhost:64944/Home/AuthLogin?BackURL=" + Server.UrlEncode(url);
        }
    }
}