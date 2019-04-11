using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GeneralCMS.Application;
using GeneralCMS.Application.AppSetting;
using GeneralCMS.Application.AppSetting.Interface;
using GeneralCMS.Application.Middleware;
using GeneralCMS.Common.LogUtility;
using log4net;
using log4net.Config;
using log4net.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection; 
using AspectCore.Extensions.DependencyInjection;
using AspectCore.Configuration;
using GeneralCMS.Application.AOP;
using Microsoft.Extensions.DependencyInjection.Extensions;
using GeneralCMS.Admin.Config;

namespace GeneralCMS.Admin
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            //log4net
            repository = LogManager.CreateRepository(LogHelper.LogRepo);
            //指定配置文件
            XmlConfigurator.Configure(repository, new FileInfo("log4net.config"));
        }

        public IConfiguration Configuration { get; }
        /// <summary>
        /// log4net 仓储库
        /// </summary>
        public static ILoggerRepository repository { get; set; }


        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //启用内存缓存
            services.AddMemoryCache();
            //认证
            services.AddAuthorization();

            //https://docs.microsoft.com/zh-cn/aspnet/core/security/authentication/cookie?view=aspnetcore-2.2
            services.AddAuthentication(SystemConfig.UserCookie) 
                 .AddCookie(SystemConfig.UserCookie, options =>
                 {
                     options.AccessDeniedPath = "/Account/Denied";
                     options.LoginPath = "/Account/Login";
                     options.LogoutPath = "/Account/Logout";  
                 });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            //注入HttpContext，才可以AOP或其他地方中获取到相关HttpContext信息
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //配置
            services.Configure<ConfigModel>(Configuration);
            services.AddSingleton<IConfigFactory<ConfigModel>, ConfigFactory<ConfigModel>>();

            services.Inject();
            //AOP
            services.ConfigureDynamicProxy();
            return services.BuildAspectInjectorProvider();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            //cookie
            app.UseCookiePolicy();
            //启用认证
            app.UseAuthentication();
            //添加获取用户真实IP中间件
            app.UseForwardedHeaders(new ForwardedHeadersOptions { ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto });
            //全局异常捕获
            app.UseErrorHandling();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            app.UseConfigRegist<ConfigModel>(Configuration);
        }
    }
}
