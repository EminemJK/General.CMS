using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Banana.Utility.Common;
using GeneralCMS.Models.Dto;
using GeneralCMS.Application.Services;
using GeneralCMS.Models.ViewModel.Frontend;
using GeneralCMS.Domain.Repository.Interface;
using GeneralCMS.Models.Entities;
using GeneralCMS.Models.Enum;
using GeneralCMS.Application.Common;
using GeneralCMS.Common.LogUtility;

namespace GeneralCMS.Application.Services.Instance
{
    public class HomeService :BaseService, IHomeService
    {
        private readonly INewsRepository newsRepository;
        private readonly IImgPlayInfoRepository imgPlayInfoRepository;
        private readonly INavigationRepositrory navigationRepositrory;
        private readonly IFriendlyLinkRepository friendlyLinkRepository;
        private readonly IPageContentRepository pageContentRepository;
        private readonly IOwnerService ownerService;
        private readonly IKeyValueService keyValueService;

        public HomeService(INavigationRepositrory navigationRepositrory, IImgPlayInfoRepository imgPlayInfoRepository,
            INewsRepository newsRepository, IFriendlyLinkRepository friendlyLinkRepository,
            IPageContentRepository pageContentRepository, IOwnerService ownerService,
            IKeyValueService keyValueService, ILoggerHelper logger, IServiceProvider serviceProvider) : base(logger, serviceProvider)
        {
            this.navigationRepositrory = navigationRepositrory;
            this.imgPlayInfoRepository = imgPlayInfoRepository;
            this.newsRepository = newsRepository;
            this.friendlyLinkRepository = friendlyLinkRepository;
            this.pageContentRepository = pageContentRepository;
            this.ownerService = ownerService;
            this.keyValueService = keyValueService;
        }

        /// <summary>
        /// 获取可用导航，排序后
        /// </summary>
        public async Task<List<NavigationDto>> GetNavigationsAsync()
        {
            var naviagationList = await Task.Run(() => navigationRepositrory.QueryNavigationEnabledAsync());
            return ModelConvertUtil<NavigationInfo, NavigationDto>.ModelCopy(naviagationList);
        }

        public async Task<NavigationDto> GetNavigationsAsync(int navId)
        {
            var naviagation = await Task.Run(() => navigationRepositrory.Query(navId));
            if (naviagation == null)
            {
                naviagation = new NavigationInfo();
            }
            return ModelConvertUtil<NavigationInfo, NavigationDto>.ModelCopy(naviagation);
        }

        public async Task<NavigationDto> GetNavigationsParentAsync(int navId)
        {
            var naviagation = await GetNavigationsAsync(navId);
            if (naviagation.ParentId != 0)
            {
                naviagation = await GetNavigationsAsync(naviagation.ParentId);
            }
            return naviagation;
        }

        /// <summary>
        /// 获取轮播图
        /// </summary>
        public async Task<List<ImgPlayDto>> GetImgPlaysAsync(int navId)
        {
            var imgList = await Task.Run(() => imgPlayInfoRepository.GetImgPlayEnabledAsync(navId));
            return ModelConvertUtil<ImgPlayInfo, ImgPlayDto>.ModelCopy(imgList);
        }

        public async Task<List<ImgPlayDto>> GetImgPlaysByTypeAsync(EImageType imageType)
        {
            var imgList = await Task.Run(() => imgPlayInfoRepository.GetImgPlayByTypeAsync(imageType));
            return ModelConvertUtil<ImgPlayInfo, ImgPlayDto>.ModelCopy(imgList);
        }

        public async Task<List<ImgPlayDto>> GetImgPlaysAsync(int navId, EImageType imageType)
        {
            var imgList = await Task.Run(() => imgPlayInfoRepository.GetImgPlaysAsync(navId, imageType));
            return ModelConvertUtil<ImgPlayInfo, ImgPlayDto>.ModelCopy(imgList);
        }

        /// <summary>
        /// 获取列表信息
        /// </summary>
        /// <param name="navId"></param>
        private async Task<List<PageContentDto>> GetPageContentsAsync(int navId)
        {
            var pageContentList = await Task.Run(() => pageContentRepository.GetPageContentAsync(navId));
            return ModelConvertUtil<PageContentInfo, PageContentDto>.ModelCopy(pageContentList);
        }

        /// <summary>
        /// 获取列表信息
        /// </summary>
        /// <param name="navId"></param>
        private async Task<List<PageContentDto>> GetHomeIndexShowDataAsync()
        {
            var pageContentList = await Task.Run(() => pageContentRepository.GetHomeIndexShowDataAsync());
            return ModelConvertUtil<PageContentInfo, PageContentDto>.ModelCopy(pageContentList);
        }

        /// <summary>
        /// 前三个公司动态
        /// </summary>
        /// <returns></returns>
        private async Task<List<NewsDto>> GetTop3News()
        {
            var news = await Task.Run(() => newsRepository.QueryListAsync(1, 3, order: "Id", asc: false));
            return ModelConvertUtil<NewsInfo, NewsDto>.ModelCopy(news.data);
        }

        /// <summary>
        /// 获取友情链接
        /// </summary>
        public async Task<List<FriendlyLinkDto>> GetFriendlyLinksAsync()
        {
            var link = await Task.Run(() => friendlyLinkRepository.GetFriendlyLinkEnabledAsync());
            return ModelConvertUtil<FriendlyLinkInfo, FriendlyLinkDto>.ModelCopy(link);
        }


        /// <summary>
        /// 获取首页信息
        /// </summary>
        public VHomePage GetHomeData()
        {
            VHomePage page = new VHomePage(); 
            var news = GetTop3News();
            var pageContent = GetHomeIndexShowDataAsync();
            var nav = GetNavigationsAsync().Result;

            ///导航的第一个必须是首页，所以可取首页Id
            var narId = nav[0].Id;
            var imagePlay = GetImgPlaysAsync(narId, EImageType.Header);  

            page.News = news.Result;

            page.ImgPlays = imagePlay.Result;
            page.PageContents = pageContent.Result;
            return page;
        }

        /// <summary>
        /// 页脚数据
        /// </summary>
        /// <returns></returns>
        public VFooterPageData GetVFooterPageData()
        {
            VFooterPageData data = new VFooterPageData();
            var companyInfo = ownerService.GetCompanyInfoAsync();
            var nav = GetNavigationsAsync();
            var link = GetFriendlyLinksAsync();
            var imagePlay = GetImgPlaysByTypeAsync(EImageType.Footer);

            data.Owner = companyInfo.Result; 
            data.FriendlyLinks = link.Result;
            data.Navigations = GetVNavigations(nav.Result);

            data.LogoUrl = keyValueService.GetSystemConfig(SystemKey.Logo).Result.Value;
            data.QRCode = keyValueService.GetSystemConfig<List<string>>(SystemKey.QRCode).Result;
            data.FooterBgUrl = imagePlay.Result.FirstOrDefault().ImgUrl;
            return data;
        }

        /// <summary>
        /// 页头数据
        /// </summary>
        /// <returns></returns>
        public VHeaderPageData GetVHeaderPageData()
        {
            VHeaderPageData data = new VHeaderPageData();
            var companyInfo = ownerService.GetCompanyInfoAsync();
            var nav = GetNavigationsAsync(); 

            data.Owner = companyInfo.Result;
            data.Navigations = GetVNavigations(nav.Result);

            data.LogoUrl = keyValueService.GetSystemConfig(SystemKey.Logo).Result.Value;
            return data;
        }

        private List<VNavigation> GetVNavigations(List<NavigationDto> alllist)
        {
            List<VNavigation> result = new List<VNavigation>();
            var rootList = alllist.FindAll(c => c.ParentId == 0);
            foreach (var root in rootList)
            {
                VNavigation tree = ModelConvertUtil<NavigationDto, VNavigation>.ModelCopy(root);
                getTree(alllist, tree.Childs, root.Id);
                result.Add(tree);
            }
            return result;
        }

        private void getTree(List<NavigationDto> alllist, List<VNavigation> showlist, int parentId)
        {
            foreach (var parent in alllist.FindAll(c =>  c.ParentId == parentId))
            {
                VNavigation tree = ModelConvertUtil<NavigationDto, VNavigation>.ModelCopy(parent); 
                getTree(alllist, tree.Childs, tree.Id);
                showlist.Add(tree);
            }
        } 
    }
}
