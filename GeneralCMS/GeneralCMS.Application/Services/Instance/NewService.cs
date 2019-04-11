using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Banana.Utility.Common;
using GeneralCMS.Models.Dto;
using GeneralCMS.Models.ViewModel.Frontend;
using GeneralCMS.Common.LogUtility;
using GeneralCMS.Domain.Repository.Interface;
using GeneralCMS.Models.Entities;

namespace GeneralCMS.Application.Services.Instance
{
    public class NewService :BaseService, INewService
    {
        private readonly INewsRepository newsRepository;
        public NewService(INewsRepository newsRepository, ILoggerHelper logger, IServiceProvider serviceProvider) : base(logger, serviceProvider)
        {
            this.newsRepository = newsRepository;
        }

        public VNewDetailPage GetNewDetailPage(int id)
        {
            var res = new VNewDetailPage();
            var info = newsRepository.Query(id);
            res.Content = ModelConvertUtil<NewsInfo, NewsDto>.ModelCopy(info);

            return res;
        }
         

        public async Task<VNewPage> GetNewPageList(int pageNum, int pageSize )
        {
            if (pageNum <= 0)
            {
                pageNum = 1;
            }
            if (pageSize <= 0 || pageSize > 10)
            {
                pageSize = 5;
            }
            var aa = newsRepository.QueryList();
            var page = await Task.Run(()=> newsRepository.GetNewPageList(pageNum, pageSize));
            VNewPage newPage = new VNewPage();
            newPage.pageCount = page.pageCount;
            newPage.pageNo = page.pageNo;
            newPage.pageSize = page.pageSize;
            newPage.dataCount = page.dataCount;
            newPage.data = ModelConvertUtil<NewsInfo, NewsDto>.ModelCopy(page.data);
            return newPage;
        }
    }
}
