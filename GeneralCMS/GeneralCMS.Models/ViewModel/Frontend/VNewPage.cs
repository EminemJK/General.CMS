using Banana.Uow.Models;
using GeneralCMS.Models.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeneralCMS.Models.ViewModel.Frontend
{
    public class VNewPage : VBasePage
    {
        public List<NewsDto> data { get; set; } 
    }

    public class VNewDetailPage : VBasePage
    {
        public NewsDto Content { get; set; }
    }
}
