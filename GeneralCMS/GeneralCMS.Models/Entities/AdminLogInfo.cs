using Banana.Uow.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeneralCMS.Models.Entities
{
    /// <summary>
    /// 后台管理操作日志表
    /// </summary>
    [Table("T_AdminLog")]
    public class AdminLogInfo : BaseModel<int>
    {
        public int AdminID { get; set; }

        [Computed]
        [Write(false)]
        public string AdminRealName { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string IP { get; set; }
    }
}
