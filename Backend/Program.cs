using Microsoft.EntityFrameworkCore;
using MokSportsApp.Data;
using MokSportsApp.Data.Repositories.Interfaces; // Ensure this namespace is correct
using MokSportsApp.Data.Repositories.Implementations;
using MokSportsApp.Services.Interfaces; // Ensure this namespace is correct
using MokSportsApp.Services.Implementations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<UserInterface, UserImplementation>(); // Corrected interfaces
builder.Services.AddScoped<IUserService, UserService>(); // Corrected interfaces

builder.Services.AddControllers();
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

app.UseAuthorization();

app.MapControllers();

app.Run();
