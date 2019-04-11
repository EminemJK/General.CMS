using System;
using System.Collections.Generic;
using System.Text;
using Banana.Uow.Models;
using GeneralCMS.Models.Enum;

namespace GeneralCMS.Models.Entities
{
    /// <summary>
    /// 后台管理员表
    /// </summary>
    [Table("T_Admin")]
    public class AdminUserInfo : BaseModel<int>
    {
        /// <summary>
        /// 登录名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 手机
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        public string RealName { get; set; }

        /// <summary>
        /// 启用禁用
        /// </summary>
        public EYesOrNo IsDisable { get; set; }
    }
}
