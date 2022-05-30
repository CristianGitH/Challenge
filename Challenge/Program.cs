using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Challenge.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ChallengeContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ChallengeContext") ?? throw new InvalidOperationException("Connection string 'ChallengeContext' not found.")));

// Add services to the container.

builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(options =>
{
    options.WithOrigins("http://localhost:3000");
    options.AllowAnyMethod();
    options.AllowAnyHeader();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
