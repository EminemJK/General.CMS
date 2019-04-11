using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using GeneralCMS.Common.LogUtility;
using GeneralCMS.Application.AOP;

namespace GeneralCMS.Application
{
    public static class ServiceDI
    {
        public static IServiceCollection Inject(this IServiceCollection services)
        {
            AddRepo(services);
            AddService(services);
            AddCommon(services);

            return services;
        }

        /// <summary>
        /// 业务服务
        /// </summary>
        /// <param name="services"></param>
        private static void AddService(IServiceCollection services)
        {
            foreach (var item in GetClassName("GeneralCMS.Application"))
            {
                foreach (var typeArray in item.Value)
                {
                    services.AddScoped(typeArray, item.Key);
                }
            }
        }

        /// <summary>
        /// 通用
        /// </summary>
        /// <param name="services"></param>
        private static void AddCommon(IServiceCollection services)
        {
            //log日志注入 
            services.AddSingleton<ILoggerHelper, LogHelper>(); 
        }

        /// <summary>
        /// 仓储
        /// </summary>
        /// <param name="services"></param>
        private static void AddRepo(IServiceCollection services)
        {
            foreach (var item in GetClassName("GeneralCMS.Domain"))
            {
                foreach (var typeArray in item.Value)
                {
                    services.AddScoped(typeArray, item.Key);
                }
            }
        } 

        private static Dictionary<Type, Type[]> GetClassName(string assemblyName)
        {
            if (!String.IsNullOrEmpty(assemblyName))
            {
                var assembly = Assembly.Load(assemblyName);
                List<Type> ts = assembly.GetTypes().ToList();

                var result = new Dictionary<Type, Type[]>();
                foreach (var item in ts.Where(s => !s.IsInterface))
                {
                    var interfaceType = item.GetInterfaces();
                    if (item.IsGenericType)
                        continue;
                    result.Add(item, interfaceType);
                }
                return result;
            }
            return new Dictionary<Type, Type[]>();
        }
    }
}
