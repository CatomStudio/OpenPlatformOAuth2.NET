OpenPlatformOAuth2.NET
======================

OpenPlatformOAuth2.NET for SinaWeiBo,TencentQQ,TaoBao

-- sinaweibo oauth2.0 --
https://api.weibo.com/oauth2/authorize?client_id=youclientid&redirect_uri=http://yousitereplace/oauth/callback&response_type=code&display=default%20&state=sinaweibo

-- qq oauth2.0 --
http://openapi.qzone.qq.com/oauth/show?which=Login&display=pc&client_id=youclientid&redirect_uri=http://yousitereplace/oauth/callback&response_type=code&display=default%20&state=qq

-- taobao oauth2.0 --
https://oauth.taobao.com/authorize?client_id=youclientid&redirect_uri=http://yousitereplace/oauth/callback&response_type=code&display=default%20&state=taobao

Example Test:
==============================================================================================

namespace GeRenXing.OpenPlatform.Test
{
    /// <summary>
    /// Description: 开放平台 OAuth 接口测试
    /// Author: 美丽的地球
    /// Email: sanxia330@qq.com
    /// QQ: 1851690435
    /// </summary>
    class Program
    {
        private static Dictionary<String, IOAuthClient> m_oauthClients;

        static void Main(string[] args)
        {
            //初始化开放平台客户端（请替换成自己的ClientId，ClientScrert，CallbackUrl）
            m_oauthClients = new Dictionary<string, IOAuthClient>();
            m_oauthClients["sinaweibo"] = new OpenPlatform.OAuthClient.SinaWeiBoClient("You ClientId", "You ClientScrert", "You Callback Url");
            m_oauthClients["qq"] = new OpenPlatform.OAuthClient.TencentQQClient("You ClientId", "You ClientScrert", "You Callback Url");
            m_oauthClients["taobao"] = new OpenPlatform.OAuthClient.TaoBaoClient("You ClientId", "You ClientScrert", "You Callback Url");

            //测试
            OAuthTest("sinaweibo");
            //OAuthTest("qq");
            //OAuthTest("taobao");

            Console.ReadKey(true);
        }

        private static void OAuthTest(String platformCode)
        {
            String authorizeUrl = String.Empty;
            if (String.IsNullOrEmpty(platformCode)) platformCode = "sinaweibo";

            Console.WriteLine("OpenPlatform Request For " + platformCode);
            Console.WriteLine("");

            IOAuthClient oauthClient = m_oauthClients[platformCode];
            oauthClient.Option.State = platformCode;

            //第一步：获取开放平台授权地址
            authorizeUrl = m_oauthClients[platformCode].GetAuthorizeUrl(ResponseType.Code);
            Console.WriteLine("Step 1 - OAuth2.0 for Redirect AuthorizeUrl: ");
            Console.WriteLine(authorizeUrl);

            //第二步：打开IE浏览器获取Code
            Process p = new Process();
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.Arguments = authorizeUrl;
            psi.FileName = "C:\\Program Files\\Internet Explorer\\iexplore.exe";
            p.StartInfo = psi;
            p.Start();

            Console.WriteLine("");
            Console.WriteLine("OAuth2.0 Input Server Response Code");
            String code = Console.ReadLine();

            //第三步：获取开放平台授权令牌
            oauthClient = m_oauthClients[platformCode];
            AuthToken accessToken = oauthClient.GetAccessTokenByAuthorizationCode(code);
            if (accessToken != null)
            {
                Console.WriteLine("");
                Console.WriteLine("Step 2 - OAuth2.0 for AccessToken: " + accessToken.AccessToken);
                //输出原始响应数据
                Console.WriteLine("GetAccessToken Raw Response : ");
                Console.WriteLine(oauthClient.Token.TraceInfo);

                //第四步：调用开放平台API，获取开放平台用户信息
                dynamic oauthProfile = oauthClient.User.GetUserInfo();

                //输出解析出来的用户昵称
                Console.WriteLine("");
                Console.WriteLine("Step 3 - Call Open API UserInfo: ");
                Console.WriteLine("UserInfo Nickname: " + oauthClient.Token.User.Nickname);
                //输出原始响应数据
                Console.WriteLine("GetUserInfo Raw Response : ");
                Console.WriteLine(oauthClient.Token.TraceInfo);
            }

        }
    }
}
