using Banana.Uow.Models;
using GeneralCMS.Models.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeneralCMS.Models.Entities
{
    /// <summary>
    /// 文章分类表
    /// </summary>
    [Table("T_Category")]
    public class CategoryInfo : BaseModel<int>
    {
        /// <summary>
        /// 所属上级Id
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// 所属分类Id路径，如：,1,3,5,
        /// </summary>
        public string ParentIdPath { get; set; }

        /// <summary>
        /// 分类名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 分类图标的URL地址，备用
        /// </summary>
        public string ImgUrl { get; set; }

        /// <summary>
        /// 是否禁用
        /// </summary>
        public EYesOrNo IsDisable { get; set; }

        /// <summary>
        /// 排序值
        /// </summary>
        public int Sort { get; set; }
    }
}
