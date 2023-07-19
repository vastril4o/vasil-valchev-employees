using Employee.Data;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Repository.Employee
{
    public class EmployeeRepository : BaseRepository<Data.Models.Employee.Employee>
    {
        public EmployeeRepository(Data.AppContext context) : base(context)
        {

        }
    }
}
