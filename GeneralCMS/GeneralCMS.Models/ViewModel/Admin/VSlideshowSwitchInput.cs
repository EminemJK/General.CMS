using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using GeneralCMS.Models.Enum;
namespace GeneralCMS.Models.ViewModel.Admin
{
    public class VSlideshowSwitchInput
    {
        [Required(ErrorMessage = "请勾选Id")]
        public List<int> Ids { get; set; }

        public EYesOrNo Off { get; set; }
    }
}
