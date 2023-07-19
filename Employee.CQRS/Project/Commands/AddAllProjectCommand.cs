using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.CQRS.Project.Commands
{
    public class AddAllProjectCommand : IRequest<int>
    {
        public IEnumerable<Data.Models.Project.Project> projects { get; set; }
    }

    public class AddAllProjectCommandHandler : IRequestHandler<AddAllProjectCommand, int>
    {
        private Data.AppContext _context;
        private readonly IMapper _mapper;

        public AddAllProjectCommandHandler(Data.AppContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Handle(AddAllProjectCommand command, CancellationToken cancellationToken)
        {
            foreach (var p in command.projects)
            {
                await _context.projects.AddAsync(_mapper.Map<Data.Models.Project.Project>(p));
            }

            await _context.SaveChangesAsync(cancellationToken);

            return 0;
        }
    }
}
