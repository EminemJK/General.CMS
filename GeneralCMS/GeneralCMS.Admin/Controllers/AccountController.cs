using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GeneralCMS.Application;
using GeneralCMS.Application.Common;
using GeneralCMS.Application.Services;
using GeneralCMS.Models.ViewModel.Admin;
using GeneralCMS.Common;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeneralCMS.Admin.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IAccountService accountService; 
        public AccountController(IAccountService accountService, IServiceProvider serviceProvider) :base(serviceProvider)
        {
            this.accountService = accountService; 
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login(string returnUrl = "")
        {
            ViewData["Title"] =  this.Config.SystemName;
            TempData["returnUrl"] = returnUrl; 
            return View(VRequestInfo.DefaultResult("", RandomHelper.GetRandomCode()));
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(VLoginInfo input, string returnUrl = "")
        {
            ViewData["Title"] = this.Config.SystemName;
            VRequestInfo msg = new VRequestInfo();
            if (!ModelState.IsValid)
            {
                msg.Code = VRequestInfo.Error;
                msg.Msg = GetModelError(ModelState);
                msg.Data = RandomHelper.GetRandomCode();
                return View(msg);
            }
            var user = accountService.Login(input.UserName, input.Password);
            if (user != null && user.Id > 0)
            {
                var identity = new ClaimsIdentity(SystemConfig.UserCookie);
                identity.AddClaim(new Claim(ClaimTypes.Sid, user.Id.ToString()));
                identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
                identity.AddClaim(new Claim(ClaimTypes.Surname, user.RealName));
                identity.AddClaim(new Claim(ClaimTypes.Role, UserType.Administrator)); // 暂且都为管理人员
                identity.AddClaim(new Claim(ClaimTypes.MobilePhone,user.Mobile));
                identity.AddClaim(new Claim(ClaimTypes.Email, user.Email));

                await HttpContext.SignInAsync(SystemConfig.UserCookie, new ClaimsPrincipal(identity), new AuthenticationProperties() {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.Now.Add(TimeSpan.FromMinutes(30))
                });
                if (string.IsNullOrEmpty(returnUrl))
                {
                    returnUrl = TempData["returnUrl"]?.ToString();
                }
                if (!string.IsNullOrEmpty(returnUrl)&& returnUrl!="/")
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }
            }
            else
            {
                msg.Code = VRequestInfo.Error;
                msg.Msg = "用户名或密码不正确";
                msg.Data = RandomHelper.GetRandomCode();
                return View(msg);
            }
        }

        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(SystemConfig.UserCookie);
            return RedirectToAction("Login");
        }

        [AllowAnonymous]
        public IActionResult Denied()
        {
            ViewData["Title"] = this.Config.SystemName;
            if (CurrentUser != null)
            {
                return View("DeniedInHome");
            }
            return View();
        }

        public IActionResult DeniedInHome()
        {
            return View();
        }
    }
}