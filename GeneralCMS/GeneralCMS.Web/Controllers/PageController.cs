using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeneralCMS.Application.Services;
using GeneralCMS.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace GeneralCMS.Web.Controllers
{
    public class PageController : Controller
    {
        private readonly IPageContentService contentService;
        public PageController(IPageContentService contentService)
        {
            this.contentService = contentService;
        }

        public IActionResult List(int navId, int pageNum = 1, int pageSize = 5)
        {
            var data = contentService.GetCustomPage(navId, pageNum, pageSize); 
            return View(data.Result);
        }

        public IActionResult Detail(int navId, int contentId)
        {
            var data = contentService.GetPageContentDetailAsync(contentId);
            data.NavigationId = navId;
            return View(data);
        }
    }
}