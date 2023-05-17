using Microsoft.AspNetCore.Mvc;
using Secrets_Sharing_BE.Constant;
using Secrets_Sharing_BE.Models;
using System.Net;
using Newtonsoft.Json;

namespace Secrets_Sharing_BE.Controllers
{
    public class UserController : BaseApiController
    {        
        private readonly IUnitOfWork _unitOfWork;
        public UserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody]LoginModel model)
        {
            try
            {
                var user = _unitOfWork.UserRepository.GetUserByEmail(model.Email);
                if (user == null)
                {
                    return Error("User Not Found");
                }
                HttpClient client = new HttpClient();
                var parameter = new Dictionary<string, string>()
                {
                    { "client_id","Client"},
                    {"grant_type","password" },
                    {"username",user.Username },
                    {"password",model.Password}
                };
                var content = new FormUrlEncodedContent(parameter);               
                var response = await client.PostAsync
                    (Const.LOGIN_URL_API,content);//this call api in identity to verify user
                var result = await response.Content.ReadAsStringAsync(); 
                var loginResponse = JsonConvert.DeserializeObject<LoginResponse>(result);
                return Success(loginResponse);
            }
            catch (Exception ex)
            {
                return Error(ex.ToString());
            }           
        }
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromForm] RegisterModel model)
        {
            try
            {
                HttpClient httpClient = new HttpClient();         
                var parameter = new Dictionary<string, string>()
                {
                    {"Name",model.Name},
                    {"Password",model.Password},
                    {"Email",model.Email}
                };
                var content = new FormUrlEncodedContent(parameter);               
                var response = await httpClient.PostAsync(Const.REGISTER_URL_API, content);//this call api in indentity to create new user
                var result = response.StatusCode;
                if(result == HttpStatusCode.OK)//if user create success
                {
                    var data = response.Content.ReadAsStringAsync();//this will return userid just created                    
                    var user = new User()
                    {
                        Id = int.Parse(data.Result),
                        Username = model.Name,
                        Email = model.Email,
                    };
                    _unitOfWork.UserRepository.Create(user);
                    _unitOfWork.Save();
                    return Success(user);
                }
                return Error("Register failed");
            }
            catch (Exception ex)
            {
                return Error(ex.ToString());
            }
        }
    }
}
