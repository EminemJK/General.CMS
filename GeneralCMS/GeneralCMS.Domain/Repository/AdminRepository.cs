using Banana.Uow;
using Banana.Uow.Models;
using GeneralCMS.Domain.Repository.Interface;
using GeneralCMS.Models.Entities;
using GeneralCMS.Models.ViewModel.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneralCMS.Domain.Repository
{
    /// <summary>
    /// 管理员信息仓储
    /// </summary>
    public class AdminRepository : Repository<AdminUserInfo>, IAdminRepository
    {
        public AdminUserInfo GetUserInfo(string userName, string psw)
        {
            return QueryList("UserName=@UserName and Password=@Password", new { Password = psw, UserName = userName }).FirstOrDefault();
        }

        public IPage<AdminUserInfo> GetUserInfoByQueryCondition(VUserListConditionInput input, string orderBy, bool isAsc)
        {
            List<string> sqlwhere = new List<string>();
            string whereStr = "";
            if (!string.IsNullOrEmpty(input.realname))
            {
                sqlwhere.Add(" RealName like @RealName ");
            }
            if (!string.IsNullOrEmpty(input.username))
            {
                sqlwhere.Add(" UserName like @UserName ");
            }
            if (!string.IsNullOrEmpty(input.phone))
            {
                sqlwhere.Add(" Phone = @Phone ");
            }
            if (!string.IsNullOrEmpty(input.email))
            {
                sqlwhere.Add(" Email = @Email ");
            }
            if (input.isdisable > -1)
            {
                sqlwhere.Add(" IsDisable = @IsDisable ");
            }
            if (sqlwhere.Count > 0)
            {
                whereStr = string.Join("and", sqlwhere);
            }

            return QueryList(input.pageNum, input.pageSize, whereString: whereStr,
                param: new { RealName = "%" + input.realname + "%",
                    UserName ="%"+input.username+"%",
                    Phone = input.phone,
                    Email = input.email,
                    IsDisable = input.isdisable }, 
                order: orderBy, asc: isAsc);
        }
    }
}
