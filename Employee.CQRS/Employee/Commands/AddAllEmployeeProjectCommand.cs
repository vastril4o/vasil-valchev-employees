using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Employee.Data.Models.Employee;

namespace Employee.CQRS.Employee.Commands
{
    public class AddAllEmployeeProjectCommand : IRequest<int>
    {
        public IEnumerable<EmployeeProject> employeeProjects { get; set; }
    }

    public class AddAllEmployeeProjectCommandHandler : IRequestHandler<AddAllEmployeeProjectCommand, int>
    {
        private Data.AppContext _context;
        private readonly IMapper _mapper;

        public AddAllEmployeeProjectCommandHandler(Data.AppContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Handle(AddAllEmployeeProjectCommand command, CancellationToken cancellationToken)
        {
            foreach (var ep in command.employeeProjects)
            {
                await _context.employeeProjects.AddAsync(_mapper.Map<EmployeeProject>(ep));
            }

            await _context.SaveChangesAsync(cancellationToken);

            return 0;
        }
    }
}
