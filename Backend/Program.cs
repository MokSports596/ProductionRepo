using Microsoft.EntityFrameworkCore;
using MokSportsApp.Data;
using MokSportsApp.Data.Repositories.Interfaces;
using MokSportsApp.Data.Repositories.Implementations;
using MokSportsApp.Services.Interfaces;
using MokSportsApp.Services.Implementations;
using System.Text.Json.Serialization;
using Hangfire;
using MokSportsApp.Services.BackgroundServices;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddHangfire(options =>

    options.SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddHangfireServer();

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
builder.Services.AddScoped<ISeasonRepository, SeasonRepository>();
builder.Services.AddScoped<ITradeRepository, TradeRepository>();


// Register your services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IFranchiseService, FranchiseService>();
builder.Services.AddScoped<ITeamService, TeamService>();
builder.Services.AddScoped<IUserStatsService, UserStatsService>();
builder.Services.AddScoped<ILeagueService, LeagueService>();
builder.Services.AddScoped<IUserLeagueService, UserLeagueService>();
builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<IDraftService, DraftService>();
builder.Services.AddScoped<ITradeService, TradeService>();
builder.Services.AddScoped<ISeasonService, SeasonService>();

builder.Services.AddScoped<ScoringService>();


// Configure JSON serialization to handle circular references
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    options.JsonSerializerOptions.MaxDepth = 64; // Adjust if needed
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseHangfireDashboard();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

#region Firebase

// Initialize Firebase Admin SDK
//FirebaseApp.Create(new AppOptions()
//{
//    Credential = GoogleCredential.FromJsonParameters(new JsonCredentialParameters()
//    {
//        Type = "service_account",
//        ProjectId = "MY_PROJECT_ID",
//        PrivateKeyId = "MY_PRIVATE_KEY_ID",
//        PrivateKey = "PRIVATE_KEY",
//        ClientEmail = "EMAIL",
//        ClientId = "CLIENT_ID",
//        TokenUri = "https://oauth2.googleapis.com/token",
//        UniverseDomain = "googleapis.com",
//    })
//});

#endregion

RecurringJob.AddOrUpdate<ExpireTrade>("ExpireTrades", job => job.ExecuteAsync(), Cron.Hourly);
RecurringJob.AddOrUpdate<LockTeamsNotification>("LockTeamsNotification", job => job.ExecuteAsync(), "0 0 * * 2-4"); //At 12:00 AM, Tuesday through Thursday
RecurringJob.AddOrUpdate<WeeklyStandingNotification>("WeeklyStandingSundayNotification", job => job.ExecuteAsync(), "59 11 * * 7"); //At 11:59 on Sunday
RecurringJob.AddOrUpdate<WeeklyStandingNotification>("WeeklyStandingMondayNotification", job => job.ExecuteAsync(), "55 17 * * 1"); //At 05:55 on Monday.
RecurringJob.AddOrUpdate<WeeklyTeamPerformanceNotification>("WeeklyTeamPerformanceNotification", job => job.ExecuteAsync(), "0 0 * * 2"); //At 12:00 AM on Tuesday.
RecurringJob.AddOrUpdate<WeeklyTopPlayerNotification>("WeeklyTopPlayerNotification", job => job.ExecuteAsync(), "0 0 * * 2"); //At 12:00 AM on Tuesday.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


app.Run();

