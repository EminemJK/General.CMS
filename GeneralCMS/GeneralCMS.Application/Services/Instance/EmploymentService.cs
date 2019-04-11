using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Banana.Utility.Common;
using GeneralCMS.Application.Common;
using GeneralCMS.Models.Dto;
using GeneralCMS.Models.ViewModel.Frontend;
using GeneralCMS.Common.LogUtility;
using GeneralCMS.Domain.Repository.Interface;
using GeneralCMS.Models.Entities;

namespace GeneralCMS.Application.Services.Instance
{
    public class EmploymentService :BaseService, IEmploymentService
    {
        private readonly IKeyValueService keyValueService;
        private readonly IRecruitmentRepository recruitmentRepository;
        public EmploymentService(IRecruitmentRepository recruitmentRepository, IKeyValueService keyValueService, ILoggerHelper logger, IServiceProvider serviceProvider) 
            : base(logger, serviceProvider)
        {
            this.recruitmentRepository = recruitmentRepository;
            this.keyValueService = keyValueService;
        }

        /// <summary>
        /// 获取招聘列表
        /// </summary>
        public VRecruitmentModel GetVRecruitmentList()
        {
            VRecruitmentModel res = new VRecruitmentModel();
            res.Recruitments = GetRecruitmentDtos().Result;
            res.Benefits = keyValueService.GetSystemConfig(SystemKey.WelfareKey).Result.Value;

            return res;
        }

        private async Task<List<RecruitmentDto>> GetRecruitmentDtos()
        {
            var res = await recruitmentRepository.QueryListAsync(order: "Sort", asc: false);
            return ModelConvertUtil<RecruitmentInfo, RecruitmentDto>.ModelCopy(res.ToList());
        }
         
    }
}
