using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GeneralCMS.Models.ViewModel.Admin
{
    public class VLoginInfo
    {
        [Required(AllowEmptyStrings = false,ErrorMessage ="用户名不能为空")]
        [StringLength(maximumLength: 8)]
        public string UserName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "密码不能为空")]
        [StringLength(maximumLength: 16)]
        public string Password { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "验证失效，请刷新界面")]
        public string Code { get; set; }
    }
}
