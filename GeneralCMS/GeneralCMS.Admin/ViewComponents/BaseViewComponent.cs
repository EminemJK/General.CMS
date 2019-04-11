using GeneralCMS.Models.ViewModel.Admin;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GeneralCMS.Admin.ViewComponents
{
    public abstract class BaseViewComponent: ViewComponent
    {
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

                _currentUser.ID = Convert.ToInt16(claims.Find(c => c.Type == ClaimTypes.Sid).Value);
                _currentUser.UserName = claims.Find(c => c.Type == ClaimTypes.Name).Value;
                _currentUser.RealName = claims.Find(c => c.Type == ClaimTypes.Surname).Value;
                _currentUser.Role = claims.Find(c => c.Type == ClaimTypes.Role).Value;
                _currentUser.Mobile = claims.Find(c => c.Type == ClaimTypes.MobilePhone).Value;
                _currentUser.Email = claims.Find(c => c.Type == ClaimTypes.Email).Value;
                _currentUser.IP = HttpContext.Connection.RemoteIpAddress.ToString();

                return _currentUser;
            }
        }

        public abstract IViewComponentResult Invoke();
    }
}
