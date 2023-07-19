using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.CQRS.Employee.Commands
{
    public class RemoveAllEmployeeProjectCommand : IRequest<int>
    {
    }

    public class RemoveAllEmployeeProjectCommandHandler : IRequestHandler<RemoveAllEmployeeProjectCommand, int>
    {
        private Data.AppContext _context;
        private readonly IMapper _mapper;

        public RemoveAllEmployeeProjectCommandHandler(Data.AppContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Handle(RemoveAllEmployeeProjectCommand command, CancellationToken cancellationToken)
        {
            foreach (var ep in _context.employeeProjects)
            {
                _context.Remove(ep);
            }

            await _context.SaveChangesAsync(cancellationToken);

            return 0;
        }
    }
}
