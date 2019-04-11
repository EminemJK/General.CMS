using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeneralCMS.Application.Common;
using GeneralCMS.Application.Services;
using GeneralCMS.Common.Extend;
using GeneralCMS.Models.Dto;
using GeneralCMS.Models.ViewModel.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeneralCMS.Admin.Controllers
{
    [Authorize(Roles = UserType.Administrator + "," + UserType.User)]
    public class PageMngController : BaseController
    {
        private readonly IPageMngService pageMngService;
        public PageMngController(IPageMngService pageMngService, IServiceProvider serviceProvider) : base(serviceProvider)
        {
            this.pageMngService = pageMngService;
        }

        #region 导航管理
        /// <summary>
        /// 导航列表
        /// </summary>
        public IActionResult Navigation()
        {
            return View();
        }

        public IActionResult GetNavigationTree()
        {
            var tree = pageMngService.GetNavigationTree();
            return ShowSuccess("获取成功", tree);
        }

        public IActionResult GetNavigationDetail(int id)
        {
            if (id <= 0)
            {
                return ShowError("Id 不存在");
            }
            return Show(pageMngService.GetNavigationDetail(id));
        }

        [HttpPost]
        public IActionResult EditNavigation(VNavigationEdit input)
        {
            if(input==null)
            {
                return ShowError("");
            }

            return ShowSuccess("");
        }
        #endregion

        #region 轮播图管理
        /// <summary>
        /// 轮播图
        /// </summary>
        public IActionResult Slideshow()
        {
            var gateway = Config.OtherService.Gateway;
            var data = pageMngService.GetNavigationImgPlays(gateway);
            data.UploadUrl = UrlCommon.CreateUrlPath(gateway, Config.OtherService.Api.Upload.Url);
            return View(data);
        }

        /// <summary>
        /// 保存修改
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult SlideshowSave(ImgPlayDto input)
        {
            if (!ModelState.IsValid)
            {
                return ShowError(GetModelError(ModelState));
            }
            input.ImgUrl = UrlCommon.GetUrlPath(Config.OtherService.Gateway, input.ImgUrl);
            var res = pageMngService.SaveNavigationImgPlays(input);
            return new JsonResult(res);
        }

        /// <summary>
        /// 启用禁用
        /// </summary>
        [HttpPost]
        public IActionResult SlideshowSwitchOff(VSlideshowSwitchInput input)
        {
            if (!ModelState.IsValid)
            {
                return ShowError(GetModelError(ModelState));
            }

            var res = pageMngService.SwitchNavigationImgPlays(input);
            return new JsonResult(res);
        }

        /// <summary>
        /// 删除
        /// </summary>
        [HttpPost]
        public IActionResult SlideshowDelete(VSlideshowSwitchInput input)
        {
            if (!ModelState.IsValid)
            {
                return ShowError(GetModelError(ModelState));
            }

            var res = pageMngService.DeleteNavigationImgPlays(input);
            return new JsonResult(res);
        }
        #endregion

        #region 友情链接

        #endregion
    }
}