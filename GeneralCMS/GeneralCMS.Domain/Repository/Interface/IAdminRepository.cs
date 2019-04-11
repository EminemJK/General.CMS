using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Banana.Uow.Interface;
using Banana.Uow.Models;
using GeneralCMS.Models.ViewModel.Admin;
using GeneralCMS.Models.Entities;

namespace GeneralCMS.Domain.Repository.Interface
{
    /// <summary>
    /// 管理员信息仓储
    /// </summary>
    public interface IAdminRepository : IRepository<AdminUserInfo>
    {
        AdminUserInfo GetUserInfo(string userName, string psw); 

        IPage<AdminUserInfo> GetUserInfoByQueryCondition(VUserListConditionInput input, string orderBy, bool isAsc);


    }
}
