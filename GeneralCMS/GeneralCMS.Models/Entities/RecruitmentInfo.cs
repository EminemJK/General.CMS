using System;
using System.Collections.Generic;
using System.Text;
using Banana.Uow.Models;

namespace GeneralCMS.Models.Entities
{
    /// <summary>
    /// 招聘信息
    /// </summary>
    [Table("T_Recruitment")]
    public class RecruitmentInfo : BaseModel<int>
    {
        /// <summary>
        /// 职位名称
        /// </summary>
        public string JobTitle { get; set; }

        /// <summary>
        /// 工作地点
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// 薪酬
        /// </summary>
        public string Salary { get; set; }

        /// <summary>
        /// 招聘人数
        /// </summary>
        public int RecruitingNum { get; set; }

        /// <summary>
        /// 职位描述
        /// </summary>
        public string JobDescription { get; set; }

        /// <summary>
        /// 发布人
        /// </summary>
        public int CreateAdminId { get; set; }

        /// <summary>
        /// 最后更新的ID
        /// </summary>
        public int UpdateAdminId { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }

        public int Sort { get; set; }
    }
}
