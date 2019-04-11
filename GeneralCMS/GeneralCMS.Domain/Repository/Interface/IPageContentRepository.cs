using Banana.Uow.Interface;
using Banana.Uow.Models;
using GeneralCMS.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GeneralCMS.Domain.Repository.Interface
{
    /// <summary>
    /// 内容仓储
    /// </summary>
    public interface IPageContentRepository : IRepository<PageContentInfo> 
    {
        /// <summary>
        /// 获取页面数据
        /// </summary>
        Task<List<PageContentInfo>> GetPageContentAsync(int navigationID);

        Task<IPage<PageContentInfo>> GetPageContentAsync(int navigationID, int pageNum, int pageSize);

        /// <summary>
        /// 获取首页展示数据
        /// </summary>
        /// <returns></returns>
        Task<List<PageContentInfo>> GetHomeIndexShowDataAsync();
    }
}
