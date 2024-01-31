
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetClubApi.Comman;
using NetClubApi.Model;

namespace NetClubApi.UserModule
{



    public interface IUserDataAccess
    {
        public Task<User> AuthenticateUser(User user);
        public Task<User> RegisterUser(User user);
    }
    public class UserDataAccess : IUserDataAccess
    {

        private readonly IHelper _helper;
        private readonly NetClubDbContext _netClubDbContext;
        public UserDataAccess(IHelper helper,NetClubDbContext netClubDbContext)
        {
            _helper = helper;
            _netClubDbContext = netClubDbContext;
        }

        // this method recive the user object and check is the user is available in the data base or not
        // if the user is not in the data base => userNotfound
        //else return the token
        public async Task<User> AuthenticateUser(User user)
        {

            try
            {


                var reader =  _netClubDbContext.Users.FirstOrDefault(users => users.Email == user.Email);

                if (reader != null)
                        {
                            
                            user.Password = _helper.EncodeBase64(user.Password.ToString());
                            if (reader.Password.CompareTo(user.Password) == 0)
                            {
                        user.FirstName = reader.FirstName;
                        user.LastName = reader.LastName;
                        user.UserName = reader.UserName;
                        user.Message.Add("valid password");
                        user.IsSuccess = true;
                               
                            }
                            else
                            {
                                user.Message.Add("Invalid Password");
                                user.IsSuccess = false;
                            }

                        }
                        else
                        {
                            user.Message.Add("Invalid Userid");
                            user.IsSuccess = false;
                        }
                        
                    
                
            }
            catch (Exception )
            {
                throw;
            }

            return user;


        }

        public  async Task<User> RegisterUser(User user)
        {
            try
            {
                var reader =  _netClubDbContext.Users.SingleOrDefault(users => users.Email == user.Email);
                if (reader != null)
                {
                    user.Message.Add("user already register");
                    user.IsSuccess = false;
                }
                else
                {
                    user.Password = _helper.EncodeBase64(user.Password);
                    user.Message.Add("registered Successfully");
                    user.IsSuccess = true;
                    _netClubDbContext.Users.Add(user);

                     _netClubDbContext.SaveChanges();
                 
                }
            }
            catch(Exception)
            {
                throw;
            }
            return user;
        }
    }
}
