using Microsoft.AspNetCore.Mvc;
using MediatR;
using System.Net;
using Structure.Infrastructure.Queries;

namespace Structure.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExercisesController : ControllerBase
    {
        private readonly IMediator _mediatr;
        private readonly ILogger<ExercisesController> _logger;

        public ExercisesController(IMediator mediatr, ILogger<ExercisesController> logger)
        {
            _mediatr = mediatr;
            _logger = logger;
        }

        [HttpGet("{id}", Name = "GetExercise")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<Core.Entities.Exercise>))]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var exercise = await _mediatr.Send(new GetExerciseQuery(id));

            if (exercise == null)
            {
                return NotFound();
            }

            return Ok(exercise);
        }

        [HttpGet(Name = "GetExercises")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<Core.Entities.Exercise>))]
        public async Task<IActionResult> GetAllAsync([FromQuery] string? name = null)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                var searchResults = await _mediatr.Send(new SearchExercisesQuery(name));
                return Ok(searchResults);
            }

            var exercises = await _mediatr.Send(new GetExercisesQuery());
            return Ok(exercises);
        }
    }
}