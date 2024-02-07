using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetClubApi.Model
{
    public class ClubRegistration
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        [Required]
        public string user_id { get; set; }
        public string club_id { get; set; }
        public string registered_date { get; set; }

        public Boolean isadmin { get; set; }

    }
}
