using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GeneralCMS.Application.AppSetting;
using GeneralCMS.Common.LogUtility;
using log4net;
using log4net.Config;
using log4net.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using GeneralCMS.Application.AppSetting.Interface;
using GeneralCMS.Application;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GeneralCMS.Application.Middleware;
using GeneralCMS.Web.Config;

namespace GeneralCMS.Web
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
        public void ConfigureServices(IServiceCollection services)
        {
            //请求用户是否启用cookie,不启用
            //services.Configure<CookiePolicyOptions>(options =>
            //{
            //    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
            //    options.CheckConsentNeeded = context => true;
            //    options.MinimumSameSitePolicy = SameSiteMode.None;
            //});
             
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
             
            //配置
            services.Configure<ConfigModel>(Configuration);
            services.AddSingleton<IConfigFactory<ConfigModel>, ConfigFactory<ConfigModel>>();

            var site = Configuration.Get<ConfigModel>().AdminSite.Split(",");
            //跨域，让管理端上传图片文件
            services.AddCors(option => option.AddPolicy("adminCors",
                policy => policy.WithOrigins(site).AllowAnyHeader().AllowAnyMethod().AllowCredentials()));

            services.Inject();
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            //全局异常捕获
            app.UseErrorHandling();

            app.UseCors("adminCors");
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
