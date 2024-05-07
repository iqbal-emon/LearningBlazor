using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace forPractice.Data
{
    public class MemberModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }        
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } = "Member";

          public int Age { get; set; }  
        public DateTime JoiningDate { get; set; }
    }
}
