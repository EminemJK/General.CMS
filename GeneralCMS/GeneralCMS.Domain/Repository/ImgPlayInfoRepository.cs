using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Banana.Uow;
using GeneralCMS.Domain.Repository.Interface;
using GeneralCMS.Models.Entities;
using GeneralCMS.Models.Enum;

namespace GeneralCMS.Domain.Repository
{
    /// <summary>
    /// 轮播图片仓储
    /// </summary>
    public class ImgPlayInfoRepository : BaseRepository<ImgPlayInfo>, IImgPlayInfoRepository
    {
        public async Task<List<ImgPlayInfo>> GetImgPlayByTypeAsync(EImageType imageType)
        {
            var res = await QueryListAsync("IsDisable =@IsDisable and Type = @Type", new { IsDisable = (int)EYesOrNo.No, Type = (int)imageType }, "Sort", asc: true);
            return res.ToList();
        }

        public async Task<List<ImgPlayInfo>> GetImgPlayEnabledAsync(int navigationID)
        {
            var res = await QueryListAsync("IsDisable =@IsDisable and NavigationID = @NavigationID", new { IsDisable = (int)EYesOrNo.No, NavigationID = navigationID }, "Sort", asc: true);
            return res.ToList();
        }

        public async Task<List<ImgPlayInfo>> GetImgPlaysAsync(int navigationID, EImageType imageType)
        {
            var res = await QueryListAsync("IsDisable =@IsDisable and Type = @Type and NavigationID = @NavigationID", new { IsDisable = (int)EYesOrNo.No, Type = (int)imageType, NavigationID = navigationID }, "Sort", asc: true);
            return res.ToList();
        }

        public async Task<List<ImgPlayInfo>> GetAllImgPlaysAsync(int navigationID, EImageType imageType)
        {
            var res = await QueryListAsync("Type = @Type and NavigationID = @NavigationID", new {Type = (int)imageType, NavigationID = navigationID }, "Sort", asc: true);
            return res.ToList();
        }


        public async Task<bool> DeleteImgPlaysAsync(List<int> ids)
        {
            string where = string.Format("Id in ({0})",string.Join(",",ids));
            return await DeleteAsync(where, new { });
        }
    }
}
