using System;
using System.Collections.Generic;
using System.Text;

namespace GeneralCMS.Models.ViewModel.Admin
{
    /// <summary>
    /// 用户列表查询条件
    /// </summary>
    public class VUserListConditionInput
    {
        public int pageNum { get; set; } = 1;

        public int pageSize { get; set; } = 10;

        public int draw { get; set; } = 1;

        public string phone { get; set; }

        public string email { get; set; }

        public string realname { get; set; }

        public string username { get; set; }

        public int isdisable { get; set; } = -1;
    }
}
