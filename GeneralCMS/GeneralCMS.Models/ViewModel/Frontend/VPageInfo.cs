using System;
using System.Collections.Generic;
using System.Text;
using Banana.Uow.Models;
using GeneralCMS.Models.Dto;
using GeneralCMS.Models.Enum;

namespace GeneralCMS.Models.ViewModel.Frontend
{
    public class VPageInfo : VBasePage
    {
        public EContentType ContentType => PageContents[0].ContentType;

        public List<PageContentDto> PageContents { get; set; } = new List<PageContentDto>(); 
    }



    public class VPageDetailInfo : VBaseModel
    {
        public PageContentDto Content { get; set; } 
    }
}
