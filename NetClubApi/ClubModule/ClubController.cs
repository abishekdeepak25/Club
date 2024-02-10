using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetClubApi.Model;
using NetClubApi.Model.ResponseModel;
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
        //get the list of club created by user
        public async Task<IActionResult> MyClubs()
        {
            try
            {

                var userClaims = User.FindFirst("id");
                var  id = userClaims.Value;
                
                List<IClubResponse> clubs = await _clubBussinessLogics.getMyClubs(int.Parse(id));
                if (clubs.Any() && clubs[0] is MyClub)
                {
                    // If the first object is of type MyClub, return a list of MyClub objects
                    var myClub = clubs.Cast<MyClub>().ToList();
                    return Ok(myClub);
                }
                else if (clubs.Any() && clubs[0] is RegisterClub)
                {
                    // If the first object is of type RegisterClub, return a list of RegisterClub objects
                    var registerClub = clubs.Cast<RegisterClub>().ToList();
                    return Ok(registerClub);
                }
                else
                {
                    // If the list is empty or doesn't contain objects of expected types, return an empty response or appropriate status code
                    return NoContent();
                }
                


            }
            catch (Exception)
            {
                throw;
            }


        }

        [HttpPost]
        [Authorize]
        //create club
        public async Task<IActionResult> CreateClub(Club club)
        {
            try
            {
                return Ok(await _clubDataAccess.CreateClub(club,int.Parse(User.FindFirst("id").Value)));
            }
            catch(Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Authorize]
        //get the list of club rigistered 
        public async Task<IActionResult> RegisteredClubs()
        {
                try
            {
                var userClaims = User.FindFirst("id");
                return Ok(await _clubBussinessLogics.RegisteredClubs(int.Parse(userClaims.Value)));
            }catch(Exception )
            {
                throw;
            }
        }

        [HttpPost]
        [Authorize]
        //add to club action
        public async Task<IActionResult> JoinClub(string code)
        {
            try
            {
                var claim = User.FindFirst("id");
                return Ok(await _clubDataAccess.ClubRegistration(code, int.Parse(claim.Value)));
            }
            catch(Exception)
            {
                throw;
            }
        }


    }
}
