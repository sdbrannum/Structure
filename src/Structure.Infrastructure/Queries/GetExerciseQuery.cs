using Structure.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Structure.Infrastructure.Data;

namespace Structure.Infrastructure.Queries
{
    public class GetExerciseQueryValidator : AbstractValidator<GetExerciseQuery>
    {
        public GetExerciseQueryValidator()
        {
            RuleFor(q => q.Id).NotEmpty();
        }
    }

    public class GetExerciseQuery : IRequest<Exercise?>
    {
        public Guid Id { get; }

        public GetExerciseQuery(Guid id)
        {
            this.Id = id;
        }
    }

    public class GetExerciseQueryHandler : IRequestHandler<GetExerciseQuery, Exercise?>
    {
        private readonly IExercisesRepository _repo;

        public GetExerciseQueryHandler(IExercisesRepository repo)
        {
            this._repo = repo;
        }

        public async Task<Exercise?> Handle(GetExerciseQuery request, CancellationToken cancellationToken)
        {
            return await _repo.GetByIdAsync(request.Id);
        }
    }
}