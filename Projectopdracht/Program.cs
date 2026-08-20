using Microsoft.EntityFrameworkCore;
using Projectopdracht.Data;
using Projectopdracht.Interface;
using Projectopdracht.Services;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddControllers()
    .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// dATABASE of IN-Memory
bool useDatabase = true;

if (useDatabase)
{
    // Gebruikt MySQL
    builder.Services.AddScoped<ILogisticsService, EfLogisticsService>();
}
else
{
    // Gebruikt de statische lijst (In-memory)
    builder.Services.AddScoped<ILogisticsService, LogisticsService>();
}

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();