using MeetMe.Modules.Profiles.Api;
using MeetMe.Modules.Users.Api;
using MeetMe.Shared;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();

builder.Services
    .AddUsersModule(builder.Configuration)
    .AddProfilesModule(builder.Configuration)
    .AddSharedFramework(builder.Configuration);

var app = builder.Build();

app.UseUsersModule();
app.UseProfilesModule();
app.UseSharedFramework();
app.ExposeUsersApi();
app.ExposeProfilesApi();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();