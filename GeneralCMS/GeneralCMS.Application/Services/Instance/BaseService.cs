using GeneralCMS.Application.AppSetting.Interface;
using GeneralCMS.Common.LogUtility;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Text;
using GeneralCMS.Application.AppSetting;
using GeneralCMS.Application.AppSetting.Models;

namespace GeneralCMS.Application.Services.Instance
{
    public class BaseService : IBaseService
    {
        protected readonly IServiceProvider serviceProvider;
        protected readonly ILoggerHelper logger;
        public BaseService(ILoggerHelper logger, IServiceProvider serviceProvider)
        {
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }

        public IConfigFactory<T> GetConfigFactory<T>() where T : BaseConfigModel, new()
        {
            return serviceProvider.GetService<ConfigFactory<T>>();
        }

        public IServiceProvider GetServiceProvider()
        {
            return serviceProvider;
        }
    }
}
