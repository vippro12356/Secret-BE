using Microsoft.AspNetCore.Mvc;
using Secrets_Sharing_BE.Models;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Secrets_Sharing_BE.Constant;

namespace Secrets_Sharing_BE.Controllers
{
    public class FileDataController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IService _services;

        public FileDataController(IUnitOfWork unitOfWork, IService services)
        {
            _unitOfWork = unitOfWork;
            _services = services;
        }
        [HttpGet]
        [Route("myfile")]
        [Authorize]//require user login to access this api
        public IActionResult GetFileByUser()
        {
            try
            {
                var user = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;//after login success will return current login userid
                if (string.IsNullOrEmpty(user))
                {
                    return Error("User doesn't exist");
                }
                int userid = int.Parse(user);
                var files = _unitOfWork.FileDataRepository.GetFiles(userid);
                return Success(files);
            }
            catch (Exception ex)
            {
                return Error(ex.ToString());
            }
        }
        [HttpPost]
        [Route("upload")]
        [Authorize]
        public IActionResult UploadFile([FromForm] FileDataModel data)
        {
            try
            {
                var user = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;               
                if(string.IsNullOrEmpty(user))
                {                    
                    return Error("User doesn't exist");
                }
                if (data.File == null)
                {
                    return Error("No file selected");
                }
                var dt = _services.FileDataService.PrepCreate(data.File, data.AutoDelete, out string error,int.Parse(user));
                if (!string.IsNullOrEmpty(error))
                {
                    return Error(error);
                }
                _services.FileDataService.Upload(data.File, out string message);
                if (!string.IsNullOrEmpty(message))
                {
                    return Error(message);
                }
                _unitOfWork.FileDataRepository.Create(dt);
                _unitOfWork.Save();
                string url = Const.DOWNLOAD_URL_API + dt.Protect;//the url user will receive after upload
                return Success(url, "Upload Successfully");
            }
            catch (Exception ex)
            {
                return Error(ex.ToString());
            }
        }
        [HttpGet]
        [Route("download/{id}")]
        [AllowAnonymous]//not require user login to access this api
        public IActionResult Download([FromRoute] string id)
        {
            try
            {
                var file = _unitOfWork.FileDataRepository.GetFile(id);
                if (file == null)
                {
                    return NotFound();
                }
                var data = _services.FileDataService.Download(file.FileName);
                if (data == null || data.Data == null || string.IsNullOrEmpty(data.ContentType))
                {
                    return Error("Can't download this file");
                }
                return File(data.Data, data.ContentType, fileDownloadName: file.FileName);
            }
            catch (Exception ex)
            {
                return Error(ex.ToString());
            }
        }
        [HttpDelete]
        [Route("delete/{id}")]
        [Authorize]
        public IActionResult Delete([FromRoute] string id)
        {
            try
            {
                var user = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(user))
                {
                    return Error("User doesn't exist");
                }
                var file = _unitOfWork.FileDataRepository.GetFile(id);
                if (file == null)
                {
                    return NotFound();
                }
                _services.FileDataService.DeleteFile(file.FileName, out string message);
                if (!string.IsNullOrEmpty(message))
                {
                    return Error(message);
                }
                _unitOfWork.FileDataRepository.Delete(file);
                _unitOfWork.Save();
                return Success("File Delete Successfully");
            }
            catch (Exception ex)
            {
                return Error(ex.ToString());
            }
        }
    }
}
