using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.CQRS.Project.Commands
{
    public class RemoveAllProjectCommand : IRequest<int>
    {

    }

    public class RemoveAllProjectCommandHandler : IRequestHandler<RemoveAllProjectCommand, int>
    {
        private Data.AppContext _context;
        private readonly IMapper _mapper;

        public RemoveAllProjectCommandHandler(Data.AppContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Handle(RemoveAllProjectCommand command, CancellationToken cancellationToken)
        {
            foreach (var p in _context.projects)
            {
                _context.Remove(p);
            }

            await _context.SaveChangesAsync(cancellationToken);

            return 0;
        }
    }
}
