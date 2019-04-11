using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Banana.Uow.Models;
using GeneralCMS.Models.Dto;
using GeneralCMS.Models.ViewModel.Frontend;

namespace GeneralCMS.Application.Services
{
    public interface IPageContentService : IBaseService
    {

        VPageDetailInfo GetPageContentDetailAsync(int id);

        /// <summary>
        /// 获取列表信息
        /// </summary>
        Task<List<PageContentDto>> GetPageContentsAsync(int navId);
        

        /// <summary>
        /// 获取关于我们
        /// </summary>
        VAboutUs GetAboutUsData(int navId);

        /// <summary>
        /// 自定义页
        /// </summary>
        /// <param name="navId"></param>
        /// <param name="pageNum"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<VPageInfo> GetCustomPage(int navId, int pageNum, int pageSize);
    }
}
