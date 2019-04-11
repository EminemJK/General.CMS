using GeneralCMS.Application.AppSetting.Interface;
using GeneralCMS.Application.AppSetting.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeneralCMS.Application.AppSetting
{
    /// <summary>
    /// 配置工厂
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ConfigFactory<T> : IConfigFactory<T> where T : BaseConfigModel, new()
    {
        /// <summary>
        /// 你的全部配置
        /// </summary>
        public T AppSettings { get; set; }

        /// <summary>
        /// Init
        /// </summary>
        /// <param name="Options"></param>
        public ConfigFactory(IOptions<T> Options) //Microsoft.Extensions.Options
        {
            this.AppSettings = Options.Value;
        }
    }
}
