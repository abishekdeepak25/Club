using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetClubApi.Comman;
using NetClubApi.Model;

namespace NetClubApi.UserModule
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserDataAccess _dataAccess;
        private readonly IHelper _helper;

        
        public  UserController(IUserDataAccess dataAccess,IHelper helper)
        {
                _dataAccess = dataAccess;
                 _helper = helper;
        }

        [HttpGet]
        [Authorize]
        public async Task<string> testing()
        {
            return "hlooo";
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<User> Authentication(User user)
        {
            user = _dataAccess.AuthenticateUser(user);
            //if (user.IsSuccess)
            //    user.Token = _helper.generateToken(user.email);
            return user;
        }

        [HttpPost]
        [AllowAnonymous]
        public  User Registration(User user)
        {
            user =  _dataAccess.RegisterUser(user);
            //if (user.IsSuccess)
            //    user.Token = _helper.generateToken(user.email);
            return user;
        }
        
    }
}
