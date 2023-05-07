using Microsoft.AspNetCore.Authentication;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Globalization;
using TrainzApi.Auth;
using TrainzApi.Services;
using TrainzLib.Models;
using TrainzLib.Operations;
using TrainzLib.Repository;
using TrainzLiteDb;
using TrainzLiteDb.Data;


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
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "BasicAuth", Version = "v1" });
    c.AddSecurityDefinition("basic", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "basic",
        In = ParameterLocation.Header,
        Description = "Basic Authorization header using the Bearer scheme."
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "basic"
                                }
                            },
                            new string[] {}
                    }
                });
});

builder.Services.AddAuthentication("BasicAuthentication")
    .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

builder.Services.AddDbContext<TrainzContext>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICrudRepository<Vagon>, VagonCrudDb>();
builder.Services.AddScoped<ICrudRepository<Way>, WayCrudDb>();
builder.Services.AddScoped<ICrudRepository<Station>, StationCrudDb>();
builder.Services.AddScoped<ICrudRepository<GruzType>, GruzCrudDb>();
builder.Services.AddScoped<ICrudRepository<VagonType>, VagonTypeCrudDb>();
builder.Services.AddScoped<IVagonInfoRepository, VagonInfoDb>();
builder.Services.AddScoped<TrainzOperator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BasicAuth v1"));
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
