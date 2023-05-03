using MeetMe.Modules.Matching.Api;
using MeetMe.Modules.Profiles.Api;
using MeetMe.Modules.Users.Api;
using MeetMe.Shared;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();

builder.Services
    .AddUsersModule(builder.Configuration)
    .AddProfilesModule(builder.Configuration)
    .AddMatchingModule(builder.Configuration)
    .AddSharedFramework(builder.Configuration);

var app = builder.Build();

app.UseUsersModule();
app.UseProfilesModule();
app.UseMatchingModule();
app.UseSharedFramework();

app.ExposeUsersApi();
app.ExposeProfilesApi();
app.ExposeMatchingApi();

app.UseSwagger();
app.UseSwaggerUI();

app.Run();