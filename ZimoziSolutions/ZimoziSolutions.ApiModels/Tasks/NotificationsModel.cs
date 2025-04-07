using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZimoziSolutions.ApiModels.Tasks
{
    public class NotificationsModel
    {
        public NotificationsModel()
        {
            Tasks = new HashSet<TaskModel>();
        }
        public int Id { get; set; }
        public string TaskUpdates { get; set; }

        public virtual ICollection<TaskModel> Tasks { get; set; }
    }
}
