using GeneralCMS.Models.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeneralCMS.Models.ViewModel.Frontend
{
    public class VHomePage
    {
        /// <summary>
        /// 轮播图
        /// </summary>
        public List<ImgPlayDto> ImgPlays { get; set; } = new List<ImgPlayDto>();

        /// <summary>
        /// 服务
        /// </summary>
        public List<PageContentDto> PageContents { get; set; } = new List<PageContentDto>();

        /// <summary>
        /// 企业动态
        /// </summary>
        public List<NewsDto> News { get; set; } = new List<NewsDto>(); 
    }

    public class VFooterPageData
    {
        /// <summary>
        /// 导航
        /// </summary>
        public List<VNavigation> Navigations { get; set; } = new List<VNavigation>();

        /// <summary>
        /// 企业信息
        /// </summary>
        public OwnerDto Owner { get; set; } = new OwnerDto();

        /// <summary>
        /// 友情链接
        /// </summary>
        public List<FriendlyLinkDto> FriendlyLinks { get; set; } = new List<FriendlyLinkDto>();

        /// <summary>
        /// 二维码信息
        /// </summary>
        public List<string> QRCode { get; set; } = new List<string>();

        public string LogoUrl { get; set; }

        /// <summary>
        /// 底部图片
        /// </summary>
        public string FooterBgUrl { get; set; }
    }

    public class VHeaderPageData
    {
        /// <summary>
        /// 导航
        /// </summary>
        public List<VNavigation> Navigations { get; set; } = new List<VNavigation>();

        /// <summary>
        /// 企业信息
        /// </summary>
        public OwnerDto Owner { get; set; } = new OwnerDto();

        public string LogoUrl { get; set; }
    }
}
