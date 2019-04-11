
using System;
using System.Collections.Generic;
using System.Text;
using GeneralCMS.Application.Services;
using AspectCore.DynamicProxy;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using AspectCore.Injector;
using GeneralCMS.Models.Dto;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Claims;
using Newtonsoft.Json.Converters;

namespace GeneralCMS.Application.AOP
{
    public class AOPServiceLogAttribute : AbstractInterceptorAttribute
    {
        public string Title { get; set; }
        public bool EncryptionParam { get; set; }

        /// <summary>
        /// AOP 日志
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="encryptionParam">是否加密参数</param>
        public AOPServiceLogAttribute(string title, bool encryptionParam = false)
        {
            this.Title = title;
            this.EncryptionParam = encryptionParam;
        }

        public async override Task Invoke(AspectContext context, AspectDelegate next)
        {
            WriteLog(context);
            await next(context);//执行被拦截的方法
        }

        /// <summary>
        /// 写入操作日志
        /// </summary>
        private void WriteLog(AspectContext context)
        {
            var actionLog = context.ServiceProvider.GetService<IActionLog>();
            var model = CreateLog(context);
            //写入数据库操作日志
             actionLog.Write(model);
        }

        private AdminLogDto CreateLog(AspectContext aspectContext)
        {
            var log = new AdminLogDto()
            {
                Title = Title,
                Content = aspectContext.ServiceMethod.Name
            };

            object factory = aspectContext.ServiceProvider.GetService(typeof(IHttpContextAccessor));
            HttpContext context = ((IHttpContextAccessor)factory).HttpContext;
            var claims = context.User.Claims.ToList();

            if (claims != null && claims.Count > 0)
            {
                log.AdminID = Convert.ToInt16(claims.Find(c => c.Type == ClaimTypes.Sid).Value);
            }
            log.IP = context.Connection.RemoteIpAddress.ToString();

            var paramString = "* [已加密]";
            if (aspectContext.Parameters.Length == 0)
            {
                paramString = "无参";
            }
            else if (!EncryptionParam)
            {
                var timeConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" };
                paramString = Newtonsoft.Json.JsonConvert.SerializeObject(aspectContext.Parameters, Newtonsoft.Json.Formatting.Indented, timeConverter);
            }

            log.Content = string.Format("请求路由：{0}      请求服务：{1}      请求参数：{2}", context.Request.Path, aspectContext.ServiceMethod.Name, paramString);
            log.CreateTime = DateTime.Now;
            return log;
        }
    }
}
