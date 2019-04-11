using GeneralCMS.Models.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeneralCMS.Models.Dto
{
    public class PageContentDto : DtoBaseModel<int>
    {
        /// <summary>
        /// 内容表现形式
        /// </summary>
        public EContentType ContentType { get; set; }

        /// <summary>
        /// 导航Id
        /// </summary>
        public int NavigationID { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// URL地址
        /// </summary>
        public string ImgUrl { get; set; }

        /// <summary>
        /// 图标代码，来自
        /// </summary>
        public string IconCode { get; set; }
        /// <summary>
        /// 是否禁用
        /// </summary>
        public EYesOrNo IsDisable { get; set; }

        /// <summary>
        /// 是否首页展示
        /// </summary>
        public EYesOrNo IsHomePageShows { get; set; }

        /// <summary>
        /// 板块名称
        /// </summary>
        public string SectionName { get; set; }

        /// <summary>
        /// 板块名称的副标题
        /// </summary>
        public string Subhead { get; set; }

        /// <summary>
        /// 排序值
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
    }
}
