using GeneralCMS.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GeneralCMS.Models.Dto
{
    /// <summary>
    /// 后台管理员信息
    /// </summary>
    public class AdminUserDto : DtoBaseModel<int>
    {
        /// <summary>
        /// 登录名
        /// </summary>
        [Required(ErrorMessage = "用户名未填写")]
        [Display(Name = "用户名")]
        [StringLength(maximumLength: 8, MinimumLength = 4, ErrorMessage = "用户名长度限制为4~8位")]
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Display(Name = "密码")]
        [StringLength(maximumLength: 20, MinimumLength = 6, ErrorMessage = "密码长度限制为6~20位")]
        public string Password { get; set; }

        /// <summary>
        /// 确认密码
        /// </summary>
        [Compare("Password")]
        public string Password2 { get; set; }

        /// <summary>
        /// 手机
        /// </summary>
        [Required(ErrorMessage = "联系方式未填写")]
        [Display(Name = "联系方式")] 
        [RegularExpression(@"^1\d{10}$", ErrorMessage = "手机号码格式不正确")]
        public string Mobile { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [StringLength(100, ErrorMessage = "邮箱地址过长")]
        [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "邮箱格式不正确")]
        public string Email { get; set; } = "";

        /// <summary>
        /// 真实姓名
        /// </summary>
        [Required(ErrorMessage = "姓名未填写")]
        [Display(Name = "姓名")]
        [StringLength(20, ErrorMessage = "姓名过长")]
        public string RealName { get; set; }

        /// <summary>
        /// 启用禁用
        /// </summary>
        public EYesOrNo IsDisable { get; set; }

        public string IsDisableString
        {
            get
            {
                return IsDisable == EYesOrNo.No ? "启用" : "禁用";
            }
        }
    }
}
