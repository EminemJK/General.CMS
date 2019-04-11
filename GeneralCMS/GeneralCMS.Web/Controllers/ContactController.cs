using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeneralCMS.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace GeneralCMS.Web.Controllers
{
    public class ContactController : Controller
    {
        private readonly IOwnerService ownerService;
        public ContactController(IOwnerService ownerService)
        {
            this.ownerService = ownerService;
        }

        public async Task<IActionResult> Index(int navId)
        {
            var data =await ownerService.GetContactInfo();
            data.NavigationId = navId;
            return View(data);
        } 
    }
}