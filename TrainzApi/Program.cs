using Serilog;
using System.Globalization;
using TrainzLib.Models;
using TrainzLib.Operations;
using TrainzLib.Repository;
using TrainzMock;

#region log config
var lc = new LoggerConfiguration()
   .MinimumLevel.Debug()
   .WriteTo.Console(formatProvider: new CultureInfo("ru-RU"))
   .WriteTo.File("logs/wr.txt", rollingInterval: RollingInterval.Day, formatProvider: new CultureInfo("ru-RU"));
Log.Logger = lc.CreateLogger();
#endregion

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICrudRepository<Vagon>, VagonCrudMock>();
builder.Services.AddScoped<ICrudRepository<Way>, WayCrudMock>();
builder.Services.AddScoped<ICrudRepository<Station>, StationCrudMock>();
builder.Services.AddScoped<ICrudRepository<GruzType>, GruzCrudMock>();
builder.Services.AddScoped<ICrudRepository<VagonType>, VagonTypeCrudMock>();
builder.Services.AddScoped<TrainzOperator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
