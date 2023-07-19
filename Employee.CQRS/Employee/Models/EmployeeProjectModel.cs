using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.CQRS.Employee.Models
{
    public class EmployeeProjectModel
    {
        public int Employee1Id { get; set; }
        public int Employee2Id { get; set; }
        public int ProjectId { get; set; }
        public int Days { get; set; }
    }
}
