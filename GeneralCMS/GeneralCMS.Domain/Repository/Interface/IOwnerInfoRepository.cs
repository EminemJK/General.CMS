using System;
using System.Collections.Generic;
using System.Text;
using Banana.Uow.Interface;
using GeneralCMS.Models.Entities;

namespace GeneralCMS.Domain.Repository.Interface
{
    /// <summary>
    /// 网站主体者信息仓储
    /// </summary>
    public interface IOwnerInfoRepository: IRepository<OwnerInfo>
    {
    }
}
