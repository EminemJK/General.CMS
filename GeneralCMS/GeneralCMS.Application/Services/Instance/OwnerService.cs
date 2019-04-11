using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Banana.Utility.Common;
using GeneralCMS.Models.Dto;
using GeneralCMS.Models.ViewModel.Frontend;
using GeneralCMS.Common.LogUtility;
using GeneralCMS.Domain.Repository.Interface;
using GeneralCMS.Models.Entities;

namespace GeneralCMS.Application.Services.Instance
{
    public class OwnerService :BaseService, IOwnerService
    {
        private readonly IOwnerInfoRepository ownerInfoRepository;

        public OwnerService(IOwnerInfoRepository ownerInfoRepository, ILoggerHelper logger, IServiceProvider serviceProvider) : base(logger, serviceProvider)
        {
            this.ownerInfoRepository = ownerInfoRepository;
        }

        /// <summary>
        /// 获取企业信息
        /// </summary>
        /// <returns></returns>
        public async Task<OwnerDto> GetCompanyInfoAsync()
        {
            var companyInfo = await ownerInfoRepository.QueryListAsync();
            return ModelConvertUtil<OwnerInfo, OwnerDto>.ModelCopy(companyInfo.ToList().FirstOrDefault());
        }

        public async Task<VContactInfo> GetContactInfo()
        {
            var res = await GetCompanyInfoAsync();
            VContactInfo contactInfo = new VContactInfo();
            contactInfo.Owner = res;
            return contactInfo;
        }
    }
}
