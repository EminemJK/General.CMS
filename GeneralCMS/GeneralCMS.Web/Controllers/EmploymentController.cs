using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GeneralCMS.Application.Services;

namespace GeneralCMS.Web.Controllers
{
    public class EmploymentController : Controller
    {
        private readonly IEmploymentService employmentService;

        public EmploymentController(IEmploymentService employmentService)
        {
            this.employmentService = employmentService; 
        }
        /// <summary>
        /// 招聘信息
        /// </summary>
        public IActionResult List(int navId)
        {
            var data = employmentService.GetVRecruitmentList();
            data.NavigationId = navId;
            return View(data);
        }
    }
}