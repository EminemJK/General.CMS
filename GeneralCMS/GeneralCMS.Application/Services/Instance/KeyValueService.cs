using GeneralCMS.Domain.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using GeneralCMS.Models.Dto;
using GeneralCMS.Models.Entities;
using Banana.Utility.Common;
using GeneralCMS.Common.LogUtility;

namespace GeneralCMS.Application.Services.Instance
{
    public class KeyValueService :BaseService, IKeyValueService
    {
        private readonly ISystemConfigRepository systemConfigRepository;
        public KeyValueService(ISystemConfigRepository systemConfigRepository, ILoggerHelper logger, IServiceProvider serviceProvider) : base(logger, serviceProvider)
        {
            this.systemConfigRepository = systemConfigRepository;
        }

        public async Task<T> GetSystemConfig<T>(string code)
        { 
            var res = await Task.Run(() => systemConfigRepository.GetValueByCode(code));
            
            return string.IsNullOrEmpty(res.Value) ? default(T) : JsonConvert.DeserializeObject<T>(res.Value);
        }
         
        public async Task<SystemConfigDto> GetSystemConfig(string code)
        {
            var res = await Task.Run(() => systemConfigRepository.GetValueByCode(code));
            return ModelConvertUtil<SystemConfigInfo, SystemConfigDto>.ModelCopy(res);
        }
    }
}
