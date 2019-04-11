using System;
using System.Collections.Generic;
using System.Linq;
using Banana.Utility.Common;
using GeneralCMS.Common.Extend;
using GeneralCMS.Common.LogUtility;
using GeneralCMS.Domain.Repository.Interface;
using GeneralCMS.Models.Dto;
using GeneralCMS.Models.Entities;
using GeneralCMS.Models.ViewModel.Admin;

namespace GeneralCMS.Application.Services.Instance
{
    public class PageMngService : BaseService, IPageMngService
    {
        private readonly INavigationRepositrory navigationRepositrory;
        private readonly IImgPlayInfoRepository playInfoRepository;
        public PageMngService(INavigationRepositrory navigationRepositrory, IImgPlayInfoRepository playInfoRepository,
            ILoggerHelper logger, IServiceProvider serviceProvider) : base(logger, serviceProvider)
        {
            this.navigationRepositrory = navigationRepositrory;
            this.playInfoRepository = playInfoRepository;
        }

        #region 轮播图管理
        public VPageImgPlay GetNavigationImgPlays(string host)
        {
            VPageImgPlay res = new VPageImgPlay();
            var navs = navigationRepositrory.QueryList("ParentId=@ParentId", new { ParentId = 0 }, "Id", true);
            foreach (var nav in navs)
            {
                VNavigationImgPlays plays = ModelConvertUtil<NavigationInfo, VNavigationImgPlays>.ModelCopy(nav);
                var imgplayHeader = playInfoRepository.GetAllImgPlaysAsync(nav.Id, Models.Enum.EImageType.Header);

                foreach (var img in imgplayHeader.Result)
                {
                    var modelDto = ModelConvertUtil<ImgPlayInfo, ImgPlayDto>.ModelCopy(img);
                    if (!string.IsNullOrEmpty(img.ImgUrl))
                        modelDto.ImgUrl = GeneralCMS.Common.Extend.UrlCommon.CreateUrlPath(host, img.ImgUrl);
                    plays.HeaderImgPlays.Add(modelDto);
                }
                res.NavigationsImgPlay.Add(plays);
            }

            var imgplayFooter = playInfoRepository.GetImgPlayByTypeAsync(Models.Enum.EImageType.Footer);
            var footerImg = ModelConvertUtil<ImgPlayInfo, ImgPlayDto>.ModelCopy(imgplayFooter.Result.FirstOrDefault() ?? new ImgPlayInfo());
            if (!string.IsNullOrEmpty(footerImg.ImgUrl))
            {
                footerImg.ImgUrl = GeneralCMS.Common.Extend.UrlCommon.CreateUrlPath(host, footerImg.ImgUrl);
            }
            //因为页脚不存在导航中
            VNavigationImgPlays footer = new VNavigationImgPlays();
            footer.Id = -1;
            footer.Name = "页脚";
            footer.HeaderImgPlays.Add(footerImg);
            res.NavigationsImgPlay.Add(footer);
            return res;
        }

        public VRequestInfo SaveNavigationImgPlays(ImgPlayDto input)
        {
            if (input.NavigationID != -1)
            {
                var navModel = navigationRepositrory.Query(input.NavigationID);
                if (navModel == null || navModel.Id == 0)
                {
                    return VRequestInfo.ErrorResult("导航不存在");
                }
            }
            bool res = false;
            if (input.Id == 0)
            {
                input.IsDisable = Models.Enum.EYesOrNo.No;
                input.CreateTime = DateTime.Now;
                res = playInfoRepository.Insert(ModelConvertUtil<ImgPlayDto, ImgPlayInfo>.ModelCopy(input)) > 0;
            }
            else
            {
                var model = ModelConvertUtil<ImgPlayDto, ImgPlayInfo>.ModelCopy(input);
                res = playInfoRepository.Update(model);
            }
            if (res)
                return VRequestInfo.SuccessResult();
            else
                return VRequestInfo.ErrorResult("保存失败");

        }

        public VRequestInfo SwitchNavigationImgPlays(VSlideshowSwitchInput input)
        {
            var list = playInfoRepository.GetModelByIdList(input.Ids.ToArray());
            if (list.Count == input.Ids.Count)
            {
                list.ForEach(i =>
                {
                    i.IsDisable = input.Off;
                    playInfoRepository.Update(i);
                });
                return VRequestInfo.SuccessResult("更新完成");
            }
            return VRequestInfo.ErrorResult("无效操作");
        }

        public VRequestInfo DeleteNavigationImgPlays(VSlideshowSwitchInput input)
        {
            var list = playInfoRepository.GetModelByIdList(input.Ids.ToArray());
            if (list.Count == input.Ids.Count)
            {
                var res = playInfoRepository.DeleteImgPlaysAsync(input.Ids);
                if (res.Result)
                    return VRequestInfo.SuccessResult("删除完成");
                else
                    return VRequestInfo.ErrorResult("删除失败");
            }
            return VRequestInfo.ErrorResult("无效操作");
        } 
        #endregion

        public VNavigationTree GetNavigationTree()
        {
            VNavigationTree tree = new VNavigationTree();

            var dataList = navigationRepositrory.QueryList(order: "Sort", asc: true);
            var root = dataList.FindAll(n => n.ParentId == 0);
            foreach (var item in root)
            {
                var node = new VNavigationTreeItem()
                {
                    navId = item.Id,
                    text = item.Name
                };
                GetTreeItem(node, dataList);
                tree.items.Add(node);
            }
            return tree;
        }

        private void GetTreeItem(VNavigationTreeItem parentNode , List<NavigationInfo> allData)
        {
            var childs = allData.FindAll(n=>n.ParentId== parentNode.navId);
            foreach (var item in childs)
            {
                var node = new VNavigationTreeItem()
                {
                    navId = item.Id,
                    text = item.Name
                };
                GetTreeItem(node, allData);
                parentNode.nodes.Add(node);
            }
        }

        public VRequestInfo GetNavigationDetail(int id)
        {
            VRequestInfo res = new VRequestInfo();
            var info = navigationRepositrory.Query(id);
            if (info == null)
            {
                return VRequestInfo.ErrorResult("导航不存在");
            }
            res.Data = ModelConvertUtil<NavigationInfo, NavigationDto>.ModelCopy(info);
            res.Code = VRequestInfo.Success;
            return res; 
        }


        public VRequestInfo EditNavigation(VNavigationEdit input)
        {
            int res = 0;
            if (input.navigationId == 0 && input.parentNavigationId == 0)
            {
                //新增一级导航
                var navInfo = new NavigationInfo()
                {
                  
                };
                navigationRepositrory.Insert(navInfo);
            }
            else if (input.navigationId == 0)
            {
                //新增子导航
            }
            else
            {
                //修改导航名称
            }
            return res > 0 ? VRequestInfo.SuccessResult("操作成功") : VRequestInfo.ErrorResult("操作失败");
        }

        
    }
}
