using Banana.Uow;
using GeneralCMS.Domain.Repository.Interface;
using GeneralCMS.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace GeneralCMS.Domain.Repository
{
    /// <summary>
    /// 操作日志仓储
    /// </summary>
    public class AdminLogRepository : Repository<AdminLogInfo>, IAdminLogRepository
    {
        public async Task<List<AdminLogInfo>> QueryByTime(DateTime starDate, DateTime endDate)
        {
            var sDateStr = starDate.ToString("yyyy-MM-dd 00:00:00");
            var eDateStr = endDate.ToString("yyyy-MM-dd 23:59:59");
            Repository<AdminUserInfo> adminRepo = new Repository<AdminUserInfo>();
            string sql = string.Format("select log.*,u.RealName AdminRealName from {0} log left join {1} u on log.AdminId=u.Id where log.createTime>=@sCreateTime and log.createTime<=@eCreateTime order by log.CreateTime desc", this.TableName, adminRepo.TableName);
            var data = await this.DBConnection.QueryAsync<AdminLogInfo>(sql, new { sCreateTime = sDateStr, eCreateTime = eDateStr });
            return data.ToList();
        }
    }
}
