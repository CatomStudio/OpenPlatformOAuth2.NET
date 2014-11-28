using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;

using GeRenXing.Utils;

namespace GeRenXing.OpenPlatform.OAuthClient
{
    /// <summary>
    /// 淘宝开放平台OAuth客户端用户信息接口实现
    /// Author: 美丽的地球
    /// Email: sanxia330@qq.com
    /// QQ: 1851690435
    /// </summary>
    public class TaoBaoUserImpl : IUserInterface
    {
        #region //字段
        private IOAuthClient oauth;
        #endregion

        #region //构造方法
        public TaoBaoUserImpl(IOAuthClient oauth)
        {
            this.oauth = oauth;
        }
        #endregion

        #region //接口实现

        #region //接口方法实现
        /// <summary>
        /// 获取第三方OAuth平台用户信息
        /// </summary>
        /// <returns></returns>
        public dynamic GetUserInfo()
        {
            String responseResult = "{\"uid\":\"" + this.oauth.Token.User.OAuthId + "\",\"nick\":\"" + this.oauth.Token.User.Nickname + "\",\"sex\":" + "\"n\"}";
            dynamic resultData = DynamicHelper.FromJSON(responseResult);

            //debug
            this.oauth.Token.TraceInfo = responseResult;

            return resultData;
        }

        public void EndSession()
        {
            String responseResult = this.oauth.Get(this.oauth.Option.Urls["EndSession"], new RequestOption() { Name = "access_token", Value = this.oauth.Token.AccessToken });

            //debug
            this.oauth.Token.TraceInfo = responseResult;
        }
        #endregion

        #endregion

    }
}
