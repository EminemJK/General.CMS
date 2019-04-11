using GeneralCMS.Models.Dto;
using GeneralCMS.Models.ViewModel.Frontend;
using GeneralCMS.Models.Enum;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GeneralCMS.Application.Services
{
    public interface IHomeService : IBaseService
    {
        /// <summary>
        /// 获取导航
        /// </summary>
        /// <returns></returns>
        Task<List<NavigationDto>> GetNavigationsAsync();

        /// <summary>
        /// 获取导航
        /// </summary>
        /// <returns></returns>
        Task<NavigationDto> GetNavigationsAsync(int navId);

        /// <summary>
        /// 获取父级导航
        /// </summary>
        /// <returns></returns>
        Task<NavigationDto> GetNavigationsParentAsync(int navId);

        /// <summary>
        /// 友情链接
        /// </summary>
        /// <returns></returns>
        Task<List<FriendlyLinkDto>> GetFriendlyLinksAsync();

        /// <summary>
        /// 获取图片信息
        /// </summary>
        /// <param name="navId"></param>
        /// <returns></returns>
        Task<List<ImgPlayDto>> GetImgPlaysAsync(int navId);

        /// <summary>
        /// 获取图片信息
        /// </summary>
        /// <param name="navId"></param>
        /// <returns></returns>
        Task<List<ImgPlayDto>> GetImgPlaysAsync(int navId, EImageType imageType);

        /// <summary>
        /// 首页数据
        /// </summary>
        /// <returns></returns>
        VHomePage GetHomeData();

        /// <summary>
        /// 页脚数据
        /// </summary>
        /// <returns></returns>
        VHeaderPageData GetVHeaderPageData();

        /// <summary>
        /// 头部数据
        /// </summary>
        /// <returns></returns>
        VFooterPageData GetVFooterPageData();
    }
}
