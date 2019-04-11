using Banana.Uow.Interface;
using GeneralCMS.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GeneralCMS.Domain.Repository.Interface
{
    /// <summary>
    /// 友情链接仓储
    /// </summary>
    public interface IFriendlyLinkRepository : IRepository<FriendlyLinkInfo>
    {
        Task<List<FriendlyLinkInfo>> GetFriendlyLinkEnabledAsync();
    }
}
