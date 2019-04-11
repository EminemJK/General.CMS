using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneralCMS.Admin.ViewComponents
{
    public class HeaderViewComponent: BaseViewComponent
    {
        public override IViewComponentResult Invoke()
        {
            return View(CurrentUser);
        }
    }
}
