using Banana.Uow.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeneralCMS.Domain.Repository.Interface
{
    /// <summary>
    /// 基类仓储接口
    /// </summary>
    public interface IBaseRepository<T> where T : class, IEntity
    {
        List<T> GetModelByIdList(int[] ids);
    }
}
