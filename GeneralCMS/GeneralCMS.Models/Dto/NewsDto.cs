using System;
using System.Collections.Generic;
using System.Text;
using GeneralCMS.Models.Enum;

namespace GeneralCMS.Models.Dto
{
    /// <summary>
    /// 文章信息
    /// </summary>
    public class NewsDto : DtoBaseModel<int>
    {
        /// <summary>
        /// 所属分类Id
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 文章首图或缩略图的URL地址
        /// </summary>
        public string ImgUrl { get; set; }

        /// <summary>
        /// 作者
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// 摘要
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// 来源
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// 文章内容，编辑器内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 发布时间，后台可更改
        /// </summary>
        public DateTime ReleaseTime { get; set; }

        /// <summary>
        /// 阅读量
        /// </summary>
        public int ViewCount { get; set; }

        /// <summary>
        /// 的值
        /// </summary>
        public string MetaKeywords { get; set; }

        /// <summary>
        /// 的值
        /// </summary>
        public string MetaDescription { get; set; }

        /// <summary>
        /// 是否禁用
        /// </summary>
        public EYesOrNo IsDisable { get; set; }

        /// <summary>
        /// 排序值
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 添加人的Id
        /// </summary>
        public int CreateAdminId { get; set; }

        /// <summary>
        /// 更新人的Id
        /// </summary>
        public int UpdateAdminId { get; set; }
    }
}
