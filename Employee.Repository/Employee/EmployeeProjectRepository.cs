using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Repository.Employee
{
    public class EmployeeProjectRepository : BaseRepository<Data.Models.Employee.EmployeeProject>
    {
        public EmployeeProjectRepository(Data.AppContext context) : base(context)
        {

        }
    }
}
