using System;
using System.Collections.Generic;
using System.Text;

namespace GeneralCMS.Models.Dto
{
    public class AdminLogDto : DtoBaseModel<int>
    {
        public int AdminID { get; set; }

        public string AdminRealName { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string IP { get; set; }
    }
}
