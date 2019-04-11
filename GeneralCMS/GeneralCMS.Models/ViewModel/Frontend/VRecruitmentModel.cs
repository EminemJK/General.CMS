using GeneralCMS.Models.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeneralCMS.Models.ViewModel.Frontend
{
    public class VRecruitmentModel : VBaseModel
    {
        public List<RecruitmentDto> Recruitments { get; set; }

        public string Benefits { get; set; }
    }
}
