using System.ComponentModel.DataAnnotations;

namespace ZimoziSolutions.Domain.TaskHistory
{
    public class TaskHistory
    {
        [Key]
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string FieldName { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public string EntityState { get; set; }
    }
}
