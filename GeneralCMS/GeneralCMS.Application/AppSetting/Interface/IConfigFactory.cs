using GeneralCMS.Application.AppSetting.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeneralCMS.Application.AppSetting.Interface
{
    public interface IConfigFactory<T> where T : BaseConfigModel
    {
        T AppSettings { get; set; }
    }
}
