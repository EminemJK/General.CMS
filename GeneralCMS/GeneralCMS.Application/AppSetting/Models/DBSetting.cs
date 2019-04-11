using System;
using System.Collections.Generic;
using System.Text;
using Banana.Uow.Models;

namespace GeneralCMS.Application.AppSetting.Models
{
    /// <summary>
    /// 基础数据库配置，勿动
    /// </summary>
    public sealed class DBSetting
    {
        public DBType DBType { get; set; }

        public string StrConnection { get; set; }
    }
}
