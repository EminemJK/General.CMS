using Banana.Uow.Interface;
using GeneralCMS.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GeneralCMS.Domain.Repository.Interface
{
    /// <summary>
    /// 操作日志仓储
    /// </summary>
    public interface IAdminLogRepository : IRepository<AdminLogInfo>
    {
        /// <summary>
        /// 按日期查询日志
        /// </summary>
        Task<List<AdminLogInfo>> QueryByTime(DateTime starDate, DateTime endDate);
    }
}
