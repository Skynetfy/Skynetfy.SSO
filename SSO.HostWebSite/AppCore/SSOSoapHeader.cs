using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Protocols;

namespace SSO.HostWebSite.AppCore
{
    public class SSOSoapHeader : SoapHeader
    {
        private string _userID = string.Empty;
        private string _passWord = string.Empty;

        public SSOSoapHeader()
        {
        }

        public SSOSoapHeader(string userId,string passWord)
        {
            Initial(userId, passWord);
        }

        #region 属性
        ///
        /// 用户名
        ///
        public string UserID
        {
            get { return _userID; }
            set { _userID = value; }
        }
        ///
        /// 加密后的密码
        ///
        public string PassWord
        {
            get { return _passWord; }
            set { _passWord = value; }
        }
        #endregion
        #region 方法
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="nUserID">用户ID</param>
        /// <param name="nPassWord">加密后的密码</param>
        public void Initial(string nUserID, string nPassWord)
        {
            UserID = nUserID;
            PassWord = nPassWord;
        }
        /// <summary>
        /// 用户名密码是否正确
        /// </summary>
        /// <param name="nUserID">用户ID</param>
        /// <param name="nPassWord">加密后的密码</param>
        /// <param name="nMsg">返回的错误信息</param>
        /// <returns>用户名密码是否正确</returns> 
        public bool IsValid(string nUserID, string nPassWord)
        {
            ;
            try
            {
                //判断用户名密码是否正确 (注：这个密码没有经过加密处理)
                if (nUserID == "admin" && nPassWord == "admin")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
        ///
        /// 用户名密码是否正确
        ///
        ///
        ///
        public bool IsValid()
        {
            return IsValid(UserID, PassWord);
        }
        #endregion
    }
}