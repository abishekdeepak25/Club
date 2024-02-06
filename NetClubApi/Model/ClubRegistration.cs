using System.ComponentModel.DataAnnotations;

namespace NetClubApi.Model
{
    public class ClubRegistration
    {

        [Key]
        public int Id { get; set; }
        public int user_id { get; set; }
        public int club_id { get; set; }
        public string registered_date { get; set; }

        public Boolean isadmin { get; set; }

    }
}
