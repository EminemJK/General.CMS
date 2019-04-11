using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Banana.Uow.Interface;
using GeneralCMS.Models.Entities;

namespace GeneralCMS.Domain.Repository.Interface
{
    /// <summary>
    /// 系统配置仓储
    /// </summary>
    public interface ISystemConfigRepository : IRepository<SystemConfigInfo>
    {
        /// <summary>
        /// 根据code 获取
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        Task<SystemConfigInfo> GetValueByCode(string code);
    }
}
