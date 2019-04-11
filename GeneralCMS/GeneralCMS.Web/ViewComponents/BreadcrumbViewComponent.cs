
using GeneralCMS.Application.Services;
using GeneralCMS.Models.ViewModel.Frontend;
using GeneralCMS.Models.Enum;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeneralCMS.Models.Dto;

namespace GeneralCMS.Web.ViewComponents
{
    public class BreadcrumbViewComponent : ViewComponent
    {
        private readonly IHomeService homeService;
        public BreadcrumbViewComponent(IHomeService homeService)
        {
            this.homeService = homeService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int navId)
        {
            VBreadcrumb breadcrumb;
            if (navId == 0)
            {
                return View(new VBreadcrumb("", "", "#"));
            }
            var nav = homeService.GetNavigationsAsync(navId);
            var img = await Task.FromResult(homeService.GetImgPlaysAsync(navId, EImageType.Header));
            var navRes = nav.Result;
            var imgRes = img.Result.FirstOrDefault() ?? new ImgPlayDto();
            if (nav.Result.ParentId > 0)
            {
                var navParent = await Task.FromResult(homeService.GetNavigationsParentAsync(navId));
                var navParentRes = navParent.Result;
                breadcrumb = new VBreadcrumb(navRes.Name, navParentRes.Name, navParentRes.Url, imgRes.ImgUrl);
            }
            else
            {
                breadcrumb = new VBreadcrumb(navRes.Name, navRes.Name, navRes.Url, imgRes.ImgUrl);
            }
            return View(breadcrumb);
        }
    }
}
