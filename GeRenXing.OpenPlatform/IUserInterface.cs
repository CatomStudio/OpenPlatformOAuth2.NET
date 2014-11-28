using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeRenXing.OpenPlatform
{
    /// <summary>
    /// OAuth基础用户接口
    /// Author: 美丽的地球
    /// Email: sanxia330@qq.com
    /// QQ: 1851690435
    /// </summary>
    public interface IUserInterface
    {
        dynamic GetUserInfo();
        void EndSession();
    }

}
