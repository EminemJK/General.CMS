using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Banana.Uow.Interface;
using GeneralCMS.Models.Entities;
using GeneralCMS.Models.Enum;

namespace GeneralCMS.Domain.Repository.Interface
{
    /// <summary>
    /// 导航仓储
    /// </summary>
    public interface INavigationRepositrory : IRepository<NavigationInfo>
    {
        /// <summary>
        /// 获取启用禁用的导航
        /// </summary>
        Task<List<NavigationInfo>> QueryNavigationEnabledAsync();
    }
}
