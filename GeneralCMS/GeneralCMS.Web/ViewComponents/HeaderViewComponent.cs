using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeneralCMS.Application.Services;
using GeneralCMS.Models.ViewModel;

namespace GeneralCMS.Web.Components
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly IHomeService homeService;
        public HeaderViewComponent(IHomeService homeService)
        {
            this.homeService = homeService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var data = await Task.FromResult(homeService.GetVHeaderPageData());
            return View(data);
        }
    }
}
