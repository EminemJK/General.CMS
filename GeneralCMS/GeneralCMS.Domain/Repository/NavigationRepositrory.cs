using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Banana.Uow;
using GeneralCMS.Domain.Repository.Interface;
using GeneralCMS.Models.Entities;
using GeneralCMS.Models.Enum;

namespace GeneralCMS.Domain.Repository
{
    /// <summary>
    /// 导航仓储
    /// </summary>
    public class NavigationRepositrory : Repository<NavigationInfo>, INavigationRepositrory
    {
        public async Task<List<NavigationInfo>> QueryNavigationEnabledAsync()
        {
            var res = await QueryListAsync("IsDisable =@IsDisable", new { IsDisable = (int)EYesOrNo.No }, "Sort", asc: true);
            return res.ToList();
        }
    }
}
