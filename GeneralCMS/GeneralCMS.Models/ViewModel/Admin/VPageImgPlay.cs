using GeneralCMS.Models.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeneralCMS.Models.ViewModel.Admin
{
    public class VPageImgPlay
    {
        public string UploadUrl { get; set; }

        public List<VNavigationImgPlays> NavigationsImgPlay { get; set; } = new List<VNavigationImgPlays>();
    }

    public class VNavigationImgPlays : NavigationDto
    {
        public string pageId
        {
            get
            {
                return Banana.Utility.NPinyin.Pinyin.GetPinyin(this.Name).Replace(" ","") + this.Id;
            }
        }

        /// <summary>
        /// 头部
        /// </summary>
        public List<ImgPlayDto> HeaderImgPlays { get; set; } = new List<ImgPlayDto>(); 
    }
}
