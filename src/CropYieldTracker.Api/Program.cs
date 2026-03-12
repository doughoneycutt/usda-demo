using CropYieldTracker.Application;
using CropYieldTracker.Application.Abstractions;
using CropYieldTracker.Contracts.Fields;
using CropYieldTracker.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplicationServices()
    .AddInfrastructureServices();

var app = builder.Build();

app.UseHttpsRedirection();

var api = app.MapGroup("/api");

api.MapGet("/fields", async (IFieldService fieldService, CancellationToken cancellationToken) =>
{
    var fields = await fieldService.GetFieldsAsync(cancellationToken);
    return Results.Ok(fields);
});

api.MapGet("/fields/{fieldId:guid}", async (Guid fieldId, IFieldService fieldService, CancellationToken cancellationToken) =>
{
    var field = await fieldService.GetFieldAsync(fieldId, cancellationToken);
    return field is null ? Results.NotFound() : Results.Ok(field);
});

api.MapPost("/fields", async (SaveFieldRequest request, IFieldService fieldService, CancellationToken cancellationToken) =>
{
    var field = await fieldService.SaveFieldAsync(request, cancellationToken);
    return Results.Created($"/api/fields/{field.Id}", field);
});

api.MapGet("/crops", async (IFieldService fieldService, CancellationToken cancellationToken) =>
{
    var crops = await fieldService.GetCropsAsync(cancellationToken);
    return Results.Ok(crops);
});

api.MapPost("/crops", async (SaveCropRequest request, IFieldService fieldService, CancellationToken cancellationToken) =>
{
    var crop = await fieldService.SaveCropAsync(request, cancellationToken);
    return Results.Created($"/api/fields/{crop.FieldId}", crop);
});

api.MapGet("/reports/fields", async (IFieldService fieldService, CancellationToken cancellationToken) =>
{
    var reports = await fieldService.GetFieldReportsAsync(cancellationToken);
    return Results.Ok(reports);
});

app.Run();
