using GeneralCMS.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneralCMS.Web.ViewComponents
{
    public class FooterViewComponent : ViewComponent
    {
        private readonly IHomeService homeService;
        public FooterViewComponent(IHomeService homeService)
        {
            this.homeService = homeService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var data = await Task.FromResult(homeService.GetVFooterPageData());
            return View(data);
        }
    }
}
