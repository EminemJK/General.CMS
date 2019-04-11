using GeneralCMS.Models.Dto;
using GeneralCMS.Models.ViewModel.Frontend;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GeneralCMS.Application.Services
{
    public interface INewService : IBaseService
    {
        Task<VNewPage> GetNewPageList(int pageNum, int pageSize);

        VNewDetailPage GetNewDetailPage(int id);
    }
}
