using Structure.Core.Entities;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Structure.Infrastructure.Data;

namespace Structure.Infrastructure.Queries
{
    public class SearchExercisesQueryValidator : AbstractValidator<SearchExercisesQuery>
    {
        public SearchExercisesQueryValidator()
        {
            RuleFor(c => c.Name).NotNull().NotEmpty();
        }
    }

    public class SearchExercisesQuery : IRequest<IEnumerable<Exercise>>
    {
        public string Name { get; }

        public SearchExercisesQuery(string name)
        {
            this.Name = name;
        }
    }

    public class SearchExercisesByNameQueryHandler : IRequestHandler<SearchExercisesQuery, IEnumerable<Exercise>>
    {
        private readonly IExercisesRepository _repo;

        public SearchExercisesByNameQueryHandler(IExercisesRepository repo)
        {
            this._repo = repo;
        }

        public async Task<IEnumerable<Exercise>> Handle(SearchExercisesQuery request, CancellationToken cancellationToken)
        {
            return await _repo.GetAllAsync(exercise =>
                exercise.Name.StartsWith(request.Name, StringComparison.OrdinalIgnoreCase) ||
                exercise.Aliases.Any(a => a.StartsWith(request.Name, StringComparison.OrdinalIgnoreCase))
            );
        }
    }
}