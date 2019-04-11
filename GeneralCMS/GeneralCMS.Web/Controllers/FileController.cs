using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using GeneralCMS.Models.ViewModel;
using GeneralCMS.Models.ViewModel.Admin;
using System.Text;
using System.IO;
using Microsoft.AspNetCore.Cors;
using GeneralCMS.Common.Extend;

namespace GeneralCMS.Web.Controllers
{
    [EnableCors("adminCors")]
    public class FileController : Controller
    {
        /// <summary>
        /// 上传文件
        /// </summary>
        [HttpPost]
        public virtual async Task<IActionResult> Upload([FromServices]IHostingEnvironment env)
        {
            UploadFileModel upload = new UploadFileModel();
            var files = Request.Form.Files; 
            if (files.Count > 0)
            {
                var file = files[0];
                string md5code = string.Empty;
                using (var inputStream = file.OpenReadStream())
                {
                    using (var md5 = System.Security.Cryptography.MD5.Create())
                    {
                        byte[] retVal = md5.ComputeHash(inputStream);
                        StringBuilder md5sb = new StringBuilder();
                        for (int i = 0; i < retVal.Length; i++)
                        {
                            md5sb.Append(retVal[i].ToString("x2"));
                        }
                        md5code = md5sb.ToString();
                    }
                }

                // 文件名完整路径  
                upload.extension = Path.GetExtension(file.FileName);
                upload.fileName = md5code + upload.extension;
                var path = string.Format(@"\images\Upload\{0}\{1}", DateTime.Today.Year.ToString(), DateTime.Today.Month.ToString().PadLeft(2, '0'));
                upload.path = string.Format(@"{0}\{1}", path, upload.fileName);
                upload.fullPath = UrlCommon.CreateUrlPath(Request.GetSiteUri(), upload.path);
                var savedFilePath = env.WebRootPath + path;

                if (!Directory.Exists(savedFilePath))
                {
                    Directory.CreateDirectory(savedFilePath);
                }
                var fullFileNamePath = Path.Combine(savedFilePath, upload.fileName);
                if (!System.IO.File.Exists(fullFileNamePath))
                {
                    try
                    {
                        using (var fileStream = new FileStream(fullFileNamePath, FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }
                    }
                    catch (Exception ex)
                    {

                    }

                }
                return new JsonResult(VRequestInfo.SuccessResult("上传成功", upload));
            }
            return new JsonResult(VRequestInfo.ErrorResult("上传失败"));
        }

     
    }
}