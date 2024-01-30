using System.ComponentModel.DataAnnotations.Schema;

namespace NetClubApi.Model
{
    public class User
    {
        public int id { get; set; }

        public string user_name { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }

        public string password { get; set; }
        public string email { get; set; }
        public string phone_number { get; set; }

        [NotMapped]
        public bool IsSuccess { get; set; }
        [NotMapped]
        public List<string> Message { get; set; }
        [NotMapped]
        public string Token { get; set; }

        
    }
}
