using Banana.Uow.Interface;
using GeneralCMS.Models.Entities;
using GeneralCMS.Models.Enum;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GeneralCMS.Domain.Repository.Interface
{
    /// <summary>
    /// 轮播图片仓储
    /// </summary>
    public interface IImgPlayInfoRepository : IBaseRepository<ImgPlayInfo>, IRepository<ImgPlayInfo>
    {
        Task<List<ImgPlayInfo>> GetImgPlayEnabledAsync(int navigationID);

        Task<List<ImgPlayInfo>> GetImgPlayByTypeAsync(EImageType imageType);

        Task<List<ImgPlayInfo>> GetImgPlaysAsync(int navigationID, EImageType imageType);

        Task<bool> DeleteImgPlaysAsync(List<int> ids);

        Task<List<ImgPlayInfo>> GetAllImgPlaysAsync(int navigationID, EImageType imageType);
    }
}
