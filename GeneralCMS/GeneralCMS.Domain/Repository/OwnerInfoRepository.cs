using Banana.Uow;
using GeneralCMS.Domain.Repository.Interface;
using GeneralCMS.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeneralCMS.Domain.Repository
{
    /// <summary>
    /// 网站主体者信息仓储
    /// </summary>
    public class OwnerInfoRepository : Repository<OwnerInfo>, IOwnerInfoRepository
    {

    }
}
