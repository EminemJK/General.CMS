using Banana.Uow.Interface;
using GeneralCMS.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GeneralCMS.Domain.Repository.Interface
{
    /// <summary>
    /// 招聘信息仓储
    /// </summary>
    public interface IRecruitmentRepository : IRepository<RecruitmentInfo>
    { 
    }
}
