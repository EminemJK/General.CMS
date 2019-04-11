using Banana.Uow;
using GeneralCMS.Domain.Repository.Interface;
using GeneralCMS.Models.Entities;
using GeneralCMS.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Banana.Uow.Models;

namespace GeneralCMS.Domain.Repository
{
    /// <summary>
    /// 内容仓储
    /// </summary>
    public class PageContentRepository : Repository<PageContentInfo>, IPageContentRepository
    {
        /// <summary>
        /// 获取未被禁用的单页信息
        /// </summary>
        /// <param name="navigationID"></param>
        /// <returns></returns>
        public async Task<List<PageContentInfo>> GetPageContentAsync(int navigationID)
        {
            var res = await QueryListAsync("IsDisable =@IsDisable and NavigationID = @NavigationID", new { IsDisable = (int)EYesOrNo.No, NavigationID = navigationID }, "Sort", asc: false);
            return res.ToList();
        }

        /// <summary>
        /// 获取未被禁用的单页信息
        /// </summary>
        /// <param name="navigationID"></param>
        /// <returns></returns>
        public async Task<IPage<PageContentInfo>> GetPageContentAsync(int navigationID, int pageNum, int pageSize)
        {
            var res = await QueryListAsync(pageNum, pageSize, "IsDisable =@IsDisable and NavigationID = @NavigationID", new { IsDisable = (int)EYesOrNo.No, NavigationID = navigationID }, "Sort", asc: false);
            return res;
        }


        /// <summary>
        /// 首页数据
        /// </summary>
        /// <returns></returns>
        public async Task<List<PageContentInfo>> GetHomeIndexShowDataAsync()
        {
            var res = await QueryListAsync("IsDisable =@IsDisable and IsHomePageShows = @IsHomePageShows", new { IsDisable = (int)EYesOrNo.No, IsHomePageShows = (int)EYesOrNo.Yes }, "Sort", asc: true);
            return res.ToList();
        }
    }
}
