using MediatR;
using Microsoft.AspNetCore.Mvc;
using Structure.Core.Entities;
using Structure.Infrastructure.Queries;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/exercise/:id", async (IMediator mediatr, [FromRoute] Guid id) =>
{
    var exercise = await mediatr.Send(new GetExerciseQuery(id));

    if (exercise != null)
    {
        Results.Ok(exercise);
    }
    else
    {
        Results.NotFound();
    }
})
.Produces<Exercise>(StatusCodes.Status200OK)
.Produces(StatusCodes.Status404NotFound);

app.MapGet("/exercises", async (IMediator mediatr, [FromQuery] string? name) =>
{
    if (!string.IsNullOrWhiteSpace(name))
    {
        var searchResults = await mediatr.Send(new SearchExercisesQuery(name));
        return searchResults;
    }

    var exercises = await mediatr.Send(new GetExercisesQuery());
    return exercises;
})
.Produces<IEnumerable<Exercise>>(StatusCodes.Status200OK)
.Produces(StatusCodes.Status404NotFound);

app.Run();