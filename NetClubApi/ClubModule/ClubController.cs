using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetClubApi.Model;
using System.Security.Claims;

namespace NetClubApi.ClubModule
{

    
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ClubController:ControllerBase
    {

        
        
        private readonly IClubBussinessLogics  _clubBussinessLogics;
        private readonly IClubDataAccess _clubDataAccess;

        public ClubController(IClubBussinessLogics clubBussinessLogic,IClubDataAccess clubDataAccess)
        {
            _clubBussinessLogics = clubBussinessLogic;
            _clubDataAccess = clubDataAccess;
            
        }
        // my club action
        [HttpGet]
        [Authorize]
        public async Task<List<MyClub>> MyClubs()
        {
            try
            {

                var userClaims = User.FindFirst("id");
                var  id = userClaims.Value;
                //if (userClaims == null || userClaims.Value == null)
                //    throw new Exception("id not found in the user claims");

                //var id = userClaims.Value ?? throw new Exception("id value is null in the user claims");
                return  await _clubBussinessLogics.getMyClubs(int.Parse(id));


            }
            catch (Exception)
            {
                throw;
            }


        }

        [HttpPost]
        [Authorize]
        public async Task<string> CreateClub(Club club)
        {
            try
            {
                return await _clubDataAccess.CreateClub(club,int.Parse(User.FindFirst("id").Value));
            }
            catch(Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<List<MyClub>> RegisteredClubs()
        {
                try
            {
                var userClaims = User.FindFirst("id");
                return await _clubBussinessLogics.RegisteredClubs(int.Parse(userClaims.Value));
            }catch(Exception )
            {
                throw;
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<String> ClubRegistration(string code)
        {
            try
            {
                var claim = User.FindFirst("id");
                return await _clubDataAccess.ClubRegistration(code,int.Parse(claim.Value));
            }
            catch(Exception)
            {
                throw;
            }
        }


    }
}
