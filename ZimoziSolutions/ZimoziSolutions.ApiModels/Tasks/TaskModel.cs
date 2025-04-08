
using ZimoziSolutions.ApiModels.UserTask;

namespace ZimoziSolutions.ApiModels.Tasks
{
    public class TaskModel
    {
        public TaskModel()
        {
            UserTasks = new HashSet<UserTasksModel>();
        }
        public int TaskId { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime? DueDate { get; set; }
        public int AssignedUserId { get; set; }
        public int TaskCommentsId { get; set; }
        public int NotificationsId { get; set; }

        public virtual ICollection<UserTasksModel> UserTasks { get; set; }
    }
}
