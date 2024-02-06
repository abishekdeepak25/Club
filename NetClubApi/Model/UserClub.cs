using System.ComponentModel.DataAnnotations;

namespace NetClubApi.Model
{
    public class UserClub
    {
        [Key]
        public int Id { get; set; }
        public int user_id { get; set; }
        public int club_id { get; set; }
        public int league_played {get;set;}
        public string join_date { get; set; }

    }
}
