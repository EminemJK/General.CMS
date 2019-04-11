using System;
using System.Collections.Generic;
using System.Text;

namespace GeneralCMS.Models.ViewModel
{
    public class UploadFileModel
    {
        public string fileName { get; set; }
        public string extension { get; set; }

        public string path { get; set; }

        public string fullPath { get; set; }
    }
}
