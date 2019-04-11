using System;
using System.Collections.Generic;
using System.Text;
using Banana.Uow.Models;
using GeneralCMS.Models.Enum;

namespace GeneralCMS.Models.Entities
{
    /// <summary>
    /// 轮播图片表
    /// </summary>
    [Table("T_ImgPlay")]
    public class ImgPlayInfo : BaseModel<int>
    {
        /// <summary>
        /// 导航Id
        /// </summary>
        public int NavigationID { get; set; }

        /// <summary>
        /// 类型或分类 0头部 1底部
        /// </summary>
        public EImageType Type { get; set; }

        /// <summary>
        /// 名称，用途说明
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 标题，轮播图片上显示的标题，按需使用
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 副标题
        /// </summary>
        public string Subhead { get; set; }

        /// <summary>
        /// URL地址
        /// </summary>
        public string ImgUrl { get; set; }

        /// <summary>
        /// 点击后跳转的地址
        /// </summary>
        public string LinkUrl { get; set; }

        /// <summary>
        /// URL跳转类型，0 新页面跳转，1 当前页跳转
        /// </summary>
        public EYesOrNo Target { get; set; }

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
