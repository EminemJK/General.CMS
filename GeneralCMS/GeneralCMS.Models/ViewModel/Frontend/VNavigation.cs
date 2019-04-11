using GeneralCMS.Models.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeneralCMS.Models.ViewModel.Frontend
{
    public class VNavigation : NavigationDto
    {
        public List<VNavigation> Childs { get; set; } = new List<VNavigation>();
    }
}
