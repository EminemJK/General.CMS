using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeneralCMS.Application.Services;
using Microsoft.AspNetCore.Mvc;
using GeneralCMS.Common.LogUtility;
using GeneralCMS.Models.ViewModel.Frontend;

namespace GeneralCMS.Web.Controllers
{
    public class NewsController : Controller
    {
        private readonly INewService newService;
        public NewsController(INewService newService)
        {
            this.newService = newService;
        }

        public async Task<IActionResult> List(int navId, int pageNum = 1, int pageSize = 5)
        {
            var data = await newService.GetNewPageList(pageNum, pageSize);
            data.NavigationId = navId;
            return View(data);
        }

        public IActionResult Details(int navId, int newsid)
        {
            var data = newService.GetNewDetailPage(newsid) ?? new VNewDetailPage() ;
            data.NavigationId = navId;
            return View(data);
        }
    }
}