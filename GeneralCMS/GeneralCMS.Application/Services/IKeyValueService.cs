using GeneralCMS.Models.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GeneralCMS.Application.Services
{
    public interface IKeyValueService : IBaseService
    {
        [AOP.AOPServiceLog("获取系统配置")]
        Task<T> GetSystemConfig<T>(string code);

        [AOP.AOPServiceLog("获取系统配置")]
        Task<SystemConfigDto> GetSystemConfig(string code);
    }
}
