using Azure;
using FoggelApi.MiddleWare;
using Microsoft.EntityFrameworkCore;
using Repositories.Data;
using Repositories.Repositories;
using Services.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(option => 
    option.UseSqlServer(builder.Configuration.GetConnectionString("DevConnectionString")));

// Add services to the container.
builder.Services.AddScoped<ObservationService>();
builder.Services.AddScoped<ObservationRepository>();
builder.Services.AddScoped<BirdsService>();
builder.Services.AddScoped<BirdsRepository>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new()
    {
        Title = "Fôggel API",
        Version = "v1"
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
    policy =>
    {
        policy.WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();
app.UseCors("AllowReactApp");
app.UseMiddleware<ExceptionHandler>();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Fôggel API");
});
app.Run();
