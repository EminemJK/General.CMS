using GeneralCMS.Models.Dto;
using GeneralCMS.Models.ViewModel.Frontend;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GeneralCMS.Application.Services
{
    public interface IOwnerService : IBaseService
    {
        Task<OwnerDto> GetCompanyInfoAsync();

        Task<VContactInfo> GetContactInfo();
    }
}
