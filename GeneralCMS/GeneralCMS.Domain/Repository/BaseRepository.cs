using Banana.Uow;
using Banana.Uow.Models;
using GeneralCMS.Domain.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeneralCMS.Domain.Repository
{
    /// <summary>
    /// 基础仓储,扩展Banana的Repository<T>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseRepository<T> : Repository<T>, IBaseRepository<T> where T : class, IEntity
    {
        public List<T> GetModelByIdList(int[] ids)
        {
            string where = string.Format("Id in ({0})", string.Join(",", ids));
            return QueryList(where, new { });
        }
    }
}
