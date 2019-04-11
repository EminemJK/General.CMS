using Banana.Uow;
using GeneralCMS.Domain.Repository.Interface;
using GeneralCMS.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeneralCMS.Domain.Repository
{
    /// <summary>
    /// 分类表仓储
    /// </summary>
    public class CategoryRepository : Repository<CategoryInfo>, ICategoryRepository
    {
    }
}
