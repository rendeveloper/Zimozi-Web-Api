using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ZimoziSolutions.Domain.Users;

namespace ZimoziSolutions.Domain.Models
{
    public class OTask
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime? DueDate { get; set; }

        [ForeignKey("AssignedUserId")]
        public int AssignedUserId { get; set; }
        public virtual User AssignedUser { get; set; }

        [ForeignKey("TaskCommentsId")]
        public int TaskCommentsId { get; set; }
        public virtual TaskComments TaskComments { get; set; }

        [ForeignKey("NotificationsId")]
        public int NotificationsId { get; set; }
        public virtual Notifications Notifications { get; set; }
    }
}
