using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SSO.Core
{
    [Serializable]
    public class SSORequest : MarshalByRefObject
    {
        public string IASID;         //各独立站点标识ID
        public string TimeStamp;     //时间戳
        public string AppUrl;        //各独立站点的访问地址
        public string Authenticator; //各独立站点的 Token

        public string UserAccount;   //账号
        public string Password;      //密码

        public string IPAddress;     //IP地址

        //为ssresponse对象做准备
        public string ErrorDescription = "认证失败";   //用户认证通过,认证失败,包数据格式不正确,数据校验不正确
        public int Result = -1;

        public static SSORequest GetSsoRequest(HttpContextBase httpContext)
        {
            var request = new SSORequest();
            request.IPAddress = httpContext.Request.UserHostAddress;
            request.IASID = httpContext.Request["IASID"].ToString();// Request本身会Decode
            request.UserAccount = httpContext.Request["UserAccount"].ToString();//this.Text
            request.Password = httpContext.Request["Password"].ToString();
            request.AppUrl = httpContext.Request["AppUrl"].ToString();
            request.Authenticator = httpContext.Request["Authenticator"].ToString();
            request.TimeStamp = httpContext.Request["TimeStamp"].ToString();
            return request;

        }
    }
}
