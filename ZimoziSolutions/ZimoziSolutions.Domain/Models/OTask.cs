using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ZimoziSolutions.Domain.Users;
using ZimoziSolutions.Domain.UserTask;

namespace ZimoziSolutions.Domain.Models
{
    public class OTask
    {
        public OTask()
        {
            UserTasks = new HashSet<UserTasks>();
        }
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

        public virtual ICollection<UserTasks> UserTasks { get; set; }
    }
}
