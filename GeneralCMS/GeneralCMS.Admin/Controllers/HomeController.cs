using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GeneralCMS.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using GeneralCMS.Application.Common; 

namespace GeneralCMS.Admin.Controllers
{
    [Authorize(Roles = UserType.Administrator+","+UserType.User)]
    public class HomeController : BaseController
    {
        public HomeController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
           
        }
         
        public IActionResult Index()
        {
            ViewData["Title"] = this.Config.SystemName;
            ViewData["data"] = DateTime.Now.ToString("yyyy年MM月dd日 HH:mm:ss");
             
            return View();
        }

        public IActionResult IFrameTest()
        {
            return View();
        }

        public IActionResult Welcome()
        {
            return View();
        }
    }
}
