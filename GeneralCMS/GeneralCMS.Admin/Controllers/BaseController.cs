using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeneralCMS.Application.AppSetting;
using GeneralCMS.Application.AppSetting.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using GeneralCMS.Models.ViewModel.Admin;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using GeneralCMS.Admin.Config;

namespace GeneralCMS.Admin.Controllers
{
    public class BaseController : Controller
    {
        public BaseController(IServiceProvider serviceProvider)
        {
            Config = serviceProvider.GetService<IConfigFactory<ConfigModel>>().AppSettings;
        }

        private VLoggedModel _currentUser;
        /// <summary>
        /// 当前用户
        /// </summary>
        public VLoggedModel CurrentUser
        {
            get
            {
                if (_currentUser != null)
                    return _currentUser;
                if (HttpContext.User.Claims == null)
                    return null;
                var claims = HttpContext.User.Claims.ToList();
               
                _currentUser = new VLoggedModel();

                _currentUser.ID = Convert.ToInt16(claims.Find(c=>c.Type== ClaimTypes.Sid).Value);
                _currentUser.UserName = claims.Find(c => c.Type == ClaimTypes.Name).Value;
                _currentUser.RealName = claims.Find(c => c.Type == ClaimTypes.Surname).Value;
                _currentUser.Role = claims.Find(c => c.Type == ClaimTypes.Role).Value;
                _currentUser.Mobile = claims.Find(c => c.Type == ClaimTypes.MobilePhone).Value;
                _currentUser.Email = claims.Find(c => c.Type == ClaimTypes.Email).Value;
                _currentUser.IP = HttpContext.Connection.RemoteIpAddress.ToString();

                return _currentUser;
            }
        }

        public ConfigModel Config { get; set; }

        public string GetModelError(ModelStateDictionary model)
        {
            string msg = "";
            foreach (var item in ModelState.Values)
            {
                if (item.Errors.Count > 0)
                {
                    msg = item.Errors[0].ErrorMessage;
                    break;
                }
            }
            return msg;
        }

        public IActionResult ShowError(string msg)
        {
            return new JsonResult(VRequestInfo.ErrorResult(msg));
        }

        public IActionResult ShowSuccess(string msg, object data = null)
        {
            return new JsonResult(VRequestInfo.SuccessResult(msg, data));
        }

        public IActionResult Show(VRequestInfo info)
        {
            return new JsonResult(info);
        }
    }
}