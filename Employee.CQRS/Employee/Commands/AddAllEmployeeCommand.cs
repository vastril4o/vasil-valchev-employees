using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.CQRS.Employee.Commands
{
    public class AddAllEmployeeCommand : IRequest<int>
    {
        public IEnumerable<Data.Models.Employee.Employee> employees { get; set; }
    }

    public class AddAllEmployeeCommandHandler : IRequestHandler<AddAllEmployeeCommand, int>
    {
        private Data.AppContext _context;
        private readonly IMapper _mapper;

        public AddAllEmployeeCommandHandler(Data.AppContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Handle(AddAllEmployeeCommand command, CancellationToken cancellationToken)
        {
            foreach (var e in command.employees)
            {
                await _context.employees.AddAsync(_mapper.Map<Data.Models.Employee.Employee>(e));
            }

            await _context.SaveChangesAsync(cancellationToken);

            return 0;
        }
    }
}
