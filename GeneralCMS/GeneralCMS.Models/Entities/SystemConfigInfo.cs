using System;
using System.Collections.Generic;
using System.Text;
using Banana.Uow.Models;

namespace GeneralCMS.Models.Entities
{
    /// <summary>
    /// 系统配置表
    /// </summary>
    [Table("T_SystemConfig")]
    public class SystemConfigInfo : BaseModel<int>
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 代码，用于代码中的标识，而不是记住 Id
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 配置值，根据需要存储相关字符串、数字、JSON等
        /// </summary>
        public string Value { get; set; }
    }
}
