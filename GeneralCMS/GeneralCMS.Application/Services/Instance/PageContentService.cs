using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GeneralCMS.Models.ViewModel.Frontend;
using GeneralCMS.Models.Dto;
using Banana.Utility.Common;
using GeneralCMS.Models.Entities;
using GeneralCMS.Domain.Repository.Interface;
using Banana.Uow.Models;
using GeneralCMS.Common.LogUtility;

namespace GeneralCMS.Application.Services.Instance
{
    public class PageContentService :BaseService, IPageContentService
    {
        private readonly INewsRepository newsRepository;
        private readonly IPageContentRepository pageContentRepository;
        private readonly INavigationRepositrory navigationRepositrory;

        public PageContentService(INewsRepository newsRepository,IPageContentRepository pageContentRepository, 
            INavigationRepositrory navigationRepositrory, ILoggerHelper logger, IServiceProvider serviceProvider) : base(logger, serviceProvider)
        {
            this.newsRepository = newsRepository;
            this.pageContentRepository = pageContentRepository;
            this.navigationRepositrory = navigationRepositrory;
        }

        public async Task<List<PageContentDto>> GetPageContentsAsync(int navId)
        {
            var pageContentList = await Task.Run(() => pageContentRepository.GetPageContentAsync(navId));
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

        public VAboutUs GetAboutUsData(int navId)
        {
            var pageCon = GetPageContentsAsync(navId);
            var news = GetTop3News();
            VAboutUs aboutUs = new VAboutUs();
            aboutUs.NavigationId = navId;
            aboutUs.PageContents = pageCon.Result;
            aboutUs.News = news.Result;
            return aboutUs;
        }

        public async Task<VPageInfo> GetCustomPage(int navId, int pageNum, int pageSize)
        {
            VPageInfo pageInfo = new VPageInfo();
            pageInfo.NavigationId = navId;
            var pageCon = await GetPageContentsAsync(navId);
            if (pageCon.Count > 0)
            {
                if (pageCon[0].ContentType == Models.Enum.EContentType.PageList)
                {
                    if (pageNum < 0)
                    {
                        pageNum = 1;
                    }
                    if (pageSize < 0 || pageSize > 20)
                    {
                        pageSize = 5;
                    }
                    PagingUtil<PageContentDto> page = new PagingUtil<PageContentDto>(pageCon, pageSize, pageNum);
                    pageInfo.pageCount = page.pageCount;
                    pageInfo.pageNo = page.pageNo;
                    pageInfo.pageSize = page.pageSize;
                    pageInfo.dataCount = page.dataCount;
                    pageInfo.PageContents = page;
                }
                else
                {
                    pageInfo.PageContents = pageCon; 
                }
            }
             
            return pageInfo;
        }

        public VPageDetailInfo GetPageContentDetailAsync(int id)
        {
            VPageDetailInfo detailInfo = new VPageDetailInfo();
            detailInfo.Content = GetPageDetailInfo(id).Result;


            return detailInfo;
        }

        private async Task<PageContentDto> GetPageDetailInfo(int id)
        {
            var res = await Task.Run(() => pageContentRepository.QueryAsync(id));
            return ModelConvertUtil<PageContentInfo, PageContentDto>.ModelCopy(res);
        }
    }
}
