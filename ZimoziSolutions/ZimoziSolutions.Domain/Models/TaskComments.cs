using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZimoziSolutions.Domain.Models
{
    public class TaskComments
    {
        public TaskComments()
        {
            Tasks = new HashSet<OTask>();
        }

        [Key]
        public int Id { get; set; }
        public string Comments { get; set; }

        public virtual ICollection<OTask> Tasks { get; set; }
    }
}
