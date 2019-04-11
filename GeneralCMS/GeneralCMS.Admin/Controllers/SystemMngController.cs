using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeneralCMS.Application.Common;
using GeneralCMS.Application.Services;
using GeneralCMS.Models.Dto;
using GeneralCMS.Models.ViewModel.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeneralCMS.Admin.Controllers
{
    [Authorize(Roles = UserType.Administrator)]
    public class SystemMngController : BaseController
    {
        private readonly IAccountService accountService;
        private readonly IActionLog actionLog;
        public SystemMngController(IServiceProvider serviceProvider, IAccountService accountService, IActionLog actionLog) : base(serviceProvider)
        {
            this.accountService = accountService;
            this.actionLog = actionLog;
        }

        #region 用户管理

        [HttpGet]
        public IActionResult UserList()
        {
            return View();
        }

        /// <summary>
        /// 用户列表数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult QueryUserList(VUserListConditionInput input)
        {
            if (input == null)
                input = new VUserListConditionInput();
            int pageCount;
            var data = accountService.GetUserList(input, out pageCount);
            return new JsonResult(new VModelTableOutput<AdminUserDto>(data, input.draw, pageCount));
        }

        /// <summary>
        /// 新增、保存修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult SaveUser(AdminUserDto input)
        {
            if (!ModelState.IsValid)
            {
                return ShowError(GetModelError(ModelState));
            }
            if (input.Id == 0)
            {
                var res = accountService.AddUser(input);
                if (res > 0)
                {
                    return ShowSuccess("新增成功");
                }
                else
                {
                    return ShowError("新增失败");
                }
            }
            else
            {
                var msg = accountService.EditUser(input);
                if (string.IsNullOrEmpty(msg))
                {
                    return ShowSuccess("保存成功");
                }
                else
                {
                    return ShowError(msg);
                }
            }
        }

        /// <summary>
        /// 查询用户详细信息
        /// </summary>
        [HttpGet]
        public IActionResult QueryUserDetail(int userid)
        {
            if (userid <= 0)
            {
                return ShowError("ID不存在");
            }
            var data = accountService.GetUserInfo(userid);
            data.Password = data.Password2 = "😁，Use email to contact me: lio.huang@qq.com";
            return ShowSuccess("查询成功", data);
        }

        /// <summary>
        /// 删除用户
        /// </summary> 
        [HttpPost]
        public IActionResult DeleteUser(int userId)
        {
            if (userId <= 0)
            {
                return ShowError("ID不存在");
            }
            var res = accountService.DeleteUser(userId);
            if (string.IsNullOrEmpty(res))
            {
                return ShowSuccess("删除成功");
            }
            return ShowError("删除失败");
        }
        #endregion

        #region 系统日志
        /// <summary>
        /// 系统日志
        /// </summary>
        /// <returns></returns>
        public IActionResult Logs(string sdate = "", string edate = "")
        {
            if (!DateTime.TryParse(sdate, out DateTime sdt))
            {
                sdt = DateTime.Now.AddDays(-30);
            }
            if (!DateTime.TryParse(edate, out DateTime edt))
            {
                edt = DateTime.Now;
            }
            if (edt < sdt)
            {
                return ShowError("查询条件不正确，结束时间不能小于开始时间");
            }
            var logs = actionLog.GetLogs(sdt, edt);
            return View(logs);
        }
        #endregion
    }
}