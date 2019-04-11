using System;
using System.Collections.Generic;
using System.Text;
using Banana.Uow.Models;

namespace GeneralCMS.Application.AppSetting.Models
{
    /// <summary>
    /// 基础配置，勿动
    /// </summary>
    public class BaseConfigModel
    {
        public DBSetting DBSetting { get; set; }

        public BaseConfigModel() { }
    }
}
