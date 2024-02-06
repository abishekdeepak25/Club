using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetClubApi.Model;

namespace NetClubApi.ClubModule
{

    
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ClubController:ControllerBase
    {

        private readonly ClubBussinessLogic  _clubBussinessLogics;
        private readonly ClubDataAccess _clubDataAccess;

        public ClubController(ClubBussinessLogic clubBussinessLogic,ClubDataAccess clubDataAccess)
        {
            _clubBussinessLogics = clubBussinessLogic;
            _clubDataAccess = clubDataAccess;
        }
        // my club action
        [HttpGet]
        [Authorize]
        public async Task<List<MyClub>> myClubs()
        {
            try
            {

                var userClaims = User.FindFirst("id");
                var  id = userClaims.Value;
                //if (userClaims == null || userClaims.Value == null)
                //    throw new Exception("id not found in the user claims");

                //var id = userClaims.Value ?? throw new Exception("id value is null in the user claims");
                return  await _clubDataAccess.getMyClubs(id);


            }
            catch (Exception)
            {
                throw;
            }


        }
        



    }
}
