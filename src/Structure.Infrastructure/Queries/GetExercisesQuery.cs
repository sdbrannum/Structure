using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Structure.Infrastructure.Data;
using FluentValidation;
using Structure.Core.Entities;

namespace Structure.Infrastructure.Queries
{
    public class GetExercisesQuery : IRequest<IEnumerable<Exercise>>
    {
    }

    public class GetExercisesQueryHandler : IRequestHandler<GetExercisesQuery, IEnumerable<Exercise>>
    {
        private readonly IExercisesRepository _repo;

        public GetExercisesQueryHandler(IExercisesRepository repo)
        {
            this._repo = repo;
        }

        public async Task<IEnumerable<Exercise>> Handle(GetExercisesQuery request, CancellationToken cancellationToken)
        {
            return await _repo.GetAllAsync();
        }
    }
}