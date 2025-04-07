using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZimoziSolutions.ApiModels.Tasks
{
    public class TaskCommentsModel
    {
        public TaskCommentsModel()
        {
            Tasks = new HashSet<TaskModel>();
        }
        public int Id { get; set; }
        public string Comments { get; set; }

        public virtual ICollection<TaskModel> Tasks { get; set; }
    }
}
