using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GeneralCMS.Models;
using GeneralCMS.Application.Services;

namespace GeneralCMS.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeService homeService;
        private readonly IPageContentService contentService; 
        public HomeController(IHomeService homeService, IPageContentService contentService)
        {
            this.homeService = homeService;
            this.contentService = contentService;
        }

        public IActionResult Index()
        {
            var data = homeService.GetHomeData();
            return View(data);
        }

        /// <summary>
        /// 请求关于我们
        /// </summary>
        /// <param name="navId">导航Id</param>
        /// <returns></returns>
        public IActionResult About(int navId)
        {
            var data = contentService.GetAboutUsData(navId);
            return View(data);
        }

        public IActionResult Error()
        { 
            return View();
        }
    }
}
