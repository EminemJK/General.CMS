using System;
using System.Collections.Generic;
using System.Text;

namespace GeneralCMS.Models.Dto
{
    /// <summary>
    /// 基础信息类
    /// </summary>
    /// <typeparam name="Key"></typeparam>
    public class DtoBaseModel<Key>
    {
        /// <summary>
        /// 主键
        /// </summary>
        public Key Id { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreateTime { get; set; }

        public virtual string CreateTimeString
        {
            get
            {
                return this.CreateTime.ToString("yyyy-MM-dd HH:mm:ss");
            }
        }
    }
}
