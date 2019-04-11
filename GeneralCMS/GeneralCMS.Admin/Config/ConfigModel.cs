
using GeneralCMS.Application.AppSetting.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeneralCMS.Admin.Config
{
    public class ConfigModel : BaseConfigModel
    {
        public string SystemName { get; set; }

        public OtherService OtherService { get; set; }
    }


    #region 用户端或者其他接口的配置
    public class OtherService
    {
        public string Gateway { get; set; }
        public ApiInfo Api { get; set; }
    }

    public class ApiInfo
    {
        public ApiItem Upload { get; set; }
    }

    public class ApiItem
    {
        public string Url { get; set; }
        public string Method { get; set; }
    } 
    #endregion
}
