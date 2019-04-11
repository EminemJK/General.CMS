using GeneralCMS.Models.ViewModel.Frontend;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeneralCMS.Application.Services
{
    public interface IEmploymentService : IBaseService
    {
        /// <summary>
        /// 获取招聘列表
        /// </summary>
        VRecruitmentModel GetVRecruitmentList();
    }
}
