using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Banana.Uow.Interface;
using Banana.Uow.Models;
using GeneralCMS.Models.Entities;

namespace GeneralCMS.Domain.Repository.Interface
{
    /// <summary>
    /// 文章仓储
    /// </summary>
    public interface INewsRepository : IRepository<NewsInfo>
    {
        Task<IPage<NewsInfo>> GetNewPageList(int pageNum, int pageSize);
    }
}
