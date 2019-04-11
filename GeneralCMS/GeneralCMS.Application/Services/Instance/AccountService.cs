using System;
using System.Collections.Generic;
using System.Text;
using GeneralCMS.Models.Dto;
using GeneralCMS.Domain.Repository.Interface;
using GeneralCMS.Common;
using Banana.Utility.Common;
using GeneralCMS.Models.Entities;
using Banana.Utility.Encryption;
using GeneralCMS.Common.LogUtility;
using GeneralCMS.Models.ViewModel.Admin;

namespace GeneralCMS.Application.Services.Instance
{
    public class AccountService : BaseService, IAccountService
    {
        private readonly IAdminRepository adminRepository;
        public AccountService(IAdminRepository adminRepository, ILoggerHelper logger, IServiceProvider serviceProvider) : base(logger, serviceProvider)
        {
            this.adminRepository = adminRepository;
        }
        public AdminUserDto Login(string userName, string passWord)
        {
            AdminUserDto res = new AdminUserDto();
            var model = adminRepository.GetUserInfo(userName, MD5.Encrypt(passWord));
            if (model != null)
            {
                res = ModelConvertUtil<AdminUserInfo, AdminUserDto>.ModelCopy(model);
            }
            return res;
        }

        public List<AdminUserDto> GetUserList(VUserListConditionInput input, out int pageCount)
        {
            var user = adminRepository.GetUserInfoByQueryCondition(input, "CreateTime", false);
            pageCount = user.pageCount;
            return ModelConvertUtil<AdminUserInfo, AdminUserDto>.ModelCopy(user.data);
        }

        public AdminUserDto GetUserInfo(int userId)
        {
            var user = adminRepository.Query(userId) ?? new AdminUserInfo();
            return ModelConvertUtil<AdminUserInfo, AdminUserDto>.ModelCopy(user);
        }

        public int AddUser(AdminUserDto user)
        {
            var userModel = ModelConvertUtil<AdminUserDto, AdminUserInfo>.ModelCopy(user);
            userModel.CreateTime = DateTime.Now;
            userModel.IsDisable = Models.Enum.EYesOrNo.No;
            userModel.Password = MD5.Encrypt(user.Password);
            return (int)adminRepository.Insert(userModel);
        }

        public string EditUser(AdminUserDto user)
        {
            string res = "";
            var oldUser = GetUserInfo(user.Id);
            if (oldUser.Id == 0)
            {
                res = "用户不存在";
            }
            else
            {
                var userModel = ModelConvertUtil<AdminUserDto, AdminUserInfo>.ModelCopy(user);
                if (string.IsNullOrEmpty(user.Password))
                {
                    //未修改密码
                    userModel.Password = oldUser.Password; 
                }
                else
                {
                    //修改密码，加密
                    userModel.Password = MD5.Encrypt(user.Password);
                }
                res = adminRepository.Update(userModel) ? "" : "修改失败";
            } 
            return res;
        }

        public string DeleteUser(int userId)
        {
            var res = "";
            var user = adminRepository.Query(userId);
            if (user == null || user.Id == 0)
            {
                res = "用户不存在";
            }
            else
            {
                res = adminRepository.Delete(user) ? "" : "删除失败";
            }
            return res;
        }
    }
}
