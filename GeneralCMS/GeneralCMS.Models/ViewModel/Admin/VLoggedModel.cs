using System;
using System.Collections.Generic;
using System.Text;

namespace GeneralCMS.Models.ViewModel.Admin
{
    public class VLoggedModel
    {
        public int ID { get; set; }
        /// <summary>
        /// 登录名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 手机
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 身份
        /// </summary>
        public string Role { get; set; }

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
        public DateTime LoginTime { get; set; }

        /// <summary>
        /// 登录IP
        /// </summary>
        public string IP { get; set; }

        public VLoggedModel()
        {
            this.LoginTime = DateTime.Now;
        }
    }
}
