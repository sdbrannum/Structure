using Structure.Core.Entities;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Structure.Infrastructure.Queries
{
    public class GetCarsByMakeQueryValidator : AbstractValidator<GetCarsByMakeQuery>
    {
        public GetCarsByMakeQueryValidator()
        {
            RuleFor(c => c.Make).NotNull().NotEmpty();
        }
    }

    public class GetCarsByMakeQuery : IRequest<IEnumerable<Car>>
    {
        public string Make { get; }
        public GetCarsByMakeQuery(string make)
        {
            this.Make = make;
        }
    }

    public class GetCarsByMakeQueryHandler : IRequestHandler<GetCarsByMakeQuery, IEnumerable<Car>>
    {
        public GetCarsByMakeQueryHandler()
        {

        }

        public Task<IEnumerable<Car>> Handle(GetCarsByMakeQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
