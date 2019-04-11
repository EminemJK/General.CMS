using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GeneralCMS.Models.ViewModel.Admin
{
    public class VNavigationEdit
    {
        public int navigationId { get; set; }

        public int parentNavigationId { get; set; }

        [Required(ErrorMessage = "请填写名称")]
        public string name { get; set; }
    }
}
