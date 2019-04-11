using Banana.Uow.Interface;
using GeneralCMS.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeneralCMS.Domain.Repository.Interface
{
    /// <summary>
    /// 分类表仓储
    /// </summary>
    public interface ICategoryRepository : IRepository<CategoryInfo>
    {
    }
}
