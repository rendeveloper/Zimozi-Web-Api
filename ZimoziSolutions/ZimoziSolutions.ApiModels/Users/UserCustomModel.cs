using System;
using System.Collections.Generic;
using System.Linq;
using ZimoziSolutions.ApiModels.Tasks;

namespace ZimoziSolutions.ApiModels.Users
{
    public class UserCustomModel
    {
        public UserCustomModel()
        {
            Tasks = new HashSet<TaskModel>();
        }

        public int Id { get; set; }
        public Guid Guid { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }

        public virtual ICollection<TaskModel> Tasks { get; set; }
    }
}
