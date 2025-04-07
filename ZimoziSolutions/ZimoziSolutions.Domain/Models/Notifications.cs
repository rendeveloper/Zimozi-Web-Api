using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZimoziSolutions.Domain.Models
{
    public class Notifications
    {
        public Notifications()
        {
            Tasks = new HashSet<OTask>();
        }
        [Key]
        public int Id { get; set; }
        public string TaskUpdates { get; set; }

        public virtual ICollection<OTask> Tasks { get; set; }
    }
}
