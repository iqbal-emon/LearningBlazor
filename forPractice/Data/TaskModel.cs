using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace forPractice.Data
{
    public class TaskModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? TaskDescription { get; set; }
        public DateTime? Deadline { get; set; }
        public string? Status { get; set; } = "pending";

        // Foreign key for MemberModel
        public int MemberId { get; set; }
        [ForeignKey("MemberId")]
        public virtual MemberModel Member { get; set; }
    }
}
