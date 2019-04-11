using GeneralCMS.Application.AppSetting.Interface;
using GeneralCMS.Application.AppSetting.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeneralCMS.Application.Services
{
    public interface IBaseService
    {
        IServiceProvider GetServiceProvider();
    }
}
