using GeneralCMS.Models.Dto;
using GeneralCMS.Models.ViewModel.Admin;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeneralCMS.Application.Services
{
    public interface IPageMngService : IBaseService
    {
        [AOP.AOPServiceLog("轮播图管理-获取列表")]
        VPageImgPlay GetNavigationImgPlays(string host);

        [AOP.AOPServiceLog("轮播图管理-保存内容")]
        VRequestInfo SaveNavigationImgPlays(ImgPlayDto input);

        [AOP.AOPServiceLog("轮播图管理-启用禁用")]
        VRequestInfo SwitchNavigationImgPlays(VSlideshowSwitchInput input);


        [AOP.AOPServiceLog("轮播图管理-删除")]
        VRequestInfo DeleteNavigationImgPlays(VSlideshowSwitchInput input);

        [AOP.AOPServiceLog("导航管理-获取导航列表")]
        VNavigationTree GetNavigationTree();

        [AOP.AOPServiceLog("导航管理-获取导航详情")]
        VRequestInfo GetNavigationDetail(int id);

        [AOP.AOPServiceLog("导航管理-编辑导航")]
        VRequestInfo EditNavigation(VNavigationEdit input);
    }
}
