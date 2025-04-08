using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZimoziSolutions.ApiModels.Tasks;
using ZimoziSolutions.ApiModels.Users;

namespace ZimoziSolutions.ApiModels.UserTask
{
    public class UserTasksModel
    {
        public int UserId { get; set; }
        public UserCustomModel User { get; set; }
        public int TaskId { get; set; }
        public TaskModel Task { get; set; }
    }
}
