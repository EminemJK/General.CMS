using System;
using System.Collections.Generic;
using System.Text;

namespace GeneralCMS.Models.ViewModel.Frontend
{
    public class VPageViewInfo : VBasePage
    {
        public string PerUrl { get; set; }

        public VPageViewInfo(VBasePage basePage, string perUrl)
        {
            this.NavigationId = basePage.NavigationId;

            this.pageCount = basePage.pageCount;
            this.pageNo = basePage.pageNo;
            this.pageSize = basePage.pageSize;
            this.dataCount = basePage.dataCount;

            this.PerUrl = perUrl;
        }
    }
}
