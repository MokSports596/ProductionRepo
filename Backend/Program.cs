using Microsoft.EntityFrameworkCore;
using MokSportsApp.Data;
using MokSportsApp.Data.Repositories.Interfaces;
using MokSportsApp.Data.Repositories.Implementations;
using MokSportsApp.Services.Interfaces;
using MokSportsApp.Services.Implementations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUserRepository, UserImplementation>();
builder.Services.AddScoped<IFranchiseRepository, FranchiseRepository>();
builder.Services.AddScoped<ITeamRepository, TeamRepository>();
builder.Services.AddScoped<IFranchiseTeamRepository, FranchiseTeamRepository>();
builder.Services.AddScoped<IStatRepository, StatRepository>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IFranchiseService, FranchiseService>();
builder.Services.AddScoped<ITeamService, TeamService>();
builder.Services.AddScoped<IFranchiseTeamService, FranchiseTeamService>();
builder.Services.AddScoped<IStatService, StatService>();

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
