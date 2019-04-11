using Banana.Uow;
using GeneralCMS.Domain.Repository.Interface;
using GeneralCMS.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralCMS.Domain.Repository
{
    /// <summary>
    /// 系统配置仓储
    /// </summary>
    public class SystemConfigRepository : Repository<SystemConfigInfo>, ISystemConfigRepository
    {
        public async Task<SystemConfigInfo> GetValueByCode(string code)
        {
            var res = await QueryListAsync("Code=@Code", new { Code = code });
            return res.ToList().FirstOrDefault();
        }
    }
}
