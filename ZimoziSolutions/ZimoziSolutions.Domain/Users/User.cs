
using System.ComponentModel.DataAnnotations;
using ZimoziSolutions.Domain.Models;
using ZimoziSolutions.Domain.UserTask;

namespace ZimoziSolutions.Domain.Users
{
    public class User
    {
        public User()
        {
            Tasks = new HashSet<OTask>();
            UserTasks = new HashSet<UserTasks>();
        }

        [Key]
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }

        public virtual ICollection<OTask> Tasks { get; set; }
        public virtual ICollection<UserTasks> UserTasks { get; set; }
    }
}
