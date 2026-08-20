using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Projectopdracht.Data;
using Projectopdracht.MinimalApi.DTOs;
using Projectopdracht.Models;
using Projectopdracht.MinimalApi.Services;
using Projectopdracht.MinimalApi.Interface;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Logistics Minimal API", Version = "v1" });
});

// Dependency Injection: Switch tussen Database (EF) en In-Memory (Mock)
bool useDatabase = true;
if (useDatabase)
    builder.Services.AddScoped<ILogisticsService, EfLogisticsService>();
else
    builder.Services.AddScoped<ILogisticsService, LogisticsService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// DEPOT ENDPOINTS

app.MapGet("/api/depots", async (ILogisticsService service) =>
    Results.Ok(await service.GetAllDepotsAsync()))
    .WithTags("Depots");

app.MapGet("/api/depots/{id:int}", async (int id, ILogisticsService service) =>
{
    var depot = await service.GetDepotByIdAsync(id);
    return depot is null
        ? Results.NotFound(new { message = $"Depot {id} niet gevonden." })
        : Results.Ok(depot);
}).WithName("GetDepotById").WithTags("Depots");

app.MapPost("/api/depots", async (DepotCreateDto dto, ILogisticsService service) =>
{
    var created = await service.AddDepotAsync(dto);
    return Results.CreatedAtRoute("GetDepotById", new { id = created.Id }, created);
}).WithTags("Depots");

app.MapPut("/api/depots/{id:int}", async (int id, DepotUpdateDto dto, ILogisticsService service) =>
{
    var success = await service.UpdateDepotAsync(id, dto);
    return success ? Results.NoContent() : Results.NotFound(new { message = $"Depot {id} niet gevonden." });
}).WithTags("Depots");

app.MapDelete("/api/depots/{id:int}", async (int id, ILogisticsService service) =>
{
    var success = await service.DeleteDepotAsync(id);
    return success ? Results.NoContent() : Results.NotFound(new { message = $"Depot {id} niet gevonden." });
}).WithTags("Depots");


// CONTAINER ENDPOINTS

app.MapGet("/api/containers", async (ILogisticsService service) =>
    Results.Ok(await service.GetAllContainersAsync()))
    .WithTags("Containers");

app.MapGet("/api/containers/{id:int}", async (int id, ILogisticsService service) =>
{
    var container = await service.GetContainerByIdAsync(id);
    return container is null
        ? Results.NotFound(new { message = $"Container {id} niet gevonden." })
        : Results.Ok(container);
}).WithName("GetContainerById").WithTags("Containers");

app.MapPost("/api/containers", async (ContainerCreateDto dto, ILogisticsService service) =>
{
    var depot = await service.GetDepotByIdAsync(dto.DepotId);
    if (depot is null) return Results.BadRequest(new { message = "Het opgegeven DepotId bestaat niet." });

    var created = await service.AddContainerAsync(dto);
    return Results.CreatedAtRoute("GetContainerById", new { id = created.Id }, created);
}).WithTags("Containers");

app.MapPut("/api/containers/{id:int}", async (int id, ContainerUpdateDto dto, ILogisticsService service) =>
{
    var success = await service.UpdateContainerAsync(id, dto);
    return success ? Results.NoContent() : Results.NotFound(new { message = $"Container {id} niet gevonden." });
}).WithTags("Containers");

app.MapDelete("/api/containers/{id:int}", async (int id, ILogisticsService service) =>
{
    var success = await service.DeleteContainerAsync(id);
    return success ? Results.NoContent() : Results.NotFound(new { message = $"Container {id} niet gevonden." });
}).WithTags("Containers");


// TRANSPORT ENDPOINTS

app.MapGet("/api/transports", async (ILogisticsService service) =>
    Results.Ok(await service.GetAllTransportsAsync()))
    .WithTags("Transports");

app.MapGet("/api/transports/{id:int}", async (int id, ILogisticsService service) =>
{
    var transport = await service.GetTransportByIdAsync(id);
    return transport is null
        ? Results.NotFound(new { message = $"Transport {id} niet gevonden." })
        : Results.Ok(transport);
}).WithName("GetTransportById").WithTags("Transports");

app.MapPost("/api/transports", async (TransportCreateDto dto, ILogisticsService service) =>
{
    var container = await service.GetContainerByIdAsync(dto.ContainerId);
    if (container is null) return Results.BadRequest(new { message = "Container bestaat niet." });

    var created = await service.AddTransportAsync(dto);
    return Results.CreatedAtRoute("GetTransportById", new { id = created.Id }, created);
}).WithTags("Transports");

app.MapPut("/api/transports/{id:int}", async (int id, TransportUpdateDto dto, ILogisticsService service) =>
{
    var success = await service.UpdateTransportAsync(id, dto);
    return success ? Results.NoContent() : Results.NotFound(new { message = $"Transport {id} niet gevonden." });
}).WithTags("Transports");

app.MapDelete("/api/transports/{id:int}", async (int id, ILogisticsService service) =>
{
    var success = await service.DeleteTransportAsync(id);
    return success ? Results.NoContent() : Results.NotFound(new { message = $"Transport {id} niet gevonden." });
}).WithTags("Transports");

app.Run();