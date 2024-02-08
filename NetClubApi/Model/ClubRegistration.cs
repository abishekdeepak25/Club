using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetClubApi.Model
{
    public class ClubRegistration
    {

        [Key]
        
        public int Id { get; set; }
        [Required]
        public int user_id { get; set; }
        public int club_id { get; set; }
        public DateTime registered_date { get; set; }

        public Boolean isadmin { get; set; }

    }
}
