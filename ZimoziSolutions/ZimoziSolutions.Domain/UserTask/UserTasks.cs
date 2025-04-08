using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZimoziSolutions.Domain.Models;
using ZimoziSolutions.Domain.Users;

namespace ZimoziSolutions.Domain.UserTask
{
    public class UserTasks
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int TaskId { get; set; }
        public OTask Task { get; set; }
    }
}
