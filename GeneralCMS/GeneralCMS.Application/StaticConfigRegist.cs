using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Banana.Uow;
using Microsoft.Extensions.Options;
using GeneralCMS.Application.AppSetting;
using Microsoft.Extensions.Configuration;
using GeneralCMS.Application.AppSetting.Models;

namespace GeneralCMS.Application
{
    public static class StaticConfigRegist
    {
        /// <summary>
        /// 静态配置
        /// </summary>
        public static IApplicationBuilder UseConfigRegist<T>(this IApplicationBuilder app, IConfiguration Configuration) where T: BaseConfigModel
        {
            var reg = Configuration.Get<T>();
            //注册数据库
            ConnectionBuilder.ConfigRegist(reg.DBSetting.StrConnection, reg.DBSetting.DBType);

            return app;
        }
    }
}
