using Employee.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Repository.Project
{
    public class ProjectRepository : BaseRepository<Data.Models.Project.Project>
    {
        public ProjectRepository(Data.AppContext context) : base(context)
        {

        }
    }
}
