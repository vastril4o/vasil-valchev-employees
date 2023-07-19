using AutoMapper;
using Employee.CQRS.Project.Commands;
using Employee.Data.Models.Employee;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.CQRS.Employee.Commands
{
    public class AddCsvCommand : IRequest<int>
    {
        public string file { get; set; }
    }

    public class AddCsvCommandHandler : IRequestHandler<AddCsvCommand, int>
    {
        private Data.AppContext _context;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public AddCsvCommandHandler(Data.AppContext context, IMapper mapper, IMediator mediator)
        {
            _context = context;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<int> Handle(AddCsvCommand command, CancellationToken cancellationToken)
        {
            await _mediator.Send(new RemoveAllEmployeeProjectCommand());
            await _mediator.Send(new RemoveAllEmployeeCommand());
            await _mediator.Send(new RemoveAllProjectCommand());

            IEnumerable<EmployeeProject> employeeProjects = readEmployeeProjectCsv(command.file);

            var employees = _mapper.Map<IEnumerable<EmployeeProject>, IEnumerable<Data.Models.Employee.Employee>>(employeeProjects.DistinctBy(x => x.EmployeeId));
            var projects = _mapper.Map<IEnumerable<EmployeeProject>, IEnumerable<Data.Models.Project.Project>>(employeeProjects.DistinctBy(x => x.ProjectId));
            
            await _mediator.Send(new AddAllEmployeeCommand() { employees = employees });
            await _mediator.Send(new AddAllProjectCommand() { projects = projects });
            await _mediator.Send(new AddAllEmployeeProjectCommand() { employeeProjects = employeeProjects });

            return 0;
        }

        private List<EmployeeProject> readEmployeeProjectCsv(string file)
        {
            List<EmployeeProject> list =
                File.ReadAllLines(file).Select(v => FromCsv(v)).ToList();

            EmployeeProject FromCsv(string csvLine)
            {
                string[] values = csvLine.Split(',');
                EmployeeProject model = new EmployeeProject();

                model.EmployeeId = Convert.ToInt32(values[0]);
                model.ProjectId = Convert.ToInt32(values[1]);

                DateTime dtFrom;
                if (DateTime.TryParse(values[2], out dtFrom))
                {
                    model.DateFrom = dtFrom;
                }

                DateTime dtTo;
                if (DateTime.TryParse(values[3], out dtTo))
                {
                    model.DateTo = dtTo;
                }

                return model;
            }

            return list;
        }
    }
}
