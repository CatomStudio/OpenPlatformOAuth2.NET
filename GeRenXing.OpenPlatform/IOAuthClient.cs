using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeRenXing.OpenPlatform
{
    /// <summary>
    /// Description: IOAuth接口
    /// Author: 美丽的地球
    /// Email: sanxia330@qq.com
    /// QQ: 1851690435
    /// </summary>
    public interface IOAuthClient
    {
        AuthOption Option { get; }
        AuthToken Token { get; }
        IUserInterface User { get; }

        String GetAuthorizeUrl(ResponseType responseType);
        AuthToken GetAccessTokenByAuthorizationCode(string code);
        AuthToken GetAccessTokenByPassword(string passport, string password);
        AuthToken GetAccessTokenByRefreshToken(string refreshToken);
        String Get(String url, params RequestOption[] options);
        String Post(String url, params RequestOption[] options);
    }

}
