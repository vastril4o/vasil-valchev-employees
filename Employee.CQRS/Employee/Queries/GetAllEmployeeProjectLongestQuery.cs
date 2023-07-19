using AutoMapper;
using Employee.CQRS.Employee.Models;
using MediatR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.CQRS.Employee.Queries
{
    public class GetAllEmployeeProjectLongestQuery : IRequest<IEnumerable<EmployeeProjectModel>>
    {

    }

    public class GetAllEmployeeProjectLongestQueryHandler : IRequestHandler<GetAllEmployeeProjectLongestQuery, IEnumerable<EmployeeProjectModel>>
    {
        private Data.AppContext _context;
        private readonly IMapper _mapper;

        public GetAllEmployeeProjectLongestQueryHandler(Data.AppContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EmployeeProjectModel>> Handle(GetAllEmployeeProjectLongestQuery request, CancellationToken cancellationToken)
        {
            List<EmployeeProjectModel> filtered = filterEmployeeProjects();

            return filtered;
        }

        private List<EmployeeProjectModel> filterEmployeeProjects()
        {
            var projects = _context.projects.AsEnumerable().DistinctBy(x => x.ProjectId).OrderBy(x => x.ProjectId).ToList();
            var employeeProjects = _context.employeeProjects.OrderBy(x => x.ProjectId).ThenBy(x => x.EmployeeId).ToList();

            List<EmployeeProjectModel> filtered = new();
            foreach (var p in projects)
            {
                EmployeeProjectModel employeeProjectModel = new();
                foreach (var ep in employeeProjects.Where(x => x.ProjectId == p.ProjectId))
                {
                    var allEmployee1Projects = employeeProjects.Where(x => x.ProjectId == p.ProjectId && x.EmployeeId == ep.EmployeeId).ToList();
                    var allEmployee2Projects = employeeProjects.Where(x => x.ProjectId == p.ProjectId && x.EmployeeId != ep.EmployeeId && x.EmployeeId > ep.EmployeeId).ToList();
                    
                    foreach (var ep1 in allEmployee1Projects)
                    {
                        if (employeeProjectModel.Employee1Id != ep1.EmployeeId)
                        {
                            if (employeeProjectModel.Employee1Id != 0 && employeeProjectModel.Days > 0) filtered.Add(employeeProjectModel);
                            employeeProjectModel = new();
                            employeeProjectModel.ProjectId = ep1.ProjectId;
                            employeeProjectModel.Employee1Id = ep1.EmployeeId;
                        }

                        foreach (var ep2 in allEmployee2Projects)
                        {
                            if (employeeProjectModel.Employee2Id == 0 || employeeProjectModel.Employee2Id == ep2.EmployeeId)
                            {
                                // can be null
                                if (!ep1.DateTo.HasValue) ep1.DateTo = DateTime.Now;
                                if (!ep2.DateTo.HasValue) ep2.DateTo = DateTime.Now;

                                int days = calculateDays(ep1.DateFrom, ep1.DateTo.Value, ep2.DateFrom, ep2.DateTo.Value);
                                employeeProjectModel.Days = employeeProjectModel.Days + days;
                                employeeProjectModel.Employee2Id = ep2.EmployeeId;
                            }
                        }
                    }
                }
            }

            return filtered.OrderByDescending(x => x.Days).ToList();
        }

        private int calculateDays(DateTime dt1From, DateTime dt1To, DateTime dt2From, DateTime dt2To)
        {
            DateTime dtFrom = DateTime.Now;
            DateTime dtTo = DateTime.Now;

            if (dt1From <= dt2From) dtFrom = dt2From;
            else dtFrom = dt1From;
            if (dt1To >= dt2To) dtTo = dt2To;
            else dtTo = dt1To;

            return (dtTo - dtFrom).Days;
        }
    }
}
