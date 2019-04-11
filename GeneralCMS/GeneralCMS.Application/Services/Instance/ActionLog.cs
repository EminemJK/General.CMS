using System;
using System.Collections.Generic;
using System.Text;
using GeneralCMS.Models.Dto;
using GeneralCMS.Domain.Repository.Interface;
using Banana.Utility.Common;
using GeneralCMS.Models.Entities;
using GeneralCMS.Common.LogUtility;
using System.Threading.Tasks;
using GeneralCMS.Models.ViewModel.Admin;

namespace GeneralCMS.Application.Services.Instance
{
    public class ActionLog : BaseService, IActionLog
    {
        private readonly IAdminLogRepository logRepository; 
        public ActionLog(IAdminLogRepository logRepository, ILoggerHelper logger, IServiceProvider serviceProvider) : base(logger, serviceProvider)
        {
            this.logRepository = logRepository;
        }

      
        public async Task Write(AdminLogDto log)
        {
            try
            {
                var logModel = ModelConvertUtil<AdminLogDto, AdminLogInfo>.ModelCopy(log);
                await Task.Run(() => logRepository.InsertAsync(logModel));
            }
            catch(Exception ex)
            {
                logger.Error(typeof(ActionLog), "AOP记录",ex);
            }
        }

        public VActionLogs GetLogs(DateTime starDate, DateTime endDate)
        {
            var res = new VActionLogs();
            var logs = logRepository.QueryByTime(starDate, endDate).Result;
           
            logs.ForEach(l => {
                var yyyyMMdd = l.CreateTime.ToString("yyyy-MM-dd");
                if (!res.Logs.ContainsKey(yyyyMMdd))
                {
                    res.Logs[yyyyMMdd] = new List<AdminLogDto>();
                }
                res.Logs[yyyyMMdd].Add(ModelConvertUtil<AdminLogInfo, AdminLogDto>.ModelCopy(l));
            });
             
            return res;
        }

    }
}
