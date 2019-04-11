using System;
using System.Collections.Generic;
using System.Text;
using GeneralCMS.Models.Dto;

namespace GeneralCMS.Models.ViewModel.Frontend
{
    public class VAboutUs: VBaseModel
    {
        public List<PageContentDto> PageContents { get; set; } = new List<PageContentDto>();

        public List<NewsDto> News { get; set; } = new List<NewsDto>();
    }
}
