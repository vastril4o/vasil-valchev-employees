using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.CQRS.Employee.Commands
{
    public class RemoveAllEmployeeCommand : IRequest<int>
    {

    }

    public class RemoveAllEmployeeCommandHandler : IRequestHandler<RemoveAllEmployeeCommand, int>
    {
        private Data.AppContext _context;
        private readonly IMapper _mapper;

        public RemoveAllEmployeeCommandHandler(Data.AppContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Handle(RemoveAllEmployeeCommand command, CancellationToken cancellationToken)
        {
            foreach (var e in _context.employees)
            {
                _context.Remove(e);
            }

            await _context.SaveChangesAsync(cancellationToken);

            return 0;
        }
    }
}
