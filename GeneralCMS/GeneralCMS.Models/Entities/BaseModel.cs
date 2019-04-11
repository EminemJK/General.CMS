using System;
using System.Collections.Generic;
using System.Text;
using Banana.Uow.Models;

namespace GeneralCMS.Models.Entities
{
    /// <summary>
    /// 基础信息类
    /// </summary>
    /// <typeparam name="Key"></typeparam>
    public class BaseModel<Key>: IEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public Key Id { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [ExceptUpdate]
        public virtual DateTime CreateTime { get; set; }
    }
}
