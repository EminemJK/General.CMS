using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeneralCMS.Application.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeneralCMS.Admin.Controllers
{
    [Authorize(Roles = UserType.Administrator + "," + UserType.User)]
    public class ContentMngController : BaseController
    {
        public ContentMngController(IServiceProvider serviceProvider) : base(serviceProvider)
        {

        }

        public IActionResult Index()
        {
            return View();
        }
    }
}