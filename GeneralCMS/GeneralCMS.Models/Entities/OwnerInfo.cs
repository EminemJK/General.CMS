using Banana.Uow.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeneralCMS.Models.Entities
{
    /// <summary>
    /// 网站主体者信息
    /// </summary>
    [Table("T_OwnerInfo")]
    public class OwnerInfo : BaseModel<int>
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Mobile { get; set; }

        public string Address { get; set; }

        public string LinkMan { get; set; }

        public string MapIFrameUrl { get; set; }
    }
}
