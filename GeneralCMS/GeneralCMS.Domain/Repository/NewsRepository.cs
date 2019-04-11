using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Banana.Uow;
using Banana.Uow.Models;
using GeneralCMS.Domain.Repository.Interface;
using GeneralCMS.Models.Entities;
using GeneralCMS.Models.Enum;

namespace GeneralCMS.Domain.Repository
{
    /// <summary>
    /// 文章仓储
    /// </summary>
    public class NewsRepository : Repository<NewsInfo>, INewsRepository
    {
        public async Task<IPage<NewsInfo>> GetNewPageList(int pageNum, int pageSize)
        {
            var page = await QueryListAsync(pageNum, pageSize, "IsDisable=@IsDisable", new { IsDisable = (int)EYesOrNo.No }, "Sort", asc: false);
            return page;
        }
    }
}
