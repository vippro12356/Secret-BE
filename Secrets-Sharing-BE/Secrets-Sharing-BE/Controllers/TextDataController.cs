using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Secrets_Sharing_BE.Constant;
using Secrets_Sharing_BE.Models;
using System.Security.Claims;

namespace Secrets_Sharing_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TextDataController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IService _service;
        public TextDataController(IService service, IUnitOfWork unitOfWork)
        {
            _service = service;
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        [Route("mytext")]
        [Authorize]//require user login to access this api
        public IActionResult GetTextByUser()
        {
            try
            {
                var user = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;//after login success will return current login userid
                if (string.IsNullOrEmpty(user))
                {
                    return Error("User doesn't exist");
                }
                int userid = int.Parse(user);
                var texts = _unitOfWork.TextDataRepository.GetTextByUser(userid);
                return Success(texts);
            }
            catch (Exception ex)
            {
                return Error(ex.ToString());
            }
        }
        [HttpPost]
        [Route("upload")]
        [Authorize]
        public IActionResult UploadText([FromBody] Request data)
        {
            try
            {
                var user = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(user))
                {
                    return Error("User doesn't exist");
                }
                var userid = User;
                var dt = _service.TextDataService.PrepUpload(data, out string error,int.Parse(user));
                if (!string.IsNullOrEmpty(error))
                {
                    return Error(error);
                }
                _unitOfWork.TextDataRepository.Create(dt);
                _unitOfWork.Save();
                string url = Const.ACCESS_URL_API + dt.Protect;//the url user will receive after upload
                return Success(url, "Upload Succesfully");
            }
            catch (Exception ex)
            {
                return Error(ex.ToString());
            }
        }
        [HttpGet]
        [Route("view/{id}")]
        [AllowAnonymous]//not require user login to access this api
        public IActionResult Access([FromRoute] string id)
        {
            try
            {
                var dt = _unitOfWork.TextDataRepository.GetData(id);
                if (dt == null)
                {
                    return NotFound();
                }
                if (dt.AutoDelete == true)
                {
                    _unitOfWork.TextDataRepository.Delete(dt);//if user want the information to be automatically deleted once it’s accessed
                    _unitOfWork.Save();
                    return Success(dt);
                }
                return Success(dt);
            }
            catch (Exception ex)
            {
                return Error(ex.ToString());
            }
        }
        [HttpDelete]
        [Route("delete/{id}")]
        [Authorize]
        public IActionResult Delete([FromRoute] int id)
        {
            try
            {
                var user = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(user))
                {
                    return Error("User doesn't exist");
                }
                var dt = _unitOfWork.TextDataRepository.Get(id);
                if (dt == null)
                {
                    return NotFound();
                }
                _unitOfWork.TextDataRepository.Delete(dt);
                _unitOfWork.Save();
                return Success("Delete Successfully");
            }
            catch (Exception ex)
            {
                return Error(ex.ToString());
            }
        }
    }
}
