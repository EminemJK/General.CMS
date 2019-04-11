using Banana.Uow;
using GeneralCMS.Domain.Repository.Interface;
using GeneralCMS.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralCMS.Domain.Repository
{
    /// <summary>
    /// 招聘信息仓储
    /// </summary>
    public class RecruitmentRepository : Repository<RecruitmentInfo>, IRecruitmentRepository
    {
         
    }
}
