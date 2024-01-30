
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetClubApi.Comman;
using NetClubApi.Model;

namespace NetClubApi.UserModule
{



    public interface IUserDataAccess
    {
        public User AuthenticateUser(User user);
        public User RegisterUser(User user);
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
        public User AuthenticateUser(User user)
        {

            try
            {


                var reader =  _netClubDbContext.user_detail.FirstOrDefault(users => users.email == user.email);

                if (reader != null)
                        {
                            
                            user.password = _helper.EncodeBase64(user.password.ToString());
                            if (reader.password.CompareTo(user.password) == 0)
                            {
                        user.first_name = reader.first_name;
                        user.last_name = reader.last_name;
                        user.user_name = reader.user_name;
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

        public  User RegisterUser(User user)
        {
            try
            {
                var reader =  _netClubDbContext.user_detail.SingleOrDefault(users => users.email == user.email);
                if (reader != null)
                {
                    user.Message.Add("user already register");
                    user.IsSuccess = false;
                }
                else
                {
                    user.password = _helper.EncodeBase64(user.password);
                    user.Message.Add("registered Successfully");
                    user.IsSuccess = true;
                    _netClubDbContext.user_detail.Add(user);

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
