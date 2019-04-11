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
    /// 友情链接仓储
    /// </summary>
    public class FriendlyLinkRepository : Repository<FriendlyLinkInfo>, IFriendlyLinkRepository
    {
        public async Task<List<FriendlyLinkInfo>> GetFriendlyLinkEnabledAsync()
        {
            var res = await QueryListAsync("IsDisable=@IsDisable", new { IsDisable = (int)EYesOrNo.No }, "Sort", asc: true);
            return res.ToList();
        }
    }
}
