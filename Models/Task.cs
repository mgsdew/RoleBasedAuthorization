using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoleBasedAuthorization.Models
{
    public partial class Task
    {
        public Task()
        {
        }
        public int TaskId { get; set; }
        public string WorkOrder { get; set; }
        public int? AssignTo { get; set; }
        public string WorkType { get; set; }
        public string TaskName { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public string Client { get; set; }
        public DateTime? ClientDueDate { get; set; }
        public DateTime? TaskCreateDate { get; set; }

    }
}
