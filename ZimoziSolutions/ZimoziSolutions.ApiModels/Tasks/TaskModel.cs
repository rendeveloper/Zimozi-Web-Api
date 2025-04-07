
namespace ZimoziSolutions.ApiModels.Tasks
{
    public class TaskModel
    {
        public int TaskId { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime? DueDate { get; set; }
        public int AssignedUserId { get; set; }
    }
}
