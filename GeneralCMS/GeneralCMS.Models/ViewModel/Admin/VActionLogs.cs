using System;
using System.Collections.Generic;
using System.Text;
using GeneralCMS.Models.Dto;

namespace GeneralCMS.Models.ViewModel.Admin
{
    public class VActionLogs
    {
        public Dictionary<string, List<AdminLogDto>> Logs { get; set; } = new Dictionary<string, List<AdminLogDto>>();
    }
}
