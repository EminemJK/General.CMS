using System;
using System.Collections.Generic;
using System.Text;
using GeneralCMS.Models.Enum;
namespace GeneralCMS.Models.Dto
{
    /// <summary>
    /// 导航表
    /// </summary>
    public class NavigationDto: DtoBaseModel<int>
    {
        /// <summary>
        /// 所属上级Id
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// URL地址
        /// </summary>
        public string Url
        {
            get
            {
                if (string.IsNullOrEmpty(_url)|| _url.Equals("/") || _url.Contains("navId="))
                {
                    return _url;
                }
                if (_url.Contains("?") && _url.Contains("/"))
                {
                    _url += "&navId=" + Id;
                }
                else if (!_url.Contains("navId") && _url.Contains("/"))
                {
                    _url += "?navId=" + Id;
                }
                return _url;
            }
            set { this._url = value; }
        }

        private string _url { get; set; }

        /// <summary>
        /// URL跳转类型，0 新页面跳转，1 当前页跳转
        /// </summary>
        public EYesOrNo Target { get; set; }

        /// <summary>
        /// 是否禁用
        /// </summary>
        public EYesOrNo IsDisable { get; set; }

        /// <summary>
        /// 是否允许禁用
        /// </summary>
        public EYesOrNo CanDisable { get; set; }

        /// <summary>
        /// 排序值
        /// </summary>
        public int Sort { get; set; }
    }
}
