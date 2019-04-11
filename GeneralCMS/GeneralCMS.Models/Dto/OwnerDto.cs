using System;
using System.Collections.Generic;
using System.Text;

namespace GeneralCMS.Models.Dto
{
    public class OwnerDto : DtoBaseModel<int>
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Mobile { get; set; }

        public string Address { get; set; }

        public string LinkMan { get; set; }

        public string MapIFrameUrl { get; set; }
    }
}
