using System;
using System.Collections.Generic;
using System.Text;

namespace GeneralCMS.Models.ViewModel.Frontend
{
    public class VBasePage: VBaseModel
    {
        public int dataCount { get; set; }

        public int pageCount { get; set; }

        public int pageNo { get; set; }
        public int pageSize { get; set; }
    }
}
