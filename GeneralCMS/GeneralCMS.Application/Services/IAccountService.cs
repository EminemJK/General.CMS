using GeneralCMS.Models.Dto;
using GeneralCMS.Models.ViewModel.Admin;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeneralCMS.Application.Services
{
    public interface IAccountService : IBaseService
    {
        [AOP.AOPServiceLog("登录")]
        AdminUserDto Login(string userName, string passWord);

        [AOP.AOPServiceLog("查询用户列表")]
        List<AdminUserDto> GetUserList(VUserListConditionInput input, out int pageCount);

        [AOP.AOPServiceLog("查询用户详细信息")]
        AdminUserDto GetUserInfo(int userId);

        [AOP.AOPServiceLog("新增用户")]
        int AddUser(AdminUserDto user);

        [AOP.AOPServiceLog("修改用户信息")]
        string EditUser(AdminUserDto user);

        [AOP.AOPServiceLog("删除")]
        string DeleteUser(int userId);
    }
}
