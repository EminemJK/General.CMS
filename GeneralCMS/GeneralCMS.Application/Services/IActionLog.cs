using GeneralCMS.Models.Dto;
using GeneralCMS.Models.ViewModel.Admin;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GeneralCMS.Application.Services
{
    /// <summary>
    /// 操作日志
    /// </summary>
    public interface IActionLog : IBaseService
    {
        Task Write(AdminLogDto log);

        [AOP.AOPServiceLog("日志查询")]
        VActionLogs GetLogs(DateTime starDate, DateTime endDate);
    }
}
