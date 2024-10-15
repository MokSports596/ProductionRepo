using Microsoft.EntityFrameworkCore;
using MokSportsApp.Data;
using MokSportsApp.Data.Repositories.Interfaces;
using MokSportsApp.Data.Repositories.Implementations;
using MokSportsApp.Services.Interfaces;
using MokSportsApp.Services.Implementations;
using System.Text.Json.Serialization;
using DotNetEnv;  // Import the DotNetEnv package to load environment variables

var builder = WebApplication.CreateBuilder(args);

// Load environment variables from the .env file
Env.Load();

// Retrieve the connection string from the environment variable
var connectionString = Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING");

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));  // Use the environment variable

// Register your repositories
builder.Services.AddScoped<IUserRepository, UserImplementation>();
builder.Services.AddScoped<IFranchiseRepository, FranchiseRepository>();
builder.Services.AddScoped<ITeamRepository, TeamRepository>();
builder.Services.AddScoped<IUserStatsRepository, UserStatsRepository>();
builder.Services.AddScoped<ILeagueRepository, LeagueRepository>();
builder.Services.AddScoped<IUserLeagueRepository, UserLeagueRepository>();
builder.Services.AddScoped<IGameRepository, GameRepository>();
builder.Services.AddScoped<IDraftRepository, DraftRepository>();
builder.Services.AddScoped<IDraftPickRepository, DraftPickRepository>();

// Register your services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IFranchiseService, FranchiseService>();
builder.Services.AddScoped<ITeamService, TeamService>();
builder.Services.AddScoped<IUserStatsService, UserStatsService>();
builder.Services.AddScoped<ILeagueService, LeagueService>();
builder.Services.AddScoped<IUserLeagueService, UserLeagueService>();
builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<IDraftService, DraftService>();

// Configure JSON serialization to handle circular references
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    options.JsonSerializerOptions.MaxDepth = 64; // Adjust if needed
});

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

app.Urls.Add("http://0.0.0.0:5062");

app.Run();
