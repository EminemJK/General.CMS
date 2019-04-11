using System;
using System.Collections.Generic;
using System.Text;

namespace GeneralCMS.Models.ViewModel.Frontend
{
    public class VBreadcrumb
    {
        public string Title { get; set; }

        public string Subtitle { get; set; }

        public string SubUrl { get; set; }

        public string BackgroundImageUrl { get; set; }

        /// <summary>
        /// 面包屑导航
        /// </summary>
        /// <param name="Title">大标题</param>
        /// <param name="Subtitle">小标题</param>
        /// <param name="SubUrl">小标题链接</param>
        /// <param name="BackgroundImageUrl">背景图</param>
        public VBreadcrumb(string Title, string Subtitle, string SubUrl, string BackgroundImageUrl = "")
        {
            this.Title = Title;
            this.Subtitle = Subtitle;
            this.SubUrl = SubUrl;
            this.BackgroundImageUrl = BackgroundImageUrl;
        }
    }
}
